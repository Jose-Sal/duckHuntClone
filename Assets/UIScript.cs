using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    //UI health
    public TextMeshProUGUI healthTxt;
    
    

    //UI Money add/subtract
    public TextMeshProUGUI moneyText;
    private void Start()
    {
        Manager.health = Manager.maxHealth;
    }

    public void takeDamage()
    {
        Manager.health -= 10;
        //update healthtext with current health
        healthTxt.text = Manager.health.ToString();
    }

    public void AddMoney()
    {
        Manager.currency += 10;
        moneyText.text = $"$ {Manager.currency.ToString()}";
    }
    public void RemoveMoney()
    {
        Manager.currency -= 10;
        moneyText.text = $"${Manager.currency.ToString()}";
    }

    public void updateUI()
    {
        moneyText.text = $"$ {Manager.currency.ToString()}";
        healthTxt.text = Manager.health.ToString();
    }

}
