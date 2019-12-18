using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    private Transform bar;
    private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        bar = transform.Find("Bar");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

}
