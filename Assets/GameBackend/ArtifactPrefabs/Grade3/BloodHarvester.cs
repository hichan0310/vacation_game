using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade3
{
    public class BloodHarvester:Artifact
    {
        private Enemy enemys;
        
        private List<AtkTags> atkTags = new() { AtkTags.waterAttack, AtkTags.artifactDamage, AtkTags.notcriticalHit};
        public LayerMask targetLayer;
        private int dmg;

        private int count = 0;
        private int max_count = 30;
        private float distance = 1.5f;
        private void Start()
        {
            this.name = "피의 수확자";
            this.description = "출혈 피해를 가하면 1스택씩 쌓이고 30스택에 도달하면 주변에 최대 체력 20%의 물 속성 피해를 가한다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve && eve.target != player && eve.atkTags.Contains(AtkTags.bloodLossDamage) && count < max_count)
            {
                count += 1;
            }
        }
        public override void update(float deltaTime)
        {
            if (count == max_count)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, distance, targetLayer);
                foreach (Collider2D collider in colliders)
                {
                    if (collider is not PolygonCollider2D || collider == null || collider.gameObject.GetComponent<Enemy>().dead) continue;
                    enemys = collider.gameObject.GetComponent<Enemy>();
                    dmg = (int)(enemys.status.maxHp * 0.2f);
                    new DmgGiveEvent(dmg, 0.5f, player, enemys, atkTags);
                }
                count = 0;
            }
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
        }
    }
}
