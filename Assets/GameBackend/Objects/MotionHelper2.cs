using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameBackend.Objects
{
    public class MotionHelper2:SkillEffect
    {
        private static readonly int Atk = Animator.StringToHash("atk");
        private bool actived;
        
        public void Start()
        {
            setAlpha(0);
            Invoke("destroy", 0.8f);
        }

        protected override void update(float deltaTime)
        {
            this.timer += deltaTime;
            checkAlpha(0.1f, 0.3f, 0, 1);
            checkAlpha(0.6f, 0.8f, 1, 0);
            checkMove(0.1f, 0.4f, new Vector3(-0.54f, -1.56f, 0f), new Vector3(0.34f, -0.76f, 0f));
            
            if (!actived && timer >= 0.3)
            {
                actived = true;
                animator.SetTrigger(Atk);
            }
        }

        public void setInfo(Entity player)
        {
            this.transform.position = player.transform.position;
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.localPosition = new Vector3(0.34f, -0.76f, 0f);
        }
    }
}