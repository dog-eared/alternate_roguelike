using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject playerObject;
	public Camera c;
	public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;

	private const int cameraZ = -50;

	void Update() {
		Vector3 targetPos = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, cameraZ);
		c.transform.position = Vector3.SmoothDamp(c.transform.position, targetPos, ref velocity, smoothTime);
  }

}
