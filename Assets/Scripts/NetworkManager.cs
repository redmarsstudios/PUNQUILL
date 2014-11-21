using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	public bool offlineMode = false;

	public bool connecting = false;
	public bool joinedRoom = false;
	public bool spawnedPlayer = false;
	public bool ShowMainMenu = false;
	private GUIStyle buttonStyle1 = new GUIStyle();

	List<string> chatMessages;
	int maxChatMessages = 5;

	// Use this for initialization
	void Start () {
		connecting = false;
		joinedRoom = false;
		spawnedPlayer = false;
		ShowMainMenu = false;

		Screen.showCursor = true;
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Awesome Dude");
		chatMessages = new List<string>();

	}
	
	void Update () {
		// When the player presses the Escape Key, Or 'Menu' Input, Toggle the Menu
		if (Input.GetButtonDown("Menu")) {
			if (ShowMainMenu) {	
				ShowMainMenu = false;
				Screen.showCursor = false;
			} else {
				ShowMainMenu = true;
				Screen.showCursor = true;
			}
		}

	}

	void OnDestroy() {
		PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
	}

	public void AddChatMessage(string m) {
		GetComponent<PhotonView>().RPC ("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
	}

	[RPC]
	void AddChatMessage_RPC(string m) {
		while(chatMessages.Count >= maxChatMessages) {
			chatMessages.RemoveAt(0);
		}
		chatMessages.Add(m);
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings( "MultiFPS v003" );
	}
	 
	void DefineStyles(){
		buttonStyle1 = GUI.skin.button;
		buttonStyle1.normal.textColor = Color.white;
		buttonStyle1.fontStyle = FontStyle.Bold;
		buttonStyle1.padding.top = 0;
		buttonStyle1.padding.left = 0;
		buttonStyle1.padding.right = 0;
		buttonStyle1.padding.bottom = 0;
		buttonStyle1.fixedWidth = 200;
		buttonStyle1.fixedHeight = 30;
		buttonStyle1.stretchWidth = true;
		buttonStyle1.stretchHeight = true;
		buttonStyle1.alignment = TextAnchor.MiddleCenter;
	}//end define Styles

	void OnGUI() {

		DefineStyles ();

		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString() );


		// This Section will pull up the Main Menu whenever the escape key is pressed.
		if (ShowMainMenu) {

			Screen.showCursor = true;

			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			if( GUILayout.Button("Exit Game") ) {
				//			if( GUI.Button( new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height ) , "Single Player", buttonStyle1) ) {
				Debug.Log ("Exit Game");
				Application.Quit();
			}
			if( GUILayout.Button("Exit Level") ) {
				//			if( GUI.Button( new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height ) , "Single Player", buttonStyle1) ) {
				Debug.Log ("Exit Level & Load Main Level");
				ShowMainMenu = false;
				LeaveScene();
				PhotonNetwork.LoadLevel (0);
				// Application.LoadLevel("0_PlayerRoom");
			}
			if( GUILayout.Button("Return to Game") ) {
				//			if( GUI.Button( new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height ) , "Single Player", buttonStyle1) ) {
				Debug.Log ("Return to level");
				ShowMainMenu = false;
				Screen.showCursor = false;
			}
			if( GUILayout.Button("Test") ) {
				//			if( GUI.Button( new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height ) , "Single Player", buttonStyle1) ) {
				Debug.Log ("Tested");
				ShowMainMenu = false;
				Screen.showCursor = false;
			}

			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();

		}

		if(ShowMainMenu || !spawnedPlayer)
			Screen.showCursor = true;
		else
			Screen.showCursor = false;

		if(PhotonNetwork.connected == false && connecting == false ) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Username: ");
			PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
			GUILayout.EndHorizontal();

			if( GUILayout.Button("Single Player") ) {
//			if( GUI.Button( new Rect (Screen.width/2, Screen.height/2, Screen.width, Screen.height ) , "Single Player", buttonStyle1) ) {
				connecting = true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
			}

			if( GUILayout.Button("Multi Player") ) {
				connecting = true;
				Connect ();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		if(PhotonNetwork.connected == true && connecting == false) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			foreach(string msg in chatMessages) {
				GUILayout.Label(msg);
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();

		}

		if (joinedRoom && !(spawnedPlayer)) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			if (PhotonNetwork.player.name == null) {
				PhotonNetwork.player.name = "No Name";
			}
			GUILayout.Label(PhotonNetwork.player.name);
			GUILayout.EndHorizontal();

			if( GUILayout.Button("SpawnPlayer") ) {
				connecting = true;
				SpawnMyPlayer();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

	}

	void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom( null );
	}

	void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");

		connecting = false;
		joinedRoom = true;

	}

	void SpawnMyPlayer() {
		AddChatMessage("Spawning player: " + PhotonNetwork.player.name);
		spawnedPlayer = true;

		if(spawnSpots == null) {
			Debug.LogError ("WTF?!?!?");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots[ Random.Range (0, spawnSpots.Length) ];
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);

		//((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("PlayerMovement")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("PlayerShooting")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("PlayerGUI")).enabled = true;
		myPlayerGO.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}

	public void TurnOnStandbyCamera () {
		standbyCamera.SetActive(true);
		spawnedPlayer = false;
	}

	public void LeaveScene () {
		PhotonNetwork.LeaveRoom ();
		spawnedPlayer = false;
		joinedRoom = false;
		OnJoinedLobby();
	}
}

