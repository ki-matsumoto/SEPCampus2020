<script async src="https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.0/MathJax.js?config=TeX-AMS_CHTML"></script>
<script type="text/x-mathjax-config">
 MathJax.Hub.Config({
 tex2jax: {
 inlineMath: [["\\(","\\)"] ],
 displayMath: [ ['$$','$$'], ["\\[","\\]"] ]
 }
 });
</script>

# 当り判定
<div style="text-align: right;">
2020年11月7日<br>
株式会社アルファオメガ  松本清明
</div>


# 検証プログラム環境
当り判定のプログラムを確認する環境を作りましょう。
円形のスプライトを２つ用意して２つの間に線を書きます。円同士が重なっていると赤い線に変更します。

<img src="./img/SS 2020-11-07 102444.png" style="border:1px solid;">


# 様々な形状同士の当り判定について
ここまで来ると様々な形状との当り判定を考慮するにはどうしたら良いか管理に悩むかと思います。その場合に Shapeという基底クラスを作り ShapeCircleやShapeRectを派生させて管理します。その時にどのような実装にすれば良いでしょうか？

前回行った円同士などの同じ形状の当り判定だけではなく以下のような様々な
組み合わせを対応する必要がある。例えば円と長方形や円とカプセルなどです。

||円|長方形|カプセル|
|:--:|:--:|:--:|:--:|
|**円**|〇|〇|〇|
|**長方形**|〇|〇|✖|
|**カプセル**|〇|✖|✖|

今回は実際のコードはこのような対応表の当り判定を作りました。
〇は今回サンプルまで作成してあります。バツは時間切れとなりまして完成できませんでした。

shapeAとshapeBというそれぞれの形状のインスタンスがあるとして以下のような感じで２つが当たっているかを判定できるようにします。

```CSharp
bool hit = shapesA.HitTest(shapesB);
```

##  基底クラスのShapeクラスを作成

### Shapeは abstract クラス宣言する
キーワードを使用して抽象クラスや関数を宣言する

### abstract 関数を用意する
abstract関数宣言すると実装は行わず派生先のクラスの関数で実装するようにします。

### override で関数を実装する
overrideは派生先のクラスの関数の実装内容を変更することができます。

### 関数名が一緒で引数が違う
同じ関数名ですが、引数が違う場合にそれぞれの関数を呼び出せる機能を関数のオーバーロードといいます。これを利用して適切な形状同士の当り判定のコードをそれぞれに記述します。

###  ダブルディスパッチの構造で実装する

ダブルディスパッチとは引数だったクラスのインスタンスのメンバー関数を呼び直します。その時 thisを渡すのですが、実際のクラスが判定できているので 関数のオーバーロードの機能を使って 形状同士の組み合わせがわかることになります。

```CSharp
    override public bool HitTest(Shape other)
    {
        　//このように ひっくり返す
        return other.HitTest(this);
    }
```


## クラス図
このようなクラス構造になります。

<img src="./img/SS 2020-11-07 101745.png" style="border:1px solid;">


### 形状クラス
```CSharp
abstract class Shape : MonoBehaviour
{
    abstract public bool HitTest(Shape other);
    abstract public bool HitTest(ShapeCircle other);
    abstract public bool HitTest(ShapeBox other);
    abstract public bool HitTest(ShapeCapsule other);
}
```

### 円形のクラス
```CSharp
class ShapeCircle : Shape
{
    override public bool HitTest(Shape other);
    override public bool HitTest(ShapeCircle other);
    override public bool HitTest(ShapeBox other);
    override public bool HitTest(ShapeCapsule other);
}
```
### 矩形のクラス
```CSharp
class ShapeBox : Shape
{
    override public bool HitTest(Shape other);
    override public bool HitTest(ShapeCircle other);
    override public bool HitTest(ShapeBox other);
    override public bool HitTest(ShapeCapsule other);
}
```

### カプセルのクラス
```CSharp
class ShapeCapsule : Shape
{
    override public bool HitTest(Shape other);
    override public bool HitTest(ShapeCircle other);
    override public bool HitTest(ShapeBox other);
    override public bool HitTest(ShapeCapsule other);
}
```


* [Shape.cs](./Assets/Shape.cs)のコード
* [ShapeCircle.cs](./Assets/ShapeCircle.cs)のコード
* [ShapeBox.cs](./Assets/ShapeBox.cs)のコード
* [ShapeCapsule.cs](./Assets/ShapeCapsule.cs)のコード


# 円と矩形との辺り判定

判定処理は以下の感じになりました。

```CSharp
    override public bool HitTest(ShapeBox other)
    {
        Vector2 hsize = other.size * 0.5f;

        float nx = Mathf.Abs(transform.position.x - other.transform.position.x);
        float ny = Mathf.Abs(transform.position.y - other.transform.position.y);
        Vector2 n = new Vector2(nx, ny);

        if (nx < hsize.x) return ny < (radius + hsize.y);
        if (ny < hsize.y) return nx < (radius + hsize.x);

        Vector2 d = n - hsize;
        float len = Mathf.Sqrt(Vector2.Dot(d, d));
        return len <= radius;
    }
```

# 円とカプセルとの辺り判定

線分と点との距離を求める計算の応用となります。

```CSharp
    override public bool HitTest(ShapeCapsule other)
    {
        var mat = Matrix4x4.Rotate(other.transform.rotation);
        var h = mat.MultiplyVector(new Vector3(other.length * 0.5f, 0, 0));
        // 2点の座標
        Vector3 p1 = other.transform.position - h;
        Vector3 p2 = other.transform.position + h;

        var ab = p2 - p1;
        var ac = new Vector3(transform.position.x, transform.position.y) - p1;

        var t = Vector2.Dot(ab, ac);
        Vector2 c;
        if (t <= 0)
        {
            c = p1;
        }
        else
        {
            float denom = Vector2.Dot(ab, ab);
            if (t >= denom)
            {
                c = p2;
            }
            else
            {
                t /= denom;
                c = p1 + ab * t;
            }
        }
        var tmp = c - new Vector2(transform.position.x, transform.position.y);
        return Mathf.Sqrt(Vector2.Dot(tmp, tmp)) <= radius + other.radius;
    }
```

# 高速化テクニック

円と円との当り判定は

お互いの半径同士を足したものとお互いの距離が小さかった場合に当たっていると判定できます。そこでお互いの距離を求める計算式は 以下の様になると思います。

$$ length = \sqrt{x^2+y^2} $$

現代のコンピューターでもルートの計算コストは高いです。
今回の計算はお互いの距離を求めたいのではなく、当たっているかを判定したいだけです。
そこでお互いに2乗をするルートをなくすことが出来るので以下のコードに計算します。

```CSharp
    override public bool HitTest(ShapeCircle other)
    {
        // 距離を求める
        float nx = transform.position.x - other.transform.position.x;
        float ny = transform.position.y - other.transform.position.y;
        Vector2 n = new Vector2(nx, ny);

        var r = radius + other.radius;
        return Vector2.Dot(n, n) < r*r;
    }
```

# 最後に
今回の課題は基底クラス、派生クラス、関数のオーバーロード、ダブルディスパッチ、割り算やMathf.Sqrt()を極力避けるなどがポイントだったかと思います。
いかがでしたでしょうか？