#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

PROJECT="${1?Project path of which to check version.}"

VERSION_FILE=$PROJECT/ProjectSettings/ProjectVersion.txt

if ! [ -f "$VERSION_FILE" ]
then
    echo "Unable to find ProjectVersion.txt in project $PROJECT"
fi

echo "Looking for Unity version in file $VERSION_FILE"
VERSION="$(grep -oP 'm_EditorVersion:\s*\K.*' $VERSION_FILE)"

if [ "$VERSION" ]
then
    echo "Found version: '$VERSION'"
    export UNITY_VERSION="$VERSION"
    echo "Writing to \$UNITY_VERSION in \$BASH_ENV"
    echo "\$UNITY_VERSION='$VERSION'" >> $BASH_ENV
else
    echo "Unable to find Unity version in $VERSION_FILE"
fi
