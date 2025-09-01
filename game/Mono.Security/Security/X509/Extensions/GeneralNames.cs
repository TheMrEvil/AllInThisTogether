using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000023 RID: 35
	internal class GeneralNames
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public GeneralNames()
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C204 File Offset: 0x0000A404
		public GeneralNames(string[] rfc822s, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			this.asn = new ASN1(48);
			if (rfc822s != null)
			{
				this.rfc822Name = new ArrayList();
				foreach (string s in rfc822s)
				{
					this.asn.Add(new ASN1(129, Encoding.ASCII.GetBytes(s)));
					this.rfc822Name.Add(rfc822s);
				}
			}
			if (dnsNames != null)
			{
				this.dnsName = new ArrayList();
				foreach (string text in dnsNames)
				{
					this.asn.Add(new ASN1(130, Encoding.ASCII.GetBytes(text)));
					this.dnsName.Add(text);
				}
			}
			if (ipAddresses != null)
			{
				this.ipAddr = new ArrayList();
				foreach (string text2 in ipAddresses)
				{
					string[] array = text2.Split(new char[]
					{
						'.',
						':'
					});
					byte[] array2 = new byte[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array2[j] = byte.Parse(array[j]);
					}
					this.asn.Add(new ASN1(135, array2));
					this.ipAddr.Add(text2);
				}
			}
			if (uris != null)
			{
				this.uris = new ArrayList();
				foreach (string text3 in uris)
				{
					this.asn.Add(new ASN1(134, Encoding.ASCII.GetBytes(text3)));
					this.uris.Add(text3);
				}
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public GeneralNames(ASN1 sequence)
		{
			int i = 0;
			while (i < sequence.Count)
			{
				byte tag = sequence[i].Tag;
				switch (tag)
				{
				case 129:
					if (this.rfc822Name == null)
					{
						this.rfc822Name = new ArrayList();
					}
					this.rfc822Name.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 130:
					if (this.dnsName == null)
					{
						this.dnsName = new ArrayList();
					}
					this.dnsName.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 131:
				case 133:
					break;
				case 132:
					goto IL_C6;
				case 134:
					if (this.uris == null)
					{
						this.uris = new ArrayList();
					}
					this.uris.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 135:
				{
					if (this.ipAddr == null)
					{
						this.ipAddr = new ArrayList();
					}
					byte[] value = sequence[i].Value;
					string value2 = (value.Length == 4) ? "." : ":";
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 0; j < value.Length; j++)
					{
						stringBuilder.Append(value[j].ToString());
						if (j < value.Length - 1)
						{
							stringBuilder.Append(value2);
						}
					}
					this.ipAddr.Add(stringBuilder.ToString());
					if (this.ipAddr == null)
					{
						this.ipAddr = new ArrayList();
					}
					break;
				}
				default:
					if (tag == 164)
					{
						goto IL_C6;
					}
					break;
				}
				IL_1CB:
				i++;
				continue;
				IL_C6:
				if (this.directoryNames == null)
				{
					this.directoryNames = new ArrayList();
				}
				this.directoryNames.Add(X501.ToString(sequence[i][0]));
				goto IL_1CB;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000C594 File Offset: 0x0000A794
		public string[] RFC822
		{
			get
			{
				if (this.rfc822Name == null)
				{
					return new string[0];
				}
				return (string[])this.rfc822Name.ToArray(typeof(string));
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		public string[] DirectoryNames
		{
			get
			{
				if (this.directoryNames == null)
				{
					return new string[0];
				}
				return (string[])this.directoryNames.ToArray(typeof(string));
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000C5EA File Offset: 0x0000A7EA
		public string[] DNSNames
		{
			get
			{
				if (this.dnsName == null)
				{
					return new string[0];
				}
				return (string[])this.dnsName.ToArray(typeof(string));
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000C615 File Offset: 0x0000A815
		public string[] UniformResourceIdentifiers
		{
			get
			{
				if (this.uris == null)
				{
					return new string[0];
				}
				return (string[])this.uris.ToArray(typeof(string));
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000C640 File Offset: 0x0000A840
		public string[] IPAddresses
		{
			get
			{
				if (this.ipAddr == null)
				{
					return new string[0];
				}
				return (string[])this.ipAddr.ToArray(typeof(string));
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000C66B File Offset: 0x0000A86B
		public byte[] GetBytes()
		{
			return this.asn.GetBytes();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C678 File Offset: 0x0000A878
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.rfc822Name != null)
			{
				foreach (object obj in this.rfc822Name)
				{
					string value = (string)obj;
					stringBuilder.Append("RFC822 Name=");
					stringBuilder.Append(value);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.dnsName != null)
			{
				foreach (object obj2 in this.dnsName)
				{
					string value2 = (string)obj2;
					stringBuilder.Append("DNS Name=");
					stringBuilder.Append(value2);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.directoryNames != null)
			{
				foreach (object obj3 in this.directoryNames)
				{
					string value3 = (string)obj3;
					stringBuilder.Append("Directory Address: ");
					stringBuilder.Append(value3);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.uris != null)
			{
				foreach (object obj4 in this.uris)
				{
					string value4 = (string)obj4;
					stringBuilder.Append("URL=");
					stringBuilder.Append(value4);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.ipAddr != null)
			{
				foreach (object obj5 in this.ipAddr)
				{
					string value5 = (string)obj5;
					stringBuilder.Append("IP Address=");
					stringBuilder.Append(value5);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DE RID: 222
		private ArrayList rfc822Name;

		// Token: 0x040000DF RID: 223
		private ArrayList dnsName;

		// Token: 0x040000E0 RID: 224
		private ArrayList directoryNames;

		// Token: 0x040000E1 RID: 225
		private ArrayList uris;

		// Token: 0x040000E2 RID: 226
		private ArrayList ipAddr;

		// Token: 0x040000E3 RID: 227
		private ASN1 asn;
	}
}
