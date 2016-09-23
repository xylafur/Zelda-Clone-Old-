using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    public GameObject body;
    public BoxCollider2D bc;
	void Start () {
        bc = GetComponent<BoxCollider2D>();
	}
    public void switchCollider(string dir)
    {
        switch (dir)
        {
            case "forward":
                bc.offset = new Vector3(0.0007196154f, 0.03765946f);
                bc.size = new Vector3(0.03664468f, 0.07818711f);
                break;
            case "back":
                bc.offset = new Vector3(-0.04907554f, -0.04907554f);
                bc.size = new Vector3(0.04603749f, 0.07708373f);
                break;
            case "right":
                bc.offset = new Vector3(0.05189698f, -0.01166973f);
                bc.size = new Vector3(0.01785869f, 0.09187736f);
                break;
            case "left":
                bc.offset = new Vector3(-0.05243448f, -0.01688614f);
                bc.size = new Vector3(0.01508357f, 0.09352645f);
                break;
        }
    }
	// Update is called once per frame
	void Update () {
        transform.position = body.transform.position;
        transform.rotation = Quaternion.identity;
	}
   
}
