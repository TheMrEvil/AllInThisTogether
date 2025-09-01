using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.AI
{
	// Token: 0x02000014 RID: 20
	public struct NavMeshQueryFilter
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000028EB File Offset: 0x00000AEB
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000028F3 File Offset: 0x00000AF3
		internal float[] costs
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<costs>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<costs>k__BackingField = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000028FC File Offset: 0x00000AFC
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002904 File Offset: 0x00000B04
		public int areaMask
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<areaMask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<areaMask>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000290D File Offset: 0x00000B0D
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002915 File Offset: 0x00000B15
		public int agentTypeID
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<agentTypeID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<agentTypeID>k__BackingField = value;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002920 File Offset: 0x00000B20
		public float GetAreaCost(int areaIndex)
		{
			bool flag = this.costs == null;
			float result;
			if (flag)
			{
				bool flag2 = areaIndex < 0 || areaIndex >= 32;
				if (flag2)
				{
					string message = string.Format("The valid range is [0:{0}]", 31);
					throw new IndexOutOfRangeException(message);
				}
				result = 1f;
			}
			else
			{
				result = this.costs[areaIndex];
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002980 File Offset: 0x00000B80
		public void SetAreaCost(int areaIndex, float cost)
		{
			bool flag = this.costs == null;
			if (flag)
			{
				this.costs = new float[32];
				for (int i = 0; i < 32; i++)
				{
					this.costs[i] = 1f;
				}
			}
			this.costs[areaIndex] = cost;
		}

		// Token: 0x0400002B RID: 43
		private const int k_AreaCostElementCount = 32;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float[] <costs>k__BackingField;

		// Token: 0x0400002D RID: 45
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <areaMask>k__BackingField;

		// Token: 0x0400002E RID: 46
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <agentTypeID>k__BackingField;
	}
}
