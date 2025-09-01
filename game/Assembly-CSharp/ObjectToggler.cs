using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class ObjectToggler : MonoBehaviour
{
	// Token: 0x0600174D RID: 5965 RVA: 0x000934DD File Offset: 0x000916DD
	public void ActivateDelayed(float delay)
	{
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			base.gameObject.SetActive(true);
		}, delay);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000934F6 File Offset: 0x000916F6
	public void DeactivateDelayed(float delay)
	{
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			base.gameObject.SetActive(false);
		}, delay);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x0009350F File Offset: 0x0009170F
	public ObjectToggler()
	{
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x00093517 File Offset: 0x00091717
	[CompilerGenerated]
	private void <ActivateDelayed>b__0_0()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x00093525 File Offset: 0x00091725
	[CompilerGenerated]
	private void <DeactivateDelayed>b__1_0()
	{
		base.gameObject.SetActive(false);
	}
}
