using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBackend
{
    public enum AtkTags
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
        shadowAttack = 9,
        criticalHit = 10,
    }

    public static class Tag
    {
        public static IEnumerable<AtkTags> atkTagIter = Enum.GetValues(typeof(AtkTags)).Cast<AtkTags>();
        public static int atkTagCount = Enum.GetValues(typeof(AtkTags)).Length;
    }
}