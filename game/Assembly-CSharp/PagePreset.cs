using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[CreateAssetMenu(menuName = "Tome Config/Page Preset", order = -4)]
public class PagePreset : ScriptableObject
{
	// Token: 0x060002A0 RID: 672 RVA: 0x00016A68 File Offset: 0x00014C68
	public PagePreset()
	{
	}

	// Token: 0x04000293 RID: 659
	[Range(0f, 100f)]
	public float Abundance = 100f;

	// Token: 0x04000294 RID: 660
	[HideInInspector]
	public string guid;

	// Token: 0x04000295 RID: 661
	public AugmentFilter Player = new AugmentFilter
	{
		CanUsePlayer = true
	};

	// Token: 0x04000296 RID: 662
	public AugmentFilter Fountain = new AugmentFilter
	{
		CanUseFountain = true
	};
}
