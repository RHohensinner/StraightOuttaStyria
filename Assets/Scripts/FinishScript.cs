using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{

	public CharacterScript player;
    public int numAllImages;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.layer == 10)
        {
            if (player.imagesCollected == numAllImages)
            {
                Application.LoadLevel(nextScene);
            }
        }
	}
}
