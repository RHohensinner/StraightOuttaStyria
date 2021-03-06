﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
	public Transform shot_shot;
    public CharacterScript player;
    public LineRenderer lineRenderer;

	public float shoot_rate = 0.15f;
	public float shoot_cd;
    public int laserDmg = 1;

    private AudioSource audio_laser;

	void Start()
	{
		shoot_cd = 0f;
	}

	public bool can_attack
	{
		get
		{
			return shoot_cd <= 0 ? true : false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (shoot_cd > 0)
		{
			shoot_cd -= Time.deltaTime;
		}
		if (player != null)
		{
			if (Input.GetButtonDown("Fire") && player.firePickedUp)
			{
				if (can_attack)
				{
					shoot_cd = shoot_rate;
					StartCoroutine(Shoot());
					player.fireCounter--;

					if (player.fireCounter == 0)
					{
						player.firePickedUp = false;
					}
				}
			}
		}
    }

	public void Attack(bool isEnemy)
	{
		if(can_attack)
		{
			shoot_cd = shoot_rate;
			var shotTransform = Instantiate(shot_shot) as Transform;
			shotTransform.position = transform.position;

			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (shot != null && move != null)
			{
				shot.isEnemyShot = isEnemy;
				//Debug.Log("direction" + move.direction.ToString());
			}
		}
	}

    IEnumerator Shoot()
    {
        audio_laser = GetComponent<AudioSource>();
        audio_laser.Play();
        RaycastHit2D hitInfo =  Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
            ShotScript shot = hitInfo.transform.GetComponent<ShotScript>();
            if (enemy != null)
            {
                
                HealthScript health = hitInfo.transform.GetComponent<HealthScript>();
                if(health != null)
                {
                    Debug.Log("DIE!!!!");
                    health.Damage(laserDmg);
                }
            }
            else if(shot != null)
            {
                Destroy(shot.gameObject);
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log("No Hit!");
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
