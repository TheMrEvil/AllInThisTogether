using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class BindingBarUI : MonoBehaviour
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0005B18C File Offset: 0x0005938C
	// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0005B194 File Offset: 0x00059394
	public BindingsPanel.TomeBinding Binding
	{
		[CompilerGenerated]
		get
		{
			return this.<Binding>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<Binding>k__BackingField = value;
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0005B1A0 File Offset: 0x000593A0
	public void Setup(BindingsPanel.TomeBinding binding)
	{
		this.Binding = binding;
		this.MainTitle.text = this.Binding.Augment.Root.Name;
		this.MainRank.Setup(this.Binding.Augment, this);
		UnlockDB.BindingUnlock bindingUnlock = UnlockDB.GetBindingUnlock(this.Binding.Augment);
		if (bindingUnlock != null && !string.IsNullOrEmpty(bindingUnlock.UnlockInfo) && !UnlockManager.IsBindingUnlocked(this.Binding.Augment))
		{
			this.UnlockInfoText.text = bindingUnlock.UnlockInfo;
			this.UnlockInfo.SetActive(true);
		}
		this.SubrankRef.SetActive(false);
		this.ClearRanks();
		foreach (BindingsPanel.TomeBinding.BindingRank bindingRank in this.Binding.Ranks)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SubrankRef, this.SubrankRef.transform.parent);
			BindingRankUI component = gameObject.GetComponent<BindingRankUI>();
			component.Setup(bindingRank.Augment, this);
			gameObject.SetActive(true);
			this.Ranks.Add(component);
		}
		this.ResetState();
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0005B2D8 File Offset: 0x000594D8
	public void TickUpdate()
	{
		this.MainRank.TickUpdate();
		foreach (BindingRankUI bindingRankUI in this.Ranks)
		{
			bindingRankUI.TickUpdate();
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0005B334 File Offset: 0x00059534
	public bool CanDeactivate(AugmentTree binding)
	{
		if (binding != this.MainRank.binding)
		{
			return true;
		}
		foreach (BindingRankUI bindingRankUI in this.Ranks)
		{
			if (BindingsPanel.instance.IsBindingActive(bindingRankUI.binding))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0005B3B0 File Offset: 0x000595B0
	public bool CanActivate(AugmentTree binding)
	{
		return binding == this.MainRank.binding || BindingsPanel.instance.IsBindingActive(this.MainRank.binding);
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0005B3E4 File Offset: 0x000595E4
	public void ResetState()
	{
		this.MainRank.ResetState();
		foreach (BindingRankUI bindingRankUI in this.Ranks)
		{
			bindingRankUI.ResetState();
		}
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0005B440 File Offset: 0x00059640
	private void ClearRanks()
	{
		foreach (BindingRankUI bindingRankUI in this.Ranks)
		{
			if (bindingRankUI != null)
			{
				UnityEngine.Object.Destroy(bindingRankUI.gameObject);
			}
		}
		this.Ranks = new List<BindingRankUI>();
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x0005B4AC File Offset: 0x000596AC
	public BindingBarUI()
	{
	}

	// Token: 0x04000BD4 RID: 3028
	[CompilerGenerated]
	private BindingsPanel.TomeBinding <Binding>k__BackingField;

	// Token: 0x04000BD5 RID: 3029
	public TextMeshProUGUI MainTitle;

	// Token: 0x04000BD6 RID: 3030
	public GameObject UnlockInfo;

	// Token: 0x04000BD7 RID: 3031
	public TextMeshProUGUI UnlockInfoText;

	// Token: 0x04000BD8 RID: 3032
	public BindingRankUI MainRank;

	// Token: 0x04000BD9 RID: 3033
	public GameObject SubrankRef;

	// Token: 0x04000BDA RID: 3034
	public List<BindingRankUI> Ranks = new List<BindingRankUI>();
}
