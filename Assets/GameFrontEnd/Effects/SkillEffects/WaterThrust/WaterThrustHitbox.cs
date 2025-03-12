using System.Collections.Generic;
using GameBackend;
using GameBackend.Events;
using UnityEngine;

namespace GameFrontEnd.Effects.SkillEffects.WaterThrust
{
    public class WaterThrustHitbox:SkillEffect
    {
        public Entity player { get; set; }

        protected override void update(float deltaTime)
        {
            
        }

        public void destroyObject()
        {
            this.destroy();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Entity target = other.gameObject.GetComponent<Entity>();
            if (target is Enemy enemy)
            {
                List<AtkTags> atkTags = new List<AtkTags> { AtkTags.normalSkill, AtkTags.waterAttack };
                int trueDmg = player.status.calculateTrueDamage(atkTags, atkCoef: 100);
                new DmgGiveEvent(trueDmg, 0.4f, player, enemy, atkTags);
            }
        }
    }
}