using UnityEngine;
using System.Collections;

public class MapCamera : MonoBehaviour
{

    public Transform target;

    private const float height = 100f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Update());

    }

    // Update is called once per frame
    IEnumerator Update()
    {

        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.position = new Vector3(target.position.x, height, target.position.z);
            transform.eulerAngles = new Vector3(90f, 0f, -target.eulerAngles.y);
        }


    }
}