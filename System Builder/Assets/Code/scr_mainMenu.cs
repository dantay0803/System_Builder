using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_mainMenu : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 231;
    //DisplayGameName
    public Text txt_title;

    void Awake()
    {
        //GetGameInfo
        StartCoroutine(engage.getGameDesc(idSG));
    }


    //LogPlayerInAsGuestWithEngage
    public void startGame(){
        //LogUserInAsGuest
        StartCoroutine(engage.guestLogin(idSG, "scene_mainMenu", "scene_playerInfo"));
    }
}
