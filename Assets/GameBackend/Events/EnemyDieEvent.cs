using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Events
{
    public class EnemyDieEvent:EventArgs
    {
        public EnemyDieEvent()
        {
            name="EnemyDieEvent";
        }

    }
}