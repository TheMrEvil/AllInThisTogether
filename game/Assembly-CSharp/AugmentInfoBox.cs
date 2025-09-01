using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000137 RID: 311
public class AugmentInfoBox : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0005AB88 File Offset: 0x00058D88
	public bool IsEmpty
	{
		get
		{
			return this.mod == null;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005AB96 File Offset: 0x00058D96
	private float yPos
	{
		get
		{
			return base.transform.position.y;
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0005ABA8 File Offset: 0x00058DA8
	public void Setup(AugmentRootNode m, int stacks, ModType modType, EntityControl owner, TextAnchor tipAnchor, Vector3 tipLoc)
	{
		this.mod = m;
		this.stackCount = stacks;
		this.modOwner = owner;
		this.ModType = modType;
		if (m != null)
		{
			if (this.icon != null)
			{
				this.icon.sprite = m.Icon;
			}
			if (this.extraIcon != null)
			{
				this.extraIcon.sprite = m.Icon;
			}
		}
		this.tooltipAnchor = tipAnchor;
		this.tooltipLoc = tipLoc;
		if (this.DmgDisplay != null)
		{
			this.DmgDisplay.SetActive(false);
		}
		if (this.countDisplay != null)
		{
			this.countDisplay.text = ((stacks > 1) ? stacks.ToString() : "");
		}
		if (this.TitleLabel != null)
		{
			this.TitleLabel.text = m.Name;
		}
		if (this.extraTitle != null)
		{
			this.extraTitle.text = m.Name;
		}
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(m);
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005ACBD File Offset: 0x00058EBD
	public void SetupDamage(int value)
	{
		if (this.DmgDisplay == null || value <= 0)
		{
			return;
		}
		this.DmgDisplay.SetActive(true);
		this.DmgText.text = this.GetDamageNumber(value);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0005ACF0 File Offset: 0x00058EF0
	private string GetDamageNumber(int dmg)
	{
		if (dmg < 1000)
		{
			return dmg.ToString();
		}
		if (dmg < 1000000)
		{
			return ((double)dmg / 1000.0).ToString("0.##") + "k";
		}
		if (dmg < 1000000000)
		{
			return ((double)dmg / 1000000.0).ToString("0.##") + "m";
		}
		return ((double)dmg / 1000000000.0).ToString("0.##") + "b";
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0005AD88 File Offset: 0x00058F88
	public void NoInteraction()
	{
		this.nonInteractive = true;
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0005AD94 File Offset: 0x00058F94
	public void OnPointerEnter(PointerEventData ev)
	{
		if (this.nonInteractive || InputManager.IsUsingController)
		{
			return;
		}
		Vector3 position = this.tooltipLoc;
		if (this.AnchorLoc != null)
		{
			position = this.AnchorLoc.position;
		}
		Tooltip.Show(position, this.tooltipAnchor, this.mod.tree as AugmentTree, this.stackCount, this.modOwner);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x0005ADFA File Offset: 0x00058FFA
	public void OnPointerExit(PointerEventData ev)
	{
		if (this.nonInteractive || InputManager.IsUsingController)
		{
			return;
		}
		Tooltip.Release();
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0005AE14 File Offset: 0x00059014
	public void OnSelect(BaseEventData ev)
	{
		if (this.nonInteractive || !InputManager.IsUsingController)
		{
			return;
		}
		Vector3 position = this.tooltipLoc;
		if (this.AnchorLoc != null)
		{
			position = this.AnchorLoc.position;
		}
		Tooltip.Show(position, this.tooltipAnchor, this.mod.tree as AugmentTree, this.stackCount, this.modOwner);
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x0005AE7A File Offset: 0x0005907A
	public AugmentInfoBox()
	{
	}

	// Token: 0x04000BBD RID: 3005
	public Image icon;

	// Token: 0x04000BBE RID: 3006
	private AugmentRootNode mod;

	// Token: 0x04000BBF RID: 3007
	private ModType ModType;

	// Token: 0x04000BC0 RID: 3008
	private EntityControl modOwner;

	// Token: 0x04000BC1 RID: 3009
	private int stackCount;

	// Token: 0x04000BC2 RID: 3010
	[Header("Optional")]
	public TextMeshProUGUI countDisplay;

	// Token: 0x04000BC3 RID: 3011
	public TextMeshProUGUI TitleLabel;

	// Token: 0x04000BC4 RID: 3012
	public RectTransform AnchorLoc;

	// Token: 0x04000BC5 RID: 3013
	[Header("Extras")]
	public Image extraIcon;

	// Token: 0x04000BC6 RID: 3014
	public TextMeshProUGUI extraTitle;

	// Token: 0x04000BC7 RID: 3015
	[Header("Damage Info")]
	public GameObject DmgDisplay;

	// Token: 0x04000BC8 RID: 3016
	public TextMeshProUGUI DmgText;

	// Token: 0x04000BC9 RID: 3017
	private TextAnchor tooltipAnchor;

	// Token: 0x04000BCA RID: 3018
	private Vector3 tooltipLoc;

	// Token: 0x04000BCB RID: 3019
	private bool nonInteractive;
}
