using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameObject overlay;
	public GameObject MainMenu;
	public Text message;
	public AudioListener mainListener;

	static float xLocationOne;
	static float yLocationOne;
	static float xLocationTwo;
	static float yLocationTwo;

	void Start () {
		Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 0) {
				Time.timeScale = 1;
				overlay.gameObject.SetActive (false);
			}
			else {
				Time.timeScale = 0;
				overlay.gameObject.SetActive (true);
			}
		}
	}

	public void SaveGame(){
		overlay.SetActive (false);
		MainMenu.SetActive (true);
		mainListener.enabled = false;
	}

	public void QuitGame(){
		overlay.SetActive (false);
		MainMenu.SetActive (true);
		mainListener.enabled = false;
		message.text = string.Empty;
		Scene MainScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(MainScene.name);
	}

	public void Volume(Slider slider){
		AudioListener.volume = slider.value;
	}
}