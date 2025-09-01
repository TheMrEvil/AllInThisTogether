using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200010F RID: 271
	public class AnonymousTypeClass : CompilerGeneratedContainer
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x00031958 File Offset: 0x0002FB58
		private AnonymousTypeClass(ModuleContainer parent, MemberName name, IList<AnonymousTypeParameter> parameters, Location loc) : base(parent, name, (parent.Evaluator != null) ? Modifiers.PUBLIC : Modifiers.INTERNAL)
		{
			this.parameters = parameters;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00031978 File Offset: 0x0002FB78
		public static AnonymousTypeClass Create(TypeContainer parent, IList<AnonymousTypeParameter> parameters, Location loc)
		{
			object arg = "<>__AnonType";
			ModuleContainer module = parent.Module;
			int counterAnonymousTypes = module.CounterAnonymousTypes;
			module.CounterAnonymousTypes = counterAnonymousTypes + 1;
			string name = arg + counterAnonymousTypes;
			TypeParameters typeParameters = null;
			ParametersCompiled args;
			SimpleName[] array;
			if (parameters.Count == 0)
			{
				args = ParametersCompiled.EmptyReadOnlyParameters;
				array = null;
			}
			else
			{
				array = new SimpleName[parameters.Count];
				typeParameters = new TypeParameters();
				Parameter[] array2 = new Parameter[parameters.Count];
				for (int i = 0; i < parameters.Count; i++)
				{
					AnonymousTypeParameter anonymousTypeParameter = parameters[i];
					for (int j = 0; j < i; j++)
					{
						if (parameters[j].Name == anonymousTypeParameter.Name)
						{
							parent.Compiler.Report.Error(833, parameters[j].Location, "`{0}': An anonymous type cannot have multiple properties with the same name", anonymousTypeParameter.Name);
							anonymousTypeParameter = new AnonymousTypeParameter(null, "$" + i.ToString(), anonymousTypeParameter.Location);
							parameters[i] = anonymousTypeParameter;
							break;
						}
					}
					array[i] = new SimpleName("<" + anonymousTypeParameter.Name + ">__T", anonymousTypeParameter.Location);
					typeParameters.Add(new TypeParameter(i, new MemberName(array[i].Name, anonymousTypeParameter.Location), null, null, Variance.None));
					array2[i] = new Parameter(array[i], anonymousTypeParameter.Name, Parameter.Modifier.NONE, null, anonymousTypeParameter.Location);
				}
				args = new ParametersCompiled(array2);
			}
			AnonymousTypeClass anonymousTypeClass = new AnonymousTypeClass(parent.Module, new MemberName(name, typeParameters, loc), parameters, loc);
			Constructor constructor = new Constructor(anonymousTypeClass, name, Modifiers.PUBLIC | Modifiers.DEBUGGER_HIDDEN, null, args, loc);
			constructor.Block = new ToplevelBlock(parent.Module.Compiler, constructor.ParameterInfo, loc, (Block.Flags)0);
			bool flag = false;
			for (int k = 0; k < parameters.Count; k++)
			{
				AnonymousTypeParameter anonymousTypeParameter2 = parameters[k];
				Field field = new Field(anonymousTypeClass, array[k], Modifiers.PRIVATE | Modifiers.READONLY | Modifiers.DEBUGGER_HIDDEN, new MemberName("<" + anonymousTypeParameter2.Name + ">", anonymousTypeParameter2.Location), null);
				if (!anonymousTypeClass.AddField(field))
				{
					flag = true;
				}
				else
				{
					constructor.Block.AddStatement(new StatementExpression(new SimpleAssign(new MemberAccess(new This(anonymousTypeParameter2.Location), field.Name), constructor.Block.GetParameterReference(k, anonymousTypeParameter2.Location))));
					ToplevelBlock toplevelBlock = new ToplevelBlock(parent.Module.Compiler, anonymousTypeParameter2.Location);
					toplevelBlock.AddStatement(new Return(new MemberAccess(new This(anonymousTypeParameter2.Location), field.Name), anonymousTypeParameter2.Location));
					Property property = new Property(anonymousTypeClass, array[k], Modifiers.PUBLIC, new MemberName(anonymousTypeParameter2.Name, anonymousTypeParameter2.Location), null);
					Property property2 = property;
					property2.Get = new PropertyBase.GetMethod(property2, (Modifiers)0, null, anonymousTypeParameter2.Location);
					property.Get.Block = toplevelBlock;
					anonymousTypeClass.AddMember(property);
				}
			}
			if (flag)
			{
				return null;
			}
			anonymousTypeClass.AddConstructor(constructor);
			return anonymousTypeClass;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		protected override bool DoDefineMembers()
		{
			if (!base.DoDefineMembers())
			{
				return false;
			}
			Location location = base.Location;
			ParametersCompiled parametersCompiled = ParametersCompiled.CreateFullyResolved(new Parameter(new TypeExpression(this.Compiler.BuiltinTypes.Object, location), "obj", Parameter.Modifier.NONE, null, location), this.Compiler.BuiltinTypes.Object);
			Method method = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.Bool, location), Modifiers.PUBLIC | Modifiers.OVERRIDE | Modifiers.DEBUGGER_HIDDEN, new MemberName("Equals", location), parametersCompiled, null);
			parametersCompiled[0].Resolve(method, 0);
			Method method2 = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.String, location), Modifiers.PUBLIC | Modifiers.OVERRIDE | Modifiers.DEBUGGER_HIDDEN, new MemberName("ToString", location), ParametersCompiled.EmptyReadOnlyParameters, null);
			ToplevelBlock toplevelBlock = new ToplevelBlock(this.Compiler, method.ParameterInfo, location, (Block.Flags)0);
			TypeExpr probe_type;
			if (this.CurrentTypeParameters != null)
			{
				TypeArguments typeArguments = new TypeArguments(new FullNamedExpression[0]);
				for (int i = 0; i < this.CurrentTypeParameters.Count; i++)
				{
					typeArguments.Add(new TypeParameterExpr(this.CurrentTypeParameters[i], base.Location));
				}
				probe_type = new GenericTypeExpr(base.Definition, typeArguments, location);
			}
			else
			{
				probe_type = new TypeExpression(base.Definition, location);
			}
			LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(this.CurrentType, toplevelBlock, location);
			toplevelBlock.AddStatement(new BlockVariable(new TypeExpression(localVariable.Type, location), localVariable));
			LocalVariableReference localVariableReference = new LocalVariableReference(localVariable, location);
			MemberAccess expr = new MemberAccess(new MemberAccess(new QualifiedAliasMember("global", "System", location), "Collections", location), "Generic", location);
			Expression expression = null;
			Expression expression2 = new StringConstant(this.Compiler.BuiltinTypes, "{", location);
			Expression expression3 = new IntConstant(this.Compiler.BuiltinTypes, -2128831035, location);
			for (int j = 0; j < this.parameters.Count; j++)
			{
				AnonymousTypeParameter anonymousTypeParameter = this.parameters[j];
				Field field = (Field)base.Members[j * 2];
				MemberAccess expr2 = new MemberAccess(new MemberAccess(expr, "EqualityComparer", new TypeArguments(new FullNamedExpression[]
				{
					new SimpleName(this.CurrentTypeParameters[j].Name, location)
				}), location), "Default", location);
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(new MemberAccess(new This(field.Location), field.Name)));
				arguments.Add(new Argument(new MemberAccess(localVariableReference, field.Name)));
				Expression expression4 = new Invocation(new MemberAccess(expr2, "Equals", location), arguments);
				Arguments arguments2 = new Arguments(1);
				arguments2.Add(new Argument(new MemberAccess(new This(field.Location), field.Name)));
				Expression right = new Invocation(new MemberAccess(expr2, "GetHashCode", location), arguments2);
				IntConstant right2 = new IntConstant(this.Compiler.BuiltinTypes, 16777619, location);
				expression3 = new Binary(Binary.Operator.Multiply, new Binary(Binary.Operator.ExclusiveOr, expression3, right), right2);
				Expression right3 = new Conditional(new BooleanExpression(new Binary(Binary.Operator.Inequality, new MemberAccess(new This(field.Location), field.Name), new NullLiteral(location))), new Invocation(new MemberAccess(new MemberAccess(new This(field.Location), field.Name), "ToString"), null), new StringConstant(this.Compiler.BuiltinTypes, string.Empty, location), location);
				if (expression == null)
				{
					expression = expression4;
					expression2 = new Binary(Binary.Operator.Addition, expression2, new Binary(Binary.Operator.Addition, new StringConstant(this.Compiler.BuiltinTypes, " " + anonymousTypeParameter.Name + " = ", location), right3));
				}
				else
				{
					expression2 = new Binary(Binary.Operator.Addition, new Binary(Binary.Operator.Addition, expression2, new StringConstant(this.Compiler.BuiltinTypes, ", " + anonymousTypeParameter.Name + " = ", location)), right3);
					expression = new Binary(Binary.Operator.LogicalAnd, expression, expression4);
				}
			}
			expression2 = new Binary(Binary.Operator.Addition, expression2, new StringConstant(this.Compiler.BuiltinTypes, " }", location));
			TemporaryVariableReference target = new TemporaryVariableReference(localVariable, location);
			toplevelBlock.AddStatement(new StatementExpression(new SimpleAssign(target, new As(toplevelBlock.GetParameterReference(0, location), probe_type, location), location)));
			Expression expression5 = new Binary(Binary.Operator.Inequality, localVariableReference, new NullLiteral(location));
			if (expression != null)
			{
				expression5 = new Binary(Binary.Operator.LogicalAnd, expression5, expression);
			}
			toplevelBlock.AddStatement(new Return(expression5, location));
			method.Block = toplevelBlock;
			method.Define();
			base.Members.Add(method);
			Method method3 = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.Int, location), Modifiers.PUBLIC | Modifiers.OVERRIDE | Modifiers.DEBUGGER_HIDDEN, new MemberName("GetHashCode", location), ParametersCompiled.EmptyReadOnlyParameters, null);
			ToplevelBlock toplevelBlock2 = new ToplevelBlock(this.Compiler, location);
			Block parent = toplevelBlock2;
			Location location2 = location;
			Block block = new Block(parent, location2, location2);
			toplevelBlock2.AddStatement(new Unchecked(block, location));
			LocalVariable localVariable2 = LocalVariable.CreateCompilerGenerated(this.Compiler.BuiltinTypes.Int, toplevelBlock2, location);
			block.AddStatement(new BlockVariable(new TypeExpression(localVariable2.Type, location), localVariable2));
			LocalVariableReference target2 = new LocalVariableReference(localVariable2, location);
			block.AddStatement(new StatementExpression(new SimpleAssign(target2, expression3)));
			LocalVariableReference localVariableReference2 = new LocalVariableReference(localVariable2, location);
			block.AddStatement(new StatementExpression(new CompoundAssign(Binary.Operator.Addition, localVariableReference2, new Binary(Binary.Operator.LeftShift, localVariableReference2, new IntConstant(this.Compiler.BuiltinTypes, 13, location)))));
			block.AddStatement(new StatementExpression(new CompoundAssign(Binary.Operator.ExclusiveOr, localVariableReference2, new Binary(Binary.Operator.RightShift, localVariableReference2, new IntConstant(this.Compiler.BuiltinTypes, 7, location)))));
			block.AddStatement(new StatementExpression(new CompoundAssign(Binary.Operator.Addition, localVariableReference2, new Binary(Binary.Operator.LeftShift, localVariableReference2, new IntConstant(this.Compiler.BuiltinTypes, 3, location)))));
			block.AddStatement(new StatementExpression(new CompoundAssign(Binary.Operator.ExclusiveOr, localVariableReference2, new Binary(Binary.Operator.RightShift, localVariableReference2, new IntConstant(this.Compiler.BuiltinTypes, 17, location)))));
			block.AddStatement(new StatementExpression(new CompoundAssign(Binary.Operator.Addition, localVariableReference2, new Binary(Binary.Operator.LeftShift, localVariableReference2, new IntConstant(this.Compiler.BuiltinTypes, 5, location)))));
			block.AddStatement(new Return(localVariableReference2, location));
			method3.Block = toplevelBlock2;
			method3.Define();
			base.Members.Add(method3);
			ToplevelBlock toplevelBlock3 = new ToplevelBlock(this.Compiler, location);
			toplevelBlock3.AddStatement(new Return(expression2, location));
			method2.Block = toplevelBlock3;
			method2.Define();
			base.Members.Add(method2);
			return true;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000323A3 File Offset: 0x000305A3
		public override string GetSignatureForError()
		{
			return "anonymous type";
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000055E7 File Offset: 0x000037E7
		public override CompilationSourceFile GetCompilationSourceFile()
		{
			return null;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000323AA File Offset: 0x000305AA
		public IList<AnonymousTypeParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0400065E RID: 1630
		public const string ClassNamePrefix = "<>__AnonType";

		// Token: 0x0400065F RID: 1631
		public const string SignatureForError = "anonymous type";

		// Token: 0x04000660 RID: 1632
		private readonly IList<AnonymousTypeParameter> parameters;
	}
}
