using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_mainMenu : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 259;
    //DisplayGameName
    public Text txt_title;
    //GameDescriptionObject
    public GameObject obj_gameDescription;
    public Text txt_gameDescriptionTitle;
    public Text txt_gameDescriptionDescription;
    //CheckIfGameDescriptionHasBeenWrote
    bool descriptionWrote = false;
    
    void Awake(){
        //GetGameInfo
        StartCoroutine(engage.getGameDesc(idSG));
        //PlayMenuMusic
        scr_soundManager.instance.playMenuMusic();
    }

    void Start(){
        //HideGameDescriptionObjectOnStartUp
        hideDescription();
    }

    //LogPlayerInAsGuestWithEngage
    public void startGame(){
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
        //LogUserInAsGuest
        StartCoroutine(engage.guestLogin(idSG, "scene_mainMenu", "scene_playerInfo"));
    }

    //ShowGameDescription
    public void showDescription(){
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
        //HideGameObjectOnStartUp
        obj_gameDescription.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        //WriteGameInfo
        writeDescription();
    }

    //HideGameDescription
    public void hideDescription(){
        //OnlyPlayButtonClickOnceTheGameDexcirptionHasBeenWritten
        if (descriptionWrote){
            //PlayButtonClick
            scr_soundManager.instance.playButtonClick();
        }
        //HideGameObjectOnStartUp
        obj_gameDescription.transform.position = new Vector2(Screen.width / 2 - 1000, Screen.height / 2);
    }

    //DisplayDescriptionTextFromAssessmentEngine
    public void writeDescription(){
        //GetDescriptionHasNotBeenWrittenAlready
        if (!descriptionWrote) {
            //GetDescriptionObjectFromAssessmentEngine
            JSONNode SGdesc = engage.getSG()["seriousGame"];
            //DisplayInformationInTextBoxes
            txt_gameDescriptionTitle.text = SGdesc["name"];
            txt_gameDescriptionDescription.text = SGdesc["description"];
            //LogDescriptionAsWritten
            descriptionWrote = true;
        }
    }

}
