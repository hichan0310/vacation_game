﻿using GameBackend.Events;
using UnityEngine;

namespace GameBackend.Objects
{
    public class Gumgi : Entity
    {
        public bool direction { get; set; } // true==right
        public Vector3 position { get; set; }
        public float time { get; set; }
        public float gumgiSpeed { get; set; } = 1.5f;
        public DmgInfo dmgInfo { get; set; }

        public void apply()
        {
            Invoke("destroy", time);
            transform.position = position;
            if (!direction)
            {
                gumgiSpeed *= -1;
            }
        }

        protected override void update(float deltaTime)
        {
            transform.position += new Vector3(gumgiSpeed * deltaTime, 0, 0);
        }
        
        void destroy()
        {
            Destroy(gameObject);
        }
    }
}