using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Knife : Weapon
{
	[SerializeField] Bullet bullet;
	[SerializeField] Damage damage;


	private void Start()
	{
		unit = GetComponentInParent<Unit>();
		canAttack = true;
		reCalcStats();
		EventSystem.playerChangeStats.AddListener(reCalcStats);
	}
	private void reCalcStats()
	{
		attackSpeed = baseAttackSpeed / (1 + (unit.attackSpeed / 100));
		damage.CalculateAttackDamage(unit);
		
	}
	public override void attack(Vector3 lookAt)
	{
		if (canAttack)
		{
			float AngleRad = Mathf.Atan2(lookAt.y - GetComponentInParent<Unit>().transform.position.y, lookAt.x - GetComponentInParent<Unit>().transform.position.x);
			float AngleDeg = (180 / Mathf.PI) * AngleRad;
			Bullet tempbull = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, AngleDeg + Random.Range(baseSpread / 2 * -1, baseSpread / 2)));
			tempbull.parrent = GetComponentInParent<Unit>().gameObject;
			tempbull.gameObject.SetActive(true);
			tempbull.damage = damage;
			Debug.Log(tempbull.damage);
			canAttack = false;
			StartCoroutine(attackdelay());
		}
	}

	public override void attackPrimary()
	{

	}
}
