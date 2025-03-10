using UnityEngine;

namespace GameBackend
{
    public class EnemySpawnInfo
    {
        public Vector2 position;
        public int id;

        public EnemySpawnInfo(Vector2 position, int id)
        {
            this.position = position;
            this.id = id;
        }

        public Enemy spawn()
        {
            Enemy enemy = Object.Instantiate(EnemyIdManager.Instance.enemies[id]);
            enemy.transform.position = position;
            return enemy;
        }
    }
}