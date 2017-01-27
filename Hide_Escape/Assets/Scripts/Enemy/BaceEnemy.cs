using UnityEngine;

using System.Collections;



public class BaceEnemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private NavMeshAgent agent;

    public Vector3[] wp;  //アタッチした物体を徘徊させるためのpos

    [SerializeField]
    private GameObject[] obj=null; //徘徊用オブジェクト

    [SerializeField]
    private GameObject target=null; //プレイヤー

 
    [SerializeField]
    GameObject gameobject;

    public float rotMax; //回転量
    

    public int wpState = 0; //waitpointの配列数
    public int size;

    public float speed; //移動量

    [SerializeField]
    private float view_length=0; //視野距

    [SerializeField]
    private float view_angle = 0; //視野角

    public float WaitTime =0.0f; //待機時間

    float time = 0.0f;   //タイマー

    public float rot_z;

    public bool tracking_flag;

    public bool flag;

    [SerializeField]
    public P_Player player;
   
    enum State //行動分岐
    {
        Patrol,
        Tracking,
        Lost_Search
    };


    [SerializeField]
    private State state=State.Patrol; 

    void Start()
    {
        //徘徊用のゲームオブジェクトの座標をwaitpointの座標に入れる
        for (int i = 0; i <obj.Length ; i++)
        {
            wp[i] = obj[i].transform.position;
            wp[i].y = 0;
            transform.position = wp[i]; 
        }

        tracking_flag = false;
        animator.SetBool("IS_CHECK", false);
        flag = player.GetHIDE_NOW();
    }

    void FixedUpdate()
    {
      switch (state) {

        case State.Patrol: //徘徊

           animator.SetBool("IS_CHECK", false);
            Patrol();
                        
            break;

         case State.Tracking: //プレイヤーを見つけて追いかける

            Tracking();

            break;

          case State.Lost_Search: //プレイヤーを見失いその場で停止する
            LostSearch();
            animator.SetBool("IS_CHECK", true);
            break;

       }
        if(flag)
        {
            state = State.Lost_Search;
        }
    }


    public void Patrol()
    {
        agent.SetDestination(wp[wpState]);

        if (Vector3.Distance(transform.position, wp[wpState]) <= 0.5f)
        {
            wpState++;
            if (wpState >= size)
            {
                wpState = 0;
                
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wp[wpState] - transform.position),2f);
            transform.position += transform.forward * speed;
        }

        if (IsDiscoveryTarget())
        {
            state = State.Tracking;
        }

    }

    public bool IsDiscoveryTarget()
    {
        Vector3 target_Vec=target.transform.position-transform.position;
        float angle = Vector3.Angle(transform.forward, target_Vec);
        Debug.DrawRay(transform.position, target_Vec, Color.black, 0, false);

        if (angle <= view_angle)
        {

            if(target_Vec.magnitude<view_length)
            {
                Debug.DrawRay(transform.position, target_Vec, Color.blue, 0, false);

                return true;
            }
        }
        
        return false;
    }

  public void Tracking()
    {
        tracking_flag = false;
        agent.Resume();
        Vector3 new_target = target.transform.position;
        new_target.y = 0;
        transform.LookAt(new_target);

        agent.SetDestination(target.transform.position);
        if (IsDiscoveryTarget() != true)
        {
            state = State.Lost_Search;

        }

    }

    public void LostSearch()
    {
       
        time = time + Time.deltaTime;

        if (time>=WaitTime)
        {
            time = 0.0f;
            state = State.Patrol;
            tracking_flag = true;

        }
    }
}

