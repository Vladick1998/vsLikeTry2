using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public static float physArmourEffectivnes = 0.06f;
	public float physDamage;
	public float fireDamage;
	public float coldDamage;
	public float lightningDamage;
	public float lightDamage;
	public float darkDamage;
	public void CalculateAttackDamage(Unit attackerUnit)
	{
		physDamage = (attackerUnit.weapon_1.baseAttackDamage + attackerUnit.AttackDmgFlat) + ((attackerUnit.weapon_1.baseAttackDamage + attackerUnit.AttackDmgFlat)/100 * attackerUnit.AttackDmgPercent);
	}
	public float CalculateRecivedDamage(Unit attacketUnit)
	{
		float damage = 0;
		damage += physDamage - (physDamage / 100 * (int)((attacketUnit.Armour * physArmourEffectivnes) / (1 + attacketUnit.Armour * physArmourEffectivnes)));
		return damage;
	}
}
