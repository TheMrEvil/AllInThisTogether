using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001C0 RID: 448
	public struct OverloadResolver
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x0006F8A5 File Offset: 0x0006DAA5
		public OverloadResolver(IList<MemberSpec> members, OverloadResolver.Restrictions restrictions, Location loc)
		{
			this = new OverloadResolver(members, null, restrictions, loc);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0006F8B4 File Offset: 0x0006DAB4
		public OverloadResolver(IList<MemberSpec> members, TypeArguments targs, OverloadResolver.Restrictions restrictions, Location loc)
		{
			this = default(OverloadResolver);
			if (members == null || members.Count == 0)
			{
				throw new ArgumentException("empty members set");
			}
			this.members = members;
			this.loc = loc;
			this.type_arguments = targs;
			this.restrictions = restrictions;
			if (this.IsDelegateInvoke)
			{
				this.restrictions |= OverloadResolver.Restrictions.NoBaseMembers;
			}
			this.base_provider = OverloadResolver.NoBaseMembers.Instance;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0006F91C File Offset: 0x0006DB1C
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0006F924 File Offset: 0x0006DB24
		public OverloadResolver.IBaseMembersProvider BaseMembersProvider
		{
			get
			{
				return this.base_provider;
			}
			set
			{
				this.base_provider = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0006F92D File Offset: 0x0006DB2D
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0006F935 File Offset: 0x0006DB35
		public bool BestCandidateIsDynamic
		{
			[CompilerGenerated]
			get
			{
				return this.<BestCandidateIsDynamic>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BestCandidateIsDynamic>k__BackingField = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0006F93E File Offset: 0x0006DB3E
		public MethodGroupExpr BestCandidateNewMethodGroup
		{
			get
			{
				return this.best_candidate_extension_group;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0006F946 File Offset: 0x0006DB46
		public TypeSpec BestCandidateReturnType
		{
			get
			{
				return this.best_candidate_return_type;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0006F94E File Offset: 0x0006DB4E
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x0006F956 File Offset: 0x0006DB56
		public OverloadResolver.IErrorHandler CustomErrors
		{
			get
			{
				return this.custom_errors;
			}
			set
			{
				this.custom_errors = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0006F95F File Offset: 0x0006DB5F
		private TypeSpec DelegateType
		{
			get
			{
				if ((this.restrictions & OverloadResolver.Restrictions.DelegateInvoke) == OverloadResolver.Restrictions.None)
				{
					throw new InternalErrorException("Not running in delegate mode", new object[]
					{
						this.loc
					});
				}
				return this.members[0].DeclaringType;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0006F99B File Offset: 0x0006DB9B
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0006F9A3 File Offset: 0x0006DBA3
		public OverloadResolver.IInstanceQualifier InstanceQualifier
		{
			get
			{
				return this.instance_qualifier;
			}
			set
			{
				this.instance_qualifier = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0006F9AC File Offset: 0x0006DBAC
		private bool IsProbingOnly
		{
			get
			{
				return (this.restrictions & OverloadResolver.Restrictions.ProbingOnly) > OverloadResolver.Restrictions.None;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0006F9B9 File Offset: 0x0006DBB9
		private bool IsDelegateInvoke
		{
			get
			{
				return (this.restrictions & OverloadResolver.Restrictions.DelegateInvoke) > OverloadResolver.Restrictions.None;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0006F9C8 File Offset: 0x0006DBC8
		private static int BetterExpressionConversion(ResolveContext ec, Argument a, TypeSpec p, TypeSpec q)
		{
			TypeSpec typeSpec = a.Type;
			if (typeSpec == InternalType.AnonymousMethod && ec.Module.Compiler.Settings.Version > LanguageVersion.ISO_2)
			{
				if (p.IsExpressionTreeType || q.IsExpressionTreeType)
				{
					if (q.MemberDefinition != p.MemberDefinition)
					{
						return 0;
					}
					q = TypeManager.GetTypeArguments(q)[0];
					p = TypeManager.GetTypeArguments(p)[0];
				}
				MethodSpec invokeMethod = Delegate.GetInvokeMethod(p);
				MethodSpec invokeMethod2 = Delegate.GetInvokeMethod(q);
				if (!TypeSpecComparer.Equals(invokeMethod.Parameters.Types, invokeMethod2.Parameters.Types))
				{
					return 0;
				}
				p = invokeMethod.ReturnType;
				TypeSpec delegate_type = q;
				q = invokeMethod2.ReturnType;
				if (p.Kind == MemberKind.Void)
				{
					if (q.Kind == MemberKind.Void)
					{
						return 0;
					}
					return 2;
				}
				else if (q.Kind == MemberKind.Void)
				{
					if (p.Kind == MemberKind.Void)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					AnonymousMethodExpression anonymousMethodExpression = (AnonymousMethodExpression)a.Expr;
					if ((p.IsGenericTask || q.IsGenericTask) && anonymousMethodExpression.Block.IsAsync && p.IsGenericTask && q.IsGenericTask)
					{
						q = q.TypeArguments[0];
						p = p.TypeArguments[0];
					}
					if (q != p)
					{
						typeSpec = anonymousMethodExpression.InferReturnType(ec, null, delegate_type);
						if (typeSpec == null)
						{
							return 1;
						}
						if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							typeSpec = ec.BuiltinTypes.Object;
						}
					}
				}
			}
			if (typeSpec == p)
			{
				return 1;
			}
			if (typeSpec == q)
			{
				return 2;
			}
			return OverloadResolver.BetterTypeConversion(ec, p, q);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0006FB40 File Offset: 0x0006DD40
		public static int BetterTypeConversion(ResolveContext ec, TypeSpec p, TypeSpec q)
		{
			if (p == null || q == null)
			{
				throw new InternalErrorException("BetterTypeConversion got a null conversion");
			}
			BuiltinTypeSpec.Type builtinType = p.BuiltinType;
			switch (builtinType)
			{
			case BuiltinTypeSpec.Type.SByte:
				builtinType = q.BuiltinType;
				if (builtinType != BuiltinTypeSpec.Type.Byte)
				{
					switch (builtinType)
					{
					case BuiltinTypeSpec.Type.UShort:
					case BuiltinTypeSpec.Type.UInt:
					case BuiltinTypeSpec.Type.ULong:
						break;
					case BuiltinTypeSpec.Type.Int:
					case BuiltinTypeSpec.Type.Long:
						goto IL_C0;
					default:
						goto IL_C0;
					}
				}
				return 1;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.UInt:
				break;
			case BuiltinTypeSpec.Type.Short:
				switch (q.BuiltinType)
				{
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.UInt:
				case BuiltinTypeSpec.Type.ULong:
					return 1;
				}
				break;
			case BuiltinTypeSpec.Type.Int:
				if (q.BuiltinType == BuiltinTypeSpec.Type.UInt || q.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					return 1;
				}
				break;
			case BuiltinTypeSpec.Type.Long:
				if (q.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					return 1;
				}
				break;
			default:
				if (builtinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return 2;
				}
				break;
			}
			IL_C0:
			builtinType = q.BuiltinType;
			switch (builtinType)
			{
			case BuiltinTypeSpec.Type.SByte:
				builtinType = p.BuiltinType;
				if (builtinType != BuiltinTypeSpec.Type.Byte)
				{
					switch (builtinType)
					{
					case BuiltinTypeSpec.Type.UShort:
					case BuiltinTypeSpec.Type.UInt:
					case BuiltinTypeSpec.Type.ULong:
						break;
					case BuiltinTypeSpec.Type.Int:
					case BuiltinTypeSpec.Type.Long:
						goto IL_16F;
					default:
						goto IL_16F;
					}
				}
				return 2;
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.UInt:
				break;
			case BuiltinTypeSpec.Type.Short:
				switch (p.BuiltinType)
				{
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.UInt:
				case BuiltinTypeSpec.Type.ULong:
					return 2;
				}
				break;
			case BuiltinTypeSpec.Type.Int:
				if (p.BuiltinType == BuiltinTypeSpec.Type.UInt || p.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					return 2;
				}
				break;
			case BuiltinTypeSpec.Type.Long:
				if (p.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					return 2;
				}
				break;
			default:
				if (builtinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return 1;
				}
				break;
			}
			IL_16F:
			Expression expr = new EmptyExpression(p);
			Expression expr2 = new EmptyExpression(q);
			bool flag = Convert.ImplicitConversionExists(ec, expr, q);
			bool flag2 = Convert.ImplicitConversionExists(ec, expr2, p);
			if (flag && !flag2)
			{
				return 1;
			}
			if (flag2 && !flag)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0006FCF0 File Offset: 0x0006DEF0
		private static bool BetterFunction(ResolveContext ec, Arguments args, MemberSpec candidate, AParametersCollection cparam, bool candidate_params, MemberSpec best, AParametersCollection bparam, bool best_params)
		{
			AParametersCollection parameters = ((IParametersMember)candidate).Parameters;
			AParametersCollection parameters2 = ((IParametersMember)best).Parameters;
			bool flag = false;
			bool flag2 = true;
			int num = (args == null) ? 0 : args.Count;
			int i = 0;
			int num2 = 0;
			int num3 = 0;
			while (i < num)
			{
				Argument argument = args[i];
				if (argument.IsDefaultArgument)
				{
					break;
				}
				NamedArgument namedArgument = argument as NamedArgument;
				TypeSpec typeSpec;
				TypeSpec typeSpec2;
				if (namedArgument != null)
				{
					int parameterIndexByName = cparam.GetParameterIndexByName(namedArgument.Name);
					typeSpec = parameters.Types[parameterIndexByName];
					if (candidate_params && parameters.FixedParameters[parameterIndexByName].ModFlags == Parameter.Modifier.PARAMS)
					{
						typeSpec = TypeManager.GetElementType(typeSpec);
					}
					parameterIndexByName = bparam.GetParameterIndexByName(namedArgument.Name);
					typeSpec2 = parameters2.Types[parameterIndexByName];
					if (best_params && parameters2.FixedParameters[parameterIndexByName].ModFlags == Parameter.Modifier.PARAMS)
					{
						typeSpec2 = TypeManager.GetElementType(typeSpec2);
					}
				}
				else
				{
					typeSpec = parameters.Types[num2];
					typeSpec2 = parameters2.Types[num3];
					if (candidate_params && parameters.FixedParameters[num2].ModFlags == Parameter.Modifier.PARAMS)
					{
						typeSpec = TypeManager.GetElementType(typeSpec);
						num2--;
					}
					if (best_params && parameters2.FixedParameters[num3].ModFlags == Parameter.Modifier.PARAMS)
					{
						typeSpec2 = TypeManager.GetElementType(typeSpec2);
						num3--;
					}
				}
				if (!TypeSpecComparer.IsEqual(typeSpec, typeSpec2))
				{
					flag2 = false;
					int num4 = OverloadResolver.BetterExpressionConversion(ec, argument, typeSpec, typeSpec2);
					if (num4 == 2)
					{
						return false;
					}
					if (num4 != 0)
					{
						flag = true;
					}
				}
				i++;
				num2++;
				num3++;
			}
			if (flag)
			{
				return true;
			}
			if (!flag2)
			{
				return parameters.Count < parameters2.Count && !candidate_params && parameters2.FixedParameters[i].HasDefaultValue;
			}
			if (candidate_params != best_params)
			{
				return !candidate_params;
			}
			while (i < parameters.Count && i < parameters2.Count)
			{
				IParameterData parameterData = parameters.FixedParameters[i];
				IParameterData parameterData2 = parameters2.FixedParameters[i];
				if (parameterData.HasDefaultValue != parameterData2.HasDefaultValue)
				{
					return parameterData.HasDefaultValue;
				}
				if (parameters.Count != parameters2.Count)
				{
					return false;
				}
				if (!parameterData.HasDefaultValue)
				{
					break;
				}
				i++;
			}
			if (parameters.Count != parameters2.Count)
			{
				return parameters.Count < parameters2.Count;
			}
			if (best.IsGeneric != candidate.IsGeneric)
			{
				return best.IsGeneric;
			}
			AParametersCollection parameters3 = ((IParametersMember)candidate.MemberDefinition).Parameters;
			AParametersCollection parameters4 = ((IParametersMember)best.MemberDefinition).Parameters;
			bool result = false;
			for (i = 0; i < num; i++)
			{
				NamedArgument namedArgument2 = (num == 0) ? null : (args[i] as NamedArgument);
				TypeSpec typeSpec;
				TypeSpec typeSpec2;
				if (namedArgument2 != null)
				{
					typeSpec = parameters3.Types[cparam.GetParameterIndexByName(namedArgument2.Name)];
					typeSpec2 = parameters4.Types[bparam.GetParameterIndexByName(namedArgument2.Name)];
				}
				else
				{
					typeSpec = parameters3.Types[i];
					typeSpec2 = parameters4.Types[i];
				}
				if (typeSpec != typeSpec2)
				{
					TypeSpec typeSpec3 = OverloadResolver.MoreSpecific(typeSpec, typeSpec2);
					if (typeSpec3 == typeSpec2)
					{
						return false;
					}
					if (typeSpec3 == typeSpec)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0007000C File Offset: 0x0006E20C
		private static bool CheckInflatedArguments(MethodSpec ms)
		{
			if (!TypeParameterSpec.HasAnyTypeParameterTypeConstrained(ms.GenericDefinition))
			{
				return true;
			}
			ConstraintChecker constraintChecker = new ConstraintChecker(null);
			TypeSpec[] types = ms.Parameters.Types;
			for (int i = 0; i < types.Length; i++)
			{
				InflatedTypeSpec inflatedTypeSpec = types[i] as InflatedTypeSpec;
				if (inflatedTypeSpec != null)
				{
					TypeSpec[] typeArguments = inflatedTypeSpec.TypeArguments;
					if (typeArguments.Length != 0 && !constraintChecker.CheckAll(inflatedTypeSpec.GetDefinition(), typeArguments, inflatedTypeSpec.Constraints, Location.Null))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00070082 File Offset: 0x0006E282
		public static void Error_ConstructorMismatch(ResolveContext rc, TypeSpec type, int argCount, Location loc)
		{
			rc.Report.Error(1729, loc, "The type `{0}' does not contain a constructor that takes `{1}' arguments", type.GetSignatureForError(), argCount.ToString());
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000700A8 File Offset: 0x0006E2A8
		private int IsApplicable(ResolveContext ec, ref Arguments arguments, int arg_count, ref MemberSpec candidate, IParametersMember pm, ref bool params_expanded_form, ref bool dynamicArgument, ref TypeSpec returnType, bool errorMode)
		{
			AParametersCollection parameters = pm.Parameters;
			AParametersCollection parameters2 = ((IParametersMember)candidate).Parameters;
			int num = parameters.Count;
			int num2 = 0;
			Arguments arguments2 = arguments;
			if (arg_count != num)
			{
				if ((this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) == OverloadResolver.Restrictions.None)
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						if (parameters.FixedParameters[i].HasDefaultValue)
						{
							num2 = parameters.Count - i;
							break;
						}
					}
				}
				if (num2 != 0)
				{
					if (parameters2.HasParams)
					{
						num2--;
						if (arg_count < num)
						{
							num--;
						}
					}
					else
					{
						if (arg_count > num)
						{
							int num3 = Math.Abs(arg_count - num);
							return 1000000000 + num3;
						}
						if (arg_count < num - num2)
						{
							int num4 = Math.Abs(num - num2 - arg_count);
							return 1000000000 + num4;
						}
					}
				}
				else if (arg_count != num)
				{
					int num5 = Math.Abs(arg_count - num);
					if (!parameters2.HasParams)
					{
						return 1000000000 + num5;
					}
					if (arg_count < num - 1)
					{
						return 1000000000 + num5;
					}
				}
				if (num2 != 0)
				{
					if (arguments == null)
					{
						arguments = new Arguments(num2);
					}
					else
					{
						Arguments arguments3 = new Arguments(num);
						arguments3.AddRange(arguments);
						arguments = arguments3;
					}
					for (int j = arg_count; j < num; j++)
					{
						arguments.Add(null);
					}
				}
			}
			if (arg_count > 0)
			{
				if (arguments[arg_count - 1] is NamedArgument)
				{
					arg_count = arguments.Count;
					int k = 0;
					while (k < arg_count)
					{
						bool flag = false;
						Argument argument;
						do
						{
							NamedArgument namedArgument = arguments[k] as NamedArgument;
							if (namedArgument == null)
							{
								break;
							}
							int parameterIndexByName = parameters.GetParameterIndexByName(namedArgument.Name);
							if (parameterIndexByName < 0)
							{
								goto Block_17;
							}
							if (parameterIndexByName == k)
							{
								break;
							}
							if (parameterIndexByName >= num)
							{
								if ((parameters2.FixedParameters[parameterIndexByName].ModFlags & Parameter.Modifier.PARAMS) == Parameter.Modifier.NONE)
								{
									break;
								}
								arguments.Add(null);
								arg_count++;
								argument = null;
							}
							else
							{
								if (parameterIndexByName == arg_count)
								{
									goto Block_21;
								}
								argument = arguments[parameterIndexByName];
								if (argument != null && !(argument is NamedArgument))
								{
									break;
								}
							}
							if (!flag)
							{
								arguments = arguments.MarkOrderedArgument(namedArgument);
								flag = true;
							}
							if (arguments == arguments2)
							{
								arguments = new Arguments(arguments2.Count);
								arguments.AddRange(arguments2);
							}
							arguments[parameterIndexByName] = arguments[k];
							arguments[k] = argument;
						}
						while (argument != null);
						k++;
						continue;
						Block_17:
						return 100000000 - k;
						Block_21:
						return 100000000 - k - 1;
					}
				}
				else
				{
					arg_count = arguments.Count;
				}
			}
			else if (arguments != null)
			{
				arg_count = arguments.Count;
			}
			if (arg_count != num && !parameters2.HasParams)
			{
				return 10000000 - Math.Abs(num - arg_count);
			}
			List<MissingTypeSpecReference> missingDependencies = candidate.GetMissingDependencies();
			if (missingDependencies != null)
			{
				ImportedTypeDefinition.Error_MissingDependency(ec, missingDependencies, this.loc);
				return -1;
			}
			MethodSpec methodSpec = candidate as MethodSpec;
			TypeSpec[] types;
			if (methodSpec != null && methodSpec.IsGeneric)
			{
				if (this.type_arguments != null)
				{
					int arity = methodSpec.Arity;
					if (arity != this.type_arguments.Count)
					{
						return 100000 - Math.Abs(this.type_arguments.Count - arity);
					}
					if (this.type_arguments.Arguments != null)
					{
						methodSpec = methodSpec.MakeGenericMethod(ec, this.type_arguments.Arguments);
					}
				}
				else
				{
					if (this.lambda_conv_msgs == null)
					{
						for (int l = 0; l < arg_count; l++)
						{
							Argument argument2 = arguments[l];
							if (argument2 != null)
							{
								AnonymousMethodExpression anonymousMethodExpression = argument2.Expr as AnonymousMethodExpression;
								if (anonymousMethodExpression != null)
								{
									if (this.lambda_conv_msgs == null)
									{
										this.lambda_conv_msgs = new SessionReportPrinter();
									}
									anonymousMethodExpression.TypeInferenceReportPrinter = this.lambda_conv_msgs;
								}
							}
						}
					}
					TypeInference typeInference = new TypeInference(arguments);
					TypeSpec[] array = typeInference.InferMethodArguments(ec, methodSpec);
					if (array == null)
					{
						return 100000 - typeInference.InferenceScore;
					}
					if (this.lambda_conv_msgs != null)
					{
						this.lambda_conv_msgs.ClearSession();
					}
					if (array.Length != 0)
					{
						if (!errorMode)
						{
							for (int m = 0; m < array.Length; m++)
							{
								if (!array[m].IsAccessible(ec))
								{
									return 100000 - m;
								}
							}
						}
						methodSpec = methodSpec.MakeGenericMethod(ec, array);
					}
				}
				if (!OverloadResolver.CheckInflatedArguments(methodSpec))
				{
					candidate = methodSpec;
					return 10000;
				}
				if (candidate != pm)
				{
					MethodSpec methodSpec2 = (MethodSpec)pm;
					TypeParameterInflator typeParameterInflator = new TypeParameterInflator(ec, methodSpec.DeclaringType, methodSpec2.GenericDefinition.TypeParameters, methodSpec.TypeArguments);
					returnType = typeParameterInflator.Inflate(returnType);
				}
				else
				{
					returnType = methodSpec.ReturnType;
				}
				candidate = methodSpec;
				parameters = methodSpec.Parameters;
				types = parameters.Types;
			}
			else
			{
				if (this.type_arguments != null)
				{
					return 1000000;
				}
				types = parameters2.Types;
			}
			Parameter.Modifier modifier = Parameter.Modifier.NONE;
			TypeSpec typeSpec = null;
			for (int n = 0; n < arg_count; n++)
			{
				Argument argument3 = arguments[n];
				if (argument3 == null)
				{
					IParameterData parameterData = parameters.FixedParameters[n];
					if (!parameterData.HasDefaultValue)
					{
						arguments = arguments2;
						return arg_count * 2 + 2;
					}
					Expression expression = parameterData.DefaultValue;
					if (expression != null)
					{
						expression = OverloadResolver.ResolveDefaultValueArgument(ec, types[n], expression, this.loc);
						if (expression == null)
						{
							for (int num6 = n; num6 < arg_count; num6++)
							{
								arguments.RemoveAt(n);
							}
							return (arg_count - n) * 2 + 1;
						}
					}
					if ((parameterData.ModFlags & Parameter.Modifier.CallerMask) != Parameter.Modifier.NONE)
					{
						if ((parameterData.ModFlags & Parameter.Modifier.CallerLineNumber) != Parameter.Modifier.NONE)
						{
							expression = new IntLiteral(ec.BuiltinTypes, this.loc.Row, this.loc);
						}
						else if ((parameterData.ModFlags & Parameter.Modifier.CallerFilePath) != Parameter.Modifier.NONE)
						{
							expression = new StringLiteral(ec.BuiltinTypes, this.loc.NameFullPath, this.loc);
						}
						else if (ec.MemberContext.CurrentMemberDefinition != null)
						{
							expression = new StringLiteral(ec.BuiltinTypes, ec.MemberContext.CurrentMemberDefinition.GetCallerMemberName(), this.loc);
						}
					}
					arguments[n] = new Argument(expression, Argument.AType.Default);
				}
				else
				{
					if (modifier != Parameter.Modifier.PARAMS)
					{
						modifier = ((parameters.FixedParameters[n].ModFlags & ~Parameter.Modifier.PARAMS) | (parameters2.FixedParameters[n].ModFlags & Parameter.Modifier.PARAMS));
						typeSpec = types[n];
					}
					else if (!params_expanded_form)
					{
						params_expanded_form = true;
						typeSpec = ((ElementTypeSpec)typeSpec).Element;
						n -= 2;
						goto IL_6AE;
					}
					int num7 = 1;
					if (!params_expanded_form)
					{
						if (argument3.IsExtensionType)
						{
							if (ExtensionMethodGroupExpr.IsExtensionTypeCompatible(argument3.Type, typeSpec))
							{
								goto IL_6AE;
							}
						}
						else
						{
							num7 = this.IsArgumentCompatible(ec, argument3, modifier, typeSpec);
							if (num7 < 0)
							{
								dynamicArgument = true;
							}
						}
					}
					if (num7 != 0 && (modifier & Parameter.Modifier.PARAMS) != Parameter.Modifier.NONE && (this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) == OverloadResolver.Restrictions.None)
					{
						if (!params_expanded_form)
						{
							typeSpec = ((ElementTypeSpec)typeSpec).Element;
						}
						if (num7 > 0)
						{
							num7 = this.IsArgumentCompatible(ec, argument3, Parameter.Modifier.NONE, typeSpec);
						}
						if (num7 < 0)
						{
							params_expanded_form = true;
							dynamicArgument = true;
						}
						else if (num7 == 0 || arg_count > parameters.Count)
						{
							params_expanded_form = true;
						}
					}
					if (num7 > 0)
					{
						if (params_expanded_form)
						{
							num7++;
						}
						return (arg_count - n) * 2 + num7;
					}
				}
				IL_6AE:;
			}
			if (dynamicArgument)
			{
				arguments = arguments2;
			}
			return 0;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0007077C File Offset: 0x0006E97C
		public static Expression ResolveDefaultValueArgument(ResolveContext ec, TypeSpec ptype, Expression e, Location loc)
		{
			if (e is Constant && e.Type == ptype)
			{
				return e;
			}
			if (e == EmptyExpression.MissingValue && (ptype.BuiltinType == BuiltinTypeSpec.Type.Object || ptype.BuiltinType == BuiltinTypeSpec.Type.Dynamic))
			{
				e = new MemberAccess(new MemberAccess(new MemberAccess(new QualifiedAliasMember(QualifiedAliasMember.GlobalAlias, "System", loc), "Reflection", loc), "Missing", loc), "Value", loc);
			}
			else if (e is Constant)
			{
				e = Convert.ImplicitConversionStandard(ec, e, ptype, loc);
				if (e == null)
				{
					return null;
				}
			}
			else
			{
				e = new DefaultValueExpression(new TypeExpression(ptype, loc), loc);
			}
			return e.Resolve(ec);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0007081C File Offset: 0x0006EA1C
		private int IsArgumentCompatible(ResolveContext ec, Argument argument, Parameter.Modifier param_mod, TypeSpec parameter)
		{
			if (((argument.Modifier | param_mod) & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
			{
				TypeSpec type = argument.Type;
				if ((argument.Modifier & Parameter.Modifier.RefOutMask) != (param_mod & Parameter.Modifier.RefOutMask))
				{
					if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic && (argument.Modifier & Parameter.Modifier.RefOutMask) == Parameter.Modifier.NONE && (this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) == OverloadResolver.Restrictions.None)
					{
						return -1;
					}
					return 1;
				}
				else if (type != parameter)
				{
					if (type == InternalType.VarOutType)
					{
						return 0;
					}
					if (!TypeSpecComparer.IsEqual(type, parameter))
					{
						if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic && (argument.Modifier & Parameter.Modifier.RefOutMask) == Parameter.Modifier.NONE && (this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) == OverloadResolver.Restrictions.None)
						{
							return -1;
						}
						return 2;
					}
				}
			}
			else
			{
				if (argument.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic && (this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) == OverloadResolver.Restrictions.None)
				{
					return -1;
				}
				if (!Convert.ImplicitConversionExists(ec, argument.Expr, parameter))
				{
					if (!parameter.IsDelegate || !(argument.Expr is AnonymousMethodExpression))
					{
						return 3;
					}
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000708EC File Offset: 0x0006EAEC
		private static TypeSpec MoreSpecific(TypeSpec p, TypeSpec q)
		{
			if (TypeManager.IsGenericParameter(p) && !TypeManager.IsGenericParameter(q))
			{
				return q;
			}
			if (!TypeManager.IsGenericParameter(p) && TypeManager.IsGenericParameter(q))
			{
				return p;
			}
			ArrayContainer arrayContainer = p as ArrayContainer;
			if (arrayContainer != null)
			{
				ArrayContainer arrayContainer2 = q as ArrayContainer;
				if (arrayContainer2 == null)
				{
					return null;
				}
				TypeSpec typeSpec = OverloadResolver.MoreSpecific(arrayContainer.Element, arrayContainer2.Element);
				if (typeSpec == arrayContainer.Element)
				{
					return p;
				}
				if (typeSpec == arrayContainer2.Element)
				{
					return q;
				}
			}
			else if (p.IsGeneric && q.IsGeneric)
			{
				TypeSpec[] typeArguments = TypeManager.GetTypeArguments(p);
				TypeSpec[] typeArguments2 = TypeManager.GetTypeArguments(q);
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < typeArguments.Length; i++)
				{
					TypeSpec typeSpec2 = OverloadResolver.MoreSpecific(typeArguments[i], typeArguments2[i]);
					if (typeSpec2 == typeArguments[i])
					{
						flag = true;
					}
					if (typeSpec2 == typeArguments2[i])
					{
						flag2 = true;
					}
				}
				if (flag && !flag2)
				{
					return p;
				}
				if (!flag && flag2)
				{
					return q;
				}
			}
			return null;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000709CC File Offset: 0x0006EBCC
		public T ResolveMember<T>(ResolveContext rc, ref Arguments args) where T : MemberSpec, IParametersMember
		{
			List<OverloadResolver.AmbiguousCandidate> list = null;
			Arguments arguments = null;
			bool flag = false;
			bool flag2 = false;
			IParametersMember parametersMember = null;
			int arg_count = (args != null) ? args.Count : 0;
			Arguments arguments2 = args;
			bool flag3 = false;
			MemberSpec memberSpec = null;
			int num = 0;
			MemberSpec memberSpec2;
			int num2;
			MethodGroupExpr methodGroupExpr;
			for (;;)
			{
				memberSpec2 = null;
				num2 = int.MaxValue;
				IList<MemberSpec> baseMembers = this.members;
				do
				{
					for (int i = 0; i < baseMembers.Count; i++)
					{
						MemberSpec memberSpec3 = baseMembers[i];
						if ((memberSpec3.Modifiers & Modifiers.OVERRIDE) == (Modifiers)0 && (flag3 || (memberSpec3.IsAccessible(rc) && (!rc.IsRuntimeBinder || memberSpec3.DeclaringType.IsAccessible(rc)) && ((memberSpec3.Modifiers & (Modifiers.PROTECTED | Modifiers.STATIC)) != Modifiers.PROTECTED || this.instance_qualifier == null || this.instance_qualifier.CheckProtectedMemberAccess(rc, memberSpec3)))))
						{
							IParametersMember parametersMember2 = memberSpec3 as IParametersMember;
							if (parametersMember2 == null)
							{
								if (Invocation.IsMemberInvocable(memberSpec3))
								{
									memberSpec = memberSpec3;
								}
							}
							else
							{
								if ((memberSpec3.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL)) != (Modifiers)0)
								{
									IParametersMember overrideMemberParameters = this.base_provider.GetOverrideMemberParameters(memberSpec3);
									if (overrideMemberParameters != null)
									{
										parametersMember2 = overrideMemberParameters;
									}
								}
								bool flag4 = false;
								bool flag5 = false;
								TypeSpec memberType = parametersMember2.MemberType;
								int num3 = this.IsApplicable(rc, ref arguments2, arg_count, ref memberSpec3, parametersMember2, ref flag4, ref flag5, ref memberType, flag3);
								if (this.lambda_conv_msgs != null)
								{
									this.lambda_conv_msgs.EndSession();
								}
								if (num3 < num2)
								{
									if (num3 < 0)
									{
										goto Block_14;
									}
									num = 1;
									if ((this.restrictions & OverloadResolver.Restrictions.GetEnumeratorLookup) == OverloadResolver.Restrictions.None || arguments2.Count == 0)
									{
										num2 = num3;
										memberSpec2 = memberSpec3;
										arguments = arguments2;
										flag = flag4;
										flag2 = flag5;
										parametersMember = parametersMember2;
										this.best_candidate_return_type = memberType;
									}
								}
								else if (num3 == 0)
								{
									if ((this.restrictions & OverloadResolver.Restrictions.BaseMembersIncluded) != OverloadResolver.Restrictions.None && TypeSpec.IsBaseClass(memberSpec2.DeclaringType, memberSpec3.DeclaringType, true))
									{
										goto IL_28F;
									}
									num++;
									bool flag6;
									if (memberSpec2.DeclaringType.IsInterface && memberSpec3.DeclaringType.ImplementsInterface(memberSpec2.DeclaringType, false))
									{
										flag6 = true;
										if (list != null)
										{
											foreach (OverloadResolver.AmbiguousCandidate ambiguousCandidate in list)
											{
												if (!memberSpec3.DeclaringType.ImplementsInterface(memberSpec2.DeclaringType, false))
												{
													flag6 = false;
													break;
												}
											}
											if (flag6)
											{
												list = null;
											}
										}
									}
									else
									{
										flag6 = OverloadResolver.BetterFunction(rc, arguments2, memberSpec3, parametersMember2.Parameters, flag4, memberSpec2, parametersMember.Parameters, flag);
									}
									if (flag6)
									{
										memberSpec2 = memberSpec3;
										arguments = arguments2;
										flag = flag4;
										flag2 = flag5;
										parametersMember = parametersMember2;
										this.best_candidate_return_type = memberType;
									}
									else
									{
										if (list == null)
										{
											list = new List<OverloadResolver.AmbiguousCandidate>();
										}
										list.Add(new OverloadResolver.AmbiguousCandidate(memberSpec3, parametersMember2.Parameters, flag4));
									}
								}
								arguments2 = args;
							}
						}
						IL_28F:;
					}
				}
				while (num2 != 0 && (baseMembers = this.base_provider.GetBaseMembers(baseMembers[0].DeclaringType.BaseType)) != null);
				if (num2 == 0)
				{
					goto IL_34C;
				}
				if (!flag3)
				{
					methodGroupExpr = this.base_provider.LookupExtensionMethod(rc);
					if (methodGroupExpr != null)
					{
						methodGroupExpr = methodGroupExpr.OverloadResolve(rc, ref args, null, this.restrictions);
						if (methodGroupExpr != null)
						{
							goto Block_30;
						}
					}
				}
				if (this.IsProbingOnly)
				{
					goto Block_31;
				}
				if (flag3 || (this.lambda_conv_msgs != null && !this.lambda_conv_msgs.IsEmpty))
				{
					goto IL_34C;
				}
				this.lambda_conv_msgs = null;
				flag3 = true;
			}
			Block_14:
			return default(T);
			Block_30:
			this.best_candidate_extension_group = methodGroupExpr;
			return (T)((object)methodGroupExpr.BestCandidate);
			Block_31:
			return default(T);
			IL_34C:
			if (num2 != 0 || flag3)
			{
				this.ReportOverloadError(rc, memberSpec2, parametersMember, arguments, flag);
				return default(T);
			}
			if (flag2)
			{
				if (args[0].IsExtensionType)
				{
					rc.Report.Error(1973, this.loc, "Type `{0}' does not contain a member `{1}' and the best extension method overload `{2}' cannot be dynamically dispatched. Consider calling the method without the extension method syntax", new string[]
					{
						args[0].Type.GetSignatureForError(),
						memberSpec2.Name,
						memberSpec2.GetSignatureForError()
					});
				}
				if (num == 1 && memberSpec2.IsGeneric && this.type_arguments != null)
				{
					MethodSpec methodSpec = memberSpec2 as MethodSpec;
					if (methodSpec != null && TypeParameterSpec.HasAnyTypeParameterConstrained(methodSpec.GenericDefinition))
					{
						ConstraintChecker constraintChecker = new ConstraintChecker(rc);
						constraintChecker.CheckAll(methodSpec.GetGenericMethodDefinition(), methodSpec.TypeArguments, methodSpec.Constraints, this.loc);
					}
				}
				this.BestCandidateIsDynamic = true;
				return default(T);
			}
			if ((this.restrictions & (OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.CovariantDelegate)) == (OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.CovariantDelegate))
			{
				return (T)((object)memberSpec2);
			}
			if (list != null)
			{
				for (int j = 0; j < list.Count; j++)
				{
					OverloadResolver.AmbiguousCandidate ambiguousCandidate2 = list[j];
					if (!OverloadResolver.BetterFunction(rc, arguments, memberSpec2, parametersMember.Parameters, flag, ambiguousCandidate2.Member, ambiguousCandidate2.Parameters, ambiguousCandidate2.Expanded))
					{
						MemberSpec member = ambiguousCandidate2.Member;
						if (this.custom_errors == null || !this.custom_errors.AmbiguousCandidates(rc, memberSpec2, member))
						{
							rc.Report.SymbolRelatedToPreviousError(memberSpec2);
							rc.Report.SymbolRelatedToPreviousError(member);
							rc.Report.Error(121, this.loc, "The call is ambiguous between the following methods or properties: `{0}' and `{1}'", memberSpec2.GetSignatureForError(), member.GetSignatureForError());
						}
						return (T)((object)memberSpec2);
					}
				}
			}
			if (memberSpec != null && !this.IsProbingOnly)
			{
				rc.Report.SymbolRelatedToPreviousError(memberSpec2);
				rc.Report.SymbolRelatedToPreviousError(memberSpec);
				rc.Report.Warning(467, 2, this.loc, "Ambiguity between method `{0}' and invocable non-method `{1}'. Using method group", memberSpec2.GetSignatureForError(), memberSpec.GetSignatureForError());
			}
			if (!this.VerifyArguments(rc, ref arguments, memberSpec2, parametersMember, flag))
			{
				return default(T);
			}
			if (memberSpec2 == null)
			{
				return default(T);
			}
			if (!this.IsProbingOnly && !rc.IsInProbingMode)
			{
				ObsoleteAttribute attributeObsolete = memberSpec2.GetAttributeObsolete();
				if (attributeObsolete != null && !rc.IsObsolete)
				{
					AttributeTester.Report_ObsoleteMessage(attributeObsolete, memberSpec2.GetSignatureForError(), this.loc, rc.Report);
				}
				memberSpec2.MemberDefinition.SetIsUsed();
			}
			args = arguments;
			return (T)((object)memberSpec2);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00070FB4 File Offset: 0x0006F1B4
		public MethodSpec ResolveOperator(ResolveContext rc, ref Arguments args)
		{
			return this.ResolveMember<MethodSpec>(rc, ref args);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00070FC0 File Offset: 0x0006F1C0
		private void ReportArgumentMismatch(ResolveContext ec, int idx, MemberSpec method, Argument a, AParametersCollection expected_par, TypeSpec paramType)
		{
			if (this.custom_errors != null && this.custom_errors.ArgumentMismatch(ec, method, a, idx))
			{
				return;
			}
			if (a.Type == InternalType.ErrorType)
			{
				return;
			}
			if (a is CollectionElementInitializer.ElementInitializerArgument)
			{
				ec.Report.SymbolRelatedToPreviousError(method);
				if ((expected_par.FixedParameters[idx].ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					ec.Report.Error(1954, this.loc, "The best overloaded collection initalizer method `{0}' cannot have `ref' or `out' modifier", TypeManager.CSharpSignature(method));
					return;
				}
				ec.Report.Error(1950, this.loc, "The best overloaded collection initalizer method `{0}' has some invalid arguments", TypeManager.CSharpSignature(method));
			}
			else if (this.IsDelegateInvoke)
			{
				ec.Report.Error(1594, this.loc, "Delegate `{0}' has some invalid arguments", this.DelegateType.GetSignatureForError());
			}
			else
			{
				ec.Report.SymbolRelatedToPreviousError(method);
				ec.Report.Error(1502, this.loc, "The best overloaded method match for `{0}' has some invalid arguments", method.GetSignatureForError());
			}
			Parameter.Modifier modifier = (idx >= expected_par.Count) ? Parameter.Modifier.NONE : expected_par.FixedParameters[idx].ModFlags;
			string text = (idx + 1).ToString();
			if (((modifier & Parameter.Modifier.RefOutMask) ^ (a.Modifier & Parameter.Modifier.RefOutMask)) == Parameter.Modifier.NONE)
			{
				string text2 = a.GetSignatureForError();
				string text3 = paramType.GetSignatureForError();
				if (text2 == text3)
				{
					text2 = a.Type.GetSignatureForErrorIncludingAssemblyName();
					text3 = paramType.GetSignatureForErrorIncludingAssemblyName();
				}
				if ((modifier & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					text2 = Parameter.GetModifierSignature(a.Modifier) + " " + text2;
					text3 = Parameter.GetModifierSignature(a.Modifier) + " " + text3;
				}
				ec.Report.Error(1503, a.Expr.Location, "Argument `#{0}' cannot convert `{1}' expression to type `{2}'", new string[]
				{
					text,
					text2,
					text3
				});
				return;
			}
			if ((modifier & Parameter.Modifier.RefOutMask) == Parameter.Modifier.NONE)
			{
				ec.Report.Error(1615, a.Expr.Location, "Argument `#{0}' does not require `{1}' modifier. Consider removing `{1}' modifier", text, Parameter.GetModifierSignature(a.Modifier));
				return;
			}
			ec.Report.Error(1620, a.Expr.Location, "Argument `#{0}' is missing `{1}' modifier", text, Parameter.GetModifierSignature(modifier));
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000711F0 File Offset: 0x0006F3F0
		private void ReportOverloadError(ResolveContext rc, MemberSpec best_candidate, IParametersMember pm, Arguments args, bool params_expanded)
		{
			int num = (this.type_arguments == null) ? 0 : this.type_arguments.Count;
			int num2 = (args == null) ? 0 : args.Count;
			if (num != best_candidate.Arity && (num > 0 || ((IParametersMember)best_candidate).Parameters.IsEmpty))
			{
				new MethodGroupExpr(new MemberSpec[]
				{
					best_candidate
				}, best_candidate.DeclaringType, this.loc).Error_TypeArgumentsCannotBeUsed(rc, best_candidate, this.loc);
				return;
			}
			if (this.lambda_conv_msgs != null && this.lambda_conv_msgs.Merge(rc.Report.Printer))
			{
				return;
			}
			if ((best_candidate.Modifiers & (Modifiers.PROTECTED | Modifiers.STATIC)) == Modifiers.PROTECTED && this.InstanceQualifier != null && !this.InstanceQualifier.CheckProtectedMemberAccess(rc, best_candidate))
			{
				MemberExpr.Error_ProtectedMemberAccess(rc, best_candidate, this.InstanceQualifier.InstanceType, this.loc);
			}
			if (pm != null && (pm.Parameters.Count == num2 || params_expanded || OverloadResolver.HasUnfilledParams(best_candidate, pm, args)))
			{
				if (!best_candidate.IsAccessible(rc) || !best_candidate.DeclaringType.IsAccessible(rc))
				{
					rc.Report.SymbolRelatedToPreviousError(best_candidate);
					Expression.ErrorIsInaccesible(rc, best_candidate.GetSignatureForError(), this.loc);
					return;
				}
				MethodSpec methodSpec = best_candidate as MethodSpec;
				if (methodSpec != null && methodSpec.IsGeneric)
				{
					bool flag = true;
					if (methodSpec.TypeArguments != null)
					{
						flag = new ConstraintChecker(rc.MemberContext).CheckAll(methodSpec.GetGenericMethodDefinition(), methodSpec.TypeArguments, methodSpec.Constraints, this.loc);
					}
					if (num == 0 && methodSpec.TypeArguments == null)
					{
						if (this.custom_errors != null && this.custom_errors.TypeInferenceFailed(rc, best_candidate))
						{
							return;
						}
						if (flag)
						{
							rc.Report.Error(411, this.loc, "The type arguments for method `{0}' cannot be inferred from the usage. Try specifying the type arguments explicitly", methodSpec.GetGenericMethodDefinition().GetSignatureForError());
						}
						return;
					}
				}
				this.VerifyArguments(rc, ref args, best_candidate, pm, params_expanded);
				return;
			}
			else
			{
				if (this.custom_errors != null && this.custom_errors.NoArgumentMatch(rc, best_candidate))
				{
					return;
				}
				if (best_candidate.Kind == MemberKind.Constructor)
				{
					rc.Report.SymbolRelatedToPreviousError(best_candidate);
					OverloadResolver.Error_ConstructorMismatch(rc, best_candidate.DeclaringType, num2, this.loc);
					return;
				}
				if (this.IsDelegateInvoke)
				{
					rc.Report.SymbolRelatedToPreviousError(this.DelegateType);
					rc.Report.Error(1593, this.loc, "Delegate `{0}' does not take `{1}' arguments", this.DelegateType.GetSignatureForError(), num2.ToString());
					return;
				}
				string arg = (best_candidate.Kind == MemberKind.Indexer) ? "this" : best_candidate.Name;
				rc.Report.SymbolRelatedToPreviousError(best_candidate);
				rc.Report.Error(1501, this.loc, "No overload for method `{0}' takes `{1}' arguments", arg, num2.ToString());
				return;
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000714A4 File Offset: 0x0006F6A4
		private static bool HasUnfilledParams(MemberSpec best_candidate, IParametersMember pm, Arguments args)
		{
			AParametersCollection parameters = ((IParametersMember)best_candidate).Parameters;
			if (!parameters.HasParams)
			{
				return false;
			}
			string text = null;
			for (int num = parameters.Count - 1; num != 0; num--)
			{
				IParameterData parameterData = parameters.FixedParameters[num];
				if ((parameterData.ModFlags & Parameter.Modifier.PARAMS) != Parameter.Modifier.NONE)
				{
					text = parameterData.Name;
					break;
				}
			}
			if (args == null)
			{
				return false;
			}
			foreach (Argument argument in args)
			{
				NamedArgument namedArgument = argument as NamedArgument;
				if (namedArgument != null && namedArgument.Name == text)
				{
					text = null;
					break;
				}
			}
			return text != null && args.Count + 1 == pm.Parameters.Count;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00071570 File Offset: 0x0006F770
		private bool VerifyArguments(ResolveContext ec, ref Arguments args, MemberSpec member, IParametersMember pm, bool chose_params_expanded)
		{
			AParametersCollection parameters = pm.Parameters;
			AParametersCollection parameters2 = ((IParametersMember)member).Parameters;
			TypeSpec[] types = parameters2.Types;
			Parameter.Modifier modifier = Parameter.Modifier.NONE;
			TypeSpec typeSpec = null;
			int i = 0;
			int num = 0;
			Argument argument = null;
			ArrayInitializer arrayInitializer = null;
			bool flag = pm.MemberType.IsPointer;
			int num2 = (args == null) ? 0 : args.Count;
			while (i < num2)
			{
				argument = args[i];
				if (argument != null)
				{
					if (modifier != Parameter.Modifier.PARAMS)
					{
						modifier = parameters2.FixedParameters[i].ModFlags;
						typeSpec = types[i];
						flag |= typeSpec.IsPointer;
						if (modifier == Parameter.Modifier.PARAMS && chose_params_expanded)
						{
							arrayInitializer = new ArrayInitializer(num2 - i, argument.Expr.Location);
							typeSpec = TypeManager.GetElementType(typeSpec);
						}
					}
					if (((argument.Modifier | modifier) & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
					{
						if ((argument.Modifier & Parameter.Modifier.RefOutMask) != (modifier & Parameter.Modifier.RefOutMask))
						{
							break;
						}
						TypeSpec type = argument.Type;
						if (type == typeSpec)
						{
							goto IL_372;
						}
						if (type == InternalType.VarOutType)
						{
							((DeclarationExpression)argument.Expr).Variable.Type = typeSpec;
							goto IL_372;
						}
						if (!TypeSpecComparer.IsEqual(type, typeSpec))
						{
							break;
						}
					}
					NamedArgument namedArgument = argument as NamedArgument;
					if (namedArgument != null)
					{
						int parameterIndexByName = parameters.GetParameterIndexByName(namedArgument.Name);
						if (parameterIndexByName < 0 || parameterIndexByName >= parameters.Count)
						{
							if (this.IsDelegateInvoke)
							{
								ec.Report.SymbolRelatedToPreviousError(this.DelegateType);
								ec.Report.Error(1746, namedArgument.Location, "The delegate `{0}' does not contain a parameter named `{1}'", this.DelegateType.GetSignatureForError(), namedArgument.Name);
							}
							else
							{
								ec.Report.SymbolRelatedToPreviousError(member);
								ec.Report.Error(1739, namedArgument.Location, "The best overloaded method match for `{0}' does not contain a parameter named `{1}'", TypeManager.CSharpSignature(member), namedArgument.Name);
							}
						}
						else if (args[parameterIndexByName] != argument && args[parameterIndexByName] != null)
						{
							if (this.IsDelegateInvoke)
							{
								ec.Report.SymbolRelatedToPreviousError(this.DelegateType);
							}
							else
							{
								ec.Report.SymbolRelatedToPreviousError(member);
							}
							ec.Report.Error(1744, namedArgument.Location, "Named argument `{0}' cannot be used for a parameter which has positional argument specified", namedArgument.Name);
						}
					}
					if (argument.Expr.Type.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
					{
						if ((this.restrictions & OverloadResolver.Restrictions.CovariantDelegate) != OverloadResolver.Restrictions.None && !Delegate.IsTypeCovariant(ec, argument.Expr.Type, typeSpec))
						{
							if (argument.IsExtensionType)
							{
								MemberAccess memberAccess = new MemberAccess(argument.Expr, member.Name, this.loc);
								memberAccess.Error_TypeDoesNotContainDefinition(ec, argument.Expr.Type, memberAccess.Name);
							}
							else
							{
								this.custom_errors.NoArgumentMatch(ec, member);
							}
							return false;
						}
						Expression expression;
						if (argument.IsExtensionType)
						{
							if (argument.Expr.Type == typeSpec || TypeSpecComparer.IsEqual(argument.Expr.Type, typeSpec))
							{
								expression = argument.Expr;
							}
							else
							{
								expression = Convert.ImplicitReferenceConversion(argument.Expr, typeSpec, false);
								if (expression == null)
								{
									expression = Convert.ImplicitBoxingConversion(argument.Expr, argument.Expr.Type, typeSpec);
								}
							}
						}
						else
						{
							expression = Convert.ImplicitConversion(ec, argument.Expr, typeSpec, this.loc);
						}
						if (expression == null)
						{
							break;
						}
						if (arrayInitializer != null)
						{
							arrayInitializer.Add(argument.Expr);
							args.RemoveAt(i--);
							num2--;
							argument.Expr = expression;
						}
						else
						{
							argument.Expr = expression;
						}
					}
				}
				IL_372:
				i++;
				num++;
			}
			if (i != num2)
			{
				while (i < num2)
				{
					Argument argument2 = args[i];
					if (argument2 != null && argument2.Type == InternalType.VarOutType)
					{
						((DeclarationExpression)argument2.Expr).Variable.Type = InternalType.ErrorType;
					}
					i++;
				}
				this.ReportArgumentMismatch(ec, num, member, argument, parameters, typeSpec);
				return false;
			}
			if (arrayInitializer == null && num2 + 1 == parameters.Count)
			{
				if (args == null)
				{
					args = new Arguments(1);
				}
				typeSpec = types[parameters.Count - 1];
				typeSpec = TypeManager.GetElementType(typeSpec);
				flag |= typeSpec.IsPointer;
				arrayInitializer = new ArrayInitializer(0, this.loc);
			}
			if (arrayInitializer != null)
			{
				args.Add(new Argument(new ArrayCreation(new TypeExpression(typeSpec, this.loc), arrayInitializer, this.loc).Resolve(ec)));
				num2++;
			}
			if (flag && !ec.IsUnsafe)
			{
				Expression.UnsafeError(ec, this.loc);
			}
			if (this.type_arguments == null && member.IsGeneric)
			{
				foreach (TypeSpec typeSpec2 in ((MethodSpec)member).TypeArguments)
				{
					if (!typeSpec2.IsAccessible(ec))
					{
						ec.Report.SymbolRelatedToPreviousError(typeSpec2);
						Expression.ErrorIsInaccesible(ec, member.GetSignatureForError(), this.loc);
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x04000973 RID: 2419
		private Location loc;

		// Token: 0x04000974 RID: 2420
		private IList<MemberSpec> members;

		// Token: 0x04000975 RID: 2421
		private TypeArguments type_arguments;

		// Token: 0x04000976 RID: 2422
		private OverloadResolver.IBaseMembersProvider base_provider;

		// Token: 0x04000977 RID: 2423
		private OverloadResolver.IErrorHandler custom_errors;

		// Token: 0x04000978 RID: 2424
		private OverloadResolver.IInstanceQualifier instance_qualifier;

		// Token: 0x04000979 RID: 2425
		private OverloadResolver.Restrictions restrictions;

		// Token: 0x0400097A RID: 2426
		private MethodGroupExpr best_candidate_extension_group;

		// Token: 0x0400097B RID: 2427
		private TypeSpec best_candidate_return_type;

		// Token: 0x0400097C RID: 2428
		private SessionReportPrinter lambda_conv_msgs;

		// Token: 0x0400097D RID: 2429
		[CompilerGenerated]
		private bool <BestCandidateIsDynamic>k__BackingField;

		// Token: 0x020003A7 RID: 935
		[Flags]
		public enum Restrictions
		{
			// Token: 0x04001057 RID: 4183
			None = 0,
			// Token: 0x04001058 RID: 4184
			DelegateInvoke = 1,
			// Token: 0x04001059 RID: 4185
			ProbingOnly = 2,
			// Token: 0x0400105A RID: 4186
			CovariantDelegate = 4,
			// Token: 0x0400105B RID: 4187
			NoBaseMembers = 8,
			// Token: 0x0400105C RID: 4188
			BaseMembersIncluded = 16,
			// Token: 0x0400105D RID: 4189
			GetEnumeratorLookup = 32
		}

		// Token: 0x020003A8 RID: 936
		public interface IBaseMembersProvider
		{
			// Token: 0x060026F4 RID: 9972
			IList<MemberSpec> GetBaseMembers(TypeSpec baseType);

			// Token: 0x060026F5 RID: 9973
			IParametersMember GetOverrideMemberParameters(MemberSpec member);

			// Token: 0x060026F6 RID: 9974
			MethodGroupExpr LookupExtensionMethod(ResolveContext rc);
		}

		// Token: 0x020003A9 RID: 937
		public interface IErrorHandler
		{
			// Token: 0x060026F7 RID: 9975
			bool AmbiguousCandidates(ResolveContext rc, MemberSpec best, MemberSpec ambiguous);

			// Token: 0x060026F8 RID: 9976
			bool ArgumentMismatch(ResolveContext rc, MemberSpec best, Argument a, int index);

			// Token: 0x060026F9 RID: 9977
			bool NoArgumentMatch(ResolveContext rc, MemberSpec best);

			// Token: 0x060026FA RID: 9978
			bool TypeInferenceFailed(ResolveContext rc, MemberSpec best);
		}

		// Token: 0x020003AA RID: 938
		public interface IInstanceQualifier
		{
			// Token: 0x170008DD RID: 2269
			// (get) Token: 0x060026FB RID: 9979
			TypeSpec InstanceType { get; }

			// Token: 0x060026FC RID: 9980
			bool CheckProtectedMemberAccess(ResolveContext rc, MemberSpec member);
		}

		// Token: 0x020003AB RID: 939
		private sealed class NoBaseMembers : OverloadResolver.IBaseMembersProvider
		{
			// Token: 0x060026FD RID: 9981 RVA: 0x000055E7 File Offset: 0x000037E7
			public IList<MemberSpec> GetBaseMembers(TypeSpec baseType)
			{
				return null;
			}

			// Token: 0x060026FE RID: 9982 RVA: 0x000055E7 File Offset: 0x000037E7
			public IParametersMember GetOverrideMemberParameters(MemberSpec member)
			{
				return null;
			}

			// Token: 0x060026FF RID: 9983 RVA: 0x000055E7 File Offset: 0x000037E7
			public MethodGroupExpr LookupExtensionMethod(ResolveContext rc)
			{
				return null;
			}

			// Token: 0x06002700 RID: 9984 RVA: 0x00002CCC File Offset: 0x00000ECC
			public NoBaseMembers()
			{
			}

			// Token: 0x06002701 RID: 9985 RVA: 0x000BAD49 File Offset: 0x000B8F49
			// Note: this type is marked as 'beforefieldinit'.
			static NoBaseMembers()
			{
			}

			// Token: 0x0400105E RID: 4190
			public static readonly OverloadResolver.IBaseMembersProvider Instance = new OverloadResolver.NoBaseMembers();
		}

		// Token: 0x020003AC RID: 940
		private struct AmbiguousCandidate
		{
			// Token: 0x06002702 RID: 9986 RVA: 0x000BAD55 File Offset: 0x000B8F55
			public AmbiguousCandidate(MemberSpec member, AParametersCollection parameters, bool expanded)
			{
				this.Member = member;
				this.Parameters = parameters;
				this.Expanded = expanded;
			}

			// Token: 0x0400105F RID: 4191
			public readonly MemberSpec Member;

			// Token: 0x04001060 RID: 4192
			public readonly bool Expanded;

			// Token: 0x04001061 RID: 4193
			public readonly AParametersCollection Parameters;
		}
	}
}
