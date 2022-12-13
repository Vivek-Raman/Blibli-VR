using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace API
{
    public abstract class APIClient : MonoBehaviour
    {
        protected abstract APIClient Init();
        
        private void Awake()
        {
            this.Init();
        }

        [Obsolete]
        public abstract List<string> ListS3Buckets();

        public abstract Task DownloadAssetBundles();
    }
}
