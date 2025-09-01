using System;
using EZCameraShake;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class PlayRandom : MonoBehaviour
{
	// Token: 0x06000163 RID: 355 RVA: 0x0000E420 File Offset: 0x0000C620
	private void Awake()
	{
		if (PlayerControl.myInstance != null)
		{
			this.distance = Vector3.Distance(PlayerControl.myInstance.Movement.cameraRoot.transform.position, base.transform.position);
		}
		this.PlayRandomClip();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000E470 File Offset: 0x0000C670
	private void PlayRandomClip()
	{
		AudioManager.PlayClipAtPoint(this.clips[UnityEngine.Random.Range(0, this.clips.Length)], base.transform.position, 1f, UnityEngine.Random.Range(this.pitchRange.x, this.pitchRange.y), this.ThreeD.x, this.ThreeD.y, this.ThreeD.z);
		if (this.cameraShake.magnitude > 0f)
		{
			CameraShaker.Instance.ShakeOnce(this.cameraShake.x * Mathf.Clamp(1f / (this.distance * this.distance * 0.1f), 0f, 1f), this.cameraShake.y, 0.1f, 0.1f);
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000E54C File Offset: 0x0000C74C
	public PlayRandom()
	{
	}

	// Token: 0x0400018D RID: 397
	public AudioClip[] clips;

	// Token: 0x0400018E RID: 398
	public Vector2 pitchRange = Vector2.one;

	// Token: 0x0400018F RID: 399
	[Tooltip("X - 3D Percent, Y - Min Dist, Z - Max Dist")]
	public Vector3 ThreeD = new Vector3(1f, 1f, 10f);

	// Token: 0x04000190 RID: 400
	public bool useDistanceDelay;

	// Token: 0x04000191 RID: 401
	[Tooltip("X - Intensity, Y - Duration")]
	public Vector2 cameraShake = Vector2.zero;

	// Token: 0x04000192 RID: 402
	private float distance = 5000f;
}
