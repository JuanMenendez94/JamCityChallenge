using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodeData))]
public class NodeDataEditor : Editor
{
    private NodeData _target;

    public override void OnInspectorGUI()
    {
        _target = (NodeData)target;

        _target.isWalkable = EditorGUILayout.Toggle("Is Walkable", _target.isWalkable);
        _target.albedo = (Texture)EditorGUILayout.ObjectField("Albedo",_target.albedo, typeof(Texture),false);
        _target.type = (NodeSharedData.Type)EditorGUILayout.EnumPopup("Type", _target.type);

        if (_target.isWalkable)
        {
            _target.cost = EditorGUILayout.IntField("Cost",_target.cost);
        }
    }
}
