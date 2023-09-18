using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
	public MrClockController enemy;
	private Image image;
	private float target;
	public int speed;

	private void Awake()
	{
		image = GetComponent<Image>();
	}
	// Update is called once per frame
	void Update()
	{
		target = (float)enemy.hp / (float)enemy.maxHp;
		if (image.fillAmount > 1 - target)
		{
			image.fillAmount -= speed * Time.deltaTime;
		}
	}
}
