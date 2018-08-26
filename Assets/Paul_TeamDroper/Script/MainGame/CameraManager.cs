using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public enum ZoomState {
		ZoomIn,
		ZoomOut,
	}
	public ZoomState _zoomState = ZoomState.ZoomOut;
	
	private Vector3 originalPosition;
	private float originalCameraView;

	private Vector3 zoomInPosition;
	private float zoomInCameraView;

	private Camera _camera;

	public void SetUp(float p_zoomInYPosition, float p_zoomInCameraView) {
		_camera = GetComponent<Camera>();
		originalCameraView = _camera.fieldOfView;
		originalPosition = transform.position;

		zoomInPosition = new Vector3(transform.position.x, p_zoomInYPosition, transform.position.z );
		zoomInCameraView = p_zoomInCameraView;
	}

	private void Update() {
		if (zoomInPosition == Vector3.zero) return;
		
		Vector3 targetPosition = (_zoomState == ZoomState.ZoomIn) ? zoomInPosition : originalPosition;
		float targetView = (_zoomState == ZoomState.ZoomIn) ? zoomInCameraView : originalCameraView;
		float diffDist =  Vector3.Distance(targetPosition, transform.position );

		if (diffDist < 1) return;
		float lerpSpeed = 5;
		Vector3 newCameraPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
		float newFieldView = Mathf.Lerp(_camera.fieldOfView, targetView,  lerpSpeed * Time.deltaTime);

		transform.position = newCameraPosition;
		_camera.fieldOfView = newFieldView;
	}

	
}
