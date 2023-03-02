using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public static Player player;
    public float immortalFramesCount;
    public bool immortalFrames;
    [SerializeField]
    SpriteRenderer sprite;
	private void Awake()
	{
        player = this;
    }
	void Start()
    {
        EventSystem.playerChangeStats.AddListener(reCalcStats);
        weapon_1 = GetComponentInChildren<Weapon>();
        //GameManager.player = this.gameObject;
        //sprite = GetComponent<SpriteRenderer>();
        //HpSlider.maxValue = HpMax;
        //ManaSlider.maxValue = ManaMax;
        hp = HpCurrent;
        mp = ManaCurrent;
        reCalcStats();
       
    }
    void Update()
    {
        //EventSystem.reCalculatePlayerStats();
        regen();
    }
    public override void reciveDamage(float dmg)
    {
        Debug.Log("DMG-> "+dmg);
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
