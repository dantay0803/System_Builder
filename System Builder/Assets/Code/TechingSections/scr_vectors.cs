using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_vectors : MonoBehaviour
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
        //GetUserCode
        getCode();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //CheckAnswer
        vectorQuestion1();
    }



    //CheckCorrectCodeForQuestion
    void vectorQuestion1()
    {
        if (usersEnteredCode.Contains("float") && usersEnteredCode.Contains("vector2"))
        {
            if (Regex.Matches(usersEnteredCode, "obj_bulletspeed").Count == 2 && Regex.Matches(usersEnteredCode, "obj_bulletpos").Count == 3)
            {
                if (usersEnteredCode.Contains("0.5") && Regex.Matches(usersEnteredCode, "this.transform.position").Count == 2)
                {
                    if (usersEnteredCode.Contains("*"))
                    {
                        if(usersEnteredCode.Contains(".y"))
                        {
                            if (usersEnteredCode.Contains("time.deltatime"))
                            {
                                if (Regex.Matches(usersEnteredCode, ";").Count == 4 && Regex.Matches(usersEnteredCode, "=").Count == 4 && usersEnteredCode.Contains("+"))
                                {
                                    //MarkVectorSectionComplete
                                    secttionComplete();
                                }
                                else
                                {
                                    scr_feedbackDisplay.instance.MessageCheckSyntax();
                                }
                            }
                            else
                            {
                                scr_feedbackDisplay.instance.MessageDeclareCheckDeltaTime();
                            }
                        }
                        else
                        {
                            scr_feedbackDisplay.instance.MessageDestroyPropertyCheck();
                        }
                    }
                    else
                    {
                        scr_feedbackDisplay.instance.MessageDeclareCheckMathSymbol();
                    }
                }
                else
                {
                    scr_feedbackDisplay.instance.MessageDeclareCheckVariableValues();
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
    void secttionComplete()
    {
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("vectorSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.vectorSectionFinished = true;
    }
}
