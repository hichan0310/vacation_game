using System;
using GameFrontEnd.Effects;
using GameFrontEnd.Objects;
using GameFrontEnd.StoryScript;
using UnityEngine;

namespace GameBackend
{
    public class DialogueObject : MonoBehaviour
    {
        public EnemyManager enemyManager;
        public DialogueManager dialogueManager;
        public PlayerObject playerObject;
        private bool dialogueStart = false; 

        private void Start()
        {
        }

        private void Update()
        {
            if (dialogueStart && Input.GetKeyDown(InputHandler.Interaction))
            {
                if (!this.dialogueManager.next())
                {
                    this.playerObject.controlable = true;
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!dialogueStart)
            {
                if (Input.GetKeyDown(InputHandler.Interaction))
                {
                    if (enemyManager.enemyWaveFinished)
                    {
                        dialogueStart = true;
                        dialogueManager=Instantiate(dialogueManager);
                        this.playerObject.controlable = false;
                    }
                }
            }
        }
    }
}