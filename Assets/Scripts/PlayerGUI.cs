using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	//variables start*********************************************************************
	public Texture Crosshairs;
	private Rect rctWindow1;
	private Rect rctWindow2;
	private Rect rctWindow3;
	private Rect rctWindow4;
	private bool blnToggleState = false;
	private float fltSliderValue = 0.5f;
	private float fltScrollerValue = 0.5f;
	private Vector2 scrollPosition = Vector2.zero;
	public Texture tex;
	public Texture hoverTex;
	public int b = 50;
//	public Texture icon;
//	private NetworkManager NetworkManagerReference;
//	private bool spawnedPlayer = false;
	private GUIStyle buttonStyle2 = new GUIStyle();
	//variables end***********************************************************************

	void Start () {

	}

	void DefineStyles(){
		buttonStyle2 = GUI.skin.button;
		buttonStyle2.normal.textColor = Color.white;
		buttonStyle2.fontStyle = FontStyle.Bold;
		buttonStyle2.padding.top = 0;
		buttonStyle2.padding.left = 0;
		buttonStyle2.padding.right = 0;
		buttonStyle2.padding.bottom = 0;
		buttonStyle2.fixedWidth = b;
		buttonStyle2.fixedHeight = b;
		buttonStyle2.stretchWidth = true;
		buttonStyle2.stretchHeight = true;

	}//end define Styles

	void OnGUI() {
		DefineStyles ();


		GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		GUI.Box (new Rect(Screen.width/2 - Crosshairs.width/2, Screen.height/2 - Crosshairs.height/2, Screen.width, Screen.height), Crosshairs, GUIStyle.none);

		if (!tex )
			Debug.LogError("No texture found, please assign a texture on the inspector");

//		Debug.Log ("Before Loop");
		for (int i=0; i<(Screen.width-2*b); i=i+b) {
//			Debug.Log ("Start of Loop");
			if (GUI.Button(new Rect(b+i, Screen.height-b, Screen.width, Screen.height), tex, buttonStyle2)){
				Debug.Log("Clicked the image");
			}
		}
//		Debug.Log ("After Loop");

		
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}

}
