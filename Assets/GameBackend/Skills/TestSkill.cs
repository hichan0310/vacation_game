using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Objects;
using UnityEngine;

namespace GameBackend.Skills
{
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
        public GameObject effect{get;private set;}

        public float timeleft { get; private set; }

        public bool active => timeleft <= 0;

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
            this.effect=objects[4];
        }

        public void update(float deltaTime)
        {
            timeleft-=deltaTime;
            if (timeleft < 0) timeleft = 0;
        }

        public void execute()
        {
            timeleft = cooltime;
            supportAttack = 3;
            GameObject obj=Object.Instantiate(effect, this.player.transform);
            obj.transform.localPosition = new Vector3(0, 0.2f, 0);
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
}