using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Skill
{
	public Bullet bullet;
	public float deviationMultiplayer = 1;

	public override void  Cast(Vector3 targetPosition)
	{
		float AngleRad = Mathf.Atan2(targetPosition.y - GetComponentInParent<Unit>().transform.position.y, targetPosition.x - GetComponentInParent<Unit>().transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		float deviation = damage.bulletCount/2 * (deviationMultiplayer * 10);
		for (int i = 0; i < damage.bulletCount; i++)
		{
			Bullet tempbull = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, deviation + AngleDeg + Random.Range(baseSpread / 2 * -1, baseSpread / 2)));
			tempbull.parrent = GetComponentInParent<Unit>().gameObject;
			tempbull.gameObject.SetActive(true);
			tempbull.damage = damage;
			deviation -= deviationMultiplayer*10;
		}
	}
}
