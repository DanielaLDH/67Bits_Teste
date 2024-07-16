using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Tooltip("Referência ao script Player para obter informações do jogador.")]
    [SerializeField] Player player;

    [Tooltip("Texto que exibe a quantidade de dinheiro.")]
    [SerializeField] Text moneyText;

    [Tooltip("Texto que exibe o nível do jogador.")]
    [SerializeField] Text lvl;
    
    private int currentLvl;


    // Update is called once per frame
    void Update()
    {
        // Atualiza os textos de dinheiro e nível na interface
        moneyText.text = "Money: " + player.money.ToString();
        lvl.text = "Level: " + currentLvl.ToString();
    }

    // LvlUp permite ao jogador subir de nível se tiver dinheiro suficiente
    public void LvlUp()
    {
        if (player.money >= player.lvlCost) 
        {
            currentLvl++;
            player.LevelUp(currentLvl);
        }
        
    }
}
