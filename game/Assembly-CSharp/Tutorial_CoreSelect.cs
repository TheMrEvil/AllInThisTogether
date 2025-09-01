using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class Tutorial_CoreSelect : MonoBehaviour
{
	// Token: 0x060009FF RID: 2559 RVA: 0x00041F1C File Offset: 0x0004011C
	private void Awake()
	{
		LorePage page = this.Page;
		page.OnActivate = (Action)Delegate.Combine(page.OnActivate, new Action(this.PageCompleted));
		foreach (TutorialCorePickup tutorialCorePickup in this.CorePickups)
		{
			tutorialCorePickup.OnSelected = (Action<TutorialCorePickup>)Delegate.Combine(tutorialCorePickup.OnSelected, new Action<TutorialCorePickup>(this.CoreSelected));
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00041FB0 File Offset: 0x000401B0
	private void PageCompleted()
	{
		foreach (TutorialCorePickup tutorialCorePickup in this.CorePickups)
		{
			tutorialCorePickup.gameObject.SetActive(true);
		}
		if (this.ExtraActive != null)
		{
			this.ExtraActive.SetActive(true);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00042020 File Offset: 0x00040220
	private void CoreSelected(TutorialCorePickup chosen)
	{
		foreach (TutorialCorePickup tutorialCorePickup in this.CorePickups)
		{
			tutorialCorePickup.Deactivate();
		}
		if (this.ExtraActive != null)
		{
			this.ExtraActive.SetActive(false);
		}
		PlayerControl.myInstance.actions.SetCore(chosen.Core);
		Action onSelectedCore = this.OnSelectedCore;
		if (onSelectedCore != null)
		{
			onSelectedCore();
		}
		Settings.SaveLoadout();
		UnityEngine.Object.Destroy(base.gameObject, 3f);
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000420C8 File Offset: 0x000402C8
	public Tutorial_CoreSelect()
	{
	}

	// Token: 0x04000890 RID: 2192
	public Action OnSelectedCore;

	// Token: 0x04000891 RID: 2193
	public LorePage Page;

	// Token: 0x04000892 RID: 2194
	public List<TutorialCorePickup> CorePickups;

	// Token: 0x04000893 RID: 2195
	public GameObject ExtraActive;
}
