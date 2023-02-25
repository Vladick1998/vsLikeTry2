using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
	#region stats
	 public UnitRace race;

	#region Xp
	[Header("Xp")]
	public float LevelCurrent;
	public float XpCurrent;
	public float XpToLevelUp;
	public float XpToLevelUpDifficultMultiplauer;
	public float xpCurrent
	{
		get { return XpCurrent; }
		set
		{
			XpCurrent = value;
			if (XpCurrent >= XpToLevelUp)
			{
				LevelCurrent += 1;
				xpCurrent -= XpToLevelUp;
				XpToLevelUp = 100 * LevelCurrent * XpToLevelUpDifficultMultiplauer;

			}
		}
	}

	#endregion
	#region Healt\Mana
	[Header("Hp Mana")]
	public Slider HpSlider;
	public Slider ManaSlider;
	public float HpMax;
	public float HpCurrent;
	public Weapon weapon_1;
	public float hp
	{
		get { return HpCurrent; }
		set
		{
			if (HpMax < value)
				HpCurrent = HpMax;
			else
			{
				HpCurrent = value;
				if (HpCurrent <= 0)
					Destroy(gameObject);
			}
			//UiUpd();
		}
	}
	public float HpRegen;
	public float HpRegenPercent;
	public float HpVampiric;
	public float ManaMax;
	public float ManaCurrent;
	public float mp
	{
		get { return ManaCurrent; }
		set
		{
			if (ManaMax < value)
				ManaCurrent = ManaMax;
			else
				ManaCurrent += value;
			//UiUpd();
		}
	}
	public float ManaRegen;
	public float ManaRegenPercent;
	public float ManaVampiric;
	#endregion
	#region Attack\Cast
	[Header("Attack stats")]
	public float AttackDmgFlat;
	public float AttackDmgPercent;
	public float AttackSpeed;

	public float CastDmgFlat;
	public float CastDmgPercent;
	public float CastSpeed;
	#endregion
	#region Defence
	[Header("Deffence stats")]
	public float Evade;
	public float Armour;
	public float ElenentalResistanse;
	public float BlockFlat;
	public float BlockPercent;
	#endregion
	#region Utility
	[Header("Utility stats")]
	public float MoveSpeed;
	public float knockbackForce;
	public float MagnetRadius
	{
		get { return (Magnet.radius); }
		set { Magnet.radius = value; }
	}
	public CircleCollider2D Magnet;
	#endregion
	#endregion
	void Update()
	{
		regen();
	}
	public void regen()
	{
		hp += (HpMax * HpRegenPercent + HpRegen) * Time.deltaTime;
		mp += (ManaMax * ManaRegenPercent + ManaRegen) * Time.deltaTime;
	}
	virtual public void reciveDamage(float dmg)
	{
		hp -= dmg;
	}
	public enum UnitRace
	{
		Human,
		Goblin
	}
}
