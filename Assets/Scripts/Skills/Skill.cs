using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum UseState
    {
        Ready,
        Colldown
    }
    [Header("Base skill Stats")]
    public float baseAttackDamage;
    public float baseAttackSpeed;
    [Space]
    public float basefireDmgFlat;
    public float basecoldDmgFlat;
    public float baselightningDmgFlat;
    public float baselightDmgFlat;
    public float basedarkDmgFlat;
    public float basecastSpeed;
    [Header("Skill specific")]
    public float attackSpeedImpact = 1;
    public float castSpeedImpact = 1;
    public float baseDelay;
    public float baseSpread;
    public float baseRange;
    public float baseColldown;
    public float baseManaCost;
    public float colldown;
    public Damage damage;
    public enum Purpose
    {
        Attack,
        Cast,
        Move,
        Buff,
        Heal
    }
    public Purpose purpose;
    public UseState skillState;
    private void Start()
    {
        damage = GetComponentInParent<Damage>();
        if (!damage)
            damage = GetComponent<Damage>();
    }
    void delayCalc()
    {
        baseDelay = 1;
    }
    public virtual void Cast(Vector3 targetPosition)
    {
		switch (purpose)
		{
			case Purpose.Attack:
				break;
			case Purpose.Cast:
				break;
			case Purpose.Move:
				break;
			case Purpose.Buff:
				break;
			case Purpose.Heal:
				break;
			default:
				break;
		}
	}
    IEnumerable Delay(float delayTime)
    {
        skillState = UseState.Colldown;
        yield return new WaitForSeconds(delayTime);
        skillState = UseState.Ready;
    }
}
