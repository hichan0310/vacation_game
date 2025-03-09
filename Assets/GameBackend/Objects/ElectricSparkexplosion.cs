using System;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ElectricSparkexplosion:SkillEffect
    {
        private void Start()
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            checkDestroy(0.5f);
        }
    }
}