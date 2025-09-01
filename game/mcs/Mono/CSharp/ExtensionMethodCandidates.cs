using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020001BC RID: 444
	public class ExtensionMethodCandidates
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x0006EEF6 File Offset: 0x0006D0F6
		public ExtensionMethodCandidates(IMemberContext context, IList<MethodSpec> methods, NamespaceContainer nsContainer, int lookupIndex)
		{
			this.context = context;
			this.methods = methods;
			this.container = nsContainer;
			this.index = lookupIndex;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0006EF1B File Offset: 0x0006D11B
		public NamespaceContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0006EF23 File Offset: 0x0006D123
		public IMemberContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0006EF2B File Offset: 0x0006D12B
		public int LookupIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0006EF33 File Offset: 0x0006D133
		public IList<MethodSpec> Methods
		{
			get
			{
				return this.methods;
			}
		}

		// Token: 0x04000965 RID: 2405
		private readonly NamespaceContainer container;

		// Token: 0x04000966 RID: 2406
		private readonly IList<MethodSpec> methods;

		// Token: 0x04000967 RID: 2407
		private readonly int index;

		// Token: 0x04000968 RID: 2408
		private readonly IMemberContext context;
	}
}
