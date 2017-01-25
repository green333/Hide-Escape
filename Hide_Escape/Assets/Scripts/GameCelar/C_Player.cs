using UnityEngine;
using System.Collections;

public class C_Player : MonoBehaviour {

    public bool RightStick_Vertical;
    public bool RightStick_Horizontal;

    public bool RUN = false;

    public float TURNING;//回転力
    public float RUNSPEED;//走ってるときの移動力
    public float SPEED; //移動力

    public static bool nock = false;    //ノックする


    public static bool GetNock()
    {
        return nock;
    }

    public enum Input_mode
    {
        KEYBOARD = 0,  //キーボード
        GAMEPAD,       //ゲームパッド
    }

    public Input_mode input_mode;

    private class P_param
    {
        public Vector3 pos;

        //更新毎に初期化する変数
        public float rotation; //回転量 ※ Xの値は使用しない
        public Vector3 rotateAxis;//回転軸　※Zの値は使用しない
        public Vector3 movement; //移動量 

        public Vector3 rotateVec;//回転量




        // Use this for initialization
        void Start()
        {
            pos = Vector3.zero;
            rotation = 0.0f;
            movement = Vector3.zero;
        }

        // Update is called once per frame
        public void Update(Vector3 pos)
        {
            this.pos = pos;

        }

        public void ResetMovement()
        {
            movement = Vector3.zero;
        }

        public void ResetRotation()
        {
            rotation = 0.0f;
        }

        public Vector3 GetAddedMovement()
        {

            Vector3 score = Vector3.zero;

            float x = pos.x + movement.x;
            float y = pos.y + movement.y;
            float z = pos.z + movement.z;

            score.Set(x, y, z);

            return score;
        }

        public void SetPos(Vector3 p)
        {
            pos = p;
        }
    }

    private P_param param;



    //---------------------------------------

    //  初期化

    //---------------------------------------
    void Start()
    {
        transform.position.Set(transform.position.x, 1, transform.position.z);//後で直す
        param = new P_param();
        param.pos = transform.position;

    }



    //---------------------------------------

    //  更新

    //---------------------------------------
    void Update () {
        
    }



    void FixedUpdate()
    {
        MoveControl();
        RotationControl();
        ObjectParamUpdate();
    }

    private void MoveControl()
    {
        Move_Input_Gamepad();
        Move_Input_Keybord();
        return;
    }


    private void RotationControl()
    {
        Rotation_Input_Gamepad();
        Rotation_Input_Keybord();
        return;
    }



    private void Move_Input_Keybord()
    {
        if (Input.GetKey(KeyCode.W))
        {
            param.movement = transform.forward;
            return;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            param.movement = -transform.forward;
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            param.movement = -transform.right;
            return;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            param.movement = transform.right;
            return;
        }

        return;

    }

    private void Move_Input_Gamepad()
    {
        Vector3 Z = transform.right * Input.GetAxis("Left Joystick Horizontal");
        Vector3 X = transform.forward * Input.GetAxis("Left Joystick Vertical");
        param.movement = Z + X;

        return;
    }





    private void Rotation_Input_Keybord()
    {


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // param.rotation -= param.;
            param.rotation -= 1.0f;
            param.rotateAxis = Vector3.up;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            param.rotation += 1.0f;
            param.rotateAxis = Vector3.up;
            return;
        }



        return;
    }

    private void Rotation_Input_Gamepad()
    {
        param.rotateVec = transform.rotation.eulerAngles;
        if (RightStick_Horizontal)
        {
            param.rotateVec.y += Input.GetAxis("Right Joystick Horizontal") * TURNING;
        }
        else
        {
            param.rotateVec.y -= Input.GetAxis("Right Joystick Horizontal") * TURNING;

        }

        if (RightStick_Vertical)
        {
            param.rotateVec.x += Input.GetAxis("Right Joystick Vertical") * TURNING;
        }
        else
        {
            param.rotateVec.x -= Input.GetAxis("Right Joystick Vertical") * TURNING;
        }


        return;

    }




    private void ObjectParamUpdate()
    {

        switch (input_mode)
        {
            case Input_mode.KEYBOARD:
                transform.rotation = Quaternion.AngleAxis(param.rotation, param.rotateAxis);
                break;
            case Input_mode.GAMEPAD:


                transform.rotation = Quaternion.Euler(param.rotateVec);
                break;
            default:
                transform.rotation = Quaternion.AngleAxis(param.rotation, param.rotateAxis);
                break;

        }

        {
            if (RUN)
            {
                transform.localPosition += param.movement * RUNSPEED;
            }
            else
            {
                transform.localPosition += param.movement * SPEED;
            }
        }
        param.rotateVec = Vector3.zero;
        param.SetPos(transform.localPosition);

        transform.position.Set(transform.position.x, 1.1f, transform.position.z);//後で直す
        param.ResetMovement();



    }




    private void Action_Input_Keybord()
    {

        if (Input.GetButton("Left Botton"))
        {
            RUN = true;
        }
        else if (Input.GetButtonUp("Left Botton"))
        {
            RUN = false;
        }

        if (Input.GetButtonDown("Right Botton"))
        {
            //Right_Button();
        }

    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "DOOR_0")
        {
            Nock_Ivent(0);
        }

        if (col.gameObject.tag == "DOOR_1")
        {
            Nock_Ivent(0);
        }

        if (col.gameObject.tag == "DOOR_2")
        {
            Nock_Ivent(0);
        }

        if(col.gameObject.tag == "DOOR_3")
        {
            Nock_Ivent(0);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "DOOR_0")
        {
            nock = false;
        }

        if (col.gameObject.tag == "DOOR_1")
        {
            nock = false;
        }

        if (col.gameObject.tag == "DOOR_2")
        {
            nock = false;
        }

        if (col.gameObject.tag == "DOOR_3")
        {
            nock = false;
        }
    }



    //----------------------------------------

    //  扉ノックするイベント

    //----------------------------------------
    void Nock_Ivent(int nock_num)
    {
        if (Input.GetKey(KeyCode.Space) && AudioManager.Instance.PlaySE_End())
        {
            AudioManager.Instance.PlaySE("nock");
            nock = true;

        }
    } 

}
