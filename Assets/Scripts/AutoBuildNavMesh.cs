using Unity.AI.Navigation;
using UnityEngine;

namespace TestTaskProj
{
    public class AutoBuildNavMesh : MonoBehaviour
    {
        private void Awake()
        {
            var surfaces = GetComponents<NavMeshSurface>();
            foreach (var surface in surfaces)
            { 
                surface.BuildNavMesh();
            }
        }
    }
}
