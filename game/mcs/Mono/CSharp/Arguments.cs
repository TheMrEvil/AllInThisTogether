using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020000FE RID: 254
	public class Arguments
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002CEAB File Offset: 0x0002B0AB
		public Arguments(int capacity)
		{
			this.args = new List<Argument>(capacity);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002CEBF File Offset: 0x0002B0BF
		private Arguments(List<Argument> args)
		{
			this.args = args;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002CECE File Offset: 0x0002B0CE
		public void Add(Argument arg)
		{
			this.args.Add(arg);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002CEDC File Offset: 0x0002B0DC
		public void AddRange(Arguments args)
		{
			this.args.AddRange(args.args);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public bool ContainsEmitWithAwait()
		{
			using (List<Argument>.Enumerator enumerator = this.args.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Expr.ContainsEmitWithAwait())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002CF50 File Offset: 0x0002B150
		public ArrayInitializer CreateDynamicBinderArguments(ResolveContext rc)
		{
			Location @null = Location.Null;
			ArrayInitializer arrayInitializer = new ArrayInitializer(this.args.Count, @null);
			MemberAccess binderNamespace = DynamicExpressionStatement.GetBinderNamespace(@null);
			foreach (Argument argument in this.args)
			{
				Arguments arguments = new Arguments(2);
				Expression expression = new IntLiteral(rc.BuiltinTypes, 0, @null);
				if (argument.Expr is Constant)
				{
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "Constant", @null));
				}
				else if (argument.ArgType == Argument.AType.Ref)
				{
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "IsRef", @null));
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "UseCompileTimeType", @null));
				}
				else if (argument.ArgType == Argument.AType.Out)
				{
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "IsOut", @null));
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "UseCompileTimeType", @null));
				}
				else if (argument.ArgType == Argument.AType.DynamicTypeName)
				{
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "IsStaticType", @null));
				}
				TypeSpec type = argument.Expr.Type;
				if (type.BuiltinType != BuiltinTypeSpec.Type.Dynamic && type != InternalType.NullLiteral)
				{
					MethodGroupExpr methodGroupExpr = argument.Expr as MethodGroupExpr;
					if (methodGroupExpr != null)
					{
						rc.Report.Error(1976, argument.Expr.Location, "The method group `{0}' cannot be used as an argument of dynamic operation. Consider using parentheses to invoke the method", methodGroupExpr.Name);
					}
					else if (type == InternalType.AnonymousMethod)
					{
						rc.Report.Error(1977, argument.Expr.Location, "An anonymous method or lambda expression cannot be used as an argument of dynamic operation. Consider using a cast");
					}
					else if (type.Kind == MemberKind.Void || type == InternalType.Arglist || type.IsPointer)
					{
						rc.Report.Error(1978, argument.Expr.Location, "An expression of type `{0}' cannot be used as an argument of dynamic operation", type.GetSignatureForError());
					}
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "UseCompileTimeType", @null));
				}
				NamedArgument namedArgument = argument as NamedArgument;
				string s;
				if (namedArgument != null)
				{
					expression = new Binary(Binary.Operator.BitwiseOr, expression, new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfoFlags", @null), "NamedArgument", @null));
					s = namedArgument.Name;
				}
				else
				{
					s = null;
				}
				arguments.Add(new Argument(expression));
				arguments.Add(new Argument(new StringLiteral(rc.BuiltinTypes, s, @null)));
				arrayInitializer.Add(new Invocation(new MemberAccess(new MemberAccess(binderNamespace, "CSharpArgumentInfo", @null), "Create", @null), arguments));
			}
			return arrayInitializer;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002D288 File Offset: 0x0002B488
		public static Arguments CreateForExpressionTree(ResolveContext ec, Arguments args, params Expression[] e)
		{
			Arguments arguments = new Arguments(((args == null) ? 0 : args.Count) + e.Length);
			for (int i = 0; i < e.Length; i++)
			{
				if (e[i] != null)
				{
					arguments.Add(new Argument(e[i]));
				}
			}
			if (args != null)
			{
				foreach (Argument argument in args.args)
				{
					Expression expression = argument.CreateExpressionTree(ec);
					if (expression != null)
					{
						arguments.Add(new Argument(expression));
					}
				}
			}
			return arguments;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002D324 File Offset: 0x0002B524
		public void CheckArrayAsAttribute(CompilerContext ctx)
		{
			foreach (Argument argument in this.args)
			{
				if (argument.Type != null && argument.Type.IsArray)
				{
					ctx.Report.Warning(3016, 1, argument.Expr.Location, "Arrays as attribute arguments are not CLS-compliant");
				}
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		public Arguments Clone(CloneContext ctx)
		{
			Arguments arguments = new Arguments(this.args.Count);
			foreach (Argument argument in this.args)
			{
				arguments.Add(argument.Clone(ctx));
			}
			return arguments;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002D414 File Offset: 0x0002B614
		public int Count
		{
			get
			{
				return this.args.Count;
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002D421 File Offset: 0x0002B621
		public void Emit(EmitContext ec)
		{
			this.Emit(ec, false, false);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002D430 File Offset: 0x0002B630
		public virtual Arguments Emit(EmitContext ec, bool dup_args, bool prepareAwait)
		{
			List<Argument> list;
			if ((dup_args && this.Count != 0) || prepareAwait)
			{
				list = new List<Argument>(this.Count);
			}
			else
			{
				list = null;
			}
			foreach (Argument argument in this.args)
			{
				if (prepareAwait)
				{
					list.Add(argument.EmitToField(ec, true));
				}
				else
				{
					argument.Emit(ec);
					if (dup_args)
					{
						if (argument.Expr.IsSideEffectFree)
						{
							list.Add(argument);
						}
						else
						{
							ec.Emit(OpCodes.Dup);
							LocalTemporary localTemporary = new LocalTemporary(argument.Type);
							localTemporary.Store(ec);
							list.Add(new Argument(localTemporary, argument.ArgType));
						}
					}
				}
			}
			if (list != null)
			{
				return new Arguments(list);
			}
			return null;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002D510 File Offset: 0x0002B710
		public virtual void FlowAnalysis(FlowAnalysisContext fc, List<MovableArgument> movable = null)
		{
			bool flag = false;
			foreach (Argument argument in this.args)
			{
				if (argument.ArgType == Argument.AType.Out)
				{
					flag = true;
				}
				else if (movable == null)
				{
					argument.FlowAnalysis(fc);
				}
				else
				{
					MovableArgument movableArgument = argument as MovableArgument;
					if (movableArgument != null && !movable.Contains(movableArgument))
					{
						argument.FlowAnalysis(fc);
					}
				}
			}
			if (!flag)
			{
				return;
			}
			foreach (Argument argument2 in this.args)
			{
				if (argument2.ArgType == Argument.AType.Out)
				{
					argument2.FlowAnalysis(fc);
				}
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		public List<Argument>.Enumerator GetEnumerator()
		{
			return this.args.GetEnumerator();
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		public bool HasDynamic
		{
			get
			{
				foreach (Argument argument in this.args)
				{
					if (argument.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic && !argument.IsByRef)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002D660 File Offset: 0x0002B860
		public bool HasNamed
		{
			get
			{
				using (List<Argument>.Enumerator enumerator = this.args.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is NamedArgument)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002D6BC File Offset: 0x0002B8BC
		public void Insert(int index, Argument arg)
		{
			this.args.Insert(index, arg);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002D6CC File Offset: 0x0002B8CC
		public static Expression[] MakeExpression(Arguments args, BuilderContext ctx)
		{
			if (args == null || args.Count == 0)
			{
				return null;
			}
			Expression[] array = new Expression[args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Argument argument = args.args[i];
				array[i] = argument.Expr.MakeExpression(ctx);
			}
			return array;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002D720 File Offset: 0x0002B920
		public Arguments MarkOrderedArgument(NamedArgument a)
		{
			if (a.Expr.IsSideEffectFree)
			{
				return this;
			}
			Arguments.ArgumentsOrdered argumentsOrdered = this as Arguments.ArgumentsOrdered;
			if (argumentsOrdered == null)
			{
				argumentsOrdered = new Arguments.ArgumentsOrdered(this);
				for (int i = 0; i < this.args.Count; i++)
				{
					Argument argument = this.args[i];
					if (argument == a)
					{
						break;
					}
					if (argument != null)
					{
						MovableArgument movableArgument = argument as MovableArgument;
						if (movableArgument == null)
						{
							movableArgument = new MovableArgument(argument);
							argumentsOrdered.args[i] = movableArgument;
						}
						argumentsOrdered.AddOrdered(movableArgument);
					}
				}
			}
			argumentsOrdered.AddOrdered(a);
			return argumentsOrdered;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002D7A4 File Offset: 0x0002B9A4
		public void Resolve(ResolveContext ec, out bool dynamic)
		{
			dynamic = false;
			foreach (Argument argument in this.args)
			{
				argument.Resolve(ec);
				if (argument.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic && !argument.IsByRef)
				{
					dynamic = true;
				}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002D814 File Offset: 0x0002BA14
		public void RemoveAt(int index)
		{
			this.args.RemoveAt(index);
		}

		// Token: 0x170003BC RID: 956
		public Argument this[int index]
		{
			get
			{
				return this.args[index];
			}
			set
			{
				this.args[index] = value;
			}
		}

		// Token: 0x0400061A RID: 1562
		private List<Argument> args;

		// Token: 0x02000376 RID: 886
		private sealed class ArgumentsOrdered : Arguments
		{
			// Token: 0x0600267A RID: 9850 RVA: 0x000B6774 File Offset: 0x000B4974
			public ArgumentsOrdered(Arguments args) : base(args.Count)
			{
				base.AddRange(args);
				this.ordered = new List<MovableArgument>();
			}

			// Token: 0x0600267B RID: 9851 RVA: 0x000B6794 File Offset: 0x000B4994
			public void AddOrdered(MovableArgument arg)
			{
				this.ordered.Add(arg);
			}

			// Token: 0x0600267C RID: 9852 RVA: 0x000B67A4 File Offset: 0x000B49A4
			public override void FlowAnalysis(FlowAnalysisContext fc, List<MovableArgument> movable = null)
			{
				foreach (MovableArgument movableArgument in this.ordered)
				{
					if (movableArgument.ArgType != Argument.AType.Out)
					{
						movableArgument.FlowAnalysis(fc);
					}
				}
				base.FlowAnalysis(fc, this.ordered);
			}

			// Token: 0x0600267D RID: 9853 RVA: 0x000B6810 File Offset: 0x000B4A10
			public override Arguments Emit(EmitContext ec, bool dup_args, bool prepareAwait)
			{
				foreach (MovableArgument movableArgument in this.ordered)
				{
					if (prepareAwait)
					{
						movableArgument.EmitToField(ec, false);
					}
					else
					{
						movableArgument.EmitToVariable(ec);
					}
				}
				return base.Emit(ec, dup_args, prepareAwait);
			}

			// Token: 0x04000F41 RID: 3905
			private readonly List<MovableArgument> ordered;
		}
	}
}
