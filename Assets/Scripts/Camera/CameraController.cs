using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float verticalOffset = 0f;
	public float lookAheadDistX = 1f;
	public float lookSmoothTimeX = 0.5f;
	public float verticalSmoothTime = 0f;
	public Vector2 focusAreaSize;

	ActorController target;

	FocusArea focusArea;

	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothVelocityY;

	bool lookAheadStopped;

	void Start() {
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<ActorController> ();
		focusArea = new FocusArea (target.col.bounds, focusAreaSize);
	}

	void LateUpdate() {
		focusArea.Update (target.col.bounds);

		Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

		if (focusArea.velocity.x != 0) {
			lookAheadDirX = Mathf.Sign (focusArea.velocity.x);
			if (Mathf.Sign (target.playerInput.x) == Mathf.Sign (focusArea.velocity.x) && target.playerInput.x != 0) {
				lookAheadStopped = false;
				targetLookAheadX = lookAheadDirX * lookAheadDistX;
			} else {
				if (!lookAheadStopped) {
					lookAheadStopped = true;
					targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDistX - currentLookAheadX) / 4f;
				}
			}
		}
			
		currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

		focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
		focusPosition += Vector2.right * currentLookAheadX;
		transform.position = (Vector3)focusPosition + Vector3.forward * -10f;
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}

	struct FocusArea {
		public Vector2 velocity;
		public Vector2 center;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds targetBounds, Vector2 size) {
			left = targetBounds.center.x - size.x/2f;
			right = targetBounds.center.x + size.x/2f;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;

			velocity = Vector2.zero;
			center = new Vector2((left+right)/2, (top+bottom)/2);
		}

		public void Update(Bounds target) {
			float shiftX = 0;
			if (target.min.x < left) {
				shiftX = target.min.x - left;
			} else if (target.max.x > right){
				shiftX = target.max.x - right;
			}

			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (target.min.y < bottom) {
				shiftY = target.min.y - bottom;
			} else if (target.max.y > top){
				shiftY = target.max.y - top;
			}

			top += shiftY;
			bottom += shiftY;

			velocity = new Vector2 (shiftX, shiftY);
			center = new Vector2((left+right)/2, (top+bottom)/2);
		}

	}
}
