using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	// Token: 0x020000E6 RID: 230
	public struct ExCostSortedList1 : IComparable<ExCostSortedList1>
	{
		// Token: 0x060003CF RID: 975 RVA: 0x000208D0 File Offset: 0x0001EAD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList1(float invalidCost)
		{
			this.cost = invalidCost;
			this.data = 0;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000208E0 File Offset: 0x0001EAE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ExCostSortedList1(float invalidCost, int initData)
		{
			this.cost = invalidCost;
			this.data = initData;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public bool IsValid
		{
			get
			{
				return this.cost >= 0f;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00020902 File Offset: 0x0001EB02
		public int Count
		{
			get
			{
				if (!this.IsValid)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0002090F File Offset: 0x0001EB0F
		public float Cost
		{
			get
			{
				return this.cost;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00020917 File Offset: 0x0001EB17
		public int Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0002091F File Offset: 0x0001EB1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(float cost, int item)
		{
			if (!this.IsValid || cost < this.cost)
			{
				this.cost = cost;
				this.data = item;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00020940 File Offset: 0x0001EB40
		public int CompareTo(ExCostSortedList1 other)
		{
			if (this.cost == other.cost)
			{
				return 0;
			}
			if (this.cost >= other.cost)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00020963 File Offset: 0x0001EB63
		public override string ToString()
		{
			return string.Format("({0} : {1})", this.cost, this.data);
		}

		// Token: 0x04000638 RID: 1592
		internal float cost;

		// Token: 0x04000639 RID: 1593
		internal int data;
	}
}
