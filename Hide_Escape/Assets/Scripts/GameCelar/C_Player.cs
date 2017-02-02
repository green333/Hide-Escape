using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System;




public class C_Player : MonoBehaviour {

    [SerializeField]
    private bool RightStick_Vertical;

    [SerializeField]
    private bool RightStick_Horizontal;
    class P_param
    {
        public Vector3 pos;

        //更新毎に初期化する変数
        public float rotation; //回転量 ※ Xの値は使用しない
        public Vector3 rotateAxis;//回転軸　※Zの値は使用しない
        public Vector3 movement; //移動量 

        public Vector3 rotateVec;//回転量


        void Start()
        {
            pos = Vector3.zero;
            rotation = 0.0f;
            movement = Vector3.zero;
        }

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


    private bool RUN = false;
    private float TURNING = 5; //回転力
    private float RUNSPEED = 0.07f;//走ってるときの移動力
    private float SPEED = 0.02f; //移動力

    private bool kncok_f = false;    //ノックする

    public new Light light = null;

    private struct Door_Knock
    {
        public bool b_knock;      //ノックした
        public int knock_num;   //ノック回数
    }

    private Door_Knock[] knock = new Door_Knock[4];   //ノック

    protected enum Knock_Order   //ノックの順番
    {
        DOOR_0 = 0,
        DOOR_1 = 1,
        DOOR_2 = 2,
        DOOR_3 = 3,
        DOOR_FALSE = 4,     //順番通り失敗
        NONE_DOOR = 5,      //何もしない
    }

    [SerializeField]
    private Image over_fade;  //ゲームオーバーのフェード

    [SerializeField]
    private Image next_clear;

    [SerializeField]
    private RGB_Color color;

    public enum Input_mode
    {
        KEYBOARD = 0,  //キーボード
        GAMEPAD,       //ゲームパッド
    }

    public enum Game_Next
    {
        CLEAR = 0,       //ゲームクリア
        OVER,            //ゲームオーバー
    }


    [SerializeField]
    private Input_mode input_mode;

    protected Knock_Order order;    //ノックの準場

    private Game_Next next;    //ノックが終わって次の処理

    private bool b_next = false;   //ゲーム終了して次の処理

    private float dist = 0.8f;    //扉までの距離

    private P_param param;


    void SetColor(Image image,float r,float g,float b,float a)
    {
        color.red = r;
        color.green = g;
        color.blue = b;
        color.alpha = a;
        image.color = new Color(color.red, color.green, color.blue, color.alpha);

    }

    //---------------------------------------

    //  初期化

    //---------------------------------------
    void Start()
    {
        transform.position.Set(transform.position.x, 1, transform.position.z);//後で直す
        param = new P_param();
        param.pos = transform.position;



        for (int i = 0; i < knock.Length; i++)
        {
            knock[i] = new Door_Knock();
            knock[i].knock_num = 0;
        }
        kncok_f = false;

        light.intensity = 0.6f;

        SetColor(over_fade, 1, 1, 1, 0);
        SetColor(next_clear, 1, 1, 1, 0);
        order = Knock_Order.NONE_DOOR;
    }




    //---------------------------------------

    //  更新

    //---------------------------------------
    void Update () {


    }
    string next_name;

    void Color_Add(Image image, float spd, string name)
    {
        if (color.alpha < 1)
        {
            color.alpha += spd;

        }
        else
        {
            next_name = name;
            if (name == "GameClear")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Application.LoadLevel(next_name);
                }
            }
            else
            {
                Application.LoadLevel(next_name);
            }
        }

        SetColor(image, color.red, color.green, color.blue, color.alpha);
    }

    //----------------------------------------------------

    //  物理の更新

    //----------------------------------------------------
    void FixedUpdate()
    {

        if (b_next)
        {
            GameNext();
        }
        else
        {
            Raycast_Hit();
            Ivent();
            Action_Input_Keybord();
            MoveControl(); 
            RotationControl();
            ObjectParamUpdate();
        }
    }


    //----------------------------------------------------

    //  ノックが終わった後の処理

    //----------------------------------------------------
    void GameNext()
    {
        switch (next)
        {
            case Game_Next.CLEAR:
                Color_Add(next_clear,0.05f, "My_Room");
                break;

            case Game_Next.OVER:
                Color_Add(over_fade, 0.05f, "GameOver");
                break;
        }
    }

    //----------------------------------------------------

    //  移動の入力管理

    //----------------------------------------------------
    private void MoveControl()
    {
        Move_Input_Gamepad();
        Move_Input_Keybord();
        return;
    }

    //----------------------------------------------------

    //  回転の入力管理

    //----------------------------------------------------
    private void RotationControl()
    {
        Rotation_Input_Gamepad();
        Rotation_Input_Keybord();
        return;
    }


    //----------------------------------------------------

    //  キーボード対応の移動

    //----------------------------------------------------
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


    //----------------------------------------------------

    //  ゲームパッド対応の移動

    //----------------------------------------------------
    private void Move_Input_Gamepad()
    {
        Vector3 Z = transform.right * Input.GetAxis("Left Joystick Horizontal");
        Vector3 X = transform.forward * Input.GetAxis("Left Joystick Vertical");
        param.movement = Z + X;

        return;
    }




    void Ivent()
    {

        for(int i = 0; i < knock.Length; i++)
        {
            if(knock[i].knock_num == 1)
            {
                knock[i].b_knock = true;
            }
            else
            {
                knock[i].b_knock = false;
            }
        }

        switch (order)   //順番通りに扉をたたく(1->2->3->0)
        {
            case Knock_Order.DOOR_0:   //一番最後の扉
                if (!knock[0].b_knock && knock[1].b_knock &&
                     knock[2].b_knock && knock[3].b_knock)
                {
                    next = Game_Next.CLEAR;
                    b_next = true;
                }
                else
                {
                    order = Knock_Order.DOOR_FALSE;
                }

                break;

            case Knock_Order.DOOR_1:   //一番最初に叩くドア
                if (!knock[0].b_knock && !knock[1].b_knock &&
                    !knock[2].b_knock && !knock[3].b_knock)
                {
                    order = Knock_Order.NONE_DOOR;
                }
                else
                {
                    order = Knock_Order.DOOR_FALSE;
                }
                break;

            case Knock_Order.DOOR_2:   //二番目に叩くドア
                if (!knock[0].b_knock && knock[1].b_knock &&
                    !knock[2].b_knock && !knock[3].b_knock)
                {
                    order = Knock_Order.NONE_DOOR;
                }
                else
                {
                    order = Knock_Order.DOOR_FALSE;
                }
                break;

            case Knock_Order.DOOR_3:   //三番目に叩くドア
                if (!knock[0].b_knock && knock[1].b_knock &&
                    knock[2].b_knock && !knock[3].b_knock)
                {
                    order = Knock_Order.NONE_DOOR;
                }
                else
                {
                    order = Knock_Order.DOOR_FALSE;
                }
                break;

            case Knock_Order.DOOR_FALSE:  //失敗した場合
                b_next = true;
                next = Game_Next.OVER;
                break;

            case Knock_Order.NONE_DOOR:
                break;
        }
    }

    //----------------------------------------------------

    //  キーボード対応の回転

    //----------------------------------------------------
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


    //----------------------------------------------------

    //  ゲームパッド対応の回転

    //----------------------------------------------------
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




    //----------------------------------------------------

    //  プレイヤーと扉の距離

    //----------------------------------------------------
    bool Raycast_Hit()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);


        if(Physics.Raycast(ray,out hit, dist) && hit.collider.tag == "DOOR_0") {
            Nock_Ivent(Knock_Order.DOOR_0);
        }

        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "DOOR_1")
        {
            Nock_Ivent(Knock_Order.DOOR_1);
        }

        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "DOOR_2")
        {
            Nock_Ivent(Knock_Order.DOOR_2);
        }

        if (Physics.Raycast(ray, out hit, dist) && hit.collider.tag == "DOOR_3")
        {
            Nock_Ivent(Knock_Order.DOOR_3);
        }
        return false;
    }


    private void ObjectParamUpdate()
    {

        if(Input.GetButtonDown("Back Botton"))
        {
            switch (input_mode)
            {
                case Input_mode.KEYBOARD:
                    input_mode = Input_mode.GAMEPAD;
                    break;

                case Input_mode.GAMEPAD:
                    input_mode = Input_mode.KEYBOARD;
                    break;

                default:
                    break;

            }

        }

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



    //----------------------------------------

    //  扉ノックするイベント

    //----------------------------------------
    void Nock_Ivent(Knock_Order order_num)
    {

        if (kncok_f)
        {
            kncok_f = false;
            knock[(int)order_num].knock_num++;
        }

        if ((Input.GetKey(KeyCode.KeypadEnter) ||
            Input.GetButton("Botton_B") )&& AudioManager.Instance.PlaySE_End())
        {
            AudioManager.Instance.PlaySE("nock");
            kncok_f = true;
            order = order_num;
        }


    } 

}
