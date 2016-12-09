using UnityEngine;
using System.Collections;

public class kowaisyouzouga_Gimmic : MonoBehaviour {

    public GameObject player;           //  プレイヤー
    public float speed;                 //  怖い肖像画の移動速度
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vt = player.transform.position - transform.position;
        if (GetComponent<Fade_Gimmick>().fadeFlg) { transform.position += vt.normalized * speed; }
	}
}
