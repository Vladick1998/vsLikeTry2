
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	
	[Header("Player component references")]
	public Rigidbody2D rb;
	[SerializeField] Animator animator;

	[Header("Player settings")]
	Player player;
	Vector2 playerInput;
	[SerializeField] float rollspeed;
	[SerializeField] GameObject hand;
	Coroutine attackCoroutine;
	enum BodyState
	{
		canMove,
		cantMove
	}
	enum HandState
	{
		canAttack,
		cantAttack
	}
	HandState handState;
	BodyState bodystate;
	private void Awake()
	{
		handState = HandState.canAttack;
		bodystate = BodyState.canMove;
		player = GetComponent<Player>();
		animator = GetComponent<Animator>();

	}

	private void FixedUpdate()
	{
		bodyRotation();
		switch (bodystate)
		{
			case BodyState.canMove:
				if (playerInput != Vector2.zero)
				{
					animator.SetInteger("animatorState", 1);
					rb.MovePosition(rb.position + playerInput * player.MoveSpeed * Time.fixedDeltaTime);

				}
				else animator.SetInteger("animatorState", 0);
				break;
			case BodyState.cantMove:
				//Debug.Log("cant move");
				break;
		}

	}

	private void bodyRotation()
	{
		//Debug.Log(transform.position.x+"-"+ Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x);
		if (transform.position.x - Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x < 0)
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
		Vector3 lookAt = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		hand.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
		#endregion
	}

	public void Move(InputAction.CallbackContext context)
	{
		playerInput = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
	}
	public void Skill(InputAction.CallbackContext context)
	{
		if (context.performed && bodystate != BodyState.cantMove)
			StartCoroutine(roll());

	}
	public void Fire(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			attackCoroutine = StartCoroutine(AttackButtonPressed());
		}
		if (context.canceled)
		{
			StopCoroutine(attackCoroutine);
		}
	}
	IEnumerator AttackButtonPressed()
	{
		while (true)
		{
			if (bodystate != BodyState.cantMove && player.weapon_1.canAttack && handState == HandState.canAttack)
			{
				animator.Play("Attack", 1);
				player.weapon_1.attackPrimary();
			}
			yield return new WaitForFixedUpdate();
		}
	}
	IEnumerator roll()
	{
		handState = HandState.cantAttack;
		bodystate = BodyState.cantMove;
		float time = 0f;
		//Vector2 rollDirection = playerInput;
		Vector2 rollDirectionMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		rollDirectionMouse = (rollDirectionMouse - rb.position).normalized;
		while (time <= 0.2)
		{

			time += Time.deltaTime;
			rb.MovePosition(rb.position + rollDirectionMouse * rollspeed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
			rb.rotation -= 36;
		}
		rb.rotation = 0;
		bodystate = BodyState.canMove;
	}
}
