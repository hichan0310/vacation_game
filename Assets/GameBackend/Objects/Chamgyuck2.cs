using UnityEngine;

namespace GameBackend.Objects
{
    public class Chamgyuck2 :SkillEffect
    {
        public void Start()
        {
            Invoke("destroy", 0.9f);
        }

        protected override void update(float deltaTime)
        {
            
        }

        public void setInfo(Entity player)
        {
            this.transform.position = player.transform.position;
            this.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            this.transform.localPosition = new Vector3(1.09f, -0.36f, 0f);
        }
    }
}
