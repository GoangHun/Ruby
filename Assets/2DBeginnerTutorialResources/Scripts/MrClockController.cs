using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MrClockController : MonoBehaviour
{
	public float speed = 4f;
	public float moveDistance = 5f;
	public int maxHp = 5;
	private int hp = 0;
    private float currentMoveDistance = 0f;
	

	private Animator animator;
	private Rigidbody2D rigidbody2d;
	private BoxCollider2D boxCollider2d;
	private Vector2 lookDirection = Vector2.right;
	private Vector2 direction = Vector2.left;
	private bool isFixed = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();

    }

	public void FixedUpdate()
	{
		Vector2 position = rigidbody2d.position;
		currentMoveDistance += speed * Time.deltaTime;
		position += direction * speed * Time.deltaTime;
		rigidbody2d.MovePosition(position);
	}

    public void Update()
	{
		if (currentMoveDistance > moveDistance)
		{
			if (direction.x > 0)
				direction = Vector2.left;
			else
				direction = Vector2.right;
			currentMoveDistance = 0f;
        }
        var directionMag = direction.magnitude;

        if (directionMag > 0)
		{
			lookDirection = direction;
		}

		animator.SetBool("IsFixed", isFixed);
		animator.SetFloat("Speed", directionMag);
		animator.SetFloat("Look X", lookDirection.x);
		animator.SetFloat("Look Y", lookDirection.y);
	}

	public void Heal(int hp)
	{
		this.hp += hp;
		if (this.hp == maxHp)
		{
            isFixed = true;
            boxCollider2d.enabled = false;
			direction = Vector2.zero;
			speed = 0f;
        }
    }
}
