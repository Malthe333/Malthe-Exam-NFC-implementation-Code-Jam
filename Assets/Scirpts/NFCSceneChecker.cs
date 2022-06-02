using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NFCSceneChecker : MonoBehaviour
{
    public static NFCSceneChecker instance = null;
    public string tagID;
    public Text tag_output_text;
    public bool tagFound = false;
    private AndroidJavaObject mActivity;
    private AndroidJavaObject mIntent;
    private string sAction;
    
	private void Awake()
    {
        if (instance == null) //Hvorfor er den en singleton?
        {
            instance = this; //aktivering, instanciates 1 gang fordi det er en static.
            DontDestroyOnLoad(base.gameObject); //finder variablen som er defineret i Monobehavior. bruges i forskellgie værdier i parent class
        }
        else
        {
            Destroy(base.gameObject);
        }
    }

    private void Start()
    {
        tag_output_text.text = "";
    }
    void Update()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		if (Application.platform == RuntimePlatform.Android)
		{
			tag_output_text.text = tagFound.ToString();
			if (sceneName != "STUDYCARD")
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
							tagID = text; //this is constantly running even though 
						SceneManager.LoadScene("STUDYCARD");
						}
						else
						{
							tag_output_text.text = "No ID found !";
						}
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
		mIntent.Call("removeExtra", "android.nfc.extra.TAG");
	}
}
