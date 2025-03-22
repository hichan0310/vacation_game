using System;
using System.Collections.Generic;
using GameBackend.Objects;
using GameFrontEnd.Effects;
using GameFrontEnd.Objects;
using UnityEngine;

namespace GameBackend
{
    public abstract class Artifact : MonoBehaviour, IEntityEventListener
    { 
        public virtual int grade => 1;
        public new string name { get; set; }
        public string description { get; set; }
        public PlayerObject player { get; set; }
        private List<Collider2D> colliders = new List<Collider2D>();


        public abstract void eventActive<T>(T eventArgs) where T : EventArgs;

        public virtual void registrarTarget(Entity target)
        {
            if (target is PlayerObject player)
            {
                this.player = player;
                player.addListener(this);
            }
        }

        public void removeSelf()
        {
            player.removeListener(this);
        }

        public virtual void update(float deltaTime)
        {
            
        }

        private void Update()
        {
            if (stayingPlayer && Input.GetKeyDown(InputHandler.Interaction) && HasPolygonCollider(colliders))
            {
                stayingPlayer.addArtifact(this);
                Destroy(gameObject);
                Debug.Log(this.name);
            }
        }
        
        private PlayerObject stayingPlayer;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!colliders.Contains(other))
            {
                colliders.Add(other);
            }
            stayingPlayer = other.GetComponent<PlayerObject>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            colliders.Remove(other);
        }

        private bool HasPolygonCollider(List<Collider2D> colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider is PolygonCollider2D && collider.GetComponent<PlayerObject>() != null)
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}