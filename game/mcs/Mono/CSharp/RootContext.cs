using System;

namespace Mono.CSharp
{
	// Token: 0x02000297 RID: 663
	public class RootContext
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0009EA9E File Offset: 0x0009CC9E
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x0009EAA5 File Offset: 0x0009CCA5
		public static ModuleContainer ToplevelTypes
		{
			get
			{
				return RootContext.root;
			}
			set
			{
				RootContext.root = value;
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00002CCC File Offset: 0x00000ECC
		public RootContext()
		{
		}

		// Token: 0x04000BF2 RID: 3058
		private static ModuleContainer root;
	}
}
