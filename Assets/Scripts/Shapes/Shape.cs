using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject squareShapeImage;
    //[HideInInspector]
    public ShapeData CurrentShapeData;

    private List<GameObject> CurrentShape = new List<GameObject>();



    void Start()
    {

        RequestNewShape(CurrentShapeData);




    }
    public void RequestNewShape(ShapeData shapeData)
    {
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;

        var totalSquareNumber = GetNumberOfSqaures(shapeData);

        while (CurrentShape.Count <= totalSquareNumber)
        {
            CurrentShape.Add(Instantiate(squareShapeImage, transform)as GameObject);
        }

        foreach(var square in CurrentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
            
        }

        var squareReact = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareReact.rect.width * squareReact.localScale.x, squareReact.rect.height * squareReact.localScale.y);

        int currentIndexInList = 0;

        for (var row =0; row < shapeData.rows; row++)
        {
            for (var column=0; column< shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    CurrentShape[currentIndexInList].SetActive(true);
                    
                   
                    CurrentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = 
                        new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), 
                        GetYPostionForShapeSqaure(shapeData, row, moveDistance));

                    currentIndexInList++;

                }
            }
        }
    }

    private float GetYPostionForShapeSqaure(ShapeData shapeData, int rows, Vector2 moveDistance)
    {
        float shiftonY = 0f;

        if(shapeData.rows > 1)
        {

            if(shapeData.rows %2 != 0)
            {

                var _middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2;
                if (rows < _middleSquareIndex)
                {
                    shiftonY = moveDistance.y * 1;
                    shiftonY *= multiplier;
                }
                else if (rows > _middleSquareIndex)
                {
                    shiftonY = moveDistance.y * -1;
                    shiftonY *= multiplier;
                }

                
            }
            else
            {
                var _middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var _middleSqaureIndex1 = (shapeData.rows == 2) ? 0 : shapeData.rows - 2;
                var multiplier = shapeData.rows / 2;
                
                if( rows==_middleSqaureIndex1 || rows == _middleSquareIndex2)
                {

                    if (rows == _middleSquareIndex2)
                    {
                        shiftonY = (moveDistance.y / 2) * -1;
                    }

                    if(rows == _middleSqaureIndex1)
                    {
                        shiftonY = (moveDistance.y / 2);
                    }
                }

                if(rows < _middleSqaureIndex1 && rows < _middleSquareIndex2)
                {
                    shiftonY = moveDistance.y * 1;
                    shiftonY *= multiplier;
                }
                else if(rows> _middleSqaureIndex1 && rows> _middleSquareIndex2)
                {
                    shiftonY = moveDistance.y * -1;
                    shiftonY = multiplier;
                }
            }
        }
        return shiftonY;

        
    }


    private float GetXPositionForShapeSquare(ShapeData shapedata,int column, Vector2 moveDistance)
    {
        float shiftonx = 0f;
        if (shapedata.columns > 1)
        {
            if(shapedata.columns % 2 != 0)
            {
                var middleSqaureIndex = (shapedata.columns - 1) / 2;
                var multiplier = (shapedata.columns - 1) / 2;
                if(column < middleSqaureIndex)
                {
                    shiftonx = moveDistance.x * -1;
                    shiftonx *= multiplier;
                }
                else if(column > middleSqaureIndex)
                {
                    shiftonx = moveDistance.x * 1;
                    shiftonx *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapedata.columns == 2) ? 1 : (shapedata.columns / 2);
                var middleSqaureIndex1 = (shapedata.columns == 2) ? 0 : shapedata.columns - 1;
                var multiplier = shapedata.columns / 2;

                if(column==middleSqaureIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                        shiftonx = moveDistance.x / 2;
                    if (column == middleSqaureIndex1)
                        shiftonx = (moveDistance.x / 2) * -1;
                }
            
                if(column < middleSqaureIndex1&& column< middleSquareIndex2)
                {

                    shiftonx = moveDistance.x * -1;
                    shiftonx *= multiplier;
                }
                else if(column > middleSqaureIndex1&& column > middleSquareIndex2)
                {
                    shiftonx = moveDistance.x * 1;
                    shiftonx *= multiplier;
                }
            }
        }
        return shiftonx;

    }
    private int GetNumberOfSqaures(ShapeData shapeData)
    {

        int number = 0;
         
        foreach(var rowData in shapeData.board)
        {
            foreach(var active in rowData.column)
            {
                if(active)
                {
                    number++;
                }
            }
        }
        return number;
    }
}
