using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{


	public void OpenLink(string url)
	{
		Application.OpenURL(url);
	}

	public void OpenLinkJS(string url)
	{
		Application.ExternalEval("window.open('"+url+"');");
	}

	public void OpenLinkJSPlugin(string url)
	{
		#if !UNITY_EDITOR
		openWindow(url);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}