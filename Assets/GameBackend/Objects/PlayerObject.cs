﻿using System;
using System.Collections.Generic;
using GameBackend.Events;
using GameBackend.Skills;
using GameBackend.Status;
using Unity.VisualScripting;
using UnityEngine;

namespace GameBackend.Objects
{
    public class PlayerObject:Player<TestPlayerInfo1>
    {
        private static readonly int Moving = Animator.StringToHash("moving");
        private List<AtkTags> atkTags = new() { AtkTags.normalAttack, AtkTags.physicalAttack };
        private Dictionary<string, int> normalAttackDamage;
        private int dmg;
        private int atknum = 0;

        private float cooltime_gumgi = 0.5f;
        private float cooltime_click = 0f;
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
        
        public ISkill normalSkill{get;private set;}
        public ISkill specialSkill{get;private set;}

        public void Start()
        {
            normalAttackDamage = new Dictionary<string, int>
            {
                {"attack_a", 15},
                {"attack_b", 15},
                {"attack_c", 20},
                {"attack_d", 23},
                {"attack_jump", 50}
            };
            Collider2D[] playerColliders = this.gameObject.GetComponents<Collider2D>();
            Collider2D enemyCollider = GameObject.Find("Enemy").GetComponent<Collider2D>();
            foreach (Collider2D playerCollider in playerColliders)
            {
                if (!playerCollider.isTrigger)
                {
                    Physics2D.IgnoreCollision(playerCollider, enemyCollider);
                }
            }
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
            if(!isJumping) cooltime_click += deltaTime;
            if(cooltime_click > 1f)
            {
                atknum = 0;
                cooltime_click = 0.25f;
                tmp = "";
            }
            else if(Input.GetMouseButtonDown(0))
            {

                if (!isJumping)
                {
                    if (cooltime_click >= 0.25f)
                    {
                    animator.SetTrigger("atk");
                    isnormalattack = true;
                    NormalAttackExecuteEvent evnt = new NormalAttackExecuteEvent(this, new List<AtkTags>());
                    this.eventActive(evnt);
                    atknum = (atknum < 3) ? atknum + 1 : 0;
                    cooltime_click = 0;
                    }
                }
                else if (!isJumpatk)
                {
                    atknum = 0;
                    cooltime_click = 0.20f;
                    animator.SetTrigger("jumpatk");
                    isJumpatk = true;
                }
            }
            
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Plate")
            {
                if(isJumping) tmp = "";
                isJumping = false;
                isJumpatk = false;
            }
        }

        protected override void OnTriggerStay2D(Collider2D other)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("attack_a") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_b") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_c") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_d") || (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_jump") && !isJumpatk)) && (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != $"{tmp}"))
                {
                    if(tmp == "attack_d" && animator.GetCurrentAnimatorStateInfo(0).IsName("attack_b"))
                        tmp = "attack_a";
                    else if(tmp == "attack_b" && animator.GetCurrentAnimatorStateInfo(0).IsName("attack_d"))
                        tmp = "attack_c";
                    else
                        tmp = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                    player = this;
                    dmg = player.status.calculateTrueDamage(atkTags, atkCoef: normalAttackDamage[tmp]);
                    // Debug.Log(tmp);
                    new DmgGiveEvent(dmg, (tmp == "attack_jump") ? 0.5f : 0f, player, enemy, atkTags);
                }
                
            }
        }
        
        public void balsa()
        {
            GameObject obj = Instantiate(gumgi, this.transform); 
            Gumgi gumgiCompo = obj.GetComponent<Gumgi>();
            gumgiCompo.direction = direction;
            gumgiCompo.position=transform.position;
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
            Vector3 moveVelocity= Vector3.zero;
            if(Input.GetKey(InputHandler.MoveLeft))
            {
                if (direction == true)
                {
                    direction = false;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                animator.SetBool(Moving, true);
                moveVelocity = Vector3.left;
            }
            else if(Input.GetKey(InputHandler.MoveRight))
            {
                if (direction == false)
                {
                    direction = true;
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
                animator.SetBool(Moving, true);
                moveVelocity = Vector3.right;
            }
            animator.SetBool(Moving, false);
            this.transform.position += moveVelocity * (movePower * deltaTime);
        }
        void Jump(float deltaTime)
        {
            if(Input.GetKey(InputHandler.Jump))
            {
                if (!isJumping)
                {
                    animator.SetTrigger("jump");
                    isJumping = true;
                    rigid.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2 (0, jumpPower);
                    rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
                }
	        }
        }
    }
}