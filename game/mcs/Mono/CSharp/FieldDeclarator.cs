using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002D7 RID: 727
	public class FieldDeclarator
	{
		// Token: 0x06002295 RID: 8853 RVA: 0x000AA6E9 File Offset: 0x000A88E9
		public FieldDeclarator(SimpleMemberName name, Expression initializer)
		{
			this.Name = name;
			this.Initializer = initializer;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x000AA6FF File Offset: 0x000A88FF
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x000AA707 File Offset: 0x000A8907
		public SimpleMemberName Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x000AA710 File Offset: 0x000A8910
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x000AA718 File Offset: 0x000A8918
		public Expression Initializer
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Initializer>k__BackingField = value;
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000AA721 File Offset: 0x000A8921
		public virtual FullNamedExpression GetFieldTypeExpression(FieldBase field)
		{
			return new TypeExpression(field.MemberType, this.Name.Location);
		}

		// Token: 0x04000D54 RID: 3412
		[CompilerGenerated]
		private SimpleMemberName <Name>k__BackingField;

		// Token: 0x04000D55 RID: 3413
		[CompilerGenerated]
		private Expression <Initializer>k__BackingField;
	}
}
