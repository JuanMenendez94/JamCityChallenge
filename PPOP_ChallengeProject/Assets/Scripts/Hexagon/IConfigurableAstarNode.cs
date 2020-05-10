using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public interface IConfigurableAstarNode : IAStarNode
{
    void AddNeighbour(IConfigurableAstarNode n);
    void Configure(NodeData data);
    Vector3 Size { get; }
    Transform Transform { get; }
}
