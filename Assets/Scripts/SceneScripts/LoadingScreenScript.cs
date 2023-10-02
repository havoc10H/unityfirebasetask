using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoadingScreenScript : MonoBehaviour
{
	public GameObject loadingIcon;

	public Animator transition;
	public GameObject[] txts = new GameObject[4];

	private Sprite[] loadingBarSpriteArray;

	private float timer = 0f;
	private float txtTimer = 0f;

	private int currentSpriteIndex = 0;
	private int currentTxtIndex = 0;

	private int maxSpriteIndex = 0;

	private void Start()
	{
		loadingBarSpriteArray = Resources.LoadAll<Sprite>("LoadingIcons");
		currentSpriteIndex = 0;
		currentTxtIndex = 0;
		txts[currentTxtIndex].SetActive(true);
		maxSpriteIndex = loadingBarSpriteArray.Length;
	}

	private void Update()
	{
		if (currentSpriteIndex == maxSpriteIndex)
		{
			return;
		}
		
		timer += Time.deltaTime;

		if (timer >= 0.4f)
		{			
			currentSpriteIndex++;			
			if (currentSpriteIndex == maxSpriteIndex)
			{
				StartCoroutine(LoadSceneWithTransition("LoginScene"));
			}
			else
			{
				loadingIcon.GetComponent<Image>().sprite = loadingBarSpriteArray[currentSpriteIndex];
				timer = 0f;
			}
		}

		txtTimer += Time.deltaTime;
		
		if (txtTimer >= 1.0f) {
			currentTxtIndex++;
			if (currentTxtIndex < 4) {
				txts[currentTxtIndex - 1].SetActive(false);
				txts[currentTxtIndex].SetActive(true);
				txtTimer = 0f;
			}
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
