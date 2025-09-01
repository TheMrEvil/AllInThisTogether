using System;
using System.Text;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the distinguished name of an X509 certificate. This class cannot be inherited.</summary>
	// Token: 0x020002CE RID: 718
	[MonoTODO("Some X500DistinguishedNameFlags options aren't supported, like DoNotUsePlusSign, DoNotUseQuotes and ForceUTF8Encoding")]
	public sealed class X500DistinguishedName : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="encodedDistinguishedName">An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object that represents the distinguished name.</param>
		// Token: 0x0600161A RID: 5658 RVA: 0x000588A1 File Offset: 0x00056AA1
		public X500DistinguishedName(AsnEncodedData encodedDistinguishedName)
		{
			if (encodedDistinguishedName == null)
			{
				throw new ArgumentNullException("encodedDistinguishedName");
			}
			base.RawData = encodedDistinguishedName.RawData;
			if (base.RawData.Length != 0)
			{
				this.DecodeRawData();
				return;
			}
			this.name = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified byte array.</summary>
		/// <param name="encodedDistinguishedName">A byte array that contains distinguished name information.</param>
		// Token: 0x0600161B RID: 5659 RVA: 0x000588DE File Offset: 0x00056ADE
		public X500DistinguishedName(byte[] encodedDistinguishedName)
		{
			if (encodedDistinguishedName == null)
			{
				throw new ArgumentNullException("encodedDistinguishedName");
			}
			base.Oid = new Oid();
			base.RawData = encodedDistinguishedName;
			if (encodedDistinguishedName.Length != 0)
			{
				this.DecodeRawData();
				return;
			}
			this.name = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified string.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		// Token: 0x0600161C RID: 5660 RVA: 0x0005891C File Offset: 0x00056B1C
		public X500DistinguishedName(string distinguishedName) : this(distinguishedName, X500DistinguishedNameFlags.Reversed)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified string and <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedNameFlags" /> flag.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		/// <param name="flag">A bitwise combination of the enumeration values that specify the characteristics of the distinguished name.</param>
		// Token: 0x0600161D RID: 5661 RVA: 0x00058928 File Offset: 0x00056B28
		public X500DistinguishedName(string distinguishedName, X500DistinguishedNameFlags flag)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			if (flag != X500DistinguishedNameFlags.None && (flag & (X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding)) == X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException("flag");
			}
			base.Oid = new Oid();
			if (distinguishedName.Length == 0)
			{
				byte[] array = new byte[2];
				array[0] = 48;
				base.RawData = array;
				this.DecodeRawData();
				return;
			}
			ASN1 asn = X501.FromString(distinguishedName);
			if ((flag & X500DistinguishedNameFlags.Reversed) != X500DistinguishedNameFlags.None)
			{
				ASN1 asn2 = new ASN1(48);
				for (int i = asn.Count - 1; i >= 0; i--)
				{
					asn2.Add(asn[i]);
				}
				asn = asn2;
			}
			base.RawData = asn.GetBytes();
			if (flag == X500DistinguishedNameFlags.None)
			{
				this.name = distinguishedName;
				return;
			}
			this.name = this.Decode(flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</summary>
		/// <param name="distinguishedName">An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</param>
		// Token: 0x0600161E RID: 5662 RVA: 0x000589E6 File Offset: 0x00056BE6
		public X500DistinguishedName(X500DistinguishedName distinguishedName)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			base.Oid = new Oid();
			base.RawData = distinguishedName.RawData;
			this.name = distinguishedName.name;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00058A1F File Offset: 0x00056C1F
		internal X500DistinguishedName(byte[] encoded, byte[] canonEncoding, string name)
		{
			this.canonEncoding = canonEncoding;
			this.name = name;
			base.Oid = new Oid();
			base.RawData = encoded;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00058A47 File Offset: 0x00056C47
		internal byte[] CanonicalEncoding
		{
			get
			{
				return this.canonEncoding;
			}
		}

		/// <summary>Gets the comma-delimited distinguished name from an X500 certificate.</summary>
		/// <returns>The comma-delimited distinguished name of the X509 certificate.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00058A4F File Offset: 0x00056C4F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Decodes a distinguished name using the characteristics specified by the <paramref name="flag" /> parameter.</summary>
		/// <param name="flag">A bitwise combination of the enumeration values that specify the characteristics of the distinguished name.</param>
		/// <returns>The decoded distinguished name.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has an invalid name.</exception>
		// Token: 0x06001622 RID: 5666 RVA: 0x00058A58 File Offset: 0x00056C58
		public string Decode(X500DistinguishedNameFlags flag)
		{
			if (flag != X500DistinguishedNameFlags.None && (flag & (X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding)) == X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException("flag");
			}
			if (base.RawData.Length == 0)
			{
				return string.Empty;
			}
			bool reversed = (flag & X500DistinguishedNameFlags.Reversed) > X500DistinguishedNameFlags.None;
			bool quotes = (flag & X500DistinguishedNameFlags.DoNotUseQuotes) == X500DistinguishedNameFlags.None;
			string separator = X500DistinguishedName.GetSeparator(flag);
			return X501.ToString(new ASN1(base.RawData), reversed, separator, quotes);
		}

		/// <summary>Returns a formatted version of an X500 distinguished name for printing or for output to a text window or to a console.</summary>
		/// <param name="multiLine">
		///   <see langword="true" /> if the return string should contain carriage returns; otherwise, <see langword="false" />.</param>
		/// <returns>A formatted string that represents the X500 distinguished name.</returns>
		// Token: 0x06001623 RID: 5667 RVA: 0x00058AB4 File Offset: 0x00056CB4
		public override string Format(bool multiLine)
		{
			if (!multiLine)
			{
				return this.Decode(X500DistinguishedNameFlags.UseCommas);
			}
			string text = this.Decode(X500DistinguishedNameFlags.UseNewLines);
			if (text.Length > 0)
			{
				return text + Environment.NewLine;
			}
			return text;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0000F883 File Offset: 0x0000DA83
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

		// Token: 0x06001625 RID: 5669 RVA: 0x00058AF4 File Offset: 0x00056CF4
		private void DecodeRawData()
		{
			if (base.RawData == null || base.RawData.Length < 3)
			{
				this.name = string.Empty;
				return;
			}
			ASN1 seq = new ASN1(base.RawData);
			this.name = X501.ToString(seq, true, ", ", true);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00058B40 File Offset: 0x00056D40
		private static string Canonize(string s)
		{
			int i = s.IndexOf('=') + 1;
			StringBuilder stringBuilder = new StringBuilder(s.Substring(0, i));
			while (i < s.Length && char.IsWhiteSpace(s, i))
			{
				i++;
			}
			s = s.TrimEnd();
			bool flag = false;
			while (i < s.Length)
			{
				if (!flag)
				{
					goto IL_4B;
				}
				flag = char.IsWhiteSpace(s, i);
				if (!flag)
				{
					goto IL_4B;
				}
				IL_69:
				i++;
				continue;
				IL_4B:
				if (char.IsWhiteSpace(s, i))
				{
					flag = true;
				}
				stringBuilder.Append(char.ToUpperInvariant(s[i]));
				goto IL_69;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00058BCC File Offset: 0x00056DCC
		internal static bool AreEqual(X500DistinguishedName name1, X500DistinguishedName name2)
		{
			if (name1 == null)
			{
				return name2 == null;
			}
			if (name2 == null)
			{
				return false;
			}
			if (name1.canonEncoding != null && name2.canonEncoding != null)
			{
				if (name1.canonEncoding.Length != name2.canonEncoding.Length)
				{
					return false;
				}
				for (int i = 0; i < name1.canonEncoding.Length; i++)
				{
					if (name1.canonEncoding[i] != name2.canonEncoding[i])
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				X500DistinguishedNameFlags flag = X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseNewLines;
				string[] separator = new string[]
				{
					Environment.NewLine
				};
				string[] array = name1.Decode(flag).Split(separator, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = name2.Decode(flag).Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (X500DistinguishedName.Canonize(array[j]) != X500DistinguishedName.Canonize(array2[j]))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x04000CE6 RID: 3302
		private const X500DistinguishedNameFlags AllFlags = X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding;

		// Token: 0x04000CE7 RID: 3303
		private string name;

		// Token: 0x04000CE8 RID: 3304
		private byte[] canonEncoding;
	}
}
