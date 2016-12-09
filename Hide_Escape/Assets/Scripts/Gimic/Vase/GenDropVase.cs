using UnityEngine;
using System.Collections;

public class GenDropVase : MonoBehaviour
{
    private const float ROTSPEED = 0.5f;

    private GameObject VaseObj;
    private Rigidbody rig;

	private void Start ()
    {
        VaseObj =
        (GameObject)
        Instantiate(
        Resources.Load("vase"),
        gameObject.transform.position,
        Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f))
        );

        VaseObj.transform.localScale = gameObject.transform.localScale;

        rig = VaseObj.GetComponent<Rigidbody>();
        rig.useGravity = false;

        VaseObj.transform.parent = gameObject.transform;
	}

    private void Update ()
    {
        Dropping();
	}

    private void Dropping()
    {
        if (gameObject.transform.localEulerAngles.x >= 180.0f) { return; }

        gameObject.transform.localEulerAngles
            +=
            new Vector3(ROTSPEED, 0.0f, 0.0f);
        
        rig.useGravity = true;
    }
}
