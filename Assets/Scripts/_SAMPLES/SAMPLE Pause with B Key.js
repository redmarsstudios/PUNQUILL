#pragma strict

private var showingCursor = false;
     
function Start(){ 
	Screen.showCursor = false;
}
     
function Update(){
    //check if pause button (escape key) is pressed
    if(Input.GetKeyDown("b")){
	    //check if game is already paused
	    if(showingCursor == true){
		    Screen.showCursor = false;
		    showingCursor = false;
		    Time.timeScale = 1;
		    AudioListener.volume = 1;
	    }
	    //else if game isn't paused, then pause it
	    else if(showingCursor == false){
		    Screen.showCursor = true;
		    showingCursor = true;
		    Time.timeScale = 0;
		    AudioListener.volume = 0;
	    }
    }
}