using System;

namespace System
{
	// Token: 0x02000213 RID: 531
	internal static class AppContextDefaultValues
	{
		// Token: 0x06001757 RID: 5975 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void PopulateDefaultValues()
		{
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005B215 File Offset: 0x00059415
		public static bool TryGetSwitchOverride(string switchName, out bool overrideValue)
		{
			overrideValue = false;
			return false;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005B21B File Offset: 0x0005941B
		// Note: this type is marked as 'beforefieldinit'.
		static AppContextDefaultValues()
		{
		}

		// Token: 0x0400164A RID: 5706
		internal const string SwitchNoAsyncCurrentCulture = "Switch.System.Globalization.NoAsyncCurrentCulture";

		// Token: 0x0400164B RID: 5707
		internal static readonly string SwitchEnforceJapaneseEraYearRanges = "Switch.System.Globalization.EnforceJapaneseEraYearRanges";

		// Token: 0x0400164C RID: 5708
		internal static readonly string SwitchFormatJapaneseFirstYearAsANumber = "Switch.System.Globalization.FormatJapaneseFirstYearAsANumber";

		// Token: 0x0400164D RID: 5709
		internal static readonly string SwitchEnforceLegacyJapaneseDateParsing = "Switch.System.Globalization.EnforceLegacyJapaneseDateParsing";

		// Token: 0x0400164E RID: 5710
		internal const string SwitchThrowExceptionIfDisposedCancellationTokenSource = "Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource";

		// Token: 0x0400164F RID: 5711
		internal const string SwitchPreserveEventListnerObjectIdentity = "Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity";

		// Token: 0x04001650 RID: 5712
		internal const string SwitchUseLegacyPathHandling = "Switch.System.IO.UseLegacyPathHandling";

		// Token: 0x04001651 RID: 5713
		internal const string SwitchBlockLongPaths = "Switch.System.IO.BlockLongPaths";

		// Token: 0x04001652 RID: 5714
		internal const string SwitchDoNotAddrOfCspParentWindowHandle = "Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle";

		// Token: 0x04001653 RID: 5715
		internal const string SwitchSetActorAsReferenceWhenCopyingClaimsIdentity = "Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity";
	}
}
