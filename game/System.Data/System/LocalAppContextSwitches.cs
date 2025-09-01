using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000067 RID: 103
	internal static class LocalAppContextSwitches
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00010C86 File Offset: 0x0000EE86
		public static bool AllowArbitraryTypeInstantiation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContext.GetCachedSwitchValue("Switch.System.Data.AllowArbitraryDataSetTypeInstantiation", ref LocalAppContextSwitches.s_allowArbitraryTypeInstantiation);
			}
		}

		// Token: 0x04000615 RID: 1557
		private static int s_allowArbitraryTypeInstantiation;
	}
}
