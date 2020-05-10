using System.Collections;
using System.Collections.Generic;
using NodeSharedData;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "ScriptableObjects/NodeData", order = 1)]
public class NodeData : ScriptableObject
{
    public Type type;
    public bool isWalkable;
    public int cost;
    public Texture albedo;
}
