using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {


	// Update is called once per frame
	void OnGUI () {
		if(Input.GetButtonDown("Menu")) {
			// Bring Up the Exit Game Menu.
			Debug.Log ("Should be opening a menu");
			DrawMainMenu ();
		}
	
	}

	void DrawMainMenu () {
		Debug.Log ("Starting OpenMenu");
		GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
		GUI.backgroundColor = Color.yellow;
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button("Exit Game") ) {
			Application.Quit();
		}		
		if( GUILayout.Button("Exit Level") ) {
			Application.LoadLevel("0_PlayerRoom");
		}		
		if( GUILayout.Button("Return to Game") ) {

		}
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
