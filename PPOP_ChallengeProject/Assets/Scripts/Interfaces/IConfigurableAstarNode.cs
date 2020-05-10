using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public interface IConfigurableAstarNode : ITransform
{
    void AddNeighbour(IConfigurableAstarNode n);
    void Configure(NodeData data, Vector2 coordinates);
    void Initialize();
    bool isWalkable { get; }
    float Cost { get; }
    NodeSharedData.Type Type {get;}
    Vector2 Coordinates { get; }
}
