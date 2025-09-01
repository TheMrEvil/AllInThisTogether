using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000162 RID: 354
public class LoadoutEntry : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000F7C RID: 3964 RVA: 0x00062294 File Offset: 0x00060494
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000622B4 File Offset: 0x000604B4
	public void Setup(Progression.FullLoadout loadout, int index)
	{
		this.Index = index;
		this.Loadout = loadout;
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(loadout.Abilities.Core.Root.magicColor);
		if (core != null)
		{
			this.SignatureIcon.sprite = core.MajorIcon;
		}
		this.isUnlocked = loadout.CanEquip();
		this.LockDisplay.SetActive(!this.isUnlocked);
		this.UpdateDisplayInfo();
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00062324 File Offset: 0x00060524
	public void UpdateDisplayInfo()
	{
		this.Title.text = this.Loadout.Name;
		this.UpdateIsEquipped(Settings.CurFullLoadout);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00062348 File Offset: 0x00060548
	public void UpdateIsEquipped(int curSelected)
	{
		bool flag = curSelected == this.Index;
		this.SelectedDisplay.SetActive(flag);
		bool flag2 = false;
		if (flag)
		{
			flag2 = !new Progression.FullLoadout(PlayerControl.myInstance).Matches(this.Loadout);
		}
		this.ChangesDisplay.SetActive(flag && flag2);
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x00062397 File Offset: 0x00060597
	public void Click()
	{
		if (this.IsNewButton)
		{
			LoadoutPanel.instance.TryCreateNewLoadout();
			return;
		}
		if (!this.isUnlocked)
		{
			return;
		}
		LoadoutPanel.instance.EquipSelectedLoadout();
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x000623BF File Offset: 0x000605BF
	public void OnSelect(BaseEventData ev)
	{
		LoadoutPanel.instance.SelectLoadout(this.Index);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000623D1 File Offset: 0x000605D1
	public LoadoutEntry()
	{
	}

	// Token: 0x04000D6C RID: 3436
	public bool IsNewButton;

	// Token: 0x04000D6D RID: 3437
	public TextMeshProUGUI Title;

	// Token: 0x04000D6E RID: 3438
	public Image SignatureIcon;

	// Token: 0x04000D6F RID: 3439
	public GameObject SelectedDisplay;

	// Token: 0x04000D70 RID: 3440
	public GameObject ChangesDisplay;

	// Token: 0x04000D71 RID: 3441
	public GameObject LockDisplay;

	// Token: 0x04000D72 RID: 3442
	[NonSerialized]
	public int Index = -1;

	// Token: 0x04000D73 RID: 3443
	[NonSerialized]
	public Progression.FullLoadout Loadout;

	// Token: 0x04000D74 RID: 3444
	private bool isUnlocked;
}
