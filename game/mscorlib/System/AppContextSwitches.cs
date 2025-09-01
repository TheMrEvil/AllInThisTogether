using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001E2 RID: 482
	internal static class AppContextSwitches
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x000519DE File Offset: 0x0004FBDE
		public static bool NoAsyncCurrentCulture
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.NoAsyncCurrentCulture", ref AppContextSwitches._noAsyncCurrentCulture);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000519EF File Offset: 0x0004FBEF
		public static bool EnforceJapaneseEraYearRanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceJapaneseEraYearRanges, ref AppContextSwitches._enforceJapaneseEraYearRanges);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00051A00 File Offset: 0x0004FC00
		public static bool FormatJapaneseFirstYearAsANumber
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchFormatJapaneseFirstYearAsANumber, ref AppContextSwitches._formatJapaneseFirstYearAsANumber);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00051A11 File Offset: 0x0004FC11
		public static bool EnforceLegacyJapaneseDateParsing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceLegacyJapaneseDateParsing, ref AppContextSwitches._enforceLegacyJapaneseDateParsing);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00051A22 File Offset: 0x0004FC22
		public static bool ThrowExceptionIfDisposedCancellationTokenSource
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource", ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00051A33 File Offset: 0x0004FC33
		public static bool PreserveEventListnerObjectIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity", ref AppContextSwitches._preserveEventListnerObjectIdentity);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00051A44 File Offset: 0x0004FC44
		public static bool UseLegacyPathHandling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.UseLegacyPathHandling", ref AppContextSwitches._useLegacyPathHandling);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00051A55 File Offset: 0x0004FC55
		public static bool BlockLongPaths
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.BlockLongPaths", ref AppContextSwitches._blockLongPaths);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00051A66 File Offset: 0x0004FC66
		public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity", ref AppContextSwitches._cloneActor);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00051A77 File Offset: 0x0004FC77
		public static bool DoNotAddrOfCspParentWindowHandle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle", ref AppContextSwitches._doNotAddrOfCspParentWindowHandle);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00051A88 File Offset: 0x0004FC88
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00051A8F File Offset: 0x0004FC8F
		private static bool DisableCaching
		{
			[CompilerGenerated]
			get
			{
				return AppContextSwitches.<DisableCaching>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				AppContextSwitches.<DisableCaching>k__BackingField = value;
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00051A98 File Offset: 0x0004FC98
		static AppContextSwitches()
		{
			bool disableCaching;
			if (AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out disableCaching))
			{
				AppContextSwitches.DisableCaching = disableCaching;
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00051AB9 File Offset: 0x0004FCB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00051AD0 File Offset: 0x0004FCD0
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (AppContextSwitches.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : -1);
			return flag;
		}

		// Token: 0x04001477 RID: 5239
		private static int _noAsyncCurrentCulture;

		// Token: 0x04001478 RID: 5240
		private static int _enforceJapaneseEraYearRanges;

		// Token: 0x04001479 RID: 5241
		private static int _formatJapaneseFirstYearAsANumber;

		// Token: 0x0400147A RID: 5242
		private static int _enforceLegacyJapaneseDateParsing;

		// Token: 0x0400147B RID: 5243
		private static int _throwExceptionIfDisposedCancellationTokenSource;

		// Token: 0x0400147C RID: 5244
		private static int _preserveEventListnerObjectIdentity;

		// Token: 0x0400147D RID: 5245
		private static int _useLegacyPathHandling;

		// Token: 0x0400147E RID: 5246
		private static int _blockLongPaths;

		// Token: 0x0400147F RID: 5247
		private static int _cloneActor;

		// Token: 0x04001480 RID: 5248
		private static int _doNotAddrOfCspParentWindowHandle;

		// Token: 0x04001481 RID: 5249
		[CompilerGenerated]
		private static bool <DisableCaching>k__BackingField;
	}
}
