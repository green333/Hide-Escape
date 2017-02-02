using UnityEngine;
using System.Collections;

public class LastKey : MonoBehaviour
{

    static bool keyGetFlg = true;
    public bool KeyGetFlg
    {
        get { return keyGetFlg; }
    }

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        if (player.GetComponent<P_Player>().GetLAST_KEY() == false)
        {
            keyGetFlg = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.SetActive(keyGetFlg);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Instance.PlaySE("item_get");
            keyGetFlg = false;
        }
    }
}
