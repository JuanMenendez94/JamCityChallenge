using UnityEngine;
using PathFinding;

public interface ITintable : IAStarNode
{
    void Tint(Color color);//tints the tile
}
