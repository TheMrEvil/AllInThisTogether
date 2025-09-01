using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000190 RID: 400
public class Diagetic_VignetteUI : MonoBehaviour
{
	// Token: 0x060010C9 RID: 4297 RVA: 0x00068897 File Offset: 0x00066A97
	public void Setup(VignetteTrigger v)
	{
		this.Title.text = v.Label;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x000688B5 File Offset: 0x00066AB5
	public void Setup(AIDiageticInteraction v)
	{
		this.Title.text = v.Label;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x000688D3 File Offset: 0x00066AD3
	public void Setup(SimpleDiagetic v)
	{
		this.Title.text = v.Label;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x000688F1 File Offset: 0x00066AF1
	public void Setup(BookMeshInteraction v)
	{
		this.Title.text = "Browse";
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0006890E File Offset: 0x00066B0E
	public void Setup(RaidScrollTrigger v)
	{
		this.Title.text = v.Label;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0006892C File Offset: 0x00066B2C
	public Diagetic_VignetteUI()
	{
	}

	// Token: 0x04000F18 RID: 3864
	public TextMeshProUGUI Title;
}
