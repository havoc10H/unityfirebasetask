using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;

public class SignupSceneScript : MonoBehaviour
{
	public TMP_InputField firstNameObject;
	public TMP_InputField lastNameObject;
	public TMP_InputField emailObject;
	public TMP_InputField passwordObject;

	public TextMeshProUGUI status;
	public GameObject spinner;

	public Animator transition;

	private bool isPasswordVisible;
	private bool emailInvalid = false;
	private bool nameInvalid = false;

	private void Start()
	{
		emailObject.onSelect.AddListener(OnInputFieldSelect);
		firstNameObject.onSelect.AddListener(OnInputNameSelect);
		lastNameObject.onSelect.AddListener(OnInputNameSelect);
		emailInvalid = false;
		nameInvalid = false;
	}
	void OnInputFieldSelect(string text)
    {
		if (emailInvalid) {
        	status.gameObject.SetActive(false);
			emailInvalid = false;
		}
    }
	void OnInputNameSelect(string text)
    {
		if (nameInvalid) {
        	status.gameObject.SetActive(false);
			nameInvalid = false;
		}
    }

	private void Update()
	{
	}

    [System.Obsolete]
    public void OnEndEdit(GameObject currentInputField) {
		string currentInputFieldName = currentInputField.gameObject.name;
		switch (currentInputFieldName) {
			case "FirstName":
				lastNameObject.Select();
				break;
			case "LastName":
				emailObject.Select();
				break;
			case "EmailID":
				passwordObject.Select();
				break;
			case "password":
				OnSignupButtonClicked();
				break;
			default:
				firstNameObject.Select();
				break;
		}
	}

    [System.Obsolete]
    public void OnSignupButtonClicked()
	{
		string firstName = firstNameObject.text;
		string lastName = lastNameObject.text;
		string email = emailObject.text;
		string password = passwordObject.text;

		string url = "https://dev-engine.chessface3d.com/api/v1/register";
		string data = $"{{\n    \"email\": \"{email}\",\n    \"password\": \"{password}\"\n}}";

		if (!validateName(firstName) || !validateName(lastName)) {
			status.text = "Name is invalid.";
			status.gameObject.SetActive(true);
			nameInvalid = true;
			return;
		}
		if (validateEmail(email)) {
			StartCoroutine(SendSignupRequest(url, data));
		} else {
			status.text = "Email address is invalid.";
			status.gameObject.SetActive(true);
			emailInvalid = true;
		}
	}

    [System.Obsolete]
    private IEnumerator<object> SendSignupRequest(string url, string data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        spinner.SetActive(true);
		yield return request.SendWebRequest();
		spinner.SetActive(false);

		ConfirmData resMsg = JsonUtility.FromJson<ConfirmData>(request.downloadHandler.text);
		if (request.result != UnityWebRequest.Result.Success)
        {
			if (request.isNetworkError)
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
			StartCoroutine(LoadSceneWithTransition("LoginScene"));
        }
	}

    [System.Serializable]
    private class ConfirmData
    {
		public string success;
        public string message;
    }

	public void OnGoogleButtonClicked()
	{
	}

	public void OnFacebookButtonClicked()
	{
	}

	public void OnLoginClicked()
	{
		StartCoroutine(LoadSceneWithTransition("LoginScene"));
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

	public static bool validateName(string name) {
		if (name != null)
			return Regex.IsMatch(name, "^[a-zA-Z]+$");
		else
			return false;
	}

	public const string MatchEmailPattern =
		@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
		+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
		+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
		+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
	IEnumerator<object> LoadSceneWithTransition(string sceneName)
    {	
		// transition.SetTrigger("TriggerFadeOut");
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
