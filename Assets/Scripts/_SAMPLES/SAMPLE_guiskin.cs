using UnityEngine;
using System.Collections;

public class SAMPLE_guiskin : MonoBehaviour
{
	//variables start*********************************************************************
	public GUISkin thisOrangeGUISkin;
	private Rect rctWindow1;
	private Rect rctWindow2;
	private Rect rctWindow3;
	private Rect rctWindow4;
	private bool blnToggleState = false;
	private float fltSliderValue = 0.5f;
	private float fltScrollerValue = 0.5f;
	private Vector2 scrollPosition = Vector2.zero;
	//variables end***********************************************************************
	
	void Awake()
	{
		if(Input.GetKeyDown(KeyCode.E)){
			rctWindow1 = new Rect(20, 20, 320, 400);
		}
		else{
			enabled = false;
		}
	}
	
	
	
	void OnGUI()
	{
		GUI.skin = thisOrangeGUISkin;
		rctWindow1 = GUI.Window(0, rctWindow1, DoMyWindow, "OPTIONS", GUI.skin.GetStyle("window"));
	}
	
	
	
	void DoMyWindow(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Label("Im a Label");
		GUILayout.Space(8);
		GUILayout.Button("Im a Button");
		GUILayout.TextField("Im a textfield");
		GUILayout.TextArea("Im a textfield\nIm the second line\nIm the third line\nIm the fourth line");
		blnToggleState = GUILayout.Toggle(blnToggleState, "Im a Toggle button");
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		//Sliders
		GUILayout.BeginHorizontal();
		fltSliderValue = GUILayout.HorizontalSlider(fltSliderValue, 0.0f, 1.1f, GUILayout.Width(128));
		fltSliderValue = GUILayout.VerticalSlider(fltSliderValue, 0.0f, 1.1f, GUILayout.Height(50));
		GUILayout.EndHorizontal();
		//Scrollbars
		GUILayout.BeginHorizontal();
		fltScrollerValue = GUILayout.HorizontalScrollbar(fltScrollerValue, 0.1f, 0.0f, 1.1f, GUILayout.Width(128));
		fltScrollerValue = GUILayout.VerticalScrollbar(fltScrollerValue, 0.1f, 0.0f, 1.1f, GUILayout.Height(90));
		GUILayout.Box("Im\na\ntest\nBox");
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUI.DragWindow();
	}
	
}