using System;
using System.Collections.Generic;

namespace System.Collections
{
	// Token: 0x02000A3A RID: 2618
	[Serializable]
	internal sealed class StructuralComparer : IComparer
	{
		// Token: 0x06005CFF RID: 23807 RVA: 0x00138DC4 File Offset: 0x00136FC4
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer<object>.Default.Compare(x, y);
			}
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0000259F File Offset: 0x0000079F
		public StructuralComparer()
		{
		}
	}
}
