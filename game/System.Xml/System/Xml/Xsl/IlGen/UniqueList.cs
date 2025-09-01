using System;
using System.Collections.Generic;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A2 RID: 1186
	internal class UniqueList<T>
	{
		// Token: 0x06002E6F RID: 11887 RVA: 0x0010FE28 File Offset: 0x0010E028
		public int Add(T value)
		{
			int num;
			if (!this.lookup.ContainsKey(value))
			{
				num = this.list.Count;
				this.lookup.Add(value, num);
				this.list.Add(value);
			}
			else
			{
				num = this.lookup[value];
			}
			return num;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0010FE78 File Offset: 0x0010E078
		public T[] ToArray()
		{
			return this.list.ToArray();
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x0010FE85 File Offset: 0x0010E085
		public UniqueList()
		{
		}

		// Token: 0x040024CD RID: 9421
		private Dictionary<T, int> lookup = new Dictionary<T, int>();

		// Token: 0x040024CE RID: 9422
		private List<T> list = new List<T>();
	}
}
