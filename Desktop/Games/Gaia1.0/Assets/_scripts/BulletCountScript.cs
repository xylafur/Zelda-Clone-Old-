using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletCountScript : MonoBehaviour {

    int bullets;
    Text display;
	void Start () {
        display = GetComponent<Text>();
	}
	public void setBullets(int bul){
        bullets = bul;
        updateDisplay();
    }
    void updateDisplay()
    {
        display.text = "Bullets: " + bullets.ToString();
    }
	
}
