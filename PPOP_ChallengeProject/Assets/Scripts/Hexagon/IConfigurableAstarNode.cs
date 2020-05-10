using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfigurableAstarNode : IObservable
{
    void AddNeighbour(IConfigurableAstarNode n);
    void Configure(NodeData data, Vector3 coordinates);
    void Initialize();
    Vector3 Coordinates { get; }
    Transform Transform { get; }
}
