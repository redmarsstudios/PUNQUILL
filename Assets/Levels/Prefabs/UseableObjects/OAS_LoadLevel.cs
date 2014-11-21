using UnityEngine;
using System.Collections;

public class OAS_LoadLevel : MonoBehaviour {
		
	public bool Enabled = false;
	public bool Activated = false;
	public bool CanToggleOnOff = true;
	public bool AffectAllPlayers = false;
	public float cooldownsetting = 2;
	float cooldown;
	public bool TimerDeactivate = false;
	public float CountdownSetting = 10;
	float countdown;

	void Start () {
		cooldown = cooldownsetting;
		countdown = CountdownSetting;
	}

	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		
		if (TimerDeactivate) {
		if (countdown <= 0 && Activated) {
				ToggleActivateThisObject ();
			} else {
				Debug.Log (cooldown);
			}
		}
		
		if(Input.GetButton("Use") && cooldown <= 0) {
			// Player wants to use the Object... So Toggle Object Activation.
			ToggleActivateThisObject ();
			cooldown = cooldownsetting;
		}
		
	}
		
		// Activate this object when the player enters the trigger
		void OnTriggerEnter(Collider other) {
			Enabled = true;
			Debug.Log ("Object is Enabled");
		}
		
		// Deactivate this object when the player exits the trigger
		void OnTriggerExit (Collider other) {
			Enabled = false;
			Debug.Log ("Object is Disabled");
		}
		
		void ToggleActivateThisObject () {
			if (Enabled) {
				if (!Activated) {
					Debug.Log ("Object is Activated");
					ActivateThisObject();
					Activated = true;
				} else {
					if (CanToggleOnOff) {
						Debug.Log ("Object is DeActivated");
						Activated = false;
						DeActivateThisObject();
					}
				}
			}
		}

	public string LevelToLoad = "Level Name Here";
	string Oops = "Oops! That Should Not Have Been Called.";
	public bool AllPlayers = false;

	public void ActivateThisObject () {
		if (AllPlayers) 
						ActivateForAllPlayers ();
				else
						ActivateForOnlyThisPlayer ();
		}

	public void DeActivateThisObject () {
		if (AllPlayers) 
			DeActivateForAllPlayers ();
		else
			DeActivateForOnlyThisPlayer ();
	}
	
	public void ActivateForOnlyThisPlayer () {
		Debug.Log ("Activated for this Player");
//		Application.LoadLevel(LevelToLoad);
		GameObject.Find ("_SCRIPTS").GetComponent<NetworkManager> ().LeaveScene ();
		PhotonNetwork.LoadLevel (1);
	}

	[RPC]
	public void ActivateForAllPlayers () {
		Debug.Log ("Activated for ALL Players");
		Debug.Log (Oops);
		Application.LoadLevel(LevelToLoad);
	}
	
	public void DeActivateForOnlyThisPlayer () {
		Debug.Log ("DeActivated for this Player");
		Debug.Log (Oops);
	}
	
	[RPC]
	public void DeActivateForAllPlayers () {
		Debug.Log ("DeActivated for ALL Players");
		Debug.Log (Oops);
	}
	
}
