using UnityEngine;
using System.Collections;

public class scr_loadUser : MonoBehaviour {

    public EngAGe engage;
    //GameID
    private const int idSG = 215;

    void Awake()
    {
        //GetGameInfo
        StartCoroutine(engage.getGameDesc(idSG));
    }


    // Use this for initialization
    void Start () {
        //LogUserInAsGuest
        StartCoroutine(engage.guestLogin(idSG, "scene_logIn", "scene_mainMenu"));
    }
}
