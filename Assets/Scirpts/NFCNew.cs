using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class NFCNew : MonoBehaviour
{
	bool Key = true;
	public string tagID;
	public Text tag_output_text;
	public bool tagFound = false;
	private AndroidJavaObject mActivity;
	private AndroidJavaObject mIntent;
	private string sAction;
	public AudioSource audioSourceRed;
	public AudioSource audioSourceYellow;
	public AudioSource audioSourceGreen;
	public GameObject Green;
	public GameObject Yellow;
	public GameObject Red;
	public GameObject Button;
	public GameObject ButtonYellow;
	public GameObject ScanAnimation;
	bool canScan = true;
	void Start()
	{
		tag_output_text.text = "";
	}
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			tag_output_text.text = tagFound.ToString();
			if (!tagFound) //remove
			{
				if (Key)
				{
					// Create new NFC Android object
					mActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"); // Activities open apps

					mIntent = mActivity.Call<AndroidJavaObject>("getIntent");
					
					sAction = mIntent.Call<String>("getAction"); // resulte are returned in the Intent object

					if (sAction == "android.nfc.action.NDEF_DISCOVERED")
					{
						Debug.Log("Tag of type NDEF");
					}
					else if (sAction == "android.nfc.action.TECH_DISCOVERED")
					{
						Debug.Log("TAG DISCOVERED");
						// Get ID of tag
						AndroidJavaObject mNdefMessage = mIntent.Call<AndroidJavaObject>("getParcelableExtra", "android.nfc.extra.TAG");
						if (mNdefMessage != null)
						{
							byte[] payLoad = mNdefMessage.Call<byte[]>("getId");
							string text = System.Convert.ToBase64String(payLoad);
							tag_output_text.text += "This is your tag text: " + text;
							canScan = true;
							tagID = text; //this is constantly running even though 
							StartCoroutine(CountDown());
						}
						else
						{
							tag_output_text.text = "No ID found !";
						}
						if (canScan)
							StartCoroutine(Count());
						return;
					}
					else if (sAction == "android.nfc.action.TAG_DISCOVERED")
					{
						Debug.Log("This type of tag is not supported !");
					}
					else
					{
						tag_output_text.text = "Scan a NFC tag to make the cube disappear...";
						return;
					}
				}
			}
		}
		mIntent.Call("removeExtra", "android.nfc.extra.TAG");
	}

	IEnumerator Count()
	{
		canScan = false;
		tagFound = true;
		Key = false;
		yield return new WaitForSeconds(0.05f);
		tagID = "dasd";
		mIntent.Call("removeExtra", "android.nfc.extra.TAG");
        tagFound = false;
		Key = true;
		yield return new WaitForSeconds(7);
		mIntent.Call("removeExtra", "android.nfc.extra.TAG");
	}
	IEnumerator CountDown()
    {
		yield return new WaitForSeconds(7);
		Green.SetActive(false);
		Yellow.SetActive(false);
		Red.SetActive(false);
		ScanAnimation.SetActive(true);
	}
	void FixedUpdate()
	{
		if (tagID == "AckwHQ==")
		{
			ScanAnimation.SetActive(false);
			Green.SetActive(true);
			Yellow.SetActive(false);
			Red.SetActive(false);
			Button.SetActive(true);
			audioSourceGreen.Play();	
		}

		if (tagID == "Ef9vHQ==")
		{
			ScanAnimation.SetActive(false);
			Yellow.SetActive(true);
			Green.SetActive(false);
			Red.SetActive(false);
			ButtonYellow.SetActive(true);
			Button.SetActive(true);
			audioSourceYellow.Play();
		}

		if (tagID == "8bh2HQ==")
		{
			ScanAnimation.SetActive(false);
			Red.SetActive(true);
			Green.SetActive(false);
			Yellow.SetActive(false);
			Button.SetActive (true);
			audioSourceRed.Play();
		}

	}
	public void OnClick()
    {
		Red.SetActive(false);
		Green.SetActive(false);
		Yellow.SetActive(false);
		Button.SetActive(false);
		ButtonYellow.SetActive(false);
		ScanAnimation.SetActive(true);
	}

}