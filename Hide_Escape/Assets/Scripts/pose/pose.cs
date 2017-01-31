using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pose : MonoBehaviour
{


    public bool POSE = false;

    public float time = 0;

   Image image=null;

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
        image = gameObject.GetComponent<Image>();
        if (!POSE)
        {
            image.enabled = true;
            Time.timeScale = 0;
            POSE = true;
        }
        else
        {
            image.enabled = false;
            Time.timeScale = 1;
            POSE = false;
        }
    }




}