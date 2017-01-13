using UnityEngine;
using System.Collections;

public class Thard_Floor_Ivent : MonoBehaviour {

    private char iventFlg;              //  イベントフラグ

    public float attackPower = 0.0f;    //  ものが倒れる勢い

    public int counter;                 //  カウンター


    private enum Flg
    {
        START = 0x01,
        COUNTOR_FLG = 0x01 << 1,
    }

	// Use this for initialization
	void Start () {
        iventFlg = (char)0x00;
	}
	
	// Update is called once per frame
	void Update () {

        if ((iventFlg & (char)Flg.COUNTOR_FLG) == (int)Flg.COUNTOR_FLG){ counter--; }
        //  イベントフラグがtrueなら
	    if((iventFlg & (char)Flg.START) == 1 && counter >= 0)
        {
            transform.position += transform.forward.normalized * attackPower;
        }
	}

    
    void OnTriggerEnter(Collider collider)
    {
        //  プレイヤーに当たったら吸い込み開始
        if (collider.gameObject.tag == "Player")
        {
            iventFlg = (char)Flg.START;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Obestacle")
        {
            iventFlg |= (char)Flg.COUNTOR_FLG;
        }
    }
}
