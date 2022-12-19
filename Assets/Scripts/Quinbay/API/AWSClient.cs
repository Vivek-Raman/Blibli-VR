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

namespace Quinbay.API
{
    public class AWSClient : APIClient
    {
        #region Secrets

        private const string AWS_ACCESS_KEY = "nope";
        private const string AWS_SECRET_KEY = "nope";

        #endregion

        [SerializeField] private string s3BucketName = "blibli-catalog-codiecon";

        [CanBeNull] private AmazonS3Client s3Client = null;

        protected override void Init()
        {
            s3Client = new AmazonS3Client(
                new BasicAWSCredentials(AWS_ACCESS_KEY, AWS_SECRET_KEY), RegionEndpoint.APSouth1);
        }

        [Obsolete]
        public override List<string> ListS3Buckets()
        {
            ListBucketsResponse response = s3Client?.ListBucketsAsync(new ListBucketsRequest()).Result;
            return response?.Buckets.ConvertAll(bucket => bucket.BucketName);
        }

        public override async Task DownloadAssetBundles()
        {
            List<string> keys = GetObjectKeysFromBucket(s3BucketName);
            List<Task<GetObjectResponse>> objectTasks = new();

            foreach (string key in keys)
            {
                GetObjectRequest request = new()
                {
                    BucketName = s3BucketName,
                    Key = key,
                };
                objectTasks.Add(s3Client?.GetObjectAsync(request));
            }
            GetObjectResponse[] responses = await Task.WhenAll(objectTasks);

            List<Task> downloadTasks = new();
            foreach (GetObjectResponse response in responses)
            {
                downloadTasks.Add(response.WriteResponseStreamToFileAsync(
                    Application.persistentDataPath + "/" + response.Key,
                    false, CancellationToken.None));
            }

            await Task.WhenAll(downloadTasks);
        }

        private List<string> GetObjectKeysFromBucket(string bucketName)
        {
            ListObjectsRequest request = new()
            {
                BucketName = bucketName,
            };
            ListObjectsResponse response = s3Client?.ListObjectsAsync(request).Result;

            List<string> keys = response?.S3Objects.ConvertAll(obj => obj.Key);
            return keys;
        }
    }
}
