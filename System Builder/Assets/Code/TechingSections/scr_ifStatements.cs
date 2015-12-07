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
        //scr_soundManager.instance.playButtonClick();
        //GetUserCode
        getCode();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //CheckChallenge
        ifstatementChallenge();
    }

    //CheckAnswerToIfStatementChallage
    void ifstatementChallenge(){
        //CheckIntHasBeenDefined
        if (usersEnteredCode.Contains("int")){
            //CheckNameOFDamageVairable
            if (usersEnteredCode.Contains("damage") && usersEnteredCode.Contains("0"))
            {
                //checkIfElseStatement
                if(usersEnteredCode.Contains("if") && usersEnteredCode.Contains("else") && Regex.Matches(usersEnteredCode, "5").Count == 3 && usersEnteredCode.Contains("name.length()")){
                    //CheckSyntax
                    if (Regex.Matches(usersEnteredCode, "=").Count == 3 && Regex.Matches(usersEnteredCode, ";").Count == 3 && usersEnteredCode.Contains(">") && usersEnteredCode.Contains("(") &&
                        usersEnteredCode.Contains(")") && Regex.Matches(usersEnteredCode, "{").Count == 2 && Regex.Matches(usersEnteredCode, "}").Count == 2){
                        //MarkSectionAsCompleteIfAllCodeFound
                        secttionComplete();
                    }
                    //DisplayFeedback
                    else{
                        scr_feedbackDisplay.instance.MessageCheckSyntax();
                    }
                }
                //DisplayFeedback
                else{
                    scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                }
                
            }
            //DisplayFeedback
            else{
                scr_feedbackDisplay.instance.MessageVariableName();
            }
        }
        //DisplayFeedback
        else{
            scr_feedbackDisplay.instance.MessageVariableType();
        }
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
