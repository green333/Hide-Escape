using UnityEngine;
using System.Collections;


public class Plate_Gimmick : Base_Gimmick{



    [SerializeField]
    private float range;              //揺れ幅

    [SerializeField]
    private bool shakeflg;            //揺れフラグ

    private float save_duration;      //揺れ幅保存

    public bool Shakeflg
    {
        get { return shakeflg; }
        set { shakeflg = value; }
    }
    //---------------------------------

    //  初期化

    //---------------------------------
	void Start () {
        save_duration = duration;    
        
	}

    //---------------------------------

    //  更新

    //---------------------------------
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    shakeflg = true;

        //}

        if(GetComponentInParent<Near_Plate_Gimmic>().GetComponentInParent<Near_Plate_Gimmic>().ShakePlateflg)
        {
            shakeflg = true;
        }

        if(shakeflg)
        {
            duration = save_duration;
            shakeflg = false;
        }

        //もし時間が0なら停止
        if (duration > 0 /*shakeflg*/)
        {
            Shake();
        }
	}



    //---------------------------------

    //  オブジェクトを揺らす

    //---------------------------------
    private void Shake()
    {
        //振動時間 -= 振動速度;
        duration -= (speed);

        ////もし時間が0なら停止
        //if (duration < 0)
        //{
        //    shakeflg = false;
        //}
        
        //揺れ
        pos = new Vector3(Random.Range(-duration, duration), 0, Random.Range(-duration, duration));
        
        transform.eulerAngles = pos;
    }
}

