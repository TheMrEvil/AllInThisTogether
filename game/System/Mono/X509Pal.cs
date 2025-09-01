using System;

namespace Mono
{
	// Token: 0x02000032 RID: 50
	internal static class X509Pal
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002F4A File Offset: 0x0000114A
		public static X509PalImpl Instance
		{
			get
			{
				return SystemDependencyProvider.Instance.X509Pal;
			}
		}
	}
}
