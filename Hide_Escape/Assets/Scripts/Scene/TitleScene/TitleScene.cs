using UnityEngine;
using System.Collections;

public class TitleScene : Scene 
{

  // シーン関係
  [SerializeField]
  private string _gameScene = "";   // 切り替え先のシーン

  // シーン関係のプロパティ
  protected string gameScene_
  {
    get { return _gameScene; }
  }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
  {
    if( Input.GetButtonDown( "Botton_A" ) )
    {
      ChangeScene( gameScene_ );
    }		
	}
}
