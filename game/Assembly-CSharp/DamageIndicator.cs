using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class DamageIndicator : MonoBehaviour
{
	// Token: 0x06001178 RID: 4472 RVA: 0x0006C1D8 File Offset: 0x0006A3D8
	private void Awake()
	{
		DamageIndicator.instance = this;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0006C1E0 File Offset: 0x0006A3E0
	public void DebugDamageIndicator()
	{
		if (this.testDamgeSourcePoint == null || PlayerMovement.myCamera == null)
		{
			return;
		}
		this.Damage(this.testDamgeSourcePoint.position, PlayerMovement.myCamera.transform.position, PlayerMovement.myCamera.transform.forward);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0006C238 File Offset: 0x0006A438
	public static void DamageTaken(DamageInfo dmg)
	{
		if (PlayerMovement.myCamera == null)
		{
			return;
		}
		PhotonView photonView = PhotonView.Find(dmg.SourceID);
		if (photonView != null)
		{
			PlayerControl component = photonView.GetComponent<PlayerControl>();
			if (component != null)
			{
				dmg.AtPoint = component.movement.GetPosition();
			}
			else
			{
				dmg.AtPoint = photonView.transform.position;
			}
		}
		DamageIndicator.instance.Damage(dmg.AtPoint, PlayerMovement.myCamera.transform.position, PlayerMovement.myCamera.transform.forward);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0006C2CC File Offset: 0x0006A4CC
	private void Damage(Vector3 damageSource, Vector3 cameraPoint, Vector3 cameraForward)
	{
		damageSource.y = 0f;
		cameraPoint.y = 0f;
		Vector3 normalized = (damageSource - cameraPoint).normalized;
		cameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;
		float angle = Vector3.SignedAngle(cameraForward, normalized, Vector3.up);
		DamageIndicator.instance.debugAngle = angle;
		this.ApplyVisual(angle, 1f);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0006C33C File Offset: 0x0006A53C
	private void ApplyVisual(float angle, float intensity = 1f)
	{
		intensity *= Settings.GetFloat(SystemSetting.DangerIntensity, 1f);
		if (Mathf.Abs(angle) < 30f)
		{
			this.top.alpha = intensity;
			return;
		}
		if (Mathf.Abs(angle) > 150f)
		{
			this.bottom.alpha = intensity;
			return;
		}
		if (angle < 0f)
		{
			this.left.alpha = intensity;
			return;
		}
		this.right.alpha = intensity;
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0006C3B0 File Offset: 0x0006A5B0
	private void Update()
	{
		if (this.top.alpha > 0f)
		{
			this.top.alpha -= Time.deltaTime * this.fadeRate;
		}
		if (this.bottom.alpha > 0f)
		{
			this.bottom.alpha -= Time.deltaTime * this.fadeRate;
		}
		if (this.left.alpha > 0f)
		{
			this.left.alpha -= Time.deltaTime * this.fadeRate;
		}
		if (this.right.alpha > 0f)
		{
			this.right.alpha -= Time.deltaTime * this.fadeRate;
		}
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x0006C47D File Offset: 0x0006A67D
	public DamageIndicator()
	{
	}

	// Token: 0x04001011 RID: 4113
	public CanvasGroup top;

	// Token: 0x04001012 RID: 4114
	public CanvasGroup bottom;

	// Token: 0x04001013 RID: 4115
	public CanvasGroup left;

	// Token: 0x04001014 RID: 4116
	public CanvasGroup right;

	// Token: 0x04001015 RID: 4117
	public float fadeRate = 6f;

	// Token: 0x04001016 RID: 4118
	public static DamageIndicator instance;

	// Token: 0x04001017 RID: 4119
	[Header("Debug")]
	public Transform testDamgeSourcePoint;

	// Token: 0x04001018 RID: 4120
	public float debugAngle;
}
