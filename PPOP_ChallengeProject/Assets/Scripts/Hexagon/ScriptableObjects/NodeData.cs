using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "ScriptableObjects/NodeData", order = 1)]
public class NodeData : ScriptableObject
{
    public NodeSharedData.Type type;
    public bool isWalkable;
    public float cost;
    public Texture albedo;
}
