using System;

namespace System
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x0005A58E File Offset: 0x0005878E
		private ReflectionOnlyType()
		{
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0005A596 File Offset: 0x00058796
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
		}
	}
}
