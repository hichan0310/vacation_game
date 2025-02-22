using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBackend
{
    public class InvokeManager
    {
        private class InvokeInfo
        {
            public readonly Action<List<object>> function;
            public readonly List<object> parameters;
            public float time;

            public InvokeInfo(Action<List<object>> function, List<object> parameters, float time)
            {
                this.function = function;
                this.parameters = parameters;
                this.time = time;
            }
        }
        
        private readonly List<InvokeInfo> invokes = new();

        public void invoke(Action<List<object>> func, List<object> args, float delay)
        {
            invokes.Add(new InvokeInfo(func, args, delay));
        }

        public void update(float deltaTime)
        {
            foreach (var invoke in invokes.ToList())
            {
                invoke.time -= deltaTime;
                if (!(invoke.time <= 0)) continue;
                invoke.function(invoke.parameters);
                invokes.Remove(invoke);
            }
        }
    }
}