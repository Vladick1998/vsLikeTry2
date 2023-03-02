using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Knife : Weapon
{
	public Skill skill;


	private void Start()
	{
		unit = GetComponentInParent<Unit>();
		canAttack = true;
	}
	public override void attack(Vector3 lookAt)
	{
		if (canAttack)
		{
			skill.Cast(lookAt);
			canAttack = false;
			StartCoroutine(attackdelay());
		}
	}

	public override void attackPrimary()
	{

	}
}
