using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200020E RID: 526
	internal class DictionaryElementInitializer : ElementInitializer
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x000830B6 File Offset: 0x000812B6
		public DictionaryElementInitializer(Arguments arguments, Expression initializer, Location loc) : base(null, initializer, loc)
		{
			this.args = arguments;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000830C8 File Offset: 0x000812C8
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(8074, this.loc, "Expression tree cannot contain a dictionary initializer");
			return null;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000830E8 File Offset: 0x000812E8
		protected override bool ResolveElement(ResolveContext rc)
		{
			Expression currentInitializerVariable = rc.CurrentInitializerVariable;
			TypeSpec type = currentInitializerVariable.Type;
			if (type.IsArray)
			{
				this.target = new ArrayAccess(new ElementAccess(currentInitializerVariable, this.args, this.loc), this.loc);
				return true;
			}
			if (type.IsPointer)
			{
				this.target = currentInitializerVariable.MakePointerAccess(rc, type, this.args);
				return true;
			}
			IList<MemberSpec> list = MemberCache.FindMembers(type, MemberCache.IndexerNameAlias, false);
			if (list == null && type.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
			{
				ElementAccess.Error_CannotApplyIndexing(rc, type, this.loc);
				return false;
			}
			this.target = new IndexerExpr(list, type, currentInitializerVariable, this.args, this.loc).Resolve(rc);
			return true;
		}

		// Token: 0x04000A0E RID: 2574
		private readonly Arguments args;
	}
}
