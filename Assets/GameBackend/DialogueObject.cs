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
            if (Input.GetKeyDown(InputHandler.Interaction))
            {
                Debug.Log("Interaction");

                if (enemyManager.enemyWaveFinished)
                {
                    Instantiate(DialogueManager);
                }
            }
        }
    }
}