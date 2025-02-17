using System.Collections.Generic;
using GameBackend.Buffs;
using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Status;

namespace GameBackend
{
    public interface ISkill
    {
        public string name { get; }
        public string description { get; }
        public bool active { get; }
        public float timeleft { get; }
        public void execute(PlayerObject player);
    }

    public class TestSkill : ISkill, IEntityEventListener
    {
        public string name => "TestSkill";

        public string description => "TestSkillDescription";

        private const float cooltime = 5;
        private Entity player;
        private int supportAttack = 0;

        public float timeleft
        {
            get => timeleft;
            private set => timeleft = value;
        }

        public bool active => timeleft == 0;

        public TestSkill()
        {
            timeleft = cooltime;
        }

        public void update(float deltaTime)
        {
            timeleft-=deltaTime;
            if (timeleft < 0) timeleft = 0;
        }

        public void execute(PlayerObject player)
        {
            timeleft = cooltime;
            supportAttack = 3;
        }
        
        public void eventActive<T>(T eventArgs) where T : EventArgs
        {
            if (eventArgs is NormalAttackExecuteEvent)
            {
                if (supportAttack > 0)
                {
                    supportAttack -= 1;
                    //추가 공격 날리기
                }
            }
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
                    (int)(0.4*status.atk*target.Value),
                    player, 
                    target.Key, 
                    atkTags);
                player.eventActive(dmgGiveEvent);
                target.Key.dmgtake(dmgGiveEvent);

                BloodLoss bloodLoss = new BloodLoss((int)(0.4 * status.atk), target.Value);
                bloodLoss.registrarTarget(target.Key);
            }
        }

        public void execute(PlayerObject player)
        {
            timeleft = cooltime;
            TimeManager.timeRate *= 0.1f;
            this.slowtime = 7;
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