using System;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ElectricSpark:SkillEffect
    {
        public BoxCollider2D targetCollider{ get; set; }
        public CircleCollider2D explodeCollider{get;set;}
        public GameObject target { get; set; }
        public float sparkSpeed { get; set; } = 5;
        public float angle { get; set; } = 0;
        public float homingAngle { get; set; }= Mathf.PI/200;
        
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
            targetCollider = this.gameObject.GetComponent<BoxCollider2D>();
            explodeCollider = this.gameObject.GetComponent<CircleCollider2D>();
        }

        protected override void update(float deltaTime)
        {
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
                if (Mathf.Sin(targetAngle-angle)>0) angle += homingAngle;
                else angle -= homingAngle;
            }
            else
            {
                target = getNearestEnemy(this.transform.position);
            }
            this.transform.position = transform.position+new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (sparkSpeed * deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explodeCollider.transform.position, explodeCollider.radius);

            foreach (var hitCollider in hitColliders)
            {
                Debug.Log($"Detected object tag: {hitCollider.tag}");
            }
            destroy();
        }
    }
}