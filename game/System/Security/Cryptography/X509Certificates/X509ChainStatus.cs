using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides a simple structure for storing X509 chain status and error information.</summary>
	// Token: 0x020002E0 RID: 736
	public struct X509ChainStatus
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x0005CBF1 File Offset: 0x0005ADF1
		internal X509ChainStatus(X509ChainStatusFlags flag)
		{
			this.status = flag;
			this.info = X509ChainStatus.GetInformation(flag);
		}

		/// <summary>Specifies the status of the X509 chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatusFlags" /> value.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0005CC06 File Offset: 0x0005AE06
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x0005CC0E File Offset: 0x0005AE0E
		public X509ChainStatusFlags Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		/// <summary>Specifies a description of the <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> value.</summary>
		/// <returns>A localizable string.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005CC17 File Offset: 0x0005AE17
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0005CC1F File Offset: 0x0005AE1F
		public string StatusInformation
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005CC28 File Offset: 0x0005AE28
		internal static string GetInformation(X509ChainStatusFlags flags)
		{
			if (flags <= X509ChainStatusFlags.InvalidNameConstraints)
			{
				if (flags <= X509ChainStatusFlags.RevocationStatusUnknown)
				{
					if (flags <= X509ChainStatusFlags.NotValidForUsage)
					{
						switch (flags)
						{
						case X509ChainStatusFlags.NoError:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.NotTimeNested:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotTimeNested | X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.NotTimeNested | X509ChainStatusFlags.Revoked:
							goto IL_125;
						case X509ChainStatusFlags.NotTimeValid:
						case X509ChainStatusFlags.NotTimeNested:
						case X509ChainStatusFlags.Revoked:
						case X509ChainStatusFlags.NotSignatureValid:
							break;
						default:
							if (flags != X509ChainStatusFlags.NotValidForUsage)
							{
								goto IL_125;
							}
							break;
						}
					}
					else if (flags != X509ChainStatusFlags.UntrustedRoot && flags != X509ChainStatusFlags.RevocationStatusUnknown)
					{
						goto IL_125;
					}
				}
				else if (flags <= X509ChainStatusFlags.InvalidExtension)
				{
					if (flags != X509ChainStatusFlags.Cyclic && flags != X509ChainStatusFlags.InvalidExtension)
					{
						goto IL_125;
					}
				}
				else if (flags != X509ChainStatusFlags.InvalidPolicyConstraints && flags != X509ChainStatusFlags.InvalidBasicConstraints && flags != X509ChainStatusFlags.InvalidNameConstraints)
				{
					goto IL_125;
				}
			}
			else if (flags <= X509ChainStatusFlags.PartialChain)
			{
				if (flags <= X509ChainStatusFlags.HasNotDefinedNameConstraint)
				{
					if (flags != X509ChainStatusFlags.HasNotSupportedNameConstraint && flags != X509ChainStatusFlags.HasNotDefinedNameConstraint)
					{
						goto IL_125;
					}
				}
				else if (flags != X509ChainStatusFlags.HasNotPermittedNameConstraint && flags != X509ChainStatusFlags.HasExcludedNameConstraint && flags != X509ChainStatusFlags.PartialChain)
				{
					goto IL_125;
				}
			}
			else if (flags <= X509ChainStatusFlags.CtlNotSignatureValid)
			{
				if (flags != X509ChainStatusFlags.CtlNotTimeValid && flags != X509ChainStatusFlags.CtlNotSignatureValid)
				{
					goto IL_125;
				}
			}
			else if (flags != X509ChainStatusFlags.CtlNotValidForUsage && flags != X509ChainStatusFlags.OfflineRevocation && flags != X509ChainStatusFlags.NoIssuanceChainPolicy)
			{
				goto IL_125;
			}
			return Locale.GetText(flags.ToString());
			IL_125:
			return string.Empty;
		}

		// Token: 0x04000D22 RID: 3362
		private X509ChainStatusFlags status;

		// Token: 0x04000D23 RID: 3363
		private string info;
	}
}
