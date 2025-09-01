using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mono.Btls
{
	// Token: 0x020000F0 RID: 240
	internal static class MonoBtlsUtils
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0000F628 File Offset: 0x0000D828
		public static bool Compare(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000F658 File Offset: 0x0000D858
		private static bool AppendEntry(StringBuilder sb, MonoBtlsX509Name name, int index, string separator, bool quotes)
		{
			MonoBtlsX509NameEntryType monoBtlsX509NameEntryType = name.GetEntryType(index);
			if (monoBtlsX509NameEntryType < MonoBtlsX509NameEntryType.Unknown)
			{
				return false;
			}
			if (monoBtlsX509NameEntryType == MonoBtlsX509NameEntryType.Unknown && MonoBtlsUtils.Compare(name.GetEntryOidData(index), MonoBtlsUtils.emailOid))
			{
				monoBtlsX509NameEntryType = MonoBtlsX509NameEntryType.Email;
			}
			int num;
			string text = name.GetEntryValue(index, out num);
			if (text == null)
			{
				return false;
			}
			string entryOid = name.GetEntryOid(index);
			if (entryOid == null)
			{
				return false;
			}
			if (sb.Length > 0)
			{
				sb.Append(separator);
			}
			switch (monoBtlsX509NameEntryType)
			{
			case MonoBtlsX509NameEntryType.CountryName:
				sb.Append("C=");
				break;
			case MonoBtlsX509NameEntryType.OrganizationName:
				sb.Append("O=");
				break;
			case MonoBtlsX509NameEntryType.OrganizationalUnitName:
				sb.Append("OU=");
				break;
			case MonoBtlsX509NameEntryType.CommonName:
				sb.Append("CN=");
				break;
			case MonoBtlsX509NameEntryType.LocalityName:
				sb.Append("L=");
				break;
			case MonoBtlsX509NameEntryType.StateOrProvinceName:
				sb.Append("S=");
				break;
			case MonoBtlsX509NameEntryType.StreetAddress:
				sb.Append("STREET=");
				break;
			case MonoBtlsX509NameEntryType.SerialNumber:
				sb.Append("SERIALNUMBER=");
				break;
			case MonoBtlsX509NameEntryType.DomainComponent:
				sb.Append("DC=");
				break;
			case MonoBtlsX509NameEntryType.UserId:
				sb.Append("UID=");
				break;
			case MonoBtlsX509NameEntryType.Email:
				sb.Append("E=");
				break;
			case MonoBtlsX509NameEntryType.DnQualifier:
				sb.Append("dnQualifier=");
				break;
			case MonoBtlsX509NameEntryType.Title:
				sb.Append("T=");
				break;
			case MonoBtlsX509NameEntryType.Surname:
				sb.Append("SN=");
				break;
			case MonoBtlsX509NameEntryType.GivenName:
				sb.Append("G=");
				break;
			case MonoBtlsX509NameEntryType.Initial:
				sb.Append("I=");
				break;
			default:
				sb.Append("OID.");
				sb.Append(entryOid);
				sb.Append("=");
				break;
			}
			char[] anyOf = new char[]
			{
				',',
				'+',
				'"',
				'\\',
				'<',
				'>',
				';'
			};
			if (quotes && num != 30 && (text.IndexOfAny(anyOf, 0, text.Length) > 0 || text.StartsWith(" ") || text.EndsWith(" ")))
			{
				text = "\"" + text + "\"";
			}
			sb.Append(text);
			return true;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000F883 File Offset: 0x0000DA83
		private static string GetSeparator(X500DistinguishedNameFlags flag)
		{
			if ((flag & X500DistinguishedNameFlags.UseSemicolons) != X500DistinguishedNameFlags.None)
			{
				return "; ";
			}
			if ((flag & X500DistinguishedNameFlags.UseCommas) != X500DistinguishedNameFlags.None)
			{
				return ", ";
			}
			if ((flag & X500DistinguishedNameFlags.UseNewLines) != X500DistinguishedNameFlags.None)
			{
				return Environment.NewLine;
			}
			return ", ";
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public static string FormatName(MonoBtlsX509Name name, X500DistinguishedNameFlags flag)
		{
			if (flag != X500DistinguishedNameFlags.None && (flag & (X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding)) == X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException("flag");
			}
			if (name.GetEntryCount() == 0)
			{
				return string.Empty;
			}
			bool reversed = (flag & X500DistinguishedNameFlags.Reversed) > X500DistinguishedNameFlags.None;
			bool quotes = (flag & X500DistinguishedNameFlags.DoNotUseQuotes) == X500DistinguishedNameFlags.None;
			string separator = MonoBtlsUtils.GetSeparator(flag);
			return MonoBtlsUtils.FormatName(name, reversed, separator, quotes);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000F908 File Offset: 0x0000DB08
		public static string FormatName(MonoBtlsX509Name name, bool reversed, string separator, bool quotes)
		{
			int entryCount = name.GetEntryCount();
			StringBuilder stringBuilder = new StringBuilder();
			if (reversed)
			{
				for (int i = entryCount - 1; i >= 0; i--)
				{
					MonoBtlsUtils.AppendEntry(stringBuilder, name, i, separator, quotes);
				}
			}
			else
			{
				for (int j = 0; j < entryCount; j++)
				{
					MonoBtlsUtils.AppendEntry(stringBuilder, name, j, separator, quotes);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000F95D File Offset: 0x0000DB5D
		// Note: this type is marked as 'beforefieldinit'.
		static MonoBtlsUtils()
		{
		}

		// Token: 0x040003D5 RID: 981
		private static byte[] emailOid = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			13,
			1,
			9,
			1
		};

		// Token: 0x040003D6 RID: 982
		private const X500DistinguishedNameFlags AllFlags = X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding;
	}
}
