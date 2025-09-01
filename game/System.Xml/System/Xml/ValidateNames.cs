using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000222 RID: 546
	internal static class ValidateNames
	{
		// Token: 0x06001439 RID: 5177 RVA: 0x0007FE34 File Offset: 0x0007E034
		internal static int ParseNmtoken(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0007FE6C File Offset: 0x0007E06C
		internal static int ParseNmtokenNoNamespaces(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0007FEB0 File Offset: 0x0007E0B0
		internal static bool IsNmtokenNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNmtokenNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0007FED4 File Offset: 0x0007E0D4
		internal static int ParseNameNoNamespaces(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0 && s[num] != ':')
				{
					return 0;
				}
				num++;
				while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0 || s[num] == ':'))
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0007FF48 File Offset: 0x0007E148
		internal static bool IsNameNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNameNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0007FF6C File Offset: 0x0007E16C
		internal static int ParseNCName(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[(int)s[num]] & 4) == 0)
				{
					return 0;
				}
				num++;
				while (num < s.Length && (ValidateNames.xmlCharType.charProperties[(int)s[num]] & 8) != 0)
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0007FFC8 File Offset: 0x0007E1C8
		internal static int ParseNCName(string s)
		{
			return ValidateNames.ParseNCName(s, 0);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0007FFD1 File Offset: 0x0007E1D1
		internal static string ParseNCNameThrow(string s)
		{
			ValidateNames.ParseNCNameInternal(s, true);
			return s;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0007FFDC File Offset: 0x0007E1DC
		private static bool ParseNCNameInternal(string s, bool throwOnError)
		{
			int num = ValidateNames.ParseNCName(s, 0);
			if (num == 0 || num != s.Length)
			{
				if (throwOnError)
				{
					ValidateNames.ThrowInvalidName(s, 0, num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0008000C File Offset: 0x0007E20C
		internal static int ParseQName(string s, int offset, out int colonOffset)
		{
			colonOffset = 0;
			int num = ValidateNames.ParseNCName(s, offset);
			if (num != 0)
			{
				offset += num;
				if (offset < s.Length && s[offset] == ':')
				{
					int num2 = ValidateNames.ParseNCName(s, offset + 1);
					if (num2 != 0)
					{
						colonOffset = offset;
						num += num2 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00080058 File Offset: 0x0007E258
		internal static void ParseQNameThrow(string s, out string prefix, out string localName)
		{
			int num2;
			int num = ValidateNames.ParseQName(s, 0, out num2);
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
			if (num2 != 0)
			{
				prefix = s.Substring(0, num2);
				localName = s.Substring(num2 + 1);
				return;
			}
			prefix = "";
			localName = s;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000800A8 File Offset: 0x0007E2A8
		internal static void ParseNameTestThrow(string s, out string prefix, out string localName)
		{
			int num;
			if (s.Length != 0 && s[0] == '*')
			{
				string text;
				localName = (text = null);
				prefix = text;
				num = 1;
			}
			else
			{
				num = ValidateNames.ParseNCName(s, 0);
				if (num != 0)
				{
					localName = s.Substring(0, num);
					if (num < s.Length && s[num] == ':')
					{
						prefix = localName;
						int num2 = num + 1;
						if (num2 < s.Length && s[num2] == '*')
						{
							localName = null;
							num += 2;
						}
						else
						{
							int num3 = ValidateNames.ParseNCName(s, num2);
							if (num3 != 0)
							{
								localName = s.Substring(num2, num3);
								num += num3 + 1;
							}
						}
					}
					else
					{
						prefix = string.Empty;
					}
				}
				else
				{
					string text;
					localName = (text = null);
					prefix = text;
				}
			}
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00080164 File Offset: 0x0007E364
		internal static void ThrowInvalidName(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				throw new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.Instance.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				throw new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			throw new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000801D8 File Offset: 0x0007E3D8
		internal static Exception GetInvalidNameException(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !ValidateNames.xmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				return new XmlException("Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00080248 File Offset: 0x0007E448
		internal static bool StartsWithXml(string s)
		{
			return s.Length >= 3 && (s[0] == 'x' || s[0] == 'X') && (s[1] == 'm' || s[1] == 'M') && (s[2] == 'l' || s[2] == 'L');
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000802A9 File Offset: 0x0007E4A9
		internal static bool IsReservedNamespace(string s)
		{
			return s.Equals("http://www.w3.org/XML/1998/namespace") || s.Equals("http://www.w3.org/2000/xmlns/");
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000802C5 File Offset: 0x0007E4C5
		internal static void ValidateNameThrow(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, true);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000802D4 File Offset: 0x0007E4D4
		internal static bool ValidateName(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			return ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, false);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000802E4 File Offset: 0x0007E4E4
		private static bool ValidateNameInternal(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags, bool throwOnError)
		{
			if ((flags & ValidateNames.Flags.NCNames) != (ValidateNames.Flags)0)
			{
				if (prefix.Length != 0 && !ValidateNames.ParseNCNameInternal(prefix, throwOnError))
				{
					return false;
				}
				if (localName.Length != 0 && !ValidateNames.ParseNCNameInternal(localName, throwOnError))
				{
					return false;
				}
			}
			if ((flags & ValidateNames.Flags.CheckLocalName) != (ValidateNames.Flags)0)
			{
				if (nodeKind != XPathNodeType.Element)
				{
					if (nodeKind != XPathNodeType.Attribute)
					{
						if (nodeKind != XPathNodeType.ProcessingInstruction)
						{
							if (localName.Length == 0)
							{
								goto IL_FA;
							}
							if (throwOnError)
							{
								throw new XmlException("A node of type '{0}' cannot have a name.", nodeKind.ToString());
							}
							return false;
						}
						else
						{
							if (localName.Length != 0 && (localName.Length != 3 || !ValidateNames.StartsWithXml(localName)))
							{
								goto IL_FA;
							}
							if (throwOnError)
							{
								throw new XmlException("'{0}' is an invalid name for processing instructions.", localName);
							}
							return false;
						}
					}
					else if (ns.Length == 0 && localName.Equals("xmlns"))
					{
						if (throwOnError)
						{
							throw new XmlException("A node of type '{0}' cannot have the name '{1}'.", new string[]
							{
								nodeKind.ToString(),
								localName
							});
						}
						return false;
					}
				}
				if (localName.Length == 0)
				{
					if (throwOnError)
					{
						throw new XmlException("The local name for elements or attributes cannot be null or an empty string.", string.Empty);
					}
					return false;
				}
			}
			IL_FA:
			if ((flags & ValidateNames.Flags.CheckPrefixMapping) != (ValidateNames.Flags)0)
			{
				if (nodeKind - XPathNodeType.Element > 2)
				{
					if (nodeKind != XPathNodeType.ProcessingInstruction)
					{
						if (prefix.Length != 0 || ns.Length != 0)
						{
							if (throwOnError)
							{
								throw new XmlException("A node of type '{0}' cannot have a name.", nodeKind.ToString());
							}
							return false;
						}
					}
					else if (prefix.Length != 0 || ns.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("'{0}' is an invalid name for processing instructions.", ValidateNames.CreateName(prefix, localName));
						}
						return false;
					}
				}
				else if (ns.Length == 0)
				{
					if (prefix.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("Cannot use a prefix with an empty namespace.", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Length == 0 && nodeKind == XPathNodeType.Attribute)
				{
					if (throwOnError)
					{
						throw new XmlException("A node of type '{0}' cannot have the name '{1}'.", new string[]
						{
							nodeKind.ToString(),
							localName
						});
					}
					return false;
				}
				else if (prefix.Equals("xml"))
				{
					if (!ns.Equals("http://www.w3.org/XML/1998/namespace"))
					{
						if (throwOnError)
						{
							throw new XmlException("Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\".", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Equals("xmlns"))
				{
					if (throwOnError)
					{
						throw new XmlException("Prefix \"xmlns\" is reserved for use by XML.", string.Empty);
					}
					return false;
				}
				else if (ValidateNames.IsReservedNamespace(ns))
				{
					if (throwOnError)
					{
						throw new XmlException("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", string.Empty);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00080531 File Offset: 0x0007E731
		private static string CreateName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0008054C File Offset: 0x0007E74C
		internal static void SplitQName(string name, out string prefix, out string lname)
		{
			int num = name.IndexOf(':');
			if (-1 == num)
			{
				prefix = string.Empty;
				lname = name;
				return;
			}
			if (num == 0 || name.Length - 1 == num)
			{
				string name2 = "The '{0}' character, hexadecimal value {1}, cannot be included in a name.";
				object[] args = XmlException.BuildCharExceptionArgs(':', '\0');
				throw new ArgumentException(Res.GetString(name2, args), "name");
			}
			prefix = name.Substring(0, num);
			num++;
			lname = name.Substring(num, name.Length - num);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000805BD File Offset: 0x0007E7BD
		// Note: this type is marked as 'beforefieldinit'.
		static ValidateNames()
		{
		}

		// Token: 0x0400128C RID: 4748
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000223 RID: 547
		internal enum Flags
		{
			// Token: 0x0400128E RID: 4750
			NCNames = 1,
			// Token: 0x0400128F RID: 4751
			CheckLocalName,
			// Token: 0x04001290 RID: 4752
			CheckPrefixMapping = 4,
			// Token: 0x04001291 RID: 4753
			All = 7,
			// Token: 0x04001292 RID: 4754
			AllExceptNCNames = 6,
			// Token: 0x04001293 RID: 4755
			AllExceptPrefixMapping = 3
		}
	}
}
