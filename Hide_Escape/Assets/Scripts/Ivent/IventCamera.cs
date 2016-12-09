using UnityEngine;
using System.Collections;

public class IventCamera : MonoBehaviour {

    private Vector3 pos;        //  位置
    public GameObject player;   //  プレイヤーのゲームオブジェクト
    public float zoomSpeed;     //  ズームスピード
    private float zoom = 0.0f;  //  ズーム
    public float panSpeed;      //  パンするスピード
    private Vector3 pan;        //  パン
    public GameObject[] target; //  その他のターゲット
    
    enum EVENT_STATE
    {
        MOVE = 0,     //  プレイヤーに追従
        ZOOM_OUT,     //  ズームアウト
        STOP,         //  ストップ
        FPS,          //  FPS視点
    }

    EVENT_STATE state;     //  イベントのステート

    //****************************************************************************************************************
    //
	// Use this for initialization
    //
    //****************************************************************************************************************
	void Start () {
        state = EVENT_STATE.MOVE;
        transform.forward = player.transform.forward;
        pan = new Vector3(0.0f, 0.0f, 0.0f);
	}

    //****************************************************************************************************************
    //
	// Update is called once per frame
    //
    //****************************************************************************************************************
	void Update () {

        //  プレイヤーのターゲット番号をチェック
        PlayerTargetNoChack();
        
        //  カメラモード切替
        CameraMode();
	}

    //****************************************************************************************************************
    //
    //  カメラモード
    //
    //****************************************************************************************************************
    void CameraMode()
    {
        switch (state)
        {
            case EVENT_STATE.MOVE:
                //  カメラ追従
                CameraCompliance();
                break;
            case EVENT_STATE.ZOOM_OUT:
                //  カメラのズームアウトとパン
                CameraZoomOutAndPan();
                break;
            case EVENT_STATE.STOP:
                //  カメラを固定
                transform.position = pos;
                break;
            case EVENT_STATE.FPS:
                //  一人称視点
                CameraFPSView();
                break;
        }
    }

    //****************************************************************************************************************
    //
    //  カメラ追従
    //
    //****************************************************************************************************************
    void CameraCompliance()
    {
        transform.position = player.transform.position + new Vector3(0.0f, 1.0f, 5.0f);
        transform.LookAt(player.transform);
    }

    //****************************************************************************************************************
    //
    //  カメラズームアウトとパン
    //
    //****************************************************************************************************************
    void CameraZoomOutAndPan()
    {
        zoom += zoomSpeed;
        pan.y += panSpeed;
        //  ズームとパンを一緒にする
        transform.position = pos + pan + (-transform.forward.normalized * zoom);
    }

    //****************************************************************************************************************
    //
    //  一人称視点
    //
    //****************************************************************************************************************
    void CameraFPSView()
    {
        transform.position = player.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        transform.LookAt(target[0].transform);
    }

    //****************************************************************************************************************
    //
    //  プレイヤーのターゲット番号チェック
    //
    //****************************************************************************************************************
    void PlayerTargetNoChack()
    {
        //  プレイヤーのターゲット番号を取得
        switch (player.GetComponent<IventPlayer>().TargetPositionNo)
        {
            case 0:
                break;
            case 1:
                TargetNo1();
                break;
            case 2:
                TargetNo2();
                break;
            case 3:
                state = EVENT_STATE.FPS;
                break;
        }
    }

    //****************************************************************************************************************
    //
    //  ターゲット番号が1番の時の処理
    //
    //****************************************************************************************************************
    void TargetNo1()
    {
         //  現在のカメラの位置を位置の一時保存用変数posに設定
         pos = transform.position;
         //  ステートをズームアウトに変更
         state = EVENT_STATE.ZOOM_OUT;
         if (player.GetComponent<IventPlayer>().TargetNo == 2)
         {
             state = EVENT_STATE.STOP;
         }
    }

    //****************************************************************************************************************
    //
    //  ターゲット番号が2番のとき
    //
    //****************************************************************************************************************
    void TargetNo2()
    {
        //  現在のカメラの位置を位置費保存する
        pos = transform.position;
        //  カメラをその場で止める
        state = EVENT_STATE.FPS;
    }
}
