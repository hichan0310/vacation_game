using GameBackend;
using UnityEngine;

namespace GameFrontEnd.Effects
{
    public class MuzzleFlashVFX:SkillEffect
    {
        private void Start()
        {
            timeIgnore = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        protected override void update(float deltaTime)
        {
            timer+=deltaTime;
            checkDestroy(1);
        }
    }
}