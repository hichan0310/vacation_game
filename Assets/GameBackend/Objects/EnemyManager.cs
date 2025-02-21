using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Objects
{
    public class EnemyManager:MonoBehaviour
    {
        private float timer = 0;
        public List<Enemy> enemies;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 6f)
            {
                timer = 0;
                Enemy enemy = enemies[Random.Range(0, enemies.Count)];
                Enemy obj = Instantiate(enemy);
                PolygonCollider2D playerCollider = GameObject.Find("Player").GetComponent<PolygonCollider2D>();
                PolygonCollider2D enemyCollider = obj.GetComponent<PolygonCollider2D>();
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
                foreach(GameObject enemy_n in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Physics2D.IgnoreCollision(enemyCollider, enemy_n.GetComponent<PolygonCollider2D>()); 
                }
                obj.transform.position = new Vector3(Random.Range(-6f, 6f), 0, 0);
            }
        }
    }
}