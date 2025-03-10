using System;
using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Skills
{
    public class TestSpecialSkill : Skill
    {
        public override string skillName => "TestSpecialSkill";
        public override string description => "TestSpecialSkillDescription";
        private const float cooltime = 10;
        private float energy = 0;
        private float slowtime = 0;
        private const float slowDuration = 7;
        private int effectCount = 0;

        public ProgressBar energyProgressBar;
        public ProgressBar timeProgressBar;
        public ImpactVFX impact;
        public FlameVFX flame;

        private Dictionary<Entity, int> targets = new();

        public override float timeleft { get; set; }

        public override bool active => timeleft <= 0 && energy >= 20;

        private void Start()
        {
            timeleft = cooltime;
        }

        private void checkImpact(int number)
        {
            if (slowtime <= slowDuration - 0.03 * number && effectCount == number)
            {
                effectCount++;
                ImpactVFX obj = Instantiate(impact);
                obj.transform.position =
                    player.transform.position +
                    new Vector3(Mathf.Sin(2 * Mathf.PI / 12 * number), Mathf.Cos(2 * Mathf.PI / 12 * number), 0);
                obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }

        private void checkflame(int number)
        {
            if (slowtime <= slowDuration - 1 - 0.5 * number && effectCount == number + 12)
            {
                effectCount++;
                FlameVFX flameObject = Instantiate(flame, player.transform, true);
                flameObject.GetComponent<FlameVFX>().time = (12 - number) * 0.5f;
                flameObject.transform.localPosition = new Vector3(Mathf.Sin(2 * Mathf.PI / 12 * number),
                    Mathf.Cos(2 * Mathf.PI / 12 * number), 0);
            }
        }

        public override void update(float deltaTime)
        {
            energyProgressBar.ratio = (float)energy / 40;
            if (slowtime > 0)
            {
                checkImpact(0);
                checkImpact(1);
                checkImpact(2);
                checkImpact(3);
                checkImpact(4);
                checkImpact(5);
                checkImpact(6);
                checkImpact(7);
                checkImpact(8);
                checkImpact(9);
                checkImpact(10);
                checkImpact(11);

                checkflame(0);
                checkflame(1);
                checkflame(2);
                checkflame(3);
                checkflame(4);
                checkflame(5);
                checkflame(6);
                checkflame(7);
                checkflame(8);
                checkflame(9);
                checkflame(10);
                checkflame(11);


                slowtime -= deltaTime;
                timeProgressBar.ratio = slowtime / slowDuration;
                if (slowtime <= 0)
                {
                    finish();
                }
            }
            else
            {
                timeleft -= deltaTime;
                if (timeleft < 0) timeleft = 0;
                timeProgressBar.ratio = -timeleft / cooltime + 1;
            }
        }

        private void finish()
        {
            effectCount = 0;
            slowtime = 0;
            TimeManager.timeRate *= 10;
            for (int i = 0; i < 10; i++)
            {
                ImpactVFX obj = Instantiate(impact);
                obj.transform.position = player.transform.position;
                obj.transform.localScale = new Vector3(1, 1, 1);
            }


            foreach (var target in targets)
            {
                if (!target.Key) continue;
                PlayerStatus status = player.status;
                List<AtkTags> atkTags = new List<AtkTags>();
                atkTags.Add(AtkTags.physicalAttack);
                atkTags.Add(AtkTags.specialSkill);
                new DmgGiveEvent(
                    status.calculateTrueDamage(atkTags, atkCoef: 40 * target.Value), 
                    1, player, target.Key, atkTags);

                BloodLoss bloodLoss = new BloodLoss(status.calculateTrueDamage(new List<AtkTags>
                {
                    AtkTags.statusEffect
                }, atkCoef: 40), target.Value, player);
                bloodLoss.registrarTarget(target.Key);
            }
            this.targets.Clear();
        }

        public override void execute()
        {
            timeleft = cooltime;
            energy = 0;
            TimeManager.timeRate *= 0.1f;
            this.slowtime = slowDuration;
        }

        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is DmgGiveEvent dmgEvent)
            {
                if (dmgEvent.atkTags.Contains(AtkTags.normalAttack))
                {
                    energy += 1*player.status.energyRecharge/100;
                }
                else if (dmgEvent.atkTags.Contains(AtkTags.normalSkill))
                {
                    energy += 2*player.status.energyRecharge/100;
                }

                if (slowtime > 0)
                {
                    if (targets.ContainsKey(dmgEvent.target))
                    {
                        targets[dmgEvent.target] += 1;
                    }
                    else
                    {
                        targets.Add(dmgEvent.target, 1);
                    }
                }
            }

            if (energy > 100) energy = 100;
        }
    }
}