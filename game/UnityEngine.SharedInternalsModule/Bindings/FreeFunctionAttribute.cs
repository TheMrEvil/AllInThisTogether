using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal class FreeFunctionAttribute : NativeMethodAttribute
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002508 File Offset: 0x00000708
		public FreeFunctionAttribute()
		{
			base.IsFreeFunction = true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000251A File Offset: 0x0000071A
		public FreeFunctionAttribute(string name) : base(name, true)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002526 File Offset: 0x00000726
		public FreeFunctionAttribute(string name, bool isThreadSafe) : base(name, true, isThreadSafe)
		{
		}
	}
}
