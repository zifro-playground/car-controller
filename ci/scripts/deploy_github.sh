#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

# https://developer.github.com/v3/repos/releases/#create-a-release

# 1. create release draft
# 2. upload artifacts
# 3. publish release

repo=${1?Local repo folder required.}
artifacts=${2?Folder containing artifacts.}
: ${PLAYGROUND_CARCONTROLLER_VERSION?}
: ${CIRCLE_REPOSITORY_URL?}
: ${CIRCLE_BUILD_URL?}
: ${CIRCLE_PROJECT_USERNAME?}
: ${CIRCLE_PROJECT_REPONAME?}
: ${CIRCLE_SHA1?}
: ${COMMIT_RANGE?}
: ${GITHUB_USER_ID?}
: ${GITHUB_API_KEY?}

PROJECT_NAME="Styr Podden"
PROJECT_VERSION="$PLAYGROUND_CARCONTROLLER_VERSION"

if ! [ -x "$(command -v jq)" ]
then
    echo "Error: jq is not installed"
    exit 1
fi

echo "Deployment for project: $PROJECT_NAME, v$PROJECT_VERSION"
echo "Using artifacts from: $artifacts"
echo

# Reading changelogs
cd $repo
echo "Fetching changelog from '$repo'"
changeset="$(git diff --shortstat $COMMIT_RANGE)"
echo "Changeset: $changeset"

echo "Referencing range '$COMMIT_RANGE'"
changelog="$(git --no-pager log --oneline --decorate $COMMIT_RANGE)"
echo "Changelog:"
echo "$changelog"
echo

TAG="v$PROJECT_VERSION"
if [ "$(git tag -l "$TAG")" ]
then
    # Tag duplication.
    echo "Tag \"$TAG\" already existed. Aborting."
    exit 0
fi

# Create tags
echo ">>> Tagging"
set +e

tagMessage="$PROJECT_NAME v$PROJECT_VERSION
This tag was created autonomously by a script in the CircleCI workflow.

:shipit: $CIRCLE_BUILD_URL
:octocat: https://github.com/$CIRCLE_PROJECT_USERNAME/$CIRCLE_PROJECT_REPONAME/commit/$CIRCLE_SHA1"

git tag -s "$TAG" -m "$MESSAGE"
local TAG_STATUS=$?
if [ $TAG_STATUS -ne 0 ]
then
    echo "<<< Unexpected error during tagging \"$TAG\". Aborting."
    exit 1
else
    echo "Added tag \"$TAG\", message \"$MESSAGE\""
fi

echo ">>> Pushing to $CIRCLE_REPOSITORY_URL"
git push --follow-tags

# Create github release draft

function escapeJson {
    : ${1?}
    local val=${1//\\/\\\\} # \ 
    # val=${val//\//\\\/} # / 
    # val=${val//\'/\\\'} # ' (not strictly needed ?)
    val=${val//\"/\\\"} # " 
    val=${val//	/\\t} # \t (tab)
    # val=${val//^M/\\\r} # \r (carriage return)
    val="$(echo "$val" | tr -d '\r')" # \r (carrige return)
    val=${val//
/\\\n} # \n (newline)
    val=${val//^L/\\\f} # \f (form feed)
    val=${val//^H/\\\b} # \b (backspace)
    echo -n "$val"
}

body="# $PROJECT_NAME release

## Changelog

$changelog"

releaseName="$PROJECT_NAME v$PROJECT_VERSION"
data="{
    \"tag_name\": \"$TAG\",
    \"target_commitish\": \"$CIRCLE_SHA1\",
    \"name\": \"$releaseName\",
    \"body\": \"$(escapeJson "$body")\",
    \"draft\": true
}"
url="https://api.github.com/repos/$CIRCLE_PROJECT_USERNAME/$CIRCLE_PROJECT_REPONAME/releases"

echo "Creating release on github with request:"
echo "$url"
echo "$data"

releaseJson="$(curl -u $GITHUB_USER_ID:$GITHUB_API_KEY -X POST --data "$data" "$url")"
releaseId="$(echo "$releaseJson" | jq '.id' -r)"
echo "export DEPLOY_RELEASE_ID='$releaseId'" >> $BASH_ENV

releaseUrl="$(echo "$releaseJson" | jq '.html_url' -r)"
artifactsUploadUrlTemplate="$(echo "$releaseJson" | jq '.upload_url' -r)"

if ! [ "$artifactsUploadUrlTemplate" ]
then
    echo "No upload url for artifacts found. Aborting."

    echo
    echo "Response:"
    echo "$releaseJson"
    exit 3
fi

echo "Using upload url: $artifactsUploadUrlTemplate"
artifactsUploadUrlBare="$(echo "$artifactsUploadUrlTemplate" | grep -oP '^[^{]+')"
function artifactsUploadUrl {
    # artifactsUploadUrl <fileName> [label]
    : ${1?Artifact file name}
    : ${2:=}

    if [ "$2" ]
    then
        echo "$artifactsUploadUrlBare?name=$1&label=$2"
    else
        echo "$artifactsUploadUrlBare?name=$1"
    fi
}

# Upload artifacts
function artifactsUpload {
    # artifactsUpload <fileName> [label]
    artifact=${1?Artifact path}
    label=${2:-"$(basename "$artifact")"}
    
    url="$(artifactsUploadUrl "$artifact" "$label")"
    contentType="$(file -b --mime-type "$artifact")"

    echo "Uploading: $label"
    echo "$url"

    curl -u $GITHUB_USER_ID:$GITHUB_API_KEY \
         -H "Content-Type: $contentType" \
         --data-binary @$artifact "$url"
}

while read -r artifact
do
    artifactsUpload $artifact
done < <(find "$artifacts" -type f)

echo
echo "Upload of artifacts complete"
echo

# Publish github release
url="https://api.github.com/repos/$CIRCLE_PROJECT_USERNAME/$CIRCLE_PROJECT_REPONAME/releases/$releaseId"
data="{
    \"draft\": false
}"

echo "Publishing release, removing draft flag, with request:"
echo "$url"
echo "$data"

curl -u $GITHUB_USER_ID:$GITHUB_API_KEY \
     -X PATCH --data "$data" "$url"

# Export variables
echo "export DEPLOY_CHANGESET='$changeset'" >> $BASH_ENV
echo "export DEPLOY_TAG='$TAG'" >> $BASH_ENV
echo "export DEPLOY_STATUS='success'" >> $BASH_ENV
echo "export DEPLOY_RELEASE_URL='$releaseUrl'" >> $BASH_ENV
echo "export DEPLOY_RELEASE_NAME='$releaseName'" >> $BASH_ENV

# is written directly on response from github
#echo "export DEPLOY_RELEASE_ID='$releaseId'" >> $BASH_ENV
