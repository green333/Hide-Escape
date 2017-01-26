using UnityEngine;
using System.Collections;


//仮　
public class Hide_Data : MonoBehaviour
{

    public Vector3 HidePoint;

    // public bool isAnimation = false;

    private Animator _animator = null;



    // Use this for initialization
    void Start()
    {
        HidePoint = transform.position;
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("IS_OPEN", false);
        _animator.SetBool("IS_CLOSE", false);
        //   gameObject.GetComponent<BoxCollider>().contactOffset=100f;
        float hoge = gameObject.GetComponent<BoxCollider>().contactOffset;
        Debug.Log("オフセット値" + hoge);

        Hide_Point_Genalate();
    }



    // Update is called once per frame
    void Update()
    {


    }

    public enum OPEN_AND_SHOUT_STATE
    {
        ERROR = -1,


        OPEN = 0,
        CLOSE

    };
    //現在の状態
    public OPEN_AND_SHOUT_STATE oass;


    public void Open_or_Close()
    {
        switch (oass)
        {

            case OPEN_AND_SHOUT_STATE.OPEN:
                _animator.SetBool("IS_OPEN", true);
                _animator.SetBool("IS_CLOSE", false);
                oass = OPEN_AND_SHOUT_STATE.CLOSE;
                break;
            case OPEN_AND_SHOUT_STATE.CLOSE:
                _animator.SetBool("IS_OPEN", false);
                _animator.SetBool("IS_CLOSE", true);
                oass = OPEN_AND_SHOUT_STATE.OPEN;

                break;
            default:
                break;
        }
    }


    public void Disable_Collider()
    {

        transform.GetComponent<BoxCollider>().enabled = false;


    }
    public void Activate_Collider()
    {
        transform.GetComponent<BoxCollider>().enabled = true;

    }
    public bool Cheak(Vector3 vec)
    {

        Vector3 front = transform.forward;
        float front_length = front.magnitude;
        float Vec_length = vec.magnitude;

        float cos = Mathf.Acos(Vector3.Dot(vec, front) / (front_length * Vec_length));
        cos = cos * 180.0f / Mathf.PI;
        Debug.Log("cos:" + cos);
        if (cos > 130)
            return true;

        return false;
    }

    public Vector3 foo;
    private void Hide_Point_Genalate() {

        HidePoint = transform.position;

         foo=transform.forward*0.3f;
         HidePoint -= foo;
    }



}
