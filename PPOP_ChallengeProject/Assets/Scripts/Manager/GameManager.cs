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


    private IConfigurableAstarNode _startNode;

    private IConfigurableAstarNode _endNode;

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
        if(_startNode == null)
        {
            _startNode = (IConfigurableAstarNode)node;
        }
        else if(_endNode == null && node != _startNode)
        {
            _endNode = (IConfigurableAstarNode)node;
            _path = AStar.GetPath(_startNode, _endNode);
            if(_path.Count > 0)
            {
                for (int i = 0; i < _path.Count; i++)
                {
                    var tintableItem = (ITintable)_path[i];
                    if (tintableItem != null)
                    {
                        var color = (i == 0 || i == _path.Count - 1) ? NodeSharedData.TintColor.EXTREMES : NodeSharedData.TintColor.ROUTE;

                        tintableItem.Tint(NodeSharedData.Instance.GetColor(color));
                    }
                }
            }
            else
            {
                print("no path");
            }
        }
        if(_path == null)
        {
            node.Tint(NodeSharedData.Instance.GetColor(NodeSharedData.TintColor.EXTREMES));
        }
        
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

    public void ResetPathing()
    {
        for (int i = 0; i < _path.Count; i++)
        {
            var tintableItem = (ITintable)_path[i];
            if (tintableItem != null)
            { 
                tintableItem.Tint(NodeSharedData.Instance.GetColor(NodeSharedData.TintColor.NO_TINT));
            }
        }
        _path = null;
        _startNode = null;
        _endNode = null;
    }
}
