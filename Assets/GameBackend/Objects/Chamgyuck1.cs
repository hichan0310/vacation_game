using UnityEngine;

namespace GameBackend.Objects
{
    public class Chamgyuck1 :SkillEffect
    {
        public void Start()
        {
            timer = -0.15f;
            setAlpha(0);
            Invoke("destroy", 1f);
        }

        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            checkAlpha(0.2f, 0.3f, 0, 1);
            checkAlpha(0.5f, 0.6f, 1, 0);
            checkScale(0.2f, 0.3f, Vector3.zero, new Vector3(-0.5f, 0.5f, 0.5f));
            checkMove(0.2f, 0.3f, new Vector3(0.34f, 0.76f, 0f), new Vector3(1.09f, 0.36f, 0));
        }

        public void setInfo(Entity player)
        {
            this.transform.position = player.transform.position;
            this.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            this.transform.localPosition = new Vector3(1.09f, 0.36f, 0);
        }
    }
}