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

        public bool next()
        {
            this.manager.next();
            return !manager.finished;
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