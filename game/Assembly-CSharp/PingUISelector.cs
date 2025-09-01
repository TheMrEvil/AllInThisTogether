using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class PingUISelector : MonoBehaviour
{
	// Token: 0x06001266 RID: 4710 RVA: 0x000719D2 File Offset: 0x0006FBD2
	private void Awake()
	{
		PingUISelector.instance = this;
		PingUISelector.IsActive = false;
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000719E0 File Offset: 0x0006FBE0
	public static void Activate()
	{
		if (PingUISelector.IsActive)
		{
			return;
		}
		PingUISelector.IsActive = true;
		foreach (PingUISelector.PingSelector pingSelector in PingUISelector.instance.PingOptions)
		{
			pingSelector.Reset();
		}
		Vector2 cameraAxis = PlayerInput.myInstance.cameraAxis;
		PingUISelector.instance.wantAngle = 0f;
		if (cameraAxis.magnitude > 0.1f)
		{
			PingUISelector.instance.wantAngle = Mathf.Atan2(cameraAxis.y, cameraAxis.x) * 57.29578f - 90f;
		}
		PingUISelector.instance.Pointer_Rotator.localRotation = Quaternion.Euler(0f, 0f, PingUISelector.instance.wantAngle);
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00071ABC File Offset: 0x0006FCBC
	public static void Deactivate()
	{
		if (!PingUISelector.IsActive)
		{
			return;
		}
		PingUISelector.IsActive = false;
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x00071ACC File Offset: 0x0006FCCC
	private void Update()
	{
		if (!PingUISelector.IsActive)
		{
			if (this.CoreGroup.alpha > 0f)
			{
				this.CoreGroup.UpdateOpacity(false, 4f, true);
			}
			return;
		}
		this.CoreGroup.UpdateOpacity(true, 4f, true);
		Vector2 cameraAxis = PlayerInput.myInstance.cameraAxis;
		if (cameraAxis.magnitude > 0.1f)
		{
			this.wantAngle = Mathf.Atan2(cameraAxis.y, cameraAxis.x) * 57.29578f - 90f;
		}
		Quaternion b = Quaternion.Euler(0f, 0f, this.wantAngle);
		this.Pointer_Rotator.localRotation = Quaternion.Lerp(this.Pointer_Rotator.localRotation, b, Time.deltaTime * 16f);
		PingUISelector.PingSelector pingSelector = this.Closest();
		foreach (PingUISelector.PingSelector pingSelector2 in this.PingOptions)
		{
			if (pingSelector2 == pingSelector)
			{
				pingSelector2.SelectedUpdate();
			}
			else
			{
				pingSelector2.UnselectedUpdate();
			}
		}
		if (pingSelector != null)
		{
			this.LabelText.text = pingSelector.Label;
		}
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x00071C00 File Offset: 0x0006FE00
	private PingUISelector.PingSelector Closest()
	{
		Vector3 position = this.Pointer_PointLocation.position;
		float num = float.MaxValue;
		PingUISelector.PingSelector result = null;
		foreach (PingUISelector.PingSelector pingSelector in this.PingOptions)
		{
			float num2 = Vector3.Distance(position, pingSelector.Root.position);
			if (num2 < num)
			{
				num = num2;
				result = pingSelector;
			}
		}
		return result;
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x00071C84 File Offset: 0x0006FE84
	public static PlayerDB.PingType GetSelectedType()
	{
		PingUISelector pingUISelector = PingUISelector.instance;
		PlayerDB.PingType? pingType;
		if (pingUISelector == null)
		{
			pingType = null;
		}
		else
		{
			PingUISelector.PingSelector pingSelector = pingUISelector.Closest();
			pingType = ((pingSelector != null) ? new PlayerDB.PingType?(pingSelector.Ping) : null);
		}
		PlayerDB.PingType? pingType2 = pingType;
		return pingType2.GetValueOrDefault();
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x00071CCB File Offset: 0x0006FECB
	public PingUISelector()
	{
	}

	// Token: 0x04001153 RID: 4435
	public static PingUISelector instance;

	// Token: 0x04001154 RID: 4436
	public static bool IsActive;

	// Token: 0x04001155 RID: 4437
	public CanvasGroup CoreGroup;

	// Token: 0x04001156 RID: 4438
	public Transform Pointer_PointLocation;

	// Token: 0x04001157 RID: 4439
	public Transform Pointer_Rotator;

	// Token: 0x04001158 RID: 4440
	public TextMeshProUGUI LabelText;

	// Token: 0x04001159 RID: 4441
	public List<PingUISelector.PingSelector> PingOptions;

	// Token: 0x0400115A RID: 4442
	private float wantAngle;

	// Token: 0x02000583 RID: 1411
	[Serializable]
	public class PingSelector
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x000D0D05 File Offset: 0x000CEF05
		public void Reset()
		{
			this.GlowBack.alpha = 0f;
			this.RotatingRing.alpha = 0f;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000D0D27 File Offset: 0x000CEF27
		public void SelectedUpdate()
		{
			this.GlowBack.UpdateOpacity(true, 4f, true);
			this.RotatingRing.UpdateOpacity(true, 4f, true);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000D0D4D File Offset: 0x000CEF4D
		public void UnselectedUpdate()
		{
			this.GlowBack.UpdateOpacity(false, 4f, true);
			this.RotatingRing.UpdateOpacity(false, 4f, true);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000D0D73 File Offset: 0x000CEF73
		public PingSelector()
		{
		}

		// Token: 0x0400277C RID: 10108
		public PlayerDB.PingType Ping;

		// Token: 0x0400277D RID: 10109
		public Transform Root;

		// Token: 0x0400277E RID: 10110
		public CanvasGroup GlowBack;

		// Token: 0x0400277F RID: 10111
		public CanvasGroup RotatingRing;

		// Token: 0x04002780 RID: 10112
		public string Label;
	}
}
