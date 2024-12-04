//using TheKiwiCoder;
using UnityEngine;
using Unity.Behavior;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestTaskProj
{
    public class NextPointsAction : Action
    {
        private List<Vector3> points = new();
        protected override Status OnStart()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
            
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}
