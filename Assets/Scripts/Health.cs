using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float hitPoints = 100f;
	public bool isAPlayer = false;
	GameObject _SCRIPTS;
	GameObject standbyCamera;
	float currentHitPoints;

	// Use this for initialization
	void Start () {
		currentHitPoints = hitPoints;
	}

	[RPC]
	public void TakeDamage(float amt) {
		currentHitPoints -= amt;

		if(currentHitPoints <= 0) {
			Die();
		}
	}

	void Die() {
		if( GetComponent<PhotonView>().instantiationId==0 ) {
			Destroy(gameObject);
		}
		else {
			// unknown test for whether this is a Master Client
			if( PhotonNetwork.isMasterClient ) {
				// test if this is a player 
				if (isAPlayer == true){
					Debug.Log ("Is a Player");
					/* Should re-enable the StandbyCamera for this client. 
					 * Has to call the public on the _SCRIPT object's Network Manager bc 
					 * GameObject.Find & related searches do not work on Inactive objects. 
					 */
					GameObject.Find("_SCRIPTS").GetComponent<NetworkManager>().TurnOnStandbyCamera();
					Debug.Log ("StandybyCamera should be ON!");
				}
			}
			// turn off the player camera
			gameObject.transform.FindChild("Main Camera").gameObject.SetActive(false);
			Debug.Log ("MainCamera (Player Camera) should be ON!");
			// destroy the object
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
