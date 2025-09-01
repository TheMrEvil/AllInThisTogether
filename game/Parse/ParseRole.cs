using System;
using System.Text.RegularExpressions;

namespace Parse
{
	// Token: 0x02000015 RID: 21
	[ParseClassName("_Role")]
	public class ParseRole : ParseObject
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00006A16 File Offset: 0x00004C16
		public ParseRole() : base(null)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006A1F File Offset: 0x00004C1F
		public ParseRole(string name, ParseACL acl) : this()
		{
			this.Name = name;
			base.ACL = acl;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006A35 File Offset: 0x00004C35
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006A42 File Offset: 0x00004C42
		[ParseFieldName("name")]
		public string Name
		{
			get
			{
				return base.GetProperty<string>("Name");
			}
			set
			{
				base.SetProperty<string>(value, "Name");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006A50 File Offset: 0x00004C50
		[ParseFieldName("users")]
		public ParseRelation<ParseUser> Users
		{
			get
			{
				return base.GetRelationProperty<ParseUser>("Users");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006A5D File Offset: 0x00004C5D
		[ParseFieldName("roles")]
		public ParseRelation<ParseRole> Roles
		{
			get
			{
				return base.GetRelationProperty<ParseRole>("Roles");
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006A6C File Offset: 0x00004C6C
		internal override void OnSettingValue(ref string key, ref object value)
		{
			base.OnSettingValue(ref key, ref value);
			if (key == "name")
			{
				if (base.ObjectId != null)
				{
					throw new InvalidOperationException("A role's name can only be set before it has been saved.");
				}
				if (!(value is string))
				{
					throw new ArgumentException("A role's name must be a string.", "value");
				}
				if (!ParseRole.namePattern.IsMatch((string)value))
				{
					throw new ArgumentException("A role's name can only contain alphanumeric characters, _, -, and spaces.", "value");
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006ADE File Offset: 0x00004CDE
		// Note: this type is marked as 'beforefieldinit'.
		static ParseRole()
		{
		}

		// Token: 0x04000031 RID: 49
		private static readonly Regex namePattern = new Regex("^[0-9a-zA-Z_\\- ]+$");
	}
}
