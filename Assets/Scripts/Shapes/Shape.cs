using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Shape : MonoBehaviour, IPointerClickHandler,IPointerUpHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerDownHandler
{
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;
    public Vector2 offSet = new Vector2(0f, 700f);
    
    [HideInInspector]
    
    public ShapeData CurrentShapeData;
    public int TotalSquareNumber { get; set; }

    private List<GameObject> CurrentShape = new List<GameObject>();
    private Vector3 shapeStartScale;
    private RectTransform _transform;
    private bool _shapeDraggable = true;
    private GameObject _canvas;
    private Vector3 _startPosition;
    private bool shapeActive = true;
    
    public void Awake()
    {
        shapeStartScale= this.GetComponent<RectTransform>().localScale;
        _transform = this.GetComponent<RectTransform>();
        _canvas = GameObject.Find("Menu Screen");
        _shapeDraggable = true;
        _startPosition = _transform.localPosition;
        shapeActive = true;
    }

    void Start()
    {
        // RequestNewShape(CurrentShapeData);
    }

    private void OnEnable()
    {
        TheGameEvents.MoveShapeToStartPosition += MoveShapeToStartPositon;
        TheGameEvents.SetShapeInActive += SetShapeInActive;
    }

    private void OnDisable()
    {
        TheGameEvents.MoveShapeToStartPosition -= MoveShapeToStartPositon;
        TheGameEvents.SetShapeInActive -= SetShapeInActive;
    }


    public bool IsonStartPositon()
    {
        return _transform.localPosition == _startPosition;
    }

    public bool IsAnyOffShapeSquareActive()
    {
        foreach (var square in CurrentShape)
        {
            if (square.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    public void DeActivateShape()
    {
        if (shapeActive)
        {
            foreach (var square in CurrentShape)
            {
                square?.GetComponent<ShapeSquare>().DeActivateShape();
            }
        }

        shapeActive = false;
    }

   
    private void SetShapeInActive()
    {
        if(IsonStartPositon() == false&& IsAnyOffShapeSquareActive())
        {
            foreach (var square in CurrentShape)
            {
                square.gameObject.SetActive(false);
                Debug.Log("DSKFJAS");
                
            }
        }
    }
    public void ActivateShape()
    {
        if (!shapeActive)
        {
            foreach (var square in CurrentShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
        }

        shapeActive = true;
    }
    public void RequestNewShape(ShapeData shapeData, GameObject parentObj)
    {
        _transform.localPosition = _startPosition;
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;

        TotalSquareNumber = GetNumberOfSqaures(shapeData);

        while (CurrentShape.Count <= TotalSquareNumber)
        {
            CurrentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
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


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    
    public  void OnBeginDrag(PointerEventData eventData)
    {
        
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchorMin = new Vector2(0, 0);
        _transform.anchorMax = new Vector2(0, 0);
        _transform.pivot = new Vector2(0, 0);

        Vector2 pos;
       // RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position,
           // Camera.main, out pos);
           Vector3 _pos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
           _pos.z = 0f;
           gameObject.transform.position = _pos;

           //_transform.localPosition = pos + offSet;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = shapeStartScale;
        TheGameEvents.CheckIfShapeCanBePlaced();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void MoveShapeToStartPositon()
    {
        _transform.transform.localPosition = _startPosition;
    }
}
