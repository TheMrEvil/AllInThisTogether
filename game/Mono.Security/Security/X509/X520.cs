using System;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x0200001D RID: 29
	public class X520
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000B921 File Offset: 0x00009B21
		public X520()
		{
		}

		// Token: 0x02000089 RID: 137
		public abstract class AttributeTypeAndValue
		{
			// Token: 0x0600053A RID: 1338 RVA: 0x000198E0 File Offset: 0x00017AE0
			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = byte.MaxValue;
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x00019901 File Offset: 0x00017B01
			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001991E File Offset: 0x00017B1E
			// (set) Token: 0x0600053D RID: 1341 RVA: 0x00019928 File Offset: 0x00017B28
			public string Value
			{
				get
				{
					return this.attrValue;
				}
				set
				{
					if (this.attrValue != null && this.attrValue.Length > this.upperBound)
					{
						throw new FormatException(string.Format(Locale.GetText("Value length bigger than upperbound ({0})."), this.upperBound));
					}
					this.attrValue = value;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600053E RID: 1342 RVA: 0x00019977 File Offset: 0x00017B77
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00019980 File Offset: 0x00017B80
			internal ASN1 GetASN1(byte encoding)
			{
				byte b = encoding;
				if (b == 255)
				{
					b = this.SelectBestEncoding();
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.oid));
				if (b != 19)
				{
					if (b != 22)
					{
						if (b == 30)
						{
							asn.Add(new ASN1(30, Encoding.BigEndianUnicode.GetBytes(this.attrValue)));
						}
					}
					else
					{
						asn.Add(new ASN1(22, Encoding.ASCII.GetBytes(this.attrValue)));
					}
				}
				else
				{
					asn.Add(new ASN1(19, Encoding.ASCII.GetBytes(this.attrValue)));
				}
				return asn;
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x00019A28 File Offset: 0x00017C28
			internal ASN1 GetASN1()
			{
				return this.GetASN1(this.encoding);
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x00019A36 File Offset: 0x00017C36
			public byte[] GetBytes(byte encoding)
			{
				return this.GetASN1(encoding).GetBytes();
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x00019A44 File Offset: 0x00017C44
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x00019A54 File Offset: 0x00017C54
			private byte SelectBestEncoding()
			{
				foreach (char c in this.attrValue)
				{
					if (c == '@' || c == '_')
					{
						return 30;
					}
					if (c > '\u007f')
					{
						return 30;
					}
				}
				return 19;
			}

			// Token: 0x040003D6 RID: 982
			private string oid;

			// Token: 0x040003D7 RID: 983
			private string attrValue;

			// Token: 0x040003D8 RID: 984
			private int upperBound;

			// Token: 0x040003D9 RID: 985
			private byte encoding;
		}

		// Token: 0x0200008A RID: 138
		public class Name : X520.AttributeTypeAndValue
		{
			// Token: 0x06000544 RID: 1348 RVA: 0x00019A98 File Offset: 0x00017C98
			public Name() : base("2.5.4.41", 32768)
			{
			}
		}

		// Token: 0x0200008B RID: 139
		public class CommonName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000545 RID: 1349 RVA: 0x00019AAA File Offset: 0x00017CAA
			public CommonName() : base("2.5.4.3", 64)
			{
			}
		}

		// Token: 0x0200008C RID: 140
		public class SerialNumber : X520.AttributeTypeAndValue
		{
			// Token: 0x06000546 RID: 1350 RVA: 0x00019AB9 File Offset: 0x00017CB9
			public SerialNumber() : base("2.5.4.5", 64, 19)
			{
			}
		}

		// Token: 0x0200008D RID: 141
		public class LocalityName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000547 RID: 1351 RVA: 0x00019ACA File Offset: 0x00017CCA
			public LocalityName() : base("2.5.4.7", 128)
			{
			}
		}

		// Token: 0x0200008E RID: 142
		public class StateOrProvinceName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000548 RID: 1352 RVA: 0x00019ADC File Offset: 0x00017CDC
			public StateOrProvinceName() : base("2.5.4.8", 128)
			{
			}
		}

		// Token: 0x0200008F RID: 143
		public class OrganizationName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000549 RID: 1353 RVA: 0x00019AEE File Offset: 0x00017CEE
			public OrganizationName() : base("2.5.4.10", 64)
			{
			}
		}

		// Token: 0x02000090 RID: 144
		public class OrganizationalUnitName : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054A RID: 1354 RVA: 0x00019AFD File Offset: 0x00017CFD
			public OrganizationalUnitName() : base("2.5.4.11", 64)
			{
			}
		}

		// Token: 0x02000091 RID: 145
		public class EmailAddress : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054B RID: 1355 RVA: 0x00019B0C File Offset: 0x00017D0C
			public EmailAddress() : base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		// Token: 0x02000092 RID: 146
		public class DomainComponent : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054C RID: 1356 RVA: 0x00019B20 File Offset: 0x00017D20
			public DomainComponent() : base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		// Token: 0x02000093 RID: 147
		public class UserId : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054D RID: 1357 RVA: 0x00019B34 File Offset: 0x00017D34
			public UserId() : base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		// Token: 0x02000094 RID: 148
		public class Oid : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054E RID: 1358 RVA: 0x00019B46 File Offset: 0x00017D46
			public Oid(string oid) : base(oid, int.MaxValue)
			{
			}
		}

		// Token: 0x02000095 RID: 149
		public class Title : X520.AttributeTypeAndValue
		{
			// Token: 0x0600054F RID: 1359 RVA: 0x00019B54 File Offset: 0x00017D54
			public Title() : base("2.5.4.12", 64)
			{
			}
		}

		// Token: 0x02000096 RID: 150
		public class CountryName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000550 RID: 1360 RVA: 0x00019B63 File Offset: 0x00017D63
			public CountryName() : base("2.5.4.6", 2, 19)
			{
			}
		}

		// Token: 0x02000097 RID: 151
		public class DnQualifier : X520.AttributeTypeAndValue
		{
			// Token: 0x06000551 RID: 1361 RVA: 0x00019B73 File Offset: 0x00017D73
			public DnQualifier() : base("2.5.4.46", 2, 19)
			{
			}
		}

		// Token: 0x02000098 RID: 152
		public class Surname : X520.AttributeTypeAndValue
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x00019B83 File Offset: 0x00017D83
			public Surname() : base("2.5.4.4", 32768)
			{
			}
		}

		// Token: 0x02000099 RID: 153
		public class GivenName : X520.AttributeTypeAndValue
		{
			// Token: 0x06000553 RID: 1363 RVA: 0x00019B95 File Offset: 0x00017D95
			public GivenName() : base("2.5.4.42", 16)
			{
			}
		}

		// Token: 0x0200009A RID: 154
		public class Initial : X520.AttributeTypeAndValue
		{
			// Token: 0x06000554 RID: 1364 RVA: 0x00019BA4 File Offset: 0x00017DA4
			public Initial() : base("2.5.4.43", 5)
			{
			}
		}
	}
}
