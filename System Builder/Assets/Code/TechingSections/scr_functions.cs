using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_functions : MonoBehaviour
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
        //scr_soundManager.instance.playButtonClick();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //GetUserInput
        getCode();
        //CheckCodeIsCorrect
        functionChallenge();
    }
    void functionChallenge()
    {

        if (usersEnteredCode.Contains("bool"))
        {
            if (Regex.Matches(usersEnteredCode, "engine").Count >= 2)
            {
                if (usersEnteredCode.Contains("void") && usersEnteredCode.Contains("startengine"))
                {
                    if (usersEnteredCode.Contains("startenginecheck"))
                    {
                        if(usersEnteredCode.Contains("if(engine"))
                        {
                            if (Regex.Matches(usersEnteredCode, "false").Count == 2)
                            {
                                if (usersEnteredCode.Contains("=") && Regex.Matches(usersEnteredCode, ";").Count == 2 && Regex.Matches(usersEnteredCode, "()").Count >= 2 && Regex.Matches(usersEnteredCode, "{").Count == 2 && Regex.Matches(usersEnteredCode, "}").Count == 2 && usersEnteredCode.Contains("=="))
                                {
                                    sectionComplete();
                                }
                                else
                                {
                                    scr_feedbackDisplay.instance.MessageCheckSyntax();
                                }
                            }
                            else
                            {
                                scr_feedbackDisplay.instance.MessageDeclareCheckVariableValues();
                            }
                        }
                        else
                        {
                            scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                        }
                    }
                    else
                    {
                        scr_feedbackDisplay.instance.MessageFunctionNameCheck();
                    }
                }
                else
                {
                    scr_feedbackDisplay.instance.MessageFunctionDeclareCheck();
                }
            }
            else
            {
                scr_feedbackDisplay.instance.MessageVariableName();
            }
        }
        else
        {
            scr_feedbackDisplay.instance.MessageVariableType();
        }

    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void sectionComplete()
    {
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("functionSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.functionSectionFinished = true;
    }
}

