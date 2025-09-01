using System;

namespace System.Reflection
{
	// Token: 0x020008FD RID: 2301
	internal struct MonoPropertyInfo
	{
		// Token: 0x0400306C RID: 12396
		public Type parent;

		// Token: 0x0400306D RID: 12397
		public Type declaring_type;

		// Token: 0x0400306E RID: 12398
		public string name;

		// Token: 0x0400306F RID: 12399
		public MethodInfo get_method;

		// Token: 0x04003070 RID: 12400
		public MethodInfo set_method;

		// Token: 0x04003071 RID: 12401
		public PropertyAttributes attrs;
	}
}
