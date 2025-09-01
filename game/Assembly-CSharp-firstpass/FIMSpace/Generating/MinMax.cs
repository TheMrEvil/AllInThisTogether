using System;
using UnityEngine;

namespace FIMSpace.Generating
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public struct MinMax
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000190FB File Offset: 0x000172FB
		public bool IsZero
		{
			get
			{
				return this.Min == 0 && this.Max == 0;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00019110 File Offset: 0x00017310
		public static MinMax zero
		{
			get
			{
				return new MinMax(0, 0);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00019119 File Offset: 0x00017319
		public Vector2 ToVector2
		{
			get
			{
				return new Vector2((float)this.Min, (float)this.Max);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001912E File Offset: 0x0001732E
		public Vector2Int ToVector2Int
		{
			get
			{
				return new Vector2Int(this.Min, this.Max);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00019141 File Offset: 0x00017341
		public MinMax(int min, int max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00019151 File Offset: 0x00017351
		public int GetRandom()
		{
			return (int)((float)this.Min + FGenerators.GetRandom() * (float)(this.Max + 1 - this.Min));
		}

		// Token: 0x04000304 RID: 772
		public int Min;

		// Token: 0x04000305 RID: 773
		public int Max;
	}
}
