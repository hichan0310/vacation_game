using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class SolarAndLunaEclipse:Artifact, IBuffStatus
    {
        private float timeStack;
        public override int grade => 4;
        private bool? buffType=null;
        private float duration;

        private void Start()
        {
            this.name = "일식, 월식";
            this.description = "시간의 변화를 흡수하여 흡수한 시간이 10초가 되면 그 시간을 소모하여 스킬을 사용하면 일식 상태, 필살기를 사용하면 월식 상태로 넘어간다. 일식 상태에서는 치명타 확률이 100% 상승한다. 월식 상태에서는 모든 공격이 출혈을 유발한다.";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            if (timeStack >= 10)
            {
                if (eventArgs is NormalSkillExecuteEvent)
                {
                    buffType = false;
                    this.timeStack = 0;
                    duration = 10;
                }
                else if (eventArgs is SpecialSkillExecuteEvent)
                {
                    buffType = true;
                    this.timeStack = 0;
                    duration = 10;
                }
            }
            
            if (duration > 0 && buffType != null && !buffType.Value)
            {
                if (eventArgs is DmgGiveEvent eve)
                {
                    if (eve.atkTags.Contains(AtkTags.statusEffect))return;
                    BloodLoss bloodLoss = new BloodLoss(player.status.calculateTrueDamage(new List<AtkTags>
                    {
                        AtkTags.statusEffect
                    }, atkCoef: 15), 4, player);
                    bloodLoss.registrarTarget(eve.target);
                }
            }
        }

        public override void update(float deltaTime)
        {
            timeStack += Mathf.Abs(TimeManager.deltaTime - deltaTime);
            if (timeStack > 10) timeStack = 10;
            duration -= deltaTime;
            if (duration < 0)
            {
                buffType = null;
                duration = 0;
            }
        }

        public void buffStatus(PlayerStatus status)
        {
            if(buffType!=null) if ((bool)buffType && duration > 0) status.crit += 100;
        }
        
        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
