using System;
using System.Collections;
using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameBackend
{
    public abstract class Entity : MonoBehaviour
    {
        public float speed { get; set; }
        public PlayerStatus status { get; set; }
        public List<IEntityEventListener> eventListener { get; set; }

        public Entity()
        {
            speed = 1;
            status = new PlayerStatus(1, 0, 0);
            eventListener = new List<IEntityEventListener>();
        }

        public void addListener(IEntityEventListener listener)
        {
            this.eventListener.Add(listener);
        }

        public void removeListener(IEntityEventListener listener)
        {
            this.eventListener.Remove(listener);
        }

        protected virtual void eventActive(EventArgs e)
        {
            foreach (var listener in eventListener)
            {
                listener.eventActive(e);
            }
        }

        protected abstract void update(float deltaTime);

        public void Update()
        {
            update(Time.deltaTime);
        }

        public void dmgtake(DmgGiveEvent dmg)
        {
            int def = status.def;
            int C = 200;
            int realDmg = (int)(dmg.trueDmg * ((float)(def) / (def + C)));
            
            eventActive(new DmgTakeEvent(realDmg, dmg.attacker, dmg.target, dmg.atkTags));
            status.nowHp -= realDmg;
        }
    }
}