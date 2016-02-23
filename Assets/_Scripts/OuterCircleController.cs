using UnityEngine;
using System.Collections;

public class OuterCircleController : MonoBehaviour {

	public float rotationSpeed = 450f;

	private int activator = 0;

	void Update () {
		transform.Rotate(0f,0f,-rotationSpeed*Time.deltaTime*activator);
	}

	public void Activation()
	{
		activator = 1;
	}

	public void DeActivation()
	{
		activator = 0;
	}
}
