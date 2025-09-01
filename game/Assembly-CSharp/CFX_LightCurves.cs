using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CFX_LightCurves : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x00004AAB File Offset: 0x00002CAB
	private void Start()
	{
		this.lightSource = base.GetComponent<Light>();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004AB9 File Offset: 0x00002CB9
	private void OnEnable()
	{
		this.startTime = Time.time;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00004AC8 File Offset: 0x00002CC8
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (num <= this.GraphScaleX)
		{
			float intensity = this.LightCurve.Evaluate(num / this.GraphScaleX) * this.GraphScaleY;
			this.lightSource.intensity = intensity;
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00004B12 File Offset: 0x00002D12
	public CFX_LightCurves()
	{
	}

	// Token: 0x04000024 RID: 36
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000025 RID: 37
	public float GraphScaleX = 1f;

	// Token: 0x04000026 RID: 38
	public float GraphScaleY = 1f;

	// Token: 0x04000027 RID: 39
	private float startTime;

	// Token: 0x04000028 RID: 40
	private Light lightSource;
}
