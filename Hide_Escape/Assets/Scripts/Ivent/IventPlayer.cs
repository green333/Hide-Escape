using UnityEngine;
using System.Collections;

public class IventPlayer : MonoBehaviour {

    public Vector3[] iventPosition;     //  イベントが起こる位置
    public GameObject[] obj;            //  イベントを配置する地点（空のオブジェクトを配置)
    public int[] stopTime;              //  イベントを配置した地点で停止する時間の指定(フレーム単位)
    private int[] stopTimer = null;     //  イベントを配置した地点で停止する時間カウント用変数
    public float speed;                 //  イベントが起こる位置まで移動するスピード

    private int targetPositionNo = 0;   //  ターゲットイベント番号
    private int size = 0;               //  配置位置の数
    public int targetNo = 0;           //  ターゲット番号

    public bool walkFlg;               //  歩きフラグ

    //****************************************************************************************************************
    //
    //  アクセサ
    //
    //****************************************************************************************************************
    public int Size
    {
        get { return size; }
    }
    //  ターゲット番号のゲッター
    public int TargetNo
    {
        get { return targetNo; }
    }
    //  ターゲットポジションの番号
    public int TargetPositionNo
    {
        get { return targetPositionNo; }
    }
    //  歩きフラグ
    public bool WalkFlg
    {
        get { return walkFlg; }
    }

    //****************************************************************************************************************
    //
	// Use this for initialization
    //
    //****************************************************************************************************************
	void Start () 
    {
        size = obj.Length;
        walkFlg = false;
        stopTimer = new int[size];

        //  イベントが起こる位置に配置した地点を配置
        for(int i = 0; i < size; ++i)
        {
            iventPosition[i] = obj[i].transform.position;
            
            stopTimer[i] = 0;
        }

        //  空のオブジェクトの0番目の位置を初期位置に設定する
        transform.position = iventPosition[0];
	}
    //****************************************************************************************************************
    //
	// Update is called once per frame
    //
    //****************************************************************************************************************
	void Update () 
    {
        walkFlg = IsPlayerStop();

        //  プレイヤーが止まる場所ならば止まる時間をカウントする
        if (walkFlg)
        {
            TimerChack();
        }
        else
        {
            Vector3 vt = iventPosition[targetNo] - transform.position;
            //  プレイヤーが止まる場所でなければプレイヤーの移動
            transform.position += vt.normalized * speed;
        }
	}

    //****************************************************************************************************************
    //
    //  プレイヤーが止まる場所化を調べる
    //
    //****************************************************************************************************************
    public bool IsPlayerStop()
    {
        //  プレイヤーがターゲットの5m範囲内に入っていて次のターゲットに行くフラグが立っていたら
        if(Vector3.Distance(transform.position, iventPosition[targetNo]) < 2.0f)
        {
            //  ターゲットの位置番号
            targetPositionNo = targetNo;
            return true;
        }

        return false;
    }

    //****************************************************************************************************************
    //
    //  タイマーのカウント
    //
    //****************************************************************************************************************
    bool IsPlayerStopTimer()
    {
        //  ターゲット番号のタイマーがターゲット番号の指定停止時間以上になったらtrueを返す
        if(stopTimer[targetPositionNo] >= stopTime[targetPositionNo])
        {
            return true;
        }
        else
        {
            stopTimer[targetPositionNo]++;
        }

        return false;
    }

    //****************************************************************************************************************
    //
    //  タイマーのチェック
    //
    //****************************************************************************************************************
    void TimerChack()
    {
        //  タイマーのカウントが終了したら次のターゲットに移るフラグをtrueにする
        if(IsPlayerStopTimer())
        {
            TargetNoNext();
        }
    }

    //****************************************************************************************************************
    //
    //  ターゲット番号を次に行くかチェックしてから加算
    //
    //****************************************************************************************************************
    void TargetNoNext()
    {
        //  ターゲット番号がサイズ以下なら
        if(targetNo < size - 1)
        {
            targetNo++;
        }
    }
}
