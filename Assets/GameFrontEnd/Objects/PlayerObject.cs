using System.Collections.Generic;
using System.Linq;
using GameBackend;
using GameBackend.Events;
using GameBackend.Skills.NormalSkill;
using GameBackend.Skills.SpecialSkill;
using GameBackend.Status;
using UnityEngine;
using GameBackend.Buffs;

namespace GameFrontEnd.Objects
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
        private float jumpPower = 4.6f;
        public bool isJumping = false;
        private bool isJumpatk = false;
        private Rigidbody2D rigid;
        private Entity player;

        public NormalSkill normalSkill;
        public SpecialSkill specialSkill;
        public List<Artifact> artifects;

        public bool controlable { get; set; } = true;
        public bool moveable { get; set; } = true;
        public bool jumpable { get; set; } = true;
        public bool attackable { get; set; } = true;
        public bool skillExecutable { get; set; } = true;
        private bool doubleJump { get; set; } = false;
        
        
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
            
            this.normalSkill=Instantiate(this.normalSkill);
            this.specialSkill=Instantiate(this.specialSkill);

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

        public void addArtifact(Artifact artifact)
        {
            this.artifects.Add(artifact);
            artifact.registrarTarget(this);
        }

        protected override void update(float deltaTime)
        {
            
            var vector2 = this.rigid.velocity;
            vector2.x = 0;
            this.rigid.velocity = vector2;
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
            if (controlable)
            {
                if (moveable) Move(deltaTime);
                if (jumpable) Jump(deltaTime);
                if (skillExecutable)
                {
                    NormalSkill(deltaTime);
                    SpecialSkill(deltaTime);
                }

                if (attackable)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!isJumping && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack_jump"))
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
            NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
            evnt.trigger();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (collision.gameObject.tag == "Plate" && contact.normal.y > 0.9f && GetComponent<Rigidbody2D>().velocity.y <= 0) 
                {
                    if (isJumping) animator.ResetTrigger("atk");
                    isJumpatk = false;
                    doubleJump = false;
                    animator.SetBool("doubleJump", false);
                    isJumping = false;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!collidersInside.Contains(other) && other.gameObject.tag == "Enemy" && other is PolygonCollider2D)
            {
                collidersInside.Add(other);
            }
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            if (collidersInside != null && other.gameObject.tag == "Enemy" && other is PolygonCollider2D)
            {
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("attack_a") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_b") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_c") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_d") ||
                     animator.GetCurrentAnimatorStateInfo(0).IsName("attack_jump")) &&
                    !animator.GetCurrentAnimatorStateInfo(0).IsName($"{tmp}"))
                {
                    tmp = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                    player = this;
                    dmg = player.status.calculateTrueDamage(atkTags, atkCoef: normalAttackDamage[tmp]);
                    foreach (Collider2D enemycollider in collidersInside)
                    {
                        if (enemycollider is PolygonCollider2D && !enemycollider.gameObject.GetComponent<Enemy>().dead)
                            new DmgGiveEvent(dmg, (tmp == "attack_jump") ? 0.5f : 0f, player,
                                enemycollider.gameObject.GetComponent<Enemy>(), atkTags).trigger();
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
            if (this.normalSkill.active && Input.GetKeyDown(InputHandler.Skill))
            {
                this.normalSkill.execute();
                NormalSkillExecuteEvent evnt = new NormalSkillExecuteEvent(this, this.normalSkill);
                evnt.trigger();
            }
        }

        void SpecialSkill(float deltaTime)
        {
            if (this.specialSkill.active && Input.GetKeyDown(InputHandler.Ultimate))
            {
                this.specialSkill.execute();
                SpecialSkillExecuteEvent evnt = new SpecialSkillExecuteEvent(this, this.specialSkill);
                evnt.trigger();
            }
        }

        void Move(float deltaTime)
        {
            Vector3 moveVelocity = Vector3.zero;
            if (InputHandler.MoveLeft.Any(key => Input.GetKey((KeyCode)key)))
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
            this.transform.position += moveVelocity * (status.movePower * deltaTime);
        }

        void Jump(float deltaTime)
        {
            if (Input.GetKeyDown(InputHandler.Jump))
            {
                if (!isJumping)
                {
                    doubleJump = true;
                    tmp = "";
                    atknum = 0;
                    animator.SetInteger("atknum", atknum);
                    animator.SetTrigger("jump");
                    
                    isJumping = true;
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2(0, jumpPower);
                    rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
                else if (doubleJump)
                {
                    doubleJump = false;
                    animator.SetBool("doubleJump", false);
                    tmp = "";
                    atknum = 0;
                    animator.SetInteger("atknum", atknum);
                    animator.SetTrigger("jump");
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2(0, jumpPower);
                    rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
            }
        }
    }
}