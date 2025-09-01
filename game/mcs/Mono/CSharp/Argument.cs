using System;

namespace Mono.CSharp
{
	// Token: 0x020000FB RID: 251
	public class Argument
	{
		// Token: 0x06000C9C RID: 3228 RVA: 0x0002CAED File Offset: 0x0002ACED
		public Argument(Expression expr, Argument.AType type)
		{
			this.Expr = expr;
			this.ArgType = type;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002CB03 File Offset: 0x0002AD03
		public Argument(Expression expr)
		{
			this.Expr = expr;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0002CB12 File Offset: 0x0002AD12
		public bool IsByRef
		{
			get
			{
				return this.ArgType == Argument.AType.Ref || this.ArgType == Argument.AType.Out;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002CB28 File Offset: 0x0002AD28
		public bool IsDefaultArgument
		{
			get
			{
				return this.ArgType == Argument.AType.Default;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002CB33 File Offset: 0x0002AD33
		public bool IsExtensionType
		{
			get
			{
				return (this.ArgType & Argument.AType.ExtensionType) == Argument.AType.ExtensionType;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002CB40 File Offset: 0x0002AD40
		public Parameter.Modifier Modifier
		{
			get
			{
				Argument.AType argType = this.ArgType;
				if (argType == Argument.AType.Ref)
				{
					return Parameter.Modifier.REF;
				}
				if (argType == Argument.AType.Out)
				{
					return Parameter.Modifier.OUT;
				}
				return Parameter.Modifier.NONE;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002CB61 File Offset: 0x0002AD61
		public TypeSpec Type
		{
			get
			{
				return this.Expr.Type;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002CB6E File Offset: 0x0002AD6E
		public Argument Clone(Expression expr)
		{
			Argument argument = (Argument)base.MemberwiseClone();
			argument.Expr = expr;
			return argument;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002CB82 File Offset: 0x0002AD82
		public Argument Clone(CloneContext clonectx)
		{
			return this.Clone(this.Expr.Clone(clonectx));
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002CB96 File Offset: 0x0002AD96
		public virtual Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.ArgType == Argument.AType.Default)
			{
				ec.Report.Error(854, this.Expr.Location, "An expression tree cannot contain an invocation which uses optional parameter");
			}
			return this.Expr.CreateExpressionTree(ec);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		public virtual void Emit(EmitContext ec)
		{
			if (this.IsByRef)
			{
				AddressOp addressOp = AddressOp.Store;
				if (this.ArgType == Argument.AType.Ref)
				{
					addressOp |= AddressOp.Load;
				}
				((IMemoryLocation)this.Expr).AddressOf(ec, addressOp);
				return;
			}
			if (this.ArgType == Argument.AType.ExtensionTypeConditionalAccess)
			{
				InstanceEmitter instanceEmitter = new InstanceEmitter(this.Expr, false);
				instanceEmitter.Emit(ec, true);
				return;
			}
			this.Expr.Emit(ec);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002CC38 File Offset: 0x0002AE38
		public Argument EmitToField(EmitContext ec, bool cloneResult)
		{
			Expression expression = this.Expr.EmitToField(ec);
			if (cloneResult && expression != this.Expr)
			{
				return new Argument(expression, this.ArgType);
			}
			this.Expr = expression;
			return this;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002CC74 File Offset: 0x0002AE74
		public void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.ArgType != Argument.AType.Out)
			{
				this.Expr.FlowAnalysis(fc);
				return;
			}
			VariableReference variableReference = this.Expr as VariableReference;
			if (variableReference != null)
			{
				if (variableReference.VariableInfo != null)
				{
					fc.SetVariableAssigned(variableReference.VariableInfo, false);
				}
				return;
			}
			FieldExpr fieldExpr = this.Expr as FieldExpr;
			if (fieldExpr != null)
			{
				fieldExpr.SetFieldAssigned(fc);
				return;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002CCD3 File Offset: 0x0002AED3
		public string GetSignatureForError()
		{
			if (this.Expr.eclass == ExprClass.MethodGroup)
			{
				return this.Expr.ExprClassName;
			}
			return this.Expr.Type.GetSignatureForError();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002CD00 File Offset: 0x0002AF00
		public bool ResolveMethodGroup(ResolveContext ec)
		{
			SimpleName simpleName = this.Expr as SimpleName;
			if (simpleName != null)
			{
				this.Expr = simpleName.GetMethodGroup();
			}
			this.Expr = this.Expr.Resolve(ec, ResolveFlags.VariableOrValue | ResolveFlags.MethodGroup);
			return this.Expr != null;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002CD48 File Offset: 0x0002AF48
		public void Resolve(ResolveContext ec)
		{
			if (this.ArgType != Argument.AType.Out)
			{
				this.Expr = this.Expr.Resolve(ec);
			}
			if (this.Expr != null && this.IsByRef)
			{
				this.Expr = this.Expr.ResolveLValue(ec, EmptyExpression.OutAccess);
			}
			if (this.Expr == null)
			{
				this.Expr = ErrorExpression.Instance;
			}
		}

		// Token: 0x04000615 RID: 1557
		public readonly Argument.AType ArgType;

		// Token: 0x04000616 RID: 1558
		public Expression Expr;

		// Token: 0x02000375 RID: 885
		public enum AType : byte
		{
			// Token: 0x04000F39 RID: 3897
			None,
			// Token: 0x04000F3A RID: 3898
			Ref,
			// Token: 0x04000F3B RID: 3899
			Out,
			// Token: 0x04000F3C RID: 3900
			Default,
			// Token: 0x04000F3D RID: 3901
			DynamicTypeName,
			// Token: 0x04000F3E RID: 3902
			ExtensionType,
			// Token: 0x04000F3F RID: 3903
			ExtensionTypeConditionalAccess = 133,
			// Token: 0x04000F40 RID: 3904
			ConditionalAccessFlag = 128
		}
	}
}
