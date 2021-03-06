﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will parse CSV files containing int values in string format. Can be updated to handle strings too.
public static class CSVParser
{
   private static char _fieldSeparator = ',';
   private static char _lineSeperator = '\n';

    public static int[,] Parse(string path, int rows, int columns)
   {
        TextAsset data = Resources.Load<TextAsset>(path);
        int[,]parsedData = new int[rows,columns];

        string[] rowData = data.text.Split(_lineSeperator); //splits lines into rows

        for (int i = 0; i < rowData.Length; i++)
        {
            string[] columnElements = rowData[i].Split(_fieldSeparator); //splits rows into individual elements

            for (int j = 0; j < columnElements.Length; j++)
            {
                //casting strings to ints.
                int parsedItem;
                bool didParse = int.TryParse(columnElements[j], out parsedItem);
                if(didParse)
                {
                    //creating the parsed grid
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
