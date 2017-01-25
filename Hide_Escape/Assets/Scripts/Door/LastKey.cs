using UnityEngine;
using System.Collections;

public class LastKey : MonoBehaviour {

    static bool keyGetFlg = true;

    public bool KeyGetFlg
    {
        get { return keyGetFlg; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.SetActive(keyGetFlg);
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            keyGetFlg = false;
        }
    }
}
