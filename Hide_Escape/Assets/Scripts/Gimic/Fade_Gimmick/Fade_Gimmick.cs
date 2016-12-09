using UnityEngine;
using System.Collections;

public class Fade_Gimmick : MonoBehaviour {

    private float alpha = 1.0f;

    public bool fadeFlg = false;        //  フェードフラグ

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fadeFlg) { FadeOut(); }
        
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fadeFlg = true;
        }
    }

    void FadeOut()
    {
        alpha -= 0.01f;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
