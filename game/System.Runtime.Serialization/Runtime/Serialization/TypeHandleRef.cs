using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000C2 RID: 194
	internal class TypeHandleRef
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0000222F File Offset: 0x0000042F
		public TypeHandleRef()
		{
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00030287 File Offset: 0x0002E487
		public TypeHandleRef(RuntimeTypeHandle value)
		{
			this.value = value;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00030296 File Offset: 0x0002E496
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0003029E File Offset: 0x0002E49E
		public RuntimeTypeHandle Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400048D RID: 1165
		private RuntimeTypeHandle value;
	}
}
