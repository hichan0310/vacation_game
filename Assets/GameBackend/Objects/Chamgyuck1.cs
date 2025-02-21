using System.Collections.Generic;
using GameBackend.Events;
using UnityEngine;

namespace GameBackend.Objects
{
    public class Chamgyuck1 : SkillEffect
    {
        private Entity player;
        private int dmg;
        private List<AtkTags> atkTags = new() { AtkTags.normalSkill, AtkTags.physicalAttack };
        private HashSet<GameObject> atkObjects = new();
        private int direction;
        private Vector3 playerPosition;

        public void Start()
        {
            timer = -0.15f;
            setAlpha(0);
            Invoke("destroy", 1f);
            this.transform.localScale = new Vector3(0, 0, 0);
        }

        protected override void update(float deltaTime)
        {
            timer += deltaTime;
            checkAlpha(0.2f, 0.3f, 0, 1);
            checkAlpha(0.5f, 0.6f, 1, 0);
            checkScale(0.2f, 0.3f, Vector3.zero, new Vector3(-0.5f * direction, 0.5f, 0.5f));
            checkMove(0.2f, 0.3f,
                this.playerPosition + new Vector3(0.34f * direction, 0.76f, 0f),
                this.playerPosition + new Vector3(1.09f * direction, 0.36f, 0));
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (atkObjects.Contains(other.gameObject)) return;
            base.OnTriggerEnter2D(other);
            this.atkObjects.Add(other.gameObject);
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy is null) return;
            DmgGiveEvent dmgGiveEvent = new DmgGiveEvent(this.dmg, 0.4f, player, enemy, atkTags);
            player.eventActive(dmgGiveEvent);
            enemy.dmgtake(dmgGiveEvent);
        }

        public void setInfo(Entity player)
        {
            this.player = player;
            this.direction = (int)player.transform.localScale.x;
            this.playerPosition = player.transform.position;
            this.transform.localScale = new Vector3(-0.5f * direction, 0.5f, 0.5f);
            this.transform.localPosition = this.playerPosition + new Vector3(1.09f, 0.36f, 0);
            this.dmg = player.status.calculateTrueDamage(atkTags, atkCoef: 100);
        }
    }
}