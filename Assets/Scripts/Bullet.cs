using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] float bulletSpeed;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float timeBeforeDead;
	public GameObject parrent;
	public Vector2 direction;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject, timeBeforeDead);
	}

	private void FixedUpdate()
	{
	
		Vector3 vector = transform.TransformDirection(new Vector2(1, 0) * bulletSpeed * Time.fixedDeltaTime);
		rb.MovePosition(rb.position + new Vector2(vector.x, vector.y)*bulletSpeed*Time.fixedDeltaTime);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject != parrent && (collision.gameObject.tag == "Unit" || collision.gameObject.tag == "Ground" ))
			Destroy(gameObject);
		//Debug.Log("trigger" + collision.gameObject);
	}
}
