using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_userInfo : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 215;
    //TextInputs
    public GameObject input_name;
    public GameObject input_age;
    public GameObject input_gender;
    public GameObject input_experience;
    //DisplayGameName
    public Text txt_title;
    //GetUserName
    string userName;
    //ConvertAgeToInt
    string userAge;
    //GetUserGender
    string userGender;
    //GetUserExperience
    string userExperience;

    void Start()
    {
        // get the seriousGame object from engage 
        JSONNode SGdesc = engage.getSG()["seriousGame"];
        // display the title and description 
        txt_title.text = SGdesc["name"];
    }

    //GetUserName
    public void getUserName(){
        userName = input_name.GetComponent<InputField>().text;
    }

    //GetUserAge
    public void getUserAge(){
        userAge = input_age.GetComponent<InputField>().text;
    }

    //GetUserName
    public void getUserGender(){
        userGender = input_gender.GetComponent<InputField>().text;
    }

    //GetUserName
    public void getUserExperience(){
        userExperience = input_experience.GetComponent<InputField>().text;
    }


    //SaveUserInfoBetweenLevels
    public void saveUserInfo(){
        PlayerPrefs.SetString("userName", userName);
        PlayerPrefs.SetString("userAge", userAge);
        PlayerPrefs.SetString("userGender", userGender);
        PlayerPrefs.SetString("userExp", userExperience);
        //Start Game
        Application.LoadLevel("scene_variables");
    }



    public void gotoMenu()
    {
        Application.LoadLevel("scene_mainMenu");
    }
}
