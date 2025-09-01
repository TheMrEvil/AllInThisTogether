using System;
using System.Collections.Generic;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200007E RID: 126
	public class ValueDropdownList<T> : List<ValueDropdownItem<T>>
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000405E File Offset: 0x0000225E
		public void Add(string text, T value)
		{
			base.Add(new ValueDropdownItem<T>(text, value));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000406D File Offset: 0x0000226D
		public void Add(T value)
		{
			base.Add(new ValueDropdownItem<T>(value.ToString(), value));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004088 File Offset: 0x00002288
		public ValueDropdownList()
		{
		}
	}
}
