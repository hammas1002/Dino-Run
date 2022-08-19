using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenInstagram()
	{
#if !UNITY_EDITOR
		openWindow("https://www.instagram.com/treetechgames/?igshid=YmMyMTA2M2Y");
#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}