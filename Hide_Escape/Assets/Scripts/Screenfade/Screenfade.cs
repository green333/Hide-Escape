using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Screenfade : MonoBehaviour
{

  // フェード処理の定数クラス
  public static class FadeType
  {

    public enum Type
    {
      OUT = 0,          // フェードアウト
      IN,               // フェードイン
      MAX_FADE_TYPE,    // 最大種類数
    };
  }


  // フェードカラーの定数
  public const int COLOR_VALUE_MAX = 255;		// 色を表す最大数値
  public const int COLOR_MAX = 3;		        // 使用色数


  // フェード処理関連			
  [SerializeField]
  private FadeType.Type _type = FadeType.Type.IN;			  // モード
  [SerializeField]
  private int _param = 1;								                // モード用カウントパラメータ
  private int _count = 0;								                // モード用カウンタ

  // フェード処理で使う色関連
  [SerializeField]
  private bool _colorFlag = false;							               // 色使用フラグ ( true : カラー、 false : モノクロ )		
  [SerializeField]
  private Color _color = new Color( 0.0f, 0.0f, 0.0f, 0.0f );  // フェード処理に使う色

  // フェード処理関連のプロパティ
  private FadeType.Type type_
  {
    set { _type = value; }
    get { return _type; }
  }

  private int count_
  {
    set { _count = value; }
    get { return _count; }
  }

  private int param_
  {
    set { _param = value; }
    get { return _param; }
  }



  // フェード処理で使う色関連のプロパティ
  private bool colorFlag_
  {
    set { _colorFlag = value; }
    get { return _colorFlag; }
  }

  private Color color_
  {
    set { _color = value; }
    get { return _color; }
  }


  // Use this for initialization
  void Start()
  {
    SetFade( type_ );
  }

  // Update is called once per frame
  void Update()
  {
    // フェードの動作の種類に応じて、不透明度をそれぞれ更新
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ -= param_;
        if( count_ <= 0 ) { count_ = 0; }

        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ += param_;
        if( count_ >= 255 ) { count_ = 255; }

        break;

      // 不正値
      default:

        return;
    }


    // カラーを使用する場合
    if( IsColor() ) ChangeColor( color_.r, color_.g, color_.b, ToColorRange( count_ ) );
    // クロの場合
    else ChangeColor( 0.0f, 0.0f, 0.0f, ToColorRange( count_ ) );
  }



  //------------------------------------------------------------------------------------------------
  //   フェード画像を表示するメソッド
  //------------------------------------------------------------------------------------------------
  public void Visible()
  {
    Visible( gameObject.GetComponent<SpriteRenderer>() );
    Visible( gameObject.GetComponent<Image>() );
  }

  // 指定したスプライトを表示するメソッド
  private void Visible( SpriteRenderer visibleSpriteRenderer )
  {
    if( visibleSpriteRenderer == null ) return;

    visibleSpriteRenderer.enabled = true;
  }

  // 指定した Image を表示するメソッド
  private void Visible( Image visibleImage )
  {
    if( visibleImage == null ) return;

    visibleImage.enabled = true;
  }


  //------------------------------------------------------------------------------------------------
  //   フェード画像を非表示にするメソッド
  //------------------------------------------------------------------------------------------------
  public void Invisible()
  {
    Invisible( gameObject.GetComponent<SpriteRenderer>() );
    Invisible( gameObject.GetComponent<Image>() );
  }

  // 指定したスプライトを非表示するメソッド
  private void Invisible( SpriteRenderer visibleSpriteRenderer )
  {
    if( visibleSpriteRenderer == null ) return;

    visibleSpriteRenderer.enabled = false;
  }

  // 指定した Image を非表示するメソッド
  private void Invisible( Image visibleImage )
  {
    if( visibleImage == null ) return;

    visibleImage.enabled = false;
  }


  //------------------------------------------------------------------------------------------------
  //   色を変更するメソッド
  //------------------------------------------------------------------------------------------------
  private void ChangeColor( float r, float g, float b, float a )
  {
    ChangeColor( gameObject.GetComponent<SpriteRenderer>(), r, g, b, a );
    ChangeColor( gameObject.GetComponent<Image>(), r, g, b, a );
  }

  // 指定したスプライトのポリゴンの色を変更するメソッド
  private void ChangeColor( SpriteRenderer changeSpriteRenderer, float r, float g, float b, float a )
  {
    if( changeSpriteRenderer == null ) return;

    changeSpriteRenderer.color = new Color( r, g, b, a );
  }

  // 指定した UI の Image のポリゴンの色を変更するメソッド
  private void ChangeColor( Image changeImage, float r, float g, float b, float a )
  {
    if( changeImage == null ) return;

    changeImage.color = new Color( r, g, b, a );
  }


  //------------------------------------------------------------------------------------------------
  //   フェードの設定するメソッド 
  //------------------------------------------------------------------------------------------------
  // 通常指定 ver
  public void Set( FadeType.Type type, int speed )
  {
    // 各パラメータ設定
    type_ = type;			   // フェードの種類
    param_ = speed;			 // フェードの速度
    colorFlag_ = false;   // クロ


    // フェードの動作の種類に応じて、不透明度をそれぞれ設定
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }
  }
  // color 整数値指定 ver
  public void SetFade( FadeType.Type type, int speed, byte r, byte g, byte b )
  {
    // 各パラメータ設定
    type_ = type;			  // フェードの種類
    param_ = speed;			// フェードの速度
    colorFlag_ = true;  // カラー

    // カラー設定
    Color color = color_;

    color.r = ToColorRange( r );
    color.g = ToColorRange( g );
    color.b = ToColorRange( b );

    _color = color;

    //  フェードの動作の種類に応じて、不透明度を設定
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }

  }
  // color パーセント指定 ver
  public void SetFade( FadeType.Type type, int speed, float r, float g, float b )
  {
    // 各パラメータ設定
    type_ = type;			  // フェードの種類
    param_ = speed;			// フェードの速度
    colorFlag_ = true;  // カラー

    // カラー設定
    Color color = color_;

    color.r = ToColorRange( r );
    color.g = ToColorRange( g );
    color.b = ToColorRange( b );

    _color = color;

    // フェードの動作の種類に応じて、不透明度を設定
    switch( type )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }

  }


  //------------------------------------------------------------------------------------------------
  //   フェードの設定するメソッド (インスペクター上で値を変更した場合に使うメソッド)
  //------------------------------------------------------------------------------------------------
  // フェード動作 (フェードインかフェードアウト) のみ ver
  public void SetFade( FadeType.Type type )
  {
    // 各パラメータ設定
    type_ = type;			    // フェードの種類

    // フェードの動作の種類に応じて、不透明度をそれぞれ設定
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }
  }
  // フェード動作とフェード速度を設定する
  public void SetFade( FadeType.Type type, int speed )
  {
    // 各パラメータ設定
    type_ = type;			   // フェードの種類
    param_ = speed;			 // フェードの速度


    // フェードの動作の種類に応じて、不透明度をそれぞれ設定
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }
  }
  // フェード動作とフェード速度と色の使用を設定する
  public void SetFade( FadeType.Type type, int speed, bool colorFlag )
  {
    // 各パラメータ設定
    type_ = type;			        // フェードの種類
    param_ = speed;			      // フェードの速度
    colorFlag_ = colorFlag;   // カラーフラグ


    // フェードの動作の種類に応じて、不透明度をそれぞれ設定
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        count_ = 255;
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        count_ = 0;
        break;

      // 不正値
      default: break;
    }
  }




  //------------------------------------------------------------------------------------------------
  //   指定した値を 0.0f ～ 1.0f の範囲に変換するメソッド 
  //------------------------------------------------------------------------------------------------
  private float ToColorRange( float color )
  {
    float ref_color = color;

    ref_color = Math.Max( 0.0f, ref_color );
    ref_color = Math.Min( ref_color, 1.0f );

    return ref_color;
  }

  private float ToColorRange( int color )
  {
    float ref_color = color;

    ref_color /= COLOR_VALUE_MAX;

    return ToColorRange( ref_color );
  }


  //------------------------------------------------------------------------------------------------
  //   フェード処理にカラーを使うのかを返すメソッド
  //    ~ 戻り値 ~
  //        true  : カラーを使用する
  //        false : カラーを使用しない
  //------------------------------------------------------------------------------------------------
  private bool IsColor()
  {
    return ( colorFlag_ == true );
  }

  //------------------------------------------------------------------------------------------------
  //   フェード処理終了したかどうかを返すメソッド
  //    ~ 戻り値 ~
  //        true  : フェード処理が終了したとき
  //        false : フェード処理が終了していないとき
  //------------------------------------------------------------------------------------------------
  public bool IsEnd()
  {
    // フェードの動作の種類に応じて、それぞれの判定を行う
    switch( type_ )
    {
      // フェードイン
      case FadeType.Type.IN:
        if( count_ <= 0 ) { return true; }
        break;

      // フェードアウト
      case FadeType.Type.OUT:
        if( count_ >= 255 ) { return true; }
        break;

      default: break;
    }

    return false;
  }
}
