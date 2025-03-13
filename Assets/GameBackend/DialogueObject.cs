using System;
using GameFrontEnd.Effects;
using GameFrontEnd.StoryScript;
using UnityEngine;

namespace GameBackend
{
    public class DialogueObject : MonoBehaviour
    {
        public EnemyManager enemyManager;
        public DialogueManager DialogueManager;
        
        
        private void Start()
        {
            Debug.Log("asdf");
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (enemyManager.enemyWaveFinished)
            {
                if (Input.GetKey(InputHandler.Interaction))
                {
                    Debug.Log("Interaction");
                }
            }
        }
    }
}