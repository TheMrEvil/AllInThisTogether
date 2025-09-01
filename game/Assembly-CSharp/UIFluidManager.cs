using System;
using System.Collections.Generic;
using Fluxy;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class UIFluidManager : MonoBehaviour
{
	// Token: 0x06001126 RID: 4390 RVA: 0x0006A4E8 File Offset: 0x000686E8
	private void Awake()
	{
		UIFluidManager.instance = this;
		foreach (GameObject gameObject in this.ContainerObjects)
		{
			gameObject.SetActive(true);
		}
		this.UpdateFromSetting();
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingChanged));
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0006A568 File Offset: 0x00068768
	private void OnSettingChanged(SystemSetting setting)
	{
		if (setting == SystemSetting.FluidFX)
		{
			this.UpdateFromSetting();
		}
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0006A578 File Offset: 0x00068778
	private void UpdateFromSetting()
	{
		if (!Settings.GetBool(SystemSetting.FluidFX, true))
		{
			foreach (FluxyCanvasContainer fluxyCanvasContainer in UIFluidManager.Containers)
			{
				fluxyCanvasContainer.DisableContainer();
			}
			this.Solver.enabled = false;
			return;
		}
		this.Solver.enabled = true;
		foreach (FluxyCanvasContainer fluxyCanvasContainer2 in UIFluidManager.Containers)
		{
			fluxyCanvasContainer2.EnableContainer();
		}
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0006A628 File Offset: 0x00068828
	private void Update()
	{
		if (this.startDelay > 0f)
		{
			this.startDelay -= Time.deltaTime;
		}
		else
		{
			this.Solver.IsActive = (PanelManager.CurPanel != PanelType.GameInvisible);
		}
		foreach (FluxyButton fluxyButton in UIFluidManager.Buttons)
		{
			fluxyButton.DoUpdate();
		}
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0006A6B0 File Offset: 0x000688B0
	public void Refresh()
	{
		UIFluidManager.ResetContainers();
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0006A6B8 File Offset: 0x000688B8
	public static void ResetContainers()
	{
		foreach (FluxyCanvasContainer fluxyCanvasContainer in UIFluidManager.Containers)
		{
			if (fluxyCanvasContainer.isActiveAndEnabled)
			{
				fluxyCanvasContainer.ResetContainer();
			}
		}
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0006A714 File Offset: 0x00068914
	public static FluxyCanvasContainer GetContainer(UIFluidType ftype)
	{
		foreach (FluxyCanvasContainer fluxyCanvasContainer in UIFluidManager.Containers)
		{
			if (fluxyCanvasContainer.FluidType == ftype)
			{
				return fluxyCanvasContainer;
			}
		}
		return null;
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0006A770 File Offset: 0x00068970
	public static void AddContainer(FluxyCanvasContainer container)
	{
		UIFluidManager.Containers.Add(container);
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0006A77D File Offset: 0x0006897D
	private void OnDestroy()
	{
		UIFluidManager.Containers.Clear();
		UIFluidManager.Buttons.Clear();
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x0006A793 File Offset: 0x00068993
	public UIFluidManager()
	{
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x0006A7A6 File Offset: 0x000689A6
	// Note: this type is marked as 'beforefieldinit'.
	static UIFluidManager()
	{
	}

	// Token: 0x04000F80 RID: 3968
	private static List<FluxyCanvasContainer> Containers = new List<FluxyCanvasContainer>();

	// Token: 0x04000F81 RID: 3969
	public static List<FluxyButton> Buttons = new List<FluxyButton>();

	// Token: 0x04000F82 RID: 3970
	public FluxySolver Solver;

	// Token: 0x04000F83 RID: 3971
	public List<GameObject> ContainerObjects;

	// Token: 0x04000F84 RID: 3972
	public static UIFluidManager instance;

	// Token: 0x04000F85 RID: 3973
	public Texture2D SplatTex;

	// Token: 0x04000F86 RID: 3974
	public Texture2D VelTex;

	// Token: 0x04000F87 RID: 3975
	public Texture2D NoiseTex;

	// Token: 0x04000F88 RID: 3976
	public Material SplatMat;

	// Token: 0x04000F89 RID: 3977
	private float startDelay = 1f;
}
