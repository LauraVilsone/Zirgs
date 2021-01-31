using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
	bool gotoHome;

	Animator anim => GetComponent<Animator>();

	public void GotoHome()
	{
		Debug.Log("Cat go home");

		if (gotoHome)
			return;

		gotoHome = true;

		anim.SetBool("Runing", true);
		anim.SetTrigger("Home");


		PathController path = GetComponentInParent<PathController>();
		path.StartPath(PathEnded);

	}

	internal void Warning()
	{
		anim.SetBool("Warning", true);
	}

	internal void WarningEnd()
	{
		anim.SetBool("Warning", false);
	}

	void PathEnded()
	{
		anim.SetBool("Runing", false);
	}

	public void Scare()
	{

		//Debug.Log("Cat is scared");
		anim.SetTrigger("Scare");
	}

}
