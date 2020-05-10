using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

[RequireComponent(typeof(MapCreator))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;


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
        var configurableNode = (IConfigurableAstarNode)node;
        if(!configurableNode.isWalkable)
        {
            UIManager.Instance.TriggerNonWalkableWarning();
            return; // we return because we mustn't process water nodes.
        }
        if(_startNode == null)
        {
            _startNode = configurableNode;
        }
        else if(_endNode == null && node != _startNode)
        {
            _endNode = configurableNode;
            _path = AStar.GetPath(_startNode, _endNode);
            if(_path.Count > 0)
            {
                for (int i = 0; i < _path.Count; i++)
                {
                    var tintableComponent= ((Node)_path[i]).transform.GetComponent<ITintable>();
                    if (tintableComponent != null)
                    {
                        var color = (i == 0 || i == _path.Count - 1) ? NodeSharedData.TintColor.EXTREMES : NodeSharedData.TintColor.ROUTE;

                        tintableComponent.Tint(NodeSharedData.Instance.GetColor(color));
                    }
                }
            }
            else
            {
                print("no path found");
            }
        }
        if(_path == null)
        {
            ITintable tintableComponent = node.Transform.GetComponent<ITintable>();
            if(tintableComponent != null)
            {
                tintableComponent.Tint(NodeSharedData.Instance.GetColor(NodeSharedData.TintColor.EXTREMES));
            }
            
        }
        
    }

    private void OnDestroyCallback()
    {
        foreach (var item in _map)
        {
            if(!item.Equals(null))
            {
                IObservable observableComponent = item.Transform.GetComponent<IObservable>();
                if(observableComponent != null)
                {
                    observableComponent.UnRegisterObserverCallback(NotifyClickedTile);
                }
                
            }      
        }
    }

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void ResetPathing()
    {
        if(_path != null)
        {
            for (int i = 0; i < _path.Count; i++)
            {
                ResetNodeColor((IConfigurableAstarNode)_path[i]);
            }
            _path = null;
        }
        
        if(_startNode != null)
        {
            ResetNodeColor(_startNode);
            _startNode = null;
        }

        if (_endNode != null)
        {
            ResetNodeColor(_endNode);
            _endNode = null;
        }
    }

    private void ResetNodeColor(IConfigurableAstarNode node)
    {
        if(node == null)
        {
            return;
        }

        ITintable tintComponent = ((Node)node).GetComponent<ITintable>();
        if (tintComponent != null)
        {
            tintComponent.Tint(NodeSharedData.Instance.GetColor(NodeSharedData.TintColor.NO_TINT));
        }
    }
}
