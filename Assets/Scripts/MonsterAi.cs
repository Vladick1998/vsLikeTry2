using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
	State state;
	Coroutine move;
	[SerializeField] 
	GameObject hand;
	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	Unit unit;
	public float thinkRate;
	public float seeRange;
	public CircleCollider2D seeColider;
	public GameObject target;
	Vector3 lastSeenPos;
	[Header("Skills")]
	public List<Weapon> skills;
	public Weapon currentSkill;
	[Space]

	[SerializeField]
	public List<AttitudesTowardOthers> Attitudes;
	[SerializeField]
	public List<Person> WhatISeeList = new List<Person>();

	[System.Serializable]
	public struct AttitudesTowardOthers
	{

		public Unit.UnitRace race;
		public int tolerance;
	}
	[System.Serializable]
	public class Person
	{
		public GameObject gameObject;
		public int dungerRate;
	}
	#region enums
	enum NatureModel
	{
		aggresive,
		neutral,
		passive
	}
	enum State
	{
		attack,
		chase,
		stand,
		sleep,
		randomWalk,
		patrol,
		doSomething
	}
	#endregion


	private void Start()
	{
		lastSeenPos = Vector3.zero;
		seeColider.radius = seeRange;
		seeColider.isTrigger = true;
		StartCoroutine(thinker());
	}
	private void Update()
	{
		if (target!=null)
			bodyRotation();
	}
	public IEnumerator thinker()
	{
		while (true)
		{

			if (move != null)
				StopCoroutine(moveTo());
			SearchAttackTarget();
			if (target != null)
			{
				state = State.attack;
				SkillChoise();
				move = StartCoroutine(moveTo());
				Vector3 distance = lastSeenPos - transform.position;
				if (distance.sqrMagnitude <= currentSkill.baseRange +1.5f)
				{
					currentSkill.attack(target.transform.position);
				}
			}
			else if (lastSeenPos != null)
			{
				state = State.chase;
			}
			else state = State.stand;
			yield return new WaitForSeconds(thinkRate);
		}
	}
	private void bodyRotation()
	{
		//Debug.Log(transform.position.x+"-"+ Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x);
		if (transform.position.x - target.transform.position.x < 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
			hand.transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(-1, 1, 1);
			hand.transform.localScale = new Vector3(-1, -1, 1);
		}
		#region hand look to mouse pos
		float AngleRad = Mathf.Atan2(target.transform.position.y - this.transform.position.y, target.transform.position.x - this.transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		hand.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
		#endregion
	}
	void SkillChoise()
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (currentSkill == null || skills[i].baseAttackDamage > currentSkill.baseAttackDamage && skills[i].skillState == 0)
			{
				currentSkill = skills[i];
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Unit" && ChekInList(collision.gameObject))
		{
			Person temp = new Person();
			temp.gameObject = collision.gameObject;
			temp.dungerRate = Attitudes[(int)collision.gameObject.GetComponent<Unit>().race].tolerance;
			WhatISeeList.Add(temp);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Unit")
		{
			RemoveInlist(collision.gameObject);
			if (collision.gameObject == target)
			{
				target = null;
			}
		}
	}
	IEnumerator moveTo()
	{
		while (target != null)
		{
			Vector3 direction = (target.transform.position - transform.position).normalized;
			direction.z = transform.position.z;
			Vector3 distance = target.transform.position - transform.position;
			if (Mathf.Abs(distance.sqrMagnitude - currentSkill.baseRange) >= 1)
				if (distance.sqrMagnitude > currentSkill.baseRange)
					rb.MovePosition(transform.position + direction * unit.MoveSpeed * Time.deltaTime);
				else 
					rb.MovePosition(transform.position - direction * unit.MoveSpeed * Time.deltaTime);
			lastSeenPos = target.transform.position;
			yield return new WaitForFixedUpdate();
		}
		while (lastSeenPos != Vector3.zero)
		{
			Vector3 direction = (lastSeenPos - transform.position).normalized;
			direction.z = transform.position.z;
			rb.MovePosition(transform.position + direction * unit.MoveSpeed * Time.deltaTime);
			Vector3 distance = lastSeenPos - transform.position;
			if (distance.sqrMagnitude < 1)
				lastSeenPos = Vector3.zero;
			yield return new WaitForFixedUpdate();
		}
	}
	#region list Works
	void SearchAttackTarget()
	{
		target = null;
		int min = 0;
		for (int i = 0; i < WhatISeeList.Count; i++)
		{
			if (WhatISeeList[i].dungerRate < min)
			{
				min = WhatISeeList[i].dungerRate;
				target = WhatISeeList[i].gameObject;
			}
		}
	}
	void RemoveInlist(GameObject toRemove)
	{
		for (int i = 0; i < WhatISeeList.Count; i++)
		{
			if (WhatISeeList[i].gameObject == toRemove)
			{
				WhatISeeList.RemoveAt(i);
				break;
			}

		}
	}
	bool ChekInList(GameObject toChek)
	{
		for (int i = 0; i < WhatISeeList.Count; i++)
			if (WhatISeeList[i].gameObject == toChek)
				return false;
		return true;

	}
	#endregion
}

