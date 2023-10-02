using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class GamePrepareSceneScript : MonoBehaviour
{
	private int chessType;
	public int level;

	// Chess Image in the chess type button
	private Transform whiteChessTransform;
	private Transform blackChessTransform;
	private Transform randomChessTransform;
	
	// Background of the chess type button
	private Transform whiteChessBkg;
	private Transform blackChessBkg;
	private Transform randomChessBkg;

	// Level slider
	public GameObject levelSliderObject;
	private Slider levelSlider;
	private Image fillImage;
	
	public GameObject referDlg;
	public GameObject levelDlg;

	public TMP_Text gameLevel;
	public TMP_Text flowerLevel;
	public TMP_Text gamesWonText;

	public Animator transition;

	private void Start()
	{
		whiteChessTransform = GameObject.Find("WhiteChess").transform.GetChild(0).GetComponent<RectTransform>();
		blackChessTransform = GameObject.Find("BlackChess").transform.GetChild(0).GetComponent<RectTransform>();
		randomChessTransform = GameObject.Find("RandomChess").transform.GetChild(0).GetComponent<RectTransform>();

		whiteChessBkg = GameObject.Find("WhiteChess").GetComponent<RectTransform>();
		blackChessBkg = GameObject.Find("BlackChess").GetComponent<RectTransform>();
		randomChessBkg = GameObject.Find("RandomChess").GetComponent<RectTransform>();
		
		levelSlider = levelSliderObject.GetComponent<Slider>();

		OnWhiteChessButtonClicked();
		OnLevelSliderChanged();
		level = 1;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnBackButtonClicked();
		}
	}

	public void OnBackButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("MainScene"));
	}

	public void OnReferButtonClicked()
	{
		referDlg.SetActive(value: true);
		levelDlg.SetActive(value: false);
	}

	public void OnReferButtonCloseClicked()
	{
		referDlg.SetActive(value: false);
		levelDlg.SetActive(value: true);
	}

	public void OnProfileButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("ProfileScene"));
	}

	public void OnSettingButtonClicked()
	{
		StartCoroutine(LoadSceneWithTransition("SettingScene"));
	}

	public void OnPlayButtonClicked()
	{
		// StartCoroutine(LoadSceneWithTransition("GameScene"));
	}

	public void OnWhiteChessButtonClicked()
	{
		chessType = 0;
		whiteChessTransform.localScale = new Vector3(1.3f, 1.6f, 1f);
		blackChessTransform.localScale = new Vector3(1f, 1f, 1f);
		randomChessTransform.localScale = new Vector3(1f, 1f, 1f);

		whiteChessBkg.localScale = new Vector3(1f, 1f, 1f);
		blackChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
		randomChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
	}

	public void OnBlackChessButtonClicked()
	{
		chessType = 1;
		whiteChessTransform.localScale = new Vector3(1f, 1f, 1f);
		blackChessTransform.localScale = new Vector3(1.3f, 1.6f, 1f);
		randomChessTransform.localScale = new Vector3(1f, 1f, 1f);

		whiteChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
		blackChessBkg.localScale = new Vector3(1f, 1f, 1f);
		randomChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
	}

	public void OnRandomChessButtonClicked()
	{
		chessType = 2;
		whiteChessTransform.localScale = new Vector3(1f, 1f, 1f);
		blackChessTransform.localScale = new Vector3(1f, 1f, 1f);
		randomChessTransform.localScale = new Vector3(1.3f, 1.6f, 1f);

		whiteChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
		blackChessBkg.localScale = new Vector3(0.9f, 0.9f, 1f);
		randomChessBkg.localScale = new Vector3(1f, 1f, 1f);
	}

	public void OnLevelSliderChanged()
	{
		fillImage = GameObject.Find("Fill").GetComponent<Image>();
		
		if (levelSlider.value < 2f) { //Set level from 1, not 0
			levelSlider.value = 2f; 
		} else if (levelSlider.value > 7f) { // set level to 6, not above 6
			levelSlider.value = 7f;
		} else if (levelSlider.value == 7) { // set image full when reach level 6
			fillImage.fillAmount = 1.0f;
		}
		string text = (levelSlider.value - 1f).ToString();
		gameLevel.text = text + "- BEGINNER";
		flowerLevel.text = text;
		gamesWonText.text = text + "/6 Games won";
		level = (int)Math.Ceiling((levelSlider.value - 1f) / 2.0f);
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
