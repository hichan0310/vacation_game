using GameBackend;
using UnityEngine;

namespace GameFrontEnd.Effects.SkillEffects.TestSkill
{
    public class MotionHelper2 : SkillEffect
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        private bool actived;
        private int direction;
        private Vector3 playerPosition;

        public void Start()
        {
            timeIgnore = true;
            setAlpha(0);
        }

        protected override void update(float deltaTime)
        {
            this.timer += deltaTime;
            checkAlpha(0.1f, 0.3f, 0, 1);
            checkAlpha(0.6f, 0.8f, 1, 0);
            checkMove(0.1f, 0.4f,
                this.playerPosition + new Vector3(-0.54f * direction, -1.56f, 0f),
                this.playerPosition + new Vector3(0.34f * direction, -0.76f, 0f));
            checkDestroy(0.8f);

            if (!actived && timer >= 0.3)
            {
                actived = true;
                animator.SetTrigger(Atk);
            }
        }

        public void setInfo(Entity player)
        {
            this.playerPosition = player.transform.position;
            this.transform.position = player.transform.position;
            this.transform.localScale = player.transform.localScale;
            this.direction = (int)player.transform.localScale.x;
            this.transform.localPosition = new Vector3(0.34f, -0.76f, 0f);
        }
    }
}