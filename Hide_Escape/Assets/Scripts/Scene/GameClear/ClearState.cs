using UnityEngine;
using System.Collections;

public class ClearState : MonoBehaviour {


    enum Clear_State
    {
        WAKE_UP = 0,           //ホワイトアウトして目覚める
        EYES_LOOK_SIDEWAYS,    //目線が横を向く
        TARGET_LOOP_ZOOM_IN,   //ターゲットに寄る
        FIN,                   //終了
    }

    [SerializeField]
    private Clear_State state;         //クリアステート


    private float angle;    //回転

    private float spd = 30.0f;   //回転速度

    private float moved;  //移動値

    //-------------------------------------------

    //  初期化

    //-------------------------------------------
    void Start () {
	}



    //-------------------------------------------

    //  更新

    //-------------------------------------------
    void Update () {


        switch(state)
        {
            case Clear_State.WAKE_UP:
                WakeUp();
                break;

            case Clear_State.EYES_LOOK_SIDEWAYS:
                Look_Sideway();
                break;

            case Clear_State.TARGET_LOOP_ZOOM_IN:
                Zoom_In();
                break;

            case Clear_State.FIN:
                Fin();
                break;
        }


    }



    //-------------------------------------------

    //  ステートをセット

    //-------------------------------------------
    void SetState(Clear_State c_state)
    {
        state = c_state;
    }


    void Zoom_State()
    {
        SetState(Clear_State.TARGET_LOOP_ZOOM_IN);
    }



    //-------------------------------------------

    //  起き上がる処理

    //-------------------------------------------
    void WakeUp()
    {

        transform.eulerAngles = new Vector3(transform.rotation.x, 
                                            transform.rotation.y,
                                            transform.rotation.z + angle * spd);

       
        if (transform.eulerAngles.z > 90)
        {
            angle = 0;
            SetState(Clear_State.EYES_LOOK_SIDEWAYS);
        }
        else
        {
            angle += Time.deltaTime;
        }
    }


    //-------------------------------------------

    //  横を向く

    //-------------------------------------------
    void Look_Sideway()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x,
                                            transform.rotation.y + angle * spd,
                                            transform.rotation.z + 90);

        if(transform.eulerAngles.y > 90)
        {
            Invoke("Zoom_State", 1);
            //SetState(Clear_State.TARGET_LOOP_ZOOM_IN);
        }
        else
        {
            angle += Time.deltaTime;
        }
    }


    //-------------------------------------------

    //  注目する（そのオブジェクトに寄る
    
    //-------------------------------------------
    void Zoom_In()
    {

        transform.position += new Vector3(0, 0, moved);

        if(transform.position.z > -1.77929)
        {
            SetState(Clear_State.FIN);
        }
        else
        {
            moved = Time.deltaTime;
        }
    }



    void Fin()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("fin");
        }
    }

}
