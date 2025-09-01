using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200022C RID: 556
	public class TypeInferenceContext
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x0008860C File Offset: 0x0008680C
		public TypeInferenceContext(TypeSpec[] typeArguments)
		{
			if (typeArguments.Length == 0)
			{
				throw new ArgumentException("Empty generic arguments");
			}
			this.fixed_types = new TypeSpec[typeArguments.Length];
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i].IsGenericParameter)
				{
					if (this.bounds == null)
					{
						this.bounds = new List<TypeInferenceContext.BoundInfo>[typeArguments.Length];
						this.tp_args = new TypeSpec[typeArguments.Length];
					}
					this.tp_args[i] = typeArguments[i];
				}
				else
				{
					this.fixed_types[i] = typeArguments[i];
				}
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00088690 File Offset: 0x00086890
		public TypeInferenceContext()
		{
			this.fixed_types = new TypeSpec[1];
			this.tp_args = new TypeSpec[1];
			this.tp_args[0] = InternalType.Arglist;
			this.bounds = new List<TypeInferenceContext.BoundInfo>[1];
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000886C9 File Offset: 0x000868C9
		public TypeSpec[] InferredTypeArguments
		{
			get
			{
				return this.fixed_types;
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000886D1 File Offset: 0x000868D1
		public void AddCommonTypeBound(TypeSpec type)
		{
			this.AddToBounds(new TypeInferenceContext.BoundInfo(type, TypeInferenceContext.BoundKind.Lower), 0, false);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000886E2 File Offset: 0x000868E2
		public void AddCommonTypeBoundAsync(TypeSpec type)
		{
			this.AddToBounds(new TypeInferenceContext.BoundInfo(type, TypeInferenceContext.BoundKind.Lower), 0, true);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000886F4 File Offset: 0x000868F4
		private void AddToBounds(TypeInferenceContext.BoundInfo bound, int index, bool voidAllowed)
		{
			if ((bound.Type.Kind == MemberKind.Void && !voidAllowed) || bound.Type.IsPointer || bound.Type.IsSpecialRuntimeType || bound.Type == InternalType.MethodGroup || bound.Type == InternalType.AnonymousMethod || bound.Type == InternalType.VarOutType)
			{
				return;
			}
			List<TypeInferenceContext.BoundInfo> list = this.bounds[index];
			if (list == null)
			{
				list = new List<TypeInferenceContext.BoundInfo>(2);
				list.Add(bound);
				this.bounds[index] = list;
				return;
			}
			if (list.Contains(bound))
			{
				return;
			}
			list.Add(bound);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00088790 File Offset: 0x00086990
		private bool AllTypesAreFixed(TypeSpec[] types)
		{
			foreach (TypeSpec typeSpec in types)
			{
				if (typeSpec.IsGenericParameter)
				{
					if (!this.IsFixed(typeSpec))
					{
						return false;
					}
				}
				else if (typeSpec.IsGeneric && !this.AllTypesAreFixed(typeSpec.TypeArguments))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000887E0 File Offset: 0x000869E0
		public int ExactInference(TypeSpec u, TypeSpec v)
		{
			if (v.IsArray)
			{
				if (!u.IsArray)
				{
					return 0;
				}
				ArrayContainer arrayContainer = (ArrayContainer)u;
				ArrayContainer arrayContainer2 = (ArrayContainer)v;
				if (arrayContainer.Rank != arrayContainer2.Rank)
				{
					return 0;
				}
				return this.ExactInference(arrayContainer.Element, arrayContainer2.Element);
			}
			else if (TypeManager.IsGenericType(v))
			{
				if (!TypeManager.IsGenericType(u) || v.MemberDefinition != u.MemberDefinition)
				{
					return 0;
				}
				TypeSpec[] typeArguments = TypeManager.GetTypeArguments(u);
				TypeSpec[] typeArguments2 = TypeManager.GetTypeArguments(v);
				if (typeArguments.Length != typeArguments2.Length)
				{
					return 0;
				}
				int num = 0;
				for (int i = 0; i < typeArguments.Length; i++)
				{
					num += this.ExactInference(typeArguments[i], typeArguments2[i]);
				}
				return Math.Min(1, num);
			}
			else
			{
				int num2 = this.IsUnfixed(v);
				if (num2 == -1)
				{
					return 0;
				}
				this.AddToBounds(new TypeInferenceContext.BoundInfo(u, TypeInferenceContext.BoundKind.Exact), num2, false);
				return 1;
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000888BC File Offset: 0x00086ABC
		public bool FixAllTypes(ResolveContext ec)
		{
			for (int i = 0; i < this.tp_args.Length; i++)
			{
				if (!this.FixType(ec, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000888EC File Offset: 0x00086AEC
		public bool FixDependentTypes(ResolveContext ec, ref bool fixed_any)
		{
			for (int i = 0; i < this.tp_args.Length; i++)
			{
				if (this.fixed_types[i] == null && this.bounds[i] != null)
				{
					if (!this.FixType(ec, i))
					{
						return false;
					}
					fixed_any = true;
				}
			}
			return true;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00088930 File Offset: 0x00086B30
		public bool FixIndependentTypeArguments(ResolveContext ec, TypeSpec[] methodParameters, ref bool fixed_any)
		{
			List<TypeSpec> list = new List<TypeSpec>(this.tp_args);
			int i = 0;
			while (i < methodParameters.Length)
			{
				TypeSpec typeSpec = methodParameters[i];
				if (typeSpec.IsDelegate)
				{
					goto IL_2D;
				}
				if (typeSpec.IsExpressionTreeType)
				{
					typeSpec = TypeManager.GetTypeArguments(typeSpec)[0];
					goto IL_2D;
				}
				IL_6F:
				i++;
				continue;
				IL_2D:
				if (typeSpec.IsGenericParameter)
				{
					goto IL_6F;
				}
				TypeSpec typeSpec2 = Delegate.GetInvokeMethod(typeSpec).ReturnType;
				while (typeSpec2.IsArray)
				{
					typeSpec2 = ((ArrayContainer)typeSpec2).Element;
				}
				if (typeSpec2.IsGenericParameter || TypeManager.IsGenericType(typeSpec2))
				{
					this.RemoveDependentTypes(list, typeSpec2);
					goto IL_6F;
				}
				goto IL_6F;
			}
			foreach (TypeSpec typeSpec3 in list)
			{
				if (typeSpec3 != null)
				{
					int num = this.IsUnfixed(typeSpec3);
					if (num >= 0 && !this.FixType(ec, num))
					{
						return false;
					}
				}
			}
			fixed_any = (list.Count > 0);
			return true;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00088A24 File Offset: 0x00086C24
		public bool FixType(ResolveContext ec, int i)
		{
			if (this.fixed_types[i] != null)
			{
				throw new InternalErrorException("Type argument has been already fixed");
			}
			List<TypeInferenceContext.BoundInfo> list = this.bounds[i];
			if (list == null)
			{
				return false;
			}
			if (list.Count == 1)
			{
				TypeSpec type = list[0].Type;
				if (type == InternalType.NullLiteral)
				{
					return false;
				}
				this.fixed_types[i] = type;
				return true;
			}
			else
			{
				bool[] array = new bool[list.Count];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = true;
				}
				for (int k = 0; k < array.Length; k++)
				{
					TypeInferenceContext.BoundInfo boundInfo = list[k];
					int num = 0;
					switch (boundInfo.Kind)
					{
					case TypeInferenceContext.BoundKind.Exact:
						while (num != array.Length)
						{
							if (k != num && array[num] && list[num].Type != boundInfo.Type)
							{
								array[num] = false;
							}
							num++;
						}
						break;
					case TypeInferenceContext.BoundKind.Lower:
						while (num != array.Length)
						{
							if (k != num && array[num] && !Convert.ImplicitConversionExists(ec, boundInfo.GetTypeExpression(), list[num].Type))
							{
								array[num] = false;
							}
							num++;
						}
						break;
					case TypeInferenceContext.BoundKind.Upper:
						while (num != array.Length)
						{
							if (k != num && array[num] && !Convert.ImplicitConversionExists(ec, list[num].GetTypeExpression(), boundInfo.Type))
							{
								array[num] = false;
							}
							num++;
						}
						break;
					}
				}
				TypeSpec typeSpec = null;
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l])
					{
						TypeInferenceContext.BoundInfo boundInfo2 = list[l];
						if (boundInfo2.Type != typeSpec)
						{
							int num2 = 0;
							while (num2 < array.Length && (l == num2 || !array[num2] || Convert.ImplicitConversionExists(ec, list[num2].GetTypeExpression(), boundInfo2.Type)))
							{
								num2++;
							}
							if (num2 == array.Length)
							{
								if (typeSpec != null)
								{
									if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
									{
										goto IL_204;
									}
									if (boundInfo2.Type.BuiltinType != BuiltinTypeSpec.Type.Dynamic && typeSpec != boundInfo2.Type)
									{
										return false;
									}
								}
								typeSpec = boundInfo2.Type;
							}
						}
					}
					IL_204:;
				}
				if (typeSpec == null)
				{
					return false;
				}
				this.fixed_types[i] = typeSpec;
				return true;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00088C54 File Offset: 0x00086E54
		public bool HasBounds(int pos)
		{
			return this.bounds[pos] != null;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00088C64 File Offset: 0x00086E64
		public TypeSpec InflateGenericArgument(IModuleContext context, TypeSpec parameter)
		{
			TypeParameterSpec typeParameterSpec = parameter as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				if (!typeParameterSpec.IsMethodOwned)
				{
					return parameter;
				}
				if (typeParameterSpec.DeclaredPosition < this.tp_args.Length && this.tp_args[typeParameterSpec.DeclaredPosition] == parameter)
				{
					return this.fixed_types[typeParameterSpec.DeclaredPosition] ?? parameter;
				}
				return parameter;
			}
			else
			{
				InflatedTypeSpec inflatedTypeSpec = parameter as InflatedTypeSpec;
				if (inflatedTypeSpec != null)
				{
					TypeSpec[] array = new TypeSpec[inflatedTypeSpec.TypeArguments.Length];
					for (int i = 0; i < array.Length; i++)
					{
						TypeSpec typeSpec = this.InflateGenericArgument(context, inflatedTypeSpec.TypeArguments[i]);
						if (typeSpec == null)
						{
							return null;
						}
						array[i] = typeSpec;
					}
					return inflatedTypeSpec.GetDefinition().MakeGenericType(context, array);
				}
				ArrayContainer arrayContainer = parameter as ArrayContainer;
				if (arrayContainer != null)
				{
					TypeSpec typeSpec2 = this.InflateGenericArgument(context, arrayContainer.Element);
					if (typeSpec2 != arrayContainer.Element)
					{
						return ArrayContainer.MakeType(context.Module, typeSpec2);
					}
				}
				return parameter;
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00088D44 File Offset: 0x00086F44
		public bool IsReturnTypeNonDependent(MethodSpec invoke, TypeSpec returnType)
		{
			AParametersCollection parameters = invoke.Parameters;
			if (parameters.IsEmpty)
			{
				return true;
			}
			while (returnType.IsArray)
			{
				returnType = ((ArrayContainer)returnType).Element;
			}
			if (returnType.IsGenericParameter)
			{
				if (this.IsFixed(returnType))
				{
					return false;
				}
			}
			else
			{
				if (!TypeManager.IsGenericType(returnType))
				{
					return false;
				}
				TypeSpec[] typeArguments = TypeManager.GetTypeArguments(returnType);
				if (this.AllTypesAreFixed(typeArguments))
				{
					return false;
				}
			}
			return this.AllTypesAreFixed(parameters.Types);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00088DB2 File Offset: 0x00086FB2
		private bool IsFixed(TypeSpec type)
		{
			return this.IsUnfixed(type) == -1;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00088DC0 File Offset: 0x00086FC0
		private int IsUnfixed(TypeSpec type)
		{
			if (!type.IsGenericParameter)
			{
				return -1;
			}
			int i = 0;
			while (i < this.tp_args.Length)
			{
				if (this.tp_args[i] == type)
				{
					if (this.fixed_types[i] == null)
					{
						return i;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return -1;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00088E02 File Offset: 0x00087002
		public int LowerBoundInference(TypeSpec u, TypeSpec v)
		{
			return this.LowerBoundInference(u, v, false);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00088E10 File Offset: 0x00087010
		private int LowerBoundInference(TypeSpec u, TypeSpec v, bool inversed)
		{
			int num = this.IsUnfixed(v);
			if (num != -1)
			{
				this.AddToBounds(new TypeInferenceContext.BoundInfo(u, inversed ? TypeInferenceContext.BoundKind.Upper : TypeInferenceContext.BoundKind.Lower), num, false);
				return 1;
			}
			ArrayContainer arrayContainer = u as ArrayContainer;
			if (arrayContainer == null)
			{
				if (v.IsGenericOrParentIsGeneric)
				{
					List<TypeSpec> list = new List<TypeSpec>();
					ITypeDefinition memberDefinition = v.MemberDefinition;
					for (TypeSpec typeSpec = u; typeSpec != null; typeSpec = typeSpec.BaseType)
					{
						if (memberDefinition == typeSpec.MemberDefinition)
						{
							list.Add(typeSpec);
						}
						if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							list.Add(typeSpec);
						}
					}
					if (u.Interfaces != null)
					{
						foreach (TypeSpec typeSpec2 in u.Interfaces)
						{
							if (memberDefinition == typeSpec2.MemberDefinition)
							{
								list.Add(typeSpec2);
							}
						}
					}
					TypeSpec[] array = null;
					TypeSpec[] allTypeArguments = TypeSpec.GetAllTypeArguments(v);
					foreach (TypeSpec typeSpec3 in list)
					{
						if (array != null)
						{
							TypeSpec[] allTypeArguments2 = TypeSpec.GetAllTypeArguments(typeSpec3);
							if (!TypeSpecComparer.Equals(array, allTypeArguments2))
							{
								return 0;
							}
							array = allTypeArguments2;
						}
						else if (typeSpec3.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							array = new TypeSpec[allTypeArguments.Length];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = typeSpec3;
							}
						}
						else
						{
							array = TypeSpec.GetAllTypeArguments(typeSpec3);
						}
					}
					if (array != null)
					{
						int num2 = 0;
						int num3 = -1;
						TypeParameterSpec[] array2 = null;
						for (int j = 0; j < array.Length; j++)
						{
							if (num3 < 0)
							{
								while (v.Arity == 0)
								{
									v = v.DeclaringType;
								}
								array2 = v.MemberDefinition.TypeParameters;
								num3 = array2.Length - 1;
							}
							Variance variance = array2[num3--].Variance;
							TypeSpec typeSpec4 = array[j];
							if (variance == Variance.None || TypeSpec.IsValueType(typeSpec4))
							{
								if (this.ExactInference(typeSpec4, allTypeArguments[j]) == 0)
								{
									num2++;
								}
							}
							else
							{
								bool inversed2 = (variance == Variance.Contravariant && !inversed) || (variance == Variance.Covariant && inversed);
								if (this.LowerBoundInference(typeSpec4, allTypeArguments[j], inversed2) == 0)
								{
									num2++;
								}
							}
						}
						return num2;
					}
					return 0;
				}
				return 0;
			}
			ArrayContainer arrayContainer2 = v as ArrayContainer;
			if (arrayContainer2 != null)
			{
				if (arrayContainer.Rank != arrayContainer2.Rank)
				{
					return 0;
				}
				if (TypeSpec.IsValueType(arrayContainer.Element))
				{
					return this.ExactInference(arrayContainer.Element, arrayContainer2.Element);
				}
				return this.LowerBoundInference(arrayContainer.Element, arrayContainer2.Element, inversed);
			}
			else
			{
				if (arrayContainer.Rank != 1 || !v.IsArrayGenericInterface)
				{
					return 0;
				}
				TypeSpec v2 = TypeManager.GetTypeArguments(v)[0];
				if (TypeSpec.IsValueType(arrayContainer.Element))
				{
					return this.ExactInference(arrayContainer.Element, v2);
				}
				return this.LowerBoundInference(arrayContainer.Element, v2);
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000890F8 File Offset: 0x000872F8
		public int OutputTypeInference(ResolveContext ec, Expression e, TypeSpec t)
		{
			AnonymousMethodExpression anonymousMethodExpression = e as AnonymousMethodExpression;
			if (anonymousMethodExpression != null)
			{
				TypeSpec typeSpec = anonymousMethodExpression.InferReturnType(ec, this, t);
				MethodSpec invokeMethod = Delegate.GetInvokeMethod(t);
				if (typeSpec != null)
				{
					TypeSpec returnType = invokeMethod.ReturnType;
					return this.LowerBoundInference(typeSpec, returnType) + 1;
				}
				AParametersCollection parameters = invokeMethod.Parameters;
				if (anonymousMethodExpression.Parameters.Count != parameters.Count)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (!(e is MethodGroupExpr))
				{
					return this.LowerBoundInference(e.Type, t) * 2;
				}
				if (!t.IsDelegate)
				{
					if (!t.IsExpressionTreeType)
					{
						return 0;
					}
					t = TypeManager.GetTypeArguments(t)[0];
				}
				MethodSpec invokeMethod2 = Delegate.GetInvokeMethod(t);
				TypeSpec returnType2 = invokeMethod2.ReturnType;
				if (!this.IsReturnTypeNonDependent(invokeMethod2, returnType2))
				{
					return 0;
				}
				TypeSpec[] array = new TypeSpec[invokeMethod2.Parameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					TypeSpec typeSpec2 = this.InflateGenericArgument(ec, invokeMethod2.Parameters.Types[i]);
					if (typeSpec2 == null)
					{
						return 0;
					}
					array[i] = typeSpec2;
				}
				MethodGroupExpr methodGroupExpr = (MethodGroupExpr)e;
				Arguments arguments = DelegateCreation.CreateDelegateMethodArguments(ec, invokeMethod2.Parameters, array, e.Location);
				methodGroupExpr = methodGroupExpr.OverloadResolve(ec, ref arguments, null, OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.CovariantDelegate);
				if (methodGroupExpr == null)
				{
					return 0;
				}
				return this.LowerBoundInference(methodGroupExpr.BestCandidateReturnType, returnType2) + 1;
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0008923C File Offset: 0x0008743C
		private void RemoveDependentTypes(List<TypeSpec> types, TypeSpec returnType)
		{
			int num = this.IsUnfixed(returnType);
			if (num >= 0)
			{
				types[num] = null;
				return;
			}
			if (TypeManager.IsGenericType(returnType))
			{
				foreach (TypeSpec returnType2 in TypeManager.GetTypeArguments(returnType))
				{
					this.RemoveDependentTypes(types, returnType2);
				}
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00089288 File Offset: 0x00087488
		public bool UnfixedVariableExists
		{
			get
			{
				TypeSpec[] array = this.fixed_types;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x04000A67 RID: 2663
		private readonly TypeSpec[] tp_args;

		// Token: 0x04000A68 RID: 2664
		private readonly TypeSpec[] fixed_types;

		// Token: 0x04000A69 RID: 2665
		private readonly List<TypeInferenceContext.BoundInfo>[] bounds;

		// Token: 0x020003C4 RID: 964
		protected enum BoundKind
		{
			// Token: 0x040010B6 RID: 4278
			Exact,
			// Token: 0x040010B7 RID: 4279
			Lower,
			// Token: 0x040010B8 RID: 4280
			Upper
		}

		// Token: 0x020003C5 RID: 965
		private struct BoundInfo : IEquatable<TypeInferenceContext.BoundInfo>
		{
			// Token: 0x06002750 RID: 10064 RVA: 0x000BBF19 File Offset: 0x000BA119
			public BoundInfo(TypeSpec type, TypeInferenceContext.BoundKind kind)
			{
				this.Type = type;
				this.Kind = kind;
			}

			// Token: 0x06002751 RID: 10065 RVA: 0x000BBF29 File Offset: 0x000BA129
			public override int GetHashCode()
			{
				return this.Type.GetHashCode();
			}

			// Token: 0x06002752 RID: 10066 RVA: 0x000BBF36 File Offset: 0x000BA136
			public Expression GetTypeExpression()
			{
				return new TypeExpression(this.Type, Location.Null);
			}

			// Token: 0x06002753 RID: 10067 RVA: 0x000BBF48 File Offset: 0x000BA148
			public bool Equals(TypeInferenceContext.BoundInfo other)
			{
				return this.Type == other.Type && this.Kind == other.Kind;
			}

			// Token: 0x040010B9 RID: 4281
			public readonly TypeSpec Type;

			// Token: 0x040010BA RID: 4282
			public readonly TypeInferenceContext.BoundKind Kind;
		}
	}
}
