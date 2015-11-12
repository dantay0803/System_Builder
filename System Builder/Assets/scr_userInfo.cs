using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_userInfo : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 213;

    //TextInputs
    public GameObject input_name;
    public GameObject input_age;
    public GameObject input_gender;
    public GameObject input_experience;

    //GetUserName
    string userName;
    //ConvertAgeToInt
    string userAge;
    //GetUserGender
    string userGender;
    //GetUserExperience
    string userExperience;

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
    public void getUserExperience()
    {
        userExperience = input_experience.GetComponent<InputField>().text;
    }


    //SaveUserInfoBetweenLevels
    public void saveUserInfo(){
        JSONNode param = engage.getParameters();
        param.Add("Name", userName);

        PlayerPrefs.SetString("userName", userName);
        PlayerPrefs.SetString("userAge", userAge);
        PlayerPrefs.SetString("userGender", userGender);
        PlayerPrefs.SetString("userExp", userExperience);
        //Start Game
        Application.LoadLevel("scene_variables");
    }
}
