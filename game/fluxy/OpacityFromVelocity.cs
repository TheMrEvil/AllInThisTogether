using System;
using Fluxy;
using UnityEngine;

// Token: 0x02000003 RID: 3
[RequireComponent(typeof(FluxyTarget))]
public class OpacityFromVelocity : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002139 File Offset: 0x00000339
	private void Awake()
	{
		this.target = base.GetComponent<FluxyTarget>();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
	private void LateUpdate()
	{
		Color color = this.target.color;
		this.f += Time.deltaTime * this.FadeInSpeed;
		float time = Mathf.Min(this.target.velocity.magnitude, this.f);
		color.a = this.velocityToOpacity.Evaluate(time);
		this.target.color = color;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021B8 File Offset: 0x000003B8
	public OpacityFromVelocity()
	{
	}

	// Token: 0x04000005 RID: 5
	public float FadeInSpeed = 25f;

	// Token: 0x04000006 RID: 6
	public AnimationCurve velocityToOpacity = new AnimationCurve();

	// Token: 0x04000007 RID: 7
	private FluxyTarget target;

	// Token: 0x04000008 RID: 8
	private float f;
}
