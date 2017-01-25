using UnityEngine;
using System.Collections;

public class Clear_Ivent : MonoBehaviour {

    struct Door_Nock
    {
        int door_0;      //←
        int door_1;      //→
        int door_2;      //↑(正面)
    }


    public enum Nock_Turn   //ノックの順番
    {
        DOOR_0 = 1,
        DOOR_1,
        DOOR_2,         
        NOCK_FALSE,    //ノック失敗
    }

    static public int nock_num;   //ノックの回数

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

        if(nock_num == (int)Nock_Turn.DOOR_0)
        {
            return;
        }

        if(nock_num == (int)Nock_Turn.DOOR_1)
        {
            return;
        }

        if(nock_num == (int)Nock_Turn.DOOR_2)
        {
            return;
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
