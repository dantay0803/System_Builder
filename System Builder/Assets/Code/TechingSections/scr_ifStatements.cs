using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_ifStatements : MonoBehaviour
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
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
    }


    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void secttionComplete()
    {
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("ifSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.ifStatementSectionFinished = true;
    }
}
