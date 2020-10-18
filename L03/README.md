# 当り判定
<div style="text-align: right;">
更新 2020年10月18日<br>
株式会社アルファオメガ  松本清明
</div>


# 検証プログラム環境準備
当り判定のプログラムを確認する環境を作りましょう。
円形のスプライトを２つ用意して２つの間に線を書きます。円同士が重なっていると赤い線に変更します。

<img src="./img/スクリーンショット 2020-10-17 110402.png" style="border:1px solid;">

## サンプルスクリプト
今回使用する２つのスクリプトを用意してください。

### 円形クラス
```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCircle : MonoBehaviour
{
    [SerializeField] private float radius = 1;

    public bool HitTest(ShapeCircle other)
    {
        // ここの実装をして下さい。
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
```


### 円形と円形を監視するスクリプト
```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour
{
    [SerializeField] private ShapeCircle shape1 = default;
    [SerializeField] private ShapeCircle shape2 = default;

    private void OnDrawGizmos()
    {
        Gizmos.color = shape1.HitTest(shape2) ? Color.red : Color.white;
        Gizmos.DrawLine(shape1.transform.position, shape2.transform.position);
    }
}
```

## Unity Hubで新規プロジェクトを作成
1. Unityのバージョンは なんでも良いです。新規にプロジェクトを用意して下さい。
1. Sceneの2Dというボタンを押しておくと見やすいと思います。
<img src="./img/スクリーンショット 2020-10-18 115618.png" style="border:1px solid;">

## Circle Sprite Aを作成します
1. Unityのメニューで[GameObject]-[2D Object]-[Sprite]で作成します。
1. GameObjectの名前を Circle Sprite Aとします。
1. Spriteの形状をKnobに変更します。
1. 色も適当に変えておきます。
1. ShapeCircle スクリプトを割り当てます。
1. TransformのScaleを 10,10,1に変更します。

## Circle Sprite Bを作成します
1. Unityのメニューで[GameObject]-[2D Object]-[Sprite]で作成します。
1. GameObjectの名前を Circle Sprite Bとします。
1. Spriteの形状をKnobに変更します。
1. 色も適当に変えておきます。
1. TransformのScaleを 10,10,1に変更します。

## 円形と円形を監視するスクリプトをカメラに設定
1. カメラオブジェクトに fooクラスを割り当て下さい。
1. インスペクター shape1に circle Sprite Aを割り当てる
1. インスペクター shape2に circle Sprite Bを割り当てる

## 動作検証
Gizmoで描画しているので再生しなくともGameObjectを移動させると描画が更新されます。
この仕組みで辺り判定のアルゴリズム書いて確認するようにして下さい。

# その他の２D判定処理

## 矩形と矩形との当り判定

### 矩形クラス
```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRect : MonoBehaviour
{
    [SerializeField] private Vector2 size = Vector2.one;

    public bool HitTest(ShapeBox other)
    {
        // ここの実装をして下さい。
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

}
```

### 矩形と矩形と監視するスクリプト
```CSharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour
{
    [SerializeField] private ShapeRect rect1 = default;
    [SerializeField] private ShapeRect rect2 = default;

    private void OnDrawGizmos()
    {
        Gizmos.color = rect1.HitTest(rect2) ? Color.red : Color.white;
        Gizmos.DrawLine(rect1.transform.position, rect2.transform.position);
    }
}
```

## 直線と直線との当り判定
実装してみて下さい。

## カプセルとカプセルの当り判定
実装してみて下さい。


# ３D判定処理

## 球と球の当り判定
実装してみて下さい。

## AABBとAABBの当り判定
実装してみて下さい。

## カプセルとカプセルの当り判定
実装してみて下さい。

## 直線と直線との当り判定
実装してみて下さい。


# 別の種類の形状と形状の組み合わせの辺り判定について
ここまで来ると様々な形状との当り判定を考慮するにはどうしたら良いか管理に悩むかと思います。その場合に Shapeという基底クラスを作り ShapeCircleやShapeRectを派生させて管理します。その時にどのような実装にすれば良いでしょうか？


## 球(円)とAABB(矩形)との当り判定
実装してみて下さい。

## 球(円)とラインとの当り判定
実装してみて下さい。

## 球(円)とカプセルとの当り判定
実装してみて下さい。

# 最後に
頭から行う必要がなく実装出来そうな課題をやってみて下さい。
2020年10月31日(土曜日)までにやってみて下さい。
自分もそれまでに答えとして作っていきます。