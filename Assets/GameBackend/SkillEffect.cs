using System;
using UnityEngine;

namespace GameBackend
{
    public abstract class SkillEffect : MonoBehaviour
    {
        protected float timer = 0;
        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        protected abstract void update(float deltaTime);
        
        public virtual void Update()
        {
            update(Time.deltaTime);
        }
        
        protected void destroy()
        {
            Destroy(gameObject);
        }
    }
}