using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200039C RID: 924
	internal sealed class PrefixQName
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x000E2CE9 File Offset: 0x000E0EE9
		internal void ClearPrefix()
		{
			this.Prefix = string.Empty;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000E2CF6 File Offset: 0x000E0EF6
		internal void SetQName(string qname)
		{
			PrefixQName.ParseQualifiedName(qname, out this.Prefix, out this.Name);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000E2D0C File Offset: 0x000E0F0C
		public static void ParseQualifiedName(string qname, out string prefix, out string local)
		{
			prefix = string.Empty;
			local = string.Empty;
			int num = ValidateNames.ParseNCName(qname);
			if (num == 0)
			{
				throw XsltException.Create("'{0}' is an invalid QName.", new string[]
				{
					qname
				});
			}
			local = qname.Substring(0, num);
			if (num < qname.Length)
			{
				if (qname[num] == ':')
				{
					int startIndex;
					num = (startIndex = num + 1);
					prefix = local;
					int num2 = ValidateNames.ParseNCName(qname, num);
					num += num2;
					if (num2 == 0)
					{
						throw XsltException.Create("'{0}' is an invalid QName.", new string[]
						{
							qname
						});
					}
					local = qname.Substring(startIndex, num2);
				}
				if (num < qname.Length)
				{
					throw XsltException.Create("'{0}' is an invalid QName.", new string[]
					{
						qname
					});
				}
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000E2DBA File Offset: 0x000E0FBA
		public static bool ValidatePrefix(string prefix)
		{
			return prefix.Length != 0 && ValidateNames.ParseNCName(prefix, 0) == prefix.Length;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0000216B File Offset: 0x0000036B
		public PrefixQName()
		{
		}

		// Token: 0x04001D76 RID: 7542
		public string Prefix;

		// Token: 0x04001D77 RID: 7543
		public string Name;

		// Token: 0x04001D78 RID: 7544
		public string Namespace;
	}
}
