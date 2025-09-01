using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000E7 RID: 231
	public struct ExCostSortedList4
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00020985 File Offset: 0x0001EB85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList4(float invalidCost)
		{
			this.costs = invalidCost;
			this.data = 0;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000209A0 File Offset: 0x0001EBA0
		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				for (int i = 3; i >= 0; i--)
				{
					if (this.costs[i] >= 0f)
					{
						return i + 1;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000209D1 File Offset: 0x0001EBD1
		public bool IsValid
		{
			get
			{
				return this.costs[0] >= 0f;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000209EC File Offset: 0x0001EBEC
		public bool Add(float cost, int item)
		{
			if (this.costs[3] >= 0f && cost > this.costs[3])
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				float num = this.costs[i];
				if (num < 0f)
				{
					this.costs[i] = cost;
					this.data[i] = item;
					return true;
				}
				if (cost < num)
				{
					for (int j = 2; j >= i; j--)
					{
						this.costs[j + 1] = this.costs[j];
						this.data[j + 1] = this.data[j];
					}
					this.costs[i] = cost;
					this.data[i] = item;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(int item)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.costs[i] >= 0f && this.data[i] == item)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00020B04 File Offset: 0x0001ED04
		public int indexOf(int item)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.costs[i] >= 0f && this.data[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00020B44 File Offset: 0x0001ED44
		public void RemoveItem(int item)
		{
			int num = -1;
			for (int i = 0; i < 4; i++)
			{
				if (this.costs[i] >= 0f && this.data[i] == item)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			for (int j = num; j < 3; j++)
			{
				this.costs[j] = this.costs[j + 1];
				this.data[j] = this.data[j + 1];
			}
			this.costs[3] = -1f;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00020BDB File Offset: 0x0001EDDB
		public float MinCost
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.costs[0];
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00020BEC File Offset: 0x0001EDEC
		public float MaxCost
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				for (int i = 3; i >= 0; i--)
				{
					if (this.costs[i] >= 0f)
					{
						return this.costs[i];
					}
				}
				return 0f;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00020C2C File Offset: 0x0001EE2C
		public override string ToString()
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			for (int i = 0; i < this.Count; i++)
			{
				ref fixedString512Bytes.Append(string.Format("({0} : {1}) ", this.costs[i], this.data[i]));
			}
			return fixedString512Bytes.ToString();
		}

		// Token: 0x0400063A RID: 1594
		internal float4 costs;

		// Token: 0x0400063B RID: 1595
		internal int4 data;
	}
}
