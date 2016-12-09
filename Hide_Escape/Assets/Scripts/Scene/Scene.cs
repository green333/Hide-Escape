using UnityEngine;
using System.Collections;

public abstract class Scene : MonoBehaviour
{

  
	// Use this for initialization	
  virtual public void Start () {
	
	}
	
	// Update is called once per frame
  virtual public void Update()
  {

	}


  //------------------------------------------------------------------------------------------------
  //   ステートを切り替えるメソッド
  //------------------------------------------------------------------------------------------------
  // 別のシーンに切り替えるメソッド   
  protected void ChangeScene( string nextSceneName )
  {
    // Null と空文字列の場合
    if( string.IsNullOrEmpty( nextSceneName ) ) return;

    Application.LoadLevel( nextSceneName );
  }
}
