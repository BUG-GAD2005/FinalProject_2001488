using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int buildingsCoinProgresValue;
    public static int buildingsGemProgresValue;
    public int buildingsCoinPrice;
    public int buildingsGemPrice;
    public static bool enoughMoney;

    public static int CoinCount { get; set; }
    public static int GemCount { get; set; }

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI GemText;
  

    void Start()
    {
        
        GemCount = 5;
        CoinCount = 20;
        
       
    }
    private void Update()
    {
        CoinText.text = "  " + CoinCount;
        GemText.text = "  " + GemCount;
        
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

        Debug.Log(CoinCount);
        Debug.Log(enoughMoney);
    }

    public static void BuildingProgressMoney(int coin, int gem)
    {
        CoinCount += coin;
        GemCount += gem;
    }


    public void CheckCoinandGem()
    {
        if (CoinCount > PlayerPrefs.GetInt("CointCount", 10)&& GemCount>PlayerPrefs.GetInt("GemCount",2))
        {
            PlayerPrefs.SetInt("CointCount", CoinCount);
            UpdateCoinandGem();
        }
    }
    public void UpdateCoinandGem()
    {
        CoinCount = PlayerPrefs.GetInt("CoinCount", 10);
        GemCount = PlayerPrefs.GetInt("GemCount", 2);

        if(CoinText !=null&&GemText!=null)
        {
            CoinText.text = $"CoinText: { CoinCount}"; 
            GemText.text = $"CoinText: { GemCount}"; 
        }

    }
}
