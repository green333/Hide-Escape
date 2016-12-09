using UnityEngine;
using System.Collections;

public class LightSystem : MonoBehaviour
{
    private const float R = 1.5f;    //  ライトの赤色情報
    private const float G = 1.0f;    //  ライトの緑色情報
    private const float B = 0.5f;    //  ライトの青色情報
    private const float MAX = 10.0f; //  ライトの範囲の最大値
    private const float MIN = 0.0f;  //  ライトの範囲の最少値
    private const float SPEED = 1.0f;//  ライトの範囲の変更速度

    private Light LightComp;    //  ライト
    private bool isAddRange;    //  ライトの範囲変更フラグ
    private float PushRange;    //  ボタン押下時のライトの範囲

    private bool _isLighting;   //  ライト点灯状態取得用変数
    private bool _isLightON;    //  ライト点灯フラグ

    //-----------------------------------------------------------------
    //  @brief  初期化
    //-----------------------------------------------------------------
    void Start()
    {
        GameObject LightObj = new GameObject("Light");  //  オブジェクト生成
        LightComp = LightObj.AddComponent<Light>();     //  オブジェクトにライトを追加
        LightComp.color = new Color(R, G, B);           //  ライトの色を設定
        LightComp.range = MIN;                          //  ライトの範囲を設定
        LightComp.type = LightType.Point;               //  ライトをポイントライトに設定
        _isLighting = false;                            //  初期ライティング状態
        _isLightON = false;                             //  初期点灯フラグ

        //  座標設定
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        LightObj.transform.position = obj.transform.position;
        LightObj.transform.localEulerAngles = obj.transform.localEulerAngles;

        //  ライトをMain Cameraの子オブジェクトにする
        LightObj.transform.parent = Camera.main.gameObject.transform;

    }

    //-----------------------------------------------------------------
    //  @brief  更新
    //-----------------------------------------------------------------
    void Update()
    {
        LightInput();   //  ライトを点灯するボタン押下時の処理
        ChangeRange();  //  ライトの範囲変更処理
    }

    //-----------------------------------------------------------------
    //  @brief  ライト点灯するボタン押下時の処理
    //-----------------------------------------------------------------
    void LightInput()
    {
        //bool isEnter = (bool)(Input.GetKeyDown(KeyCode.KeypadEnter));
        //bool isLRPush = (bool)(Input.GetButton("Right Botton") || Input.GetButton("Left Botton"));

        //if (isEnter || isLRPush)             //  押下判定

        if (_isLightON)
        {
            if (!isAddRange)                 //  ライトの範囲変更フラグがfalseなら
            {
                isAddRange = true;           //  ライトの範囲変更フラグを立てる
                PushRange = LightComp.range; //  ライトの範囲を保存
                _isLightON = false;
            }
        }
    }

    //-----------------------------------------------------------------
    //  @brief  ライトの範囲変更処理
    //-----------------------------------------------------------------
    void ChangeRange()
    {
        if (!isAddRange) { return; }                        //  ライトの範囲変更フラグがfalseならリターン

        if (PushRange <= MIN) { LightComp.range += SPEED; } //  暗かったら明るくする
        if (PushRange >= MAX) { LightComp.range -= SPEED; } //  明るかったら暗くする

        if (LightComp.range >= MAX ||                       //  ライトの範囲が最大か最少なら
            LightComp.range <= MIN)
        {
            isAddRange = false;                             //  ライトの範囲変更フラグをfalseにする
        }
    }


    //-----------------------------------------------------------------
    //  @brief  ライト点灯フラグのゲッター
    //  @param[in]  ライト点灯状態(bool)
    //-----------------------------------------------------------------
    public bool GetIsLighting()
    {
        if (LightComp.range == MAX) { _isLighting = true; }
        if (LightComp.range == MIN) { _isLighting = false; }

        return _isLighting;
    }

    //-----------------------------------------------------------------
    //  @brief  点灯
    //-----------------------------------------------------------------
    public void Lighting()
    {
        _isLightON = true;
    }
}
