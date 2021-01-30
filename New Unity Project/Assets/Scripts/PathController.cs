using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

	public Transform body;
	public float speed = 5;
	public Transform[] points;

	int currentPath;
	bool pathStarted;
	bool pathEnded;

	Action OnPathEneded;

	public void StartPath(Action endPath)
	{
		if (pathStarted)
			return;

		OnPathEneded = endPath;
		currentPath = 0;
		pathStarted = true;
		pathEnded = false;
	}

	private void Update()
	{
		if (!pathStarted || pathEnded)
			return;

		body.position = Vector3.MoveTowards(body.position, points[currentPath].position, speed * Time.deltaTime);
		body.LookAt(points[currentPath]);

		if (Vector3.Distance(body.position, points[currentPath].position) > 1)
			return;

		currentPath++;
		if (currentPath >= points.Length)
		{
			OnPathEneded.Invoke();
			pathEnded = true;
		}


	}
}
