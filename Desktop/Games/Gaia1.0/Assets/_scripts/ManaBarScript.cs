using UnityEngine;
using System.Collections;

public class ManaBarScript : MonoBehaviour {

    private int maxMana, currentMana;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setMana(int mana)
    {
        currentMana = mana;
        changeDisplay();
    }
    public void setMaxMana(int mana)
    {
        maxMana = mana;
        currentMana = maxMana;
        changeDisplay();
    }
    void changeDisplay()
    {
        float temp = (float)currentMana / maxMana;
        transform.localScale = new Vector3(temp, 1f, 0f);
    }
}
