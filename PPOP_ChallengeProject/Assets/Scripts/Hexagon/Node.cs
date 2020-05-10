using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;
using System;

public class Node : MonoBehaviour, IConfigurableAstarNode , IClickable , IObservable
{
    private List<IConfigurableAstarNode> _neighbours;
    private Renderer _renderer;
    private float _cost;
    private List<Action<IObservable>> _clickCallbacks;
    private Vector3 _coordinates;


    NodeData Debugdata;

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
        return 0;
    }

    public void Initialize()
    {
        _renderer = GetComponent<Renderer>();
        _clickCallbacks = new List<Action<IObservable>>();
    }
    //we will set the data from the scriptable objects to the node here.
    public void Configure(NodeData data,Vector3 coordinates)
    {
        Debugdata = data;
        _coordinates = coordinates;
        _renderer.material.SetTexture("_MainTex", data.albedo);
        _cost = data.isWalkable ? data.cost : Mathf.Infinity;
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

    public List<Action> Callbacks => throw new NotImplementedException();

    public void AddNeighbour(IConfigurableAstarNode n)
    {
        _neighbours.Add(n);
    }

    public void OnClick()
    {
        Debug.Log("clicked on tile of type " + Debugdata.type);
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
}
