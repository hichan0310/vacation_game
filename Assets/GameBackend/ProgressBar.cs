using UnityEngine;

namespace GameBackend
{
    public class ProgressBar:MonoBehaviour
    {
        private GameObject progressBar;
        private float length = 1.28f;
        public float ratio=0;
        private Vector3 startPosition;

        private void Start()
        {
            progressBar = GameObject.Find("progressbar"); 
            startPosition = progressBar.transform.localPosition;
        }

        private void Update()
        {
            var scale = progressBar.transform.localScale;
            scale.x = ratio;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(length*ratio/2-length/2,0,2)+startPosition;
        }
    }
}