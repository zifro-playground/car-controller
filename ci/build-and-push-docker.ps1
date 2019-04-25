
# Builds and pushes the docker images

# THIS SCRIPT IS MEANT TO BE USED FOR DEVELOPMENT PURPOSES
# DO NOT USE IN PRODUCTION

param(
    # Docker image account
    [string]
    $account = "zifrose",
    
    # Docker images (same as filenames without .Dockerfile)
    [string[]]
    $images = @(
        "unity3d-webgl",
        "unity3d"
    ),

    # Unity version, also used as Docker image tag
    [string]
    $UnityVersion = "2018.3.11f1"
)

$basePath = Resolve-Path $PSCommandPath | Split-Path -Parent

Write-Host ">>> Building" -BackgroundColor Green
$step = 0
$steps = $images.Count * 2

foreach ($image in $images) {
    $step++;
    $imageFullName = "$account/$($image):$UnityVersion"
    $file = Join-Path $basePath "$image.Dockerfile"
    Write-Host "> Building $imageFullName docker image (step $step/$steps)" -BackgroundColor DarkGreen
    Write-Host ""
    docker build . -t $imageFullName -f $file --build-arg UNITY_VERSION=$UnityVersion
    if (-not $?) {
        throw "Failed to build $imageFullName (step $step/$steps)"
    }
    Write-Host ""
}

Write-Host ">>> Pushing" -BackgroundColor Blue

foreach ($image in $images) {
    $step++;
    $imageFullName = "$account/$($image):$UnityVersion"
    Write-Host "> Pushing $imageFullName docker image (step $step/$steps)" -BackgroundColor DarkBlue
    Write-Host ""
    docker push $imageFullName
    if (-not $?) {
        throw "Failed to push $imageFullName (step $step/$steps)"
    }
    Write-Host ""
}

Write-Host "<<< Build and push complete" -BackgroundColor Cyan
