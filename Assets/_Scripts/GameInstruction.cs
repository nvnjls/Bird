using UnityEngine;
using System.Collections;

public class GameInstruction : MonoBehaviour {

    public GameObject GetOut;
    public GameObject GetIn;
    public GameInstruction()
    {
        GetIn = (GameObject) Resources.Load("Instruction");
    }
    public void Out()
    {
        GetIn.SetActive(false);
        GetOut.SetActive(true);
    }

    public void In()
    {
        GetIn.SetActive(true);
        GetOut.SetActive(false);
    }
}
