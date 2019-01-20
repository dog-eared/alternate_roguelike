//﻿using System.Collections;
using System.Collections.Generic;

public static class GameSettings {
	/**
	 * GAME SETTINGS (static, non-MonoBehaviour)
	 * Purpose: Holds costs of combat actions and animation times
	 * TODO This will probably hold other stuff later.
	 */

	public static Dictionary<string, int> actionCosts = new Dictionary<string, int>() {
		{"move", 50},
		{"pause", 0},
		{"melee", 50},
		{"shoot", 55},
		{"quickCast", 55},
		{"longCast", 100}
	};

	public static Dictionary<string, float> animSpeeds = new Dictionary<string, float>() {
		{"walk", 0.2f},
		{"melee", 0.7f},
		{"shoot", 0.32f},
		{"useItem", 0.45f}
	};

}
