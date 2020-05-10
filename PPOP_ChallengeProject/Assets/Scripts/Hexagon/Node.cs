using System.Collections;
using System.Collections.Generic;
using PathFinding;
using NodeSharedData;
using UnityEngine;

public class Node : MonoBehaviour, IConfigurableAstarNode
{
    private List<IConfigurableAstarNode> _neighbours;
    private Renderer _renderer;
    private float _cost;

    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            return _neighbours;
        }
    }

    public float CostTo(IAStarNode neighbour)
    {
        return 0;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        return 0;
    }

    //we will set the data from the scriptable objects to the node here.
    public void Configure(NodeData data)
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetTexture("_MainTex", data.albedo);
        _cost = data.isWalkable ? data.cost : Mathf.Infinity;
    }

    public Vector3 Size
    {
        get
        {
            return _renderer.bounds.size;
        }
    }

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public void AddNeighbour(IConfigurableAstarNode n)
    {
        _neighbours.Add(n);
    }
}
