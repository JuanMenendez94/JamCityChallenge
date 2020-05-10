using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public interface ITransform : IAStarNode
{
    Transform Transform { get; }
}
