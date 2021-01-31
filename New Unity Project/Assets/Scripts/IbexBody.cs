using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IbexBody : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("IbexRox"))
			return;

		IbexController ibex = GetComponentInParent<IbexController>();

		ibex.StunIbex();
	}
}
