using System.Collections;
using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend
{
    public interface ISkill : IEntityEventListener
    {
        public string name { get; }
        public string description { get; }
        public bool active { get; }
        public float timeleft { get; }
        public void execute();
        public void requireObjects(List<GameObject> objects);
    }

    public class TestSkill : ISkill
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        public string name => "TestSkill";

        public string description => "TestSkillDescription";

        private const float cooltime = 5;
        private Entity player;
        private int supportAttack = 0;
        
        
        public GameObject motionHelper1{get;private set;}
        public GameObject motionHelper2{get;private set;}
        public GameObject chamgyuck1{get;private set;}
        public GameObject chamgyuck2{get;private set;}

        public float timeleft { get; private set; }

        public bool active => timeleft == 0;

        public TestSkill()
        {
            timeleft = cooltime;
        }

        public void requireObjects(List<GameObject> objects)
        {
            this.motionHelper1=objects[0];
            this.motionHelper2=objects[1];
            this.chamgyuck1=objects[2];
            this.chamgyuck2=objects[3];
        }

        public void update(float deltaTime)
        {
            timeleft-=deltaTime;
            if (timeleft < 0) timeleft = 0;
            
            
            //todo
        }

        public void execute()
        {
            timeleft = cooltime;
            supportAttack = 300;
        }
        
        public void eventActive<T>(T eventArgs) where T : EventArgs
        {
            if (eventArgs is NormalAttackExecuteEvent)
            {
                if (supportAttack > 0)
                {
                    activateAttack();
                    supportAttack -= 1;
                }
            }
        }

        private void activateAttack()
        {
            GameObject instance1 = Object.Instantiate(motionHelper1);
            GameObject instance2 = Object.Instantiate(motionHelper2);
            GameObject chamgyuckInstance1 = Object.Instantiate(chamgyuck1);
            GameObject chamgyuckInstance2 = Object.Instantiate(chamgyuck2);

            MotionHelper1 helper1 = instance1.GetComponent<MotionHelper1>();
            helper1.setInfo(player);
            Chamgyuck1 cham1 = chamgyuckInstance1.GetComponent<Chamgyuck1>();
            cham1.setInfo(player);
            MotionHelper2 helper2 = instance2.GetComponent<MotionHelper2>();
            helper2.setInfo(player);
            Chamgyuck2 cham2 = chamgyuckInstance2.GetComponent<Chamgyuck2>();
            cham2.setInfo(player);
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
    
    
    
    public class TestSpecialSkill : ISkill, IEntityEventListener
    {
        public string name => "TestSkill";
        public string description => "TestSkillDescription";
        private const float cooltime = 10;
        private int energy=0;
        private Entity player;
        private float slowtime = 0;
        
        private Dictionary<Entity, int> targets = new Dictionary<Entity, int>();

        public float timeleft
        {
            get => timeleft;
            private set => timeleft = value;
        }

        public bool active => timeleft == 0 && energy == 20;

        public TestSpecialSkill()
        {
            timeleft = cooltime;
        }

        public void update(float deltaTime)
        {
            timeleft -= deltaTime;
            if (timeleft < 0) timeleft = 0;
            if (slowtime > 0)
            {
                slowtime -= deltaTime;
                if (slowtime <= 0)
                {
                    finish();
                }
            }
        }

        private void finish()
        {
            slowtime = 0;
            TimeManager.timeRate *= 10;

            foreach (var target in targets)
            {
                PlayerStatus status = player.status;
                List<AtkTags> atkTags = new List<AtkTags>();
                atkTags.Add(AtkTags.physicalAttack);
                atkTags.Add(AtkTags.specialSkill);
                DmgGiveEvent dmgGiveEvent = new DmgGiveEvent(
                    status.calculateTrueDamage(atkTags, atkCoef:40),
                    1, 
                    player, 
                    target.Key, 
                    atkTags);
                player.eventActive(dmgGiveEvent);
                target.Key.dmgtake(dmgGiveEvent);

                BloodLoss bloodLoss = new BloodLoss((int)(0.4 * status.atk), target.Value);
                bloodLoss.registrarTarget(target.Key);
            }
        }

        public void execute()
        {
            timeleft = cooltime;
            TimeManager.timeRate *= 0.1f;
            this.slowtime = 7;
        }

        public void requireObjects(List<GameObject> objects)
        {
            // 오브젝트 추가해서 여기에서 스킬 이펙트 구현
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