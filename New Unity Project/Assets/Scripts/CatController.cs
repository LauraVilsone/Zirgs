using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
	bool gotoHome;

  public void GotoHome()
	{
		Debug.Log("Cat go home");

		if (gotoHome)
			return;

		gotoHome = true;

		Animator anim  = GetComponent<Animator>();
		anim.SetBool("Runing", true);
		anim.SetTrigger("Home");


		PathController path = GetComponentInParent<PathController>();
		path.StartPath(PathEnded);

	}

	void PathEnded()
	{
		Animator anim = GetComponent<Animator>();
		anim.SetBool("Runing", false);
	}

	public void Scare()
	{
		Debug.Log("Cat is scared");

	}

}
