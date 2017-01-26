using UnityEngine;
using System.Collections;

public class Clear_Ivent : MonoBehaviour
{


    public enum Nock_Turn   //ノックの順番
    {
        DOOR_0 = 0x01,
        DOOR_1 = 0x02,
        DOOR_2 = 0x04,
        NOCK_FALSE = 0x08,    //ノック失敗
    }




    void Start()
    {
    }

    void Update()
    {

    }


    //-------------------------------------

    //  ノック回数
    //  引数nock_numはノックした回数

    //-------------------------------------
    void Nock_Num(int nock_)
    {

    }

}