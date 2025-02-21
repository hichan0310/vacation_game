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
                obj.transform.position = new Vector3(Random.Range(-6f, 6f), 0, 0);
            }
        }
    }
}