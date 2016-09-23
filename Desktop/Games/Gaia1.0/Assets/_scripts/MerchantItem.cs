using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MerchantItem : MonoBehaviour {
    public int price;
    public KeyCode key;
    public string item;
    public GameObject playerObj;
    public GameObject merchObj;

    private Player player;
    private Merchant merch;
    private AudioSource audio;
    private Text text;
    private int quantity, max;

	
	void Start () {
        text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        player = playerObj.GetComponent<Player>();
        merch = merchObj.GetComponent<Merchant>();
	}
	
	// Update is called once per frame
	void Update () {
        quantity = player.getAmmount((string)item);
        max = player.getMax(item);
        string display = "Price: " + price.ToString() + "  You Have: " + quantity.ToString() + "/" + max.ToString() + "  Press: " + key.ToString();
        text.text = display;

        if (merch.getTrig() && Input.GetKeyDown(key) )
        {
            if (player.transaction(price))
            {
                audio.Play();
                player.getItem(item);
            }
        }

	}
}
