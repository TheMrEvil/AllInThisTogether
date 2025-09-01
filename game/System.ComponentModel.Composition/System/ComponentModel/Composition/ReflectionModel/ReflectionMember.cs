using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200007A RID: 122
	internal abstract class ReflectionMember : ReflectionItem
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600032E RID: 814
		public abstract bool CanRead { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009EEC File Offset: 0x000080EC
		public Type DeclaringType
		{
			get
			{
				return this.UnderlyingMember.DeclaringType;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00009EF9 File Offset: 0x000080F9
		public override string Name
		{
			get
			{
				return this.UnderlyingMember.Name;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009F06 File Offset: 0x00008106
		public override string GetDisplayName()
		{
			return this.UnderlyingMember.GetDisplayName();
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000332 RID: 818
		public abstract bool RequiresInstance { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000333 RID: 819
		public abstract MemberInfo UnderlyingMember { get; }

		// Token: 0x06000334 RID: 820
		public abstract object GetValue(object instance);

		// Token: 0x06000335 RID: 821 RVA: 0x00009F13 File Offset: 0x00008113
		protected ReflectionMember()
		{
		}
	}
}
