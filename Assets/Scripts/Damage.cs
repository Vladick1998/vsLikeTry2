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
		physDamage = (attackerUnit.weapon_1.baseAttackDamage + attackerUnit.attackDmgFlat) + ((attackerUnit.weapon_1.baseAttackDamage + attackerUnit.attackDmgFlat) / 100 * attackerUnit.attackDmgPercent);
		fireDamage = (attackerUnit.weapon_1.basefireDmgFlat + attackerUnit.fireDmgFlat) + ((attackerUnit.weapon_1.basefireDmgFlat + attackerUnit.fireDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		coldDamage = (attackerUnit.weapon_1.basecoldDmgFlat + attackerUnit.coldDmgFlat) + ((attackerUnit.weapon_1.basecoldDmgFlat + attackerUnit.coldDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		lightningDamage = (attackerUnit.weapon_1.baselightningDmgFlat + attackerUnit.lightningDmgFlat) + ((attackerUnit.weapon_1.baselightningDmgFlat + attackerUnit.lightningDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		lightDamage = (attackerUnit.weapon_1.baselightDmgFlat + attackerUnit.lightDmgFlat) + ((attackerUnit.weapon_1.baselightDmgFlat + attackerUnit.lightDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		darkDamage = (attackerUnit.weapon_1.basedarkDmgFlat + attackerUnit.darkDmgFlat) + ((attackerUnit.weapon_1.basedarkDmgFlat + attackerUnit.darkDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
	}
	public float CalculateRecivedDamage(Unit attacketUnit)
	{
		float damage = 0;
		Debug.Log((((attacketUnit.Armour * physArmourEffectivnes) / (1 + attacketUnit.Armour * physArmourEffectivnes))));
		damage += physDamage - attacketUnit.BlockFlat - (physDamage * ((attacketUnit.Armour * physArmourEffectivnes) / (1 + attacketUnit.Armour * physArmourEffectivnes)));
		return damage;
	}
}
