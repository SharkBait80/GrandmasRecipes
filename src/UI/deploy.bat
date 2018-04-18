echo Deploying UI to S3
aws s3 rm s3://grandmas-recipes.info --recursive

aws s3 cp . s3://grandmas-recipes.info --recursive

echo Deployment done.
