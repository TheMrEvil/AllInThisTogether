using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x02000820 RID: 2080
	internal static class MailHeaderInfo
	{
		// Token: 0x0600422F RID: 16943 RVA: 0x000E4BAC File Offset: 0x000E2DAC
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.s_headerInfo.Length; i++)
			{
				MailHeaderInfo.s_headerDictionary.Add(MailHeaderInfo.s_headerInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000E4EE8 File Offset: 0x000E30E8
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.s_headerInfo[(int)id].NormalizedName;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000E4F08 File Offset: 0x000E3108
		internal static MailHeaderID GetID(string name)
		{
			int result;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out result))
			{
				return MailHeaderID.Unknown;
			}
			return (MailHeaderID)result;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000E4F28 File Offset: 0x000E3128
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].IsUserSettable;
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000E4F58 File Offset: 0x000E3158
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) && MailHeaderInfo.s_headerInfo[num].IsSingleton;
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000E4F88 File Offset: 0x000E3188
		internal static string NormalizeCase(string name)
		{
			int num;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num))
			{
				return name;
			}
			return MailHeaderInfo.s_headerInfo[num].NormalizedName;
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000E4FB8 File Offset: 0x000E31B8
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].AllowsUnicode;
		}

		// Token: 0x0400283B RID: 10299
		private static readonly MailHeaderInfo.HeaderInfo[] s_headerInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x0400283C RID: 10300
		private static readonly Dictionary<string, int> s_headerDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000821 RID: 2081
		private readonly struct HeaderInfo
		{
			// Token: 0x06004236 RID: 16950 RVA: 0x000E4FE6 File Offset: 0x000E31E6
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x0400283D RID: 10301
			public readonly string NormalizedName;

			// Token: 0x0400283E RID: 10302
			public readonly bool IsSingleton;

			// Token: 0x0400283F RID: 10303
			public readonly MailHeaderID ID;

			// Token: 0x04002840 RID: 10304
			public readonly bool IsUserSettable;

			// Token: 0x04002841 RID: 10305
			public readonly bool AllowsUnicode;
		}
	}
}
