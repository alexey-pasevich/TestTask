using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace TestTaskProj
{
    public class PointsContainer : MonoBehaviour
    {
        [SerializeField] public List<Transform> m_points;

        public List<Vector3> GetPoints()
        {
            var list = new List<Vector3>();
            foreach (var p in m_points)
            {
                list.Add(p.position);
            }
            return list;
        }
    }
}
