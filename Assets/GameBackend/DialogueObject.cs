using System;
using GameFrontEnd.Effects;
using UnityEngine;

namespace GameBackend
{
    public class DialogueObject:MonoBehaviour
    {
        public EnemyManager enemyManager;
        private void OnCollisionStay(Collision other)
        {
            Debug.Log(other.gameObject.name);
            //이거부터 안됨
            if (enemyManager.enemyWaveFinished)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (Input.GetKeyDown(InputHandler.Interaction))
                    {
                        Debug.Log("Interaction");
                    }
                }
            }
        }
    }
}