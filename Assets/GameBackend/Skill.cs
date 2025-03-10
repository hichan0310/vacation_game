using System.Collections;
using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend
{
    public interface ISkill : IEntityEventListener
    {
        public string skillName { get; }
        public string description { get; }
        public bool active { get; }
        public float timeleft { get; set; }
        public void execute();
    }

    public abstract class Skill : MonoBehaviour, ISkill
    {
        protected Entity player;
        public abstract void eventActive<T>(T eventArgs) where T : EventArgs;

        public void registrarTarget(Entity target)
        {
            this.player = target;
            player.addListener(this);
        }

        public void removeSelf()
        {
            player.removeListener(this);
        }

        public abstract void update(float deltaTime);
        
        public abstract string skillName { get; }
        public abstract string description { get; }
        public abstract bool active { get; }
        public abstract float timeleft { get; set; }
        public abstract void execute();
    }
}