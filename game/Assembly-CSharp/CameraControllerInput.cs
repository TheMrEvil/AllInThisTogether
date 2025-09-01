using System;
using CMF;
using InControl;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class CameraControllerInput : CameraInput
{
	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600074B RID: 1867 RVA: 0x00034AE9 File Offset: 0x00032CE9
	private InputActions actions
	{
		get
		{
			return global::InputManager.Actions;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x00034AF0 File Offset: 0x00032CF0
	private bool invertHorizontalInput
	{
		get
		{
			return Settings.GetInt(SystemSetting.CameraInvert, 0) >= 2;
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x0600074D RID: 1869 RVA: 0x00034B00 File Offset: 0x00032D00
	private bool invertVerticalInput
	{
		get
		{
			int @int = Settings.GetInt(SystemSetting.CameraInvert, 0);
			return @int == 1 || @int == 3;
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00034B20 File Offset: 0x00032D20
	private void Awake()
	{
		this.control = base.GetComponentInParent<PlayerControl>();
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00034B30 File Offset: 0x00032D30
	public override float GetHorizontalCameraInput()
	{
		if (!PanelManager.ShouldLockCursor() || this.control.IsSpectator || this.control.Input.CameraOverriden)
		{
			return 0f;
		}
		float num = this.actions.Camera.X;
		if (this.actions.Camera.LastInputType == BindingSourceType.MouseBindingSource)
		{
			num *= this.MouseMultiplier / Time.deltaTime;
		}
		num = (this.invertHorizontalInput ? (-1f * num) : num);
		float @float = Settings.GetFloat(SystemSetting.CameraSensitivity, 0.75f);
		num *= @float * @float * 0.5f;
		float float2 = Settings.GetFloat(SystemSetting.CameraAccel, 0.25f);
		if (float2 > 0f)
		{
			this.xAccel = Mathf.MoveTowards(this.xAccel, (float)((Mathf.Abs(num) > 0f) ? 1 : 0), Time.deltaTime * this.Accel / float2);
		}
		else
		{
			this.xAccel = 1f;
		}
		float float3 = Settings.GetFloat(SystemSetting.CameraSmoothing, 0.25f);
		if (float3 > 0f)
		{
			if (Mathf.Abs(this.xAccumulation) < Mathf.Abs(num))
			{
				this.xAccumulation = Mathf.Lerp(this.xAccumulation, num, this.Snappiness / float3 * Time.deltaTime);
			}
			else
			{
				this.xAccumulation = Mathf.Lerp(this.xAccumulation, num, this.Snappiness / float3 * 3f * Time.deltaTime);
			}
		}
		else
		{
			this.xAccumulation = num;
		}
		return this.xAccumulation * this.xAccel;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00034CA0 File Offset: 0x00032EA0
	public override float GetVerticalCameraInput()
	{
		if (!PanelManager.ShouldLockCursor() || this.control.IsSpectator || this.control.Input.CameraOverriden)
		{
			return 0f;
		}
		float num = -this.actions.Camera.Y;
		if (this.actions.Camera.LastInputType == BindingSourceType.MouseBindingSource)
		{
			num *= this.MouseMultiplier / Time.deltaTime;
		}
		num = (this.invertVerticalInput ? (-1f * num) : num);
		float @float = Settings.GetFloat(SystemSetting.CameraSensitivity_Vert, 0.6f);
		num *= @float * @float * 0.5f;
		float float2 = Settings.GetFloat(SystemSetting.CameraAccel, 0f);
		if (float2 > 0f)
		{
			this.yAccel = Mathf.MoveTowards(this.yAccel, (float)((Mathf.Abs(num) > 0f) ? 1 : 0), Time.deltaTime * this.Accel / float2);
		}
		else
		{
			this.yAccel = 1f;
		}
		float float3 = Settings.GetFloat(SystemSetting.CameraSmoothing, 0.25f);
		if (float3 > 0f)
		{
			if (Mathf.Abs(this.yAccumulation) < Mathf.Abs(num))
			{
				this.yAccumulation = Mathf.Lerp(this.yAccumulation, num, this.Snappiness / float3 * Time.deltaTime);
			}
			else
			{
				this.yAccumulation = Mathf.Lerp(this.yAccumulation, num, this.Snappiness / float3 * 3f * Time.deltaTime);
			}
		}
		else
		{
			this.yAccumulation = num;
		}
		return this.yAccumulation * this.yAccel;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00034E12 File Offset: 0x00033012
	public CameraControllerInput()
	{
	}

	// Token: 0x040005E2 RID: 1506
	private PlayerControl control;

	// Token: 0x040005E3 RID: 1507
	public float MouseMultiplier = 5f;

	// Token: 0x040005E4 RID: 1508
	public float Snappiness = 10f;

	// Token: 0x040005E5 RID: 1509
	public float Accel = 32f;

	// Token: 0x040005E6 RID: 1510
	private float xAccel;

	// Token: 0x040005E7 RID: 1511
	private float yAccel;

	// Token: 0x040005E8 RID: 1512
	private float xAccumulation;

	// Token: 0x040005E9 RID: 1513
	private float yAccumulation;

	// Token: 0x040005EA RID: 1514
	public bool AimHasTarget;
}
