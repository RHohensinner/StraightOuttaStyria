using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
	public CharacterScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(player.fireActive)
    	{
    		//handle player fire
    	}
    	if(player.potionActive)
    	{
    		//handle player potion
    		//player.health = player.maxHealth;
    		//player.potionActive = false;
    	}
    	if(player.shieldActive)
    	{
            //handle player shield
    	}
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
    	if(player.firePickedUp)
    	{
    		Debug.Log("Destroy Fire");
    		//player.firePickedUp = false;
    		Destroy(this.gameObject);
    	}
    	if(player.potionPickedUp)
    	{
    		Debug.Log("Destroy Potion");
    		//player.potionPickedUp = false;
    		Destroy(this.gameObject);
    	}
    	if(player.shieldPickedUp)
    	{
    		Debug.Log("Destroy Shield");
    		//player.shieldPickedUp = false;
    		Destroy(this.gameObject);
    	}
    }
}
