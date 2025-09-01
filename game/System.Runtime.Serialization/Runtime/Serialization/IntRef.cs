using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C3 RID: 195
	internal class IntRef
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x000302A7 File Offset: 0x0002E4A7
		public IntRef(int value)
		{
			this.value = value;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x000302B6 File Offset: 0x0002E4B6
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400048E RID: 1166
		private int value;
	}
}
