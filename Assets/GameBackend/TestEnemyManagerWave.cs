using System;
using System.Collections.Generic;
using System.Linq;
using GameFrontEnd.Effects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameBackend
{
    public class TestEnemyManagerWave:EnemyManager
    {
        private int waveNumber = 0;
        
        private List<List<EnemySpawnInfo>> waveDatas = new();

        public override bool enemyWaveFinished => waveDatas.Count <= waveNumber;

        public void Awake()
        {
            waveDatas.Add(new()
            {
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
            });
            
            waveDatas.Add(new()
            {
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
            });
            
            waveDatas.Add(new()
            {
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
                new EnemySpawnInfo(new Vector2(Random.Range(-6f, 6f), 0), 0),
            });
        }

        protected override void update(float deltaTime)
        {
            this.spawnedEnemies.RemoveAll(x => !x);
            if (this.spawnedEnemies.Count == 0)
            {
                if (!this.enemyWaveFinished)
                {
                    foreach (var enemyInfo in waveDatas[waveNumber])
                    {
                        spawnedEnemies.Add(enemyInfo.spawn());
                    }

                    waveNumber++;
                }
            }
        }
    }
}