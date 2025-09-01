using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000135 RID: 309
public class AugmentBookBarItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0005A564 File Offset: 0x00058764
	public AugmentRootNode Mod
	{
		get
		{
			return this.mod;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0005A56C File Offset: 0x0005876C
	// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0005A574 File Offset: 0x00058774
	public bool CanShred
	{
		[CompilerGenerated]
		get
		{
			return this.<CanShred>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<CanShred>k__BackingField = value;
		}
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0005A580 File Offset: 0x00058780
	public void Setup(AugmentRootNode augment, PlayerControl plr = null)
	{
		this.mod = augment;
		this.owner = plr;
		this.Title.text = augment.Name;
		this.TitleHover.text = augment.Name;
		this.Icon.sprite = augment.Icon;
		this.IconBorder.sprite = GameDB.Quality(augment.DisplayQuality).Border;
		this.SetupPingInfo();
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x0005A5EF File Offset: 0x000587EF
	public void AllowShredding()
	{
		this.CanShred = true;
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x0005A5F8 File Offset: 0x000587F8
	private void SetupPingInfo()
	{
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null || this.mod == null)
		{
			return;
		}
		component.Setup(this.mod);
		UIPingable uipingable = component;
		UIPing.UIPingType pingType;
		switch (this.mod.modType)
		{
		case ModType.Enemy:
			pingType = UIPing.UIPingType.Augment_Enemy;
			break;
		case ModType.Fountain:
			pingType = UIPing.UIPingType.Augment_Font;
			break;
		case ModType.Binding:
			pingType = UIPing.UIPingType.Augment_Binding;
			break;
		default:
			pingType = UIPing.UIPingType.Augment_Player;
			break;
		}
		uipingable.PingType = pingType;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0005A66D File Offset: 0x0005886D
	public void OverrideAnchorLocation(TextAnchor loc)
	{
		this.anchorLocation = loc;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0005A678 File Offset: 0x00058878
	public void OnSelect(BaseEventData ev)
	{
		Tooltip.Show(this.TooltipAnchor.position, this.anchorLocation, this.mod.tree as AugmentTree, 1, this.owner);
		Action onSelected = this.OnSelected;
		if (onSelected != null)
		{
			onSelected();
		}
		if (this.CanShred)
		{
			this.ShredDisplay.SetActive(true);
			this.ShredFill.fillAmount = 0f;
		}
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0005A6E7 File Offset: 0x000588E7
	public void OnDeselect(BaseEventData ev)
	{
		Tooltip.Release();
		Action onDeselected = this.OnDeselected;
		if (onDeselected != null)
		{
			onDeselected();
		}
		this.ShredDisplay.SetActive(false);
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0005A70B File Offset: 0x0005890B
	public AugmentBookBarItem()
	{
	}

	// Token: 0x04000BA5 RID: 2981
	public Image Icon;

	// Token: 0x04000BA6 RID: 2982
	public Image IconBorder;

	// Token: 0x04000BA7 RID: 2983
	public TextMeshProUGUI Title;

	// Token: 0x04000BA8 RID: 2984
	public TextMeshProUGUI TitleHover;

	// Token: 0x04000BA9 RID: 2985
	public RectTransform TooltipAnchor;

	// Token: 0x04000BAA RID: 2986
	public CanvasGroup HoverGroup;

	// Token: 0x04000BAB RID: 2987
	public Action OnSelected;

	// Token: 0x04000BAC RID: 2988
	public Action OnDeselected;

	// Token: 0x04000BAD RID: 2989
	private AugmentRootNode mod;

	// Token: 0x04000BAE RID: 2990
	private PlayerControl owner;

	// Token: 0x04000BAF RID: 2991
	private TextAnchor anchorLocation = TextAnchor.MiddleLeft;

	// Token: 0x04000BB0 RID: 2992
	public GameObject ShredDisplay;

	// Token: 0x04000BB1 RID: 2993
	public Image ShredFill;

	// Token: 0x04000BB2 RID: 2994
	[CompilerGenerated]
	private bool <CanShred>k__BackingField;
}
