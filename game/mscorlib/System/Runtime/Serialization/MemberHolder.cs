using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000654 RID: 1620
	[Serializable]
	internal sealed class MemberHolder
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x000D1A84 File Offset: 0x000CFC84
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this._memberType = type;
			this._context = ctx;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000D1A9A File Offset: 0x000CFC9A
		public override int GetHashCode()
		{
			return this._memberType.GetHashCode();
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000D1AA8 File Offset: 0x000CFCA8
		public override bool Equals(object obj)
		{
			MemberHolder memberHolder = obj as MemberHolder;
			return memberHolder != null && memberHolder._memberType == this._memberType && memberHolder._context.State == this._context.State;
		}

		// Token: 0x04002721 RID: 10017
		internal readonly MemberInfo[] _members;

		// Token: 0x04002722 RID: 10018
		internal readonly Type _memberType;

		// Token: 0x04002723 RID: 10019
		internal readonly StreamingContext _context;
	}
}
