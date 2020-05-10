using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    private Collider[] asd;

    private Action _onDestroyCallback = delegate { };
    //list of scriptable objects which contain different tiles data
    public List<NodeData> dataPrefabs;

    private Dictionary<NodeSharedData.Type, NodeData> _dataTable;

    //amount of rows and columns to display
    public int rows;
    public int columns;

    //offsets between rows and columns
    public float tileRowOffset;
    public float tileColumnOffset;

    public GameObject nodePrefab;//base hexagon prefab to load
    public string CsvDataPath; //spreadsheet to determine which tile to load on each position

    private int[,] parsedMapValues;

    public void Initialize()
    {
        parsedMapValues = CSVParser.Parse(CsvDataPath, rows, columns); //rows and columns need to match the rows and columns on the csv. TODO : Determine rows and columns from csv input
        InitializeDataTable();
    }

    private void InitializeDataTable()
    {
        _dataTable = new Dictionary<NodeSharedData.Type, NodeData>(); //cached tiles data. Todo : create a class that can provide this info

        foreach (var data in dataPrefabs)
        {
            if (!_dataTable.ContainsKey(data.type))
            {
                _dataTable.Add(data.type, data);
            }
            else
            {
                throw new System.Exception("Duplicated tile data element");
            }
        }
    }

    //This function creates the map and configures it according to the rows, columns and the loaded CSV file
    public IConfigurableAstarNode[,] CreateMap(Action<IObservable> OnClickedCallback)
    {
        var map = new IConfigurableAstarNode[rows, columns];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject element = Instantiate(nodePrefab);

                /*tile position and color configuration*/
                IConfigurableAstarNode ConfigurableComponent = element.GetComponent<IConfigurableAstarNode>();
                ConfigurableComponent.Initialize();
                float columnOffset = i % 2 == 0 ? tileColumnOffset * 0.5f : tileColumnOffset;

                Vector3 offsetDistance = new Vector3(i * tileRowOffset, 0, j * tileColumnOffset + columnOffset);
                ConfigurableComponent.Transform.name = "(" + i + ")" + " " + "(" + j + ")";
                ConfigurableComponent.Transform.position = transform.position + offsetDistance;
                ConfigurableComponent.Configure(_dataTable[(NodeSharedData.Type)parsedMapValues[i, j]], new Vector3(i, 0, j));

                /*setting tile interaction callbacks*/
                ConfigurableComponent.RegisterObserverCallback(OnClickedCallback);

                //adding the element to the map
                map[i, j] = ConfigurableComponent;
  
                //making connection with previous nodes
                ConnectNodes(map, i, j);
            }
        }

        return map;
    }

    /*Creates a 2 way node connection*/
    private void ConnectNodes(IConfigurableAstarNode[,]map,int row, int column)
    {
        var current = map[row,column];

        /*the following block of code checks bounds with previous created nodes and make a 2 way connection*/
        if (column - 1 >= 0)
        {
            var toConnect = map[row, column - 1];
            ConnectNodesBothWays(current, toConnect);
        }
        if (row - 1 >= 0)
        {
            var toConnect = map[row - 1, column];
            ConnectNodesBothWays(current, toConnect);

            if (column + 1 < columns && (row - 1) % 2 == 0) //we need to check if the previous row is even to make the connections work in this hexagonal map. We can avoid it in a square map
            {
                var toConnect2 = map[row - 1, column + 1];
                ConnectNodesBothWays(current, toConnect2);
            }

            if (column - 1 >= 0 && (row - 1) % 2 == 1)//we need to check if the previous row is not even to make the connections work in this hexagonal map. We can avoid it in a square map
            {
                var toConnect3 = map[row - 1, column - 1];
                ConnectNodesBothWays(current, toConnect3);
            }
        }
    }

    private void ConnectNodesBothWays(IConfigurableAstarNode node1, IConfigurableAstarNode node2)
    {
        node1.AddNeighbour(node2);
        node2.AddNeighbour(node1);
    }

    public Action OnDestroyCallback
    {
        set
        {
            _onDestroyCallback = value;
        }
    }

    private void OnDestroy()
    {
        _onDestroyCallback();
    }
}
