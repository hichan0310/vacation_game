using GameBackend.Objects;
using UnityEngine;

namespace GameBackend
{
    public abstract class Artifect : MonoBehaviour, IEntityEventListener
    { 
        public virtual int grade => 1;
        public string name { get; set; }
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
    }
}