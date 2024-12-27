using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBackend
{
    public class Entity : MonoBehaviour
    {
        public PlayerStatus status { get; set; }
        public List<IEntityEventListener> eventListener { get; set; }

        public void addListener(IEntityEventListener listener)
        {
            this.eventListener.Add(listener);
        }

        public void removeListener(IEntityEventListener listener)
        {
            this.eventListener.Remove(listener);
        }

        private void cleanEventListener()
        {
            eventListener.RemoveAll(listener => listener == null);
        }
    }
}
