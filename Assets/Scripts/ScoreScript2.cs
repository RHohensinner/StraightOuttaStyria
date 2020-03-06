using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript2 : MonoBehaviour
{
	public static float scoreValue = 120f;
	public static float initial_score;
	Text scoreText;
	// Start is called before the first frame update
	void Start()
    {
		scoreText = GetComponent<Text>();
		initial_score = scoreValue;
	}

    // Update is called once per frame
    void Update()
    {
		scoreValue -= Time.deltaTime;
		scoreText.text = "Score: " + ((int)scoreValue).ToString();
    }
}
