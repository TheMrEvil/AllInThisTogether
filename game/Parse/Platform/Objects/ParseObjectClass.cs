using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Objects
{
	// Token: 0x0200002F RID: 47
	internal class ParseObjectClass
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000099B0 File Offset: 0x00007BB0
		public ParseObjectClass(Type type, ConstructorInfo constructor)
		{
			this.TypeInfo = type.GetTypeInfo();
			this.DeclaredName = this.TypeInfo.GetParseClassName();
			this.Constructor = constructor;
			this.Constructor = constructor;
			this.PropertyMappings = (from property in type.GetProperties()
			select new ValueTuple<PropertyInfo, ParseFieldNameAttribute>(property, property.GetCustomAttribute(true)) into set
			where set.Item2 != null
			select set).ToDictionary(([TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})] ValueTuple<PropertyInfo, ParseFieldNameAttribute> set) => set.Item1.Name, ([TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})] ValueTuple<PropertyInfo, ParseFieldNameAttribute> set) => set.Item2.FieldName);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009A87 File Offset: 0x00007C87
		public TypeInfo TypeInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeInfo>k__BackingField;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009A8F File Offset: 0x00007C8F
		public string DeclaredName
		{
			[CompilerGenerated]
			get
			{
				return this.<DeclaredName>k__BackingField;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009A97 File Offset: 0x00007C97
		public IDictionary<string, string> PropertyMappings
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyMappings>k__BackingField;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009A9F File Offset: 0x00007C9F
		public ParseObject Instantiate()
		{
			return this.Constructor.Invoke(null) as ParseObject;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00009AB2 File Offset: 0x00007CB2
		private ConstructorInfo Constructor
		{
			[CompilerGenerated]
			get
			{
				return this.<Constructor>k__BackingField;
			}
		}

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private readonly TypeInfo <TypeInfo>k__BackingField;

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		private readonly string <DeclaredName>k__BackingField;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private readonly IDictionary<string, string> <PropertyMappings>k__BackingField;

		// Token: 0x0400005F RID: 95
		[CompilerGenerated]
		private readonly ConstructorInfo <Constructor>k__BackingField;

		// Token: 0x020000FD RID: 253
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006CD RID: 1741 RVA: 0x00014F90 File Offset: 0x00013190
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x00014F9C File Offset: 0x0001319C
			public <>c()
			{
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x00014FA4 File Offset: 0x000131A4
			[return: TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})]
			internal ValueTuple<PropertyInfo, ParseFieldNameAttribute> <.ctor>b__0_0(PropertyInfo property)
			{
				return new ValueTuple<PropertyInfo, ParseFieldNameAttribute>(property, property.GetCustomAttribute(true));
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x00014FB3 File Offset: 0x000131B3
			internal bool <.ctor>b__0_1([TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})] ValueTuple<PropertyInfo, ParseFieldNameAttribute> set)
			{
				return set.Item2 != null;
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00014FBE File Offset: 0x000131BE
			internal string <.ctor>b__0_2([TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})] ValueTuple<PropertyInfo, ParseFieldNameAttribute> set)
			{
				return set.Item1.Name;
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x00014FCB File Offset: 0x000131CB
			internal string <.ctor>b__0_3([TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})] ValueTuple<PropertyInfo, ParseFieldNameAttribute> set)
			{
				return set.Item2.FieldName;
			}

			// Token: 0x04000207 RID: 519
			public static readonly ParseObjectClass.<>c <>9 = new ParseObjectClass.<>c();

			// Token: 0x04000208 RID: 520
			[TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})]
			public static Func<PropertyInfo, ValueTuple<PropertyInfo, ParseFieldNameAttribute>> <>9__0_0;

			// Token: 0x04000209 RID: 521
			[TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})]
			public static Func<ValueTuple<PropertyInfo, ParseFieldNameAttribute>, bool> <>9__0_1;

			// Token: 0x0400020A RID: 522
			[TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})]
			public static Func<ValueTuple<PropertyInfo, ParseFieldNameAttribute>, string> <>9__0_2;

			// Token: 0x0400020B RID: 523
			[TupleElementNames(new string[]
			{
				"Property",
				"FieldNameAttribute"
			})]
			public static Func<ValueTuple<PropertyInfo, ParseFieldNameAttribute>, string> <>9__0_3;
		}
	}
}
