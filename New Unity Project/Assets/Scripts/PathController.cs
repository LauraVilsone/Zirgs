using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

	public Transform body;
	public float speed = 5;
	public float angularSpeed = 1;
	public Transform[] points;

	int currentPath;
	internal bool pathStarted;
	internal bool pathEnded;

	Action OnPathEneded;
	Action OnPathReach;

	public void StartPath(Action endPath, Action reachPath = null)
	{
		if (pathStarted)
			return;

		OnPathEneded = endPath;
		OnPathReach = reachPath;

		currentPath = 0;
		pathStarted = true;
		pathEnded = false;
	}

	public float TowardTransform(Transform target, float sp)
	{
		body.position = Vector3.MoveTowards(body.position, target.position, sp * Time.deltaTime);

		LootAt(body, target, angularSpeed);

		return Vector3.Distance(body.position, target.position);
	}

	private void Update()
	{
		if (!pathStarted || pathEnded)
			return;

		if (TowardTransform(points[currentPath], speed) > 1)
			return;

		currentPath++;
		if (currentPath >= points.Length)
		{
			OnPathEneded.Invoke();
			pathEnded = true;
			return;
		}

		if (OnPathReach != null)
			OnPathReach.Invoke();
	}

	public static void LootAt(Transform body, Transform target, float speed)
	{

		Vector3 forward = target.position - body.position;
		if (forward.magnitude < 0.01f)
			return;

		Quaternion look = Quaternion.LookRotation(forward);
		float angle = Quaternion.Angle(body.rotation, look);
		body.rotation = Quaternion.RotateTowards(body.rotation, look, angle * Time.deltaTime * speed);
		Vector3 euler = body.eulerAngles;
		euler.x = 0;
		euler.z = 0;
		body.eulerAngles = euler;

	}

	public static float TowardStep(Transform body, Transform target, float sped, float angularSpeed)
	{
		body.position = Vector3.MoveTowards(body.position, target.position, sped * Time.deltaTime);

		LootAt(body, target, angularSpeed);

		return Vector3.Distance(body.position, target.position);
	}
}
