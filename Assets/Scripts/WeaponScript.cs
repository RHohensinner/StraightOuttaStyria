using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public CharacterScript player;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire") && player.firePickedUp)
        {
            StartCoroutine(Shoot());
            player.fireCounter--;

            if(player.fireCounter == 0)
            {
                player.firePickedUp = false;
            }

        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo =  Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            /*Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                //implement damage behaviour
            }*/

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
