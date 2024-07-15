using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text moneyText;
    [SerializeField] Text lvl;
    
    private int currentLvl;


    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + player.money.ToString();
        lvl.text = "Level: " + currentLvl.ToString();
    }

    public void LvlUp()
    {
        if (player.money >= player.lvlCost) 
        {
            currentLvl++;
            player.LevelUp(currentLvl);
        }
        
    }
}
