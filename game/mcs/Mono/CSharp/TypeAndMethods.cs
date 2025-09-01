using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000272 RID: 626
	internal struct TypeAndMethods
	{
		// Token: 0x04000B54 RID: 2900
		public TypeSpec type;

		// Token: 0x04000B55 RID: 2901
		public IList<MethodSpec> methods;

		// Token: 0x04000B56 RID: 2902
		public bool optional;

		// Token: 0x04000B57 RID: 2903
		public MethodData[] found;

		// Token: 0x04000B58 RID: 2904
		public MethodSpec[] need_proxy;
	}
}
