using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Objects
{
    public class TestEnemy1:Enemy
    {
        private List<AtkTags> atkTags = new() { AtkTags.normalAttack, AtkTags.physicalAttack };
        public GameObject hpBar;
        private GameObject hpBarObject;
        private ProgressBar progressBar;


        private void Start()
        {
            GameObject.FindGameObjectsWithTag("Player");
            hpBarObject=Instantiate(hpBar, transform, true);
            hpBarObject.transform.localPosition = new Vector3(0, 0.5f, 0.1f);
            progressBar = hpBarObject.GetComponent<ProgressBar>();
            this.status = new PlayerStatus(10000, 100, 100);
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (this.dead)
            {
                Destroy(hpBarObject);
            }
            else
            {
                progressBar.ratio=(float)status.nowHp/status.maxHp;
                progressBar.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            if(other is PolygonCollider2D && other.gameObject.tag == "Player" && isAttack == true)
            {
                isAttack = false;
                PlayerObject player = other.GetComponent<PlayerObject>();
                invokeManager.invoke(normalAttackDamage, new List<object> {player}, 0.2f);
            }
        }

        private void normalAttackDamage(List<object> players)
        {
            foreach (PlayerObject player in players)
            {
                new DmgGiveEvent(
                    this.status.calculateTrueDamage(atkTags, atkCoef: 100),
                    0, this, player, atkTags
                );
            }
        }
    }
}