using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using SimpleJSON;
using System.Runtime.InteropServices;
using AesEverywhere;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;


public class BooomAPI : MonoBehaviour
{
	AES256 aes = new AES256();
	[SerializeField] private SendScoreJson _SendScoreJson = new SendScoreJson();

	#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowMessage(string message);
	#endif
	
    public GameObject gusetuiExit;
	public Link externalurl;
	public bool BuildForBoom;
	public GameObject ScoreUi;
	public GameObject yourScoreBackplate;
	public GameObject highScoreBackplate;
	public GameObject UserLoginUi;
	public GameObject GameMainMenu;
	public GameObject BgPortrate;
	public GameObject BgLandscape;
	public GameObject GuestUi;
	public GameObject UserUi;
	public GameObject UserVerifyingUi;
	public GameObject LoadingLeaderbordUi;
	public GameObject LeaderbordGuestUi;
	public GameObject leaderborderror;
	public GameObject LeaderBordUiPortrate;
	public GameObject LeaderBordUiLandscape;
	public GameObject leaderbordPortrate;
	public GameObject gameOverPortrate;
	public GameObject leaderbordLandscape;
	public GameObject gameOverLandscape;
    public GameObject[] yourScoreOverlyLandscape;
	public GameObject[] yourScoreOverlyPortrate;
	public GameObject fullscreenOnbtn;
	public GameObject fullscreenOffbtn;
	public GameObject FortArcPointsUI;
	public Text ScoreInput;
	public Text FortArcPoints;
	public Text DisplayName;
	public Text YoueScorePortrate;
	public Text HiScorePortrate;
	public Text YoueScoreLandscape;
	public Text HiScoreLandscape;
	public Text popupscoretxt;
	public Text popupHiscoretxt;
	public Text gusetScore;
	
	
	public string apikey;
	public int gameid;
	public int gamescore;
	public float fortPoints;
	public float addBonusPoints;
	public string videoUrl;
	public string redirectLink;
	public string checkBonusPoints;

	string input = "";
	public Text[] leaderbordNameLandscape;
	public Text[] leaderbordScoreLandscape;
	public Text[] leaderbordNamePortrait;
	public Text[] leaderbordScorePortrait;
	public bool gusetUser=true;
	public bool orientationLandscape=true;
	public bool leaderbordOn = false;
	public bool Gameover = false;
	public bool mobile = false;
	public bool iPhone = false;
	public bool fullScreen = false;
	public GameController gamecontroller;
	string Dsandbox = "";
	string Dtoken = "";
	string Did = "";
	string Dusercall = "";
	string Dhiscorecall = "";
	string Dscorecall = "";
	string Dsuggestcall = "";
	string Dkey = "";
	string Dfortarcpoints = "";
	string Daddbonuspoints = "";
	string Dcheckbonuspoints = "";
	string Dgetvideoad = "";
	public GameObject[] moregameiconL;
	public GameObject[] moregameiconP;
	public GameObject moregame_Landscape;
	public GameObject moregame_Portrait;
	public GameObject moregame_Loading;
	public string[] iconurl;
	public Text[] moreGameNameL;
	public Text[] moreGameNameP;
	private AudioSource audioSource;
	bool gameopen= false;
	public GameObject videoController;

    // Start is called before the first frame update
    void Start()
    {
		UserLoginUi.SetActive(true);
		GameMainMenu.SetActive(false);

		if (BuildForBoom)
		{
			LoadData();
		}
		else
		{
			TextAsset txtXmlAsset = Resources.Load<TextAsset>("hostdata");
			var doc = XDocument.Parse(txtXmlAsset.text);
			parseXmlFile(doc.ToString());
		}
    }
	
	void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus == true)
		{
			Time.timeScale = 1;

			if(gameopen)
			{
				gamecontroller.mainAudioOn();
			}
		}
		if (hasFocus == false)
		{
			Time.timeScale = 0;
			gamecontroller.mainAudioOff();
		}
	}

	void parseXmlFile(string xmlData)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load (new StringReader(xmlData));
		
		string xmlPathPattern ="//data/globle";
		XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
		foreach(XmlNode node in myNodeList)
		{
			XmlNode sandbox  = node.FirstChild;
			XmlNode token  = sandbox.NextSibling;
			XmlNode id  = token.NextSibling;
			XmlNode usercall  = id.NextSibling;
			XmlNode hiscorecall  = usercall.NextSibling;
			XmlNode scorecall  = hiscorecall.NextSibling;
			XmlNode suggestcall  = scorecall.NextSibling;
			XmlNode key  = suggestcall.NextSibling;
			XmlNode fortarcpoints = key.NextSibling;
			XmlNode addbonuspoints = fortarcpoints.NextSibling;
			XmlNode checkbonuspoints = addbonuspoints.NextSibling;
			XmlNode getvideoad = checkbonuspoints.NextSibling;
			Dsandbox = sandbox.InnerXml;
			Dtoken = token.InnerXml;
			Did = id.InnerXml;
	        Dusercall = usercall.InnerXml;
	        Dhiscorecall = hiscorecall.InnerXml;
			Dscorecall = scorecall.InnerXml;
			Dsuggestcall = suggestcall.InnerXml;
			Dkey = key.InnerXml;
			Dfortarcpoints = fortarcpoints.InnerXml;
			Daddbonuspoints = addbonuspoints.InnerXml;
			Dcheckbonuspoints = checkbonuspoints.InnerXml;
			Dgetvideoad = getvideoad.InnerXml;

			if (int.Parse(Dsandbox)==0)
			{
				gameid = int.Parse(Did);
				apikey= Dtoken;
				StartCoroutine(GetSandboxUser(Dusercall));
			}
			else
			{
				gameid = int.Parse(Did);
				login();
			}
		}
	}
	
	void Update()
	{
		if (Screen.width < Screen.height)
		{			
			BgPortrate.SetActive(true);
			BgLandscape.SetActive(false);
			orientationLandscape = false;
			yourScoreBackplate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			highScoreBackplate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
		else if (Screen.width > Screen.height)
		{
			BgLandscape.SetActive(true);
			BgPortrate.SetActive(false);
			orientationLandscape = true;
			yourScoreBackplate.transform.localScale = new Vector3(1f, 1f, 1f);
			highScoreBackplate.transform.localScale = new Vector3(1f, 1f, 1f);
		}

		if (Screen.fullScreen)
		{
			fullScreen = true;
			fullscreenOnbtn.SetActive(false);
			fullscreenOffbtn.SetActive(true);
		}
		else
		{
			fullScreen = false;
			fullscreenOnbtn.SetActive(true);
			fullscreenOffbtn.SetActive(false);
		}
	}
	
	public void LoadData()
	{
		SendToJS();
	}

    public void gameReplay()
    {
        UserLoginUi.SetActive(false);
        GameMainMenu.SetActive(false);
        GuestUi.SetActive(false);
        gusetuiExit.SetActive(false);
        UserUi.SetActive(false);
        UserVerifyingUi.SetActive(false);
        LoadingLeaderbordUi.SetActive(false);
        LeaderBordUiPortrate.SetActive(false);
        LeaderBordUiLandscape.SetActive(false);
        LeaderbordGuestUi.SetActive(false);
        leaderbordPortrate.SetActive(false);
        gameOverPortrate.SetActive(false);
        leaderbordLandscape.SetActive(false);
        gameOverLandscape.SetActive(false);
        Gameover = false;
        leaderbordOn = false;
        yourScoreBackplate.SetActive(false);
        highScoreBackplate.SetActive(false);
        ScoreUi.SetActive(false);
        gamecontroller.GameStartre();
    }

    public void login()
	{
		if (apikey == "guset")
		{
			GuestUi.SetActive(true);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			DisplayName.text = "Guest";
			gusetUser=true;
		}
		else
		{
	        StartCoroutine(GetCurrentUser(Dusercall));
		}
	}
	
	public void fullscreenOn()
	{
		if(!fullScreen)
		{
			Screen.fullScreen = !Screen.fullScreen;
			fullScreen = true;
			fullscreenOnbtn.SetActive(false);
			fullscreenOffbtn.SetActive(true);
		}
	}
	
	public void fullscreenOff()
	{
		if(fullScreen)
		{
			Screen.fullScreen = !Screen.fullScreen;
			fullScreen = false;
			fullscreenOnbtn.SetActive(true);
			fullscreenOffbtn.SetActive(false);
		}
	}
	
	public void ContinuBtn()
	{
		UserLoginUi.SetActive(false);
		GameMainMenu.SetActive(true);
		yourScoreBackplate.SetActive(false);
		highScoreBackplate.SetActive(false);
		ScoreUi.SetActive(false);
		gameopen=true;
	}
		
	public void closePopupBtn()
	{
		yourScoreBackplate.SetActive(false);
		highScoreBackplate.SetActive(false);
		ScoreUi.SetActive(false);
	}	
	
	public void LoginBtn()
	{
		if(mobile && iPhone)
		{
			externalurl.OpenLink("https://www.fortarc.com/login");
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS("https://www.fortarc.com/login");
		}
		else
		{
			externalurl.OpenLinkJSPlugin("https://www.fortarc.com/login");
		}
	}
	
	public void LeaderbordExit()
	{
		UserLoginUi.SetActive(false);
		GameMainMenu.SetActive(true);
		GuestUi.SetActive(false);
		gusetuiExit.SetActive(false);
		UserUi.SetActive(false);
		UserVerifyingUi.SetActive(false);
		LoadingLeaderbordUi.SetActive(false);
		LeaderBordUiPortrate.SetActive(false);
		LeaderBordUiLandscape.SetActive(false);
		LeaderbordGuestUi.SetActive(false);
		leaderbordPortrate.SetActive(false);
		gameOverPortrate.SetActive(false);
		leaderbordLandscape.SetActive(false);
		gameOverLandscape.SetActive(false);
		Gameover=false;
		leaderbordOn=false;
		yourScoreBackplate.SetActive(false);
		highScoreBackplate.SetActive(false);
		ScoreUi.SetActive(false);	   
	}
	
	public void sendscoredata()
	{
		if (gusetUser)
		{
			UserLoginUi.SetActive(true);
			GameMainMenu.SetActive(false);
			GuestUi.SetActive(false);
			gusetuiExit.SetActive(false);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			LoadingLeaderbordUi.SetActive(false);
			LeaderBordUiPortrate.SetActive(false);
			LeaderBordUiLandscape.SetActive(false);
			LeaderbordGuestUi.SetActive(true);
			yourScoreBackplate.SetActive(true);
			highScoreBackplate.SetActive(false);
			ScoreUi.SetActive(true);
			popupscoretxt.text = ScoreInput.text;
			gusetScore.text = ScoreInput.text;
		}
		else
		{
			Gameover = true;
			UserLoginUi.SetActive(true);
		    GameMainMenu.SetActive(false);
			GuestUi.SetActive(false);
			gusetuiExit.SetActive(false);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			LoadingLeaderbordUi.SetActive(true);
			LeaderBordUiPortrate.SetActive(false);
			LeaderBordUiLandscape.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			gamescore = int.Parse(ScoreInput.text);
			 
			if (int.Parse(Dsandbox)==0)
		    {
				StartCoroutine(SendSandboxScore(Dscorecall));
		    }
		    else
		    {
				StartCoroutine(SendScore(Dscorecall));
		    }
		}
	}
	
	public void leaderbord()
	{
		if(!gusetUser)
		{
			UserLoginUi.SetActive(true);
		    GameMainMenu.SetActive(false);
			GuestUi.SetActive(false);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			LoadingLeaderbordUi.SetActive(true);
			LeaderBordUiPortrate.SetActive(false);
			LeaderBordUiLandscape.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderbordPortrate.SetActive(false);
			gameOverPortrate.SetActive(false);
			leaderbordLandscape.SetActive(false);
			gameOverLandscape.SetActive(false);
			moregame_Portrait.SetActive(false);
			moregame_Landscape.SetActive(false);
			moregame_Loading.SetActive(false);
			   
			if (int.Parse(Dsandbox)==0)
		    {
				StartCoroutine(GetSanboxLeadeboard(Dhiscorecall));
		    }
		    else
		    {
				StartCoroutine(GetLeadeboard(Dhiscorecall));
		    }
		}
		else
		{
			UserLoginUi.SetActive(true);
		    GameMainMenu.SetActive(false);
			GuestUi.SetActive(false);
			gusetuiExit.SetActive(true);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			LoadingLeaderbordUi.SetActive(false);
			LeaderBordUiPortrate.SetActive(false);
			LeaderBordUiLandscape.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderbordPortrate.SetActive(false);
			gameOverPortrate.SetActive(false);
			leaderbordLandscape.SetActive(false);
			gameOverLandscape.SetActive(false);
			moregame_Portrait.SetActive(false);
			moregame_Landscape.SetActive(false);
			moregame_Loading.SetActive(false);
		}
	}

    public void SendToJS() {
        string MessageToSend = "login";
        //Debug.Log("Sending message to JavaScript: " + MessageToSend);
		#if UNITY_WEBGL && !UNITY_EDITOR
			ShowMessage(MessageToSend);
		#endif
    }
	
    public void SendToUnity(string message)
    {
		apikey = message;
    }
	
	public void checkMobile(string message)
    {
		if(message == "mobile")
		{
			iPhone = false;
			mobile = true;
			Debug.Log("unity mobile");
		}
		else if(message == "iPhone")
		{
			iPhone = true;
			mobile = true;
			Debug.Log("unity iphone");
		}
		else
		{
			mobile = false;
			iPhone = false;
			Debug.Log("unity pc");
		}
    }
	
	public void xmldata(string message)
    {
		parseXmlFile (message);
    }
	
	public void Moregames()
    {
		UserLoginUi.SetActive(true);
		GameMainMenu.SetActive(false);
		LoadingLeaderbordUi.SetActive(false);
		LeaderbordGuestUi.SetActive(false);
		leaderborderror.SetActive(false);
		UserUi.SetActive(false);
		UserVerifyingUi.SetActive(false);
		LeaderBordUiPortrate.SetActive(false);
		LeaderBordUiLandscape.SetActive(false);
		leaderbordPortrate.SetActive(false);
		leaderbordLandscape.SetActive(false);
		gameOverLandscape.SetActive(false);
		StartCoroutine(GetMoregames(Dsuggestcall));
		moregame_Loading.SetActive(true);
    }
	
	IEnumerator GetMoregames(string uri)
	{
		UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
		
		yield return uwr.SendWebRequest();
		
		if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
			if (orientationLandscape)
			{
				moregame_Landscape.SetActive(true);
				moregame_Portrait.SetActive(false);
				moregame_Loading.SetActive(false);
				string jsonString = uwr.downloadHandler.text;
				JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
				int teamcount = playerJson["data"].Count;

				for (int i = 0; i < teamcount; i++)
				{
					moreGameNameL[i].text = playerJson["data"].AsArray[i]["name"];
					moreGameNameP[i].text = playerJson["data"].AsArray[i]["name"];
					iconurl[i] = playerJson["data"].AsArray[i]["game_play_url"];
					string tempimageUrl = "https://www.fortarc.com/" + (playerJson["data"].AsArray[i]["image_url"]);
					WWW www = new WWW(tempimageUrl);
					yield return www;
					moregameiconL[i].GetComponent<Image >().sprite = Sprite.Create(www.texture, new Rect(0, 0, 125, 125), Vector2.zero);
					moregameiconP[i].GetComponent<Image >().sprite = Sprite.Create(www.texture, new Rect(0, 0, 125, 125), Vector2.zero);
				}
			}
			else
			{
				moregame_Portrait.SetActive(true);
				moregame_Landscape.SetActive(false);
				moregame_Loading.SetActive(false);
				string jsonString = uwr.downloadHandler.text;
				JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
				int teamcount = playerJson["data"].Count;

				for (int i = 0; i < teamcount; i++)
				{
					moreGameNameL[i].text = playerJson["data"].AsArray[i]["name"];
					moreGameNameP[i].text = playerJson["data"].AsArray[i]["name"];
					iconurl[i] = playerJson["data"].AsArray[i]["game_play_url"];
					string tempimageUrl = "https://www.fortarc.com/" + (playerJson["data"].AsArray[i]["image_url"]);
					WWW www = new WWW(tempimageUrl);
					yield return www;
					moregameiconL[i].GetComponent<Image >().sprite = Sprite.Create(www.texture, new Rect(0, 0, 125, 125), Vector2.zero);
					moregameiconP[i].GetComponent<Image >().sprite = Sprite.Create(www.texture, new Rect(0, 0, 125, 125), Vector2.zero);
			   
				}            	
            }
	    }
	}
	
	public void Moregamesclose()
	{
		UserLoginUi.SetActive(false);
		GameMainMenu.SetActive(false);
		LoadingLeaderbordUi.SetActive(false);
		LeaderbordGuestUi.SetActive(false);
		leaderborderror.SetActive(false);
		UserUi.SetActive(false);
		UserVerifyingUi.SetActive(false);
		gameOverLandscape.SetActive(false);
		moregame_Loading.SetActive(false);
		moregame_Landscape.SetActive(false);
		moregame_Portrait.SetActive(false);

		if(gusetUser)
		{
			UserLoginUi.SetActive(true);
			GameMainMenu.SetActive(false);
			GuestUi.SetActive(false);
			gusetuiExit.SetActive(false);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			LoadingLeaderbordUi.SetActive(false);
			LeaderBordUiPortrate.SetActive(false);
			LeaderBordUiLandscape.SetActive(false);
			LeaderbordGuestUi.SetActive(true);
			yourScoreBackplate.SetActive(true);
			highScoreBackplate.SetActive(false);
			ScoreUi.SetActive(true);
			popupscoretxt.text = ScoreInput.text;
			gusetScore.text = ScoreInput.text;
		}
		else
		{
			leaderbord();	
		}
	}
	
	public void Moregamebtn1()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[0];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	public void Moregamebtn2()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[1];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	public void Moregamebtn3()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[2];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	public void Moregamebtn4()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[3];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	public void Moregamebtn5()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[4];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	public void Moregamebtn6()
	{
		string tempimageUrl= "https://www.fortarc.com" + iconurl[5];
		
		if(mobile && iPhone)
		{
			externalurl.OpenLink(tempimageUrl);
		}
		else if(mobile)
		{
			externalurl.OpenLinkJS(tempimageUrl);
		}
		else
		{
			externalurl.OpenLinkJSPlugin(tempimageUrl);
		}
	}
	
	IEnumerator GetSanboxLeadeboard(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization","Bearer " + Dtoken);
		
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderborderror.SetActive(true);
        }
        else
        {
			//Debug.Log(uwr.downloadHandler.text);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderborderror.SetActive(false);
			leaderbordOn = true;
			
			if (orientationLandscape)
			{
				if (Gameover)
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(false);
					LeaderBordUiLandscape.SetActive(true);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(true);
					gameOverPortrate.SetActive(false);
				}
				else
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(false);
					LeaderBordUiLandscape.SetActive(true);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(true);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(false);
				}
			}
			else
			{
				if (Gameover)
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(true);
					LeaderBordUiLandscape.SetActive(false);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(true);
				}
				else
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(true);
					LeaderBordUiLandscape.SetActive(false);
					leaderbordPortrate.SetActive(true);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(false);
				}
			}
			
            string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			int teamcount = playerJson["data"].Count;
            
			for (int i = 0; i < teamcount; i++)
            {
				leaderbordNameLandscape[i].text = playerJson["data"].AsArray[i]["name"];
				leaderbordScoreLandscape[i].text = playerJson["data"].AsArray[i]["score"];
				leaderbordNamePortrait[i].text = playerJson["data"].AsArray[i]["name"];
				leaderbordScorePortrait[i].text = playerJson["data"].AsArray[i]["score"];

            }
			
			int userRank = (int.Parse(playerJson["user_rank"]))-1;
			
			if (userRank == 403)
			{
				
			}
		    else
			{
				for (int i = 0; i < 9; i++)
				{
					yourScoreOverlyLandscape[i].SetActive(false);
					yourScoreOverlyPortrate[i].SetActive(false);
				}
				yourScoreOverlyLandscape[userRank].SetActive(true);
				yourScoreOverlyPortrate[userRank].SetActive(true);
			}
        }
    }

	IEnumerator GetLeadeboard(string uri)
    {
		string decrypted = aes.Decrypt(apikey, Dkey);
		
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization","Bearer " + decrypted);
		
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderborderror.SetActive(true);
        }
        else
        {
			//Debug.Log(uwr.downloadHandler.text);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(false);
			leaderborderror.SetActive(false);
			leaderbordOn = true;
			
			if (orientationLandscape)
			{
				if (Gameover)
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(false);
					LeaderBordUiLandscape.SetActive(true);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(true);
					gameOverPortrate.SetActive(false);
				}
				else
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(false);
					LeaderBordUiLandscape.SetActive(true);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(true);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(false);
				}
			}
			else
			{
				if (Gameover)
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(true);
					LeaderBordUiLandscape.SetActive(false);
					leaderbordPortrate.SetActive(false);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(true);
				}
				else
				{
					UserLoginUi.SetActive(true);
					GameMainMenu.SetActive(false);
					GuestUi.SetActive(false);
					UserUi.SetActive(false);
					UserVerifyingUi.SetActive(false);
					LeaderBordUiPortrate.SetActive(true);
					LeaderBordUiLandscape.SetActive(false);
					leaderbordPortrate.SetActive(true);
					leaderbordLandscape.SetActive(false);
					gameOverLandscape.SetActive(false);
					gameOverPortrate.SetActive(false);
				}
			}
			
            string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			int teamcount = playerJson["data"].Count;
            
			for (int i = 0; i < teamcount; i++)
            {
				leaderbordNameLandscape[i].text = playerJson["data"].AsArray[i]["name"];
				leaderbordScoreLandscape[i].text = playerJson["data"].AsArray[i]["score"];
				leaderbordNamePortrait[i].text = playerJson["data"].AsArray[i]["name"];
				leaderbordScorePortrait[i].text = playerJson["data"].AsArray[i]["score"];

            }
			
			int userRank = (int.Parse(playerJson["user_rank"]))-1;
			
			if (userRank == 403)
			{
				
			}
		    else
			{
				for (int i = 0; i < 9; i++)
				{
					yourScoreOverlyLandscape[i].SetActive(false);
					yourScoreOverlyPortrate[i].SetActive(false);
				}
				yourScoreOverlyLandscape[userRank].SetActive(true);
				yourScoreOverlyPortrate[userRank].SetActive(true);
			}
        }
    }
	
	IEnumerator GetSandboxUser(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization","Bearer " + Dtoken);
        
		yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
			GuestUi.SetActive(true);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			gusetUser=true;
        }
        else
        {
			//Debug.Log(uwr.downloadHandler.text);
            string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			var msg= playerJson["message"];
			
			if (msg == "Unauthenticated.")
			{
				gusetUser=true;
				GuestUi.SetActive(false);
				UserUi.SetActive(true);
				UserVerifyingUi.SetActive(false);
				DisplayName.text = "Guest";
			}
		    else
			{
				DisplayName.text = playerJson["data"]["user"]["user_name"];
				GuestUi.SetActive(false);
				UserUi.SetActive(true);
				UserVerifyingUi.SetActive(false);
				gusetUser=false;
			}
        }
    }
	
    IEnumerator GetCurrentUser(string uri)
    {
		string decrypted = aes.Decrypt(apikey, Dkey);
		//Debug.Log(decrypted);
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization","Bearer " + decrypted);
        
		yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
			GuestUi.SetActive(true);
			UserUi.SetActive(false);
			UserVerifyingUi.SetActive(false);
			gusetUser=true;
        }
        else
        {
			//Debug.Log(uwr.downloadHandler.text);
            string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			var msg= playerJson["message"];
			
			if (msg == "Unauthenticated.")
			{
				gusetUser=true;
				GuestUi.SetActive(false);
				UserUi.SetActive(true);
				UserVerifyingUi.SetActive(false);
				DisplayName.text = "Guest";
			}
		    else
			{
				DisplayName.text = playerJson["data"]["user"]["user_name"];
				GuestUi.SetActive(false);
				UserUi.SetActive(true);
				UserVerifyingUi.SetActive(false);
				gusetUser=false;
				loadFortArcPoints();
			}
        }
    }

	IEnumerator SendSandboxScore(string uri)
    {
		_SendScoreJson.game_registry_id = gameid;
		_SendScoreJson.score=gamescore;
        string ScoreJson = JsonUtility.ToJson(_SendScoreJson);
		//Debug.Log(ScoreJson);

        var req = new UnityWebRequest(uri, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(ScoreJson);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Accept", "application/json");
		req.SetRequestHeader("Authorization","Bearer " + Dtoken);
		
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(true);
			yourScoreBackplate.SetActive(true);
			highScoreBackplate.SetActive(false);
			ScoreUi.SetActive(true);
			popupscoretxt.text = ScoreInput.text;
			popupHiscoretxt.text = ScoreInput.text;
        }
        else
        {
			string jsonString = req.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			YoueScorePortrate.text = playerJson["data"]["last_score"];
			HiScorePortrate.text =  playerJson["data"]["highest_score"];
			YoueScoreLandscape.text = playerJson["data"]["last_score"];
			HiScoreLandscape.text = playerJson["data"]["highest_score"];
			//Debug.Log(req.downloadHandler.text);
			leaderbord();
			
			if ((ScoreInput.text)==(playerJson["data"]["highest_score"]))
			{
				ScoreUi.SetActive(true);
				popupscoretxt.text = ScoreInput.text;
				popupHiscoretxt.text = ScoreInput.text;
				yourScoreBackplate.SetActive(false);
				highScoreBackplate.SetActive(true);
			}
			else
			{
				ScoreUi.SetActive(true);
				popupscoretxt.text = ScoreInput.text;
				popupHiscoretxt.text = ScoreInput.text;
				yourScoreBackplate.SetActive(true);
				highScoreBackplate.SetActive(false);
			}
        }
    }	
	
	IEnumerator SendScore(string uri)
    {
		string decrypted = aes.Decrypt(apikey, Dkey);
		
		_SendScoreJson.game_registry_id = gameid;
		_SendScoreJson.score=gamescore;
        string ScoreJson = JsonUtility.ToJson(_SendScoreJson);
		//Debug.Log(ScoreJson);

        var req = new UnityWebRequest(uri, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(ScoreJson);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Accept", "application/json");
		req.SetRequestHeader("Authorization","Bearer " + decrypted);
		
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log("Error While Sending: " + req.error);
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(true);
			yourScoreBackplate.SetActive(true);
			highScoreBackplate.SetActive(false);
			ScoreUi.SetActive(true);
			popupscoretxt.text = ScoreInput.text;
			popupHiscoretxt.text = ScoreInput.text;
        }
        else
        {
			string jsonString = req.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			YoueScorePortrate.text = playerJson["data"]["last_score"];
			HiScorePortrate.text =  playerJson["data"]["highest_score"];
			YoueScoreLandscape.text = playerJson["data"]["last_score"];
			HiScoreLandscape.text = playerJson["data"]["highest_score"];
			//Debug.Log(req.downloadHandler.text);
			leaderbord();
			
			if ((playerJson["data"]["last_score"])==(playerJson["data"]["highest_score"]))
			{
				ScoreUi.SetActive(true);
				popupscoretxt.text = ScoreInput.text;
				popupHiscoretxt.text = ScoreInput.text;
				yourScoreBackplate.SetActive(false);
				highScoreBackplate.SetActive(true);
			}
			else
			{
				ScoreUi.SetActive(true);
				popupscoretxt.text = ScoreInput.text;
				popupHiscoretxt.text = ScoreInput.text;
				yourScoreBackplate.SetActive(true);
				highScoreBackplate.SetActive(false);
			}
        }
    }
	
	IEnumerator GetForArcPoints(string uri)
    {
		string decrypted = aes.Decrypt(apikey, Dkey);
		//Debug.Log("decrypted: " + decrypted);

		UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization", "Bearer " + decrypted);
		
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uwr.error);
		}
		else
		{
			//Debug.Log("Suc: " + uwr.url);
			string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
			
			var msg = playerJson["message"];
			
			if (msg == "Success")
			{
				fortPoints = playerJson["data"]["points_balance"];

				FortArcPoints.text = "Points: " + fortPoints.ToString();
				FortArcPointsUI.SetActive(true);
			}
			else
			{
				FortArcPointsUI.SetActive(false);
			}
		}
	}

	IEnumerator CheckBonusPoints(string uri)
	{
		Debug.Log("checkBonusPoints: " + fortPoints);
		string decrypted = aes.Decrypt(apikey, Dkey);

		UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization", "Bearer " + decrypted);

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uwr.error);
		}
		else
		{
			Debug.Log("check bonus url");
			string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

			var msg = playerJson["status"];

			if (msg == "success")
			{
				checkBonusPoints = playerJson["message"];
			}
            else
            {
				Debug.Log("Fail: " + playerJson["message"]);
            }
		}
	}

	IEnumerator SendBonusPoints(string uri)
    {
		Debug.Log("addBonusPoints: " + fortPoints);
		string decrypted = aes.Decrypt(apikey, Dkey);

		UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");
		uwr.SetRequestHeader("Authorization", "Bearer " + decrypted);
		
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uwr.error);
		}
		else
		{
			Debug.Log("add bonus url: " + uwr.url);
			string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

			var msg = playerJson["message"];
            /*StartCoroutine(GetForArcPoints(uri));
            Debug.Log("get points:" + msg);*/

            if (msg == "Success")
			{
				addBonusPoints = playerJson["data"]["points_balance"];

				FortArcPoints.text = "Points: " + addBonusPoints.ToString();
				Debug.Log("get points:" + msg);
				FortArcPointsUI.SetActive(true);
			}
			else
			{
				FortArcPointsUI.SetActive(false);
			}
		}
	}

	IEnumerator GetVideoAdvertiesment(string uri)
    {
		Debug.Log("GetVideoAdvertiesment(string uri)");
		UnityWebRequest uwr = UnityWebRequest.Get(uri);
		uwr.SetRequestHeader("Content-Type", "application/json");
		uwr.SetRequestHeader("Accept", "application/json");

		if (!gusetUser && BuildForBoom)
		{
			string decrypted = aes.Decrypt(apikey, Dkey);
			uwr.SetRequestHeader("Authorization", "Bearer " + decrypted);
		}
		
		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError)
		{
			Debug.Log("Error While Sending: " + uwr.error);
			FindObjectOfType<VideoController>().CloseVideoAdError();
		}
		else
		{
			Debug.Log("get video url: " + uwr.url);
			string jsonString = uwr.downloadHandler.text;
			JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

			var msg = playerJson["message"];
			if (msg == "Successful")
			{
				Debug.Log("get url: " + playerJson["data"][0]["video"]);
				videoUrl = "https://fortarc.com" + playerJson["data"][0]["video"];
				redirectLink = playerJson["data"][0]["redirect_link"];
				Debug.Log("Video Url: " + videoUrl);

				videoController.GetComponent<VideoController>().Play(videoUrl);
                if (FindObjectOfType<GameController>().adWatchCount > 0)
                {
					FindObjectOfType<GameController>().adWatchCount--;
				}
                else
                {
					FindObjectOfType<GameController>().adWatchCount = 0;
				}
			}
		}
	}

	public void loadFortArcPoints()
    {
        if (!gusetUser)
        {
			StartCoroutine(GetForArcPoints(Dfortarcpoints));
		}
    }

	public void checkBonusPoint()
	{
		if (!gusetUser)
		{
			//Debug.Log("Start Coroutine Send Earned Points");
			StartCoroutine(CheckBonusPoints(Dcheckbonuspoints));
		}
	}

	public void sendBonusPoint()
    {
		if (!gusetUser)
		{
			Debug.Log("Start Coroutine Send Earned Points");
			StartCoroutine(SendBonusPoints(Daddbonuspoints));
		}
	}

    public void getVideoAdvertiesments()
	{
		//Debug.Log("getVideoAdvertiesments()");
		StartCoroutine(GetVideoAdvertiesment(Dgetvideoad));
    }

    public void loadLeadebordFromMore()
    {
        if (gusetUser)
        {
			Gameover=true;
			LoadingLeaderbordUi.SetActive(false);
			LeaderbordGuestUi.SetActive(true);
			yourScoreBackplate.SetActive(true);
			highScoreBackplate.SetActive(false);
			ScoreUi.SetActive(true);
			popupscoretxt.text = ScoreInput.text;
			popupHiscoretxt.text = ScoreInput.text;
        }
        else
        {
			Gameover=true;
			leaderbord();
        }
    }
}

[System.Serializable]

public class SendScoreJson{
    public int game_registry_id;
    public int score;
}
