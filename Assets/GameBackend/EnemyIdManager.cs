using System.Collections.Generic;
using UnityEngine;

namespace GameBackend
{
    public class EnemyIdManager:MonoBehaviour
    {
        public List<Enemy> enemies=new();
        public static EnemyIdManager Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this; // Singleton 초기화
            }
            else
            {
                Destroy(gameObject); // 중복된 매니저 제거
            }
        }
    }
}