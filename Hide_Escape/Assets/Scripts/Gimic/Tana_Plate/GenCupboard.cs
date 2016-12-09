using UnityEngine;
using System.Collections;

public class GenCupboard : MonoBehaviour
{
    private const float MOVESPEED = 0.02f;

    private GameObject ShelfObj = null;
    private GameObject PlateObj = null;
    private GameObject Poltergeist = null;

    private void Start()
    {
        GenerateShelf();
        GeneratePlate();
        GeneratePoltergeist();
    }

    private void Update()
    {
        //  皿落す
        DroppingPlate();
    }

    //-------------------------------------------------------------------
    //  @brief  皿落とすやつ生成
    //-------------------------------------------------------------------
    private void GeneratePoltergeist()
    {
        Poltergeist = (GameObject)Instantiate(Resources.Load("Poltergeist"));

        Poltergeist.transform.localScale
            = new Vector3(2.0f, 2.0f, 2.0f);

        Poltergeist.transform.position
            = gameObject.transform.position
            + gameObject.transform.forward;
            //+ new Vector3(0.0f, 1.0f, 2.0f);

        BoxCollider bcl = Poltergeist.GetComponent<BoxCollider>();
        bcl.size = Poltergeist.transform.localScale * 0.5f;
    }

    //-------------------------------------------------------------------
    //  @brief  皿落とす
    //-------------------------------------------------------------------
    private void DroppingPlate()
    {
        Poltergeist.transform.position -= new Vector3(0.0f, 0.0f, MOVESPEED);
    }

    //-------------------------------------------------------------------
    //  @brief  棚生成
    //-------------------------------------------------------------------
    private void GenerateShelf()
    {
        ShelfObj = (GameObject)
        Instantiate(
            Resources.Load("shelf"),
            gameObject.transform.position,
            Quaternion.Euler(gameObject.transform.localEulerAngles)
            );
        ShelfObj.transform.parent = gameObject.transform;
    }

    //-------------------------------------------------------------------
    //  @brief  皿生成
    //-------------------------------------------------------------------
    private void GeneratePlate()
    {
        Vector3[] list = new Vector3[]
        {
            ShelfObj.transform.position - new Vector3(0.8f, 0.0f, 0.6f),
            ShelfObj.transform.position - new Vector3(-0.8f, 0.0f, 0.6f),
            ShelfObj.transform.position - new Vector3(0.8f,  0.0f, -0.6f),
            ShelfObj.transform.position - new Vector3(-0.8f, 0.0f, -0.6f),
        };

        float UpValue = -1.0f;
        Vector3 Up = new Vector3(0.0f, 0.0f, 0.0f);

        for (int y = 0; y < 16; y++)
        {
            if (y == 11) { UpValue = 2.0f; }

            Up = new Vector3(0.0f, UpValue, 0.0f);

            for (int xz = 0; xz < list.Length; xz++)
            {
                PlateObj = (GameObject)
                    Instantiate(
                    Resources.Load("plate"),
                    list[xz] + Up,
                    Quaternion.identity);

                PlateObj.transform.parent = gameObject.transform;
            }

            UpValue += 0.2f;
        }
    }

}
