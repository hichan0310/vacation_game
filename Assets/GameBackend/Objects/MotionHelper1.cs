using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameBackend.Objects
{
    public class MotionHelper1 : SkillEffect
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        private bool actived;
        private int direction;
        private Vector3 playerPosition;


        public void Start()
        {
            setAlpha(0);
            Invoke("destroy", 0.7f);
        }

        protected override void update(float deltaTime)
        {
            this.timer += deltaTime;
            checkAlpha(0, 0.2f, 0, 1);
            checkAlpha(0.5f, 0.7f, 1, 0);
            checkMove(0, 0.3f,
                this.playerPosition + new Vector3(-0.54f * direction, 1.56f, 0f),
                this.playerPosition + new Vector3(0.34f * direction, 0.76f, 0f));

            if (!actived && timer >= 0.2)
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
            this.transform.localPosition = new Vector3(0.34f, 0.76f, 0f);
        }
    }
}