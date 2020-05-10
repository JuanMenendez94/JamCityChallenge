using UnityEngine;

public class NodeSharedData: MonoBehaviour
{
    private static NodeSharedData _instance;

    [SerializeField]
    private Color _extremeColor;

    [SerializeField]
    private Color _routeColor;

    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
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
        LENGTH
    }

    public static NodeSharedData Instance
    {
        get
        {
            return _instance;
        }
    }

    public Color GetColor(TintColor color)
    {
        if((int)color >= (int)TintColor.LENGTH)
        {
            throw new System.Exception("Color id non existent");
        }
        return color == TintColor.EXTREMES ? _extremeColor : _routeColor; // if we had to handle more colors we could store these values in a table
    }
}