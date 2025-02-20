using System;

namespace GameBackend.Objects
{
    public class NormalSkillEffect:SkillEffect
    {
        public void Start()
        {
            Invoke("destroy", 0.5f);
        }

        protected override void update(float deltaTime)
        {
            
        }
    }
}