using UnityEngine;
using System.Collections;

public class FlyOutHand : MonoBehaviour
{
    private const float ANGLE_SPEED = 5.0f;
    private const float POS_SPEED = 0.1f;

    private Vector3 angle;
    private Vector3 pos;
    private GameObject obj;

    public bool flyOutFlg = false;

    private void Start()
    {
        obj = transform.FindChild("hand").gameObject;
        angle = obj.transform.localEulerAngles;
        pos = obj.transform.position;
    }

    private void Update()
    {
        if (flyOutFlg) { FlyOut(); }
    }

    private void FlyOut()
    {
        if (angle.x >= 180)
        {
            obj.SetActive(false);
            return;
        }

        angle += new Vector3(ANGLE_SPEED, 0.0f, ANGLE_SPEED);
        pos -= new Vector3(0.0f, 0.0f, POS_SPEED);

        obj.transform.localEulerAngles = angle;
        obj.transform.position = pos;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            flyOutFlg = true;
        }
    }
}
