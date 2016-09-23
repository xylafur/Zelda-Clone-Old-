using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MerchantMoney : MonoBehaviour {
    public GameObject playerObj;

    private Text text;
    private Player player;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        player = playerObj.GetComponent<Player>();
	}
	void Update () {
        string money = player.getAmmount("Money").ToString();
        text.text = "Current Money: " + money;
	}
}
