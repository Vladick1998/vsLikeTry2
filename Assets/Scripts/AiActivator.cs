using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActivator : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterAi monster = collision.gameObject.GetComponent<MonsterAi>();
		if (monster)
		{
			Debug.Log(collision.gameObject.name);
			monster.enabled = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		MonsterAi monster = collision.gameObject.GetComponent<MonsterAi>();
		if (monster)
		{
			monster.enabled = false;
		}
	}
}
