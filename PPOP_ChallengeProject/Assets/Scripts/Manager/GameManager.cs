using System.Collections;
using System.Collections.Generic;
using NodeSharedData;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<NodeData> dataPrefabs;

    private Dictionary<Type, NodeData> _dataTable;


    public int rows;
    public int columns;
    public float tileRowOffset;
    public float tileColumnOffset;
    public GameObject nodePrefab;
    public string CsvDataPath;

    private IConfigurableAstarNode[,] _map;

    private int[,] parsedMap;

    // Start is called before the first frame update
    void Start()
    {
        parsedMap = CSVParser.Parse(CsvDataPath, rows,columns);


        _dataTable = new Dictionary<Type, NodeData>();

        foreach(var data in dataPrefabs)
        {
            if(!_dataTable.ContainsKey(data.type))
            {
                _dataTable.Add(data.type, data);
            }
        }

        _map = CreateMap();
    }


    //This function creates the map and configures it according to the rows, columns and the loaded CSV file
    private IConfigurableAstarNode[,] CreateMap()
    {
        var tempMap = new IConfigurableAstarNode[rows, columns];

        for (int i = 0; i < tempMap.GetLength(0); i++)
        {
            for (int j = 0; j < tempMap.GetLength(1); j++)
            {
                IConfigurableAstarNode element = Instantiate(nodePrefab).GetComponent<IConfigurableAstarNode>();
                float columnOffset = i % 2 == 0 ? tileColumnOffset * 0.5f : tileColumnOffset;

                Vector3 offsetDistance = new Vector3(i * tileRowOffset, 0,j * tileColumnOffset + columnOffset);
                element.Transform.name = "(" + i + ")" + " " + "(" + j + ")"; 
                element.Transform.position = transform.position + offsetDistance;
                element.Configure(_dataTable[(Type)parsedMap[i, j]]);
            }
        }

        return tempMap;
    }
}
