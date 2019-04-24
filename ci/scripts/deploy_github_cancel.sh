#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

: ${CIRCLE_PROJECT_REPONAME?}
: ${CIRCLE_PROJECT_USERNAME?}

: ${DEPLOY_RELEASE_ID:=}
: ${GITHUB_USER_ID?}
: ${GITHUB_API_KEY?}

if ! [ -x "$(command -v jq)" ]
then
    echo "Error: jq is not installed"
    exit 1
fi

if ! [ "$DEPLOY_RELEASE_ID" ]
then
    echo "No release was added. Aborting."
    exit 0
fi

url="https://api.github.com/repos/$CIRCLE_PROJECT_USERNAME/$CIRCLE_PROJECT_REPONAME/releases/$DEPLOY_RELEASE_ID"

echo "Removing release $DEPLOY_RELEASE_ID with request:"
echo "$url"

curl -u $GITHUB_USER_ID:$GITHUB_API_KEY -X DELETE "$url"
