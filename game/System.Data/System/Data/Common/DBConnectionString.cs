using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x020003C6 RID: 966
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06002EE3 RID: 12003 RVA: 0x000C9292 File Offset: 0x000C7492
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool useOdbcRules) : this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000C92A9 File Offset: 0x000C74A9
		internal DBConnectionString(DbConnectionOptions connectionOptions) : this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000C92B8 File Offset: 0x000C74B8
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool mustCloneDictionary)
		{
			if (behavior <= KeyRestrictionBehavior.PreventUsage)
			{
				this._behavior = behavior;
				this._encryptedUsersConnectionString = connectionOptions.UsersConnectionString(false);
				this._hasPassword = connectionOptions._hasPasswordKeyword;
				this._parsetable = connectionOptions.Parsetable;
				this._keychain = connectionOptions._keyChain;
				if (this._hasPassword && !connectionOptions.HasPersistablePassword)
				{
					if (mustCloneDictionary)
					{
						this._parsetable = new Dictionary<string, string>(this._parsetable);
					}
					if (this._parsetable.ContainsKey("password"))
					{
						this._parsetable["password"] = "*";
					}
					if (this._parsetable.ContainsKey("pwd"))
					{
						this._parsetable["pwd"] = "*";
					}
					this._keychain = connectionOptions.ReplacePasswordPwd(out this._encryptedUsersConnectionString, true);
				}
				if (!string.IsNullOrEmpty(restrictions))
				{
					this._restrictionValues = DBConnectionString.ParseRestrictions(restrictions, synonyms);
					this._restrictions = restrictions;
				}
				return;
			}
			throw ADP.InvalidKeyRestrictionBehavior(behavior);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000C93B4 File Offset: 0x000C75B4
		private DBConnectionString(DBConnectionString connectionString, string[] restrictionValues, KeyRestrictionBehavior behavior)
		{
			this._encryptedUsersConnectionString = connectionString._encryptedUsersConnectionString;
			this._parsetable = connectionString._parsetable;
			this._keychain = connectionString._keychain;
			this._hasPassword = connectionString._hasPassword;
			this._restrictionValues = restrictionValues;
			this._restrictions = null;
			this._behavior = behavior;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000C940C File Offset: 0x000C760C
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000C9414 File Offset: 0x000C7614
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000C941C File Offset: 0x000C761C
		internal bool IsEmpty
		{
			get
			{
				return this._keychain == null;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000C9427 File Offset: 0x000C7627
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000C9430 File Offset: 0x000C7630
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && restrictionValues.Length != 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							if (!string.IsNullOrEmpty(restrictionValues[i]))
							{
								stringBuilder.Append(restrictionValues[i]);
								stringBuilder.Append("=;");
							}
						}
						text = stringBuilder.ToString();
					}
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		// Token: 0x170007C9 RID: 1993
		internal string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000C94A8 File Offset: 0x000C76A8
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000C94B8 File Offset: 0x000C76B8
		internal DBConnectionString Intersect(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			string[] restrictionValues = null;
			if (entry == null)
			{
				behavior = KeyRestrictionBehavior.AllowOnly;
			}
			else if (this._behavior != entry._behavior)
			{
				behavior = KeyRestrictionBehavior.AllowOnly;
				if (entry._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (!ADP.IsEmptyArray(this._restrictionValues))
					{
						if (!ADP.IsEmptyArray(entry._restrictionValues))
						{
							restrictionValues = DBConnectionString.NewRestrictionAllowOnly(entry._restrictionValues, this._restrictionValues);
						}
					}
					else
					{
						restrictionValues = entry._restrictionValues;
					}
				}
				else if (!ADP.IsEmptyArray(this._restrictionValues))
				{
					if (!ADP.IsEmptyArray(entry._restrictionValues))
					{
						restrictionValues = DBConnectionString.NewRestrictionAllowOnly(this._restrictionValues, entry._restrictionValues);
					}
					else
					{
						restrictionValues = this._restrictionValues;
					}
				}
			}
			else if (KeyRestrictionBehavior.PreventUsage == this._behavior)
			{
				if (ADP.IsEmptyArray(this._restrictionValues))
				{
					restrictionValues = entry._restrictionValues;
				}
				else if (ADP.IsEmptyArray(entry._restrictionValues))
				{
					restrictionValues = this._restrictionValues;
				}
				else
				{
					restrictionValues = DBConnectionString.NoDuplicateUnion(this._restrictionValues, entry._restrictionValues);
				}
			}
			else if (!ADP.IsEmptyArray(this._restrictionValues) && !ADP.IsEmptyArray(entry._restrictionValues))
			{
				if (this._restrictionValues.Length <= entry._restrictionValues.Length)
				{
					restrictionValues = DBConnectionString.NewRestrictionIntersect(this._restrictionValues, entry._restrictionValues);
				}
				else
				{
					restrictionValues = DBConnectionString.NewRestrictionIntersect(entry._restrictionValues, this._restrictionValues);
				}
			}
			return new DBConnectionString(this, restrictionValues, behavior);
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000C9618 File Offset: 0x000C7818
		[Conditional("DEBUG")]
		private void ValidateCombinedSet(DBConnectionString componentSet, DBConnectionString combinedSet)
		{
			if (componentSet != null && combinedSet._restrictionValues != null && componentSet._restrictionValues != null)
			{
				if (componentSet._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
					{
						KeyRestrictionBehavior behavior = combinedSet._behavior;
						return;
					}
				}
				else if (componentSet._behavior == KeyRestrictionBehavior.PreventUsage && combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
				{
					KeyRestrictionBehavior behavior2 = combinedSet._behavior;
				}
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000C966C File Offset: 0x000C786C
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000C968C File Offset: 0x000C788C
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			if (behavior != KeyRestrictionBehavior.AllowOnly)
			{
				if (behavior != KeyRestrictionBehavior.PreventUsage)
				{
					throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
				}
				if (this._restrictionValues != null)
				{
					foreach (string keyword in this._restrictionValues)
					{
						if (entry.ContainsKey(keyword))
						{
							return false;
						}
					}
				}
			}
			else
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000C971C File Offset: 0x000C791C
		private static string[] NewRestrictionAllowOnly(string[] allowonly, string[] preventusage)
		{
			List<string> list = null;
			for (int i = 0; i < allowonly.Length; i++)
			{
				if (0 > Array.BinarySearch<string>(preventusage, allowonly[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(allowonly[i]);
				}
			}
			string[] result = null;
			if (list != null)
			{
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000C976C File Offset: 0x000C796C
		private static string[] NewRestrictionIntersect(string[] a, string[] b)
		{
			List<string> list = null;
			for (int i = 0; i < a.Length; i++)
			{
				if (0 <= Array.BinarySearch<string>(b, a[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(a[i]);
				}
			}
			string[] result = null;
			if (list != null)
			{
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000C97BC File Offset: 0x000C79BC
		private static string[] NoDuplicateUnion(string[] a, string[] b)
		{
			List<string> list = new List<string>(a.Length + b.Length);
			for (int i = 0; i < a.Length; i++)
			{
				list.Add(a[i]);
			}
			for (int j = 0; j < b.Length; j++)
			{
				if (0 > Array.BinarySearch<string>(a, b[j], StringComparer.Ordinal))
				{
					list.Add(b[j]);
				}
			}
			string[] array = list.ToArray();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000C9828 File Offset: 0x000C7A28
		private static string[] ParseRestrictions(string restrictions, Dictionary<string, string> synonyms)
		{
			List<string> list = new List<string>();
			StringBuilder buffer = new StringBuilder(restrictions.Length);
			int i = 0;
			int length = restrictions.Length;
			while (i < length)
			{
				int currentPosition = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(restrictions, currentPosition, buffer, false, out text, out text2);
				if (!string.IsNullOrEmpty(text))
				{
					string text3 = (synonyms != null) ? synonyms[text] : text;
					if (string.IsNullOrEmpty(text3))
					{
						throw ADP.KeywordNotSupported(text);
					}
					list.Add(text3);
				}
			}
			return DBConnectionString.RemoveDuplicates(list.ToArray());
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000C98A8 File Offset: 0x000C7AA8
		internal static string[] RemoveDuplicates(string[] restrictions)
		{
			int num = restrictions.Length;
			if (0 < num)
			{
				Array.Sort<string>(restrictions, StringComparer.Ordinal);
				for (int i = 1; i < restrictions.Length; i++)
				{
					string text = restrictions[i - 1];
					if (text.Length == 0 || text == restrictions[i])
					{
						restrictions[i - 1] = null;
						num--;
					}
				}
				if (restrictions[restrictions.Length - 1].Length == 0)
				{
					restrictions[restrictions.Length - 1] = null;
					num--;
				}
				if (num != restrictions.Length)
				{
					string[] array = new string[num];
					num = 0;
					for (int j = 0; j < restrictions.Length; j++)
					{
						if (restrictions[j] != null)
						{
							array[num++] = restrictions[j];
						}
					}
					restrictions = array;
				}
			}
			return restrictions;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C994C File Offset: 0x000C7B4C
		[Conditional("DEBUG")]
		private static void Verify(string[] restrictionValues)
		{
			if (restrictionValues != null)
			{
				for (int i = 1; i < restrictionValues.Length; i++)
				{
				}
			}
		}

		// Token: 0x04001BF4 RID: 7156
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x04001BF5 RID: 7157
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x04001BF6 RID: 7158
		private readonly NameValuePair _keychain;

		// Token: 0x04001BF7 RID: 7159
		private readonly bool _hasPassword;

		// Token: 0x04001BF8 RID: 7160
		private readonly string[] _restrictionValues;

		// Token: 0x04001BF9 RID: 7161
		private readonly string _restrictions;

		// Token: 0x04001BFA RID: 7162
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x04001BFB RID: 7163
		private readonly string _encryptedActualConnectionString;

		// Token: 0x020003C7 RID: 967
		private static class KEY
		{
			// Token: 0x04001BFC RID: 7164
			internal const string Password = "password";

			// Token: 0x04001BFD RID: 7165
			internal const string PersistSecurityInfo = "persist security info";

			// Token: 0x04001BFE RID: 7166
			internal const string Pwd = "pwd";
		}
	}
}
