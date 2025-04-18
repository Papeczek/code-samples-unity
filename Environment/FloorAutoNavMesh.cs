using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class FloorAutoNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private Mesh islandMesh;
    private void Awake()
    {
        if(_navMeshSurface == null)
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
        }
        if (meshCollider == null)
        {
            meshCollider = GetComponent<MeshCollider>();
        }
        meshCollider.sharedMesh = islandMesh;

    }
    private IEnumerator Start()
    {
        float delay = 1f;
        yield return new WaitForSeconds(delay);
        _navMeshSurface.BuildNavMesh();
    }
}
