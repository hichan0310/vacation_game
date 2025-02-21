using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Status
{
    public class PlayerStatus
    {
        private int baseHp { get; }
        public int addHp { get; set; }
        public float increaseHp { get; set; }

        public int maxHp
        {
            get { return (int)(baseHp * (increaseHp / 100 + 1) + addHp); }
        }

        public int nowHp { get; set; }

        private int baseAtk { get; }
        public int addAtk { get; set; }
        public float increaseAtk { get; set; }

        public int atk
        {
            get { return (int)(baseAtk * (increaseAtk / 100 + 1) + addAtk); }
        }

        private int baseDef { get; }
        public int addDef { get; set; }
        public float increaseDef { get; set; }

        public int def
        {
            get { return (int)(baseDef * (increaseDef / 100 + 1) + addDef); }
        }

        public float crit { get; set; }
        public float critDmg { get; set; }
        public float[] dmgUp { get; set; }

        public PlayerStatus(int baseHp, int baseAtk, int baseDef)
        {
            this.baseHp = baseHp;
            this.addHp = 0;
            this.increaseHp = 0;
            this.nowHp = this.maxHp;
            this.baseAtk = baseAtk;
            this.addAtk = 0;
            this.increaseAtk = 0;
            this.baseDef = baseDef;
            this.addDef = 0;
            this.increaseDef = 0;
            this.crit = 5;
            this.critDmg = 50;
            this.dmgUp = new float[Tag.atkTagCount];
        }

        public int calculateTrueDamage(List<AtkTags> atkTags, float atkCoef=0, float hpCoef=0, float defCoef=0)
        {
            int dmg = (int)((atkCoef * atk + defCoef * def + hpCoef * maxHp)/100);
            if (Random.value < crit / 100)
            {
                dmg = (int)(dmg * (1 + critDmg / 100));
                atkTags.Add(AtkTags.criticalHit);
            }
            
            foreach (AtkTags atkTag in atkTags)
            {
                dmg = (int)((dmgUp[(int)atkTag] / 100 + 1) * dmg);
            }
            return dmg;
        }
    }

    public interface IBuffStatus
    {
        public void buffStatus(PlayerStatus status);
    }
}