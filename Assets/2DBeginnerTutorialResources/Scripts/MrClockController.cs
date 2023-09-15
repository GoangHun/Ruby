using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MrClockController : MonoBehaviour
{
	public float speed = 4f;
	private float currentMoveDistance = 0f;
	public float moveDistance = 100f;

	private Animator animator;
	private Rigidbody2D rigidbody2d;
	private Vector2 lookDirection = Vector2.right;
	private Vector2 direction = Vector2.zero;
	private bool isFixed = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	public void FixedUpdate()
	{
		Vector2 position = rigidbody2d.position;
		currentMoveDistance = speed * Time.deltaTime;
		position += direction * currentMoveDistance;
		rigidbody2d.MovePosition(position);
	}

	public void Update()
	{
		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");
		direction = new Vector3(h, v);
		var directionMag = direction.magnitude;

		if (currentMoveDistance > moveDistance)
		{
			direction
		}


		if (directionMag > 1)
		{
			direction.Normalize();
		}
		if (directionMag > 0)
		{
			lookDirection = direction;
			isFixed = false;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isFixed = true;
		}

		animator.SetBool("IsFixed", isFixed);
		animator.SetFloat("Speed", directionMag);
		animator.SetFloat("Look X", lookDirection.x);
		animator.SetFloat("Look Y", lookDirection.y);
	}
}
