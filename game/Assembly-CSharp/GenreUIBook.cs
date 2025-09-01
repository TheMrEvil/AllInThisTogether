using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000191 RID: 401
public class GenreUIBook : Selectable
{
	// Token: 0x060010CF RID: 4303 RVA: 0x00068934 File Offset: 0x00066B34
	private new void Start()
	{
		this.ping = base.GetComponent<UIPingable>();
		UIPingable uipingable = this.ping;
		if (uipingable == null)
		{
			return;
		}
		uipingable.Setup(this.Genre);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x00068958 File Offset: 0x00066B58
	public void UpdateLockedInfo()
	{
		if (this.Genre == null)
		{
			return;
		}
		this.isUnlocked = UnlockManager.IsGenreUnlocked(this.Genre);
		foreach (GameObject gameObject in this.UnlockedVisible)
		{
			gameObject.gameObject.SetActive(this.isUnlocked);
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000689D4 File Offset: 0x00066BD4
	public void TickUpdate(bool isHovered, bool isSelected)
	{
		if (this.MeshDisplay != null)
		{
			this.UpdateMaterial(isHovered, isSelected);
		}
		if (isHovered && !isSelected && InputManager.IsUsingController && InputManager.UIAct.UIPrimary.WasPressed)
		{
			GenreUIControl.instance.SelectBook(this);
		}
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x00068A20 File Offset: 0x00066C20
	private void UpdateMaterial(bool isHovered, bool isSelected)
	{
		if (this._propBlock == null)
		{
			this._propBlock = new MaterialPropertyBlock();
		}
		this.MeshDisplay.GetPropertyBlock(this._propBlock);
		this.hoverVal = Mathf.Lerp(this.hoverVal, (float)(isHovered ? 1 : 0), Time.deltaTime * 6f);
		this.bwVal = Mathf.Lerp(this.bwVal, (float)(this.isUnlocked ? 0 : 1), Time.deltaTime * 4f);
		this._propBlock.SetFloat("_BWFade", this.bwVal);
		this._propBlock.SetFloat("_RimValue", this.hoverVal);
		this.MeshDisplay.SetPropertyBlock(this._propBlock);
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x00068ADB File Offset: 0x00066CDB
	public override void OnSelect(BaseEventData ev)
	{
		base.OnSelect(ev);
		if (!InputManager.IsUsingController)
		{
			return;
		}
		GenreUIControl.instance.HoverBook(this);
		GenreUIControl.instance.SelectBook(this);
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x00068B02 File Offset: 0x00066D02
	public void OnHovered()
	{
		UIPingable uipingable = this.ping;
		if (uipingable == null)
		{
			return;
		}
		uipingable.OnSelect(new BaseEventData(null));
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00068B1A File Offset: 0x00066D1A
	public override void OnDeselect(BaseEventData ev)
	{
		base.OnDeselect(ev);
		UIPingable uipingable = this.ping;
		if (uipingable == null)
		{
			return;
		}
		uipingable.OnSelect(ev);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x00068B34 File Offset: 0x00066D34
	public GenreUIBook()
	{
	}

	// Token: 0x04000F19 RID: 3865
	public GenreTree Genre;

	// Token: 0x04000F1A RID: 3866
	public Transform BottomAnchor;

	// Token: 0x04000F1B RID: 3867
	public Transform TooltipAnchor;

	// Token: 0x04000F1C RID: 3868
	public TextAnchor TooltipView;

	// Token: 0x04000F1D RID: 3869
	public Renderer MeshDisplay;

	// Token: 0x04000F1E RID: 3870
	private MaterialPropertyBlock _propBlock;

	// Token: 0x04000F1F RID: 3871
	public List<GameObject> UnlockedVisible;

	// Token: 0x04000F20 RID: 3872
	[NonSerialized]
	public bool isUnlocked;

	// Token: 0x04000F21 RID: 3873
	private float bwVal;

	// Token: 0x04000F22 RID: 3874
	private float hoverVal;

	// Token: 0x04000F23 RID: 3875
	private UIPingable ping;
}
