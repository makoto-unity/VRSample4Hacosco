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
