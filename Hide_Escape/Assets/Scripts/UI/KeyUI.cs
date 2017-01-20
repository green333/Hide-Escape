using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyUIInfo
{
    public GameObject keyUI;           // キーUIのオブジェクト
    public bool activeKeyFlg = false;     // キーがアクティブになっているかを調べるフラグ
}

public class KeyUI : MonoBehaviour {

    public Image key;

    public GameObject player;

    private Image[] keyUI;           // キーUIのオブジェクト
    private bool[] activeKeyFlg;     // キーがアクティブになっているかを調べるフラグ

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
            
            //keyUI[i].transform.parent = transform;
            keyUI[i] = (Image)Instantiate(key, new Vector3(80.0f + (i * 30.0f), 380.0f, 1.0f), new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
            //  keyUIを子に設定する
            keyUI[i].transform.parent = transform;
           // keyUI[i].GetComponent<RectTransform>().sizeDelta.Scale(new Vector2(keyScale, keyScale));
            activeKeyFlg[i] = false;
        }


        ////  UIを鍵の数だけ生成
        //uiInfo = new KeyUIInfo[player.GetComponent<DoorManager>().Key.Count];
        ////  鍵の数だけUIの情報を設定
        //for (int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
        //{
        //    //  鍵情報を子に設定する
        //    uiInfo[i].keyUI.transform.parent = transform;
        //    uiInfo[i].activeKeyFlg = false;
        //    uiInfo[i].keyUI = key;    
        //    keyInfo = i;
        //}



	}
	
	// Update is called once per frame
	void Update () {
        AudioManager.Instance.PlaySE("heart_dqn", true);

	    for(int i = 0; i < player.GetComponent<DoorManager>().Key.Count; ++i)
        {
            //  鍵がアクティブ状態なら飛ばす
            if (player.GetComponent<DoorManager>().Key[i].obj.activeInHierarchy) { continue; }


            //  鍵のUIがアクティブ状態じゃないならば鍵を表示してフラグをtrueにする
            if(activeKeyFlg[i] == false)
            {

                activeKeyFlg[i] = true;
            }


            ////  鍵のUIがアクティブ状態じゃないならば鍵を表示してフラグをtrueにする
            //if (uiInfo[i].activeKeyFlg == false) 
            //{
            //    uiInfo[i].keyUI.transform.position = new Vector3(100.0f, 100.0f, 1.0f);
            //    uiInfo[i].activeKeyFlg = true;
            //}

        }
	}
}
