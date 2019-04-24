#!/bin/bash

DESTINATION=${1?Artifacts destination folder}

: ${CIRCLE_BUILD_NUM?}
: ${CIRCLE_API_KEY?}
: ${CIRCLE_BUILD_URL?}
: ${CIRCLE_PROJECT_USERNAME?}
: ${CIRCLE_PROJECT_REPONAME?}

if ! [ -x "$(command -v jq)" ]
then
    echo "Error: jq is not installed"
    exit 1
fi

artifactsJson="$(curl -u $CIRCLE_API_KEY: "https://circleci.com/api/v1.1/project/github/$CIRCLE_PROJECT_USERNAME/$CIRCLE_PROJECT_REPONAME/$CIRCLE_BUILD_NUM/artifacts")"
echo "Artifacts json result from CircleCI API:"
echo "$artifactsJson"
echo

artifactsUrls="$(echo "$artifactsJson" | jq '.[].url' -r)"

if ! [ "$artifactsUrls" ]
then
    echo "Found no artifacts. Aborting."
    exit 2
fi

mkdir -pv $DESTINATION
cd $DESTINATION
echo "Using folder: $DESTINATION"

while read -r url
do
    echo "Downloading $url"
    wget $url
done < <(echo "$artifactsUrls")

echo "Done."
