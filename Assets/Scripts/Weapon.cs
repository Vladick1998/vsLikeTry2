using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public enum UseState
	{
		Ready,
		Colldown
	}
	public UseState skillState;
	public enum Purpose
	{
		Attack,
		Move,
		Buff,
		Heal
	}
	public Purpose purpose;
	[Header("Weapon component references")]
	public SpriteRenderer spriteRenderer;

	[Header("Base Weapon Stats")]
	public float baseAttackDamage;
	public float baseAttackSpeed;
	[Space]
	public float basefireDmgFlat;
	public float basecoldDmgFlat;
	public float baselightningDmgFlat;
	public float baselightDmgFlat;
	public float basedarkDmgFlat;
	public float basecastSpeed;
	[Space]
	public float baseSpread;
	public float baseRange;
	public float baseColldown;
	public float baseManaCost;
	[Space]
	public Transform handRotation;
	public Unit unit;
	[Header("Stats")]
	public float attackSpeed;
	public float attackDamage;
	public bool canAttack = true;

	public virtual void attack(Vector3 lookAt)
	{

	}
	public virtual void attackPrimary()
	{

	}
	public IEnumerator attackdelay()
	{
		spriteRenderer.enabled = false;
		yield return new WaitForSeconds(attackSpeed);
		canAttack = true;
		spriteRenderer.enabled = true;
	}
}

