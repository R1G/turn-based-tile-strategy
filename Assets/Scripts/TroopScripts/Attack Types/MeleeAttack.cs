using System;
using System.Collections;



public class MeleeAttack {

	//might wanna change these to ints (as well as health)

	float rollFloor = 0f;
	float rollCeiling = 3f;
		
	float damage;


	//right here I'm using rolls to determine damage. We can change this later, and w emight want to

	public MeleeAttack(float unitStrength, float weaponAtkValue, float debuffs) {

		//we're probably gonna use more complex 
		rollFloor += unitStrength;
		rollCeiling += weaponAtkValue;
		rollCeiling -= debuffs;

		damage = UnityEngine.Random.Range (rollFloor, rollCeiling);


	}

	public float getDamage() {
		return damage;
	}




}
