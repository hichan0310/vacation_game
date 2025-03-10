using System.Collections.Generic;
using GameBackend;
using UnityEngine;

namespace GameFrontEnd.Effects
{
    public class EnemyManager:MonoBehaviour
    {
        public virtual bool enemyWaveFinished => false;

        protected List<Enemy> spawnedEnemies { get; } = new();

        protected virtual void update(float deltaTime){}

        private void Update()
        {
            update(TimeManager.deltaTime);
        }
    }
}