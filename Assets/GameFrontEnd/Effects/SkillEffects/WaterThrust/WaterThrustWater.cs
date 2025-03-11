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

        public void Awake()
        {
            this.timeIgnore = true;
        }
        
        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            this.transform.position+=velocity*deltaTime;
            this.velocity*=1-deltaTime;
            checkDestroy(1);
        }

        public Entity caster {get;set; }

        private HashSet<Enemy> hit=new ();

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("dmg");
            var col = other.gameObject.GetComponent<Enemy>();
            if (hit.Contains(col)) return;
            hit.Add(col);
        }
    }
}