using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Net.Mail;
using System;


public class LoginSceneScript : MonoBehaviour
{   
    [System.Serializable]
	public class ResponseData
	{
		public bool success;
		public string message;
		public UserData data;
	}

	[System.Serializable]
	public class UserData
	{
		public string user_id;
		public string email;
		public string access_token;
		public string id_token;
		public int expire_in;
	}
	public TMP_InputField emailObject;

	public TMP_InputField passwordObject;

    public Animator transition;

	public GameObject spinner;
	public TextMeshProUGUI status;

    private bool isPasswordVisible;
	private bool emailInvalid = false;

	private void Start()
	{
		emailObject.onSelect.AddListener(OnInputFieldSelect);
		emailInvalid = false;
	}
	void OnInputFieldSelect(string text)
    {
		if (emailInvalid) {
        	status.gameObject.SetActive(false);
			emailInvalid = false;
		}
    }
	public void OnEndEdit(GameObject currentInputField) {
		string currentInputFieldName = currentInputField.gameObject.name;
		switch (currentInputFieldName) {
			case "EmailID":
				passwordObject.Select();
				break;
			case "password":
				OnLoginButtonClicked();
				break;
			default:
				emailObject.Select();
				break;
		}
	}

	public void OnLoginButtonClicked()
	{		
		string email = emailObject.text;
		string password = passwordObject.text;

		string url = "https://dev-engine.chessface3d.com/api/v1/login";
		string data = $"{{\n    \"email\": \"{email}\",\n    \"password\": \"{password}\"\n}}";

		if (validateEmail(email)) {
			StartCoroutine(SendLoginRequest(url, data));
		} else {
			status.text = "Email address is invalid.";
			status.gameObject.SetActive(true);
			emailInvalid = true;
		}
		
	}
    private IEnumerator<object> SendLoginRequest(string url, string data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

		spinner.SetActive(true);
        yield return request.SendWebRequest();
		spinner.SetActive(false);

		ResponseData resMsg = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
		if (request.result != UnityWebRequest.Result.Success)
        {
			if (request.result == UnityWebRequest.Result.ConnectionError)
            {
				status.text = "Network error.";
				status.gameObject.SetActive(true);
				yield return new WaitForSeconds(2.0f);
				status.gameObject.SetActive(false);
            }
            else
            {
                status.text = resMsg.message;
				status.gameObject.SetActive(true);
				yield return new WaitForSeconds(2.0f);
				status.gameObject.SetActive(false);
            }
        }
        else
        {
			PlayerPrefs.SetString("emailAddress", resMsg.data.email);
        	PlayerPrefs.Save();
			StartCoroutine(LoadSceneWithTransition("MainScene"));
        }
	}
	public void OnGoogleButtonClicked()
	{
	}

	public void OnFacebookButtonClicked()
	{
	}

	public void OnForgotPasswordClicked()
	{
        StartCoroutine(LoadSceneWithTransition("ForgotpasswordScene"));
	}

	public void OnSignUpClicked()
	{
        StartCoroutine(LoadSceneWithTransition("SignupScene"));
	}
    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            passwordObject.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordObject.contentType = TMP_InputField.ContentType.Password;
        }

        passwordObject.ForceLabelUpdate();
    }
	public static bool validateEmail(string email)
	{
		// if (email != null)
		// 	return Regex.IsMatch(email, MatchEmailPattern);
		// else
		// 	return false;
		try
		{
			MailAddress mailAddress = new MailAddress(email);
			return true;
		}
		catch (FormatException)
		{
			return false;
		}
	}

	// public const string MatchEmailPattern =
	// 	@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
	// 	+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
	// 	+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
	// 	+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    IEnumerator<object> LoadSceneWithTransition(string sceneName)
    {
		gameObject.GetComponent<ChangeCanvasGroupColor>().enabled = true;
        Destroy(GameObject.Find("Directional Light"));
		Destroy(GameObject.Find("EventSystem"));
        LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive);//, LocalPhysicsMode.Physics3D
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, param);
		yield return new WaitForSeconds(1.0f);
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));		
    }
}
