using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class scr_playerInput : MonoBehaviour
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
    //BooleansToMarkQuestionsAsCorrect
    bool questionOneCorrect = false;
    bool questionTwoCorrect = false;

    //GetUserCode
    public void getCode(){
        usersEnteredCode = input_code.GetComponent<InputField>().text;
    }

    //CheckTheUsersCodeIsRight
    public void checkCode(){
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
        //GetUserCode
        getCode();
        //setTheUserCodeAsAllLowerCase
        usersEnteredCode.ToLower();
        //CheckAnswers
        if (!questionOneCorrect && !questionTwoCorrect){
            checkQuestionOne();
        }
        else if(questionOneCorrect && !questionTwoCorrect){
            checkQuestionTwo();
        }
    }    

    //CheckAnswerToQuestionOne
    void checkQuestionOne(){
        if (usersEnteredCode.Contains("if")){
            if (usersEnteredCode.Contains("input.getkeydown")){
                if (usersEnteredCode.Contains("k")){
                    if (usersEnteredCode.Contains("print") && usersEnteredCode.Contains("K key has been pressed")){
                        if (usersEnteredCode.Contains(";") && Regex.Matches(usersEnteredCode, "(").Count == 3 && Regex.Matches(usersEnteredCode, ")").Count == 3 && Regex.Matches(usersEnteredCode, "\"").Count == 4 && usersEnteredCode.Contains("{") && usersEnteredCode.Contains("}")){
                            //MarkQuestionOneAsCorrect
                            questionOneCorrect = true;
                            //DisplayQuestionAsCorrect
                            scr_feedbackDisplay.instance.MessageQuesstionCorrect();
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
                    scr_feedbackDisplay.instance.MessageKeyCodeCheck();
                }
            }
            //DisplayFeedback
            else{
                scr_feedbackDisplay.instance.MessageInputPropertyCheck();
            }
        }
        //DisplayFeedback
        else{
            scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
        }
    }

    //CheckAnswerToQuestionOne
    void checkQuestionTwo(){
        if(usersEnteredCode.Contains("float") && usersEnteredCode.Contains("vector2")){
            if(usersEnteredCode.Contains("shipspeed") && usersEnteredCode.Contains("obj_shippos")){
                if(usersEnteredCode.Contains("0.5f")){
                    if(usersEnteredCode.Contains("void")){
                        if (usersEnteredCode.Contains("moveship")){
                            if(Regex.Matches(usersEnteredCode, "if").Count == 2){
                                if(Regex.Matches(usersEnteredCode, "input.getkeydown").Count == 2){
                                    if(usersEnteredCode.Contains("left") && usersEnteredCode.Contains("right")){
                                        if(Regex.Matches(usersEnteredCode, "obj_shippos").Count == 5){
                                            if(Regex.Matches(usersEnteredCode, "this.transform.position").Count == 2){
                                                if(Regex.Matches(usersEnteredCode, ".x").Count == 2 && Regex.Matches(usersEnteredCode, "shipspeed").Count == 3){
                                                    if(Regex.Matches(usersEnteredCode, "time.deltatime").Count == 3){
                                                        if(Regex.Matches(usersEnteredCode, "obj_shippos").Count == 6){
                                                            if (Regex.Matches(usersEnteredCode, "=").Count == 4 && Regex.Matches(usersEnteredCode, ";").Count == 7 && Regex.Matches(usersEnteredCode, "+=").Count == 2 && Regex.Matches(usersEnteredCode, "\"").Count == 4 && Regex.Matches(usersEnteredCode, "{").Count == 3 && Regex.Matches(usersEnteredCode, "}").Count == 3 && Regex.Matches(usersEnteredCode, "(").Count == 5 && Regex.Matches(usersEnteredCode, ")").Count == 5){
                                                                if(Regex.Matches(usersEnteredCode, "*").Count == 2){
                                                                    //MarkQuestionAsComplete
                                                                    questionTwoCorrect = true;
                                                                    //MarkSectionAsComplete
                                                                    sectionComplete();
                                                                }
                                                                //DisplayFeedback
                                                                else{
                                                                    scr_feedbackDisplay.instance.MessageDeclareCheckMathSymbol();
                                                                }
                                                            }
                                                            //DisplayFeedback
                                                            else{
                                                                scr_feedbackDisplay.instance.MessageCheckSyntax();
                                                            }
                                                        }
                                                        //DisplayFeedback
                                                        else{
                                                            scr_feedbackDisplay.instance.MessageFunctionCodeCheck();
                                                        }
                                                    }
                                                    //DisplayFeedack
                                                    else{
                                                        scr_feedbackDisplay.instance.MessageDeclareCheckDeltaTime();
                                                    }
                                                }
                                                //DisplayFeedback
                                                else{
                                                    scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                                                }
                                            }
                                            //DisplayFeedback
                                            else{
                                                scr_feedbackDisplay.instance.MessageDeclareCheckTransformPosition();
                                            }
                                        }
                                        //DisaplayFeedback
                                        else{
                                            scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                                        }
                                    }
                                    //DisplayFeedback
                                    else{
                                        scr_feedbackDisplay.instance.MessageKeyCodeCheck();
                                    }
                                }
                                //DisplayFeedback
                                else{
                                    scr_feedbackDisplay.instance.MessageInputPropertyCheck();
                                }
                            }
                            //DisplayFeedback
                            else{
                                scr_feedbackDisplay.instance.MessageDeclareCheckIfStatement();
                            }
                        }
                        //DisplayFeedback
                        else{
                            scr_feedbackDisplay.instance.MessageFunctionNameCheck();
                        }
                    }
                    //DisplayFeedback
                    else{
                        scr_feedbackDisplay.instance.MessageFunctionDeclareCheck();
                    }
                }
                //DisplayFeedback
                else{
                    scr_feedbackDisplay.instance.MessageDeclareCheckVariableValues();
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

    //SetUpInstructionsForQuestionTwo
    void setUpPlayerMovementInputQuestion(){
        //DisplayShipMessage
        txt_shipMessage.text = "Now we have learned how to work out my controls system, let’s get this ship moving.";
        //DisplayUserInstructions
        txt_insturctions.text = "To move the ship create a float variable called shipSpeed and set it to 0.5f, then declare a vector2 variable set as obj_shipPos. Now declare a function called move ship."
                                + " Check for a left arrow key input like down previously but this time replace “K” inside the brackets to “left”, create another if statement underneath and do the same"
                                + " but this time for the right arrow. Set obj_shipPos to the transform position like done with the bullet, now you are going to set obj_shipPos.x to equal obj_shipPos.x"
                                + " += shipSpeed multiplied by delta time again like with the bullets. Then you want to update the transform position to equal obj_shipPos." +
                                "\n" + "Tip: Remember what you learned about functions, if statements and vectors previously.";
        //DeleteOldUserCode
        input_code.GetComponent<InputField>().text = "";

    }

    //DisplayMessageToLetThePlayerKnowTheyCompletedTheStage
    void sectionComplete(){
        //CheckOutcomes
        JSONNode vals = JSON.Parse("{\"status\" : \"" + "complete" + "\" }");
        StartCoroutine(engage.assess("playerInputSectionComplete", vals, scr_feedbackDisplay.instance.ActionAssessed));
        //SetSectionAsCompleteInFeedbackScript
        scr_feedbackDisplay.instance.playerInputSectionFinished = true;
    }
}
