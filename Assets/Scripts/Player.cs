using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public static Player player;
    public float immortalFramesCount;
    public bool immortalFrames;
    SpriteRenderer sprite;
	private void Awake()
	{
        player = this;
    }
	void Start()
    {
        weapon_1 = GetComponentInChildren<Weapon>();
        //GameManager.player = this.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        //HpSlider.maxValue = HpMax;
        //ManaSlider.maxValue = ManaMax;
        hp = HpCurrent;
        mp = ManaCurrent;
       
    }
    void Update()
    {
        EventSystem.reCalculatePlayerStats();
    }
    public override void reciveDamage(float dmg)
    {
        //Debug.Log("penis");
        if (immortalFrames == false)
        {
            immortalFrames = true;
            StartCoroutine(immortal());
            base.reciveDamage(dmg);
        }
    }

    public IEnumerator immortal()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(immortalFramesCount);
        immortalFrames = false;
        sprite.color = Color.white;
    }
}
