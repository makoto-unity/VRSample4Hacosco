# VRSample4Hacosco
Unityの VR Sample ( https://unity3d.com/jp/learn/tutorials/topics/virtual-reality/vr-overview ) のハコスコ対応版です。

東大ハコスコ授業

いったんこれだけでもやってしまおう

・Unityのインストール：
http://unity3d.com/jp/get-unity/download/archive

・事前準備
https://unity3d.sakura.ne.jp/unity/android-build.html

Unityってゲームだけじゃないんだよ。

エディタの説明
File → Build Settings
Androidに

VR Sample をインポート

インストール
それぞれのUnityPackageをD&Dしてください

・SD ユニティちゃん
http://unity-chan.com/download/releaseNote.php?id=SDUnityChan

・Google VR SDK
https://developers.google.com/vr/unity/download

・VR Sample
https://www.assetstore.unity3d.com/en/#!/content/51519

シーンを開く
Assets/GoogleVR/DemoScenes/ControllerDemo/ControllerDemo

File→Build Settings
上記 Scenes In Build を一旦全部消して、
Add Open Scenesをクリックして追加

・実機転送
設定→開発者向けなんとか→USBデバッグ
実機をUSBでつないで
File → Build Settings → Build And Run


ステップ１
・ユニティちゃんを出してハコスコで見る
File → New Scene
File → Save Scene 名前は適当に「Step1」
GameObject → 3D Object → Plane
歯車でリセット
Scale (4,4,4)

Project ビューで Assets で
Create → Material
で名前をPlaneMat
Texture を GoogleVR/DemoScenes/HeadsetDemo/GroundPlane 
Tiling を X 40, Y 40
ひとつの区切りが1m

Assets/UnityChan/SD_unitychan/Prefabs/SD_unitychan_humanoid.prefab をD&D
Position (0,0,1) Rotation (0, 180, 0)

Idle Change オフ
Face Update オフ

MainCamera を削除
Assets/GoogleVR/Prefabs/GvrMain をD&D
Position(0, 0.8, 0)

File→Build Settings
上記 Scenes In Build を消して、
Add Open Scenesをクリックして追加
ビルド&ラン

GvrMain/Head/MainCamera をオフ
Assets/VRSampleScenes/Prefabs/Utils/MainCamera　を GvrMain/Head に D&D
Position (0, 0, 0)

SD_unitychan_humanoid にAdd Component → Capsule Collider 
Center (0, 0.5, 0)  Radius 0.25

Add Component → VR Interactive Item 
Add Component → MyInteractive と入力して、新規Component を増やす

MyInteractive.cs
---
using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class MyInteractive : MonoBehaviour {
	
	VRInteractiveItem interactiveItem;
	Animator anim;

	void Awake ()
	{
		interactiveItem = GetComponent<VRInteractiveItem>();
		anim = GetComponent<Animator> ();
	}

	private void OnEnable ()
	{
		interactiveItem.OnOver += Smile;
		interactiveItem.OnOut += Normal;
	}

	private void OnDisable ()
	{
		interactiveItem.OnOver -= Smile;
		interactiveItem.OnOut -= Normal;
	}

	private void Smile()
	{
		anim.CrossFade("smile@sd_hmd", 0);
		anim.SetLayerWeight (1, 1);
	}

	private void Normal()
	{
		anim.CrossFade("default@sd_hmd", 0);
		anim.SetLayerWeight (1, 1);
	}
}
---



ステップ２
・VR Sample を使ってみる
Assets/VRSampleScenes/Scenes/MainMenu
をダブルクリックで開きます。

ただこのままビルドしてAndroidに転用しても動きません。
これはOculus Rift/Gear VR用になっていて、Cardboard用になっていないためです。
まず、最初にあるMainCamera をオフにします。

Assets/GoogleVR/Prefabs/GvrMain

をD&Dします。もとにあった (先ほどオフにした)MainCameraのTransform の歯車アイコンをクリックして、
「Copy Component」をクリックしてパラメーターを

FlyerMovementController.cs
---
                Quaternion headRotation = InputTracking.GetLocalRotation (VRNode.Head);
の次の行に
				if ( head ) {
					headRotation = head.localRotation;
				}
をいれます。
---

ステップ３
・オリジナルコンテンツ

