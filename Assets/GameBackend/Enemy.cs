using GameBackend.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend
{
    public class Enemy : Entity
    {
        protected int targetingRange=100;
        protected GameObject target;
        protected bool direction; // 오른쪽이 true
        
        protected bool staggered = false;
        protected bool knockbacked = false;
        protected float forceSum = 0;
        protected float forceStaggered = 0;
        protected float staggerRisist = 0.5f;
        protected float knockbackRisist = 2f;
        
        protected float staggerTimer = 0;
        protected float knockbackTimer = 0;
        
        public GameObject getNearestPlayer(Vector2 position)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject nearest = null;
            float minDistance = float.MaxValue;

            foreach (GameObject player in players)
            {
                float distance = Vector2.Distance(position, player.transform.position);
                if (distance < minDistance && distance < targetingRange)
                {
                    minDistance = distance;
                    nearest = player;
                }
            }
            return nearest;
        }
        
        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            
            forceSum -= deltaTime;
            forceStaggered -= deltaTime;
            forceSum = Mathf.Max(forceSum, 0);
            forceStaggered = Mathf.Max(forceStaggered, 0);
            
            if (forceStaggered > staggerRisist)
            {
                forceStaggered -= staggerRisist;
                staggered = true;
                staggerTimer = 0;
            }
            if (forceSum > knockbackRisist)
            {
                forceSum = 0;
                knockbacked = true;
                knockbackTimer = 0;
            }
            if (knockbacked) { if (knockback(deltaTime)) return; }
            else if (staggered) { if (stagger(deltaTime)) return; }
            
            if (!target)
            {
                GameObject nearest = getNearestPlayer(transform.position);
                target = nearest;
            }

            if (target.transform.position.x > this.transform.position.x) direction = true;
            else direction = false;
            
            Vector3 pos = this.transform.position;
            pos.x += (direction?0.3f:-0.3f)*deltaTime;
            this.transform.position = pos;
        }

        public override void dmgtake(DmgGiveEvent dmg)
        {
            base.dmgtake(dmg);
            this.forceSum += dmg.force;
            this.forceStaggered += dmg.force;
        }

        public override bool stagger(float deltaTime)
        {
            if (staggerTimer == 0)
            {
                //경직 시작
                Debug.Log("stagger");
            }

            Vector3 pos = this.transform.position;
            pos.x += (direction?-1:1)*(Mathf.Max(1-staggerTimer, 0))*deltaTime;
            this.transform.position = pos;
            if (staggerTimer >= 1.5)
            {
                staggered = false;
                return false;
            }
            
            staggerTimer += deltaTime;
            return true;
        }

        public override bool knockback(float deltaTime)
        {
            this.target = null; //넉백 -> 타겟 초기화
            if (knockbackTimer == 0)
            {
                //넉백 시작
                Debug.Log("knockback");
            }

            Vector3 pos = this.transform.position;
            pos.x += (direction?-1:1)*(Mathf.Max(3-knockbackTimer, 0))*deltaTime;
            this.transform.position = pos;
            if (knockbackTimer >= 4)
            {
                knockbacked = false;
                return false;
            }

            knockbackTimer+=deltaTime;
            return true;
        }
    }
}