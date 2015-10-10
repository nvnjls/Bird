using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public void OnReplayButtonClicked()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnHomeButtonClicked()
	{
		Application.LoadLevel(0);
	}

}
