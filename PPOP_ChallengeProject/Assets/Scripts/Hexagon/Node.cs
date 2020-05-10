using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;
using System;

public class Node : MonoBehaviour, IConfigurableAstarNode , IClickable , IObservable
{
    private List<IAStarNode> _neighbours;
    private float _cost;
    private List<Action<IObservable>> _clickCallbacks;
    private Vector2 _coordinates;
    private NodeSharedData.Type _type;
    private bool _isWalkable;

    public IEnumerable<IAStarNode> Neighbours
    {
        get
        {
            return _neighbours;
        }
    }

    public float CostTo(IAStarNode neighbour)
    {
        IConfigurableAstarNode node = (IConfigurableAstarNode)neighbour;
        if (node != null)
        {
            return node.Cost;
        }
        else
        {
            throw new System.Exception("Can't cast IAstarNode");
        }

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
        else
        {
            throw new System.Exception("Can't cast IAstarNode");
        }
    }

    public void Initialize()
    {
        _clickCallbacks = new List<Action<IObservable>>();
        _neighbours = new List<IAStarNode>();
    }

    //we will set the data from the scriptable objects to the node here.
    public void Configure(NodeData data,Vector2 coordinates)
    {
        _coordinates = coordinates;
        _isWalkable = data.isWalkable;
        _cost = data.isWalkable ? data.cost : Mathf.Infinity;
        _type = data.type;

        ITintable tintComponent = GetComponent<ITintable>();
        if (tintComponent != null)
        {
            tintComponent.SetTexture(data.albedo);
        }
    }

    /*Properties*/
    public bool isWalkable
    {
        get
        {
            return _isWalkable;
        }
    }

    public float Cost
    {
        get
        {
            return _cost;
        }
    }
    public Vector2 Coordinates
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

    //Behaviors to trigger when this tile is clicked.
    public List<Action<IObservable>> ObservedCallbacks
    {
        get
        {
            return _clickCallbacks;
        }
    }
    /*------------------------------------------------------*/
    public void AddNeighbour(IConfigurableAstarNode n)
    {
        _neighbours.Add(n);
    }

    //triggers click callbacks
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

    //Uncomment for connections debugging
    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;

        foreach(var n in _neighbours)
        {
            var a  = (IConfigurableAstarNode)n;
            Gizmos.DrawLine(transform.position + Vector3.up *0.5f,a.Transform.position + Vector3.up*0.5f);
        }*/
    }
}
