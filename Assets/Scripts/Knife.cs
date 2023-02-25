using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Knife : Weapon
{
	[SerializeField] Bullet bullet;

	private void Start()
	{
		unit = GetComponentInParent<Unit>();
		canAttack = true;
		reCalcStats();
		EventSystem.playerChangeStats.AddListener(reCalcStats);
	}
	private void reCalcStats()
	{
		attackSpeed = baseAttackSpeed / (1 + (Player.player.AttackSpeed / 100));
	}
	public override void attack(Vector3 lookAt)
	{
		float AngleRad = Mathf.Atan2(lookAt.y - GetComponentInParent<Unit>().transform.position.y, lookAt.x - GetComponentInParent<Unit>().transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		Bullet tempbull = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, AngleDeg + Random.Range(baseSpread / 2 * -1, baseSpread / 2)));
		tempbull.parrent = GetComponentInParent<Unit>().gameObject;
		tempbull.gameObject.SetActive(true);
		canAttack = false;
		StartCoroutine(attackdelay());
	}

	public override void attackPrimary()
	{
		Vector3 lookAt = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		float AngleRad = Mathf.Atan2(lookAt.y - GetComponentInParent<Unit>().transform.position.y, lookAt.x - GetComponentInParent<Unit>().transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		Bullet tempbull = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, AngleDeg + Random.Range(baseSpread / 2 * -1, baseSpread / 2)));
		tempbull.parrent = GetComponentInParent<Unit>().gameObject;
		tempbull.gameObject.SetActive(true);
		canAttack = false;
		StartCoroutine(attackdelay());
	}
}
