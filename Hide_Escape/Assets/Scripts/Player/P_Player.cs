using UnityEngine;
using System.Collections;

//プレイヤーの操作全般を行うクラスです




public class P_Player : MonoBehaviour
{
    //**********************************************************************
    //
    //**********************************************************************

    /***Inspector上で変更できるようにするために　ここで宣言してあります   ***/
    public float SPEED; //移動力
    public float RUNSPEED;//走ってるときの移動力
    public float TURNING;//回転力

    /*
     パラメーターを管理する
     * transformの値いじる場合はなるたけ　このクラスのメンバーに値をいれてから
     */
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

    /*
     * 入力状態の管理用
     */
    public enum INPUT_MODE
    {
        KEYBORD,
        GAMEPAD
    }
    public static INPUT_MODE Inputmode=INPUT_MODE.GAMEPAD;


    //カメラ回転に際しこの値を元に回転方向を決めます
    public static bool RightStick_Vertical;
    public static bool RightStick_Horizontal=true;



    // Use this for initialization
    void Start()
    {

        transform.position.Set(transform.position.x, 1, transform.position.z);//後で直す
        param = new P_param();
        param.pos = transform.position;


        int keynum = gameObject.GetComponent<DoorManager>().Key.Count;
        KeyData = new bool[keynum];
        for (int i = 0; i < keynum; i++)
        {
            KeyData[i] = false;
        }
    }


    //**********************************************************************
    //                     更新
    //**********************************************************************


    public enum STATE
    {
        NORMAL,
        HIDE_MOATION,
        HIDE
    }
    public STATE state = STATE.NORMAL;




    // ※　使用禁止　下記のFixedUpdateを使用のこと
    void Update()
    { }


    void FixedUpdate()
    {
        //※隠れる動作中も作動させるため例外的に設置
        if (Input.GetButtonDown("Right Botton"))
        {
            Right_Button();
        }
        if (Input.GetButtonDown("Back Botton"))
        {
            OperationMode_Change();
        }
        switch (state)
        {
            case STATE.NORMAL:
                MoveControl();
                Action_Cheak();
                Action_Input_Gamepad();


                break;
            case STATE.HIDE_MOATION:
                Hide_Action();
                break;


        }


        param.Update(transform.position);
        RotationControl();
        transform.rotation.Set(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
        ObjectParamUpdate();
        Rotation_Limit();

    }

    public bool RUN = false;
    private void ObjectParamUpdate()
    {

        switch (Inputmode)
        {
            case INPUT_MODE.KEYBORD:
                transform.rotation = Quaternion.AngleAxis(param.rotation, param.rotateAxis);
                break;
            case INPUT_MODE.GAMEPAD:


                transform.rotation = Quaternion.Euler(param.rotateVec);
                break;
            default:
                transform.rotation = Quaternion.AngleAxis(param.rotation, param.rotateAxis);
                break;

        }
        param.movement.y = 0.0f;
        if (Contact_Collision)
        {
            param.movement = Vector3.zero;
        }

        if (param.movement != Vector3.zero)
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


    /*
     *カメラの移動制限を行うクラスです
     *
     *  ※後のためのメモ
     *  初期姿勢時のx軸の値は0
     *  ー＞真上を向いた状態は　-90　＝　270
     *  ー＞真下を向いた状態が　90
     *  
     * 0~360 を-180～180に変換をかけた上で-70～70　に収まるように調整
      */
    public void Rotation_Limit()
    {
        Vector3 rotate = transform.rotation.eulerAngles;

        const float minA = -70;
        const float maxA = 70;


        if (rotate.x > 180)
            rotate.x -= 360;
        float work = Mathf.Clamp(rotate.x, minA, maxA);
        if (work < 0)
            work += 360;
        rotate.x = work;
        transform.eulerAngles = rotate;

    }


    //**********************************************************************
    //                      入力系
    //**********************************************************************

    /*
     ※後ほどここは整理する予定
    */

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
        // Debug.Log(param.movement);

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

    //入力判定用のタイマー　
    /*
     * この値を元に　ボタン押された時と押され続けられてる判定を取ります
    */
    public int Input_Timer = -1;
    //押され続けた判定　この値以上なら押され続けた　処理に移行します
    private const int CONTINUE_TO_BUTTON = 10;

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
            Right_Button();
        }

    }

    private void Action_Input_Gamepad()
    {

        //leftbottonとRightBottonは対応する処理ゆえに　return をつけていません


        if (Input.GetButton("Left Botton"))
        {
            RUN = true;
        }
        else if (Input.GetButtonUp("Left Botton"))
        {
            RUN = false;
        }

        //if (Input.GetButtonDown("Right Botton"))
        //{
        //    Right_Button();
        //}



        //
        if (Input.GetButton("Botton_A"))
        {

            if (Input_Timer > CONTINUE_TO_BUTTON)
            {
                Button_A();
                Debug.Log("押され続けてる");
            }
            Debug.Log("Aが押された");
            Button_A_PUSH();
            Input_Timer++;
            return;
        }
        else if (Input.GetButtonUp("Botton_A"))
        {
            //仮　後で直す
            Input_Timer = -1;
            RUN = false;
        }

        if (Input.GetButton("Botton_B"))
        {

            return;
        }
        if (Input.GetButton("Botton_X"))
        {

            return;
        }
        if (Input.GetButtonDown("Botton_Y"))
        {
            Debug.Log("yボタン押した");
            bool Rotation_Initializer;
            int timer = -1;
            for (Rotation_Initializer = false, timer = 60; !Rotation_Initializer || timer > 0; timer--)
            {
                float x, y, z;
                x = transform.eulerAngles.x;
                // y = transform.eulerAngles.y;
                z = transform.eulerAngles.z;

                transform.Rotate(-x, 0, -z);

                if ((int)x == 0/*&&(int)y==0*/&& (int)z == 0)
                    Rotation_Initializer = true;
            }



            return;
        }


        if (Input.GetAxis("Right Trigger") > 0)
        {
            Debug.Log("トリガー押した");
            return;
        }
        if (Input.GetAxis("Left Trigger") > 0)
        {

            return;
        }

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



    public void OperationMode_Change()
    {
        if (Inputmode == INPUT_MODE.GAMEPAD)
            Inputmode = INPUT_MODE.KEYBORD;
        else
            Inputmode = INPUT_MODE.GAMEPAD;
    }

    public void RightStick_Horizontal_InputDirection_Change()
    {
        if (RightStick_Horizontal)
            RightStick_Horizontal = false;
        else
            RightStick_Horizontal = true;
    }
    public void RightStick_Vertical_InputDirection_Change()
    {
        if (RightStick_Vertical)
            RightStick_Vertical = false;
        else
            RightStick_Vertical = true;
    }

    //**********************************************************************
    //                     各動作判別系
    //**********************************************************************
    /*
     * 各ボタンに応じた処理に
     */

    //Aボタン を　押した瞬間の動作
    private void Button_A_PUSH()
    {

        if (HIDEABLE == true)
        {


            Hide_Action();
        }

    }
    //ボタン押され続けられているとき処理
    private void Button_A()
    {

        RUN = true;

    }

    private void Right_Button()
    {


        GameObject Light = gameObject.transform.FindChild("Rantan").gameObject;
        LightSystem hoge = Light.GetComponent<LightSystem>();
        hoge.Lighting();
    }

    //**********************************************************************
    //                     各動作処理系
    //**********************************************************************

    //各処理にて用いる case管理用　　※名前はそのうち直します
    public enum STEP
    {

        NONE,

        //隠れる動作用
        HIDE_INIALIZE,//初期化
        HIDE_PREPARATION,//
        HIDE_START,//扉あける
        HIDE_1, //移動　（扉閉めるまで）
        HIDE_NOW,	//隠れている
        HIDE_2,	//扉開ける（）
        HIDE_3,	//移動（）
        HIDE_FINISH



    }
    public STEP step = STEP.NONE;



    //***************************************************
    //                    隠れる動作
    //***************************************************

    private Vector3 targetpos = Vector3.zero;//目標座標　（値保持用）
    private Vector3 originalpos = Vector3.zero;//元座標
    private Vector3 hide_movement = Vector3.zero;//隠れるとき用の移動量

    private Hide_Data hide = null;

    public bool HIDE_NOW = false; //隠れているかフラグ　※Enemyで主に使用

    private const float LENGTHDECIDE = 1.13f;//隠れる動作中の移動の際、この値以下になった場合に次のケースに移行
    private bool Hide_Action()
    {
        switch (step)
        {
            //
            case STEP.HIDE_INIALIZE:

                state = STATE.HIDE_MOATION;
                step = STEP.HIDE_PREPARATION;

                hide = null;
                targetpos = Vector3.zero;
                originalpos = Vector3.zero;
                hide_movement = Vector3.zero;
                break;

            //目標座標等の取得
            case STEP.HIDE_PREPARATION:
                {
                    Vector3 fwd = transform.TransformDirection(Vector3.forward);
                    RaycastHit hit;


                    if (Physics.Raycast(transform.position, fwd, out hit, 1))
                    {
                        if (hit.collider.tag == "Hide_Object")
                        {

                            hide = hit.collider.GetComponent<Hide_Data>();

                            targetpos = hide.HidePoint;
                            originalpos = param.pos;
                            hide_movement = (targetpos - originalpos).normalized * 0.1f;
                            if (!hide.Cheak(hide_movement * 10))
                            {
                                step = STEP.HIDE_FINISH;


                                break;
                            }
                            HIDE_NOW = true;
                            step = STEP.HIDE_START;
                        }
                    }
                    else
                        step = STEP.HIDE_FINISH;
                }

                break;

            //扉明け
            case STEP.HIDE_START:
                hide.Open_or_Close();
                //  hide.Disable_Collider();
                transform.GetComponent<BoxCollider>().enabled = false;
                //param.pos=targetpos;
                step = STEP.HIDE_1;
                break;

            //隠れる
            case STEP.HIDE_1:
                {
                    float length;
                    length = (targetpos - param.pos).magnitude;
                    Debug.Log("length:" + length);
                    if (length < LENGTHDECIDE)
                    {
                        hide.Open_or_Close();
                        // param.SetPos(targetpos);
                        step = STEP.HIDE_NOW;
                        transform.position.Set(targetpos.x, 1.1f, targetpos.z);//後で直す


                    }
                    else
                    {

                        param.movement = hide_movement * 3;
                    }

                }
                break;

            //隠れている
            case STEP.HIDE_NOW:

                if (Input.GetButton("Botton_A"))
                {
                    step = STEP.HIDE_2;
                }
                break;

            //扉明け
            case STEP.HIDE_2:
                hide.Open_or_Close();
                step = STEP.HIDE_3;

                break;

            //移動
            case STEP.HIDE_3:
                {
                    float length;
                    length = (originalpos - param.pos).magnitude;
                    //  if (length < LENGTHDECIDE)
                    if (length < 0.3f)
                    {

                        hide.Open_or_Close();
                        param.SetPos(originalpos);
                        step = STEP.HIDE_FINISH;
                        transform.GetComponent<BoxCollider>().enabled = true;

                    }
                    else
                    {
                        param.movement = -hide_movement * 3;
                    }

                }

                break;
            //終了時処理
            case STEP.HIDE_FINISH:
                HIDE_NOW = false;
                originalpos = Vector3.zero;
                targetpos = Vector3.zero;
                hide_movement = Vector3.zero;

                hide = null;
                state = STATE.NORMAL;
                step = STEP.NONE;

                break;
            default:
                step = STEP.HIDE_INIALIZE;
                break;
        }
        return false;
    }


    //**********************************************************************
    //                     当たり判定
    //**********************************************************************
    //※　最初に　tag名を比較した後　それだけで判断できない場合　nameを用いて判定します

    //*** 能動的な判定 ***//
    /*
     仕様
     * 隠れたりとか能動的に行う処理用です
     * 
    */

    /*  各対応tag名
    * 
    *HIDEABLE ::Hide_Object
    
    */

    void Action_Cheak()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 1))
        {
            //隠れる事ができるか判定
            if (hit.collider.tag == "Hide_Object")
            {
                Hideable();
            }
            else
            {
                Reset_Menber_for_State();
            }

        }
        else
        {
            Reset_Menber_for_State();

        }

    }


    //*** 受動的な判定 ***//
    /*
     :仕様
     * 
     * ステージ移動は　ステージ移動用のオブジェクトをステージに設置し　
     *  それにふれたら遷移判定を行う　その際　接触した物体の名前を用いて判定します
     * ゲームオーバーは　敵に触れた時点で　遷移
   */


    /*  各対応tag名
     * ※　chengeSceneに関しては　nameも使用
   
     * IS_GAMEOVER :: Enemy
     * IS_GAMECLEAR :: CLEAR
     * IS_NEXTSTAGE :: ChengeScene  NEXT
     * IS_PREVSTAGE :: ChengeScene  PREV
     */

    public bool Contact_Collision = false;
    PopUp pop = null;

    public static bool LAST_KEY = false;

    public Door door = null;



    public string hoge;

    void OnCollisionEnter(Collision col)
    {
        Contact_Collision = true;
        param.movement = Vector3.zero;

        //敵との接触判定　＝＝死亡判定
        if (col.gameObject.tag == "Hide_Object")
        {
            Hide_PopUp(col);
          
        }
        //敵との接触判定　＝＝死亡判定
        if (col.gameObject.tag == "Enemy")
        {
            Deth_Prosess();
            return;
        }
        //ゲームクリア判定
        else if (col.gameObject.tag == "CLEAR")
        {
            Clear_prosess();
            return;
            ;
        }
        //シーン移動判定
        else if (col.gameObject.tag == "ChengeScene")
        {
            ChengeScene_Proasess(col.gameObject.name.ToString());
            return;
        }



        //かぎに接触
        if (col.gameObject.tag == "KEY")
        {

            this.CheakKey(col.gameObject.name.ToString());

            pop = GetComponent<PopUp>();

            if (col.gameObject.name == "Last_Key")
            {
                pop.SetText("    玄関の鍵を手に入れた！！     ");//仮
                LAST_KEY = true;

            }
            else
                pop.SetText("    かぎを取得     ");//仮

            pop.Activate(60);
        }


        if (col.gameObject.tag == "DOOR")
        {

            hoge = col.gameObject.name.ToString();
            DoorPopup(col.gameObject.name.ToString());


        }




        if (col.gameObject.tag == "FrontDoor")
        {

            if (LAST_KEY)
            {



                door = col.collider.GetComponent<Door>();
                door.Open_Door();
                Clear_prosess();
            }

        }


    }
    void OnCollisionStay(Collision col)
    {

        Debug.Log("接触中");

        param.movement = Vector3.zero;
        Contact_Collision = false;


    }
    void OnCollisionExit(Collision col)
    {
        Contact_Collision = false;
        pop = GetComponent<PopUp>();
        pop.Deactivate();
    }

    //鍵の取得状況
    public bool[] KeyData;
    //  public  int KeyData;

    private void DoorPopup(string name)
    {
        //
        for (int i = 0; i < KeyData.Length; i++)
        {
            if (KeyData[i] && (name == "door" + i))
            {
                pop = GetComponent<PopUp>();
                pop.SetText("　B:扉を開ける　");
                pop.Activate();
                return;
            }



        }
        pop = GetComponent<PopUp>();
        pop.SetText(" 鍵がかかっているようだ・・・ ");//仮
        pop.Activate(180);
        return;

    }

    private void CheakKey(string name)
    {

        for (int i = 0; i < KeyData.Length; i++)
        {
            if (name == "key" + i)
            {
                KeyData[i] = true;

            }

        }

    }


    private void Hide_PopUp(Collision col)
    {

        hide = col.collider.GetComponent<Hide_Data>();
        Vector3 pos = hide.HidePoint;
        Vector3 length = (pos - transform.position).normalized;
        if (hide.Cheak(length))
        {
            pop = GetComponent<PopUp>();
            pop.SetText(" A:隠れる");//仮
            pop.Activate();
        }

        hide = null;
    }






    //**********************************************************************
    //                  ステート切り替え関連　主に　Action関数・Update関数　にて使用
    //**********************************************************************

    /*
     *仕様上　レイがあたっていない状態　==　動作ができない状態　を示すために
     * 毎フレーム　関連する変数はすべて　faleseにさせています　
     */



    /*
     HIDEABLE 隠れる事ができるかどうかを示す変数
     *  この変数がtrueのときに　特定のボタンを押すことで隠れる動作をさせる（予定）
     */



    //Updateで使う変数　Update関数で使用
    public bool HIDEABLE = false;

    private void Hideable()
    {
        HIDEABLE = true;
    }

    //関連変数初期化関数
    private void Reset_Menber_for_State()
    {
        HIDEABLE = false;

    }

    //**********************************************************************
    //                    シーン切り替え関連　主に　OnColision関数内にて使用
    //**********************************************************************


    //各処理で使う変数　GameScene.cs　にて参照
    public bool IS_GAMEOVER = false;
    public bool IS_GAMECLEAR = false;
    public bool IS_NEXTSTAGE = false;
    public bool IS_PREVSTAGE = false;



    /*死亡判定*/

    private void Deth_Prosess()
    {
        IS_GAMEOVER = true;
    }

    /*クリア判定*/
    private void Clear_prosess()
    {
        IS_GAMECLEAR = true;
        LAST_KEY = false;
        return;
    }

    /*ステージ移動判定*/

    private void ChengeScene_Proasess(string name)
    {

        if (name == "NEXT")
        {
            Chenge_NextScene();
            return;
        }
        else if (name == "PREV")
        {
            Chenge_PrevScene();
            return;
        }

        return;

    }

    //次のステージへの移動
    private void Chenge_NextScene()
    {
        IS_NEXTSTAGE = true;
        return;
    }
    //前のステージへの移動
    private void Chenge_PrevScene()
    {
        IS_PREVSTAGE = true;
        return;
    }



    //**********************************************************************
    //                     ゲッター
    //**********************************************************************

    public bool GetLAST_KEY()
    {
        return LAST_KEY;
    }

    public bool GetHIDE_NOW()
    {
        return HIDE_NOW;
    }



}