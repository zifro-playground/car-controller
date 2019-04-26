#!/bin/bash

# Set error flags
set -o nounset
set +o errexit
set -o pipefail

PROJECT=${1?Project path}
: ${BUILD_TARGET?Build target (ex: WebGL)}
: ${BUILD_NAME?Build name (output name)}
: ${BUILD_PATH:=$PROJECT/Builds/$BUILD_TARGET/}

if ! [[ "$BUILD_PATH" = /* ]]
then
    # Build path is relative => use current directory
    $BUILD_PATH=$PWD/$BUILD_PATH
fi

mkdir -pv $BUILD_PATH

echo
echo ">>>>>> Building Unity project targetting '$BUILD_TARGET'"
echo

START=$(date +%s%3N)

${UNITY_EXECUTABLE:-xvfb-run -as '-screen 0 640x480x24' /opt/Unity/Editor/Unity} \
    -batchmode \
    -projectPath $PROJECT \
    -buildTarget $BUILD_TARGET \
    -customBuildTarget $BUILD_TARGET \
    -customBuildName $BUILD_NAME \
    -customBuildPath $BUILD_PATH \
    -customBuildOptions AcceptExternalModificationsToPlayer \
    -executeMethod BuildCommand.PerformBuild \
    -quit \
    -logfile ${BUILD_LOG:-}

UNITY_EXIT_CODE=$?

LOGS=~/.config/unity3d/Unity/Editor/Editor.log
if [ -f $LOGS ]
then
    echo "(Reading logs from $LOGS)"
    cat $LOGS
    rm $LOGS
    exit 1
fi

echo
echo "<<<<<< Unity '$BUILD_TARGET' execution complete"
echo

END=$(date +%s%3N)
DIFF=$((END-START))

MS=$((DIFF%1000))
SEC=$((DIFF/1000%60))
MIN=$((DIFF/1000/60))

echo "Took $MIN min, $SEC sec, $MS ms"

if [ $UNITY_EXIT_CODE -eq 0 ]; then
    echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
    echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
    echo "Run failure (other failure)";
else
    echo "Unexpected exit code $UNITY_EXIT_CODE";
    exit $UNITY_EXIT_CODE
fi

ls -la $BUILD_PATH
[ -n "$(ls -A $BUILD_PATH)" ] # fail if build folder is empty
