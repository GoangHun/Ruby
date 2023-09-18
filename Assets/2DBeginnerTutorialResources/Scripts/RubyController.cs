using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
	private ParticleSystem hitParticle;

	public float speed = 4f;

	public Projectile projectilePrefab;
	private Animator animator;
	private Rigidbody2D rigidbody2d;
	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;
	[SerializeField]private AudioClip hitClip;
	[SerializeField]private AudioClip collectable;
	private Vector2 lookDirection = Vector2.right;
	private Vector2 direction = Vector2.zero;

	public int currentHp;
	public int maxHp = 20;

	public float timeInvincible = 1f;
	private bool isInvincible = false;
	private float invincibleTimer;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
		hitParticle = GetComponentInChildren<ParticleSystem>(); //자식 오브젝트 포함 컴포넌트가 하나만 있을 때 가능
	}

	private void Start()
	{
		currentHp = maxHp;
		//hitParticle.Stop();
	}

	public void FixedUpdate()
	{
		Vector2 position = rigidbody2d.position;
		position += direction * speed * Time.deltaTime;
		rigidbody2d.MovePosition(position);

		//transform.position += direction * speed * Time.deltaTime;
	}

	public void Update()
	{
		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer < 0)
			{
				isInvincible = false;
				spriteRenderer.color = Color.white;
			}
		}

		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");
		direction = new Vector3(h,v);
		var directionMag = direction.magnitude;

		if (directionMag > 1)
		{
			direction.Normalize();
		}
		if (directionMag > 0)
		{
			lookDirection = direction;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			var lookNomalized = lookDirection.normalized;
			var pos = rigidbody2d.position;
			pos.y += 0.5f;
			pos += lookNomalized * 0.5f;

			var projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
			projectile.Launch(lookNomalized, 10);
			animator.SetTrigger("Launch");
		}

		animator.SetFloat("Speed", directionMag);
		animator.SetFloat("Look X", lookDirection.x);
		animator.SetFloat("Look Y", lookDirection.y);
	}

	public void Heal(int hpPoint)
	{
		currentHp += Mathf.Clamp(currentHp + hpPoint, 0, maxHp);
		audioSource.PlayOneShot(collectable);	
		AudioSource.PlayClipAtPoint(collectable, transform.position);	//
		Debug.Log(audioSource.isPlaying);
		Debug.Log("Heal: " + currentHp);
	}

	public void TakeDamage(int damage)
	{
		if (isInvincible)
			return;
		spriteRenderer.color = Color.red;

		audioSource.clip = hitClip;
		audioSource.Play();

		currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
		Debug.Log(currentHp);

		isInvincible = true;	
		invincibleTimer = timeInvincible;
		animator.SetTrigger("Hit");

		//hitParticle.Stop();
		hitParticle.Play();
	}
}
