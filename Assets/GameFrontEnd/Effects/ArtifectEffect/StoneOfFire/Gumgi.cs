using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using UnityEngine;

namespace GameBackend.Objects
{
    public class Gumgi : SkillEffect
    {
        public bool direction { get; set; } // true==right
        public Vector3 position { get; set; }
        public float time { get; set; }
        public float gumgiSpeed { get; set; } = 1.5f;
        public DmgInfo dmgInfo { get; set; }
        
        private readonly HashSet<Enemy> atkObjects = new();

        public void apply()
        {
            this.timeIgnore = true;
            transform.position = position;
            if (!direction)
            {
                gumgiSpeed *= -1;
            }
        }

        protected override void update(float deltaTime)
        {
            transform.position += new Vector3(gumgiSpeed * deltaTime, 0, 0);
            checkDestroy(time);
        }
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
            if (atkObjects.Contains(enemy) || !enemy) return;
            new DmgGiveEvent(dmgInfo.trueDmg, dmgInfo.force, dmgInfo.attacker, enemy, dmgInfo.atkTags);
            DefIgnore defIgnore = new DefIgnore(50, 4);
            defIgnore.registrarTarget(enemy);
            atkObjects.Add(enemy);
        }
        
        void destroy()
        {
            Destroy(gameObject);
        }
    }
}