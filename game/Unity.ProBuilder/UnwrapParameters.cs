using System;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public sealed class UnwrapParameters
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0002177A File Offset: 0x0001F97A
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00021782 File Offset: 0x0001F982
		public float hardAngle
		{
			get
			{
				return this.m_HardAngle;
			}
			set
			{
				this.m_HardAngle = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0002178B File Offset: 0x0001F98B
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00021793 File Offset: 0x0001F993
		public float packMargin
		{
			get
			{
				return this.m_PackMargin;
			}
			set
			{
				this.m_PackMargin = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0002179C File Offset: 0x0001F99C
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000217A4 File Offset: 0x0001F9A4
		public float angleError
		{
			get
			{
				return this.m_AngleError;
			}
			set
			{
				this.m_AngleError = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000217AD File Offset: 0x0001F9AD
		// (set) Token: 0x0600038B RID: 907 RVA: 0x000217B5 File Offset: 0x0001F9B5
		public float areaError
		{
			get
			{
				return this.m_AreaError;
			}
			set
			{
				this.m_AreaError = value;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000217BE File Offset: 0x0001F9BE
		public UnwrapParameters()
		{
			this.Reset();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000217F8 File Offset: 0x0001F9F8
		public UnwrapParameters(UnwrapParameters other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.hardAngle = other.hardAngle;
			this.packMargin = other.packMargin;
			this.angleError = other.angleError;
			this.areaError = other.areaError;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00021875 File Offset: 0x0001FA75
		public void Reset()
		{
			this.hardAngle = 88f;
			this.packMargin = 20f;
			this.angleError = 8f;
			this.areaError = 15f;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000218A4 File Offset: 0x0001FAA4
		public override string ToString()
		{
			return string.Format("hardAngle: {0}\npackMargin: {1}\nangleError: {2}\nareaError: {3}", new object[]
			{
				this.hardAngle,
				this.packMargin,
				this.angleError,
				this.areaError
			});
		}

		// Token: 0x04000207 RID: 519
		internal const float k_HardAngle = 88f;

		// Token: 0x04000208 RID: 520
		internal const float k_PackMargin = 20f;

		// Token: 0x04000209 RID: 521
		internal const float k_AngleError = 8f;

		// Token: 0x0400020A RID: 522
		internal const float k_AreaError = 15f;

		// Token: 0x0400020B RID: 523
		[Tooltip("Angle between neighbor triangles that will generate seam.")]
		[Range(1f, 180f)]
		[SerializeField]
		[FormerlySerializedAs("hardAngle")]
		private float m_HardAngle = 88f;

		// Token: 0x0400020C RID: 524
		[Tooltip("Measured in pixels, assuming mesh will cover an entire 1024x1024 lightmap.")]
		[Range(1f, 64f)]
		[SerializeField]
		[FormerlySerializedAs("packMargin")]
		private float m_PackMargin = 20f;

		// Token: 0x0400020D RID: 525
		[Tooltip("Measured in percents. Angle error measures deviation of UV angles from geometry angles. Area error measures deviation of UV triangles area from geometry triangles if they were uniformly scaled.")]
		[Range(1f, 75f)]
		[SerializeField]
		[FormerlySerializedAs("angleError")]
		private float m_AngleError = 8f;

		// Token: 0x0400020E RID: 526
		[Range(1f, 75f)]
		[SerializeField]
		[FormerlySerializedAs("areaError")]
		private float m_AreaError = 15f;
	}
}
