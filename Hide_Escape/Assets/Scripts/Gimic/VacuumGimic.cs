using UnityEngine;
using System.Collections;

public class VacuumGimic : MonoBehaviour {

    public GameObject player;               //  プレイヤーのゲームオブジェクト

    public float vacuumSpeed = 1.0f;        //  プレイヤーを吸い込むスピード

    private bool vacuumflg;                 //  プレイヤー吸い込みフラグ

    private float holeScale = 0.01f;   //  ワープホールの大きさを変えるスピード

	// Use this for initialization
	void Start () {
        vacuumflg = false;
	}
	
	// Update is called once per frame
	void Update () {

        //  プレイヤーからのベクトル
        Vector3 vt = transform.position - player.transform.position;

        //  吸い込みフラグが立っていたら
        if (vacuumflg)
        {
            ScaleChange();
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 10.5f);
            VacuumPlayer(vt);
        }
	}

    //  ワープホールの大きさチェンジ
    private void ScaleChange()
    {
        if (holeScale <= 0.62f) { holeScale += 0.01f; }
        transform.localScale = new Vector3(holeScale, holeScale, holeScale);
    }

    //  プレイヤーを吸い込む
    private void VacuumPlayer(Vector3 playerVec)
    {
        if (GetComponent<SpriteRenderer>().color.a >= 0.9f)
        {
            //  吸い込み速度分プレイヤーを吸い込む
            player.transform.position += playerVec.normalized * vacuumSpeed;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //  プレイヤーに当たったら吸い込み開始
        if(collider.gameObject.tag == "Player")
        {
            vacuumflg = true;
        }
    }
}
