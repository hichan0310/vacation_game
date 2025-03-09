using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ElectricSpark:SkillEffect
    {
        public GameObject target { get; set; }
        public LayerMask enemyLayer; 
        public float sparkSpeed { get; set; } = 5;
        public float angle { get; set; } = 0;
        public float homingAngle { get; set; }= Mathf.PI*1.5f;
        private List<AtkTags> atkTags = new() { AtkTags.lightningAttack, AtkTags.artifectDamage };
        private ContactFilter2D filter;
        private int dmg;
        private Entity player;
        private Enemy enemy;
        private Enemy enemys;
        private bool isCrash;
        public float time { get; set; }=0;
        public ElectricSparkexplosion explosion;

        private GameObject getNearestEnemy(Vector2 position)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearest = null;
            float minDistance = float.MaxValue;

            foreach (GameObject player in players)
            {
                float distance = Vector2.Distance(position, player.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = player;
                }
            }
            return nearest;
        }

        private void Start()
        {
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), GameObject.FindWithTag("Player").GetComponent<Player<TestPlayerInfo1>>().GetComponent<PolygonCollider2D>());
            player = GameObject.FindWithTag("Player").GetComponent<Player<TestPlayerInfo1>>();
            filter.SetLayerMask(enemyLayer);
            isCrash = true;
        }

        protected override void update(float deltaTime)
        {
            timer += deltaTime;
            checkDestroy(time);
            this.transform.rotation = Quaternion.Euler(0,0,angle*Mathf.Rad2Deg);
            if (target)
            {
                var direction = target.transform.position - this.transform.position;
                var tangent = direction.y / direction.x;
                var targetAngle = Mathf.Atan(tangent);
                if (direction.x < 0)
                {
                    if (direction.y < 0) targetAngle -= Mathf.PI;
                    else targetAngle += Mathf.PI;
                }
                if (Mathf.Sin(targetAngle-angle)>0) angle += homingAngle*deltaTime;
                else angle -= homingAngle*deltaTime;
            }
            else
            {
                target = getNearestEnemy(this.transform.position);
            }
            this.transform.position = transform.position+new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (sparkSpeed * deltaTime);
            foreach(GameObject enemy_n in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), enemy_n.GetComponent<PolygonCollider2D>()); 
            }
            foreach(GameObject skillefect_n in GameObject.FindGameObjectsWithTag("Skilleffect"))
            {
                if (skillefect_n == GetComponent<CircleCollider2D>()) continue;
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), skillefect_n.GetComponent<CircleCollider2D>()); 
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy" && other is PolygonCollider2D && isCrash && !other.gameObject.GetComponent<Enemy>().dead)
            {
                enemy = other.gameObject.GetComponent<Enemy>();
                dmg = player.status.calculateTrueDamage(atkTags, atkCoef: 50);
                new DmgGiveEvent(dmg, 0f, player, enemy, atkTags);
                isCrash = false;
                PolygonCollider2D[] colliders = new PolygonCollider2D[30];
                GetComponent<CircleCollider2D>().OverlapCollider(filter, colliders);
                foreach (PolygonCollider2D collider in colliders)
                {
                    if (collider == null || collider == other || collider.gameObject.GetComponent<Enemy>().dead) continue;
                    enemys = collider.gameObject.GetComponent<Enemy>();
                    dmg = player.status.calculateTrueDamage(atkTags, atkCoef: 100);
                    new DmgGiveEvent(dmg, 0f, player, enemys, atkTags);
                }
                Destroy(this.gameObject);
                ElectricSparkexplosion obj = Instantiate(explosion);
                obj.transform.position = this.transform.position;
            }
        }
    }
}