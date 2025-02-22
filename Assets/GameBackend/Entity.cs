using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Status;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameBackend
{
    public abstract class Entity : MonoBehaviour
    {
        public float speed { get; set; } = 1;
        public bool dead { get; set; } = false;
        private bool rightBefore = false;

        public PlayerStatus status { get; set; } = new(1, 0, 0);
        public List<IEntityEventListener> eventListener { get; set; } = new();

        protected Animator animator;

        private void Awake()
        {
            speed = 1;
            animator = GetComponent<Animator>();
        }

        public void addListener(IEntityEventListener listener)
        {
            this.eventListener.Add(listener);
        }

        public void removeListener(IEntityEventListener listener)
        {
            this.eventListener.Remove(listener);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
        }

        protected virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
        }

        public virtual void eventActive(EventArgs e)
        {
            foreach (var listener in eventListener)
            {
                listener.eventActive(e);
            }
        }

        protected virtual void update(float deltaTime)
        {
            this.eventListener.ToList().ForEach(listener => listener.update(deltaTime));
        }

        public virtual void Update()
        {
            if (dead)
            {
                {
                    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("dead") && this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        Destroy(this.gameObject);
                    }
                }
                if (!rightBefore) return;
                else rightBefore = false;
            }

            update(TimeManager.deltaTime * speed);
        }

        public virtual void dmgtake(DmgGiveEvent dmg)
        {
            int def = status.def;
            int C = 200;
            int realDmg = (int)(dmg.trueDmg * ((float)(C) / (def + C)));

            eventActive(new DmgTakeEvent(realDmg, dmg.attacker, dmg.target, dmg.atkTags));
            status.nowHp -= realDmg;
            if (status.nowHp <= 0) die(dmg.target);
        }

        public virtual bool stagger(float deltaTime)
        {
            return true;
        }

        public virtual bool knockback(float deltaTime)
        {
            return true;
        }

        public virtual void die(Entity dietarget)
        {
            if (dead) return;
            this.dead = true;
            this.rightBefore = true;
            this.animator.SetTrigger("dead");
        }
    }

    public class EmptyEntity : Entity
    {
        public static EmptyEntity Instance { get; private set; }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this; // Singleton 초기화
            }
            else
            {
                Destroy(gameObject); // 중복된 매니저 제거
            }
        }
    }
}