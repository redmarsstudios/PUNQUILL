using UnityEngine;
using System.Collections;

public class UseableObject : MonoBehaviour {

	public bool EnabledObject = false;
	public bool ActiveObject = false;
	public bool CanToggleOnOff = true;
	public bool AffectAllPlayers = false;
	public float cooldownsetting = 2;
	float cooldown = 2;
	public bool countdownToDeactivate = false;
	public float countdownsetting = 10;
	float countdown;
	public Component ObjectActivationScriptName;

	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;

		if (countdownToDeactivate) {
			if (countdown <= 0 && ActiveObject) {
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
		EnabledObject = true;
		Debug.Log ("Object is Enabled");
	}

	// Deactivate this object when the player exits the trigger
	void OnTriggerExit (Collider other) {
		EnabledObject = false;
		Debug.Log ("Object is Disabled");
	}

	void ToggleActivateThisObject () {
		if (EnabledObject == true) {
			if (ActiveObject == false) {
				Debug.Log ("Object is Activated");
				Debug.Log (ObjectActivationScriptName);
//				gameObject.transform.GetComponent(ObjectActivationScriptName) as ObjectActivationScriptName.ActivateThisObject ();
//				gameObject.transform.ObjectActivationScriptName.ActivateThisObject();
				ActiveObject = true;
			} else {
				if (CanToggleOnOff == true) {
//					gameObject.transform.GetComponent(ObjectActivationScriptName) as ObjectActivationScriptName.DeActivateThisObject ();
//					gameObject.transform.GetComponent<OAS_LoadLevel>().DeActivateThisObject ();
					Debug.Log ("Object is Activated");
				}
			}
			ActiveObject = false;
		}
	}
}
