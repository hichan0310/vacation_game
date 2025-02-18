using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameBackend.Objects
{
    public class MotionHelper1:SkillEffect
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
            if (!actived && timer >= 0.2)
            {
                Debug.Log("triggered");
                actived = true;
                animator.SetTrigger(Atk);
            } 
        }

        public void setInfo(Entity player)
        {
            float tmp = 1;
            if (player.transform.localScale.x < 0)
            {
                Debug.Log("-");
                tmp = -1;
            }
            //this.transform.SetParent(player.transform);
            this.transform.position = player.transform.position;
            this.transform.localScale = new Vector3(tmp, 1, 1);
            this.transform.localPosition = new Vector3(0.34f*tmp, 0.76f, 0f);
        }
    }
}