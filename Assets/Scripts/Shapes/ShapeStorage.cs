using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
        public List<Shape> shapeList;
    


    private void OnEnable()
    {
        TheGameEvents.RequestNewShapes += RequestNewShapes;
    }
    private void OnDisable()
    {
        TheGameEvents.RequestNewShapes -= RequestNewShapes;
    }

   
    void Start()
    {
        for (int i = 0; i < shapeList.Count; i++)
        {
            shapeList[i].CreateShape(shapeData[i]);      
        }    
    }

   

    public Shape GetCurrentSelectedShape()
        {
            foreach (var shape in shapeList)
            {
            
            if (shape.IsonStartPositon() == false && shape.IsAnyOffShapeSquareActive())
                    return shape;
                
                    
                
            }
            Debug.Log("SEÇMEDİN SEÇMEDİN");
            return null;
        }


    private void RequestNewShapes()
    {
        //for (int i = 0; i < shapeList.Count; i++)
        //{
        //    shapeList[i].CreateShape(shapeData[i], gameObject);

        //}

        int i = 0;

        foreach (var shape in shapeList)
        {
            //var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[i], gameObject);
            i++;
        }
    }
}
