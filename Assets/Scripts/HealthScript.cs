using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthScript : MonoBehaviour
{
	
	public int hp = 1; // enemy base hp
    public int maxHealth = 3;

	public bool isEnemy = true;

    public void Damage(int damageCount)
	{
        CharacterScript character = GetComponent<CharacterScript>();
        EnemyScript enemy = GetComponent<EnemyScript>();
        if(character != null)
        {
            if(character.shieldActive)
            {
                character.shieldDamageCounter--;
                if(character.shieldDamageCounter <= 0)
                {
                    character.shieldActive = false;
                }
            }
            else
            {
                hp -= damageCount;
            }
        }
        else if(enemy != null)
        {
            hp -= damageCount;
        }
		

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            Damage(1);
            Destroy(enemy.gameObject);
        }
    }
}
