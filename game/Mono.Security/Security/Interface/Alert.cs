using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000036 RID: 54
	public class Alert
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public AlertLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000ED70 File Offset: 0x0000CF70
		public AlertDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public string Message
		{
			get
			{
				return Alert.GetAlertMessage(this.description);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000ED85 File Offset: 0x0000CF85
		public bool IsWarning
		{
			get
			{
				return this.level == AlertLevel.Warning;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000ED93 File Offset: 0x0000CF93
		public bool IsCloseNotify
		{
			get
			{
				return this.IsWarning && this.description == AlertDescription.CloseNotify;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public Alert(AlertDescription description)
		{
			this.description = description;
			this.inferAlertLevel();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000EDBD File Offset: 0x0000CFBD
		public Alert(AlertLevel level, AlertDescription description)
		{
			this.level = level;
			this.description = description;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		private void inferAlertLevel()
		{
			AlertDescription alertDescription = this.description;
			if (alertDescription <= AlertDescription.ExportRestriction)
			{
				if (alertDescription <= AlertDescription.UnexpectedMessage)
				{
					if (alertDescription != AlertDescription.CloseNotify)
					{
						if (alertDescription != AlertDescription.UnexpectedMessage)
						{
							goto IL_C5;
						}
						goto IL_C5;
					}
				}
				else
				{
					if (alertDescription - AlertDescription.BadRecordMAC <= 2)
					{
						goto IL_C5;
					}
					switch (alertDescription)
					{
					case AlertDescription.DecompressionFailure:
					case (AlertDescription)31:
					case (AlertDescription)32:
					case (AlertDescription)33:
					case (AlertDescription)34:
					case (AlertDescription)35:
					case (AlertDescription)36:
					case (AlertDescription)37:
					case (AlertDescription)38:
					case (AlertDescription)39:
					case AlertDescription.HandshakeFailure:
					case AlertDescription.NoCertificate_RESERVED:
					case AlertDescription.BadCertificate:
					case AlertDescription.UnsupportedCertificate:
					case AlertDescription.CertificateRevoked:
					case AlertDescription.CertificateExpired:
					case AlertDescription.CertificateUnknown:
					case AlertDescription.IlegalParameter:
					case AlertDescription.UnknownCA:
					case AlertDescription.AccessDenied:
					case AlertDescription.DecodeError:
					case AlertDescription.DecryptError:
						goto IL_C5;
					default:
						if (alertDescription != AlertDescription.ExportRestriction)
						{
							goto IL_C5;
						}
						goto IL_C5;
					}
				}
			}
			else if (alertDescription <= AlertDescription.InternalError)
			{
				if (alertDescription - AlertDescription.ProtocolVersion > 1 && alertDescription != AlertDescription.InternalError)
				{
					goto IL_C5;
				}
				goto IL_C5;
			}
			else if (alertDescription != AlertDescription.UserCancelled && alertDescription != AlertDescription.NoRenegotiation)
			{
				if (alertDescription != AlertDescription.UnsupportedExtension)
				{
					goto IL_C5;
				}
				goto IL_C5;
			}
			this.level = AlertLevel.Warning;
			return;
			IL_C5:
			this.level = AlertLevel.Fatal;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		public override string ToString()
		{
			return string.Format("[Alert: {0}:{1}]", this.Level, this.Description);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000EECF File Offset: 0x0000D0CF
		public static string GetAlertMessage(AlertDescription description)
		{
			return "The authentication or decryption has failed.";
		}

		// Token: 0x04000141 RID: 321
		private AlertLevel level;

		// Token: 0x04000142 RID: 322
		private AlertDescription description;
	}
}
