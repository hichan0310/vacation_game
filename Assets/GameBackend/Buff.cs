using System.Collections.Generic;
using UnityEditor;
using GameBackend.Events;
using GameBackend.Status;

namespace GameBackend
{
    public class Buff : IBuffStatus, IEntityEventListener
    {
        private List<Entity> targets = new List<Entity>();
        
        public virtual void buffStatus(PlayerStatus status) {}
        public virtual void eventActive<T>(T eventArgs) where T : EventArgs {}

        public virtual void registrarTarget(Entity target)
        {
            this.targets.Add(target);
            target.addListener(this);
        }

        public void removeSelf()
        {
            foreach (var target in targets)
            {
                target.removeListener(this);
            }
        }

        public virtual bool active()
        {
            return true;
        }
    }
}