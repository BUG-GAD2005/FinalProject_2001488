using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ProgressBar : MonoBehaviour
{
    public Image progressBar;

    float PawnTimer = 0f;
    float HouseTimer = 0f;
    float CastleTimer = 0f;
    float CarTimer = 0f;
    float ShipTimer = 0f;
    float TrainTimer = 0f;
    
    public bool Pawn;
    public bool House;
    public bool Car;
    public bool Train;
    public bool Ship;
    public bool Castle;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        PawnProgressBarUpdate();
        HouseProgressBarUpdate();
        CastleProgressBarUpdate();
        CarProgressBarUpdate();
        ShipProgressBarUpdate();
    }

    public void PawnProgressBarUpdate()
    {
        if (Pawn && BuildingCalculations.PawnCount!=0)
        {
            PawnTimer += Time.deltaTime;
            progressBar.fillAmount = PawnTimer / BuildingCalculations.pawnEarnTimer;
            if (PawnTimer >= BuildingCalculations.pawnEarnTimer)
            {
                PawnTimer = 0;
            }
        }
    }

    public void HouseProgressBarUpdate()
    {
        if (House && BuildingCalculations.houseCount != 0)
        {
            HouseTimer += Time.deltaTime;
            progressBar.fillAmount = HouseTimer / BuildingCalculations.houseEarnTimer;
            if (HouseTimer >= BuildingCalculations.houseEarnTimer)
            {
                HouseTimer = 0;
            }
        }
    }

    public void CastleProgressBarUpdate()
    {
        if (Castle && BuildingCalculations.castleCount != 0)
        {
            CastleTimer += Time.deltaTime;
            progressBar.fillAmount = CastleTimer / BuildingCalculations.castleEarnTimer;
            if (CastleTimer >= BuildingCalculations.castleEarnTimer)
            {
                CastleTimer = 0;
            }
        }
    }

    public void CarProgressBarUpdate()
    {
        if (Car && BuildingCalculations.carCount != 0)
        {
            CarTimer += Time.deltaTime;
            progressBar.fillAmount = CarTimer / BuildingCalculations.carEarnTimer;
            if (CarTimer >= BuildingCalculations.carEarnTimer)
            {
                CarTimer = 0;
            }
        }
    }

    public void ShipProgressBarUpdate()
    {
        if (Ship && BuildingCalculations.shipCount != 0)
        {
            ShipTimer += Time.deltaTime;
            progressBar.fillAmount = ShipTimer / BuildingCalculations.shipEarnTimer;
            if (ShipTimer >= BuildingCalculations.shipEarnTimer)
            {
                ShipTimer = 0;
            }
        }
    }

    public void TrainProgressBarUpdate()
    {
        if (Train && BuildingCalculations.trainCount != 0)
        {
            TrainTimer += Time.deltaTime;
            progressBar.fillAmount = TrainTimer / BuildingCalculations.trainEarnTimer;
            if (TrainTimer >= BuildingCalculations.trainEarnTimer)
            {
                TrainTimer = 0;
            }
        }
    }

}
