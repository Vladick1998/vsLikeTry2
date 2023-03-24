using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
	public enum StatType
	{
		Health,
		Mana
	}
	public enum StatMod
	{
		Add,
		Increase,
		More
	}
	public StatType statType;
	public float statFlat;
	public float statIncrease;
	public float statMore;
	public float statFinal
	{
		get
		{
			return reCalculate();
		}
	}
	float reCalculate()
	{
		float finalStat = (statFlat/100*statIncrease)/100 * statMore;
		return finalStat;
	}
}