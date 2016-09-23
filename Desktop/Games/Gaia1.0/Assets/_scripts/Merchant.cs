using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Merchant : MonoBehaviour {
    public GameObject player;

    bool trig = false;
    void Start()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            rend.enabled = false;
        }
        foreach (Text rend in GetComponentsInChildren<Text>())
        {
            rend.enabled = false;
        }
       // GetComponent<Renderer>().enabled = false;
	}
    public bool getTrig()
    {
        return trig;
    }
	// Update is called once per frame
	void Update () {
        if (trig)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                {
                    rend.enabled = false;
                }
                foreach (Text rend in GetComponentsInChildren<Text>())
                {
                    rend.enabled = false;
                }
                player.GetComponent<Player>().modeSwitch(false);

                trig = false;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        trig = true;
        player.GetComponent<Player>().modeSwitch(true);
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            rend.enabled = true;
        }
        foreach (Text rend in GetComponentsInChildren<Text>())
        {
            rend.enabled = true;
        }
    }
}
