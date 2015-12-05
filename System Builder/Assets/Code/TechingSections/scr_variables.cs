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
    private const int idSG = 259;
    //CodeInputs
    public GameObject input_code;
    //DisplayShipMessage
    public Text txt_shipMessage;
    //DisplayInstructions
    public Text txt_insturctions;
    //HoldTheCode
    string usersEnteredCode = "";
    //GetThePlayersName
    string playerName;
    //UsedToEnsurePlayerHasCompletedStringSection
    bool nameEntered = false;
    //UsedToEnsurePlayerHasCompletedNameLengthSection
    bool nameLengthEntered = false;
    //UsedToEnusreIntSectionHasBeenCompleted
    bool intSectionEntered = false;
    //UsedToEnusreFloatSectionHasBeenCompleted
    bool floatSectionEntered = false;

    void Start(){
        //PlayGameMusic
        scr_soundManager.instance.playGameMusic();
    }

    //GetUserCode
    public void getCode(){
        usersEnteredCode = input_code.GetComponent<InputField>().text;
    }

    //CheckTheUsersCodeIsRight
    public void checkCode(){
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //IfNameEnteredChallnegeNotCompleteRunIt
        if(!nameEntered && !nameLengthEntered && !intSectionEntered && !floatSectionEntered)
        {
            nameInputChallenge();
        }
        //IfNameEnteredChallegngeCompleteAndNameLenegthChallanegNotCompleteRunIt
        else if(nameEntered && !nameLengthEntered && !intSectionEntered && !floatSectionEntered)
        {
            nameLengthChallenge();
        }
        //IfNameLengthChallengeCompleteAndIntSectionNotComplete
        else if(nameEntered && nameLengthEntered && !intSectionEntered && !floatSectionEntered)
        {
            intChallenge();
        }
        //IfIntChallageCompleteAndFloatSectionNotComplete
        else if(nameEntered && nameLengthEntered && intSectionEntered && !floatSectionEntered){
            floatChallenge();
        }
    }

    //CheckThePlayerEnterNameStage
    void nameInputChallenge(){
        //CheckTheUserHasDefinedAStringVariable
        if (usersEnteredCode.Contains("string")){
            //CheckForVariableName
            if (usersEnteredCode.Contains("name")){
                //CheckForSyntax
                if (usersEnteredCode.Contains("=") && usersEnteredCode.Contains(";") && Regex.Matches(usersEnteredCode, "\"").Count == 2)
                {
                    //ShowQuestionCorrectMessage
                    scr_feedbackDisplay.instance.MessageQuesstionCorrect();
                    //GetTheUsersName
                    getUserName();
                    //SetSectionAsComplete
                    nameEntered = true;
                    //SetUpNextSection
                    setUpStringLengthSection();
                }
                //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
                else{
                    //DisplayFeedback
                    scr_feedbackDisplay.instance.MessageCheckSyntax();
                }
            }
            //IfNotDsiaplyTipMessgae
            else{
                //DisplayFeedback
                scr_feedbackDisplay.instance.MessageVariableName();
            }
        }
        //IfNotResetTheScoreForCheckingTheCodeAndDsiaplyTipMessgae
        else{
            //DisplayFeedback
            scr_feedbackDisplay.instance.MessageVariableType();
        }        
    }

    //CheckThePlayerNameLengthStage
    void nameLengthChallenge(){
        //CheckTheUserHasTypedTheCorrectVariableName
        if (usersEnteredCode.Contains("name")){
            //CheckFor.LengthWell 
            if (usersEnteredCode.Contains("length")){
                //CheckForSyntax
                if (usersEnteredCode.Contains(".") && usersEnteredCode.Contains("()") && usersEnteredCode.Contains(";")){
                    //EnsureUserDoesNotTypeAnySpaces
                    if (usersEnteredCode.Contains(" ")){
                        //DisplayFeedback
                        scr_feedbackDisplay.instance.MessageCheckWhitespace();
                    }
                    //IfNoSpacesFoundMarkTheQuestionAsComplete
                    else{
                        //ShowQuestionCorrectMessage
                        scr_feedbackDisplay.instance.MessageQuesstionCorrect();
                        //SetSectionAsComplete
                        nameLengthEntered = true;
                        //setUpStringLengthSection
                        setUpIntMathSection();
                    }
                }
                //IfNotDsiaplyTipMessgae
                else{
                    //DisplayFeedback
                    scr_feedbackDisplay.instance.MessageCheckSyntax();
                }
            }
            //IfNotDsiaplyTipMessgae
            else{
                //DisplayFeedback
                scr_feedbackDisplay.instance.MessageCheckProperty();
            }
        }
        //IfNotDsiaplyTipMessgae
        else{
            //DisplayFeedback
            scr_feedbackDisplay.instance.MessageVariableName();
        }
    }

    //CheckIntStage
    void intChallenge(){
        //CheckTwoIntVariablesHaveBeenDefined
        if (Regex.Matches(usersEnteredCode, "int").Count == 2){
            //CheckVariableNamesHaveBeenDefined
            if (usersEnteredCode.Contains("number1") && usersEnteredCode.Contains("number2")){
                //CheckForAdditionSymbol
                if (usersEnteredCode.Contains("+")){
                    //CheckForSyntax
                    if (Regex.Matches(usersEnteredCode, "=").Count == 2 && Regex.Matches(usersEnteredCode, ";").Count >= 2){
                        //setUpFloatSection
                        setUpFloatMathSection();
                        //MarkSectionAsComplete
                        intSectionEntered = true;
                        //ShowQuestionCorrectMessage
                        scr_feedbackDisplay.instance.MessageQuesstionCorrect();
                    }
                    //DisplayFeedback
                    else{
                        scr_feedbackDisplay.instance.MessageCheckSyntax();
                    }
                }
                //DisplayFeedbackIfWrong
                else{
                    scr_feedbackDisplay.instance.MessageDeclareCheckMathSymbol();
                }

            }
            //DisplayFeedbackIfWrong
            else{
                scr_feedbackDisplay.instance.MessageVariableName();
            } 
        }
        //DisplayFeedbackIfWrong
        else{
            scr_feedbackDisplay.instance.MessageDeclareAllVariables();
        }
    }

    //CheckFloatStage
    void floatChallenge(){
        //CheckBothFloatsAreDefined
        if (Regex.Matches(usersEnteredCode, "float").Count == 2){
            //CheckVariablesAreDecleared
            if(usersEnteredCode.Contains("number1") && usersEnteredCode.Contains("number2")){
                //CheckTheNumbersAreBeingMultiplied
                if (usersEnteredCode.Contains("*")){
                    //CheckCorretValuesEntered
                    if (usersEnteredCode.Contains("5.63") && usersEnteredCode.Contains("8.25")){
                        //CheckForAllTheCorrectSyntax
                        if (Regex.Matches(usersEnteredCode, "float").Count == 2 && Regex.Matches(usersEnteredCode, "=").Count == 2 && Regex.Matches(usersEnteredCode, ";").Count >= 2){
                            //MarkVairableSectionAsComplete
                            sectionComplete();
                        }
                        //DisplayFeedbackIfWrong
                        else{
                            scr_feedbackDisplay.instance.MessageCheckSyntax();
                        }
                    }
                    //DisplayFeedbackIfWrong
                    else{
                        scr_feedbackDisplay.instance.MessageDeclareCheckVariableValues();
                    }
                }
                //DisplayFeedbackIfWrong
                else{
                    scr_feedbackDisplay.instance.MessageDeclareCheckMathSymbol();
                    
                }
            }
            //DisplayFeedbackIfWrong
            else{
                scr_feedbackDisplay.instance.MessageVariableName();
            }
        }
        //DisplayFeedbackIfWrong
        else{
            scr_feedbackDisplay.instance.MessageDeclareAllVariables();
        }
    }

    //setUpStringLengthSection
    void setUpStringLengthSection(){
        //StoryMessage
        txt_shipMessage.text = "It’s nice to meet you " + playerName +
                                ". If you remember the damage of your bullet is the same as your rank but that part of my system is gone so you will need to make it the length of your name.";
        //CodingInsturctions
        txt_insturctions.text = "In C# you can use the  .length();  property to get the length of a string, now get the length of your name using the string created last time." +
                                "\n" + "Tip: the last string was called name.";
        //DeleteOldUserCode
        input_code.GetComponent<InputField>().text = "";
    }

    //setUpIntSection
    void setUpIntMathSection(){
        //StoryMessage
        txt_shipMessage.text = "In order for you to access more of my systems I’m going to need your help to find out what systems are still working in order to do this we need to start with the basics."
                                + " So let’s start with some mathematical questions, start by adding any two numbers together.";
        //CodingInsturctions
        txt_insturctions.text = "Numbers are stored as an int (Integer), so define two of them similar what you did with your name in the first question. Use number1 and number2 as your variable names." +
                                "\n" + "Tip: Addition is done by number1 + number2.";
        //DeleteOldUserCode
        input_code.GetComponent<InputField>().text = "";
    }

    //setUpFloatSection
    void setUpFloatMathSection(){
        //StoryMessage
        txt_shipMessage.text = "Now it’s time to check the rest of my systems only this time there is a small twist, the checks need to be more precise so you will need to use two Floating point numbers"
                                + " and multiply them. The two numbers to use are 5.63 and 8.25.";
        //CodingInsturctions
        txt_insturctions.text = "Floating point numbers are defined as a float." + "\n" + "Tip: The start (*) character is used to multiple numbers.";
        //DeleteOldUserCode
        input_code.GetComponent<InputField>().text = "";
    }

    //GetTheUsersNameFromTheInputedString
    void getUserName(){
        playerName = PlayerPrefs.GetString("userName");
    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void sectionComplete(){
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("varibaleSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.variableSectionFinished = true;
    }
}