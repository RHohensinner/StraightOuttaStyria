using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
	//private bool hasSpawn; // for when level too bicc
	private WeaponScript[] weapons;
	private Collider2D colliderComponent;
	private SpriteRenderer rendererComponent;

	/* For when level too bicc
	void Awake() 
	{
		
	}
	*/

	void Start()
	{
		weapons = GetComponentsInChildren<WeaponScript>();
		colliderComponent = GetComponent<Collider2D>();
		rendererComponent = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		foreach (WeaponScript weapon in weapons)
		{
			if(weapon != null && weapon.can_attack)
			{
				weapon.Attack(true);
			}
		}
	}
}
