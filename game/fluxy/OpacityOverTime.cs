using System;
using Fluxy;
using UnityEngine;

// Token: 0x02000004 RID: 4
[RequireComponent(typeof(FluxyTarget))]
public class OpacityOverTime : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x000021D6 File Offset: 0x000003D6
	private void Awake()
	{
		this.target = base.GetComponent<FluxyTarget>();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021E4 File Offset: 0x000003E4
	private void Update()
	{
		Color color = this.target.color;
		this.t += Time.deltaTime / this.Duration;
		color.a = this.OpacityCurve.Evaluate(this.t);
		this.target.color = color;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000223A File Offset: 0x0000043A
	public OpacityOverTime()
	{
	}

	// Token: 0x04000009 RID: 9
	public AnimationCurve OpacityCurve;

	// Token: 0x0400000A RID: 10
	public float Duration = 3f;

	// Token: 0x0400000B RID: 11
	private FluxyTarget target;

	// Token: 0x0400000C RID: 12
	private float t;
}
