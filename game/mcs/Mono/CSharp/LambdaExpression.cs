using System;

namespace Mono.CSharp
{
	// Token: 0x02000183 RID: 387
	public class LambdaExpression : AnonymousMethodExpression
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x0004CFB4 File Offset: 0x0004B1B4
		public LambdaExpression(Location loc) : base(loc)
		{
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004CFC0 File Offset: 0x0004B1C0
		protected override Expression CreateExpressionTree(ResolveContext ec, TypeSpec delegate_type)
		{
			if (ec.IsInProbingMode)
			{
				return this;
			}
			BlockContext ec2 = new BlockContext(ec.MemberContext, ec.ConstructorBlock, ec.BuiltinTypes.Void)
			{
				CurrentAnonymousMethod = ec.CurrentAnonymousMethod
			};
			Expression expr = base.Parameters.CreateExpressionTree(ec2, this.loc);
			Expression expression = this.Block.CreateExpressionTree(ec);
			if (expression == null)
			{
				return null;
			}
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(expression));
			arguments.Add(new Argument(expr));
			return base.CreateExpressionFactoryCall(ec, "Lambda", new TypeArguments(new FullNamedExpression[]
			{
				new TypeExpression(delegate_type, this.loc)
			}), arguments);
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004D06B File Offset: 0x0004B26B
		public override bool HasExplicitParameters
		{
			get
			{
				return base.Parameters.Count > 0 && !(base.Parameters.FixedParameters[0] is ImplicitLambdaParameter);
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004D098 File Offset: 0x0004B298
		protected override ParametersCompiled ResolveParameters(ResolveContext ec, TypeInferenceContext tic, TypeSpec delegateType)
		{
			if (!delegateType.IsDelegate)
			{
				return null;
			}
			AParametersCollection parameters = Delegate.GetParameters(delegateType);
			if (this.HasExplicitParameters)
			{
				if (!base.VerifyExplicitParameters(ec, tic, delegateType, parameters))
				{
					return null;
				}
				return base.Parameters;
			}
			else
			{
				if (!base.VerifyParameterCompatibility(ec, tic, delegateType, parameters, ec.IsInProbingMode))
				{
					return null;
				}
				TypeSpec[] array = new TypeSpec[base.Parameters.Count];
				for (int i = 0; i < parameters.Count; i++)
				{
					if ((parameters.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
					{
						return null;
					}
					TypeSpec typeSpec = parameters.Types[i];
					if (tic != null)
					{
						typeSpec = tic.InflateGenericArgument(ec, typeSpec);
					}
					array[i] = typeSpec;
					ImplicitLambdaParameter implicitLambdaParameter = (ImplicitLambdaParameter)base.Parameters.FixedParameters[i];
					implicitLambdaParameter.SetParameterType(typeSpec);
					implicitLambdaParameter.Resolve(null, i);
				}
				base.Parameters.Types = array;
				return base.Parameters;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004D168 File Offset: 0x0004B368
		protected override AnonymousMethodBody CompatibleMethodFactory(TypeSpec returnType, TypeSpec delegateType, ParametersCompiled p, ParametersBlock b)
		{
			return new LambdaMethod(p, b, returnType, delegateType, this.loc);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004D17A File Offset: 0x0004B37A
		protected override bool DoResolveParameters(ResolveContext rc)
		{
			return !this.HasExplicitParameters || base.Parameters.Resolve(rc);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004D192 File Offset: 0x0004B392
		public override string GetSignatureForError()
		{
			return "lambda expression";
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004D199 File Offset: 0x0004B399
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
