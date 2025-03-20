using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace GameBackend.Status
{
    public class PlayerStatus
    {
        private int baseHp { get; }
        public int addHp { get; set; }
        public float increaseHp { get; set; }

        public int maxHp => (int)(baseHp * (increaseHp / 100 + 1) + addHp);

        public int nowHp { get; set; }
        public int shieldHp { get; set; }

        private int baseAtk { get; }
        public int addAtk { get; set; }
        public float increaseAtk { get; set; }

        public int atk => (int)(baseAtk * (increaseAtk / 100 + 1) + addAtk);

        private int baseDef { get; }
        public int addDef { get; set; }
        public float increaseDef { get; set; }

        public int def => (int)(baseDef * (increaseDef / 100 + 1) + addDef);

        public float crit { get; set; }
        public float critDmg { get; set; }
        public float[] dmgUp { get; set; }
        public float movePower { get; set; }
        public float energyRecharge { get; set; }
        
        // 사실 배열 많이 쓰면 이거 복사할 때 무리가 갈 가능성도 있긴 해서 피증 하나만 하려고 했는데 2d면 딱히 상관 없으려나?
        public float[] dmgDrain { get; set; }

        public PlayerStatus(int baseHp, int baseAtk, int baseDef)
        {
            this.baseHp = baseHp;
            this.addHp = 0;
            this.increaseHp = 0;
            this.nowHp = this.maxHp;
            this.shieldHp = 0;
            this.baseAtk = baseAtk;
            this.addAtk = 0;
            this.increaseAtk = 0;
            this.baseDef = baseDef;
            this.addDef = 0;
            this.increaseDef = 0;
            this.crit = 5;
            this.critDmg = 50;
            this.dmgUp = new float[Tag.atkTagCount];

            this.movePower = 2.2f;
            this.energyRecharge = 100f;
            this.dmgDrain = new float[Tag.atkTagCount];
            for (int i = 0; i < Tag.atkTagCount; i++)
            {
                this.dmgDrain[i] = 1.0f;
            }
        }

        public PlayerStatus(PlayerStatus copy)
        {
            this.baseHp = copy.baseHp;
            this.addHp = copy.addHp;
            this.increaseHp = copy.increaseHp;
            this.nowHp = copy.nowHp;
            this.shieldHp = copy.shieldHp;
            this.baseAtk = copy.baseAtk;
            this.addAtk = copy.addAtk;
            this.increaseAtk = copy.increaseAtk;
            this.baseDef = copy.baseDef;
            this.addDef = copy.addDef;
            this.increaseDef = copy.increaseDef;
            this.crit = copy.crit;
            this.critDmg = copy.critDmg;
            this.dmgUp = new float[Tag.atkTagCount];
            this.dmgDrain = new float[Tag.atkTagCount];
            
            this.movePower=copy.movePower;
            this.energyRecharge = copy.energyRecharge;
            Array.Copy(copy.dmgUp, this.dmgUp, Tag.atkTagCount);
            Array.Copy(copy.dmgDrain, this.dmgDrain, Tag.atkTagCount);
        }

        public int calculateTrueDamage(List<AtkTags> atkTags, float atkCoef=0, float hpCoef=0, float defCoef=0)
        {
            int dmg = (int)((atkCoef * atk + defCoef * def + hpCoef * maxHp)/100);
            if (Random.value < crit / 100 && !atkTags.Contains(AtkTags.notcriticalHit))
            {
                dmg = (int)(dmg * (1 + critDmg / 100));
                atkTags.Add(AtkTags.criticalHit);
            }
            else if (atkTags.Contains(AtkTags.criticalHit))
            {
                atkTags.Remove(AtkTags.criticalHit);
            }

            float dmgUpSum = 0;
            foreach (AtkTags atkTag in atkTags)
            {
                dmgUpSum += dmgUp[(int)atkTag];
                dmg = (int)(dmgDrain[(int)atkTag]*dmg);
            }
            dmg = (int)((dmgUpSum / 100 + 1) * dmg);
            return dmg;
        }
    }

    public interface IBuffStatus
    {
        public void buffStatus(PlayerStatus status);
    }
}