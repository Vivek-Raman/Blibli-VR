using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Quinbay.API
{
    public abstract class APIClient : MonoBehaviour
    {
        protected abstract void Init();
        
        private void Awake()
        {
            Init();
        }

        [Obsolete]
        public abstract List<string> ListS3Buckets();

        public abstract Task DownloadAssetBundles();
    }
}
