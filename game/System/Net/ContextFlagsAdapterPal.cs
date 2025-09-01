using System;

namespace System.Net
{
	// Token: 0x0200055F RID: 1375
	internal static class ContextFlagsAdapterPal
	{
		// Token: 0x06002C99 RID: 11417 RVA: 0x000980F8 File Offset: 0x000962F8
		internal static ContextFlagsPal GetContextFlagsPalFromInterop(Interop.SspiCli.ContextFlags win32Flags)
		{
			ContextFlagsPal contextFlagsPal = ContextFlagsPal.None;
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((win32Flags & contextFlagMapping.Win32Flag) == contextFlagMapping.Win32Flag)
				{
					contextFlagsPal |= contextFlagMapping.ContextFlag;
				}
			}
			return contextFlagsPal;
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x00098140 File Offset: 0x00096340
		internal static Interop.SspiCli.ContextFlags GetInteropFromContextFlagsPal(ContextFlagsPal flags)
		{
			Interop.SspiCli.ContextFlags contextFlags = Interop.SspiCli.ContextFlags.Zero;
			foreach (ContextFlagsAdapterPal.ContextFlagMapping contextFlagMapping in ContextFlagsAdapterPal.s_contextFlagMapping)
			{
				if ((flags & contextFlagMapping.ContextFlag) == contextFlagMapping.ContextFlag)
				{
					contextFlags |= contextFlagMapping.Win32Flag;
				}
			}
			return contextFlags;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x00098188 File Offset: 0x00096388
		// Note: this type is marked as 'beforefieldinit'.
		static ContextFlagsAdapterPal()
		{
		}

		// Token: 0x040017F9 RID: 6137
		private static readonly ContextFlagsAdapterPal.ContextFlagMapping[] s_contextFlagMapping = new ContextFlagsAdapterPal.ContextFlagMapping[]
		{
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptExtendedError, ContextFlagsPal.AcceptExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.InitManualCredValidation, ContextFlagsPal.InitManualCredValidation),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptIntegrity, ContextFlagsPal.AcceptIntegrity),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptStream, ContextFlagsPal.AcceptStream),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AllocateMemory, ContextFlagsPal.AllocateMemory),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AllowMissingBindings, ContextFlagsPal.AllowMissingBindings),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.Confidentiality, ContextFlagsPal.Confidentiality),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.Connection, ContextFlagsPal.Connection),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.Delegate, ContextFlagsPal.Delegate),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.InitExtendedError, ContextFlagsPal.InitExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptIntegrity, ContextFlagsPal.AcceptIntegrity),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.InitManualCredValidation, ContextFlagsPal.InitManualCredValidation),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptStream, ContextFlagsPal.AcceptStream),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.AcceptExtendedError, ContextFlagsPal.AcceptExtendedError),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.InitUseSuppliedCreds, ContextFlagsPal.InitUseSuppliedCreds),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.MutualAuth, ContextFlagsPal.MutualAuth),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.ProxyBindings, ContextFlagsPal.ProxyBindings),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.ReplayDetect, ContextFlagsPal.ReplayDetect),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.SequenceDetect, ContextFlagsPal.SequenceDetect),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.UnverifiedTargetName, ContextFlagsPal.UnverifiedTargetName),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.UseSessionKey, ContextFlagsPal.UseSessionKey),
			new ContextFlagsAdapterPal.ContextFlagMapping(Interop.SspiCli.ContextFlags.Zero, ContextFlagsPal.None)
		};

		// Token: 0x02000560 RID: 1376
		private readonly struct ContextFlagMapping
		{
			// Token: 0x06002C9C RID: 11420 RVA: 0x0009835E File Offset: 0x0009655E
			public ContextFlagMapping(Interop.SspiCli.ContextFlags win32Flag, ContextFlagsPal contextFlag)
			{
				this.Win32Flag = win32Flag;
				this.ContextFlag = contextFlag;
			}

			// Token: 0x040017FA RID: 6138
			public readonly Interop.SspiCli.ContextFlags Win32Flag;

			// Token: 0x040017FB RID: 6139
			public readonly ContextFlagsPal ContextFlag;
		}
	}
}
