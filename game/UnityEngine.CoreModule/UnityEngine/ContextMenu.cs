using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F2 RID: 498
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x00023F37 File Offset: 0x00022137
		public ContextMenu(string itemName) : this(itemName, false)
		{
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00023F43 File Offset: 0x00022143
		public ContextMenu(string itemName, bool isValidateFunction) : this(itemName, isValidateFunction, 1000000)
		{
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00023F54 File Offset: 0x00022154
		public ContextMenu(string itemName, bool isValidateFunction, int priority)
		{
			this.menuItem = itemName;
			this.validate = isValidateFunction;
			this.priority = priority;
		}

		// Token: 0x040007D7 RID: 2007
		public readonly string menuItem;

		// Token: 0x040007D8 RID: 2008
		public readonly bool validate;

		// Token: 0x040007D9 RID: 2009
		public readonly int priority;
	}
}
