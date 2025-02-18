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
            animator = GetComponent<Animator>();
            Invoke("destroy", 1f);
        }

        protected override void update(float deltaTime)
        {
            this.timer += deltaTime;
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