using GameBackend;
using UnityEngine;

namespace GameFrontEnd.Effects.ArtifactEffect.ElectricStone
{
    public class ElectricSparkExplosion:SkillEffect
    {
        private void Start()
        {
            this.timeIgnore = true;
            this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            checkDestroy(0.5f);
        }
    }
}