using UnityEngine;
using System.Collections;

public class Clear_Ivent : MonoBehaviour {

    //struct Door_Nock
    //{ 
    //    bool door_0;      //←
    //    bool door_1;      //→
    //    bool door_2;      //↑(正面)
    //}


    public struct Door_Nock
    {
        int nock;     //ノック回数
        bool door;    //
    }

    static public Door_Nock[] door_nock;

    public enum Nock_Turn   //ノックの順番
    {
        DOOR_0 = 1,
        DOOR_1,
        DOOR_2,         
        NOCK_FALSE,    //ノック失敗
    }

    static int nock_num;   //ノックの回数


    static int GetNock_Num()
    {
        return nock_num;
    }

    static void SetNock_Num(int num)
    {
        nock_num = num;
    }


	void Start () {
        
	}
	
	void Update () {

        Debug.Log(nock_num);

        if (C_Player.GetNock())
        {
            //if (nock_num == (int)Nock_Turn.DOOR_0)
            {
                nock_num++;
                return;
            }

            //if (nock_num == (int)Nock_Turn.DOOR_1)
            {
                //nock_num++;
                //return;
            }

            //if (nock_num == (int)Nock_Turn.DOOR_2)
            {
                //nock_num++;
                //return;
            }
        }

	}


    //-------------------------------------

    //  ノック回数
    //  引数nock_numはノックした回数

    //-------------------------------------
    void Nock_Num(int nock_)
    {
        if(nock_ == nock_num)
        {
            nock_num++;
        }

        //if(nock_ > 1)
        //{
            //nock_num = (int)Nock_Turn.NOCK_FALSE;
        //}
    }
}
