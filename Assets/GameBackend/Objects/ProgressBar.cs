using System;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ProgressBar:MonoBehaviour
    {
        private GameObject progressBar;
        private float length = 1.28f;
        public float ratio=0;

        private void Start()
        {
            progressBar = transform.Find("progressbar").gameObject;
        }

        private void Update()
        {
            var scale = progressBar.transform.localScale;
            scale.x = ratio;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(length*ratio/2-length/2,0,2);
        }
    }
}