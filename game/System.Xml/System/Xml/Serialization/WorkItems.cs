using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002DD RID: 733
	internal class WorkItems
	{
		// Token: 0x170005A9 RID: 1449
		internal ImportStructWorkItem this[int index]
		{
			get
			{
				return (ImportStructWorkItem)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000A399C File Offset: 0x000A1B9C
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000A39A9 File Offset: 0x000A1BA9
		internal void Add(ImportStructWorkItem item)
		{
			this.list.Add(item);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000A39B8 File Offset: 0x000A1BB8
		internal bool Contains(StructMapping mapping)
		{
			return this.IndexOf(mapping) >= 0;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000A39C8 File Offset: 0x000A1BC8
		internal int IndexOf(StructMapping mapping)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Mapping == mapping)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000A39F8 File Offset: 0x000A1BF8
		internal void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000A3A06 File Offset: 0x000A1C06
		public WorkItems()
		{
		}

		// Token: 0x04001A19 RID: 6681
		private ArrayList list = new ArrayList();
	}
}
