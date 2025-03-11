using System;
using System.Collections.Generic;
using System.Linq;
using GameBackend;
using UnityEngine;

namespace GameFrontEnd.Effects.SkillEffects.WaterThrust
{
    public class WaterThrustWater:SkillEffect
    {
        public Vector3 velocity { get; set; }
        protected override void update(float deltaTime)
        {
            this.transform.position+=velocity*deltaTime;
            this.velocity*=1-deltaTime;
        }

        public Entity caster {get;set; }

        private HashSet<Enemy> hit=new ();

        private void OnTriggerEnter(Collider other)
        {
            var col = other.gameObject.GetComponent<Enemy>();
            if (hit.Contains(col)) return;
            hit.Add(col);
            Debug.Log("dmg");
        }
    }
}