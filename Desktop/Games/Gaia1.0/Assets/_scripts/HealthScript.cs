using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
    private RectTransform rt;
    private int maxHealth, currentHealth;
	// Use this for initialization
	void Start () {
	    rt = gameObject.GetComponent<RectTransform>();
	}
    public void healthUpdate(int health)
    {
        currentHealth = health;
        barUpdate();
    }
    public void maxHealthUpdate(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        barUpdate();
    }
    void barUpdate()
    {
        float temp = (float)currentHealth / maxHealth;
        transform.localScale = new Vector3(temp, 1f, 0f);
    }
    public void setHealth(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
