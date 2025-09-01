using System;
using System.Collections.Generic;
using CMF;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class CameraFOV : MonoBehaviour
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000169 RID: 361 RVA: 0x0000E663 File Offset: 0x0000C863
	public float BaseFOV
	{
		get
		{
			return Settings.GetFloat(SystemSetting.FOV, 68f);
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000E671 File Offset: 0x0000C871
	private void Awake()
	{
		this.control = base.GetComponentInParent<PlayerControl>();
		if (this.control.IsMine)
		{
			CameraFOV.instance = this;
		}
		this.camControl = base.GetComponentInParent<CameraController>();
		this.CollectCameras();
		this.lastWantFOV = this.BaseFOV;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
	private void Start()
	{
		this.CollectCameras();
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
	private void CollectCameras()
	{
		this.cameras.Clear();
		Camera component = base.GetComponent<Camera>();
		Camera[] componentsInChildren = base.GetComponentsInChildren<Camera>();
		if (component != null)
		{
			this.cameras.Add(component);
		}
		foreach (Camera item in componentsInChildren)
		{
			this.cameras.Add(item);
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000E714 File Offset: 0x0000C914
	private void LateUpdate()
	{
		int num = 325;
		if (Scene_Settings.instance != null && Scene_Settings.instance.OverrideDrawDistance > 0)
		{
			num = Scene_Settings.instance.OverrideDrawDistance;
		}
		float fieldOfView = this.CurrentFOV();
		foreach (Camera camera in this.cameras)
		{
			camera.fieldOfView = fieldOfView;
			camera.farClipPlane = (float)num;
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
	public void ModifyFOV(CameraFOV.FOVEffect effect)
	{
		if (Settings.GetBool(SystemSetting.FOVEffects, true))
		{
			this.effects.Add(effect);
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
	public void CancelEffect(string UID)
	{
		foreach (CameraFOV.FOVEffect foveffect in this.effects)
		{
			if (!foveffect.IsCompleted && !(foveffect.UID != UID))
			{
				foveffect.IsCanceled = true;
			}
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000E824 File Offset: 0x0000CA24
	public void ClearEffects()
	{
		this.effects.Clear();
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000E834 File Offset: 0x0000CA34
	private float CurrentFOV()
	{
		float num = this.BaseFOV;
		if (this.control.IsSpectator)
		{
			num = this.SpectatorFOV;
		}
		float b = num + this.SpeedFOVMod.Evaluate(this.control.Movement.GetVelocity().magnitude);
		if (PanelManager.CurPanel != PanelType.GameInvisible)
		{
			b = 68f;
		}
		this.lastWantFOV = Mathf.Lerp(this.lastWantFOV, b, Time.deltaTime * 4f);
		float num2 = this.lastWantFOV;
		for (int i = this.effects.Count - 1; i >= 0; i--)
		{
			this.effects[i].Tick();
			num2 += this.effects[i].Evaluate();
			if (this.effects[i].IsCompleted)
			{
				this.effects.RemoveAt(i);
			}
		}
		return Mathf.Clamp(num2, this.Bounds.x, this.Bounds.y);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000E934 File Offset: 0x0000CB34
	public CameraFOV()
	{
	}

	// Token: 0x0400019E RID: 414
	private PlayerControl control;

	// Token: 0x0400019F RID: 415
	private CameraController camControl;

	// Token: 0x040001A0 RID: 416
	public float SpectatorFOV = 68f;

	// Token: 0x040001A1 RID: 417
	public AnimationCurve SpeedFOVMod;

	// Token: 0x040001A2 RID: 418
	public Vector2 Bounds = new Vector2(45f, 150f);

	// Token: 0x040001A3 RID: 419
	private List<Camera> cameras = new List<Camera>();

	// Token: 0x040001A4 RID: 420
	private List<CameraFOV.FOVEffect> effects = new List<CameraFOV.FOVEffect>();

	// Token: 0x040001A5 RID: 421
	public static CameraFOV instance;

	// Token: 0x040001A6 RID: 422
	private float lastWantFOV;

	// Token: 0x020003FC RID: 1020
	[Serializable]
	public class FOVEffect
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x000C0BB3 File Offset: 0x000BEDB3
		public bool IsCompleted
		{
			get
			{
				return (this.IsCanceled && this.Weight <= 0.01f) || (this.Duration > 0f && this.T >= this.Duration);
			}
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000C0BEC File Offset: 0x000BEDEC
		public void Tick()
		{
			if (this.T < this.Duration || this.Duration == 0f)
			{
				this.T += Time.deltaTime;
			}
			if (this.IsCanceled && this.Weight > 0f)
			{
				this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 8f);
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000C0C5C File Offset: 0x000BEE5C
		public float Evaluate()
		{
			return this.OffsetCurve.Evaluate(this.T) * this.Weight;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000C0C76 File Offset: 0x000BEE76
		public CameraFOV.FOVEffect Clone()
		{
			return base.MemberwiseClone() as CameraFOV.FOVEffect;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000C0C84 File Offset: 0x000BEE84
		public FOVEffect()
		{
		}

		// Token: 0x04002122 RID: 8482
		[HideInInspector]
		public string UID;

		// Token: 0x04002123 RID: 8483
		[HideInInspector]
		public bool IsCanceled;

		// Token: 0x04002124 RID: 8484
		private float Weight = 1f;

		// Token: 0x04002125 RID: 8485
		private float T;

		// Token: 0x04002126 RID: 8486
		public float Duration = 0.3f;

		// Token: 0x04002127 RID: 8487
		public AnimationCurve OffsetCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.3f, 0f)
		});
	}
}
