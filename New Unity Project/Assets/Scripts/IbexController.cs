using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IbexController : MonoBehaviour
{
	public Animator anim;
	public float chasingSpeed = 7;
	public float ibexRadius = 45;

	public float sheepSpeed = 2;

	[Header("Sheeps")]
	public Transform[] sheeps;
	public Transform homePos;
	public Transform[] stablePos;


	PathController path => GetComponent<PathController>();
	HorseController horse;


	bool isChasing;
	bool isDead;

	private void Start()
	{
		anim.SetBool("Patrol", true);
		path.StartPath(EndPath, ReachPath);

		horse = FindObjectOfType<HorseController>();

		//isDead = true;
	}

	internal void StunIbex()
	{
		if (isDead)
			return;

		isDead = true;

		anim.SetBool("Patrol", false);
		anim.SetBool("Chase", false);

		anim.SetTrigger("Die");
	}

	void EndPath()
	{
		if (isDead)
			return;

		anim.SetBool("Patrol", false);
		Invoke(nameof(StartPath), 10);
	}

	void StartPath()
	{
		if (isDead || isChasing)
			return;

		path.StartPath(EndPath);
	}

	void ReachPath()
	{
		if (isDead || isChasing)
			return;

		path.pathStarted = false;
		anim.SetBool("Patrol", false);
		Invoke(nameof(IddleIbex), 10);
	}

	void IddleIbex()
	{
		if (isDead || isChasing)
			return;

		path.pathStarted = true;
		anim.SetBool("Patrol", true);
	}

	private void Update()
	{
		if (isDead)
		{
			ShweepControl();
			return;
		}

		float distance = Vector3.Distance(horse.transform.position, transform.position);

		if (distance > ibexRadius)
		{
			if (!isChasing)
				return;

			isChasing = false;
			if (path.pathEnded)
			{
				path.StartPath(EndPath);
			}
			else
			{
				path.pathStarted = true;
			}

			anim.SetBool("Chase", false);
			anim.SetBool("Patrol", true);

			//TODO end chasing
			return;
		}

		if (!isChasing)
		{
			path.pathStarted = false;
			anim.SetBool("Patrol", false);
			anim.SetBool("Chase", true);
			isChasing = true;
		}

		path.TowardTransform(horse.transform, chasingSpeed);

		//TODO start chasing
	}

	private void ShweepControl()
	{
		int stable = -1;
		foreach (Transform sheepTransform in sheeps)
		{
			stable++;
			Animator anim = sheepTransform.GetComponentInChildren<Animator>();
			SheepController sheep = sheepTransform.GetComponent<SheepController>();
			if (sheep.reachStable)
			{
				anim.SetBool("Walk", false);
				continue;
			}

			if (sheep.reachHome)
			{
				anim.SetBool("Walk", true);
				if (PathController.TowardStep(sheepTransform, stablePos[stable], sheepSpeed, 2) <= 1)
					sheep.reachStable = true;

				continue;
			}

			float d = Vector3.Distance(sheepTransform.position, homePos.position);
			if (d <= ibexRadius / 2)
			{
				anim.SetBool("Walk", true);
				if (PathController.TowardStep(sheepTransform, homePos, sheepSpeed, 2) <= 1)
					sheep.reachHome = true;

				continue;
			}


			d = Vector3.Distance(sheepTransform.position, horse.transform.position);
			if (d > ibexRadius || d <= 3)
			{
				anim.SetBool("Walk", false);
				continue;
			}

			anim.SetBool("Walk", true);
			PathController.TowardStep(sheepTransform, horse.transform, sheepSpeed, 2);
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, ibexRadius / 2);
	}
}
