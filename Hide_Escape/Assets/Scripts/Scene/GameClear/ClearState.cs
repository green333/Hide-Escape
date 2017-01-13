using UnityEngine;
using System.Collections;

public class ClearState : MonoBehaviour {

    //[SerializeField]
    //private GameObject main_camera;   //カメラ

    enum Clear_State
    {
        WAKE_UP = 0,         //ホワイトアウトして目覚める
        EYES_LOOK_SIDEWAYS,  //目線が横を向く
        TARGET_LOOP_ZOOM_IN,   //ターゲットに寄る
        FIN,
    }

    [SerializeField]
    Clear_State state;

	void Start () {
	}



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
                break;

        }

    }

    void SetState(Clear_State st)
    {
        state = st;
    }

    float angle;    //回転

    float spd = 30.0f;   //回転速度

    void WakeUp()
    {
        angle += Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, angle * spd);

        if (transform.eulerAngles.z > 90)
        {
            angle = 0;
            SetState(Clear_State.EYES_LOOK_SIDEWAYS);   
        }
    }

    void Look_Sideway()
    {
        
        angle += Time.deltaTime;

        transform.eulerAngles = new Vector3(0, angle * spd, 90);

        if(transform.eulerAngles.y > 90)
        {
            SetState(Clear_State.TARGET_LOOP_ZOOM_IN);
        }
    }

    float moved;  //移動値


    void Zoom_In()
    {
        moved = Time.deltaTime;

        transform.position += new Vector3(0, 0, moved);

        if(transform.position.z > -1.77929)
        {
            SetState(Clear_State.FIN);
        }
    }


}
