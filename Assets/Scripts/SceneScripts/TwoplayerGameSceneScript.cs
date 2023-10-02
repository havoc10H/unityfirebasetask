using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TwoplayerGameSceneScript : MonoBehaviour
{
	public GameObject pauseDialog;

	public GameObject quitDialog;
	public GameObject purchaseCoinDialog;
	public GameObject referDialog;
	public GameObject settingsDialog;
	public GameObject rotObj;
	
	// public TextMeshProUGUI my_nickname; // my nick name
	// public TextMeshProUGUI other_nickname; // other nick name
	public bool isDialogOpened;
	public GameObject target;

	private Vector3 mouseOrigin;
    private Vector3 originCamPos = new Vector3(4.0f, 7.0f, -3.0f);
	private Vector3 firstCamPos = new Vector3(4.0f, 12.61f, 1.98f);
	private Vector3 offset;
	private readonly float originView = 60.0f;
	private Quaternion initialRotation = new Quaternion(0.4462f, 0.0f, 0.0f, 0.89493f);
	private Quaternion firstRotation = new Quaternion(0.65276f, 0.0f, 0.0f, 0.75276f);
	private float rotationTime =  0f;
	private float totalAng = 30.0f;
	private float totalFieldOfView = 30.0f;
	private bool doCamRot = true;
	private readonly float fixedTimeStep = 1f / 30f;

	private void Start()
	{
		if (target == null)
        {         
            enabled = false;  // Disable the script
            return;
        }
		isDialogOpened = false;
		Camera.main.transform.position = firstCamPos;
		Camera.main.transform.rotation = firstRotation;
		Camera.main.fieldOfView = originView + totalFieldOfView;
		rotationTime += fixedTimeStep;
	}

	public void OnBackButtonClicked()
	{
		isDialogOpened = true;
		pauseDialog.SetActive(false);
		quitDialog.SetActive(value: true);
	}

	public void OnQuitNoButtonClicked()
	{
		isDialogOpened = false;
		quitDialog.SetActive(value: false);
	}
	public void OnQuitYesButtonClicked()
	{
		isDialogOpened = false;
		quitDialog.SetActive(value: false);
		StartCoroutine(LoadSceneWithTransition("MainScene"));
	}

	public void OnEyeButtonClicked()
	{
	}

	public void OnUndoButtonClicked()
	{
		isDialogOpened = true;
		quitDialog.SetActive(value: true);
	}

	public void OnReferButtonClicked()
	{
		isDialogOpened = true;
		referDialog.SetActive(value: true);
	}
	public void OnReferCloseButtonClicked()
	{
		isDialogOpened = false;
		referDialog.SetActive(value: false);
	}

	public void OnPurchaseCoinButtonClicked()
	{
		isDialogOpened = true;
		purchaseCoinDialog.SetActive(value: true);
	}

	public void OnPurchaseNoThanksButtonClicked()
	{
		isDialogOpened = false;
		purchaseCoinDialog.SetActive(value: false);
	}

	public void OnPurchaseYesButtonClicked()
	{
		isDialogOpened = false;
		purchaseCoinDialog.SetActive(value: false);
	}

	public void OnSettingButtonClicked()
	{
		settingsDialog.SetActive(true);
	}

	public void OnSettingsBackBtnClicked() {
		settingsDialog.SetActive(false);
	}

	public void OnHintButtonClicked()
	{
	}

	public void OnRingStarButtonClicked()
	{	
		Camera.main.transform.position = originCamPos;
		Camera.main.fieldOfView = originView;
		Camera.main.transform.rotation = initialRotation;
	}

	public void OnPauseButtonClicked()
	{
		isDialogOpened = true;
		pauseDialog.SetActive(value: true);
	}

	public void OnResumeButtonClicked()
	{
		isDialogOpened = false;
		pauseDialog.SetActive(value: false);
	}

	public void OnSaveButtonClicked()
	{
	}

	public void OnLoadbuttonClicked()
	{
	}

	public void OnHelpButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("HelpScene"));
	}

    private void FixedUpdate()
    {			
		if(rotationTime <= 1.0f && doCamRot == true) {
			rotationTime += fixedTimeStep;
			Camera.main.transform.RotateAround(rotObj.transform.position, Vector3.right, -totalAng * fixedTimeStep);
			Camera.main.fieldOfView -= fixedTimeStep * totalFieldOfView;
			Debug.Log(Camera.main.transform.rotation);
			Debug.Log(Camera.main.transform.position);
		} else {
			rotationTime = 0f;
			doCamRot = false;
		}
    }

	IEnumerator<object> LoadSceneWithTransition(string sceneName)
    {		
		// transition.SetTrigger("TriggerFadeOut");
        Destroy(GameObject.Find("Directional Light"));
		Destroy(GameObject.Find("EventSystem"));
        LoadSceneParameters param = new(LoadSceneMode.Additive);//, LocalPhysicsMode.Physics3D
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, param);
		yield return new WaitForSeconds(1.0f);
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));		
    }
}