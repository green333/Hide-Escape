using UnityEngine;
using System.Collections;

public class Fade_Gimmick : MonoBehaviour {

    private float alpha = 0.0f;

    public bool fadeFlg = false;        //  フェードフラグ

    //  フェードステート
    public enum FadeState
    {
        FADE_IN = 0,
        FADE_OUT,
        STATE_MAX,
    }

    public FadeState state = FadeState.FADE_OUT;

	// Use this for initialization
	void Start () {
        //  設定されたステートによってアルファの初期値を決定する
	    switch(state)
        {
            case FadeState.FADE_IN:
                alpha = 0.0f;
                break;
            case FadeState.FADE_OUT:
                alpha = 1.0f;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (IsFade(FadeState.FADE_OUT)){ FadeOut(); }
        else if (IsFade(FadeState.FADE_IN)) { FadeIn(); }

        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
	}

    private bool IsFade(FadeState state)
    {
        //  フェードフラグが立っていて指定したステートならばtrueを返す
        if (fadeFlg && this.state == state) { return true; }

        return false;
    }

    private void FadeOut()
    {
        alpha -= 0.01f;
    }

    private void FadeIn()
    {
        alpha += 0.01f;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fadeFlg = true;
            AudioManager.Instance.PlaySE("Surprised");
        }
    }
}
