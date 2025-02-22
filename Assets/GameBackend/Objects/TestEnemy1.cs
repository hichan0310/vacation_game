using GameBackend.Status;
using GameBackend.Events;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameBackend.Objects
{
    public class TestEnemy1:Enemy
    {
        private List<AtkTags> atkTags = new() { AtkTags.normalAttack, AtkTags.physicalAttack };
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
                Player<TestPlayerInfo1> player = other.GetComponent<Player<TestPlayerInfo1>>();
                StartCoroutine(Damage(player));
            }
        }
        IEnumerator Damage(Player<TestPlayerInfo1> player)
        {
            yield return new WaitForSeconds(0.2f);
            new DmgGiveEvent(this.status.calculateTrueDamage(atkTags, atkCoef: 100), 0, this, player, atkTags);
        }

    }
}