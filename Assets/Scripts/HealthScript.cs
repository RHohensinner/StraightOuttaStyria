using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthScript : MonoBehaviour
{
	
	public int hp = 1; // enemy base hp

	public bool isEnemy = true;

	public void Damage(int damageCount)
	{
		hp -= damageCount;

		if (hp <= 0)
		{
			// entity shot gets yeeted
			// SpecialEffectsHelper.Instance.Explosion(transform.position);
			// SoundEffectsHelper.Instance.MakeExplosionSound();

			// Yeet!
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				// Destroy the yeeting tool
				Destroy(shot.gameObject); // shot gets yeeted
			}
		}
	}
}
