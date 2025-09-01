using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class DebugAnimator : MonoBehaviour
{
	// Token: 0x060016C1 RID: 5825 RVA: 0x00090BD4 File Offset: 0x0008EDD4
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x00090BE2 File Offset: 0x0008EDE2
	public void TryPlayAnim()
	{
		this.anim.Play(this.AnimToPlay);
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x00090BF5 File Offset: 0x0008EDF5
	public void TryCrossfade()
	{
		this.anim.CrossFade(this.AnimToPlay, this.AnimCrossVal);
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x00090C0E File Offset: 0x0008EE0E
	public void ApplyTrigger()
	{
		this.anim.SetTrigger(this.TriggerValue);
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x00090C21 File Offset: 0x0008EE21
	private void Update()
	{
		if (this.FloatToSet.Length > 0)
		{
			this.anim.SetFloat(this.FloatToSet, this.FloatVal);
		}
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x00090C48 File Offset: 0x0008EE48
	public DebugAnimator()
	{
	}

	// Token: 0x040016D9 RID: 5849
	private Animator anim;

	// Token: 0x040016DA RID: 5850
	[Header("Float Values")]
	public string FloatToSet;

	// Token: 0x040016DB RID: 5851
	public float FloatVal;

	// Token: 0x040016DC RID: 5852
	[Header("Anim Values")]
	public string AnimToPlay;

	// Token: 0x040016DD RID: 5853
	public float AnimCrossVal;

	// Token: 0x040016DE RID: 5854
	[Header("Trigger Values")]
	public string TriggerValue;
}
