using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004DA RID: 1242
	[Serializable]
	internal sealed class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x0008AB56 File Offset: 0x00088D56
		public TreeSet()
		{
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0008AB5E File Offset: 0x00088D5E
		public TreeSet(IComparer<T> comparer) : base(comparer)
		{
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0008AB67 File Offset: 0x00088D67
		public TreeSet(SerializationInfo siInfo, StreamingContext context) : base(siInfo, context)
		{
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0008AB71 File Offset: 0x00088D71
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", item));
			}
			return flag;
		}
	}
}
