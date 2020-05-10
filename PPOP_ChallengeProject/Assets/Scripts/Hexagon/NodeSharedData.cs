using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSharedData: MonoBehaviour
{
    private static NodeSharedData _instance;

    [SerializeField]
    private Color _extremeColor;

    [SerializeField]
    private Color _routeColor;

    [SerializeField]
    private Color _defaultColor;

    private Dictionary<TintColor,Color> _colorTable;

    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
            InitializeColorTable();
        }
    }
    public enum Type
    {
        GRASS,
        FOREST,
        DESERT,
        MOUNTAIN,
        WATER
    }

    public enum TintColor
    {
        EXTREMES,
        ROUTE,
        NO_TINT
    }

    public static NodeSharedData Instance
    {
        get
        {
            return _instance;
        }
    }

    /*returns a tile color based on id*/
    public Color GetColor(TintColor color)
    {
        if(!_colorTable.ContainsKey(color))
        {
            throw new System.Exception("Color id non existent");
        }
        return _colorTable[color];
    }

    private void InitializeColorTable()
    {
        _colorTable = new Dictionary<TintColor, Color>();

        _colorTable.Add(TintColor.EXTREMES, _extremeColor);
        _colorTable.Add(TintColor.ROUTE, _routeColor);
        _colorTable.Add(TintColor.NO_TINT, _defaultColor);
    }
}