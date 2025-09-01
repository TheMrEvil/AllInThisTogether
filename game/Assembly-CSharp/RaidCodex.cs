using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000C5 RID: 197
public class RaidCodex : MonoBehaviour
{
	// Token: 0x06000904 RID: 2308 RVA: 0x0003D24F File Offset: 0x0003B44F
	private void Start()
	{
		RaidScene.instance.EncounterStarted.AddListener(new UnityAction(this.EncounterStarted));
		RaidScene.instance.EncounterReset.AddListener(new UnityAction(this.EncounterReset));
		this.EncounterReset();
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0003D28D File Offset: 0x0003B48D
	private void EncounterStarted()
	{
		this.DisplayIndicator.SetActive(false);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0003D29C File Offset: 0x0003B49C
	private void EncounterReset()
	{
		bool flag = Settings.HasCompletedUITutorial(UITutorial.Tutorial.RaidCodex);
		this.DisplayIndicator.SetActive(!flag);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
	public RaidCodex()
	{
	}

	// Token: 0x04000796 RID: 1942
	public GameObject DisplayIndicator;
}
