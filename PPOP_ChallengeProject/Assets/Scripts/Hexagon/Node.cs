using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;
using System;

public class Node : MonoBehaviour, IConfigurableAstarNode , IClickable , IObservable
{
    private List<IAStarNode> _neighbours;
    private Renderer _renderer;
    private float _cost;
    private List<Action<IObservable>> _clickCallbacks;
    private Vector3 _coordinates;
    private NodeSharedData.Type _type;

    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            return _neighbours;
        }
    }

    public float CostTo(IAStarNode neighbour)
    {
        return _cost;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        IConfigurableAstarNode node = (IConfigurableAstarNode)target;
        if(node != null)
        {
            Vector2 origin = new Vector2(transform.position.x, transform.position.z);
            Vector2 destination = new Vector2(node.Transform.position.x, node.Transform.position.z); ;
            return Utility.PythagoreanDistance(origin, destination);
        }
        return 0;
    }

    public void Initialize()
    {
        _renderer = GetComponent<Renderer>();
        _clickCallbacks = new List<Action<IObservable>>();
        _neighbours = new List<IAStarNode>();
    }
    //we will set the data from the scriptable objects to the node here.
    public void Configure(NodeData data,Vector3 coordinates)
    {
        _coordinates = coordinates;
        _renderer.material.SetTexture("_MainTex", data.albedo);
        _cost = data.isWalkable ? data.cost : Mathf.Infinity;
        _type = data.type;
    }

    public Vector3 Coordinates
    {
        get
        {
            return _coordinates;
        }
    }
    public Transform Transform
    {
        get
        {
            return transform;
        }
    }
    public NodeSharedData.Type Type
    {
        get
        {
            return _type;
        }
    }

    public List<Action<IObservable>> ObservedCallbacks
    {
        get
        {
            return _clickCallbacks;
        }
    }

    public void AddNeighbour(IConfigurableAstarNode n)
    {
        _neighbours.Add(n);
    }

    public void OnClick()
    {
        foreach(var cb in _clickCallbacks)
        {
            cb(this);
        }
    }

    public void RegisterObserverCallback(Action<IObservable> f)
    {
        _clickCallbacks.Add(f);
    }

    public void UnRegisterObserverCallback(Action<IObservable> f)
    {
        _clickCallbacks.Remove(f);
    }

    public void Tint(Color color)//tints the tile
    {
        _renderer.material.SetColor("_Color", color);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(var n in _neighbours)
        {
            var a  = (IConfigurableAstarNode)n;
            Gizmos.DrawLine(transform.position + Vector3.up *0.5f,a.Transform.position + Vector3.up*0.5f);
        }
    }
}
