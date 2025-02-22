using System;
using System.Linq;
using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Skills;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend.Objects
{
    public class PlayerObject : Player<TestPlayerInfo1>
    {
        private List<AtkTags> atkTags = new() { AtkTags.normalAttack, AtkTags.physicalAttack };
        private Dictionary<string, int> normalAttackDamage;
        private List<Collider2D> collidersInside = new();
        private int dmg;
        private int atknum = 0;
        public GameObject hpBar;
        private GameObject hpBarObject;
        private ProgressBar progressBar;
        
        private float lastAttackTime = 0f;
        private float comboResetTime = 0.8f;
        private float attackCooldown = 0.25f;
        private string tmp = "";
        private bool direction = true;
        private float movePower = 1.6f;
        private float jumpPower = 3.5f;
        private bool isJumping = false;
        private bool isJumpatk = false;
        private bool isnormalattack = false;
        private Rigidbody2D rigid;
        private Entity player;     

        public Skill normalSkill;
        public Skill specialSkill;
        public List<Artifect> artifects;
        
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Start()
        {
            normalAttackDamage = new Dictionary<string, int>
            {
                { "attack_a", 15 },
                { "attack_b", 15 },
                { "attack_c", 20 },
                { "attack_d", 23 },
                { "attack_jump", 50 }
            };

            rigid = this.gameObject.GetComponent<Rigidbody2D>();
            hpBarObject=Instantiate(hpBar, transform, true);
            hpBarObject.transform.localPosition = new Vector3(0, 0.8f, 0);
            progressBar = hpBarObject.GetComponent<ProgressBar>();
            this.status = new PlayerStatus(10000, 1000, 100);

            normalSkill.registrarTarget(this);
            specialSkill.registrarTarget(this);

            foreach (var artifect in artifects)
            {
                artifect.registrarTarget(this);
            }
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            if (this.dead)
            {
                Destroy(hpBarObject);
            }
            else
            {
                progressBar.ratio=(float)status.nowHp/status.maxHp;
                progressBar.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
            animator.SetBool("jumping", isJumping);
            animator.SetInteger("atknum", atknum);
            Move(deltaTime);
            Jump(deltaTime);
            NormalSkill(deltaTime);
            SpecialSkill(deltaTime);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (!isJumping)
                {
                    animator.ResetTrigger("walk");
                    AttemptAttack();
                }
                else 
                {
                    if (!isJumpatk)
                    {
                        animator.SetTrigger("jumpatk");
                        isJumpatk = true;
                    }
                }
            }
            
            if (this.dead)
            {
                Destroy(hpBarObject);
            }
            else
            {
                progressBar.ratio=(float)status.nowHp/status.maxHp;
                progressBar.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
        }

        void AttemptAttack()
        {
            float currentTime = Time.time;
            
            if (currentTime - lastAttackTime < attackCooldown)
            {
                return; 
            }

            if (currentTime - lastAttackTime > comboResetTime)
            {
                tmp = ""; 
                atknum = 0;
            }

            PlayAttackAnimation();
            lastAttackTime = currentTime;
            atknum = (atknum + 1) % 4; 
        }

        void PlayAttackAnimation()
        {
            animator.SetInteger("atknum", atknum);
            animator.SetTrigger("atk");
            isnormalattack = true;
            NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
            this.eventActive(evnt);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Plate")
            { 
                if (isJumping) animator.ResetTrigger("atk");
                isJumping = false;
                isJumpatk = false;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!collidersInside.Contains(other) && other.gameObject.tag == "Enemy")
            {
                collidersInside.Add(other);
            }
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            if (collidersInside != null)
            {
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("attack_a") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_b") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_c") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_d") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_jump")) &&
                    (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != $"{tmp}"))
                {
                    tmp = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                    player = this;
                    dmg = player.status.calculateTrueDamage(atkTags, atkCoef: normalAttackDamage[tmp]);
                    foreach (Collider2D enemycollider in collidersInside)
                    {
                        if (enemycollider is PolygonCollider2D && !enemycollider.gameObject.GetComponent<Enemy>().dead)
                            new DmgGiveEvent(dmg, (tmp == "attack_jump") ? 0.5f : 0f, player,
                                enemycollider.gameObject.GetComponent<Enemy>(), atkTags);
                    }
                }
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (collidersInside.Contains(other))
            {
                collidersInside.Remove(other);
            }
        }

        void NormalSkill(float deltaTime)
        {
            if (this.normalSkill.active && Input.GetKey(InputHandler.Skill))
            {
                this.normalSkill.execute();
            }
        }

        void SpecialSkill(float deltaTime)
        {
            if (this.specialSkill.active && Input.GetKey(InputHandler.Ultimate))
            {
                this.specialSkill.execute();
            }
        }

        void Move(float deltaTime)
        {
            Vector3 moveVelocity = Vector3.zero;
            if (InputHandler.MoveLeft.Any(key => Input.GetKey(key)))
            {
                animator.SetTrigger("walk");
                if (direction == true)
                {
                    direction = false;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }

                moveVelocity = Vector3.left;
            }
            else if (InputHandler.MoveRight.Any(key => Input.GetKey(key)))
            {
                animator.SetTrigger("walk");
                if (direction == false)
                {
                    direction = true;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }

                moveVelocity = Vector3.right;
            }
            this.transform.position += moveVelocity * (movePower * deltaTime);
        }

        void Jump(float deltaTime)
        {
            if (Input.GetKey(InputHandler.Jump))
            {
                if (!isJumping)
                {
                    tmp = "";
                    atknum = 0;
                    animator.SetInteger("atknum", atknum);
                    animator.SetTrigger("jump");
                    isJumping = true;
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2(0, jumpPower);
                    rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
            }
        }
    }
}