using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int buildingsCoinProgresValue;
    public static int buildingsGemProgresValue;
    public int buildingsCoinPrice;
    public int buildingsGemPrice;
    public static bool enoughMoney;
    public float GemTimer=0;
    public float CoinTimer=0;


    public static int CoinCount { get; set; }
    public static int GemCount { get; set; }

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI GemText;
  

    void Start()
    {
        
        CoinCount = PlayerPrefs.GetInt("CointCount", CoinCount);
        
        GemCount = PlayerPrefs.GetInt("GemCount",GemCount);
        
        
       
    }
    private void Update()
    {
        CoinText.text = "  " + CoinCount;
        GemText.text = "  " + GemCount;
        PlayerPrefs.SetInt("CointCount", CoinCount);
        PlayerPrefs.SetInt("GemCount",GemCount);

        GemTimer+=Time.deltaTime;
        CoinTimer+=Time.deltaTime;
            
        

        if(GemTimer>=15 )
        {
            GemCount++;
            GemTimer=0;
        }

        if(CoinTimer>=5)
        {
            CoinCount++;
            CoinTimer=0;
            
        }
        
    }


    public static void BuildingPrice(int coin, int gem)
    {
        if (CoinCount >= coin && GemCount >= gem)
        {
            enoughMoney = true;

            if (enoughMoney)
            {
                CoinCount -= coin;
                GemCount -= gem;
            }
        }
        else
        {
            enoughMoney = false;
        }

        //Debug.Log(CoinCount);
        //Debug.Log(enoughMoney);
    }

    public static void BuildingProgressMoney(int coin, int gem)
    {
        CoinCount += coin;
        GemCount += gem;
    }


    



    public void RestartLevel()
    {
       
       CoinCount=0;
       GemCount=0;
       BuildingCalculations.PawnCount=0;
       BuildingCalculations.carCount=0;
       BuildingCalculations.houseCount=0;
       BuildingCalculations.castleCount=0;
       BuildingCalculations.shipCount=0;
       BuildingCalculations.trainCount=0;
        SceneManager.LoadScene("Restart");
    }
    public void RestartLevel2()
    {
       
       CoinCount=0;
       GemCount=0;
       BuildingCalculations.PawnCount=0;
       BuildingCalculations.carCount=0;
       BuildingCalculations.houseCount=0;
       BuildingCalculations.castleCount=0;
       BuildingCalculations.shipCount=0;
       BuildingCalculations.trainCount=0;
        SceneManager.LoadScene("SampleScene");
    }
}
