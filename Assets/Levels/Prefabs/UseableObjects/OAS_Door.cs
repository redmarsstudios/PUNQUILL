using UnityEngine;
using System.Collections;

public class OAS_Door : MonoBehaviour {

/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
*/
	string Oops = "Oops! That Should Not Have Been Called.";


	public void ActivateForOnlyThisPlayer () {
		Debug.Log ("Activated for this Player");
	}

	[RPC]
	public void ActivateForAllPlayers () {
		Debug.Log ("Activated for ALL Players");
	}
	
	public void DeActivateForOnlyThisPlayer () {
		Debug.Log ("DeActivated for this Player");
	}
	
	[RPC]
	public void DeActivateForAllPlayers () {
		Debug.Log ("DeActivated for ALL Players");
	}
	
}
