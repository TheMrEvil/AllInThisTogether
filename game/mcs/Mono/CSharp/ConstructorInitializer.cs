using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200024D RID: 589
	public abstract class ConstructorInitializer : ExpressionStatement
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x0008F431 File Offset: 0x0008D631
		protected ConstructorInitializer(Arguments argument_list, Location loc)
		{
			this.argument_list = argument_list;
			this.loc = loc;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0008F447 File Offset: 0x0008D647
		public Arguments Arguments
		{
			get
			{
				return this.argument_list;
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0000225C File Offset: 0x0000045C
		public override bool ContainsEmitWithAwait()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0008F450 File Offset: 0x0008D650
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Value;
			Constructor constructor = (Constructor)ec.MemberContext;
			using (ec.Set(ResolveContext.Options.BaseInitializer))
			{
				if (this.argument_list != null)
				{
					bool flag;
					this.argument_list.Resolve(ec, out flag);
					if (flag)
					{
						ec.Report.Error(1975, this.loc, "The constructor call cannot be dynamically dispatched within constructor initializer");
						return null;
					}
				}
				this.type = ec.CurrentType;
				if (this is ConstructorBaseInitializer)
				{
					if (ec.CurrentType.BaseType == null)
					{
						return this;
					}
					this.type = ec.CurrentType.BaseType;
					if (ec.CurrentType.IsStruct)
					{
						ec.Report.Error(522, this.loc, "`{0}': Struct constructors cannot call base constructors", constructor.GetSignatureForError());
						return this;
					}
				}
				this.base_ctor = Expression.ConstructorLookup(ec, this.type, ref this.argument_list, this.loc);
			}
			if (this.base_ctor != null && this.base_ctor.MemberDefinition == constructor.Spec.MemberDefinition)
			{
				ec.Report.Error(516, this.loc, "Constructor `{0}' cannot call itself", constructor.GetSignatureForError());
			}
			return this;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
		public override void Emit(EmitContext ec)
		{
			if (this.base_ctor != null)
			{
				CallEmitter callEmitter = default(CallEmitter);
				callEmitter.InstanceExpression = new CompilerGeneratedThis(this.type, this.loc);
				callEmitter.EmitPredefined(ec, this.base_ctor, this.argument_list, false, null);
				return;
			}
			if (this.type == ec.BuiltinTypes.Object)
			{
				return;
			}
			ec.Emit(OpCodes.Ldarg_0);
			ec.Emit(OpCodes.Initobj, this.type);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0008F628 File Offset: 0x0008D828
		public override void EmitStatement(EmitContext ec)
		{
			this.Emit(ec);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0008F631 File Offset: 0x0008D831
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.argument_list != null)
			{
				this.argument_list.FlowAnalysis(fc, null);
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private Arguments argument_list;

		// Token: 0x04000AD7 RID: 2775
		private MethodSpec base_ctor;
	}
}
