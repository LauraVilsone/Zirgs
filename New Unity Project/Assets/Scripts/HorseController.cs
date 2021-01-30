using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{

		if (!Input.GetKey(KeyCode.Space))
			return;

		if (other.name == "Cat")
		{
			//TODO scare cat
			CatController cat = other.GetComponentInParent<CatController>();
			if (!cat)
				return;

			cat.Scare();
			return;
		}

		if (other.name == "CatBack")
		{

			CatController cat = other.GetComponentInParent<CatController>();
			if (!cat)
				return;

			cat.GotoHome();
		}
	}
}
