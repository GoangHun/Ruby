using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public int hpPoint = 1;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.SendMessage("Heal", hpPoint, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
