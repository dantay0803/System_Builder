using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using SimpleJSON;
using System;

public class EngAGe : MonoBehaviour {
	
	static public EngAGe E;
		
	private static int idStudent;
	private static int idPlayer = -1;
	private static int version = 0;
	private static int idGameplay;
	private static JSONArray parameters;
	private static JSONArray scores = new JSONArray ();
	private static JSONArray feedback = new JSONArray ();
	private static JSONArray badgesWon = new JSONArray();
	private static JSONNode leaderboard = new JSONNode();

	private static JSONNode seriousGame = new JSONNode();
	
	private static string error;
	private static int errorCode;
	
	private Dictionary<string, string> headers = new Dictionary<string, string>();

	void Awake() {
		E = this;
	}
	
	// Use this for initialization
	void Start () {		
		headers.Add("Content-Type", "application/json");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ************* Get and Set ****************** //

	public int getErrorCode()
	{
		return errorCode;
	}
	public string getError()
	{
		return error;
	}
	public int getIdStudent()
	{
		return idStudent;
	}
	public int getIdPlayer()
	{
		return idPlayer;
	}
	public int getVersion()
	{
		return version;
	}
	public int getIdGameplay()
	{
		return idGameplay;
	}
	public JSONArray getParameters()
	{
		return parameters;
	}
	public JSONArray getScores()
	{
		return scores;
	}
	public JSONArray getFeedback()
	{
		return feedback;
	}
	public JSONArray getBadges()
	{
		return badgesWon;
	}
	public JSONNode getLeaderboardList()
	{
		return leaderboard;
	}
	public JSONNode getSG()
	{
		return seriousGame;
	}

	// ************* Web services calls ****************** //
	//private string baseURL = "http://docker:8080";
	private string baseURL = "http://146.191.107.189:8080";


	public IEnumerator loginStudent(int p_idSG, string p_username, string p_password, 
	                                string sceneLoginFail, string sceneNoParameters, string sceneParameters)
	{
		print ("--- loginStudent ---");
		
		string URL = baseURL + "/SGaccess";
		
		string postDataString = 
			"{" + 
				"\"idSG\": " + p_idSG + 
				", \"username\": \"" + p_username + "\"" +
				", \"password\": \"" + p_password + "\"" +
				"}";
		print (postDataString);

		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(postDataString), headers);

		// wait for the requst to finish
		yield return www;

		JSONNode loginData = JSON.Parse(www.text);
		
		bool loginSuccess = loginData["loginSuccess"].AsBool;
		
		if (!loginSuccess)
		{
			errorCode = 201;
			error = "Login failed";
			Application.LoadLevel(sceneLoginFail);
		}
		else if (loginData["version"] != null)
		{
			errorCode = 0;
			error = "";
			if (loginData["idPlayer"] != null)
			{
				idPlayer = loginData["idPlayer"].AsInt;
				version = loginData["version"].AsInt;
				idStudent = loginData["student"]["id"].AsInt;
				parameters = loginData["params"].AsArray;
				
				Application.LoadLevel(sceneNoParameters);
			}
			else
			{
				version = loginData["version"].AsInt;
				idStudent = loginData["student"]["id"].AsInt;
				parameters = loginData["params"].AsArray;
				
				Application.LoadLevel(sceneParameters);
			}
		}
		else
		{
			errorCode = 203;
			error = "Sorry, this game is not public and you don't have access to it.";
			Application.LoadLevel(sceneLoginFail);
		}
	}

	public IEnumerator guestLogin(int p_idSG, string sceneLoginFail, string sceneParameters)
	{
		print ("--- loginStudent ---");
		
		string URL = baseURL + "/SGaccess";
		
		string postDataString = 
			"{" + 
				"\"idSG\": " + p_idSG + 
				", \"username\": \" \"" +
				", \"password\": \" \"" +
				"}";
		print (postDataString);

		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(postDataString), headers);
		
		// wait for the requst to finish
		yield return www;

		JSONNode loginData = JSON.Parse(www.text);

		print (www.text);
		
		if (loginData["version"] != null)
		{
			errorCode = 0;
			error = "";
			idStudent = 0;
			parameters = loginData["params"].AsArray;
			version = loginData["version"].AsInt;
			Application.LoadLevel(sceneParameters);
		}
		else
		{
			errorCode = 202;
			error = "Sorry this game is not public";
			Application.LoadLevel(sceneLoginFail);
		}				
	}

	public IEnumerator startGameplay(int p_idSG, string sceneGame)
	{
		scores = new JSONArray ();
		feedback = new JSONArray ();
		seriousGame = new JSONNode();
				
		print ("--- startGameplay ---");

		string putDataString = "";
		
		// existing player
		if (idPlayer != -1)
		{		
			putDataString = 
				"{" + 
					"\"idSG\": " + p_idSG + 
					", \"version\": " + version + 
					", \"idPlayer\": " + idPlayer +
					"}";
		}
		// new player -> create one
		else 
		{
			putDataString = 
				"{" + 
					"\"idSG\": " + p_idSG + 
					", \"version\": " + version + 
					", \"idStudent\": " + idStudent +
					", \"params\": " + parameters.ToString() +
					"}";
		}
		print (putDataString);

		string URL = baseURL + "/gameplay/start";

		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(putDataString), headers);
		
		// wait for the requst to finish
		yield return www;

		
		idGameplay = int.Parse(www.text);
		
		print ("Gameplay Started! id: " + idGameplay);
		print ("--- getScores ---");
		
		string URL2 = baseURL + "/gameplay/" + idGameplay + "/scores/";

		WWW www2 = new WWW(URL2);
		
		// wait for the requst to finish
		yield return www2;

		scores = JSON.Parse(www2.text).AsArray;
		print ("Scores received! " + scores.ToString());
		
		Application.LoadLevel(sceneGame);
	}

	public IEnumerator assess(string p_action, JSONNode p_values, Action<JSONNode> callback)
	{
		print ("--- assess action (" + p_action + ") ---");
				
		string putDataString = 
			"{" + 
				"\"action\": \"" + p_action + "\"" +
				", \"values\": " + p_values.ToString() + 
				"}";
		
		string URL = baseURL + "/gameplay/" + idGameplay + "/assessAndScore";
				
		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(putDataString), headers);
		
		// wait for the requst to finish
		yield return www;

		JSONNode returnAssess = JSON.Parse (www.text);
		
		feedback = returnAssess["feedback"].AsArray;
		scores = returnAssess["scores"].AsArray;
		print ("Action " + putDataString + " assessed! returned: " + returnAssess.ToString());
		foreach (JSONNode f in feedback)
		{			
			// log badge
			if (string.Equals(f["type"], "BADGE"))
			{
				badgesWon.Add(f);
			}	
		}
		
		callback (returnAssess);
	}
	
	public IEnumerator endGameplay(bool win)
	{
		print ("--- end Gameplay ---");
		string winString = (win) ? "win" : "lose";
		string URL = baseURL + "/gameplay/"+ idGameplay + "/end/" + winString;
		print (URL);

		Dictionary<string, string> headers2 = new Dictionary<string, string>();		
		headers2.Add("Content-Type", "text/plain");

		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(winString), headers2);

		// wait for the requst to finish
		yield return www;
			
		if (www.error != null && !www.error.Equals(""))
		{
			print ("Error: " + www.error);
		}
		else 
		{
			print ("Gameplay Ended! return: " + www.text );
		}
	}	
	
	public IEnumerator endGameplay(String win)
	{
		print ("--- end Gameplay ---");
		string URL = baseURL + "/gameplay/"+ idGameplay + "/end/" + win;
		print (URL);
		
		WWW www = new WWW(URL, Encoding.UTF8.GetBytes(win));
		
		// wait for the requst to finish
		yield return www;
		
		print ("Gameplay Ended! return: " + www.text);
	}

	public IEnumerator updateScores(Action<JSONArray> callbackScore)
	{
		print ("--- getScores ---");
		
		string URL = baseURL + "/gameplay/" + idGameplay + "/scores/";

		WWW www = new WWW(URL);
		
		// wait for the requst to finish
		yield return www;

		scores = JSON.Parse(www.text).AsArray;
		print ("Scores received! " + scores.ToString());
		callbackScore(scores);
	}
	
	public IEnumerator updateFeedback(Action<JSONArray> callbackFeedback)
	{
		print ("--- update Feedback ---");
		
		string URL = baseURL + "/gameplay/" + idGameplay + "/feedback/";

		WWW www = new WWW(URL);
		
		// wait for the requst to finish
		yield return www;

		feedback = JSON.Parse(www.text).AsArray;
		print ("Feedback received! " + feedback.ToString());
		foreach (JSONNode f in feedback)
		{			
			// log badge
			if (string.Equals(f["type"], "BADGE"))
			{
				badgesWon.Add(f);
			}	
		}
		
		callbackFeedback (feedback);
	}

	public IEnumerator getGameDesc(int p_idSG)
	{
		print ("--- get SG ---");
		
		string URL = baseURL + "/seriousgame/" + p_idSG + "/version/" + version;
		
		WWW www = new WWW(URL);
		
		// wait for the requst to finish
		yield return www;

		seriousGame = JSON.Parse(www.text);
		print ("Serious game detailed received! " + seriousGame.ToString());
	}

	
	public IEnumerator getBadgesWon(int p_idSG)
	{
		print ("--- get Badges ---");

		string URL = baseURL + "/badges/seriousgame/" + p_idSG + "/version/" + version + "/player/" + idPlayer;

		WWW www = new WWW(URL);
		
		// wait for the requst to finish
		yield return www;
		
		badgesWon = JSON.Parse(www.text).AsArray;
		print ("Badges received! " + badgesWon.ToString());
	}

	public IEnumerator getLeaderboard(int p_idSG)
	{
		print ("--- get Leader Board ---");
		
		string URL = baseURL + "/learninganalytics/leaderboard/seriousgame/" + p_idSG + "/version/" + version;
		
		WWW www = new WWW(URL);
		
		// wait for the requst to finish
		yield return www;

		leaderboard = JSON.Parse(www.text);
		print ("Leader board received! " + leaderboard.ToString());
	}
}
