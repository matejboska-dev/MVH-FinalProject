using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour {

	private Animator anim;

	void Start()
	{
		this.anim = GetComponent<Animator>();
	}

	public void SetAnimator(int i)
	{
		this.anim.SetInteger("SwitchToMenu", i);
	}
}
