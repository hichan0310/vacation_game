﻿using System;
using UnityEngine;

namespace GameBackend.Objects
{
    public class ImpactVFX:SkillEffect
    {
        private void Start()
        {
            timeIgnore = true;
            //transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            checkDestroy(4);
        }
    }
}