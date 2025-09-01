using System;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class CurveSerializeData
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000D412 File Offset: 0x0000B612
		public CurveSerializeData()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000D43C File Offset: 0x0000B63C
		public CurveSerializeData(float value)
		{
			this.value = value;
			this.useCurve = false;
			this.curve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000D49C File Offset: 0x0000B69C
		public CurveSerializeData(float value, float curveStart, float curveEnd, bool useCurve = true)
		{
			this.value = value;
			this.useCurve = useCurve;
			this.curve = AnimationCurve.Linear(0f, Mathf.Clamp01(curveStart), 1f, Mathf.Clamp01(curveEnd));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		public CurveSerializeData(float value, AnimationCurve curve)
		{
			this.value = value;
			this.useCurve = true;
			this.curve = curve;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000D53A File Offset: 0x0000B73A
		public void SetValue(float value)
		{
			this.value = value;
			this.useCurve = false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000D54A File Offset: 0x0000B74A
		public void SetValue(float value, float curveStart, float curveEnd, bool useCurve = true)
		{
			this.value = value;
			this.useCurve = useCurve;
			this.curve = AnimationCurve.Linear(0f, Mathf.Clamp01(curveStart), 1f, Mathf.Clamp01(curveEnd));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000D57C File Offset: 0x0000B77C
		public void SetValue(float value, AnimationCurve curve)
		{
			this.value = value;
			this.useCurve = true;
			this.curve = curve;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000D593 File Offset: 0x0000B793
		public void DataValidate(float min, float max)
		{
			this.value = Mathf.Clamp(this.value, min, max);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public float Evaluate(float time)
		{
			if (this.useCurve)
			{
				return this.curve.Evaluate(time) * this.value;
			}
			return this.value;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public float4x4 ConvertFloatArray()
		{
			if (this.useCurve)
			{
				return DataUtility.ConvertAnimationCurve(this.curve) * this.value;
			}
			return this.value;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		public CurveSerializeData Clone()
		{
			return new CurveSerializeData
			{
				value = this.value,
				useCurve = this.useCurve,
				curve = new AnimationCurve(this.curve.keys)
				{
					preWrapMode = this.curve.preWrapMode,
					postWrapMode = this.curve.postWrapMode
				}
			};
		}

		// Token: 0x0400021F RID: 543
		public float value;

		// Token: 0x04000220 RID: 544
		public bool useCurve;

		// Token: 0x04000221 RID: 545
		public AnimationCurve curve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
	}
}
