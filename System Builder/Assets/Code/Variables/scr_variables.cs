using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_variables : MonoBehaviour {

    public EngAGe engage;
    //GameID
    private const int idSG = 248;

    //CodeInputs
    public GameObject input_code;
    //DisplayShipMessage
    public Text txt_shipMessage;
    //DisplayInstructions
    public Text txt_insturctions;
    //HoldTheCode
    string usersEnteredCode = "";
    //EnusreTheUserHasTheCorrectAmountOfCodeBeforeMovingOn
    int codeCorrect = 0;
    //GetThePlayersName
    string playerName;
    //UsedToEnsurePlayerHasCompletedStringSection
    bool nameEntered = false;
    //UsedToEnsurePlayerHasCompletedNameLengthSection
    bool nameLengthEntered = false;
    //DisplayFeedbackToUser
    public Text txt_feedback;



    JSONNode feedback;

    void Start(){
        //GetFeedback
        feedback = engage.getFeedback()["seriousGame"];
    }


    //GetUserName
    public void getCode(){
        usersEnteredCode = input_code.GetComponent<InputField>().text;
    }

    //CheckTheUsersCodeIsRight
    public void checkCode(){
        //ResetPlayerScore
        codeCorrect = 0;
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //IfPlayerEntredNameSectionNotCompleteCheckIt
        if(!nameEntered){
            nameInputChallenge();
        }
        //IfNameLenfthEnteredNotCompleteButNameEnteredIsCheckNameLengthSection
        else if(nameEntered && !nameLengthEntered){
            nameLengthChallenge();
        }
    }

    //CheckThePlayerName/LengthStage
    void nameLengthChallenge(){
        //CheckTheUserHasTypedTheCorrectVariableName
        if(usersEnteredCode.Contains("name")){
            codeCorrect++;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Have you used the correct variable name?");
        }
        //CheckFor.LengthWell 
        if(usersEnteredCode.Contains("length")){
            codeCorrect++;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Did you type the property correctly?");
        }
        //CheckForSyntax
        if(usersEnteredCode.Contains(".") && usersEnteredCode.Contains("()") && usersEnteredCode.Contains(";")){
            codeCorrect += 3;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Check your syntax");
        }
        //EnsureUserDoesNotTypeAnySpaces
        if (usersEnteredCode.Contains(" "))
        {
            codeCorrect = 0;
            Debug.Log("Remove any spaces");
        }
        //CheckThatAllTheCodeHasBeenProperlyInput
        if(codeCorrect >= 5){
            //IfItHasDisplayCoorrectCodeMessageToAllowThePlayerToContinue
            correctCode();
            //SetSectionAsComplete
            nameLengthEntered = true;
        }
    }

    //setUpStringLengthSection
    void setUpStringLengthSection(){
        //StoryMessage
        txt_shipMessage.text = "It’s nice to meet you " + playerName +
                                ". If you remember the damage of your bullet is the same as your rank but that part of my system is gone so you will need to make it the length of your name.";

        //CodingInsturctions
        txt_insturctions.text = "In C# you can use the  .length();  property to get the length of a string, now get the length of your name using the string created last time." +
                                "/n Tip the last string was called name.";
    }

    //CheckThePlayerEnterNameStage
    void nameInputChallenge(){
        //CheckTheUserHasDefinedAStringVariable
        if (usersEnteredCode.Contains("string")){
            codeCorrect++;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Have you defined the variable type?");
        }
        //CheckForVariableName
        if (usersEnteredCode.Contains("name")){
            codeCorrect++;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Did you set the name of the string variable?");
        }
        //CheckForSyntax
        if (usersEnteredCode.Contains("=") && usersEnteredCode.Contains(";") && usersEnteredCode.Contains("\""))
        {
            codeCorrect += 3;
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            codeCorrect = 0;
            Debug.Log("Check your syntax");
        }
        //CheckThatAllTheCodeHasBeenProperlyInput
        if (codeCorrect >= 5){
            //GetTheUsersName
            getUserName();
            //IfItHasDisplayCoorrectCodeMessageToAllowThePlayerToContinue
            correctCode();
            //SetSectionAsComplete
            nameEntered = true;
            //SetUpNextSection
            setUpStringLengthSection();
        }
    }

    //GetTheUsersNameFromTheInputedString
    void getUserName(){
        playerName = PlayerPrefs.GetString("userName");
    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void correctCode()
    {
        Debug.Log("Well done");
    }
}
