using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public bool showAtStart = true;
	public GameObject overlay;
	public AudioListener mainListener;

	public void Start() {
		overlay.SetActive (true);
		mainListener.enabled = false;
		Time.timeScale = 0;
	}

	public void StartGame() {		
		overlay.SetActive (false);
		mainListener.enabled = true;
		Time.timeScale = 1;
	}

	public void LoadGame() {
		overlay.SetActive (false);
		mainListener.enabled = true;
		Time.timeScale = 1;
	}
}