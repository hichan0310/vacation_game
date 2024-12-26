using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBackend.Status
{
    public static class Tag
    {
        public enum Tags
        {
            normalAttack = 0,
            normalSkill = 1,
            specialSkill = 2,
            physicalAttack = 3,
            fireAttack = 4,
            waterAttack = 5,
            windAttack = 6,
            earthAttack = 7,
            lightningAttack = 8,
            shadowAttack = 9
        }

        public static IEnumerable<Tags> iter = Enum.GetValues(typeof(Tags)).Cast<Tags>();

        public static int tagCount = Enum.GetValues(typeof(Tags)).Length;
    }

    public class PlayerStatus
    {
        private int maxHp { get; }
        private int nowHp { get; }
        private int atk { get; }
        private int def { get; }
        private int crit { get; }
        private int critDmg { get; }
        private float[] dmgUp { get; }

        PlayerStatus(int maxHp, int nowHp, int atk, int def, int crit, int critDmg)
        {
            this.maxHp = maxHp;
            this.nowHp = nowHp;
            this.atk = atk;
            this.def = def;
            this.crit = crit;
            this.critDmg = critDmg;
            this.dmgUp = new float[Tag.tagCount];
        }
    }
}