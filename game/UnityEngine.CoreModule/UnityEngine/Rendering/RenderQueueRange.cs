using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000409 RID: 1033
	public struct RenderQueueRange : IEquatable<RenderQueueRange>
	{
		// Token: 0x06002345 RID: 9029 RVA: 0x0003B748 File Offset: 0x00039948
		public RenderQueueRange(int lowerBound, int upperBound)
		{
			bool flag = lowerBound < 0 || lowerBound > 5000;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("lowerBound", lowerBound, string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
			}
			bool flag2 = upperBound < 0 || upperBound > 5000;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("upperBound", upperBound, string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
			}
			this.m_LowerBound = lowerBound;
			this.m_UpperBound = upperBound;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0003B7E0 File Offset: 0x000399E0
		public static RenderQueueRange all
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0003B80C File Offset: 0x00039A0C
		public static RenderQueueRange opaque
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 0,
					m_UpperBound = 2500
				};
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x0003B838 File Offset: 0x00039A38
		public static RenderQueueRange transparent
		{
			get
			{
				return new RenderQueueRange
				{
					m_LowerBound = 2501,
					m_UpperBound = 5000
				};
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0003B868 File Offset: 0x00039A68
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x0003B880 File Offset: 0x00039A80
		public int lowerBound
		{
			get
			{
				return this.m_LowerBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The lower bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_LowerBound = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x0003B8C8 File Offset: 0x00039AC8
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x0003B8E0 File Offset: 0x00039AE0
		public int upperBound
		{
			get
			{
				return this.m_UpperBound;
			}
			set
			{
				bool flag = value < 0 || value > 5000;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("The upper bound must be at least {0} and at most {1}.", 0, 5000));
				}
				this.m_UpperBound = value;
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0003B928 File Offset: 0x00039B28
		public bool Equals(RenderQueueRange other)
		{
			return this.m_LowerBound == other.m_LowerBound && this.m_UpperBound == other.m_UpperBound;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0003B95C File Offset: 0x00039B5C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderQueueRange && this.Equals((RenderQueueRange)obj);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0003B994 File Offset: 0x00039B94
		public override int GetHashCode()
		{
			return this.m_LowerBound * 397 ^ this.m_UpperBound;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0003B9BC File Offset: 0x00039BBC
		public static bool operator ==(RenderQueueRange left, RenderQueueRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		public static bool operator !=(RenderQueueRange left, RenderQueueRange right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0003B9F5 File Offset: 0x00039BF5
		// Note: this type is marked as 'beforefieldinit'.
		static RenderQueueRange()
		{
		}

		// Token: 0x04000D1A RID: 3354
		private int m_LowerBound;

		// Token: 0x04000D1B RID: 3355
		private int m_UpperBound;

		// Token: 0x04000D1C RID: 3356
		private const int k_MinimumBound = 0;

		// Token: 0x04000D1D RID: 3357
		public static readonly int minimumBound = 0;

		// Token: 0x04000D1E RID: 3358
		private const int k_MaximumBound = 5000;

		// Token: 0x04000D1F RID: 3359
		public static readonly int maximumBound = 5000;
	}
}
