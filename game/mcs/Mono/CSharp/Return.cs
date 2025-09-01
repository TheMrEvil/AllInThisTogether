using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002A4 RID: 676
	public class Return : ExitStatement
	{
		// Token: 0x06002097 RID: 8343 RVA: 0x000A070E File Offset: 0x0009E90E
		public Return(Expression expr, Location l)
		{
			this.expr = expr;
			this.loc = l;
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x000A0724 File Offset: 0x0009E924
		// (set) Token: 0x06002099 RID: 8345 RVA: 0x000A072C File Offset: 0x0009E92C
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
			protected set
			{
				this.expr = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x000022F4 File Offset: 0x000004F4
		protected override bool IsLocalExit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000A0738 File Offset: 0x0009E938
		protected override bool DoResolve(BlockContext ec)
		{
			TypeSpec typeSpec = ec.ReturnType;
			if (this.expr == null)
			{
				if (typeSpec.Kind == MemberKind.Void || typeSpec == InternalType.ErrorType)
				{
					return true;
				}
				if (ec.CurrentAnonymousMethod is AsyncInitializer)
				{
					AsyncTaskStorey asyncTaskStorey = (AsyncTaskStorey)ec.CurrentAnonymousMethod.Storey;
					if (asyncTaskStorey.ReturnType == ec.Module.PredefinedTypes.Task.TypeSpec)
					{
						this.expr = EmptyExpression.Null;
						return true;
					}
					if (asyncTaskStorey.ReturnType.IsGenericTask)
					{
						typeSpec = asyncTaskStorey.ReturnType.TypeArguments[0];
					}
				}
				if (ec.CurrentIterator != null)
				{
					this.Error_ReturnFromIterator(ec);
				}
				else if (typeSpec != InternalType.ErrorType)
				{
					ec.Report.Error(126, this.loc, "An object of a type convertible to `{0}' is required for the return statement", typeSpec.GetSignatureForError());
				}
				return false;
			}
			else
			{
				this.expr = this.expr.Resolve(ec);
				AnonymousExpression currentAnonymousMethod = ec.CurrentAnonymousMethod;
				if (currentAnonymousMethod == null)
				{
					if (typeSpec.Kind == MemberKind.Void)
					{
						ec.Report.Error(127, this.loc, "`{0}': A return keyword must not be followed by any expression when method returns void", ec.GetSignatureForError());
						return false;
					}
				}
				else
				{
					if (currentAnonymousMethod.IsIterator)
					{
						this.Error_ReturnFromIterator(ec);
						return false;
					}
					AsyncInitializer asyncInitializer = currentAnonymousMethod as AsyncInitializer;
					if (asyncInitializer != null)
					{
						if (this.expr != null)
						{
							TypeSpec returnType = ((AsyncTaskStorey)currentAnonymousMethod.Storey).ReturnType;
							if (returnType == null && asyncInitializer.ReturnTypeInference != null)
							{
								if (this.expr.Type.Kind == MemberKind.Void && !(this is ContextualReturn))
								{
									ec.Report.Error(4029, this.loc, "Cannot return an expression of type `void'");
								}
								else
								{
									asyncInitializer.ReturnTypeInference.AddCommonTypeBoundAsync(this.expr.Type);
								}
								return true;
							}
							if (returnType.Kind == MemberKind.Void)
							{
								ec.Report.Error(8030, this.loc, "Anonymous function or lambda expression converted to a void returning delegate cannot return a value");
								return false;
							}
							if (!returnType.IsGenericTask)
							{
								if (this is ContextualReturn)
								{
									return true;
								}
								if (asyncInitializer.DelegateType != null)
								{
									ec.Report.Error(8031, this.loc, "Async lambda expression or anonymous method converted to a `Task' cannot return a value. Consider returning `Task<T>'");
								}
								else
								{
									ec.Report.Error(1997, this.loc, "`{0}': A return keyword must not be followed by an expression when async method returns `Task'. Consider using `Task<T>' return type", ec.GetSignatureForError());
								}
								return false;
							}
							else if (this.expr.Type == returnType)
							{
								ec.Report.Error(4016, this.loc, "`{0}': The return expression type of async method must be `{1}' rather than `Task<{1}>'", ec.GetSignatureForError(), returnType.TypeArguments[0].GetSignatureForError());
							}
							else
							{
								typeSpec = returnType.TypeArguments[0];
							}
						}
					}
					else
					{
						if (typeSpec.Kind == MemberKind.Void)
						{
							ec.Report.Error(8030, this.loc, "Anonymous function or lambda expression converted to a void returning delegate cannot return a value");
							return false;
						}
						AnonymousMethodBody anonymousMethodBody = currentAnonymousMethod as AnonymousMethodBody;
						if (anonymousMethodBody != null && this.expr != null)
						{
							if (anonymousMethodBody.ReturnTypeInference != null)
							{
								anonymousMethodBody.ReturnTypeInference.AddCommonTypeBound(this.expr.Type);
								return true;
							}
							if (this is ContextualReturn && !ec.IsInProbingMode && ec.Module.Compiler.Settings.Optimize)
							{
								anonymousMethodBody.DirectMethodGroupConversion = this.expr.CanReduceLambda(anonymousMethodBody);
							}
						}
					}
				}
				if (this.expr == null)
				{
					return false;
				}
				if (this.expr.Type != typeSpec && this.expr.Type != InternalType.ErrorType)
				{
					this.expr = Convert.ImplicitConversionRequired(ec, this.expr, typeSpec, this.loc);
					if (this.expr == null)
					{
						if (currentAnonymousMethod != null && typeSpec == ec.ReturnType)
						{
							ec.Report.Error(1662, this.loc, "Cannot convert `{0}' to delegate type `{1}' because some of the return types in the block are not implicitly convertible to the delegate return type", currentAnonymousMethod.ContainerType, currentAnonymousMethod.GetSignatureForError());
						}
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000A0AE8 File Offset: 0x0009ECE8
		protected override void DoEmit(EmitContext ec)
		{
			if (this.expr != null)
			{
				AsyncInitializer asyncInitializer = ec.CurrentAnonymousMethod as AsyncInitializer;
				if (asyncInitializer != null)
				{
					AsyncTaskStorey asyncTaskStorey = (AsyncTaskStorey)asyncInitializer.Storey;
					Label label = asyncInitializer.BodyEnd;
					if (asyncTaskStorey.HoistedReturnValue != null)
					{
						if (ec.TryFinallyUnwind != null)
						{
							if (asyncTaskStorey.HoistedReturnValue is VariableReference)
							{
								asyncTaskStorey.HoistedReturnValue = ec.GetTemporaryField(asyncTaskStorey.HoistedReturnValue.Type, false);
							}
							label = TryFinally.EmitRedirectedReturn(ec, asyncInitializer);
						}
						((IAssignMethod)asyncTaskStorey.HoistedReturnValue).EmitAssign(ec, this.expr, false, false);
						ec.EmitEpilogue();
					}
					else
					{
						this.expr.Emit(ec);
						if (ec.TryFinallyUnwind != null)
						{
							label = TryFinally.EmitRedirectedReturn(ec, asyncInitializer);
						}
					}
					ec.Emit(OpCodes.Leave, label);
					return;
				}
				this.expr.Emit(ec);
				ec.EmitEpilogue();
				if (this.unwind_protect || ec.EmitAccurateDebugInfo)
				{
					ec.Emit(OpCodes.Stloc, ec.TemporaryReturn());
				}
			}
			if (this.unwind_protect)
			{
				ec.Emit(OpCodes.Leave, ec.CreateReturnLabel());
				return;
			}
			if (ec.EmitAccurateDebugInfo)
			{
				ec.Emit(OpCodes.Br, ec.CreateReturnLabel());
				return;
			}
			ec.Emit(OpCodes.Ret);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000A0C1E File Offset: 0x0009EE1E
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.expr != null)
			{
				this.expr.FlowAnalysis(fc);
			}
			base.DoFlowAnalysis(fc);
			return true;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000A0C3D File Offset: 0x0009EE3D
		private void Error_ReturnFromIterator(ResolveContext rc)
		{
			rc.Report.Error(1622, this.loc, "Cannot return a value from iterators. Use the yield return statement to return a value, or yield break to end the iteration");
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0008953C File Offset: 0x0008773C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			return Reachability.CreateUnreachable();
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000A0C5C File Offset: 0x0009EE5C
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Return @return = (Return)t;
			if (this.expr != null)
			{
				@return.expr = this.expr.Clone(clonectx);
			}
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000A0C8A File Offset: 0x0009EE8A
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C2B RID: 3115
		private Expression expr;
	}
}
