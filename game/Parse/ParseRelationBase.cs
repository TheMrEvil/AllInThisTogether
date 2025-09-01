using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure.Control;

namespace Parse
{
	// Token: 0x02000013 RID: 19
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ParseRelationBase : IJsonConvertible
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006825 File Offset: 0x00004A25
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000682D File Offset: 0x00004A2D
		private ParseObject Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Parent>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006836 File Offset: 0x00004A36
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000683E File Offset: 0x00004A3E
		private string Key
		{
			[CompilerGenerated]
			get
			{
				return this.<Key>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Key>k__BackingField = value;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006847 File Offset: 0x00004A47
		internal ParseRelationBase(ParseObject parent, string key)
		{
			this.EnsureParentAndKey(parent, key);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006857 File Offset: 0x00004A57
		internal ParseRelationBase(ParseObject parent, string key, string targetClassName) : this(parent, key)
		{
			this.TargetClassName = targetClassName;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006868 File Offset: 0x00004A68
		internal void EnsureParentAndKey(ParseObject parent, string key)
		{
			if (this.Parent == null)
			{
				this.Parent = parent;
			}
			if (this.Key == null)
			{
				this.Key = key;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006898 File Offset: 0x00004A98
		internal void Add(ParseObject entity)
		{
			ParseRelationOperation parseRelationOperation = new ParseRelationOperation(this.Parent.Services.ClassController, new ParseObject[]
			{
				entity
			}, null);
			this.Parent.PerformOperation(this.Key, parseRelationOperation);
			this.TargetClassName = parseRelationOperation.TargetClassName;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000068E4 File Offset: 0x00004AE4
		internal void Remove(ParseObject entity)
		{
			ParseRelationOperation parseRelationOperation = new ParseRelationOperation(this.Parent.Services.ClassController, null, new ParseObject[]
			{
				entity
			});
			this.Parent.PerformOperation(this.Key, parseRelationOperation);
			this.TargetClassName = parseRelationOperation.TargetClassName;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006930 File Offset: 0x00004B30
		IDictionary<string, object> IJsonConvertible.ConvertToJSON()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["__type"] = "Relation";
			dictionary["className"] = this.TargetClassName;
			return dictionary;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006958 File Offset: 0x00004B58
		internal ParseQuery<T> GetQuery<T>() where T : ParseObject
		{
			if (this.TargetClassName == null)
			{
				return new ParseQuery<T>(this.Parent.Services, this.Parent.ClassName).RedirectClassName(this.Key).WhereRelatedTo(this.Parent, this.Key);
			}
			return new ParseQuery<T>(this.Parent.Services, this.TargetClassName).WhereRelatedTo(this.Parent, this.Key);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000069CC File Offset: 0x00004BCC
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000069D4 File Offset: 0x00004BD4
		internal string TargetClassName
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetClassName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetClassName>k__BackingField = value;
			}
		}

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		private ParseObject <Parent>k__BackingField;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		private string <Key>k__BackingField;

		// Token: 0x04000030 RID: 48
		[CompilerGenerated]
		private string <TargetClassName>k__BackingField;
	}
}
