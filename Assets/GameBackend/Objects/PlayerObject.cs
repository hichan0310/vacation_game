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
        private static readonly int Moving = Animator.StringToHash("moving");
        private List<AtkTags> atkTags = new() { AtkTags.normalAttack, AtkTags.physicalAttack };
        private Dictionary<string, int> normalAttackDamage;
        private List<Collider2D> collidersInside = new List<Collider2D>();
        private int dmg;
        private int atknum = 0;

        private float cooltime_gumgi = 0.5f;
        private float lastAttackTime = 0f;
        private float comboResetTime = 0.8f;
        private float attackCooldown = 0.25f;
        private string tmp = "";
        private bool direction = true;
        private float movePower = 1f;
        private float jumpPower = 2f;
        private bool isJumping = false;
        private bool isJumpatk = false;
        private bool isnormalattack = false;
        private Rigidbody2D rigid;
        private Entity player;

        public GameObject normalSkillProgressBar;
        public GameObject motionHelper1;
        public GameObject motionHelper2;
        public GameObject chamgyuck1;
        public GameObject chamgyuck2;
        public GameObject normalSkillEffect;

        public GameObject specialSkillTimeProgressBar;
        public GameObject specialSkillEnergyProgressBar;
        public GameObject specialSkillImpact;
        public GameObject specialSkillFlame;

        public ISkill normalSkill { get; private set; }
        public ISkill specialSkill { get; private set; }

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
            this.status = new PlayerStatus(10000, 1000, 100);

            normalSkill = new TestSkill();
            normalSkill.requireObjects(new List<GameObject>
            {
                normalSkillProgressBar,
                motionHelper1,
                motionHelper2,
                chamgyuck1,
                chamgyuck2,
                normalSkillEffect,
            });
            normalSkill.registrarTarget(this);

            specialSkill = new TestSpecialSkill();
            specialSkill.requireObjects(new List<GameObject>
            {
                specialSkillTimeProgressBar,
                specialSkillEnergyProgressBar,
                specialSkillImpact,
                specialSkillFlame,
            });
            specialSkill.registrarTarget(this);
        }

        protected override void update(float deltaTime)
        {
            base.update(deltaTime);
            animator.SetBool("jumping", isJumping);
            animator.SetInteger("atknum", atknum);
            Move(deltaTime);
            Jump(deltaTime);
            NormalSkill(deltaTime);
            SpecialSkill(deltaTime);
            // cooltime_gumgi += deltaTime;
            // animator.SetTrigger(Atk);
            // if (cooltime_gumgi >= 2)
            // {
            //     
            //     animator.SetTrigger(Atk);
            //     cooltime_gumgi = 0;
            //     direction = !direction;
            //     Invoke("balsa", 0.4f);
            //     NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
            //     this.eventActive(evnt);
            // } 
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
                     (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_jump") && gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)) &&
                    (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != $"{tmp}"))
                {
                    tmp = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                    player = this;
                    dmg = player.status.calculateTrueDamage(atkTags, atkCoef: normalAttackDamage[tmp]);
                    foreach (Collider2D enemycollider in collidersInside)
                    {
                        if (enemycollider is PolygonCollider2D)
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

        public void balsa()
        {
            GameObject obj = Instantiate(gumgi, this.transform);
            Gumgi gumgiCompo = obj.GetComponent<Gumgi>();
            gumgiCompo.direction = direction;
            gumgiCompo.position = transform.position;
            gumgiCompo.time = 0.4f;
            gumgiCompo.speed = 6;
            List<AtkTags> atkTag = new List<AtkTags>();
            atkTag.Add(AtkTags.fireAttack);
            atkTag.Add(AtkTags.normalAttack);
            gumgiCompo.dmgInfo = new DmgInfo(100, 10, this, atkTag);
            gumgiCompo.apply();
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