using System;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class CosmeticsUIControl : MonoBehaviour
{
	// Token: 0x060010AE RID: 4270 RVA: 0x00067EE1 File Offset: 0x000660E1
	private void Awake()
	{
		CosmeticsUIControl.instance = this;
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x00067F10 File Offset: 0x00066110
	public void TransitionIn()
	{
		this.Display.gameObject.SetActive(true);
		this.ChangeFX.Play();
		PlayerControl.myInstance.Input.OverrideCamera(this.CameraAnchor, 6f, false);
		CosmeticSet curSet = PlayerControl.myInstance.Display.CurSet;
		CosmeticsUIControl.SetCosmetic(curSet.Head);
		CosmeticsUIControl.SetCosmetic(curSet.Skin);
		CosmeticsUIControl.SetCosmetic(curSet.Back);
		this.lightRef.Activate();
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x00067F8E File Offset: 0x0006618E
	public void TransitionOut()
	{
		this.Display.gameObject.SetActive(false);
		this.ChangeFX.Play();
		PlayerControl.myInstance.Input.ReturnCamera(6f, true);
		this.lightRef.Deactivate();
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x00067FCC File Offset: 0x000661CC
	private void OnPanelChanged(PanelType from, PanelType to)
	{
		if (to == PanelType.Cosmetics)
		{
			this.TransitionIn();
			return;
		}
		if (from == PanelType.Cosmetics)
		{
			this.TransitionOut();
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x00067FE5 File Offset: 0x000661E5
	public static void SetCosmetic(Cosmetic c)
	{
		CosmeticsUIControl.instance.Display.SetCosmetic(c);
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x00067FF7 File Offset: 0x000661F7
	public static void SetCosmeticTab(CosmeticType cType)
	{
		CosmeticsUIControl.instance.Display.SetPreviewTab(cType);
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x00068009 File Offset: 0x00066209
	public static void EquipFX()
	{
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0006800B File Offset: 0x0006620B
	private void OnDestroy()
	{
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Remove(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(this.OnPanelChanged));
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x00068033 File Offset: 0x00066233
	public CosmeticsUIControl()
	{
	}

	// Token: 0x04000EF1 RID: 3825
	public Transform CameraAnchor;

	// Token: 0x04000EF2 RID: 3826
	public CosmeticDisplay Display;

	// Token: 0x04000EF3 RID: 3827
	public static CosmeticsUIControl instance;

	// Token: 0x04000EF4 RID: 3828
	public EffectLight lightRef;

	// Token: 0x04000EF5 RID: 3829
	public float MoveTime = 3f;

	// Token: 0x04000EF6 RID: 3830
	private Transform camHolderParent;

	// Token: 0x04000EF7 RID: 3831
	public ParticleSystem ChangeFX;

	// Token: 0x04000EF8 RID: 3832
	public ParticleSystem DummyChangedFX;
}
