using System;

namespace Mono.CSharp
{
	// Token: 0x0200022B RID: 555
	internal class TypeInference
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x0008834B File Offset: 0x0008654B
		public TypeInference(Arguments arguments)
		{
			this.arguments = arguments;
			if (arguments != null)
			{
				this.arg_count = arguments.Count;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00088369 File Offset: 0x00086569
		public int InferenceScore
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00088374 File Offset: 0x00086574
		public TypeSpec[] InferMethodArguments(ResolveContext ec, MethodSpec method)
		{
			TypeInferenceContext typeInferenceContext = new TypeInferenceContext(method.GenericDefinition.TypeParameters);
			if (!typeInferenceContext.UnfixedVariableExists)
			{
				return TypeSpec.EmptyTypes;
			}
			AParametersCollection parameters = method.Parameters;
			if (!this.InferInPhases(ec, typeInferenceContext, parameters))
			{
				return null;
			}
			return typeInferenceContext.InferredTypeArguments;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000883BC File Offset: 0x000865BC
		private bool InferInPhases(ResolveContext ec, TypeInferenceContext tic, AParametersCollection methodParameters)
		{
			int num;
			if (methodParameters.HasParams)
			{
				num = methodParameters.Count - 1;
			}
			else
			{
				num = this.arg_count;
			}
			TypeSpec[] array = methodParameters.Types;
			TypeSpec typeSpec = null;
			for (int i = 0; i < this.arg_count; i++)
			{
				Argument argument = this.arguments[i];
				if (argument != null)
				{
					if (i < num)
					{
						typeSpec = methodParameters.Types[i];
					}
					else if (i == num)
					{
						if (this.arg_count == num + 1 && TypeManager.HasElementType(argument.Type))
						{
							typeSpec = methodParameters.Types[num];
						}
						else
						{
							typeSpec = TypeManager.GetElementType(methodParameters.Types[num]);
						}
						array = (TypeSpec[])array.Clone();
						array[i] = typeSpec;
					}
					AnonymousMethodExpression anonymousMethodExpression = argument.Expr as AnonymousMethodExpression;
					if (anonymousMethodExpression != null)
					{
						if (anonymousMethodExpression.ExplicitTypeInference(tic, typeSpec))
						{
							this.score++;
						}
					}
					else if (argument.IsByRef)
					{
						this.score += tic.ExactInference(argument.Type, typeSpec);
					}
					else if (argument.Expr.Type != InternalType.NullLiteral)
					{
						if (TypeSpec.IsValueType(typeSpec))
						{
							this.score += tic.LowerBoundInference(argument.Type, typeSpec);
						}
						else
						{
							this.score += tic.OutputTypeInference(ec, argument.Expr, typeSpec);
						}
					}
				}
			}
			bool flag = false;
			return tic.FixIndependentTypeArguments(ec, array, ref flag) && this.DoSecondPhase(ec, tic, array, !flag);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00088540 File Offset: 0x00086740
		private bool DoSecondPhase(ResolveContext ec, TypeInferenceContext tic, TypeSpec[] methodParameters, bool fixDependent)
		{
			bool flag = false;
			if (fixDependent && !tic.FixDependentTypes(ec, ref flag))
			{
				return false;
			}
			if (!tic.UnfixedVariableExists)
			{
				return true;
			}
			if (!flag && fixDependent)
			{
				return false;
			}
			int i = 0;
			while (i < this.arg_count)
			{
				TypeSpec typeSpec = methodParameters[(i >= methodParameters.Length) ? (methodParameters.Length - 1) : i];
				if (typeSpec.IsDelegate)
				{
					goto IL_56;
				}
				if (typeSpec.IsExpressionTreeType)
				{
					typeSpec = TypeManager.GetTypeArguments(typeSpec)[0];
					goto IL_56;
				}
				IL_A4:
				i++;
				continue;
				IL_56:
				MethodSpec invokeMethod = Delegate.GetInvokeMethod(typeSpec);
				TypeSpec returnType = invokeMethod.ReturnType;
				if (tic.IsReturnTypeNonDependent(invokeMethod, returnType) && this.arguments[i] != null)
				{
					this.score += tic.OutputTypeInference(ec, this.arguments[i].Expr, typeSpec);
					goto IL_A4;
				}
				goto IL_A4;
			}
			return this.DoSecondPhase(ec, tic, methodParameters, true);
		}

		// Token: 0x04000A64 RID: 2660
		private int score;

		// Token: 0x04000A65 RID: 2661
		private readonly Arguments arguments;

		// Token: 0x04000A66 RID: 2662
		private readonly int arg_count;
	}
}
