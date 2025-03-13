using System;
using UnityEngine;

namespace GameFrontEnd.StoryScript
{
    public class DialogueManager : MonoBehaviour
    {
        public DialogueActionManager manager;
        private void Awake()
        {
            
        }

        private void Start()
        {
            manager.storyFilePath = "Assets/GameBackend/CSV/dialogue.csv";
        }

        private void Update()
        {
            if (manager.finished)
            {
                Destroy(gameObject);
            }
        }
    }
}