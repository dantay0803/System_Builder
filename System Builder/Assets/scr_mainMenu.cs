using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_mainMenu : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 215;
    //DisplayGameName
    public Text txt_title;

    void Start(){
        // get the seriousGame object from engage 
        JSONNode SGdesc = engage.getSG()["seriousGame"];
        // display the title and description 
        txt_title.text = SGdesc["name"];
    }

    //LogPlayerInAsGuestWithEngage
    public void startGame(){
        Application.LoadLevel("scene_playerInfo");
    }
}
