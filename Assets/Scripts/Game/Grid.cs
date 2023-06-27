using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Grid : MonoBehaviour
{
    public ShapeStorage shapeStorage;
    public int colums = 0;
    public int rows = 0;
    public float _squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;


    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> gridSquares = new List<GameObject>();
    
    void Start()
    {
        CreateGrid();
    }

    public void OnEnable()
    {
        TheGameEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;

    }

    public void OnDisable()
    {
        TheGameEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquarePositions();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;

        for(var row=0; row< rows; ++row)
        {
            for(var column=0; column<colums; ++column)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].GetComponent<GridSquare>().SqaureIndex = square_index;
                gridSquares[gridSquares.Count - 1].transform.SetParent(this.transform);
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                gridSquares[gridSquares.Count - 1].GetComponent<GridSquare>().SetImage(square_index % 2 == 0);
                square_index++;
            }
        }
    }

    private void SetGridSquarePositions()
    {
        int collumn_number = 0;
        int row_number = 0;

        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        var square_react = gridSquares[0].GetComponent<RectTransform>();

        _offset.x = square_react.rect.width * square_react.transform.localScale.x + everySquareOffset;
        _offset.y = square_react.rect.height * square_react.transform.localScale.y + everySquareOffset;
        foreach(GameObject square in gridSquares)
        {
            if (collumn_number + 1 > colums)
            {
                square_gap_number.x = 0;
                collumn_number = 0;
                row_number++;
                row_moved = false;

            }

            var pos_x_offset = _offset.x * collumn_number + (square_gap_number.x * _squaresGap);
            var pos_y_offset = _offset.y * row_number + (square_gap_number.y * _squaresGap);

            if(collumn_number > 0 && collumn_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += _squaresGap;


            }
            
            if (row_number > 0 && row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += _squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset, 0.0f);

            collumn_number++;
        }

    }


    private void CheckIfShapeCanBePlaced()
    {
        var squareIndexes = new List<int>();
        foreach (var square in gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if (gridSquare.Selected && !gridSquare.SquareOccupied )
            {
                squareIndexes.Add(gridSquare.SqaureIndex);
                gridSquare.Selected = false;
                //gridSquare.ActiavateSquare();

            }
        }
        
        var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
        
        //if (currentSelectedShape = null) return;

        if (currentSelectedShape.TotalSquareNumber == squareIndexes.Count)
        {
            foreach (var squareIndex in squareIndexes)
            {
                gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();   
            }


            var shapeLeft = 6;

            foreach (var shape in shapeStorage.shapeList)
            {
                if (shape.IsonStartPositon() && shape.IsAnyOffShapeSquareActive())
                {
                    shapeLeft--;
                    
                }
            }
            

            if (shapeLeft != 6)
            {
                TheGameEvents.RequestNewShapes();
                shapeLeft = 0;
                //GameObject.FindGameObjectWithTag("Buildings").SetActive(false);
            }
            else
            {
                TheGameEvents.SetShapeInActive();
            }
        }
        else
        {
            TheGameEvents.MoveShapeToStartPosition();
            


        }
    }   
    
}
