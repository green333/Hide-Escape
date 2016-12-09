using UnityEngine;
using System.Collections;

public class TiltHand : MonoBehaviour
{
    private const float SPEED = 10.0f;

    private Vector3 angle;
    private Rigidbody rigid;

    public bool tiltFlg = false;

    private void Start()
    {
        angle = transform.localEulerAngles;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (tiltFlg)
        {
            Tilt();
        }
    }

    private void Tilt()
    {
        if (angle.z >= 10) { return; }

        rigid.useGravity = true;
        angle += new Vector3(-SPEED, 0.0f, SPEED);
        gameObject.transform.localEulerAngles = angle;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            tiltFlg = true;
        }
    }
}
