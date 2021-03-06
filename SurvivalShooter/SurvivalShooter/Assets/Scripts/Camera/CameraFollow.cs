﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothing = 20f;

	private Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		if (target.GetComponent<PlayerHealth>().GetPlayerHealth() > 0)
		{
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		
	}

	public void SetTarget(Transform pTransform)
	{
		target = pTransform;
	}
}
