using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{

	bool isSpace;
	private void Update()
	{
		isSpace = Input.GetKey(KeyCode.Space);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "CatBack")
		{
			//TODO scare cat
			CatController cat = other.GetComponentInParent<CatController>();
			if (!cat)
				return;

			cat.Warning();
			return;
		}
	}



	private void OnTriggerExit(Collider other)
	{
		if (other.name == "CatBack")
		{
			//TODO scare cat
			CatController cat = other.GetComponentInParent<CatController>();
			if (!cat)
				return;

			cat.WarningEnd();
			return;
		}
	}
	private void OnTriggerStay(Collider other)
	{

		if (!isSpace)
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
