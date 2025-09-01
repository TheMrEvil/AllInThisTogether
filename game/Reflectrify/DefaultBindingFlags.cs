using System;
using System.Reflection;

namespace Reflectrify
{
	// Token: 0x0200000D RID: 13
	public static class DefaultBindingFlags
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000268A File Offset: 0x0000088A
		public static BindingFlags Instance
		{
			get
			{
				return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000268E File Offset: 0x0000088E
		public static BindingFlags Static
		{
			get
			{
				return BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			}
		}
	}
}
