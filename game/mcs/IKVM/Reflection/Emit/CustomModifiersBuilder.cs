using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000D9 RID: 217
	public sealed class CustomModifiersBuilder
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00023300 File Offset: 0x00021500
		public void AddRequired(Type type)
		{
			CustomModifiersBuilder.Item item;
			item.type = type;
			item.required = true;
			this.list.Add(item);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002332C File Offset: 0x0002152C
		public void AddOptional(Type type)
		{
			CustomModifiersBuilder.Item item;
			item.type = type;
			item.required = false;
			this.list.Add(item);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00023358 File Offset: 0x00021558
		public void Add(Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			foreach (CustomModifiers.Entry entry in CustomModifiers.FromReqOpt(requiredCustomModifiers, optionalCustomModifiers))
			{
				CustomModifiersBuilder.Item item;
				item.type = entry.Type;
				item.required = entry.IsRequired;
				this.list.Add(item);
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000233D0 File Offset: 0x000215D0
		public CustomModifiers Create()
		{
			return new CustomModifiers(this.list);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000233DD File Offset: 0x000215DD
		public CustomModifiersBuilder()
		{
		}

		// Token: 0x0400042E RID: 1070
		private readonly List<CustomModifiersBuilder.Item> list = new List<CustomModifiersBuilder.Item>();

		// Token: 0x02000366 RID: 870
		internal struct Item
		{
			// Token: 0x04000F0C RID: 3852
			internal Type type;

			// Token: 0x04000F0D RID: 3853
			internal bool required;
		}
	}
}
