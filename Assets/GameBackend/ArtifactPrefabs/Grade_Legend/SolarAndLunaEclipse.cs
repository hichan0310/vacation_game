using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class SolarAndLunaEclipse:Artifact, IBuffStatus
    {
        private float cooldown = 10;
        private readonly float coolTime = 10;
        private float duration = 0;
        private readonly float duration_max = 15;
        private bool isBuff = false;
        private bool Bufftype = true; // true가 스킬 false가 궁극기\
        private bool isLunaeclipse = false;
        public override int grade => 4;

        private void Start()
        {
            this.name = "일식, 월식";
            this.description = "시간의 변화를 흡수하여 흡수한 시간이 10초가 되면 그 시간을 소모하여 스킬을 사용하면 일식 상태, 필살기를 사용하면 월식 상태로 넘어간다. 일식 상태에서는 치명타 확률이 100% 상승한다. 월식 상태에서는 모든 공격이 출혈을 유발한다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if (cooldown == 0)
            {
                if (eventArgs is NormalSkillExecuteEvent)
                {
                    cooldown = coolTime;
                    duration = duration_max;
                    isBuff = true;
                    Bufftype = true;
                    buffStatus(player.status);
                    player.addBuff(this);
                }
                else if (eventArgs is SpecialSkillExecuteEvent)
                {
                    cooldown = coolTime;
                    duration = duration_max;
                    isBuff = true;
                    Bufftype = false;
                    isLunaeclipse = true;
                }

            }
            if(isLunaeclipse && eventArgs is DmgGiveEvent dmgGiveEvent && !dmgGiveEvent.atkTags.Contains(AtkTags.statusEffect))
            {
                BloodLoss bloodLoss = new BloodLoss(player.status.calculateTrueDamage(new List<AtkTags>
                {
                    AtkTags.statusEffect
                }, atkCoef: 5), 1, player);
                bloodLoss.registrarTarget(dmgGiveEvent.target);
                player.addBuff(bloodLoss);
            }
        }

        public override void update(float deltaTime)
        {
            duration -= deltaTime;
            if(duration <= 0)
            {
                cooldown += TimeManager.deltaTime - Time.deltaTime;
            }
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
                        player.removeBuff(this);
                        isBuff = false;
                    }
                    else
                    {
                        player.removeBuff(this);
                        isLunaeclipse = false;
                    }
                }
            } 
            if(Input.GetKeyDown(KeyCode.L)) 
            {
                Debug.Log($"cooldown : {cooldown}");
                Debug.Log($"duration : {duration}");
                Debug.Log($"crit : {player.status.crit}");
            }   
            if(Input.GetKeyDown(KeyCode.K)) 
            {
                cooldown = 0;
            }            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.crit += 100;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
        }
    }
}
