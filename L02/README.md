# MonoBehaviour について
<div style="text-align: right;">
2020年10月3日<br>
株式会社アルファオメガ  松本清明
</div>

# はじめに
UnityのMonoBehaviourについて考えてみようと思います。

# MonoBehaviourってなに？

Unityでプログラムをする時はMonoBehaviourから継承したスクリプトに記述して GameObjectにアタッチして使用するということです。


## クラス継承
MonoBehaviourのクラス定義を見るためにプロジェクトを用意してみましょう。

### Unityで新規プロジェクトを作ります
1. UnityHubで新規作成を選びUnityのバージョンを選びます。バージョンは現在使用している最新版で問題ないです。
1. フォルダーと名前を決めて作成を押してください。
<img src="./img/スクリーンショット 2020-10-03 095751.png" style="border:1px solid;">
1. Unityのメニューの[Asset]-[Create]-[C# Script]でスクリプトを作ります。ここでは Fooにしましょう。
1. Fooスクリプトをダブルクリックして Visual Stduio 2019を起動して下さい。
1. Visual Stdio 2019のソースコード上で MonoBehaviour にカーソルを合わせてマウス右メニューを開き[定義へ移動]を選択してください。もちろんF12キーでも開くことができます。
<img src="./img/スクリーンショット 2020-10-03 085600.png" style="border:1px solid;">

### MonoBehaviour クラス
```CSharp
using System;
using System.Collections;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
    public class Behaviour : Component
    {
        public Behaviour();
        public bool enabled { get; set; }
        public bool isActiveAndEnabled { get; }
    }
    public class MonoBehaviour : Behaviour
    {
        public MonoBehaviour();
        public bool useGUILayout { get; set; }
        public bool runInEditMode { get; set; }
        public static void print(object message);
        public void CancelInvoke(string methodName);
        public void CancelInvoke();
        public void Invoke(string methodName, float time);
        public void InvokeRepeating(string methodName, float time, float repeatRate);
        public bool IsInvoking(string methodName);
        public bool IsInvoking();
        public Coroutine StartCoroutine(string methodName);
        public Coroutine StartCoroutine(IEnumerator routine);
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);
        public Coroutine StartCoroutine_Auto(IEnumerator routine);
        public void StopAllCoroutines();
        public void StopCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine routine);
        public void StopCoroutine(string methodName);
    }
}
```
## Behaviourの機能

### スクリプトの有効／無効
Behaviour.enbled でスクリプトを動作する／しないを決めることができます。

### StartCoroutine / StopCoroutine コルーチン制御
C/C++言語仕様にないが他の言語ではコルーチンという仕組みが実装されている言語が有ります。C#も実装されていてUnityから使用することができる様になっています。
簡単に説明するとプログラムは基本的には頭から最後までCPUが全速力で実行しますが、何か待たないといけない時に中断できる機能が備わっています。

* [疑似マルチタスク](https://kotobank.jp/word/%E6%93%AC%E4%BC%BC%E3%83%9E%E3%83%AB%E3%83%81%E3%82%BF%E3%82%B9%E3%82%AF-2635)
* [タスクシステム](https://qiita.com/Serbonis/items/0a9d890bee1748f889fa)

### ログ出力のprint関数

#### MonoBehaviour.print() で出力する方法
MonoBehaviourのメンバー関数にあったので説明しますが print関数でログに出力することができます。

public static void MonoBehaviour.print (object message);
##### 例
```C#
print( "ログ出力" );
```
[MonoBehaviour-print - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/current/ScriptReference/MonoBehaviour-print.html)

#### Debug.Log() で出力する方法
前から存在するログの出力方法はこちらで出力するのが一般的かもしれません。
```C#
Debug.Log("ログ出力" );
```

[Debug-Log - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/current/ScriptReference/Debug.Log.html)




# GameObjectってなに？
GameObjectは Unityで扱う最小単位のオブジェクトです。Unity2018からはEntity Component System(ECS) が出来たので最小というのが出来たのですがそれは省略しますが、
今もUnityのワールド空間上に置く最小の点を表すのです。

DCCツール(Maya,SoftImage,3DStduioMax etc)を使用したことがあればヒエラルキーがあると思いますが、それのイメージと同じなのかと思います。その他のオーサリングソフト例えばフォトショップではレイヤーでしょうか？

## クラス継承
```CSharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
    public sealed class GameObject : Object
    {
        public GameObject();
        public GameObject(string name);
        public GameObject(string name, params Type[] components);
        public Component particleSystem { get; }
        public Transform transform { get; }
        public int layer { get; set; }
        public bool active { get; set; }
        public bool activeSelf { get; }
        public bool activeInHierarchy { get; }
        public bool isStatic { get; set; }
        public string tag { get; set; }
        public Scene scene { get; }
        public ulong sceneCullingMask { get; }
        public GameObject gameObject { get; }
        public Component rigidbody2D { get; }
        public Component camera { get; }
        public Component light { get; }
        public Component animation { get; }
        public Component constantForce { get; }
        public Component renderer { get; }
        public Component audio { get; }
        public Component networkView { get; }
        public Component collider { get; }
        public Component collider2D { get; }
        public Component rigidbody { get; }
        public Component hingeJoint { get; }
        public static GameObject CreatePrimitive(PrimitiveType type);
        public static GameObject Find(string name);
        public static GameObject[] FindGameObjectsWithTag(string tag);
        public static GameObject FindGameObjectWithTag(string tag);
        public static GameObject FindWithTag(string tag);
        public Component AddComponent(string className);
        public Component AddComponent(Type componentType);
        public T AddComponent<T>() where T : Component;
        public void BroadcastMessage(string methodName);
        public void BroadcastMessage(string methodName, object parameter);
        public void BroadcastMessage(string methodName, [Internal.DefaultValue("null")] object parameter, [Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
        public void BroadcastMessage(string methodName, SendMessageOptions options);
        public bool CompareTag(string tag);
        public Component GetComponent(string type);
        public T GetComponent<T>();
        public Component GetComponent(Type type);
        public T GetComponentInChildren<T>();
        public T GetComponentInChildren<T>([Internal.DefaultValue("false")] bool includeInactive);
        public Component GetComponentInChildren(Type type);
        public Component GetComponentInParent(Type type, bool includeInactive);
        public T GetComponentInParent<T>();
        public T GetComponentInParent<T>([Internal.DefaultValue("false")] bool includeInactive);
        public void GetComponents<T>(List<T> results);
        public Component[] GetComponents(Type type);
        public T[] GetComponents<T>();
        public void GetComponents(Type type, List<Component> results);
        public void GetComponentsInChildren<T>(List<T> results);
        public T[] GetComponentsInChildren<T>();
        public void GetComponentsInChildren<T>(bool includeInactive, List<T> results);
        public T[] GetComponentsInChildren<T>(bool includeInactive);
        public Component[] GetComponentsInChildren(Type type, [Internal.DefaultValue("false")] bool includeInactive);
        public Component[] GetComponentsInChildren(Type type);
        public T[] GetComponentsInParent<T>();
        public void GetComponentsInParent<T>(bool includeInactive, List<T> results);
        public T[] GetComponentsInParent<T>(bool includeInactive);
        public Component[] GetComponentsInParent(Type type, [Internal.DefaultValue("false")] bool includeInactive);
        public Component[] GetComponentsInParent(Type type);
        public void PlayAnimation(Object animation);
        public void SampleAnimation(Object clip, float time);
        public void SendMessage(string methodName);
        public void SendMessage(string methodName, object value);
        public void SendMessage(string methodName, [Internal.DefaultValue("null")] object value, [Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
        public void SendMessage(string methodName, SendMessageOptions options);
        public void SendMessageUpwards(string methodName);
        public void SendMessageUpwards(string methodName, object value);
        public void SendMessageUpwards(string methodName, [Internal.DefaultValue("null")] object value, [Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
        public void SendMessageUpwards(string methodName, SendMessageOptions options);
        public void SetActive(bool value);
        public void SetActiveRecursively(bool state);
        public void StopAnimation();
        public bool TryGetComponent(Type type, out Component component);
        public bool TryGetComponent<T>(out T component);
    }
}
```

## GameObjectの機能

### コンポーネントの管理
自分(GameObject)にくっついているコンポーネントは何がついているかを管理しています。コンポーネントの追加や削除を行います。

GameObjectがどんな機能を追加するのはC#言語の継承を使うのではなくコンポーネントというソフトウェア工学の手法をお元に機能拡張を行っている。


### メッセージ送信
ゲームオブジェクトから別のゲームオブジェクトにメッセージを送ることができます。メッセージを送るとは何でしょうか？オブジェクト指向の用語としてメッセージパッシングとう言葉があります。通常のプログラムは関数として別の関数を呼び出して巨大なプログラムを構築するのですが、直接関数で呼び出せない処理も存在します。UnityにおいてSendMessageは頻繁に使うものではないのですが、OSレベルの通知をUnityのゲームオブジェクトにくっついているスクリプトに渡すときに使用します。
アプリ内のWebViewでバナーをタップしたらゲーム中のショップに遷移したとかは WebViewはOSの機能なのですが、ゲーム中のMonoBehaviour から継承したスクリプトの関数を呼び出すことができます。

Unityとは違うのですがWindows,iOS,AndroidなどのOSレベルでは以下のこともあります。

* 同じOSで動いている別のアプリに情報を送りたい。
* 別の場所にあるコンピューターから情報を受け取る。

OSが「シャットダウンしたいのですが、落としていいですか？」とアプリに連絡をします。そこで受け取ったら書くアプリは「いや待てよ作業中でセーブしないといけないからちょっと待っててね」としてシャットダウンしても問題ないようにアプリを準備します。準備が終わったら「OK問題」ないよと連絡します。まあ実際はこんな感じのフランクな会話ではないですが、OSの決められたルールでアプリ間通信を行っているのです。

# Componentってなに？

### Component クラス
```CSharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
    public class Component : Object
    {
        public Component();
        public Component particleSystem { get; }
        public Transform transform { get; }
        public GameObject gameObject { get; }
        public string tag { get; set; }
        public Component rigidbody { get; }
        public Component hingeJoint { get; }
        public Component camera { get; }
        public Component rigidbody2D { get; }
        public Component animation { get; }
        public Component constantForce { get; }
        public Component renderer { get; }
        public Component audio { get; }
        public Component networkView { get; }
        public Component collider { get; }
        public Component collider2D { get; }
        public Component light { get; }
        public void BroadcastMessage(string methodName, SendMessageOptions options);
        public void BroadcastMessage(string methodName);
        public void BroadcastMessage(string methodName, object parameter);
        public void BroadcastMessage(string methodName, [Internal.DefaultValue("null")] object parameter, [Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
        public bool CompareTag(string tag);
        public Component GetComponent(Type type);
        public T GetComponent<T>();
        public Component GetComponent(string type);
        public Component GetComponentInChildren(Type t, bool includeInactive);
        public Component GetComponentInChildren(Type t);
        public T GetComponentInChildren<T>([Internal.DefaultValue("false")] bool includeInactive);
        public T GetComponentInChildren<T>();
        public Component GetComponentInParent(Type t);
        public T GetComponentInParent<T>();
        public Component[] GetComponents(Type type);
        public void GetComponents(Type type, List<Component> results);
        public void GetComponents<T>(List<T> results);
        public T[] GetComponents<T>();
        public Component[] GetComponentsInChildren(Type t);
        public void GetComponentsInChildren<T>(List<T> results);
        public T[] GetComponentsInChildren<T>();
        public void GetComponentsInChildren<T>(bool includeInactive, List<T> result);
        public T[] GetComponentsInChildren<T>(bool includeInactive);
        public Component[] GetComponentsInChildren(Type t, bool includeInactive);
        public T[] GetComponentsInParent<T>(bool includeInactive);
        public Component[] GetComponentsInParent(Type t);
        public Component[] GetComponentsInParent(Type t, [Internal.        public T[] GetComponentsInParent<T>();
        public void GetComponentsInParent<T>(bool includeInactive, List<T> results);
        public void SendMessage(string methodName, object value, SendMessageOptions options);
        public void SendMessage(string methodName);
        public void SendMessage(string methodName, object value);
        public void SendMessage(string methodName, SendMessageOptions options);
        public void SendMessageUpwards(string methodName, object value);
        public void SendMessageUpwards(string methodName, SendMessageOptions options);
        public void SendMessageUpwards(string methodName);
        public void SendMessageUpwards(string methodName, [Internal.DefaultValue("null")] object value, [Internal.DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);
        public bool TryGetComponent<T>(out T component);
        public bool TryGetComponent(Type type, out Component component);
    }
}
```
## Componentの機能


## 各種Component
代表的な Component( コンポーネント )を見てみましょう。

### Transform Component

### ３D空間上の姿勢情報を保持している

#### 変数
ワールド空間上での姿勢情報
|変数名|説明|
|:--|:--|
|[position](https://docs.unity3d.com/ja/current/ScriptReference/Transform-position.html)|３D座標|
|[rotation](https://docs.unity3d.com/ja/current/ScriptReference/Transform-rotation.html)|回転向き|
|[lossyScale](https://docs.unity3d.com/ja/current/ScriptReference/Transform-lossyScale.html)|大きさ|

### ヒエラルキーの情報
Transform.parentという変数を持っていて 自分の親は誰かを表しています。
ヒエラルキーにはゲームオブジェクトの情報が出ていてるので、GameObjectがヒエラルキーを管理していると思うと思うのですが、厳密にはTransform Componetが管理していいるのです。

詳しくは [UnityEngine.Transform - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/current/ScriptReference/Transform.html)を見て下さい。

### RectTransform Component

[UnityEngine.RectTransform - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/current/ScriptReference/RectTransform.html)

### Behaviour Component
Behaviourもコンポーネントです。 皆さんの書いているスクリプトは Behaviourから派生しているのを作っていると思いますので、結局カスタムコンポーネントを作成していることなのです。

[UnityEngine.Behaviour - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/current/ScriptReference/Behaviour.html)


# UnityEngine.Objectってなに？

```CSharp
using System;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
    public class Object
    {
        public Object();
        public HideFlags hideFlags { get; set; }
        public string name { get; set; }
        public static void Destroy(Object obj, [DefaultValue("0.0F")] float t);
        public static void Destroy(Object obj);
        public static void DestroyImmediate(Object obj);
        public static void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);
        public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t);
        public static void DestroyObject(Object obj);
        public static void DontDestroyOnLoad(Object target);
        public static T FindObjectOfType<T>() where T : Object;
        public static T FindObjectOfType<T>(bool includeInactive) where T : Object;
        public static Object FindObjectOfType(Type type);
        public static Object FindObjectOfType(Type type, bool includeInactive);
        public static T[] FindObjectsOfType<T>() where T : Object;
        public static Object[] FindObjectsOfType(Type type, bool includeInactive);
        public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object;
        public static Object[] FindObjectsOfType(Type type);
        public static Object[] FindObjectsOfTypeAll(Type type);
        public static Object[] FindObjectsOfTypeIncludingAssets(Type type);
        public static Object[] FindSceneObjectsOfType(Type type);
        public static T Instantiate<T>(T original, Transform parent) where T : Object;
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);
        public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object;
        public static Object Instantiate(Object original);
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace);
        public static T Instantiate<T>(T original) where T : Object;
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object;
        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object;
        public static Object Instantiate(Object original, Transform parent);
        public override bool Equals(object other);
        public override int GetHashCode();
        public int GetInstanceID();
        public override string ToString();
        public static bool operator ==(Object x, Object y);
        public static bool operator !=(Object x, Object y);
        public static implicit operator bool(Object exists);
    }
}
```
## UnityEngine.Objectの機能
Unityゲームエンジンの最小単位のオブジェクト。全てのクラスはここから派生して作成されている。


# おまけ
Unityゲームエンジンは代表的なクラスは全てはC++言語で実装されています。


# まとめ

* ドキュメントやVS2019のクラス定義を積極的に見て継承しているクラスにどんなプロパティーや関数が存在するかを確認しましょう。
* MonoBehaviourでゲームオブジェクトを制御するが GameObjectから派生しているわけではない。
