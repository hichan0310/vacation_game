using GameBackend.Events;
using GameBackend.Objects;
using GameBackend.Objects.SkillEffects.TestSkill;
using UnityEngine;

namespace GameBackend.Skills
{
    public class TestSkill : Skill
    {
        public override string skillName => "TestSkill";

        public override string description => "TestSkillDescription";

        private const float cooltime = 5;
        private int supportAttack;

        public MotionHelper1 motionHelper1;
        public MotionHelper2 motionHelper2;
        public Chamgyuck1 chamgyuck1;
        public Chamgyuck2 chamgyuck2;
        public NormalSkillEffect effect;
        public ProgressBar progressbar;

        public override float timeleft { get; set; }

        public override bool active => timeleft <= 0;

        private void Start()
        {
            timeleft = cooltime;
        }

        public override void update(float deltaTime)
        {
            timeleft-=deltaTime;
            if (timeleft < 0) timeleft = 0;
            progressbar.ratio = -timeleft / cooltime+1;
        }

        public override void execute()
        {
            timeleft = cooltime;
            supportAttack = 3;
            NormalSkillEffect obj=Instantiate(effect, this.player.transform);
            obj.transform.localPosition = new Vector3(0, 0.2f, 0);
        }

        public override void eventActive<T>(T eventArgs)
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
            MotionHelper1 instance1 = Instantiate(motionHelper1);
            MotionHelper2 instance2 = Instantiate(motionHelper2);
            Chamgyuck1 chamgyuckInstance1 = Instantiate(chamgyuck1);
            Chamgyuck2 chamgyuckInstance2 = Instantiate(chamgyuck2);

            MotionHelper1 helper1 = instance1.GetComponent<MotionHelper1>();
            helper1.setInfo(player);
            Chamgyuck1 cham1 = chamgyuckInstance1.GetComponent<Chamgyuck1>();
            cham1.setInfo(player);
            MotionHelper2 helper2 = instance2.GetComponent<MotionHelper2>();
            helper2.setInfo(player);
            Chamgyuck2 cham2 = chamgyuckInstance2.GetComponent<Chamgyuck2>();
            cham2.setInfo(player);
        }
    }
}