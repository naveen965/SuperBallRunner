using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public GameObject videoUIRawImage;
    public GameObject player;
    //public string url;
    private bool isPlay;

    Link externalLink;
    private string redirectURL;
    private bool mobile;
    private bool iPhone;

    // Start is called before the first frame update
    void Start()
    {
        mobile = FindObjectOfType<BooomAPI>().mobile;
        iPhone = FindObjectOfType<BooomAPI>().iPhone;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = true;
        //SetClip();
        Invoke("PlayVideo", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            videoPlayer.Play();
            videoPlayer.isLooping = false;
            isPlay = false;
        }
    }

    /*public void SetClip()
    {
        videoPlayer.url = FindObjectOfType<BooomAPI>().videoUrl;
        redirrectURL = FindObjectOfType<BooomAPI>().redirectLink;
    }*/

    public void Play(string url)
    {
        videoPlayer.url = url;
        redirectURL = FindObjectOfType<BooomAPI>().redirectLink;
        isPlay = true;
        videoPlayer.loopPointReached += CloseAd;
        FindObjectOfType<GameController>().addhelth();
    }

    public void ClickAds()
    {
        if (mobile && iPhone)
        {
            externalLink.OpenLink(redirectURL);
        }
        else if (mobile)
        {
            externalLink.OpenLinkJS(redirectURL);
        }
        else
        {
            externalLink.OpenLinkJSPlugin(redirectURL);
        }
    }

    public void CloseAd(VideoPlayer vp)
    {
        videoUIRawImage.SetActive(false);
        player.GetComponent<Timer>().enabled = true;
        player.GetComponent<rotate>().RotationSpeed = -1000;
        FindObjectOfType<GameController>().audio.Play();
        FindObjectOfType<GameController>().animator.enabled = true;
        FindObjectOfType<GameController>().GameoveON = false;
        FindObjectOfType<GameController>().GameFinished.SetActive(false);
        FindObjectOfType<GameController>().WatchVideo.SetActive(false);
        FindObjectOfType<GameController>().Exit.SetActive(false);

        StartCoroutine(sheildOnOff(4));
    }

    IEnumerator sheildOnOff(float time)
    {
        FindObjectOfType<GameController>().SheldON = true;

        yield return new WaitForSeconds(time);

        FindObjectOfType<GameController>().SheldON = false;
    }

    public void CloseVideoAdError()
    {
        videoUIRawImage.SetActive(false);
        FindObjectOfType<GameController>().ClickExit();
    }
}
