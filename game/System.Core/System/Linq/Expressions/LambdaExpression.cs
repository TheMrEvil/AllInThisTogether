using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Describes a lambda expression. This captures a block of code that is similar to a .NET method body.</summary>
	// Token: 0x0200026B RID: 619
	[DebuggerTypeProxy(typeof(Expression.LambdaExpressionProxy))]
	public abstract class LambdaExpression : Expression, IParameterProvider
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x0003AEB1 File Offset: 0x000390B1
		internal LambdaExpression(Expression body)
		{
			this._body = body;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LambdaExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0003AEC0 File Offset: 0x000390C0
		public sealed override Type Type
		{
			get
			{
				return this.TypeCore;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600120A RID: 4618
		internal abstract Type TypeCore { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600120B RID: 4619
		internal abstract Type PublicType { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0003AEC8 File Offset: 0x000390C8
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Lambda;
			}
		}

		/// <summary>Gets the parameters of the lambda expression.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects that represent the parameters of the lambda expression.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0003AECC File Offset: 0x000390CC
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this.GetOrMakeParameters();
			}
		}

		/// <summary>Gets the name of the lambda expression.</summary>
		/// <returns>The name of the lambda expression.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0003AED4 File Offset: 0x000390D4
		public string Name
		{
			get
			{
				return this.NameCore;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0000392D File Offset: 0x00001B2D
		internal virtual string NameCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the body of the lambda expression.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the body of the lambda expression.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0003AEDC File Offset: 0x000390DC
		public Expression Body
		{
			get
			{
				return this._body;
			}
		}

		/// <summary>Gets the return type of the lambda expression.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the lambda expression.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0003AEE4 File Offset: 0x000390E4
		public Type ReturnType
		{
			get
			{
				return this.Type.GetInvokeMethod().ReturnType;
			}
		}

		/// <summary>Gets the value that indicates if the lambda expression will be compiled with the tail call optimization.</summary>
		/// <returns>True if the lambda expression will be compiled with the tail call optimization, otherwise false.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0003AEF6 File Offset: 0x000390F6
		public bool TailCall
		{
			get
			{
				return this.TailCallCore;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000023D1 File Offset: 0x000005D1
		internal virtual bool TailCallCore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003AEFE File Offset: 0x000390FE
		[ExcludeFromCodeCoverage]
		ParameterExpression IParameterProvider.GetParameter(int index)
		{
			return this.GetParameter(index);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual ParameterExpression GetParameter(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0003AF07 File Offset: 0x00039107
		[ExcludeFromCodeCoverage]
		int IParameterProvider.ParameterCount
		{
			get
			{
				return this.ParameterCount;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		internal virtual int ParameterCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Produces a delegate that represents the lambda expression.</summary>
		/// <returns>A <see cref="T:System.Delegate" /> that contains the compiled version of the lambda expression.</returns>
		// Token: 0x06001219 RID: 4633 RVA: 0x0003AF0F File Offset: 0x0003910F
		public Delegate Compile()
		{
			return this.Compile(false);
		}

		/// <summary>Produces an interpreted or compiled delegate that represents the lambda expression. </summary>
		/// <param name="preferInterpretation">
		///   <see langword="true" /> to indicate that the expression should be compiled to an interpreted form, if it's available; otherwise, <see langword="false" />.</param>
		/// <returns>A delegate that represents the compiled lambda expression described by the <see cref="T:System.Linq.Expressions.LambdaExpression" /> object.</returns>
		// Token: 0x0600121A RID: 4634 RVA: 0x0003AF18 File Offset: 0x00039118
		public Delegate Compile(bool preferInterpretation)
		{
			return LambdaCompiler.Compile(this);
		}

		/// <summary>Compiles the lambda into a method definition.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.Emit.MethodBuilder" /> which will be used to hold the lambda's IL.</param>
		// Token: 0x0600121B RID: 4635 RVA: 0x0003AF20 File Offset: 0x00039120
		public void CompileToMethod(MethodBuilder method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.Requires(method.IsStatic, "method");
			if (method.DeclaringType as TypeBuilder == null)
			{
				throw Error.MethodBuilderDoesNotHaveTypeBuilder();
			}
			LambdaCompiler.Compile(this, method);
		}

		// Token: 0x0600121C RID: 4636
		internal abstract LambdaExpression Accept(StackSpiller spiller);

		/// <summary>Produces a delegate that represents the lambda expression.</summary>
		/// <param name="debugInfoGenerator">Debugging information generator used by the compiler to mark sequence points and annotate local variables.</param>
		/// <returns>A delegate containing the compiled version of the lambda.</returns>
		// Token: 0x0600121D RID: 4637 RVA: 0x0003AF5D File Offset: 0x0003915D
		public Delegate Compile(DebugInfoGenerator debugInfoGenerator)
		{
			return this.Compile();
		}

		/// <summary>Compiles the lambda into a method definition and custom debug information.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.Emit.MethodBuilder" /> which will be used to hold the lambda's IL.</param>
		/// <param name="debugInfoGenerator">Debugging information generator used by the compiler to mark sequence points and annotate local variables.</param>
		// Token: 0x0600121E RID: 4638 RVA: 0x0003AF65 File Offset: 0x00039165
		public void CompileToMethod(MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
		{
			this.CompileToMethod(method);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0000235B File Offset: 0x0000055B
		internal LambdaExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A0D RID: 2573
		private readonly Expression _body;
	}
}
