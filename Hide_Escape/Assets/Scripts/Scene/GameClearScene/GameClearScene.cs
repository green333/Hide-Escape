using UnityEngine;
using System.Collections;

public class GameClearScene : Scene
{

  // シーン関係
  [SerializeField]
  private string _titleScene = "";   // 切り替え先のシーン

  // シーン関係のプロパティ
  private string titleScene_
  {
    get { return _titleScene; }
  }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
  public override void Update()
  {
    if( Input.GetButtonDown( "Botton_A" ) )
    {
      ChangeScene( titleScene_ );
    }		
	}
}
