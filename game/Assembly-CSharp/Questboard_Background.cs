using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class Questboard_Background : MonoBehaviour
{
	// Token: 0x06000F8B RID: 3979 RVA: 0x000625A5 File Offset: 0x000607A5
	private void Awake()
	{
		this.Fader.HideImmediate();
		PanelManager instance = PanelManager.instance;
		instance.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(instance.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x000625D8 File Offset: 0x000607D8
	private void Start()
	{
		this.Panels.Add(PanelType.Questboard);
		this.Panels.Add(PanelType.SignatureChallenges);
		this.Panels.Add(PanelType.DailyQuests);
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x00062601 File Offset: 0x00060801
	private void Update()
	{
		this.Fader.UpdateOpacity(this.WantVisible, 4f, true);
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0006261C File Offset: 0x0006081C
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		if (from == PanelType.GameInvisible && to == PanelType.Questboard)
		{
			UnityMainThreadDispatcher.Instance().Invoke(delegate
			{
				this.WantVisible = true;
			}, 0.2f);
			return;
		}
		if (this.Panels.Contains(to))
		{
			this.WantVisible = true;
			return;
		}
		this.WantVisible = false;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0006266B File Offset: 0x0006086B
	public Questboard_Background()
	{
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x0006267E File Offset: 0x0006087E
	[CompilerGenerated]
	private void <OnPanelChanged>b__6_0()
	{
		this.WantVisible = true;
	}

	// Token: 0x04000D7F RID: 3455
	public CanvasGroup Fader;

	// Token: 0x04000D80 RID: 3456
	private List<PanelType> Panels = new List<PanelType>();

	// Token: 0x04000D81 RID: 3457
	private bool WantVisible;
}
