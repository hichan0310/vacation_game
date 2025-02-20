using System;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ProgressBar:MonoBehaviour
    {
        public PlayerObject player;
        private GameObject progressBar;
        private float length = 1.28f;

        private void Start()
        {
            progressBar = GameObject.Find("progressbar");
        }

        private void Update()
        {
            float ratio = 1-player.normalSkill.timeleft / 5;
            var scale = progressBar.transform.localScale;
            scale.x = ratio;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(length*ratio/2-length/2,0,2);
        }
    }
}