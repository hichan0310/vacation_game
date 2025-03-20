using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;
using UnityEngine.Rendering;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class FlameOfTheBegin : Artifact, IBuffStatus
    {
        private List<AtkTags> atkTags = new() { AtkTags.fireAttack, AtkTags.selfHit, AtkTags.notcriticalHit};
        private float cooldown = 0;
        private readonly float coolTime = 0.25f;
        private void Start()
        {
            this.name = "태초의 불씨";
            this.description = "자신에게 지속적으로 공격력의 1%에 해당하는 화상 피해를 입힌다. hp가 30% 이상일 때만 hp를 잃고 투사체 데미지가 100% 증가하는 버프를 얻는다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public override void update(float deltaTime)
        {
            cooldown += deltaTime;
            if(cooldown > coolTime)
            {
                if(player.status.nowHp >= player.status.maxHp * 0.3f)
                {
                    new DmgGiveEvent(
                        player.status.calculateTrueDamage(atkTags, atkCoef: 1),
                        0, player, player, atkTags
                    );
                }
                cooldown = 0;
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                SetBuff();
            }
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgDrain[(int)AtkTags.projectileDamage] *= 2;
        }

        private void SetBuff()
        {
            buffStatus(player.status);
            player.addBuff(this);
        }
    }
}
