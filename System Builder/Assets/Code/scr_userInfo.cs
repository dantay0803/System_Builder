using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class scr_userInfo : MonoBehaviour {
    public EngAGe engage;
    //GameID
    private const int idSG = 248;
    //TextInputs
    public GameObject input_name;
    public GameObject input_age;
    public GameObject input_gender;
    public GameObject input_experience;
    //DisplayGameName
    public Text txt_title;
    //TextDisplaysOfTheQuestionsToAskTheUser
    public Text txt_QuestionName;
    public Text txt_QuestionAge;
    public Text txt_QuestionGender;
    public Text txt_QuestionExp;
    //GetUserName
    string userName;
    //GetUserAge
    string userAge;
    //ConvertAgeToInt
    int userAgeAsInt;
    //GetUserGender
    string userGender;
    //GetUserExperience
    string userExperience;

    //RunOnStartOfScene
    void Start(){
        // get the seriousGame object from engage 
        JSONNode SGdesc = engage.getSG()["seriousGame"];
        // display the title and description 
        txt_title.text = SGdesc["name"];
        //DisplayQuestionsForTheUser
        displayUserQuestions();
    }

    //DisplayQuestionForGuestLogIn
    void displayUserQuestions()
    {
        //CheckTheAmountOfLoopsForGettingTheQuestionForTheTextBox
        int loops = 0;

        //GetTheUserQuestions
        foreach (JSONNode param in engage.getParameters()){
            //KeepTrackOfTheAmountOfLoopsToPlaceTheCorrectQuestionInTheCorrectTextBox
            if (loops == 0){
                txt_QuestionName.text = param["question"];
            }
            else if (loops == 1){
                txt_QuestionAge.text = param["question"];
            }
            else if (loops == 2){
                txt_QuestionGender.text = param["question"];
            }
            else if(loops == 3){
                txt_QuestionExp.text = param["question"];
            }
            loops++;
        }
    }

    //GetUserName
    public void getUserName(){
        userName = input_name.GetComponent<InputField>().text;
    }

    //GetUserAge
    public void getUserAge(){
        userAge = input_age.GetComponent<InputField>().text;
        userAgeAsInt = int.Parse(userAge);
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
    public void saveUserInfoLocaly(){
        //SaveInfoToAssessmentEngine
        saveUserInfoToAssestmentEngine();
        //SaveDestailsLocaly
        PlayerPrefs.SetString("userName", userName);
        PlayerPrefs.SetInt("userAge", userAgeAsInt);
        PlayerPrefs.SetString("userGender", userGender);
        PlayerPrefs.SetString("userExp", userExperience);
        //Start Game
        StartCoroutine(engage.startGameplay(idSG, "scene_variables"));
    }

    //SaveUserInfoToServer
    void saveUserInfoToAssestmentEngine(){
        int loops = 0;
        //LoopThroughUserParamaters
        foreach(JSONNode param in engage.getParameters()){
            if(loops == 0){
                param.Add("value", input_name.GetComponent<InputField>().text);
            }
            else if(loops == 1){
                param.Add("value", input_age.GetComponent<InputField>().text);
            }
            else if(loops == 2){
                param.Add("value", input_gender.GetComponent<InputField>().text);
            }
            else if(loops == 3){
                param.Add("value", input_experience.GetComponent<InputField>().text);
            }
            loops++;
        }
    }
}
