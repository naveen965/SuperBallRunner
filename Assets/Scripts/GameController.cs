using System.Collections;
using System.Collections.Generic;    
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject score;
	public GameObject Coin;
    public Text scoreTxt;
    public GameObject gameStartUI;
    public GameObject track;
    public GameObject gameOverUI;   
    public GameObject gameMenuUI;
	public GameObject gameLogin;
    public GameObject gameInstructionUI;
    public GameObject leaderboardUI;
	public GameObject PauseMenu;
	public GameObject PauseButton;
	public Text email;
    public Text nametxt;
    public GameObject warningMsg;
    public GameObject serverApi;
    public GameObject nameText;
    public GameObject nextBtn;
    public GameObject startBtn;
    public GameObject audioON;
    public GameObject audioOff;
	public GameObject OnSound;
	public GameObject OffSound;
	public AudioClip tapSound;
    public AudioClip backgroundMusic;
    public AudioSource audio;
    public Button nextBtnB;
    public Button startBtnB;
    public Animator animator;
	public bool GameoveON;
	public static bool GameIsPaused = false;
	public BooomAPI booomapi;
	public GameObject cutomize;
	public GameObject bgselect;
	public GameObject ballselect;
	public GameObject selectUI;
	public int uiStatus=0;
	public int balltyper=0;
	public int bgtyper=0;
	public Renderer balltexture;
	public Texture[] ballimages;
	public GameObject[] balliconframs;
    public GameObject[] bgframs;
	public GameObject[] Background;
	public GameObject Progress;
	public GameObject Helth;
	public GameObject multiply;
	public GameObject sheld;
	public Animator timerAnimation;
	public GameObject[] Helthicon;
	public int helth=0;
	public Text infotext;
	public Animator infotextAnim;
	public GameObject infotextObj;
	public GameObject ForceField;
	public GameObject GameFinished;
	public GameObject Logos;
	public GameObject WatchVideo;
	public GameObject Exit;
	public GameObject RawImage;
	public bool SheldON=false;
	public GameObject[] spawnPos;
	public GameObject[] spawnObj;
	public GameObject[] starSpawnPosision;
	public GameObject stars;
	public string LastPowerup = "health";
	public float scoreAmount;
	private float pointIncreased=10;

	public VideoController videoController;
	public int adWatchCount = 3;


	void Start()
    {
		scoreAmount = 0f;
		LastPowerup = "health";

		if (PlayerPrefs.HasKey("balltype"))
		{
			balltyper = PlayerPrefs.GetInt("balltype");
			changeball(balltyper);
		}
		else
		{
			PlayerPrefs.SetInt("balltype", 0);
			balltyper=0;
			changeball(balltyper);
		}
		
		
		if (PlayerPrefs.HasKey("bgtype"))
		{
			bgtyper = PlayerPrefs.GetInt("bgtype");
			changeBg(bgtyper);
		}
		else
		{
			PlayerPrefs.SetInt("bgtype", 0);
			bgtyper=0;
			changeBg(bgtyper);
		}		
		
        //animator.enabled = false;
        //player.GetComponent<Timer>().enabled = false;
    }
	
    void Update()
    {
		scoreAmount += pointIncreased * Time.deltaTime;
		
		if (scoreAmount > 100)
        {
			scoreAmount = 0f;
			InstantiatePowerups();
        }
	}

	public void ClickPause()
	{
		if (GameIsPaused)
		{
			Resume();
		}
		else
		{
			Pause();
		}
	}

	public void Resume()
	{
		PauseMenu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		PauseButton.SetActive(true);
	}

	public void Pause()
	{
		PauseMenu.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
		PauseButton.SetActive(false);
	}

	public void InstantiatePowerups()
	{
		removepowrup();
		var RndObj = Random.Range(0,spawnObj.Length);
		var RndPos = Random.Range(0,spawnPos.Length);
		var RandomPosision = Random.Range(0, starSpawnPosision.Length);

		var currentobject = Instantiate(spawnObj[RndObj], new Vector3(spawnPos[RndPos].transform.position.x, spawnPos[RndPos].transform.position.y, spawnPos[RndPos].transform.position.z), spawnPos[RndPos].transform.rotation);
		currentobject.transform.parent = spawnPos[RndPos].transform;

		var starObject = Instantiate(stars, new Vector3(starSpawnPosision[RandomPosision].transform.position.x, starSpawnPosision[RandomPosision].transform.position.y, starSpawnPosision[RandomPosision].transform.position.z), starSpawnPosision[RandomPosision].transform.rotation);
		starObject.transform.parent = starSpawnPosision[RandomPosision].transform;

		if (RndObj == 0)
		{
			LastPowerup = "health";
		}
		if(RndObj == 1)
		{
			LastPowerup = "power";
		}
		if(RndObj == 2)
		{
			LastPowerup = "sheld";
		}
        if (RndObj == 3)
        {
			booomapi.checkBonusPoint();
            if (booomapi.checkBonusPoints == "Win bonus point")
            {
				LastPowerup = "coin";
            }
		}
	}
	
	public void removepowrup()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(LastPowerup);
        foreach(GameObject enemy in enemies)
        GameObject.Destroy(enemy);
	}
	
	public void dubblescore()
	{
		//player.GetComponent<Timer>().pointIncreasedPerSecond = 100;
		StartCoroutine(ExecuteAfterTime(10));
		Progress.SetActive(true);
		Helth.SetActive(false);
		multiply.SetActive(true);
		sheld.SetActive(false);
		timerAnimation.Play("timecount", 0, 0.25f);
		infotext.text="Score Multiplier Activated";
		infotextAnim.Play("infotextanim", 0, 0.25f);
		infotextObj.SetActive(true);
	}
	
	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
	 
		//player.GetComponent<Timer>().pointIncreasedPerSecond = 50;
		Progress.SetActive(false);
		Helth.SetActive(true);
		multiply.SetActive(false);
		sheld.SetActive(false);
		infotextObj.SetActive(false);
	}
	
	public void addhelth()
	{
		if(helth<3)
		{
			Helthicon[helth].SetActive(true);
			helth++;
			infotext.text="One Health Powerup Added";
	       	infotextAnim.Play("infotextanim", 0, 0.25f);
			infotextObj.SetActive(true);
		}
	}
	
	public void sheldOn()
	{
		SheldON=true;
		StartCoroutine(sheldOff(15));
		ForceField.SetActive(true);
		infotext.text="Shield Activated";
		timerAnimation.Play("timecount", 0, 0.25f);
		infotextAnim.Play("infotextanim", 0, 0.25f);
		infotextObj.SetActive(true);
		Progress.SetActive(true);
		Helth.SetActive(false);
		multiply.SetActive(false);
		sheld.SetActive(true);
	}
	
	IEnumerator sheldOff(float time)
	{
		yield return new WaitForSeconds(time);
	 
		infotext.text="Shield Deactivated";
		infotextAnim.Play("infotextanim", 0, 0.25f);
		infotextObj.SetActive(true);		
		SheldON=false;
		Progress.SetActive(false);
		Helth.SetActive(true);
		multiply.SetActive(false);
		sheld.SetActive(false);
		ForceField.SetActive(false);
	}
	
	public void changeball(int balltype)
	{
		balltexture.material.SetTexture("_MainTex", ballimages[balltype]);
		balltyper=balltype;

		PlayerPrefs.SetInt("balltype", balltype);
		for (int i = 0; i < 8; i++)
		{
           balliconframs[i].SetActive(false);
		}
		
		balliconframs[balltype].SetActive(true);
	}
	
	public void changeBg(int bgtype)
	{
		bgtyper=bgtype;
		PlayerPrefs.SetInt("bgtype", bgtyper);
		for (int i = 0; i < 4; i++)
		{
           bgframs[i].SetActive(false);
		   Background[i].SetActive(false);
		}
		
		bgframs[bgtype].SetActive(true);
		Background[bgtype].SetActive(true);
	}
	
    public void OpenGameInstruction()
    {
        cutomize.SetActive(false);
        audio.PlayOneShot(tapSound);
        gameMenuUI.SetActive(false);
        gameInstructionUI.SetActive(true);
		scoreTxt.text = "0";
		//player.GetComponent<Timer>().ScoreReset();
		GameoveON=false;
		SheldON = false;
		cutomize.SetActive(false);
		uiStatus=0;
    }
	
	public void CloseCustomizeui()
	{
		if (uiStatus==0)
		{		
			cutomize.SetActive(false);
			bgselect.SetActive(false);
			ballselect.SetActive(false);
			selectUI.SetActive(false);
		}
		else
		{
			cutomize.SetActive(true);
			bgselect.SetActive(false);
			ballselect.SetActive(false);
			selectUI.SetActive(true);
			uiStatus=0;			
		}
	}
	
	public void selectBackground()
	{
		bgselect.SetActive(true);
		ballselect.SetActive(false);
		selectUI.SetActive(false);
		uiStatus=1;
	}
	
	public void selectBall()
	{
		bgselect.SetActive(false);
		ballselect.SetActive(true);
		selectUI.SetActive(false);	
        uiStatus=1;		
	}
	
	public void Customizeui()
	{
		cutomize.SetActive(true);
		bgselect.SetActive(false);
		ballselect.SetActive(false);
		selectUI.SetActive(true);
		uiStatus=0;
	}
	
	/*public void OpenGameMenu()
    {
        audio.PlayOneShot(tapSound);
        gameMenuUI.SetActive(true);
		gameStartUI.SetActive(false);
    }*/
	
	public void mainAudioOff()
	{
		audio.Pause();
	}
	
	public void mainAudioOn()
	{
		audio.UnPause();
	}

    public void GameStart()
    {
		helth=0;
		gameMenuUI.SetActive(false);
		cutomize.SetActive(false);
        audio.Play();
        audio.PlayOneShot(tapSound);
        gameInstructionUI.SetActive(false);
        //animator.Play("Take 001", 0, 0f);
        score.SetActive(true);
		PauseButton.SetActive(true);
        //player.GetComponent<Timer>().enabled = true;
		//player.GetComponent<rotate>().RotationSpeed = -1000f;
		//animator.enabled = true;
		Helth.SetActive(true);
		GameoveON = false;
		SheldON = false;
	}

    public void GameStartre()
    {
        audio.Play();
        audio.PlayOneShot(tapSound);
        gameInstructionUI.SetActive(false);

        //animator.Play("Take 001", 0, 0f);
        score.SetActive(true);
        //FortArcPoints.SetActive(true);
        //player.GetComponent<Timer>().enabled = true;
        //player.GetComponent<rotate>().RotationSpeed = -1000f;
        //animator.enabled = true;
        scoreTxt.text = "0";
        //player.GetComponent<Timer>().ScoreReset();
        GameoveON = false;
    }
    // Start is called before the first frame update

    public void StartGame()
    {
        audio.PlayOneShot(tapSound);
        if (email.text == "")
        {
            warningMsg.SetActive(true);
        }
        else
        {
            warningMsg.SetActive(false);
            nextBtnB.interactable = false;
        }     
    }

    public void GameOver()
    {
		if(helth == 0)
		{
			if (!GameoveON && !SheldON)
			{
				if(adWatchCount != 0)
                {
					//player.GetComponent<Timer>().enabled = false;
					//player.GetComponent<rotate>().RotationSpeed = 0;
					audio.Stop();
					//animator.enabled = false;
					//gameOverUI.SetActive(true);
					GameFinished.SetActive(true);
					WatchVideo.SetActive(true);
					Exit.SetActive(true);
					RawImage.SetActive(false);
					Debug.Log("Game Over");
					GameoveON = true;
				}
                else
                {
					//player.GetComponent<Timer>().enabled = false;
					//player.GetComponent<rotate>().RotationSpeed = 0;
					audio.Stop();
					//animator.enabled = false;
					booomapi.sendscoredata();
					Debug.Log("Game Over");
					GameoveON = true;
				}
			}
		}
		else
		{
			if (!SheldON)
			{
				helth = helth - 1;
				Helthicon[helth].SetActive(false);
				infotext.text = "You Lost One Helth";
				infotextAnim.Play("infotextanim", 0, 0.25f);
				infotextObj.SetActive(true);
			}
		}
    }

    public void ClickExit()
    {
		audio.PlayOneShot(tapSound);
		booomapi.sendscoredata();
		GameFinished.SetActive(false);
	}

	public void WatchAd()
    {
		booomapi.getVideoAdvertiesments();
		Debug.Log("WatchAd()");
		RawImage.SetActive(true);
		Logos.SetActive(false);
		WatchVideo.SetActive(false);
		Exit.SetActive(false);
	}

    /*public void PlayAgain()
    {
        audio.PlayOneShot(tapSound);
        gameInstructionUI.SetActive(true);
        scoreTxt.text = "0";
        player.GetComponent<Timer>().ScoreReset();
        gameOverUI.SetActive(false);
    }

    public void OpenLeaderBoard()
    {
        audio.PlayOneShot(tapSound);
        gameMenuUI.SetActive(false);
        leaderboardUI.SetActive(true);
		booomapi.leaderbord();
    }

    public void CloseLeaderBoard()
    {
        audio.PlayOneShot(tapSound);
        gameMenuUI.SetActive(true);
        leaderboardUI.SetActive(false);
    }

    public void GameLoad()
    {
        audio.PlayOneShot(tapSound);
        gameStartUI.SetActive(false);
        gameMenuUI.SetActive(true);
    }
    
    public void GameUserSaveFormOpen()
    {
        audio.PlayOneShot(tapSound);
        nameText.SetActive(true);
        nextBtn.SetActive(false);
        startBtn.SetActive(true);
    }*/

	public void ExitGameToMenu()
	{
		audio.PlayOneShot(tapSound);
		gameMenuUI.SetActive(true);
		PauseMenu.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		//player.GetComponent<Timer>().enabled = false;
		//player.GetComponent<rotate>().RotationSpeed = 0;
		audio.Stop();
		//animator.enabled = false;
		GameoveON = true;
	}

	public void SoundOff()
	{
		audioOff.SetActive(true);
		audioON.SetActive(false);
		OffSound.SetActive(true);
		OnSound.SetActive(false);
		audio.volume = 0f;
		AudioListener.volume = 0;
	}

	public void SoundON()
	{
		audioOff.SetActive(false);
		audioON.SetActive(true);
		OffSound.SetActive(false);
		OnSound.SetActive(true);
		audio.volume = 1f;
		AudioListener.volume = 1;
	}

	public void CoinCollected()
    {
		booomapi.sendBonusPoint();
	}
}
