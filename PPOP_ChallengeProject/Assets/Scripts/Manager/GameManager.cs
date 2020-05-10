using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

[RequireComponent(typeof(MapCreator))]
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;


    private IConfigurableAstarNode[,] _map;
    private MapCreator _mapCreator;


    private IConfigurableAstarNode startNode;

    private IConfigurableAstarNode endNode;

    private IList<IAStarNode> _path;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _mapCreator = GetComponent<MapCreator>();
        Initialize();
    }


    private void Initialize()
    {
        _mapCreator.OnDestroyCallback = OnDestroyCallback;
        _mapCreator.Initialize();
        _map = _mapCreator.CreateMap(NotifyClickedTile);
    }

    private void NotifyClickedTile(IObservable node)
    {
        if(startNode == null)
        {
            startNode = (IConfigurableAstarNode)node;
        }
        else if(endNode == null)
        {
            endNode = (IConfigurableAstarNode)node;
            _path = AStar.GetPath(startNode, endNode);
        }
        else
        {
            return;
        }
        node.Tint(NodeSharedData.Instance.GetColor(NodeSharedData.TintColor.EXTREMES));
    }

    private void OnDestroyCallback()
    {
       /* foreach (var item in _map)
        {
            item.UnRegisterObserverCallback(NotifyClickedTile);
        }*/
    }

    public GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
}
