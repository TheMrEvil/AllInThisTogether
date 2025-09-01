using System;

namespace System.Collections
{
	// Token: 0x02000A39 RID: 2617
	[Serializable]
	internal sealed class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x06005CFC RID: 23804 RVA: 0x00138D60 File Offset: 0x00136F60
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x00138D98 File Offset: 0x00136F98
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x0000259F File Offset: 0x0000079F
		public StructuralEqualityComparer()
		{
		}
	}
}
