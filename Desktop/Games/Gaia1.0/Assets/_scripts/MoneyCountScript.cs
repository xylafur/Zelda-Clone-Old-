using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyCountScript : MonoBehaviour {

    int money;
    Text display;
    void Start()
    {
        display = GetComponent<Text>();
    }
    public void setMoney(int mon)
    {
        money = mon;
        updateDisplay();
    }
    void updateDisplay()
    {
        display.text = "Money: " + money.ToString();
    }
}
