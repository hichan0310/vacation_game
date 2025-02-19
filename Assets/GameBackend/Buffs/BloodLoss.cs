using System.Collections.Generic;
using GameBackend.Events;

namespace GameBackend.Buffs
{
    public class BloodLoss:Buff
    {
        private float timer = 0;
        private int damage{get;set;}
        private int times{get;set;}

        public BloodLoss(int damage, int times)
        {
            this.damage = damage;
            this.times = times;
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
                    atkTags.Add(AtkTags.StatusEffect);
                    DmgGiveEvent dmgTakeEvent=new DmgGiveEvent(damage, 0, EmptyEntity.Instance, target, atkTags);
                    target.dmgtake(dmgTakeEvent);
                }

                if (times <= 0)
                {
                    removeSelf();
                }
            }
        }
    }
}