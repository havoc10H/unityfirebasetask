using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainSceneScript : MonoBehaviour
{
	public GameObject ReferDialog;
	
	public Animator transition;

	public void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
		}
	}

	public void OnGameTypeComputerClicked()
	{
		SceneManager.LoadScene("AIPlayPrepareScene");
		// StartCoroutine(LoadSceneWithTransition("AIGameScene"));
	}

	public void OnGameTypeHumanClicked()
	{
		StartCoroutine(LoadSceneWithTransition("TwoPlayerPrepareScene"));
	}

	public void OnGameTypeOnlineClicked()
	{
		StartCoroutine(LoadSceneWithTransition("OnlineGamePrepareScene"));
	}

	public void OnGameTypeFutureClicked()
	{
	}

	public void OnInviteButtonClicked()
	{
		ReferDialog.SetActive(value: true);
	}

	public void OnInviteButtonCloseClicked()
	{
		ReferDialog.SetActive(value: false);
	}

	public void OnLikeButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("FeedbackScene"));
	}
	public void OnSettingButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("SettingScene"));
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