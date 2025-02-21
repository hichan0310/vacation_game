using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Objects
{
    public class TestEnemy1:Enemy
    {
        public GameObject hpBar;
        private GameObject hpBarObject;
        private ProgressBar progressBar;
        private GameObject[] players;
        
        private void Start()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            hpBarObject=Instantiate(hpBar, transform, true);
            hpBarObject.transform.localPosition = new Vector3(0, 0.5f, 0);
            progressBar = hpBarObject.GetComponent<ProgressBar>();
            this.status = new PlayerStatus(10000, 100, 100);
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            progressBar.ratio=(float)status.nowHp/status.maxHp;
            progressBar.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
        }
    }
}