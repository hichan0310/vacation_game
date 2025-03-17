using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class SolarAndLunaEclipse:Artifact, IBuffStatus
    {
        public override int grade => 4;
        private float cooldown = 0;
        private float coolTime = 10;
        private float duration = 0;
        private float duration_max = 10;
        private bool isBuff = false;
        private bool Bufftype = true; // true가 스킬 false가 궁극기
        
        private void Start()
        {
            this.name = "일식, 월식";
            this.description = "시간의 변화를 흡수하여 흡수한 시간이 10초가 되면 그 시간을 소모하여 스킬을 사용하면 일식 상태, 필살기를 사용하면 월식 상태로 넘어간다. 일식 상태에서는 치명타 확률이 100% 상승한다. 월식 상태에서는 모든 공격이 출혈을 유발한다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if (cooldown == 0)
            {
                if (eventArgs is NormalSkillAttackExecuteEvent)
                {
                    cooldown = coolTime;
                    duration = duration_max;
                    isBuff = true;
                    Bufftype = true;
                    buffStatus(player.status);
                }
                else if (eventArgs is UltimateSkillAttackExecuteEvent)
                {
                    cooldown = coolTime;
                    duration = duration_max;
                    isBuff = true;
                    Bufftype = false;
                    // 모든 스킬 출혈은 어케만들지 고민좀 해봐야겠다
                }

            }
        }

        public override void update(float deltaTime)
        {
            duration -= deltaTime;
            if (cooldown < 0)
            {
                cooldown = 0;
            } 
            if (duration < 0)
            {
                duration = 0;
                if(isBuff == true)
                {
                    if(Bufftype == true)
                    {
                        removebuffStatus(player.status);
                        isBuff = false;
                    }
                    else
                    {

                    }
                }
            } 
            if(Input.GetKeyDown(KeyCode.L)) // 시간의 변화를 흡수한다는게 뭔지 모르겠어서 일단 임시 테스트용
            {
                cooldown -= 1;
            }            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.crit += 100;
        }

        public void removebuffStatus(PlayerStatus status)
        {
            status.crit -= 100;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
