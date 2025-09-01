using System;

namespace System.Collections.Generic
{
	// Token: 0x02000ACE RID: 2766
	[Serializable]
	internal sealed class InternalStringComparer : EqualityComparer<string>
	{
		// Token: 0x060062C1 RID: 25281 RVA: 0x0014A50C File Offset: 0x0014870C
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0014A519 File Offset: 0x00148719
		public override bool Equals(string x, string y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x == y || x.Equals(y);
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x0014A530 File Offset: 0x00148730
		internal override int IndexOf(string[] array, string value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (Array.UnsafeLoad<string>(array, i) == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x0014A560 File Offset: 0x00148760
		public InternalStringComparer()
		{
		}
	}
}
