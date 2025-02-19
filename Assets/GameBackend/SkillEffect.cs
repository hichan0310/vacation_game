using System;
using UnityEngine;

namespace GameBackend
{
    public abstract class SkillEffect : MonoBehaviour
    {
        protected float timer = 0;
        protected Animator animator;
        protected SpriteRenderer spriteRenderer;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected abstract void update(float deltaTime);

        public virtual void Update()
        {
            update(Time.deltaTime);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        protected void destroy()
        {
            Destroy(gameObject);
        }

        protected void setAlpha(float alpha)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Clamp01(alpha);
            spriteRenderer.color = color;
        }

        protected void checkAlpha(float start, float end, float startAlpha, float endAlpha)
        {
            if (start <= timer && timer < end)
            {
                setAlpha(Mathf.Lerp(startAlpha, endAlpha, (timer-start)/(end-start)));
            }
        }

        protected void checkMove(float start, float end, Vector3 startPos, Vector3 endPos)
        {
            if (start <= timer && timer < end)
            {
                Vector3 pos = Vector3.Lerp(startPos, endPos, (timer-start)/(end-start));
                this.transform.localPosition = pos;
            }
        }

        protected void checkScale(float start, float end, Vector3 startScale, Vector3 endScale)
        {
            if (start <= timer && timer < end)
            {
                Vector3 pos = Vector3.Lerp(startScale, endScale, (timer-start)/(end-start));
                this.transform.localScale = pos;
            }
        }
    }
}