using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyUIInfo
{
    public Image keyUI;           // キーUIのオブジェクト
    public bool activeKeyFlg = false;     // キーがアクティブになっているかを調べるフラグ
}

public class KeyUI : MonoBehaviour {

    public Image key;

    public GameObject player;

    private Image[] keyUI;           // キーUIのオブジェクト
    private bool[] activeKeyFlg;     // キーがアクティブになっているかを調べるフラグ

    //private KeyUIInfo[] uiInfo;     //  キーUI

    public int keyInfo;
	// Use this for initialization
	void Start () {
        keyInfo = player.GetComponent<DoorManager>().Key.Count;

        //  UIを鍵の数だけ生成
        keyUI = new Image[player.GetComponent<DoorManager>().Key.Count];
        activeKeyFlg = new bool[player.GetComponent<DoorManager>().Key.Count];

        //  鍵の数だけUIの情報を設定
        for(int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
        {
            keyUI[i] = (Image)Instantiate(key, new Vector3(80.0f + (i * 30.0f), 380.0f, 1.0f), new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
            //  keyUIを子に設定する
            keyUI[i].transform.parent = transform;
            activeKeyFlg[i] = false;
            
        }

	}

	// Update is called once per frame
	void Update () {
        AudioManager.Instance.PlaySE("heart_dqn", true);

	    for(int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
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
