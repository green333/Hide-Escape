using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{

    public Image key;
    public Image keyFrame;

    public GameObject player;

    private Image[] keyUI;           // キーUIのオブジェクト
    private bool[] activeKeyFlg;     // キーがアクティブになっているかを調べるフラグ

    private Image[] lastKeyUI = new Image[2];

    //private KeyUIInfo[] uiInfo;     //  キーUI

    public int keyInfo;
    // Use this for initialization
    void Start()
    {
        keyInfo = player.GetComponent<DoorManager>().Key.Count;

        //  UIを鍵の数だけ生成
        keyUI = new Image[player.GetComponent<DoorManager>().Key.Count];
        activeKeyFlg = new bool[player.GetComponent<DoorManager>().Key.Count];


        //  鍵の数だけUIの情報を設定
        for (int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
        {
            keyUI[i] = (Image)Instantiate(key);
            keyUI[i].rectTransform.sizeDelta = new Vector2(key.rectTransform.sizeDelta.x * (Screen.width / 800), key.rectTransform.sizeDelta.y * (Screen.height / 400));
            keyUI[i].rectTransform.position = new Vector3(Screen.width / 10 + ((i * 12) * keyUI[i].rectTransform.sizeDelta.x), Screen.height - (Screen.height / 10), 1.0f);
            //  keyUIを子に設定する
            keyUI[i].transform.parent = transform;
            activeKeyFlg[i] = false;
            //  鍵のUIがアクティブ状態ならUIをアクティブにする
            keyUI[i].gameObject.SetActive(activeKeyFlg[i]);
        }
        keyFrame.rectTransform.sizeDelta = key.rectTransform.sizeDelta;
        lastKeyUI[0] = (Image)Instantiate(key);
        lastKeyUI[1] = (Image)Instantiate(keyFrame);
        for (int i = 0; i < 2; ++i)
        {
            lastKeyUI[i].rectTransform.position = new Vector2(Screen.width / 25, Screen.height - (Screen.height / 6));
            lastKeyUI[i].rectTransform.sizeDelta = new Vector2(key.rectTransform.sizeDelta.x * ((Screen.width + Screen.height) / 550), key.rectTransform.sizeDelta.y * ((Screen.width + Screen.height) / 550));
            lastKeyUI[i].transform.parent = transform;
        }
        lastKeyUI[0].gameObject.SetActive(false);
        lastKeyUI[1].gameObject.SetActive(true);

    }

    public bool flg = false;
    // Update is called once per frame
    void Update()
    {
        AudioManager.Instance.PlaySE("heart_dqn", true);

        flg = player.GetComponent<P_Player>().GetLAST_KEY();
        if (player.GetComponent<P_Player>().GetLAST_KEY()) { lastKeyUI[0].gameObject.SetActive(true); }
        //  各部屋の鍵のUI
        for (int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
        {
            //  ドアが開かれたら鍵のUIを非アクティブ状態にする
            if (player.GetComponent<DoorManager>().Door[i].collider.isTrigger)
            {
                activeKeyFlg[i] = false;
            }

            //  鍵のUIがアクティブ状態ならUIをアクティブにする
            keyUI[i].gameObject.SetActive(activeKeyFlg[i]);

            //  鍵がアクティブ状態なら飛ばす
            if (player.GetComponent<DoorManager>().Key[i].obj.activeInHierarchy) { continue; }


            //  鍵のUIがアクティブ状態じゃないならば鍵を表示してフラグをtrueにする
            if (activeKeyFlg[i] == false)
            {
                activeKeyFlg[i] = true;
            }


        }
    }
}
