using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_OptionsMenuBehaviour : MonoBehaviour {
	public Toggle hardMode;
	public Toggle violenceMode;
	public Slider sfxVolume;
	public Slider musicVolume;

	private void OnEnable() {
		hardMode.isOn = (GUIManager.options["hardMode"] > 0);
		violenceMode.isOn = (GUIManager.options["violenceMode"] > 0);
		sfxVolume.value = (GUIManager.options["sfxVolume"]);
		musicVolume.value = (GUIManager.options["musicVolume"]);
	}

	public void SaveChanges_Click() {
		//Bool toggles
		int hardModeOn = (hardMode.isOn == true) ? 1 : 0;
		PlayerPrefs.SetInt("hardMode", hardModeOn);

		int violenceModeOn = (violenceMode.isOn == true) ? 1 : 0;
		PlayerPrefs.SetInt("violenceMode", violenceModeOn);

		//Numeric values
		PlayerPrefs.SetInt("sfxVolume", (int)sfxVolume.value);
		PlayerPrefs.SetInt("musicVolume", (int)musicVolume.value);

		GUIManager.SetOptions();
		GUIManager.Instance.CloseWindow();
	}

	public void Cancel_Click() {
		GUIManager.Instance.CloseWindow();
	}


}
