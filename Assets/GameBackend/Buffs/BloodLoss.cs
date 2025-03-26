using System.Collections.Generic;
using GameBackend.Events;

namespace GameBackend.Buffs
{
    public class BloodLoss:Buff
    {
        private float timer = 0;
        private int damage{get;set;}
        private int times{get;set;}
        private Entity caster{get;set;}

        public BloodLoss(int damage, int times, Entity caster)
        {
            this.damage = damage;
            this.times = times;
            this.caster = caster;
        }

        public override void update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= 0.5)
            {
                timer = 0;
                times-=1;
                foreach (var target in targets)
                {
                    List<AtkTags> atkTags = new List<AtkTags>();
                    atkTags.Add(AtkTags.bloodLossDamage);
                    new DmgGiveEvent(damage, 0, caster, target, atkTags);
                }

                if (times <= 0)
                {
                    removeSelf();
                }
            }
        }
    }
}