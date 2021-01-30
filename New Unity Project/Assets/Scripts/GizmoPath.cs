using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoPath : MonoBehaviour
{

	[Range(0,1)]
	public float radius = 0.5f;
	public Color color = Color.white;

	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
