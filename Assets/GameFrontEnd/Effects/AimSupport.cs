using GameBackend;
using GameFrontEnd.Objects;
using UnityEngine;

namespace GameFrontEnd.Effects
{
    public class AimSupport:MonoBehaviour
    {
        public EnemyManager manager;
        public PlayerObject player;
        private float assistAngleRange = 20f; // ±도
        private float assistRange = 5f;
        public LayerMask wallMask;

        private LineRenderer lr;

        void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 0.02f;
            lr.endWidth = 0.02f;
            lr.startColor = new Color(1f, 0.6f, 0f, 0.2f);
            lr.endColor = new Color(1f, 0.6f, 0f, 0.2f);
        }

        void Update()
        {
            Vector3 start = player.transform.position;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            Vector3 aimDir = (mouseWorld - start).normalized;
            Vector3 targetPos = mouseWorld; // 기본: 마우스

            Transform bestTarget = null;
            float bestScore = float.MaxValue;

            foreach (Enemy enemy in manager.spawnedEnemies)
            {
                Vector3 toEnemy = (enemy.transform.position - start);
                float dist = toEnemy.magnitude;

                if (dist > assistRange) continue;

                float angle = Vector3.Angle(aimDir, toEnemy.normalized);
                if (angle > assistAngleRange) continue;

                // 벽 체크
                if (Physics2D.Raycast(start, toEnemy.normalized, dist, wallMask)) continue;

                if (dist < bestScore)
                {
                    bestScore = dist;
                    bestTarget = enemy.transform;
                }
            }

            if (bestTarget)
            {
                targetPos = bestTarget.position;
            }

            lr.SetPosition(0, start);
            lr.SetPosition(1, targetPos);
        }
    }
}