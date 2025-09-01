using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000205 RID: 517
	public class UserCast : Expression
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x00082468 File Offset: 0x00080668
		public UserCast(MethodSpec method, Expression source, Location l)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.method = method;
			this.source = source;
			this.type = method.ReturnType;
			this.loc = l;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0008249F File Offset: 0x0008069F
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x000824A7 File Offset: 0x000806A7
		public Expression Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000824B0 File Offset: 0x000806B0
		public override bool ContainsEmitWithAwait()
		{
			return this.source.ContainsEmitWithAwait();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000824C0 File Offset: 0x000806C0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(3);
			arguments.Add(new Argument(this.source.CreateExpressionTree(ec)));
			arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			arguments.Add(new Argument(new TypeOfMethod(this.method, this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Convert", arguments);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00082530 File Offset: 0x00080730
		protected override Expression DoResolve(ResolveContext ec)
		{
			ObsoleteAttribute attributeObsolete = this.method.GetAttributeObsolete();
			if (attributeObsolete != null)
			{
				AttributeTester.Report_ObsoleteMessage(attributeObsolete, this.GetSignatureForError(), this.loc, ec.Report);
			}
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0008256C File Offset: 0x0008076C
		public override void Emit(EmitContext ec)
		{
			this.source.Emit(ec);
			ec.MarkCallEntry(this.loc);
			ec.Emit(OpCodes.Call, this.method);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00082597 File Offset: 0x00080797
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.source.FlowAnalysis(fc);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000825A5 File Offset: 0x000807A5
		public override string GetSignatureForError()
		{
			return TypeManager.CSharpSignature(this.method);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000825B2 File Offset: 0x000807B2
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Convert(this.source.MakeExpression(ctx), this.type.GetMetaInfo(), (MethodInfo)this.method.GetMetaInfo());
		}

		// Token: 0x04000A01 RID: 2561
		private MethodSpec method;

		// Token: 0x04000A02 RID: 2562
		private Expression source;
	}
}
