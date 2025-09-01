using System;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class Indicatable : MonoBehaviour
{
	// Token: 0x060012ED RID: 4845 RVA: 0x00074901 File Offset: 0x00072B01
	public virtual bool ShouldIndicate()
	{
		return !(PlayerCamera.myInstance == null) && Vector3.Distance(PlayerCamera.myInstance.transform.position, base.transform.position) <= 512f;
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060012EE RID: 4846 RVA: 0x0007493B File Offset: 0x00072B3B
	public virtual Transform Root
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x00074943 File Offset: 0x00072B43
	public Indicatable()
	{
	}

	// Token: 0x0400120A RID: 4618
	public Color IndicatorColor = new Color(255f, 255f, 255f, 255f);

	// Token: 0x0400120B RID: 4619
	public Sprite IconOverride;

	// Token: 0x0400120C RID: 4620
	public float RotationSpeed;

	// Token: 0x0400120D RID: 4621
	public Sprite ArrowOverride;

	// Token: 0x0400120E RID: 4622
	public float IconScale = 1f;

	// Token: 0x0400120F RID: 4623
	public float ArrowScaleMult = 1f;
}
