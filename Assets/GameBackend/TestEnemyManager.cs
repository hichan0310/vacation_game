using System.Collections.Generic;
using GameFrontEnd.Effects;
using UnityEngine;

namespace GameBackend
{
    public class TestEnemyManager : EnemyManager
    {
        private float timer = 6;
        
        protected override void update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= 6)
            {
                timer = 0;

                var spawnInfo = new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0);
                Enemy enemy = spawnInfo.spawn();
                
                spawnedEnemies.Add(enemy);
            }
        }
    }
}