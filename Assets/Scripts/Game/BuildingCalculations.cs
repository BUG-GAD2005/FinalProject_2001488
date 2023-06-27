using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingCalculations : MonoBehaviour
{
    
    [Header("Pawn")]
    public int pawnSquareCount;
    public int pawnCoinReduction;
    public int pawnGemReduction;
    public int pawnCoinGain;
    public int pawnGemGain;
    public static float pawnEarnTimer = 2;
    public static int PawnCount;

    [Header("House")]
    public int houseSquareCount;
    public int houseCoinReduction;
    public int houseGemReduction;
    public int houseCoinGain;
    public int houseGemGain;
    public static float houseEarnTimer = 7;
    public static int houseCount;

    [Header("Castle")]
    public int castleSquareCount;
    public int castleCoinReduction;
    public int castleGemReduction;
    public int castleCoinGain;
    public int castleGemGain;
    public static float castleEarnTimer = 12;
    public static int castleCount;

    [Header("Car")]
    public int carSquareCount;
    public int carCoinReduction;
    public int carGemReduction;
    public int carCoinGain;
    public int carGemGain;
    public static float carEarnTimer = 5;
    public static int carCount;

    [Header("Ship")]
    public int shipSquareCount;
    public int shipCoinReduction;
    public int shipGemReduction;
    public int shipCoinGain;
    public int shipGemGain;
    public static float shipEarnTimer = 6;
    public static int shipCount;

    [Header("Train")]
    public int trainSquareCount;
    public int trainCoinReduction;
    public int trainGemReduction;
    public int trainCoinGain;
    public int trainGemGain;
    public static float trainEarnTimer = 8;
    public static int trainCount;

    int activeGrid;
    int currentCoin;
    int currentGem;

    GridSquare gridSquare;

    float pawnTimer = 0;
    float houseTimer = 0;
    float castleTimer = 0;
    float carTimer = 0;
    float shipTimer = 0;
    float trainTimer = 0;
   

    private void FixedUpdate()
    {
       
        if (activeGrid <= GridSquare.activeGrid)
        {
            BuildingsCounts();

            GridSquare.activeGrid = activeGrid;
        }
        Debug.Log(trainCount);

        currentCoin = GameManager.CoinCount;
        currentGem = GameManager.buildingsGemProgresValue;

        PawnBuildAndProgress(pawnCoinGain, pawnGemGain);
        HouseBuildAndProgress(houseCoinGain, houseGemGain);
        CastleBuildAndProgress(castleCoinGain, castleGemGain);
        CarBuildAndProgress(carCoinGain, carGemGain);
        ShipBuildAndProgress(shipCoinGain, shipGemGain);
        TrainBuildAndProgress(trainCoinGain, trainGemGain);
    }

    public  void BuildingsCounts()
    {
        if (GridSquare.activeGrid - activeGrid == carSquareCount)
        {
            GameManager.BuildingPrice(carCoinReduction, carGemReduction);
            carCount++;
        }
        else if (GridSquare.activeGrid - activeGrid == houseSquareCount)
        {
            GameManager.BuildingPrice(houseCoinReduction, houseGemReduction);
            houseCount++;
           
        }
        else if (GridSquare.activeGrid - activeGrid == pawnSquareCount)
        {
            GameManager.BuildingPrice(pawnCoinReduction, pawnGemReduction);
            PawnCount++;
        }
        else if (GridSquare.activeGrid - activeGrid == castleSquareCount)
        {
            GameManager.BuildingPrice(castleCoinReduction, castleGemReduction);
            castleCount++;
        }
        else if (GridSquare.activeGrid - activeGrid == shipSquareCount)
        {
            GameManager.BuildingPrice(shipCoinReduction, shipGemReduction);
            shipCount++;
        }
        else if (GridSquare.activeGrid - activeGrid == trainSquareCount)
        {
            GameManager.BuildingPrice(trainCoinReduction, trainGemReduction);
            trainCount++;
        }
    }


    void PawnBuildAndProgress(int coin, int gem)
    {
        if (PawnCount != 0)
        {
            pawnTimer += Time.deltaTime;
            if (pawnTimer >= pawnEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * PawnCount, gem * PawnCount);
                pawnTimer = 0;

                
            }
        }
    }

    void HouseBuildAndProgress(int coin, int gem)
    {
        if (houseCount != 0)
        {
            houseTimer += Time.deltaTime;
            if (houseTimer >= houseEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * houseCount, gem * houseCount);
                houseTimer = 0;

                
            }
        }
    }

    void CastleBuildAndProgress(int coin, int gem)
    {
        if (castleCount != 0)
        {
            castleTimer += Time.deltaTime;
            if (castleTimer >= castleEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * castleCount, gem * castleCount);
                castleTimer = 0;

             
            }
        }
    }

    void CarBuildAndProgress(int coin, int gem)
    {
        if (carCount != 0)
        {
            carTimer += Time.deltaTime;
            if (carTimer >= carEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * carCount, gem * carCount);
                carTimer = 0;

                
            }
        }
    }

    void ShipBuildAndProgress(int coin, int gem)
    {
        if (shipCount != 0)
        {
            shipTimer += Time.deltaTime;
            if (shipTimer >= shipEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * shipCount, gem * shipCount);
                shipTimer = 0;

                
            }
        }
    }

    void TrainBuildAndProgress(int coin, int gem)
    {
        if (trainCount != 0)
        {
            trainTimer += Time.deltaTime;
            if (trainTimer >= trainEarnTimer)
            {
                GameManager.BuildingProgressMoney(coin * trainCount, gem * trainCount);
                trainTimer = 0;

                
            }
        }
    }

}
