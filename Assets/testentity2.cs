using System.Collections;
using System.Collections.Generic;
using GameBackend;
using GameBackend.Status;
using UnityEngine;

public class testentity2 : Entity
{
    public testentity2(): base()
    {
        this.status = new PlayerStatus(10000, 300, 300);
    }
    protected override void update(float deltaTime)
    {
        
    }
}
