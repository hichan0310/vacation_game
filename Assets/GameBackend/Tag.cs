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
        jumpAttack = 3,
        physicalAttack = 4,
        fireAttack = 5,
        waterAttack = 6,
        windAttack = 7,
        lightningAttack = 8,
        shadowAttack = 9,
        iceAttack = 10,
        criticalHit = 11,
        statusEffect = 12,
        selfHit = 13,
        bloodLossDamage = 14,
        artifactDamage = 15,
        projectileDamage = 16,
        notcriticalHit = 17,
    }

    public static class Tag
    {
        public static IEnumerable<AtkTags> atkTagIter = Enum.GetValues(typeof(AtkTags)).Cast<AtkTags>();
        public static int atkTagCount = Enum.GetValues(typeof(AtkTags)).Length;
    }
}