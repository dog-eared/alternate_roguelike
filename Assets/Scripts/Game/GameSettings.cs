using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings {

	public static Dictionary<string, int> actionCosts = new Dictionary<string, int>() {
		{"move", 50},
		{"pause", 0},
		{"attack", 50},
		{"shoot", 55},
		{"quickCast", 55},
		{"longCast", 100}
	};

	public static Dictionary<string, float> animSpeeds = new Dictionary<string, float>() {
		{"walk", 0.2f},
		{"melee", 0.3f},
		{"shoot", 0.32f},
		{"useItem", 0.45f}
	};

}
