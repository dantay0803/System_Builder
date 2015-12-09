using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_spawning : MonoBehaviour
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
    public void checkCode()
    {
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
        //GetUserCode
        getCode();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //CheckUserAnswer
        checkQuestion1();
    }

    //CheckUserAnswer
    void checkQuestion1(){
        if (usersEnteredCode.Contains("gameobject")){
            if(usersEnteredCode.Contains("public") && usersEnteredCode.Contains("obj_bullet")){
                if (usersEnteredCode.Contains("void")){
                    if(usersEnteredCode.Contains("createBullet")){
                        if (usersEnteredCode.Contains("if")){
                            if(usersEnteredCode.Contains("input.getkey")){
                                if(usersEnteredCode.Contains("Instantiate") && Regex.Matches(usersEnteredCode, "obj_bullet").Count == 2 && usersEnteredCode.Contains("new Vector2") && Regex.Matches(usersEnteredCode, "this.transform.position.").Count == 2 && usersEnteredCode.Contains("x") && usersEnteredCode.Contains("y") && usersEnteredCode.Contains("Quaternion.identity")){
                                    if (usersEnteredCode.Contains("sapce")){
                                        if (Regex.Matches(usersEnteredCode, ";").Count == 2 && Regex.Matches(usersEnteredCode, "(").Count == 5 && Regex.Matches(usersEnteredCode, ")").Count == 5 && Regex.Matches(usersEnteredCode, "{").Count == 2 && Regex.Matches(usersEnteredCode, "}").Count == 2 && Regex.Matches(usersEnteredCode, ",").Count == 3 && Regex.Matches(usersEnteredCode, "\"").Count == 2){
                                            //MarkAnswerAsCorrect
                                            sectionComplete();
                                        }
                                        //DisplayErrorMessage
                                        else{
                                            scr_feedbackDisplay.instance.MessageCheckSyntax();
                                        }
                                    }
                                    //DisplayErrorMessage
                                    else{
                                        scr_feedbackDisplay.instance.MessageKeyCodeCheck();
                                    }
                                }
                                //DisplayErrorMessage
                                else{
                                    scr_feedbackDisplay.instance.MessageCheckInstantiateCode();
                                }
                            }
                            //DisplayErrorMessage
                            else{
                                scr_feedbackDisplay.instance.MessageInputPropertyCheck();
                            }
                        }
                        //DisplayErrorMessage
                        else{
                            scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                        }
                    }
                    //DisplayErrorMessage
                    else{
                        scr_feedbackDisplay.instance.MessageFunctionNameCheck();
                    }
                }
                //DisplayErrorMessage
                else{
                    scr_feedbackDisplay.instance.MessageFunctionDeclareCheck();
                }
            }
            //DisplayErrorMessage
            else{
                scr_feedbackDisplay.instance.MessageDeclareCheckVariableValues();
            }
        }
        //DisplayErrorMessage
        else{
            scr_feedbackDisplay.instance.MessageVariableType();
        }
    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void sectionComplete(){
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("spawningObjectsSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.spawningSectionFinished = true;
    }
}