using UnityEngine;
using System.Collections;

public class IventPlayerAnime : MonoBehaviour {

    //  プレイヤーキャラのアニメーション
    private Animator anime = null;

	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
        //  最初は停止している
        anime.SetBool("walkFlg", false);
	}
	
	// Update is called once per frame
	void Update () {
        
        //  プレイヤーが止まっているとき
        if(GetComponentInParent<IventPlayer>().GetComponent<IventPlayer>().WalkFlg)
        {
            anime.SetBool("walkFlg", false);
        }
        else
        {
            anime.SetBool("walkFlg", true);
        }
	}

    void FixedUpdate()
    {

    }
}
