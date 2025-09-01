using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C7 RID: 455
public class RezBar : MonoBehaviour
{
	// Token: 0x0600129B RID: 4763 RVA: 0x00072B2A File Offset: 0x00070D2A
	private void Awake()
	{
		this.FillRect = this.FillBar.rectTransform;
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x00072B3D File Offset: 0x00070D3D
	private void Start()
	{
		this.parentGroups = base.GetComponentsInParent<CanvasGroup>();
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00072B4B File Offset: 0x00070D4B
	private void Update()
	{
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00072B50 File Offset: 0x00070D50
	private bool IsVisible()
	{
		CanvasGroup[] array = this.parentGroups;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].alpha == 0f)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00072B84 File Offset: 0x00070D84
	public RezBar()
	{
	}

	// Token: 0x0400119F RID: 4511
	public Image FillBar;

	// Token: 0x040011A0 RID: 4512
	private RectTransform FillRect;

	// Token: 0x040011A1 RID: 4513
	public ParticleSystem RezFX;

	// Token: 0x040011A2 RID: 4514
	public RectTransform FXRect;

	// Token: 0x040011A3 RID: 4515
	public float BaseGravity;

	// Token: 0x040011A4 RID: 4516
	public float BaseLifetime;

	// Token: 0x040011A5 RID: 4517
	public AnimationCurve RateCurve;

	// Token: 0x040011A6 RID: 4518
	public AnimationCurve SpeedCurve;

	// Token: 0x040011A7 RID: 4519
	public float EndSize;

	// Token: 0x040011A8 RID: 4520
	public float EndSpeed;

	// Token: 0x040011A9 RID: 4521
	public float EndLifetime;

	// Token: 0x040011AA RID: 4522
	private CanvasGroup[] parentGroups;
}
