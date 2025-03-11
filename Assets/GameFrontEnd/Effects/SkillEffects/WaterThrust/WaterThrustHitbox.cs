using System.Collections.Generic;
using GameBackend;
using GameBackend.Events;
using UnityEngine;

namespace GameFrontEnd.Effects.SkillEffects.WaterThrust
{
    public class WaterThrustHitbox:MonoBehaviour
    {
        private Entity player { get; set; }
        
        private void OnCollisionEnter(Collision other)
        {
            Entity target = other.gameObject.GetComponent<Entity>();
            List<AtkTags> atkTags = new List<AtkTags> { AtkTags.normalSkill , AtkTags.waterAttack };
            int trueDmg = player.status.calculateTrueDamage(atkTags, atkCoef: 300);
            new DmgGiveEvent(trueDmg, 0.8f, player, target, atkTags);
        }
    }
}