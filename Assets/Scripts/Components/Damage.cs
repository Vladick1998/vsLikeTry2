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
	public float castSpeed;
	public float attackSpeed;
	public int bulletCount;
	//calculate unit stats + weaponstats
	public void CalculateAttackDamage(Unit attackerUnit)
	{
		castSpeed = attackerUnit.castSpeed;
		attackSpeed = attackerUnit.attackSpeed;
		physDamage = (attackerUnit.weapon_1.baseAttackDamage + attackerUnit.attackDmgFlat) + ((attackerUnit.weapon_1.baseAttackDamage + attackerUnit.attackDmgFlat) / 100 * attackerUnit.attackDmgPercent);
		fireDamage = (attackerUnit.weapon_1.basefireDmgFlat + attackerUnit.fireDmgFlat) + ((attackerUnit.weapon_1.basefireDmgFlat + attackerUnit.fireDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		coldDamage = (attackerUnit.weapon_1.basecoldDmgFlat + attackerUnit.coldDmgFlat) + ((attackerUnit.weapon_1.basecoldDmgFlat + attackerUnit.coldDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		lightningDamage = (attackerUnit.weapon_1.baselightningDmgFlat + attackerUnit.lightningDmgFlat) + ((attackerUnit.weapon_1.baselightningDmgFlat + attackerUnit.lightningDmgFlat) / 100 * attackerUnit.elementalDmgPercent);
		lightDamage = (attackerUnit.weapon_1.baselightDmgFlat + attackerUnit.lightDmgFlat) + ((attackerUnit.weapon_1.baselightDmgFlat + attackerUnit.lightDmgFlat) / 100 * (attackerUnit.elementalDmgPercent/2 + attackerUnit.attackDmgPercent/2));
		darkDamage = (attackerUnit.weapon_1.basedarkDmgFlat + attackerUnit.darkDmgFlat);
	}
	public float CalculateRecivedDamage(Unit attacketUnit)
	{
		float damage = 0;

		damage += physDamage - attacketUnit.BlockFlat - (physDamage * ((attacketUnit.Armour * physArmourEffectivnes) / (1 + attacketUnit.Armour * physArmourEffectivnes)));
		damage += fireDamage - (fireDamage/100 * attacketUnit.ElenentalResistanse);
		damage += coldDamage - (coldDamage / 100 * attacketUnit.ElenentalResistanse);
		damage += lightningDamage - (lightningDamage / 100 * attacketUnit.ElenentalResistanse);
		damage += (lightDamage - (lightDamage / 100 * (attacketUnit.ElenentalResistanse/2)) - (lightDamage * ((attacketUnit.Armour * physArmourEffectivnes) / (1 + attacketUnit.Armour * physArmourEffectivnes))));
		damage += darkDamage - attacketUnit.BlockFlat * 2;
		return damage;
	}
}
