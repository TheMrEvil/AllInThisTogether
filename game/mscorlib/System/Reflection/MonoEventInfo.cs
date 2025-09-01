using System;

namespace System.Reflection
{
	// Token: 0x020008F3 RID: 2291
	internal struct MonoEventInfo
	{
		// Token: 0x04003046 RID: 12358
		public Type declaring_type;

		// Token: 0x04003047 RID: 12359
		public Type reflected_type;

		// Token: 0x04003048 RID: 12360
		public string name;

		// Token: 0x04003049 RID: 12361
		public MethodInfo add_method;

		// Token: 0x0400304A RID: 12362
		public MethodInfo remove_method;

		// Token: 0x0400304B RID: 12363
		public MethodInfo raise_method;

		// Token: 0x0400304C RID: 12364
		public EventAttributes attrs;

		// Token: 0x0400304D RID: 12365
		public MethodInfo[] other_methods;
	}
}
