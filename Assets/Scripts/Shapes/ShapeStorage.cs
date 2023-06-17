using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
        public List<Shape> shapeList;
        
    
        void Start()
        {
            foreach (var shape in shapeList)
            {
                var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
                shape.CreateShape(shapeData[shapeIndex]);
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
}
