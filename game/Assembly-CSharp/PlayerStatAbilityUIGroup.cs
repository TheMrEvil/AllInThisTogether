using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000176 RID: 374
public class PlayerStatAbilityUIGroup : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	// Token: 0x06000FEE RID: 4078 RVA: 0x00064460 File Offset: 0x00062660
	public void Setup(AbilityTree ability, PlayerControl owner, int damage)
	{
		this.ownerRef = owner;
		this.abilityRef = ability;
		this.Icon.sprite = ability.Root.Usage.AbilityMetadata.Icon;
		if (this.DmgText != null)
		{
			this.DmgText.text = this.GetDamageNumber(damage);
		}
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(ability);
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x000644CC File Offset: 0x000626CC
	public void Setup(AugmentTree augment, PlayerControl owner, int damage)
	{
		this.ownerRef = owner;
		this.augmentRef = augment;
		this.Icon.sprite = augment.Root.Icon;
		if (this.DmgText != null)
		{
			this.DmgText.text = this.GetDamageNumber(damage);
		}
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(augment);
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x00064530 File Offset: 0x00062730
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

	// Token: 0x06000FF1 RID: 4081 RVA: 0x000645C8 File Offset: 0x000627C8
	public void OnPointerEnter(PointerEventData e)
	{
		if (this.abilityRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, this.TooltipAlign, this.abilityRef, this.ownerRef);
			return;
		}
		if (this.augmentRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, this.TooltipAlign, this.augmentRef, 1, this.ownerRef);
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00064637 File Offset: 0x00062837
	public void OnPointerExit(PointerEventData e)
	{
		Tooltip.Release();
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00064640 File Offset: 0x00062840
	public void OnSelect(BaseEventData ev)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.abilityRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, this.TooltipAlign, this.abilityRef, this.ownerRef);
			return;
		}
		if (this.augmentRef != null)
		{
			Tooltip.Show(this.TooltipAnchor.position, this.TooltipAlign, this.augmentRef, 1, this.ownerRef);
		}
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000646B7 File Offset: 0x000628B7
	public void OnDeselect(BaseEventData ev)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		Tooltip.Release();
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000646C6 File Offset: 0x000628C6
	public PlayerStatAbilityUIGroup()
	{
	}

	// Token: 0x04000E08 RID: 3592
	public Image Icon;

	// Token: 0x04000E09 RID: 3593
	public TextMeshProUGUI DmgText;

	// Token: 0x04000E0A RID: 3594
	private AbilityTree abilityRef;

	// Token: 0x04000E0B RID: 3595
	private AugmentTree augmentRef;

	// Token: 0x04000E0C RID: 3596
	private PlayerControl ownerRef;

	// Token: 0x04000E0D RID: 3597
	public Transform TooltipAnchor;

	// Token: 0x04000E0E RID: 3598
	public TextAnchor TooltipAlign;
}
