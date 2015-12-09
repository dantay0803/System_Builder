using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_collisions : MonoBehaviour
{

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

    //GetUserCode
    public void getCode()
    {
        usersEnteredCode = input_code.GetComponent<InputField>().text;
    }

    //CheckTheUsersCodeIsRight
    public void checkCode(){
        //PlayButtonClick
        //scr_soundManager.instance.playButtonClick();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //GetUserInput
        getCode();
        //CheckCodeIsCorrect
        functioncollide();
    }

    //CheckAnswer
    void functioncollide(){
        if (usersEnteredCode.Contains("void") && usersEnteredCode.Contains("collider2d") && Regex.Matches(usersEnteredCode, "object").Count >= 2 && usersEnteredCode.Contains("(") && usersEnteredCode.Contains(")"))
        {
            if (usersEnteredCode.Contains("ontriggerenter2d")){
                if (usersEnteredCode.Contains("if") && usersEnteredCode.Contains("obj_bullet(clone)") && usersEnteredCode.Contains("object.gameobject.name") && usersEnteredCode.Contains("this.gameobject") && usersEnteredCode.Contains("(") && usersEnteredCode.Contains(")"))
                {
                    if (usersEnteredCode.Contains("destroy")){
                        if (Regex.Matches(usersEnteredCode, "{").Count == 2 && Regex.Matches(usersEnteredCode, "}").Count == 2 && Regex.Matches(usersEnteredCode, "\"").Count == 2 && Regex.Matches(usersEnteredCode, ";").Count == 2){
                            sectionComplete();
                        }
                        else{
                            scr_feedbackDisplay.instance.MessageCheckSyntax();
                        }
                    }
                    else{
                        scr_feedbackDisplay.instance.MessageDestroyPropertyCheck();
                    }
                }
                else{
                    scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                }
            }
            else{
                scr_feedbackDisplay.instance.MessageFunctionNameCheck();
            }
        }
        else{
            scr_feedbackDisplay.instance.MessageFunctionDeclareCheck();
        }
    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void sectionComplete()
    {
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("collisionsSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.collisionsSectionFinished = true;
    }
}