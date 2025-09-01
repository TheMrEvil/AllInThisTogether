using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.CSharp
{
	// Token: 0x020001BD RID: 445
	internal class ExtensionMethodGroupExpr : MethodGroupExpr, OverloadResolver.IErrorHandler
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0006EF3B File Offset: 0x0006D13B
		public ExtensionMethodGroupExpr(ExtensionMethodCandidates candidates, Expression extensionExpr, Location loc) : base(candidates.Methods.Cast<MemberSpec>().ToList<MemberSpec>(), extensionExpr.Type, loc)
		{
			this.candidates = candidates;
			this.ExtensionExpression = extensionExpr;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0006EF68 File Offset: 0x0006D168
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (base.ConditionalAccess)
			{
				fc.BranchConditionalAccessDefiniteAssignment();
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0006EF78 File Offset: 0x0006D178
		public override IList<MemberSpec> GetBaseMembers(TypeSpec baseType)
		{
			if (this.candidates == null)
			{
				return null;
			}
			int arity = (this.type_arguments == null) ? 0 : this.type_arguments.Count;
			this.candidates = this.candidates.Container.LookupExtensionMethod(this.candidates.Context, this.Name, arity, this.candidates.LookupIndex);
			if (this.candidates == null)
			{
				return null;
			}
			return this.candidates.Methods.Cast<MemberSpec>().ToList<MemberSpec>();
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0006EFF8 File Offset: 0x0006D1F8
		public static bool IsExtensionTypeCompatible(TypeSpec argType, TypeSpec extensionType)
		{
			return argType == extensionType || TypeSpecComparer.IsEqual(argType, extensionType) || Convert.ImplicitReferenceConversionExists(argType, extensionType, false) || Convert.ImplicitBoxingConversion(null, argType, extensionType) != null;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0006F01E File Offset: 0x0006D21E
		public bool ResolveNameOf(ResolveContext rc, MemberAccess ma)
		{
			rc.Report.Error(8093, ma.Location, "An argument to nameof operator cannot be extension method group");
			return false;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000055E7 File Offset: 0x000037E7
		public override MethodGroupExpr LookupExtensionMethod(ResolveContext rc)
		{
			return null;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0006F03C File Offset: 0x0006D23C
		public override MethodGroupExpr OverloadResolve(ResolveContext ec, ref Arguments arguments, OverloadResolver.IErrorHandler ehandler, OverloadResolver.Restrictions restr)
		{
			if (arguments == null)
			{
				arguments = new Arguments(1);
			}
			this.ExtensionExpression = this.ExtensionExpression.Resolve(ec);
			if (this.ExtensionExpression == null)
			{
				return null;
			}
			ExtensionMethodCandidates extensionMethodCandidates = this.candidates;
			Argument.AType type = base.ConditionalAccess ? Argument.AType.ExtensionTypeConditionalAccess : Argument.AType.ExtensionType;
			arguments.Insert(0, new Argument(this.ExtensionExpression, type));
			bool flag = base.OverloadResolve(ec, ref arguments, ehandler ?? this, restr) != null;
			this.candidates = extensionMethodCandidates;
			if (!flag)
			{
				arguments.RemoveAt(0);
				return null;
			}
			MemberExpr memberExpr = this.ExtensionExpression as MemberExpr;
			if (memberExpr != null)
			{
				memberExpr.ResolveInstanceExpression(ec, null);
				FieldExpr fieldExpr = memberExpr as FieldExpr;
				if (fieldExpr != null)
				{
					fieldExpr.Spec.MemberDefinition.SetIsUsed();
				}
			}
			this.InstanceExpression = null;
			return this;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000022F4 File Offset: 0x000004F4
		bool OverloadResolver.IErrorHandler.AmbiguousCandidates(ResolveContext rc, MemberSpec best, MemberSpec ambiguous)
		{
			return false;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		bool OverloadResolver.IErrorHandler.ArgumentMismatch(ResolveContext rc, MemberSpec best, Argument arg, int index)
		{
			rc.Report.SymbolRelatedToPreviousError(best);
			if (index == 0)
			{
				rc.Report.Error(1929, this.loc, "Type `{0}' does not contain a member `{1}' and the best extension method overload `{2}' requires an instance of type `{3}'", new string[]
				{
					this.queried_type.GetSignatureForError(),
					this.Name,
					best.GetSignatureForError(),
					((MethodSpec)best).Parameters.ExtensionMethodType.GetSignatureForError()
				});
			}
			else
			{
				rc.Report.Error(1928, this.loc, "Type `{0}' does not contain a member `{1}' and the best extension method overload `{2}' has some invalid arguments", new string[]
				{
					this.queried_type.GetSignatureForError(),
					this.Name,
					best.GetSignatureForError()
				});
			}
			return true;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000022F4 File Offset: 0x000004F4
		bool OverloadResolver.IErrorHandler.NoArgumentMatch(ResolveContext rc, MemberSpec best)
		{
			return false;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000022F4 File Offset: 0x000004F4
		bool OverloadResolver.IErrorHandler.TypeInferenceFailed(ResolveContext rc, MemberSpec best)
		{
			return false;
		}

		// Token: 0x04000969 RID: 2409
		private ExtensionMethodCandidates candidates;

		// Token: 0x0400096A RID: 2410
		public Expression ExtensionExpression;
	}
}
