using Pulumi;
using Pulumi.Aws.S3;

class MyStack : Stack
{
    public MyStack()
    {
        // Create an AWS resource (S3 Bucket)
        var bucket = new Bucket("my-bucket");

        var bucketObject = new BucketObject("bucketobject", new BucketObjectArgs
        {
            Key = "file.txt",
            Bucket = bucket.Id,
            Source = new FileAsset("file.txt")
        });


        var thisObject = GetBucketObject.Invoke(new GetBucketObjectInvokeArgs
        {
            Bucket = bucket.Id,
            Key = bucketObject.Key
        });

        BucketObjectModified = thisObject.Apply(x => x.LastModified);
    }

   [Output] 
   public Output<string> BucketObjectModified {get;set;}
}
