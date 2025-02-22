using GameBackend.Objects;
using UnityEngine;

namespace GameBackend
{
    public abstract class Artifect : MonoBehaviour, IEntityEventListener
    {
        public string name { get; set; }
        public string description { get; set; }
        public PlayerObject player { get; set; }

        public abstract void eventActive<T>(T eventArgs) where T : EventArgs;

        public void registrarTarget(Entity target)
        {
            player = target.GetComponent<PlayerObject>();
            player.addListener(this);
        }

        public void removeSelf()
        {
            player.removeListener(this);
        }

        public void update(float deltaTime){}
    }
}