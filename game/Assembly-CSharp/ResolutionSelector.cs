using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class ResolutionSelector : MonoBehaviour
{
	// Token: 0x06001640 RID: 5696 RVA: 0x0008C7F9 File Offset: 0x0008A9F9
	private void Awake()
	{
		ResolutionSelector.instance = this;
		this.SetupResolutions();
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x0008C807 File Offset: 0x0008AA07
	public static void SetupFromSave(int x, int y)
	{
		ResolutionSelector.wantResolution = new Vector2((float)x, (float)y);
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0008C818 File Offset: 0x0008AA18
	private void SetupResolutions()
	{
		HashSet<ValueTuple<int, int>> hashSet = new HashSet<ValueTuple<int, int>>();
		foreach (Resolution resolution in Screen.resolutions)
		{
			if (resolution.width >= 1280 && (float)resolution.width / (float)resolution.height >= 1.5f)
			{
				hashSet.Add(new ValueTuple<int, int>(resolution.width, resolution.height));
			}
		}
		this.options = new List<Vector2>();
		foreach (ValueTuple<int, int> valueTuple in hashSet)
		{
			this.options.Add(new Vector2((float)valueTuple.Item1, (float)valueTuple.Item2));
		}
		this.options.Reverse();
		if (ResolutionSelector.wantResolution != Vector2.zero)
		{
			if (!this.options.Contains(ResolutionSelector.wantResolution))
			{
				this.options.Add(ResolutionSelector.wantResolution);
				Debug.Log(string.Format("Had Custom Resolution ({0}x{1})), adding to list", (int)ResolutionSelector.wantResolution.x, (int)ResolutionSelector.wantResolution.y));
			}
			int resolutionID = this.options.IndexOf(ResolutionSelector.wantResolution);
			ResolutionSelector.instance.SetResolutionID(resolutionID);
			this.skipLoadValue = true;
		}
		Action onScreenSettingsChanged = this.OnScreenSettingsChanged;
		if (onScreenSettingsChanged == null)
		{
			return;
		}
		onScreenSettingsChanged();
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x0008C990 File Offset: 0x0008AB90
	public void SetFullscreenMode(int id)
	{
		this.fullScreenMode = (FullScreenMode)id;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x0008C999 File Offset: 0x0008AB99
	public void SetResolutionID(int id)
	{
		if (this.skipLoadValue)
		{
			this.skipLoadValue = false;
			return;
		}
		this.wantID = id;
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
	public void ApplySetting()
	{
		if ((float)Screen.width == this.options[this.wantID].x && (float)Screen.height == this.options[this.wantID].y && Screen.fullScreenMode == this.fullScreenMode)
		{
			return;
		}
		this.currentID = this.wantID;
		ResolutionSelector.wantResolution = this.options[this.currentID];
		Screen.SetResolution((int)ResolutionSelector.wantResolution.x, (int)ResolutionSelector.wantResolution.y, this.fullScreenMode);
		Settings.SaveResolutionExplicit(ResolutionSelector.wantResolution);
		this.SetupResolutions();
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x0008CA5E File Offset: 0x0008AC5E
	public ResolutionSelector()
	{
	}

	// Token: 0x040015DB RID: 5595
	public static ResolutionSelector instance;

	// Token: 0x040015DC RID: 5596
	public List<Vector2> options;

	// Token: 0x040015DD RID: 5597
	private int currentID = -1;

	// Token: 0x040015DE RID: 5598
	private int wantID;

	// Token: 0x040015DF RID: 5599
	private static Vector2 wantResolution;

	// Token: 0x040015E0 RID: 5600
	private bool skipLoadValue;

	// Token: 0x040015E1 RID: 5601
	private FullScreenMode fullScreenMode;

	// Token: 0x040015E2 RID: 5602
	public Action OnScreenSettingsChanged;
}
