using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x02000016 RID: 22
	public class ParseACL : IJsonConvertible
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00006AF0 File Offset: 0x00004CF0
		internal ParseACL(IDictionary<string, object> jsonObject)
		{
			this.readers = new HashSet<string>(jsonObject.Where(delegate(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return ((IDictionary<string, object>)keyValuePair.Value).ContainsKey("read");
			}).Select(delegate(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return keyValuePair.Key;
			}));
			this.writers = new HashSet<string>(jsonObject.Where(delegate(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return ((IDictionary<string, object>)keyValuePair.Value).ContainsKey("write");
			}).Select(delegate(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return keyValuePair.Key;
			}));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006BC1 File Offset: 0x00004DC1
		public ParseACL()
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006BDF File Offset: 0x00004DDF
		public ParseACL(ParseUser owner)
		{
			this.SetReadAccess(owner, true);
			this.SetWriteAccess(owner, true);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006C10 File Offset: 0x00004E10
		IDictionary<string, object> IJsonConvertible.ConvertToJSON()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string text in this.readers.Union(this.writers))
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				if (this.readers.Contains(text))
				{
					dictionary2["read"] = true;
				}
				if (this.writers.Contains(text))
				{
					dictionary2["write"] = true;
				}
				dictionary[text] = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006CB4 File Offset: 0x00004EB4
		private void SetAccess(ParseACL.AccessKind kind, string userId, bool allowed)
		{
			if (userId == null)
			{
				throw new ArgumentException("Cannot set access for an unsaved user or role.");
			}
			ICollection<string> collection;
			if (kind != ParseACL.AccessKind.Read)
			{
				if (kind != ParseACL.AccessKind.Write)
				{
					throw new NotImplementedException("Unknown AccessKind");
				}
				collection = this.writers;
			}
			else
			{
				collection = this.readers;
			}
			if (allowed)
			{
				collection.Add(userId);
				return;
			}
			collection.Remove(userId);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006D0A File Offset: 0x00004F0A
		private bool GetAccess(ParseACL.AccessKind kind, string userId)
		{
			if (userId == null)
			{
				throw new ArgumentException("Cannot get access for an unsaved user or role.");
			}
			if (kind == ParseACL.AccessKind.Read)
			{
				return this.readers.Contains(userId);
			}
			if (kind != ParseACL.AccessKind.Write)
			{
				throw new NotImplementedException("Unknown AccessKind");
			}
			return this.writers.Contains(userId);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006D47 File Offset: 0x00004F47
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006D55 File Offset: 0x00004F55
		public bool PublicReadAccess
		{
			get
			{
				return this.GetAccess(ParseACL.AccessKind.Read, "*");
			}
			set
			{
				this.SetAccess(ParseACL.AccessKind.Read, "*", value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006D72 File Offset: 0x00004F72
		public bool PublicWriteAccess
		{
			get
			{
				return this.GetAccess(ParseACL.AccessKind.Write, "*");
			}
			set
			{
				this.SetAccess(ParseACL.AccessKind.Write, "*", value);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006D81 File Offset: 0x00004F81
		public void SetReadAccess(string userId, bool allowed)
		{
			this.SetAccess(ParseACL.AccessKind.Read, userId, allowed);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006D8C File Offset: 0x00004F8C
		public void SetReadAccess(ParseUser user, bool allowed)
		{
			this.SetReadAccess(user.ObjectId, allowed);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006D9B File Offset: 0x00004F9B
		public void SetWriteAccess(string userId, bool allowed)
		{
			this.SetAccess(ParseACL.AccessKind.Write, userId, allowed);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006DA6 File Offset: 0x00004FA6
		public void SetWriteAccess(ParseUser user, bool allowed)
		{
			this.SetWriteAccess(user.ObjectId, allowed);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006DB5 File Offset: 0x00004FB5
		public bool GetReadAccess(string userId)
		{
			return this.GetAccess(ParseACL.AccessKind.Read, userId);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006DBF File Offset: 0x00004FBF
		public bool GetReadAccess(ParseUser user)
		{
			return this.GetReadAccess(user.ObjectId);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006DCD File Offset: 0x00004FCD
		public bool GetWriteAccess(string userId)
		{
			return this.GetAccess(ParseACL.AccessKind.Write, userId);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006DD7 File Offset: 0x00004FD7
		public bool GetWriteAccess(ParseUser user)
		{
			return this.GetWriteAccess(user.ObjectId);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public void SetRoleReadAccess(string roleName, bool allowed)
		{
			this.SetAccess(ParseACL.AccessKind.Read, "role:" + roleName, allowed);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006DFA File Offset: 0x00004FFA
		public void SetRoleReadAccess(ParseRole role, bool allowed)
		{
			this.SetRoleReadAccess(role.Name, allowed);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006E09 File Offset: 0x00005009
		public bool GetRoleReadAccess(string roleName)
		{
			return this.GetAccess(ParseACL.AccessKind.Read, "role:" + roleName);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006E1D File Offset: 0x0000501D
		public bool GetRoleReadAccess(ParseRole role)
		{
			return this.GetRoleReadAccess(role.Name);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006E2B File Offset: 0x0000502B
		public void SetRoleWriteAccess(string roleName, bool allowed)
		{
			this.SetAccess(ParseACL.AccessKind.Write, "role:" + roleName, allowed);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006E40 File Offset: 0x00005040
		public void SetRoleWriteAccess(ParseRole role, bool allowed)
		{
			this.SetRoleWriteAccess(role.Name, allowed);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006E4F File Offset: 0x0000504F
		public bool GetRoleWriteAccess(string roleName)
		{
			return this.GetAccess(ParseACL.AccessKind.Write, "role:" + roleName);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006E63 File Offset: 0x00005063
		public bool GetRoleWriteAccess(ParseRole role)
		{
			return this.GetRoleWriteAccess(role.Name);
		}

		// Token: 0x04000032 RID: 50
		private const string publicName = "*";

		// Token: 0x04000033 RID: 51
		private readonly ICollection<string> readers = new HashSet<string>();

		// Token: 0x04000034 RID: 52
		private readonly ICollection<string> writers = new HashSet<string>();

		// Token: 0x020000BA RID: 186
		private enum AccessKind
		{
			// Token: 0x0400014A RID: 330
			Read,
			// Token: 0x0400014B RID: 331
			Write
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000607 RID: 1543 RVA: 0x000133B3 File Offset: 0x000115B3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x000133BF File Offset: 0x000115BF
			public <>c()
			{
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x000133C8 File Offset: 0x000115C8
			internal bool <.ctor>b__4_0(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return ((IDictionary<string, object>)keyValuePair.Value).ContainsKey("read");
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x000133F0 File Offset: 0x000115F0
			internal string <.ctor>b__4_1(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return keyValuePair.Key;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x00013408 File Offset: 0x00011608
			internal bool <.ctor>b__4_2(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return ((IDictionary<string, object>)keyValuePair.Value).ContainsKey("write");
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x00013430 File Offset: 0x00011630
			internal string <.ctor>b__4_3(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return keyValuePair.Key;
			}

			// Token: 0x0400014C RID: 332
			public static readonly ParseACL.<>c <>9 = new ParseACL.<>c();

			// Token: 0x0400014D RID: 333
			public static Func<KeyValuePair<string, object>, bool> <>9__4_0;

			// Token: 0x0400014E RID: 334
			public static Func<KeyValuePair<string, object>, string> <>9__4_1;

			// Token: 0x0400014F RID: 335
			public static Func<KeyValuePair<string, object>, bool> <>9__4_2;

			// Token: 0x04000150 RID: 336
			public static Func<KeyValuePair<string, object>, string> <>9__4_3;
		}
	}
}
