using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;
using UnityEngine;

namespace GameBackend.Skills
{
    public class TestSpecialSkill : ISkill, IEntityEventListener
    {
        public string name => "TestSpecialSkill";
        public string description => "TestSpecialSkillDescription";
        private const float cooltime = 10;
        private int energy=0;
        private Entity player;
        private float slowtime = 0;
        private const float slowDuration = 7;
        private int effectCount = 0;
        
        public ProgressBar energyProgressBar { get; private set; }
        public ProgressBar timeProgressBar { get; private set; }
        public GameObject impact{get;private set;}
        public GameObject flame{get;private set;}
        
        private Dictionary<Entity, int> targets = new();

        public float timeleft { get; private set; }

        public bool active => timeleft <= 0 && energy >= 20;

        public TestSpecialSkill()
        {
            timeleft = cooltime;
        }

        private void checkImpact(int number)
        {
            if (slowtime <= slowDuration - 0.03*number && effectCount == number)
            {
                effectCount++;
                Object.Instantiate(impact).transform.position = 
                    player.transform.position+ 
                    new Vector3(Mathf.Sin(2*Mathf.PI/12*number), Mathf.Cos(2*Mathf.PI/12*number), 0);
            }
        }
        
        private void checkflame(int number)
        {
            if (slowtime <= slowDuration - 1 - 0.5*number && effectCount == number+12)
            {
                effectCount++;
                GameObject flameObject = Object.Instantiate(flame, player.transform, true);
                flameObject.GetComponent<FlameVFX>().time = 0.1f; //(12 - number) * 0.5f;
                flameObject.transform.localPosition = new Vector3(Mathf.Sin(2*Mathf.PI/12*number), Mathf.Cos(2*Mathf.PI/12*number), 0);
            }
        }

        public void update(float deltaTime)
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
                timeProgressBar.ratio = -timeleft / cooltime+1;
            }
        }

        private void finish()
        {
            effectCount = 0;
            slowtime = 0;
            TimeManager.timeRate *= 10;

            foreach (var target in targets)
            {
                PlayerStatus status = player.status;
                List<AtkTags> atkTags = new List<AtkTags>();
                atkTags.Add(AtkTags.physicalAttack);
                atkTags.Add(AtkTags.specialSkill);
                new DmgGiveEvent(status.calculateTrueDamage(atkTags, atkCoef:40*target.Value), 1, player, target.Key, atkTags);

                BloodLoss bloodLoss = new BloodLoss(status.calculateTrueDamage(new List<AtkTags>
                {
                    AtkTags.StatusEffect
                }, atkCoef:40), target.Value, player);
                bloodLoss.registrarTarget(target.Key);
            }
        }

        public void execute()
        {
            timeleft = cooltime;
            energy = 0;
            TimeManager.timeRate *= 0.1f;
            this.slowtime = slowDuration;
        }

        public void requireObjects(List<GameObject> objects)
        {
            timeProgressBar=objects[0].GetComponent<ProgressBar>();
            energyProgressBar=objects[1].GetComponent<ProgressBar>();
            impact=objects[2];
            flame=objects[3];
        }

        public void eventActive<T>(T eventArgs) where T : EventArgs
        {
            if (eventArgs is DmgGiveEvent dmgEvent)
            {
                if (dmgEvent.atkTags.Contains(AtkTags.normalAttack))
                {
                    energy += 1;
                }
                else if (dmgEvent.atkTags.Contains(AtkTags.normalSkill))
                {
                    energy += 2;
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
            if (energy > 40) energy = 40;
        }

        public void registrarTarget(Entity target)
        {
            player = target;
            player.addListener(this);
        }

        public void removeSelf()
        {
            player.removeListener(this);
        }
    }
}