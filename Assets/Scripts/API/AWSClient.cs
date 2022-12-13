using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using JetBrains.Annotations;
using UnityEngine;

namespace API
{
    public class AWSClient : APIClient
    {
        private const string AWS_ACCESS_KEY = "nope";
        private const string AWS_SECRET_KEY = "nope";

        [SerializeField] private string s3BucketName = "blibli-catalog-codiecon";

        [CanBeNull] private AmazonS3Client _s3Client = null;

        protected override APIClient Init()
        {
            _s3Client =
                new AmazonS3Client(
                    new BasicAWSCredentials(AWS_ACCESS_KEY, AWS_SECRET_KEY),
                    RegionEndpoint.APSouth1);

            return this;
        }

        [Obsolete]
        public override List<string> ListS3Buckets()
        {
            ListBucketsResponse response = _s3Client?.ListBucketsAsync(new ListBucketsRequest()).Result;
            return response?.Buckets.ConvertAll(bucket => bucket.BucketName);
        }

        public override async Task DownloadAssetBundles()
        {
            List<string> keys = GetObjectKeysFromBucket(s3BucketName);
            List<Task<GetObjectResponse>> tasks = new List<Task<GetObjectResponse>>();

            foreach (string key in keys)
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = s3BucketName,
                    Key = key,
                };
                tasks.Add(_s3Client?.GetObjectAsync(request));
            }

            GetObjectResponse[] responses = Task.WhenAll(tasks).Result;
            foreach (GetObjectResponse response in responses)
            {
                await response.WriteResponseStreamToFileAsync(Application.streamingAssetsPath + "/" + response.Key,
                    false, CancellationToken.None);
            }
        }

        private List<string> GetObjectKeysFromBucket(string bucketName)
        {
            ListObjectsRequest request = new ListObjectsRequest
            {
                BucketName = s3BucketName,
            };
            ListObjectsResponse response = _s3Client?.ListObjectsAsync(request).Result;

            List<string> keys = response?.S3Objects.ConvertAll(obj => obj.Key);
            return keys;
        }
    }
}
