using UnityEngine;
using System.Collections;

public class pose : MonoBehaviour
{


    public bool POSE = false;

    public float time = 0;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = Time.timeScale;

        if (Input.GetButtonDown("Start Botton"))
        {

            Pose_ChengeProsess();

        }

    }

    private void Pose_ChengeProsess()
    {
        if (!POSE)
        {
            Time.timeScale = 0;
            POSE = true;
        }
        else
        {
            Time.timeScale = 1;
            POSE = false;
        }
    }




}