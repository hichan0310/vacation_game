using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Events
{
    public class EntityDieEvent:EventArgs
    {
        protected Entity entity;
        public EntityDieEvent(Entity entity)
        {
            name="EntityDieEvent";
            this.entity = entity;
        }

        public override void trigger()
        {
            entity.eventActive(this);
        }
    }
}