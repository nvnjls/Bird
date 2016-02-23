using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpMagnitude;
	public float gravityMagnitude;
	public OuterCircleController circleController;

	private float timeGapForReactivation = 1f;
	private Vector2 deltaMovement;
	private bool handlePhysics = false;
	private bool handleInput = true;
	private bool reactivateControlls = true;
	private bool touchTop = false;
	private bool touchDown = false;
	private bool gameStarted = false;

	private ScoreManager scoreManager;
	private SpriteRenderer renderer;
    GameInstruction gis;
    void Start () {
		scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        gis = GameObject.FindGameObjectWithTag("GetIn").GetComponent<GameInstruction>();
        renderer = GetComponent<SpriteRenderer>();
		deltaMovement = new Vector2(0f,0f);
	}
	

	void Update () {
		if(!gameStarted && Input.GetMouseButtonDown(0))
		{
			circleController.Activation();
			handlePhysics = true;
			gameStarted = true;
		}
		if(handlePhysics)
		{
			deltaMovement.y -= gravityMagnitude*Time.deltaTime;

			if(Input.GetMouseButtonDown(0) && handleInput == true)
			{
				deltaMovement.y = jumpMagnitude;
			}
			else if(handleInput == false)transform.Rotate(0f,0f,90f*Time.deltaTime);

			transform.Translate(deltaMovement*Time.deltaTime,Space.World);
		}
		else if(reactivateControlls && Input.GetMouseButtonDown(0))
		{
			handlePhysics = true;
			reactivateControlls = false;
		}

	}


	private void Death()
	{
		if(handleInput)
		{
			deltaMovement.y = jumpMagnitude*0.8f;
			handleInput = false;
			circleController.DeActivation();
			StartCoroutine("SpriteColourAnim");
		}
	}

	private IEnumerator SpriteColourAnim()
	{
		float value = 1f;

		while(value >= 0.3f)
		{
			renderer.color = new Color(value,value,value);
			value -= 0.02f;
			yield return new WaitForSeconds(0.01f);
		}

		yield return new WaitForSeconds(1f);
		scoreManager.GameOver();
	}

	private IEnumerator ReActivatingControlls()
	{
		yield return new WaitForSeconds(timeGapForReactivation);
		reactivateControlls = true;
	}


	private void OnOutsideCounterTouch()
	{	
		if(touchDown == true && touchTop == true)
			return;
		// score

		// reactivate controlls
		touchTop = true;
		handlePhysics = false;
		StartCoroutine("ReActivatingControlls");
	}

	private void OnSuccessfullEscape()
	{
        scoreManager.In();
       // gis.In();
		touchDown = true;
	}

	private void OnSuccessfullEntry()
	{
		ScoreManager.S_Score += 1;
		int high = PlayerPrefs.GetInt("HIGH",0);
		PlayerPrefs.SetInt("HIGH", (high < ScoreManager.S_Score ? ScoreManager.S_Score : high));

        scoreManager.Out();
        //gis.Out();
		touchTop = false;
		touchDown = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("OuterCircle"))
		{
			Death();
		}
		else if(other.CompareTag("OutsideCounter"))
		{	
			OnOutsideCounterTouch();
		}else if(other.CompareTag("InsideCounter"))
		{
			if(touchDown == true && touchTop == true)
			{
				OnSuccessfullEntry();
			}
		}

	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "InsideCounter")
		{
			OnSuccessfullEscape();
		}
	
	}


}
