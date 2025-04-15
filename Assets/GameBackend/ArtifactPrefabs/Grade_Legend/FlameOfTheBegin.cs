using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class FlameOfTheBegin : Artifact, IBuffStatus
    {
        private List<AtkTags> atkTags = new() { AtkTags.fireAttack, AtkTags.selfHit, AtkTags.notcriticalHit};
        private float cooldown = 0;
        private readonly float coolTime = 4;
        private float Bufftime = 0;
        private bool buffOn = false;
        
        
        private void Start()
        {
            this.name = "태초의 불씨";
            this.description = "자신에게 4초 간격으로 공격력의 1%에 해당하는 화상 피해를 입힌다. hp가 30% 이상일 때만 hp를 잃고 투사체 데미지가 4초동안 100% 증가하는 버프를 얻는다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public override void update(float deltaTime)
        {
            cooldown += deltaTime;
            Bufftime += deltaTime;
            if(Bufftime > 4)
            {
                buffOn = false;
            }
            if(cooldown > coolTime)
            {
                if(player.status.nowHp >= player.status.maxHp * 0.3f)
                {
                    new DmgGiveEvent(
                        player.status.calculateTrueDamage(atkTags, atkCoef: 1),
                        0, player, player, atkTags
                    ).trigger();
                    buffOn = true;
                    Bufftime = 0;
                }
                else
                {
                    buffOn = false;
                }
                cooldown = 0;
            }
        }

        public void buffStatus(PlayerStatus status)
        {
            if(buffOn)status.dmgDrain[(int)AtkTags.projectileDamage] *= 2;
        }
        
        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
