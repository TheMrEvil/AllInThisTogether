using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x0200036A RID: 874
	internal class DbConnectionOptions
	{
		// Token: 0x06002901 RID: 10497 RVA: 0x000B3695 File Offset: 0x000B1895
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000B36A0 File Offset: 0x000B18A0
		private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
		{
			string usersConnectionString = this._usersConnectionString;
			if (this._hasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
			{
				this.ReplacePasswordPwd(out usersConnectionString, false);
			}
			return usersConnectionString ?? string.Empty;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000B36DE File Offset: 0x000B18DE
		internal bool HasPersistablePassword
		{
			get
			{
				return !this._hasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000B36F8 File Offset: 0x000B18F8
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			string stringValue;
			if (!this._parsetable.TryGetValue(keyName, out stringValue))
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, stringValue);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000B3720 File Offset: 0x000B1920
		internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string strvalue = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue(keyName);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000B37AA File Offset: 0x000B19AA
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst) == 0;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000B37BC File Offset: 0x000B19BC
		[Conditional("DEBUG")]
		[Conditional("DEBUG")]
		private static void DebugTraceKeyValuePair(string keyname, string keyvalue, Dictionary<string, string> synonyms)
		{
			string b = (synonyms != null) ? synonyms[keyname] : keyname;
			if ("password" != b && "pwd" != b)
			{
				if (keyvalue != null)
				{
					DataCommonEventSource.Log.Trace<string, string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}', KeyValue='{1}'", keyname, keyvalue);
					return;
				}
				DataCommonEventSource.Log.Trace<string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}'", keyname);
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000B3818 File Offset: 0x000B1A18
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000B3858 File Offset: 0x000B1A58
		private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
		{
			int num = buffer.Length;
			int i = 0;
			if (trimWhitespace)
			{
				while (i < num)
				{
					if (!char.IsWhiteSpace(buffer[i]))
					{
						break;
					}
					i++;
				}
				while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
				{
					num--;
				}
			}
			return buffer.ToString(i, num - i);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000B38B0 File Offset: 0x000B1AB0
		internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
		{
			int index = currentPosition;
			buffer.Length = 0;
			keyname = null;
			keyvalue = null;
			char c = '\0';
			DbConnectionOptions.ParserState parserState = DbConnectionOptions.ParserState.NothingYet;
			int length = connectionString.Length;
			while (currentPosition < length)
			{
				c = connectionString[currentPosition];
				switch (parserState)
				{
				case DbConnectionOptions.ParserState.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						if (c == '\0')
						{
							parserState = DbConnectionOptions.ParserState.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw ADP.ConnectionStringSyntax(index);
							}
							index = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_248;
							}
							parserState = DbConnectionOptions.ParserState.KeyEqual;
						}
					}
					break;
				case DbConnectionOptions.ParserState.Key:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_248;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_248;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_107;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_107;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_248;
					}
					if (char.IsControl(c))
					{
						goto IL_25C;
					}
					if (';' == c)
					{
						goto IL_25C;
					}
					goto IL_248;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_248;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_212;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_248;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_212;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_248;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					goto IL_248;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_212;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_212;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_250:
				currentPosition++;
				continue;
				IL_107:
				if (char.IsWhiteSpace(c))
				{
					goto IL_250;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_248;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_250;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_250;
					}
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_248;
				IL_212:
				if (char.IsWhiteSpace(c))
				{
					goto IL_250;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_250;
				}
				throw ADP.ConnectionStringSyntax(index);
				IL_248:
				buffer.Append(c);
				goto IL_250;
			}
			IL_25C:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
			case DbConnectionOptions.ParserState.BraceQuoteValue:
				throw ADP.ConnectionStringSyntax(index);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if (!useOdbcRules && ('\'' == c2 || '"' == c2))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000B3BD4 File Offset: 0x000B1DD4
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000B3BE5 File Offset: 0x000B1DE5
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000B3C1C File Offset: 0x000B1E1C
		private static NameValuePair ParseInternal(Dictionary<string, string> parsetable, string connectionString, bool buildChain, Dictionary<string, string> synonyms, bool firstKey)
		{
			StringBuilder buffer = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair result = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string value;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, buffer, firstKey, out text, out value);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string text3;
				string text2 = (synonyms != null) ? (synonyms.TryGetValue(text, out text3) ? text3 : null) : text;
				if (!DbConnectionOptions.IsKeyNameValid(text2))
				{
					throw ADP.KeywordNotSupported(text);
				}
				if (!firstKey || !parsetable.ContainsKey(text2))
				{
					parsetable[text2] = value;
				}
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text2, value, i - num));
				}
				else if (buildChain)
				{
					nameValuePair = (result = new NameValuePair(text2, value, i - num));
				}
			}
			return result;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000B3CDC File Offset: 0x000B1EDC
		internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
		{
			int num = 0;
			NameValuePair result = null;
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair3 = this._keyChain; nameValuePair3 != null; nameValuePair3 = nameValuePair3.Next)
			{
				if ("password" != nameValuePair3.Name && "pwd" != nameValuePair3.Name)
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair3.Length);
					if (fakePassword)
					{
						nameValuePair2 = new NameValuePair(nameValuePair3.Name, nameValuePair3.Value, nameValuePair3.Length);
					}
				}
				else if (fakePassword)
				{
					stringBuilder.Append(nameValuePair3.Name).Append("=*;");
					nameValuePair2 = new NameValuePair(nameValuePair3.Name, "*", nameValuePair3.Name.Length + "=*;".Length);
				}
				if (fakePassword)
				{
					if (nameValuePair != null)
					{
						nameValuePair = (nameValuePair.Next = nameValuePair2);
					}
					else
					{
						result = (nameValuePair = nameValuePair2);
					}
				}
				num += nameValuePair3.Length;
			}
			constr = stringBuilder.ToString();
			return result;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000B3DF0 File Offset: 0x000B1FF0
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms, bool useOdbcRules)
		{
			this._useOdbcRules = useOdbcRules;
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, this._useOdbcRules);
				this._hasPasswordKeyword = (this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd"));
				this._hasUserIdKeyword = (this._parsetable.ContainsKey("user id") || this._parsetable.ContainsKey("uid"));
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000B3EA9 File Offset: 0x000B20A9
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x170006D3 RID: 1747
		public string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000B3EC0 File Offset: 0x000B20C0
		internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue, bool useOdbcRules)
		{
			ADP.CheckArgumentNull(builder, "builder");
			ADP.CheckArgumentLength(keyName, "keyName");
			if (keyName == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyName))
			{
				throw ADP.InvalidKeyname(keyName);
			}
			if (keyValue != null && !DbConnectionOptions.IsValueValidInternal(keyValue))
			{
				throw ADP.InvalidValue(keyName);
			}
			if (0 < builder.Length && ';' != builder[builder.Length - 1])
			{
				builder.Append(';');
			}
			if (useOdbcRules)
			{
				builder.Append(keyName);
			}
			else
			{
				builder.Append(keyName.Replace("=", "=="));
			}
			builder.Append('=');
			if (keyValue != null)
			{
				if (useOdbcRules)
				{
					if (0 < keyValue.Length && ('{' == keyValue[0] || 0 <= keyValue.IndexOf(';') || string.Compare("Driver", keyName, StringComparison.OrdinalIgnoreCase) == 0) && !DbConnectionOptions.s_connectionStringQuoteOdbcValueRegex.IsMatch(keyValue))
					{
						builder.Append('{').Append(keyValue.Replace("}", "}}")).Append('}');
						return;
					}
					builder.Append(keyValue);
					return;
				}
				else
				{
					if (DbConnectionOptions.s_connectionStringQuoteValueRegex.IsMatch(keyValue))
					{
						builder.Append(keyValue);
						return;
					}
					if (-1 != keyValue.IndexOf('"') && -1 == keyValue.IndexOf('\''))
					{
						builder.Append('\'');
						builder.Append(keyValue);
						builder.Append('\'');
						return;
					}
					builder.Append('"');
					builder.Append(keyValue.Replace("\"", "\"\""));
					builder.Append('"');
				}
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000B403F File Offset: 0x000B223F
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000B4048 File Offset: 0x000B2248
		internal string ExpandKeyword(string keyword, string replacementValue)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				if (nameValuePair.Name == keyword && nameValuePair.Value == this[keyword])
				{
					DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, replacementValue, this._useOdbcRules);
					stringBuilder.Append(';');
					flag = true;
				}
				else
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
				}
				num += nameValuePair.Length;
			}
			if (!flag)
			{
				DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, keyword, replacementValue, this._useOdbcRules);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000B40F3 File Offset: 0x000B22F3
		internal static void ValidateKeyValuePair(string keyword, string value)
		{
			if (keyword == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyword))
			{
				throw ADP.InvalidKeyname(keyword);
			}
			if (value != null && !DbConnectionOptions.s_connectionStringValidValueRegex.IsMatch(value))
			{
				throw ADP.InvalidValue(keyword);
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000B4124 File Offset: 0x000B2324
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms)
		{
			this._parsetable = new Dictionary<string, string>();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, false);
				this._hasPasswordKeyword = (this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd"));
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000B41A6 File Offset: 0x000B23A6
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this._hasPasswordKeyword = connectionOptions._hasPasswordKeyword;
			this._parsetable = connectionOptions._parsetable;
			this._keyChain = connectionOptions._keyChain;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000B41DE File Offset: 0x000B23DE
		public bool IsEmpty
		{
			get
			{
				return this._keyChain == null;
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000B41E9 File Offset: 0x000B23E9
		internal bool TryGetParsetableValue(string key, out string value)
		{
			return this._parsetable.TryGetValue(key, out value);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000B41F8 File Offset: 0x000B23F8
		public bool ConvertValueToIntegratedSecurity()
		{
			string text;
			return this._parsetable.TryGetValue("integrated security", out text) && text != null && this.ConvertValueToIntegratedSecurityInternal(text);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000B4228 File Offset: 0x000B2428
		internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string strvalue = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue("integrated security");
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000B42D0 File Offset: 0x000B24D0
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, text);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000B42FC File Offset: 0x000B24FC
		internal static int ConvertToInt32Internal(string keyname, string stringValue)
		{
			int result;
			try
			{
				result = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, inner2);
			}
			return result;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000B4348 File Offset: 0x000B2548
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000B436B File Offset: 0x000B256B
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000B437C File Offset: 0x000B257C
		internal static string ExpandDataDirectory(string keyword, string value, ref string datadir)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = datadir;
				if (text2 == null)
				{
					object data = AppDomain.CurrentDomain.GetData("DataDirectory");
					text2 = (data as string);
					if (data != null && text2 == null)
					{
						throw ADP.InvalidDataDirectory();
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = AppDomain.CurrentDomain.BaseDirectory;
					}
					if (text2 == null)
					{
						text2 = "";
					}
					datadir = text2;
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + "\\" + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000B448C File Offset: 0x000B268C
		internal string ExpandDataDirectories(ref string filename, ref int position)
		{
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			string text = null;
			int num = 0;
			bool flag = false;
			string text2;
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				text2 = nameValuePair.Value;
				if (this._useOdbcRules)
				{
					string name = nameValuePair.Name;
					if (!(name == "driver") && !(name == "pwd") && !(name == "uid"))
					{
						text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
					}
				}
				else
				{
					string name = nameValuePair.Name;
					uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
					if (num2 <= 2781420622U)
					{
						if (num2 <= 1433271620U)
						{
							if (num2 != 910909208U)
							{
								if (num2 == 1433271620U)
								{
									if (name == "pwd")
									{
										goto IL_1AB;
									}
								}
							}
							else if (name == "password")
							{
								goto IL_1AB;
							}
						}
						else if (num2 != 1556604621U)
						{
							if (num2 == 2781420622U)
							{
								if (name == "data provider")
								{
									goto IL_1AB;
								}
							}
						}
						else if (name == "uid")
						{
							goto IL_1AB;
						}
					}
					else if (num2 <= 3082861500U)
					{
						if (num2 != 2906666283U)
						{
							if (num2 == 3082861500U)
							{
								if (name == "provider")
								{
									goto IL_1AB;
								}
							}
						}
						else if (name == "user id")
						{
							goto IL_1AB;
						}
					}
					else if (num2 != 4008387664U)
					{
						if (num2 == 4015305829U)
						{
							if (name == "extended properties")
							{
								goto IL_1AB;
							}
						}
					}
					else if (name == "remote provider")
					{
						goto IL_1AB;
					}
					text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
				}
				IL_1AB:
				if (text2 == null)
				{
					text2 = nameValuePair.Value;
				}
				if (this._useOdbcRules || "file name" != nameValuePair.Name)
				{
					if (text2 != nameValuePair.Value)
					{
						flag = true;
						DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, text2, this._useOdbcRules);
						stringBuilder.Append(';');
					}
					else
					{
						stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
					}
				}
				else
				{
					flag = true;
					filename = text2;
					position = stringBuilder.Length;
				}
				num += nameValuePair.Length;
			}
			if (flag)
			{
				text2 = stringBuilder.ToString();
			}
			else
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000B46EC File Offset: 0x000B28EC
		internal bool HasBlankPassword
		{
			get
			{
				if (this.ConvertValueToIntegratedSecurity())
				{
					return false;
				}
				if (this._parsetable.ContainsKey("password"))
				{
					return ADP.IsEmpty(this._parsetable["password"]);
				}
				if (this._parsetable.ContainsKey("pwd"))
				{
					return ADP.IsEmpty(this._parsetable["pwd"]);
				}
				return (this._parsetable.ContainsKey("user id") && !ADP.IsEmpty(this._parsetable["user id"])) || (this._parsetable.ContainsKey("uid") && !ADP.IsEmpty(this._parsetable["uid"]));
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000B47B0 File Offset: 0x000B29B0
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionOptions()
		{
		}

		// Token: 0x04001A2E RID: 6702
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x04001A2F RID: 6703
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x04001A30 RID: 6704
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x04001A31 RID: 6705
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x04001A32 RID: 6706
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04001A33 RID: 6707
		private static readonly Regex s_connectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x04001A34 RID: 6708
		private static readonly Regex s_connectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x04001A35 RID: 6709
		private static readonly Regex s_connectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x04001A36 RID: 6710
		private static readonly Regex s_connectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001A37 RID: 6711
		private readonly string _usersConnectionString;

		// Token: 0x04001A38 RID: 6712
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x04001A39 RID: 6713
		internal readonly NameValuePair _keyChain;

		// Token: 0x04001A3A RID: 6714
		internal readonly bool _hasPasswordKeyword;

		// Token: 0x04001A3B RID: 6715
		internal readonly bool _useOdbcRules;

		// Token: 0x04001A3C RID: 6716
		internal readonly bool _hasUserIdKeyword;

		// Token: 0x0200036B RID: 875
		private static class KEY
		{
			// Token: 0x04001A3D RID: 6717
			internal const string Integrated_Security = "integrated security";

			// Token: 0x04001A3E RID: 6718
			internal const string Password = "password";

			// Token: 0x04001A3F RID: 6719
			internal const string Persist_Security_Info = "persist security info";

			// Token: 0x04001A40 RID: 6720
			internal const string User_ID = "user id";
		}

		// Token: 0x0200036C RID: 876
		private static class SYNONYM
		{
			// Token: 0x04001A41 RID: 6721
			internal const string Pwd = "pwd";

			// Token: 0x04001A42 RID: 6722
			internal const string UID = "uid";
		}

		// Token: 0x0200036D RID: 877
		private enum ParserState
		{
			// Token: 0x04001A44 RID: 6724
			NothingYet = 1,
			// Token: 0x04001A45 RID: 6725
			Key,
			// Token: 0x04001A46 RID: 6726
			KeyEqual,
			// Token: 0x04001A47 RID: 6727
			KeyEnd,
			// Token: 0x04001A48 RID: 6728
			UnquotedValue,
			// Token: 0x04001A49 RID: 6729
			DoubleQuoteValue,
			// Token: 0x04001A4A RID: 6730
			DoubleQuoteValueQuote,
			// Token: 0x04001A4B RID: 6731
			SingleQuoteValue,
			// Token: 0x04001A4C RID: 6732
			SingleQuoteValueQuote,
			// Token: 0x04001A4D RID: 6733
			BraceQuoteValue,
			// Token: 0x04001A4E RID: 6734
			BraceQuoteValueQuote,
			// Token: 0x04001A4F RID: 6735
			QuotedValueEnd,
			// Token: 0x04001A50 RID: 6736
			NullTermination
		}
	}
}
