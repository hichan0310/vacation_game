using System.Collections;
using System.Collections.Generic;
using GameBackend;
using GameBackend.Events;
using UnityEngine;

public class testentity1 : Entity
{
    private float t = 1;
    public Entity target;

    // Update is called once per frame
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void update(float deltaTime)
    {
        base.update(deltaTime);
        t-=deltaTime;
        if (t < 0)
        {
            t = 1;
            var tags = new List<AtkTags>();
            tags.Add(AtkTags.normalAttack);
            new DmgGiveEvent(100, 0, this, target, tags).trigger();
        }
    }
}
