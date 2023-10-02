using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ForgotpasswordSceneScript : MonoBehaviour
{
	public GameObject forgotPasswordDialog;

	public GameObject verificationDialog;

	public GameObject newPasswordDialog;

	public TMP_InputField emailObject;

	public TMP_InputField verificationCode1;

	public TMP_InputField verificationCode2;

	public TMP_InputField verificationCode3;

	public TMP_InputField verificationCode4;

	public TMP_InputField verificationCode5;

	public TMP_InputField newPassword;

	public TMP_InputField retypePassword;

	public Animator transition;

	private int backFlag;

	private void Start()
	{
		backFlag = 0;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnBacked();
		}
	}

	public void OnSendButtonClicked()
	{
		/*
		string email = emailObject.text;

		// Create a form to hold data
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        string url = "http://localhost:3000/logintest";

		StartCoroutine(SendEmailRequest(form, url));
		*/
		forgotPasswordDialog.SetActive(value: false);
		verificationDialog.SetActive(value: true);
		backFlag = 1;
	}
/*
	private IEnumerator SendEmailRequest(WWWForm form, string url)
    {   	
        // Send the form data to the server using a POST request
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Login successful, get the welcome message as JSON
            string jsonText = www.downloadHandler.text;

            // Parse the JSON using JsonUtility
            ConfirmData welcomeMessage = JsonUtility.FromJson<ConfirmData>(jsonText);

            // Access the parsed data
            // Debug.Log("Welcome, " + welcomeMessage.message + welcomeMessage.email);
            // Debug.Log(welcomeMessage.email);
            forgotPasswordDialog.SetActive(value: false);
			verificationDialog.SetActive(value: true);
			backFlag = 1;
        }
        else
        {
            // Login failed
            Debug.Log("Login failed: " + www.error);
        }
	}

    [System.Serializable]
    private class ConfirmData
    {
		public string message;
        public string email;
        public string password;
    }
*/
	public void OnResentButtonClicked()
	{
		verificationCode1.text = "";
		verificationCode2.text = "";
		verificationCode3.text = "";
		verificationCode4.text = "";
		verificationCode5.text = "";
	}

	public void OnVerifyButtonClicked()
	{
		/*
		string message = verificationCode1.text + verificationCode2.text + verificationCode3.text + verificationCode4.text + verificationCode5.text;

		// Create a form to hold data
        WWWForm form = new WWWForm();
        form.AddField("verify", message);

        string url = "http://localhost:3000/logintest";

		StartCoroutine(SendVerifyRequest(form, url));
		*/
		verificationDialog.SetActive(value: false);
		newPasswordDialog.SetActive(value: true);
		backFlag = 2;
	}
/*
	private IEnumerator SendVerifyRequest(WWWForm form, string url)
    {   	
        // Send the form data to the server using a POST request
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Login successful, get the welcome message as JSON
            string jsonText = www.downloadHandler.text;

            // Parse the JSON using JsonUtility
            ConfirmData welcomeMessage = JsonUtility.FromJson<ConfirmData>(jsonText);

            // Access the parsed data
            // Debug.Log("Welcome, " + welcomeMessage.message + welcomeMessage.email);
            // Debug.Log(welcomeMessage.email);
            
			verificationDialog.SetActive(value: false);
			newPasswordDialog.SetActive(value: true);
			backFlag = 2;
        }
        else
        {
            // Login failed
            Debug.Log("Login failed: " + www.error);
        }
	}
*/
	public void OnSubmitButtonClicked()
	{
		/*
		string newPass = newPassword.text;
		string confirmPass = retypePassword.text;

		// Create a form to hold data
        WWWForm form = new WWWForm();
        form.AddField("newPass", newPass);
        form.AddField("confirmPass", confirmPass);

        string url = "http://localhost:3000/logintest";

		StartCoroutine(SendPasswordResetRequest(form, url));
		*/
		StartCoroutine(LoadSceneWithTransition("LoginScene"));
	}
/*
	private IEnumerator SendPasswordResetRequest(WWWForm form, string url)
    {   	
        // Send the form data to the server using a POST request
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Login successful, get the welcome message as JSON
            string jsonText = www.downloadHandler.text;

            // Parse the JSON using JsonUtility
            ConfirmData welcomeMessage = JsonUtility.FromJson<ConfirmData>(jsonText);

            // Access the parsed data
            // Debug.Log("Welcome, " + welcomeMessage.message + welcomeMessage.email);
            // Debug.Log(welcomeMessage.email);
            SceneManager.LoadScene("LoginScene");
        }
        else
        {
            // Login failed
            Debug.Log("Login failed: " + www.error);
        }
	}
*/
	public void OnForgotPasswordBackButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("LoginScene"));
	}

	public void OnVerifyBackButtonClicked()
	{
		verificationDialog.SetActive(value: false);
		forgotPasswordDialog.SetActive(value: true);
		backFlag = 0;
	}

	public void OnNewPasswordBackButtonClicked()
	{
		newPasswordDialog.SetActive(value: false);
		verificationDialog.SetActive(value: true);
		backFlag = 1;
	}

	public void OnBacked()
	{
		if (backFlag == 0)
		{
			OnForgotPasswordBackButtonClicked();
		}
		else if (backFlag == 1)
		{
			OnVerifyBackButtonClicked();
		}
		else
		{
			OnNewPasswordBackButtonClicked();
		}
	}
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