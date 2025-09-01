using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class BindingGlobalNode : MonoBehaviour
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x0005B4C0 File Offset: 0x000596C0
	public void Setup(WaveDB.GlobalBinding b)
	{
		this.activeAt = b.ActiveAt;
		this.binding = b.Binding;
		this.LoadRotation();
		this.InfoRef.Setup(this.binding, 1, ModType.Binding, null, TextAnchor.LowerCenter, this.InfoRef.AnchorLoc.position);
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.Setup(b.Binding);
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0005B52C File Offset: 0x0005972C
	public void TickUpdate(int curLevel)
	{
		this.isActive = (curLevel >= this.activeAt);
		foreach (CanvasGroup cg in this.activeGroups)
		{
			cg.UpdateOpacity(this.isActive, 3f, true);
		}
		this.FluxDisplay.DefaultWeight = (float)(this.isActive ? 1 : 0);
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0005B5B4 File Offset: 0x000597B4
	private void LoadRotation()
	{
		float num = BindingProgressAreaUI.GetPointOnRing((float)this.activeAt + 1f) * 360f;
		base.transform.localEulerAngles = new Vector3(0f, 0f, -num);
		this.CounterRotator.localEulerAngles = new Vector3(0f, 0f, num);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0005B611 File Offset: 0x00059811
	public BindingGlobalNode()
	{
	}

	// Token: 0x04000BDB RID: 3035
	public AugmentInfoBox InfoRef;

	// Token: 0x04000BDC RID: 3036
	public FluxyButton FluxDisplay;

	// Token: 0x04000BDD RID: 3037
	private AugmentTree binding;

	// Token: 0x04000BDE RID: 3038
	private int activeAt;

	// Token: 0x04000BDF RID: 3039
	public Transform CounterRotator;

	// Token: 0x04000BE0 RID: 3040
	public List<CanvasGroup> activeGroups;

	// Token: 0x04000BE1 RID: 3041
	private bool isActive;
}
