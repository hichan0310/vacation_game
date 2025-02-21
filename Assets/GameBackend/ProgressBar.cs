using UnityEngine;

namespace GameBackend
{
    public class ProgressBar:MonoBehaviour
    {
        private GameObject progressBar;
        private GameObject progressBound;
        public float length = 1.28f;
        public float ratio { get; set; } = 0;
        private Vector3 startPosition;

        private void Start()
        {
            progressBar = transform.Find("progressbar").gameObject; 
            progressBound = transform.Find("progressbound").gameObject;
            startPosition = progressBar.transform.localPosition;
            progressBar.transform.localScale = new Vector3(length/1.28f, length/1.28f, length/1.28f);
            progressBound.transform.localScale = new Vector3(length/1.28f, length/1.28f, length/1.28f);
        }

        private void Update()
        {
            ratio = Mathf.Clamp(ratio, 0, 1.0f);
            var scale = progressBar.transform.localScale;
            scale.x = ratio*length/1.28f;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(length*ratio/2-length/2,0,2)+startPosition;
        }
    }
}