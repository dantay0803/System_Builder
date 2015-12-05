﻿using UnityEngine;
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
        this.gameObject.transform.position = new Vector2(Screen.width/2-1000, Screen.height/2);
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
        }
    }

    //ShowFeedback
    void showFeedback(){
        this.gameObject.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    //DiasplayAssessmentEngineFeedback
    public void UpdateFeedback(JSONArray feedbackReceived){
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
        //SetMessage
        txt_feedback.text = "Well done!" + "\n" + "You got the question correct";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableType
    public void MessageVariableType(){
        //SetMessage
        txt_feedback.text = "Have you defined the variable type?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableName
    public void MessageVariableName(){
        //SetMessage
        txt_feedback.text = "Did you set the name of the variable correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckSyntax
    public void MessageCheckSyntax(){
        //SetMessage
        txt_feedback.text = "Check your syntax";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckProperty
    public void MessageCheckProperty(){
        //SetMessage
        txt_feedback.text = "Did you type the property correctly?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageCheckWhitespace(){
        //SetMessage
        txt_feedback.text = "Remove any excess spaces";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageDeclareAllVariables(){
        //SetMessage
        txt_feedback.text = "Did you declare all the variables?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckWhitespace
    public void MessageDeclareCheckMathSymbol(){
        //SetMessage
        txt_feedback.text = "Did you enter the correct math symbol?";
        //ShowFeedback
        showFeedback();
    }

    //MessageToCheckVariableValues
    public void MessageDeclareCheckVariableValues(){
        //SetMessage
        txt_feedback.text = "Check the values entered for the variables";
        //ShowFeedback
        showFeedback();
    }
}