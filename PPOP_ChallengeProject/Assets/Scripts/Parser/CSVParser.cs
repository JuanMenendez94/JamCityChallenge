using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will parse CSV containing int values only. Can be updated to manage strings.
public static class CSVParser
{
   private static char _fieldSeparator = ',';
   private static char _lineSeperator = '\n';

    public static int[,] Parse(string path, int rows, int columns)
   {
        Debug.Log(path);
        TextAsset data = Resources.Load<TextAsset>(path);
        int[,]parsedData = new int[rows,columns];

        string[] rowData = data.text.Split(_lineSeperator);

        for (int i = 0; i < rowData.Length; i++)
        {
            string[] columnElements = rowData[i].Split(_fieldSeparator);
            for (int j = 0; j < columnElements.Length; j++)
            {
                int parsedItem;
                bool didParse = int.TryParse(columnElements[j], out parsedItem);
                if(didParse)
                {
                    parsedData[i, j] = parsedItem;
                }
                else
                {
                    throw new System.Exception("Couldn't parse csv element : " + i + " " + j);
                }
                
            }
        }
        return parsedData;
   }
}
