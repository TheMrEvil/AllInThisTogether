using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public sealed class Spline
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00012510 File Offset: 0x00010710
		public Spline(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
			this.cachedData = new float[128];
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0001255C File Offset: 0x0001075C
		public void Cache(int frame)
		{
			if (frame == this.frameCount)
			{
				return;
			}
			int length = this.curve.length;
			if (this.m_Loop && length > 1)
			{
				if (this.m_InternalLoopingCurve == null)
				{
					this.m_InternalLoopingCurve = new AnimationCurve();
				}
				Keyframe key = this.curve[length - 1];
				key.time -= this.m_Range;
				Keyframe key2 = this.curve[0];
				key2.time += this.m_Range;
				this.m_InternalLoopingCurve.keys = this.curve.keys;
				this.m_InternalLoopingCurve.AddKey(key);
				this.m_InternalLoopingCurve.AddKey(key2);
			}
			for (int i = 0; i < 128; i++)
			{
				this.cachedData[i] = this.Evaluate((float)i * 0.0078125f, length);
			}
			this.frameCount = Time.renderedFrameCount;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001264A File Offset: 0x0001084A
		public float Evaluate(float t, int length)
		{
			if (length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001267B File Offset: 0x0001087B
		public float Evaluate(float t)
		{
			return this.Evaluate(t, this.curve.length);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0001268F File Offset: 0x0001088F
		public override int GetHashCode()
		{
			return 17 * 23 + this.curve.GetHashCode();
		}

		// Token: 0x040002B4 RID: 692
		public const int k_Precision = 128;

		// Token: 0x040002B5 RID: 693
		public const float k_Step = 0.0078125f;

		// Token: 0x040002B6 RID: 694
		public AnimationCurve curve;

		// Token: 0x040002B7 RID: 695
		[SerializeField]
		private bool m_Loop;

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x040002B9 RID: 697
		[SerializeField]
		private float m_Range;

		// Token: 0x040002BA RID: 698
		private AnimationCurve m_InternalLoopingCurve;

		// Token: 0x040002BB RID: 699
		private int frameCount = -1;

		// Token: 0x040002BC RID: 700
		public float[] cachedData;
	}
}
