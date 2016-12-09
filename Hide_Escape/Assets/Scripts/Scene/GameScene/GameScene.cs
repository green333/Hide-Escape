using UnityEngine;
using System.Collections;


public class GameScene : Scene
{

  // シーン関係
  [SerializeField]
  private string _nextStageScene = "";      // 次のステージのシーン名
  [SerializeField]
  private string _previousStageScene = "";  // 前のステージのシーン名

  [SerializeField]
  private string _gameOverScene = "";       // ゲームオーバーのシーン名
  [SerializeField]
  private string _gameClearScene = "";      // ゲームオーバーのシーン名


  // プレイヤー関係
  [SerializeField]
  private P_Player _player = null;      // プレイヤー


  protected delegate bool hoge();

  // シーン関係のプロパティ
  private string gameClearScene_
  {
    get { return _gameClearScene; }
  }

  private string gameOverScene_
  {
    get { return _gameOverScene; }
  }

  private string nextStageScene_
  {
    get { return _nextStageScene; }
  }

  private string previousStageScene_
  {
    get { return _previousStageScene; }
  }

  // プレイヤー関係のプロパティ
  private P_Player player_
  {
    get { return _player; }
  }


	// Update is called once per frame
	public override void Update () 
  {

    // ゲームオーバー画面に切り替えれる場合
    if( CanChangeGameOverScene() )
    {
      ChangeScene( gameOverScene_ );
      return;
    }

    // 次のステージ画面に切り替えれる場合
    if( CanChangeNextStageScene() )
    {
      ChangeScene( nextStageScene_ );
    }

    // 前のステージ画面に切り替えれる場合
    if( CanChangePreviousStageScene() )
    {
      ChangeScene( previousStageScene_ );
    }


    // ゲームクリア画面に切り替えれる場合
    if( CanChangeGameClearScene() )
    {
      ChangeScene( gameClearScene_ );
    }

	}


  //------------------------------------------------------------------------------------------------
  //   次のステージ画面に切り替えれるか確認するメソッド
  //    ~ 戻り値 ~
  //        true  : 次のステージ画面に切り替えれるとき
  //        false : 次のステージ画面に切り替えれないとき
  //------------------------------------------------------------------------------------------------
  private bool CanChangeNextStageScene()
  {
    if( NotHasPlayer() ) return false;

    return player_.IS_NEXTSTAGE;
  }

  //------------------------------------------------------------------------------------------------
  //   前のステージ画面に切り替えれるか確認するメソッド
  //    ~ 戻り値 ~
  //        true  : 前のステージ画面に切り替えれるとき
  //        false : 前のステージ画面に切り替えれないとき
  //------------------------------------------------------------------------------------------------
  private bool CanChangePreviousStageScene()
  {
    if( NotHasPlayer() ) return false;

    return player_.IS_PREVSTAGE;
  }

  //------------------------------------------------------------------------------------------------
  //   ゲームオーバー画面に切り替えれるか確認するメソッド
  //    ~ 戻り値 ~
  //        true  : ゲームオーバー画面に切り替えれるとき
  //        false : ゲームオーバー画面に切り替えれないとき
  //------------------------------------------------------------------------------------------------
  private bool CanChangeGameOverScene()
  {
    if( NotHasPlayer() ) return false;

    return player_.IS_GAMEOVER;
  }

  //------------------------------------------------------------------------------------------------
  //   ゲームクリア画面に切り替えれるか確認するメソッド
  //    ~ 戻り値 ~
  //        true  : ゲームクリア画面に切り替えれるとき
  //        false : ゲームクリア画面に切り替えれないとき
  //------------------------------------------------------------------------------------------------
  private bool CanChangeGameClearScene()
  {
    if( NotHasPlayer() ) return false;

   
    return player_.IS_GAMECLEAR;
  }




  //------------------------------------------------------------------------------------------------
  //   プレイヤーを持っていないかどうか確認するメソッド
  //    ~ 戻り値 ~
  //        true  : 持っていないとき
  //        false : 持っているとき
  //------------------------------------------------------------------------------------------------
  private bool NotHasPlayer()
  {
    return ( player_ == null );
  }
}
