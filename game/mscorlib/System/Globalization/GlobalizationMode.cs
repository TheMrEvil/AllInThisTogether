using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200097E RID: 2430
	internal static class GlobalizationMode
	{
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x00121221 File Offset: 0x0011F421
		internal static bool Invariant
		{
			[CompilerGenerated]
			get
			{
				return GlobalizationMode.<Invariant>k__BackingField;
			}
		} = GlobalizationMode.GetGlobalizationInvariantMode();

		// Token: 0x060055A6 RID: 21926 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		private static bool GetGlobalizationInvariantMode()
		{
			return false;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00121228 File Offset: 0x0011F428
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalizationMode()
		{
		}

		// Token: 0x04003553 RID: 13651
		private const string c_InvariantModeConfigSwitch = "System.Globalization.Invariant";

		// Token: 0x04003554 RID: 13652
		[CompilerGenerated]
		private static readonly bool <Invariant>k__BackingField;
	}
}
