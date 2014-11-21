using UnityEngine;
using System.Collections;

public class DeathVolume : MonoBehaviour {

	Transform doomedTransform;
	WeaponData weaponData;

	// Destroy everything that enters the trigger
	void OnTriggerEnter(Collider other) {
		// Instead of calling this line:
		// 		Destroy(other.gameObject);
		// We will have the players take 1 million damage to thier health

		// Get the Health script Component off of the Player that as entered the Death Volume 
		doomedTransform = other.gameObject.transform;
		Health h = doomedTransform.GetComponent<Health>();
		Debug.Log ("Selected  "+h);

		// make sure we select that the parent Transform (with the health component)
		while(h == null && doomedTransform.parent) {
			doomedTransform = doomedTransform.parent;
			h = doomedTransform.GetComponent<Health>();
			Debug.Log ("Selected  "+h);
		}
		
		// Once we reach here, doomedTransform may no longer be the doomedTransform we started with!
		
		if(h != null) {
			// This next line is the equivalent of calling:
			//    				h.TakeDamage( damage );
			// Except more "networky"
			PhotonView pv = h.GetComponent<PhotonView>();
			if(pv==null) {
				Debug.LogError("Freak Out! Designer Goofed: No Photon View is Present");
			}
			else {
				Debug.Log ("DN Calls TakeDamage");
				weaponData = gameObject.GetComponentInChildren<WeaponData>();
				Debug.Log ("DN Should Have WeaponData defined as >> " + weaponData + " << which = " + weaponData.damage);
				h.GetComponent<PhotonView>().RPC ("TakeDamage", PhotonTargets.AllBuffered, weaponData.damage);
				Debug.Log ("DN TakeDamage Called Player should be dead");
			}
		}
	}

}
