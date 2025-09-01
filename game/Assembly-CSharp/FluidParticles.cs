using System;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class FluidParticles : MonoBehaviour
{
	// Token: 0x06001721 RID: 5921 RVA: 0x00092884 File Offset: 0x00090A84
	private void Awake()
	{
		this.SetActiveObject();
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Combine(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingChanged));
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x000928AC File Offset: 0x00090AAC
	private void OnSettingChanged(SystemSetting setting)
	{
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x000928AE File Offset: 0x00090AAE
	private void SetActiveObject()
	{
		if (!Settings.GetBool(SystemSetting.FluidFX, true))
		{
			this.ParticleFallback.SetActive(true);
			return;
		}
		this.FluidObj.SetActive(true);
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x000928D3 File Offset: 0x00090AD3
	private void OnDestroy()
	{
		Settings.OnSystemSettingsChanged = (Action<SystemSetting>)Delegate.Remove(Settings.OnSystemSettingsChanged, new Action<SystemSetting>(this.OnSettingChanged));
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000928F5 File Offset: 0x00090AF5
	public FluidParticles()
	{
	}

	// Token: 0x040016EB RID: 5867
	public GameObject FluidObj;

	// Token: 0x040016EC RID: 5868
	public GameObject ParticleFallback;
}
