using GameBackend.Events;
using UnityEngine;

namespace GameBackend.Objects
{
    public class Gumgi : Entity
    {
        public bool direction { get; set; } // true==right
        public Vector3 position { get; set; }
        public float time { get; set; }
        public static float gumgiSpeed { get; set; }
        public DmgInfo dmgInfo { get; set; }

        public void apply()
        {
            Invoke("destroy", time);
            Vector3 scale = transform.localScale;
            transform.position = position;
            if (!direction)
            {
                scale.x *= -1;
                transform.localScale = scale;
                Gumgi.gumgiSpeed = -1;
            }
            else
            {
                Gumgi.gumgiSpeed = 1;
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