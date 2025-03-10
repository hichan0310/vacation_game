using System;
using GameFrontEnd.Effects;
using UnityEngine;

namespace GameBackend
{
    public class DialogueObject:MonoBehaviour
    {
        public EnemyManager enemyManager;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name); //fucking not work
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