using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000270 RID: 624
	public class ParametersCompiled : AParametersCollection
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x00096C61 File Offset: 0x00094E61
		private ParametersCompiled()
		{
			this.parameters = new Parameter[0];
			this.types = TypeSpec.EmptyTypes;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00096C80 File Offset: 0x00094E80
		private ParametersCompiled(IParameterData[] parameters, TypeSpec[] types)
		{
			this.parameters = parameters;
			this.types = types;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00096C98 File Offset: 0x00094E98
		public ParametersCompiled(params Parameter[] parameters)
		{
			if (parameters == null || parameters.Length == 0)
			{
				throw new ArgumentException("Use EmptyReadOnlyParameters");
			}
			this.parameters = parameters;
			int num = parameters.Length;
			for (int i = 0; i < num; i++)
			{
				this.has_params |= ((parameters[i].ModFlags & Parameter.Modifier.PARAMS) > Parameter.Modifier.NONE);
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00096CEE File Offset: 0x00094EEE
		public ParametersCompiled(Parameter[] parameters, bool has_arglist) : this(parameters)
		{
			this.has_arglist = has_arglist;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00096CFE File Offset: 0x00094EFE
		public static ParametersCompiled CreateFullyResolved(Parameter p, TypeSpec type)
		{
			return new ParametersCompiled(new Parameter[]
			{
				p
			}, new TypeSpec[]
			{
				type
			});
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00096D19 File Offset: 0x00094F19
		public static ParametersCompiled CreateFullyResolved(Parameter[] parameters, TypeSpec[] types)
		{
			return new ParametersCompiled(parameters, types);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00096D24 File Offset: 0x00094F24
		public static ParametersCompiled Prefix(ParametersCompiled parameters, Parameter p, TypeSpec type)
		{
			TypeSpec[] array = new TypeSpec[parameters.Count + 1];
			array[0] = type;
			Array.Copy(parameters.Types, 0, array, 1, parameters.Count);
			Parameter[] array2 = new Parameter[array.Length];
			array2[0] = p;
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				array2[i + 1] = parameter;
				parameter.SetIndex(i + 1);
			}
			return ParametersCompiled.CreateFullyResolved(array2, array);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00096D94 File Offset: 0x00094F94
		public static AParametersCollection CreateFullyResolved(params TypeSpec[] types)
		{
			ParameterData[] array = new ParameterData[types.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ParameterData(null, Parameter.Modifier.NONE, null);
			}
			return new ParametersCompiled(array, types);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00096DCA File Offset: 0x00094FCA
		public static ParametersCompiled CreateImplicitParameter(FullNamedExpression texpr, Location loc)
		{
			return new ParametersCompiled(new Parameter[]
			{
				new Parameter(texpr, "value", Parameter.Modifier.NONE, null, loc)
			}, null);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00096DEC File Offset: 0x00094FEC
		public void CheckConstraints(IMemberContext mc)
		{
			foreach (Parameter parameter in this.parameters)
			{
				if (parameter.TypeExpression != null)
				{
					ConstraintChecker.Check(mc, parameter.Type, parameter.TypeExpression.Location);
				}
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00096E38 File Offset: 0x00095038
		public static int IsSameClsSignature(AParametersCollection a, AParametersCollection b)
		{
			int num = 0;
			for (int i = 0; i < a.Count; i++)
			{
				TypeSpec typeSpec = a.Types[i];
				TypeSpec typeSpec2 = b.Types[i];
				if (TypeSpecComparer.Override.IsEqual(typeSpec, typeSpec2))
				{
					if ((a.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != (b.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask))
					{
						num |= 1;
					}
				}
				else
				{
					ArrayContainer arrayContainer = typeSpec as ArrayContainer;
					if (arrayContainer == null)
					{
						return 0;
					}
					ArrayContainer arrayContainer2 = typeSpec2 as ArrayContainer;
					if (arrayContainer2 == null)
					{
						return 0;
					}
					if (arrayContainer.Element is ArrayContainer || arrayContainer2.Element is ArrayContainer)
					{
						num |= 2;
					}
					else
					{
						if (arrayContainer.Rank == arrayContainer2.Rank || !TypeSpecComparer.Override.IsEqual(arrayContainer.Element, arrayContainer2.Element))
						{
							return 0;
						}
						num |= 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00096F0B File Offset: 0x0009510B
		public static ParametersCompiled MergeGenerated(CompilerContext ctx, ParametersCompiled userParams, bool checkConflicts, Parameter compilerParams, TypeSpec compilerTypes)
		{
			return ParametersCompiled.MergeGenerated(ctx, userParams, checkConflicts, new Parameter[]
			{
				compilerParams
			}, new TypeSpec[]
			{
				compilerTypes
			});
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00096F2C File Offset: 0x0009512C
		public static ParametersCompiled MergeGenerated(CompilerContext ctx, ParametersCompiled userParams, bool checkConflicts, Parameter[] compilerParams, TypeSpec[] compilerTypes)
		{
			Parameter[] array = new Parameter[userParams.Count + compilerParams.Length];
			userParams.FixedParameters.CopyTo(array, 0);
			TypeSpec[] array2;
			if (userParams.types != null)
			{
				array2 = new TypeSpec[array.Length];
				userParams.Types.CopyTo(array2, 0);
			}
			else
			{
				array2 = null;
			}
			int num = userParams.Count;
			int num2 = 0;
			foreach (Parameter parameter in compilerParams)
			{
				for (int j = 0; j < num; j++)
				{
					while (parameter.Name == array[j].Name)
					{
						if (checkConflicts && j < userParams.Count)
						{
							ctx.Report.Error(316, userParams[j].Location, "The parameter name `{0}' conflicts with a compiler generated name", parameter.Name);
						}
						parameter.Name = "_" + parameter.Name;
					}
				}
				array[num] = parameter;
				if (array2 != null)
				{
					array2[num] = compilerTypes[num2++];
				}
				num++;
			}
			return new ParametersCompiled(array, array2)
			{
				has_params = userParams.has_params
			};
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00097044 File Offset: 0x00095244
		public void CheckParameters(MemberCore member)
		{
			for (int i = 0; i < this.parameters.Length; i++)
			{
				string name = this.parameters[i].Name;
				for (int j = i + 1; j < this.parameters.Length; j++)
				{
					if (this.parameters[j].Name == name)
					{
						this[j].Error_DuplicateName(member.Compiler.Report);
					}
				}
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000970B4 File Offset: 0x000952B4
		public bool Resolve(IMemberContext ec)
		{
			if (this.types != null)
			{
				return true;
			}
			this.types = new TypeSpec[base.Count];
			bool result = true;
			for (int i = 0; i < base.FixedParameters.Length; i++)
			{
				TypeSpec typeSpec = this[i].Resolve(ec, i);
				if (typeSpec == null)
				{
					result = false;
				}
				else
				{
					this.types[i] = typeSpec;
				}
			}
			return result;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00097114 File Offset: 0x00095314
		public void ResolveDefaultValues(MemberCore m)
		{
			ResolveContext resolveContext = null;
			for (int i = 0; i < this.parameters.Length; i++)
			{
				Parameter parameter = (Parameter)this.parameters[i];
				if (parameter.HasDefaultValue || parameter.OptAttributes != null)
				{
					if (resolveContext == null)
					{
						resolveContext = new ResolveContext(m);
					}
					parameter.ResolveDefaultValue(resolveContext);
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00097168 File Offset: 0x00095368
		public void ApplyAttributes(IMemberContext mc, MethodBase builder)
		{
			if (base.Count == 0)
			{
				return;
			}
			MethodBuilder mb = builder as MethodBuilder;
			ConstructorBuilder cb = builder as ConstructorBuilder;
			PredefinedAttributes predefinedAttributes = mc.Module.PredefinedAttributes;
			for (int i = 0; i < base.Count; i++)
			{
				this[i].ApplyAttributes(mb, cb, i + 1, predefinedAttributes);
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000971BC File Offset: 0x000953BC
		public void VerifyClsCompliance(IMemberContext ctx)
		{
			IParameterData[] fixedParameters = base.FixedParameters;
			for (int i = 0; i < fixedParameters.Length; i++)
			{
				((Parameter)fixedParameters[i]).IsClsCompliant(ctx);
			}
		}

		// Token: 0x1700070B RID: 1803
		public Parameter this[int pos]
		{
			get
			{
				return (Parameter)this.parameters[pos];
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000971FC File Offset: 0x000953FC
		public Expression CreateExpressionTree(BlockContext ec, Location loc)
		{
			ArrayInitializer arrayInitializer = new ArrayInitializer(base.Count, loc);
			foreach (Parameter parameter in base.FixedParameters)
			{
				StatementExpression statementExpression = new StatementExpression(parameter.CreateExpressionTreeVariable(ec), Location.Null);
				if (statementExpression.Resolve(ec))
				{
					ec.CurrentBlock.AddScopeStatement(new TemporaryVariableReference.Declarator(parameter.ExpressionTreeVariableReference()));
					ec.CurrentBlock.AddScopeStatement(statementExpression);
				}
				arrayInitializer.Add(parameter.ExpressionTreeVariableReference());
			}
			return new ArrayCreation(Parameter.ResolveParameterExpressionType(ec, loc), arrayInitializer, loc);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00097290 File Offset: 0x00095490
		public ParametersCompiled Clone()
		{
			ParametersCompiled parametersCompiled = (ParametersCompiled)base.MemberwiseClone();
			parametersCompiled.parameters = new IParameterData[this.parameters.Length];
			for (int i = 0; i < base.Count; i++)
			{
				parametersCompiled.parameters[i] = this[i].Clone();
			}
			return parametersCompiled;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000972E2 File Offset: 0x000954E2
		// Note: this type is marked as 'beforefieldinit'.
		static ParametersCompiled()
		{
		}

		// Token: 0x04000B52 RID: 2898
		public static readonly ParametersCompiled EmptyReadOnlyParameters = new ParametersCompiled();

		// Token: 0x04000B53 RID: 2899
		public static readonly ParametersCompiled Undefined = new ParametersCompiled();
	}
}
