using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public RubyController rubyController;
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
		target = (float)rubyController.currentHp / (float)rubyController.maxHp;
		if (image.fillAmount > target )
		{
			image.fillAmount -= speed * Time.deltaTime;
		}
	}
}
