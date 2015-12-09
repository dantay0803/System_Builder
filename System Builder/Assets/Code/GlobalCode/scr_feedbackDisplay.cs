using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using SimpleJSON;
using System;
using System.Text.RegularExpressions;

public class scr_feedbackDisplay : MonoBehaviour {

    public EngAGe engage;
    //GameID
    private const int idSG = 259;
    //TextToDisplayFeedback
    public Text txt_feedback;
    //ThisGameObject
    public static scr_feedbackDisplay instance = null;
    //BoolsToMarkEachSectionAsCompleteInOrderToMoveLevel
    public bool variableSectionFinished = false;
    public bool ifStatementSectionFinished = false;
    public bool vectorSectionFinished = false;
    public bool functionSectionFinished = false;
    public bool playerInputSectionFinished = false;
    public bool spawningSectionFinished = false;
    public bool collisionsSectionFinished = false;

    //Use this for initialization
    void Start () {
        //HideFeedbackObject
        hideFeedbackObject();
        //SetInstanceToThisGameObjectIsIstanceIsNull
        if (instance == null){
            instance = this;
        }
        //IfInstanceIsNotThisObjectDestroyObject
        else if (instance != this){
            Destroy(gameObject);
        }
    }

    //PlayButtonClick
    public void playeButtonClick(){
        //PlayButtonClick
        scr_soundManager.instance.playButtonClick();
    }

    //HideFeedbackObject
    public void hideFeedbackObject(){
        //HideFeedbackPanel
        this.gameObject.transform.position = new Vector2(Screen.width / 2 - Screen.width, Screen.height/2);
        //CheckIfSectionsAreCompleteToMoveOne
        if (variableSectionFinished && Application.loadedLevelName == "scene_variables"){
            Application.LoadLevel("scene_ifStatements");
        }
        if (ifStatementSectionFinished && Application.loadedLevelName == "scene_ifStatements"){
            Application.LoadLevel("scene_vectors");
        }
        if (vectorSectionFinished && Application.loadedLevelName == "scene_vectors"){
            Application.LoadLevel("scene_functions");
        }
        if (functionSectionFinished && Application.loadedLevelName == "scene_functions"){
            Application.LoadLevel("scene_playerInput");
        }
        if (playerInputSectionFinished && Application.loadedLevelName == "scene_playerInput"){
            Application.LoadLevel("scene_spawning");
        }
        if (spawningSectionFinished && Application.loadedLevelName == "scene_spawning"){
            Application.LoadLevel("scene_collisions");
        }
        if (collisionsSectionFinished && Application.loadedLevelName == "scene_collisions"){
            Application.LoadLevel("scene_bossFight");
            //MarkGameWinAndGameComplete
            StartCoroutine(engage.endGameplay(true));

        }
    }

    //ShowFeedback
    void showFeedback(){
        this.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    //DiasplayAssessmentEngineFeedback
    public void UpdateFeedback(JSONArray feedbackReceived){
        //SetTextColour
        txt_feedback.color = Color.green;
        foreach (JSONNode f in feedbackReceived){
            //SetMessage
            txt_feedback.text = f["message"];
            //ShowFeedback
            showFeedback();
        }
    }
    //GetAssessmentEngineFeedback
    public void ActionAssessed(JSONNode jsonReturned){
        UpdateFeedback(jsonReturned["feedback"].AsArray);
    }



    //MessageToShowQuestionCorrect
    public void MessageQuesstionCorrect(){
        //SetTextColour
        txt_feedback.color = Color.green;
        //SetMessage
        txt_feedback.text = "Well done!" + "\n" + "You got the question correct";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableType
    public void MessageVariableType(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Have you defined the variable type?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableName
    public void MessageVariableName(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you set the name of the variable correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckSyntax
    public void MessageCheckSyntax(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your syntax";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckProperty
    public void MessageCheckProperty(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you type the property correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageCheckWhitespace(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Remove any excess spaces";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageDeclareAllVariables(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you declare all the variables?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageDeclareCheckMathSymbol(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you enter the correct math symbol?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableValues
    public void MessageDeclareCheckVariableValues(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check the values entered for the variables";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckIfStatement
    public void MessageDeclareCheckIfStatement(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your if statement for errors";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckElseStatement
    public void MessageDeclareCheckElseStatement(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your else statement for errors";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckDeltaTimeStatement
    public void MessageDeclareCheckDeltaTime(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your Time.deltaTime declaration";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVector2Statement
    public void MessageDeclareCheckVectors(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your vector declaration";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckTransformPostionStatement
    public void MessageDeclareCheckTransformPosition(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you correctly add the transform position correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckFunctionDecleration
    public void MessageFunctionDeclareCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you declare your function correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckFunctionName
    public void MessageFunctionNameCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you name your function correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckFunctionCode
    public void MessageFunctionCodeCheck()
    {
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check your function code";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckKeyCode
    public void MessageKeyCodeCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you use the correct key code?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckInputProperty
    public void MessageInputPropertyCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you add the input.GetKeyDown property?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckKeyCode
    public void MessageDeclareCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you use the correct key code? ";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckColliderParamater
    public void MessageColliderParameterCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you add in the collider parameter?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckDestroyProperty
    public void MessageDestroyPropertyCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you include the destroy property?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckObjectNameCheckStatement
    public void MessageObjectNameCheck(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you add the object nanme check?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckCheckInstantiateCode
    public void MessageCheckInstantiateCode(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Check the instantiate code";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckGameObjectDefined
    public void MessageCheckGameObjectDefined(){
        //SetTextColour
        txt_feedback.color = Color.red;
        //SetMessage
        txt_feedback.text = "Did you define the game object correctly?";
        //ShowFeedback
        showFeedback();
    }
}
