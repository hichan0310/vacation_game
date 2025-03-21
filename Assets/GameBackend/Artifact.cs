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

        public virtual void update(float deltaTime){}

        private bool test = true;
        private void OnTriggerStay2D(Collider2D other)
        {
            var p = other.GetComponent<PlayerObject>();
            if (p && test)
            {
                if (Input.GetKeyDown(InputHandler.Interaction))
                {
                    p.addArtifact(this);
                    Destroy(gameObject);
                    Debug.Log(test);
                    test = false;
                }
            }
        }
    }
}