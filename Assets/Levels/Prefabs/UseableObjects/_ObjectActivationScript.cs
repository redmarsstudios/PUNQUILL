using UnityEngine;
using System.Collections;

public class ObjectActivationScript : MonoBehaviour {

/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
*/

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
