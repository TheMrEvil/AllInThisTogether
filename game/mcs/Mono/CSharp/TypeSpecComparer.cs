using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020002E0 RID: 736
	internal static class TypeSpecComparer
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x000AC7F0 File Offset: 0x000AA9F0
		public static bool Equals(TypeSpec[] x, TypeSpec[] y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (!TypeSpecComparer.IsEqual(x[i], y[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000AC82C File Offset: 0x000AAA2C
		public static bool IsEqual(TypeSpec a, TypeSpec b)
		{
			if (a == b)
			{
				return a.Kind != MemberKind.InternalCompilerType || a.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.IsArray)
			{
				ArrayContainer arrayContainer = (ArrayContainer)a;
				ArrayContainer arrayContainer2 = b as ArrayContainer;
				return arrayContainer2 != null && arrayContainer.Rank == arrayContainer2.Rank && TypeSpecComparer.IsEqual(arrayContainer.Element, arrayContainer2.Element);
			}
			if (!a.IsGeneric || !b.IsGeneric)
			{
				return (a.BuiltinType == BuiltinTypeSpec.Type.Dynamic || b.BuiltinType == BuiltinTypeSpec.Type.Dynamic) && (b.BuiltinType == BuiltinTypeSpec.Type.Object || a.BuiltinType == BuiltinTypeSpec.Type.Object);
			}
			if (a.MemberDefinition != b.MemberDefinition)
			{
				return false;
			}
			while (TypeSpecComparer.Equals(a.TypeArguments, b.TypeArguments))
			{
				a = a.DeclaringType;
				b = b.DeclaringType;
				if (a == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000AC912 File Offset: 0x000AAB12
		// Note: this type is marked as 'beforefieldinit'.
		static TypeSpecComparer()
		{
		}

		// Token: 0x04000D70 RID: 3440
		public static readonly TypeSpecComparer.DefaultImpl Default = new TypeSpecComparer.DefaultImpl();

		// Token: 0x02000406 RID: 1030
		public class DefaultImpl : IEqualityComparer<TypeSpec[]>
		{
			// Token: 0x0600283B RID: 10299 RVA: 0x000BECEC File Offset: 0x000BCEEC
			bool IEqualityComparer<TypeSpec[]>.Equals(TypeSpec[] x, TypeSpec[] y)
			{
				if (x == y)
				{
					return true;
				}
				if (x.Length != y.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i] != y[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600283C RID: 10300 RVA: 0x000BED24 File Offset: 0x000BCF24
			int IEqualityComparer<TypeSpec[]>.GetHashCode(TypeSpec[] obj)
			{
				int num = 0;
				for (int i = 0; i < obj.Length; i++)
				{
					num = (num << 5) - num + obj[i].GetHashCode();
				}
				return num;
			}

			// Token: 0x0600283D RID: 10301 RVA: 0x00002CCC File Offset: 0x00000ECC
			public DefaultImpl()
			{
			}
		}

		// Token: 0x02000407 RID: 1031
		public static class Override
		{
			// Token: 0x0600283E RID: 10302 RVA: 0x000BED54 File Offset: 0x000BCF54
			public static bool IsEqual(TypeSpec a, TypeSpec b)
			{
				if (a == b)
				{
					return true;
				}
				TypeParameterSpec typeParameterSpec = a as TypeParameterSpec;
				if (typeParameterSpec != null)
				{
					TypeParameterSpec typeParameterSpec2 = b as TypeParameterSpec;
					return typeParameterSpec2 != null && typeParameterSpec.IsMethodOwned == typeParameterSpec2.IsMethodOwned && typeParameterSpec.DeclaredPosition == typeParameterSpec2.DeclaredPosition;
				}
				ArrayContainer arrayContainer = a as ArrayContainer;
				if (arrayContainer != null)
				{
					ArrayContainer arrayContainer2 = b as ArrayContainer;
					return arrayContainer2 != null && arrayContainer.Rank == arrayContainer2.Rank && TypeSpecComparer.Override.IsEqual(arrayContainer.Element, arrayContainer2.Element);
				}
				if (a.BuiltinType == BuiltinTypeSpec.Type.Dynamic || b.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return b.BuiltinType == BuiltinTypeSpec.Type.Object || a.BuiltinType == BuiltinTypeSpec.Type.Object;
				}
				if (a.MemberDefinition != b.MemberDefinition)
				{
					return false;
				}
				for (;;)
				{
					for (int i = 0; i < a.TypeArguments.Length; i++)
					{
						if (!TypeSpecComparer.Override.IsEqual(a.TypeArguments[i], b.TypeArguments[i]))
						{
							return false;
						}
					}
					a = a.DeclaringType;
					b = b.DeclaringType;
					if (a == null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600283F RID: 10303 RVA: 0x000BEE54 File Offset: 0x000BD054
			public static bool IsEqual(TypeSpec[] a, TypeSpec[] b)
			{
				if (a == b)
				{
					return true;
				}
				if (a.Length != b.Length)
				{
					return false;
				}
				for (int i = 0; i < a.Length; i++)
				{
					if (!TypeSpecComparer.Override.IsEqual(a[i], b[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002840 RID: 10304 RVA: 0x000BEE90 File Offset: 0x000BD090
			public static bool IsSame(TypeSpec[] a, TypeSpec[] b)
			{
				if (a == b)
				{
					return true;
				}
				if (a == null || b == null || a.Length != b.Length)
				{
					return false;
				}
				for (int i = 0; i < a.Length; i++)
				{
					bool flag = false;
					for (int j = 0; j < b.Length; j++)
					{
						if (TypeSpecComparer.Override.IsEqual(a[i], b[j]))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002841 RID: 10305 RVA: 0x000BEEE8 File Offset: 0x000BD0E8
			public static bool IsEqual(AParametersCollection a, AParametersCollection b)
			{
				if (a == b)
				{
					return true;
				}
				if (a.Count != b.Count)
				{
					return false;
				}
				for (int i = 0; i < a.Count; i++)
				{
					if (!TypeSpecComparer.Override.IsEqual(a.Types[i], b.Types[i]))
					{
						return false;
					}
					if ((a.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != (b.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x02000408 RID: 1032
		public static class Variant
		{
			// Token: 0x06002842 RID: 10306 RVA: 0x000BEF58 File Offset: 0x000BD158
			public static bool IsEqual(TypeSpec type1, TypeSpec type2)
			{
				if (!type1.IsGeneric || !type2.IsGeneric)
				{
					return false;
				}
				ITypeDefinition memberDefinition = type2.MemberDefinition;
				if (type1.MemberDefinition != memberDefinition)
				{
					return false;
				}
				TypeSpec[] typeArguments = type1.TypeArguments;
				TypeSpec[] typeArguments2 = type2.TypeArguments;
				TypeParameterSpec[] typeParameters = memberDefinition.TypeParameters;
				if (!type1.IsInterface && !type1.IsDelegate)
				{
					return false;
				}
				for (int i = 0; i < typeParameters.Length; i++)
				{
					if (!TypeSpecComparer.IsEqual(typeArguments[i], typeArguments2[i]))
					{
						Variance variance = typeParameters[i].Variance;
						if (variance == Variance.None)
						{
							return false;
						}
						if (variance == Variance.Covariant)
						{
							if (!Convert.ImplicitReferenceConversionExists(typeArguments[i], typeArguments2[i]))
							{
								return false;
							}
						}
						else if (!Convert.ImplicitReferenceConversionExists(typeArguments2[i], typeArguments[i]))
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x02000409 RID: 1033
		public static class Unify
		{
			// Token: 0x06002843 RID: 10307 RVA: 0x000BF00C File Offset: 0x000BD20C
			public static bool IsEqual(TypeSpec a, TypeSpec b)
			{
				if (a.MemberDefinition != b.MemberDefinition)
				{
					IList<TypeSpec> interfaces = a.Interfaces;
					if (interfaces != null)
					{
						foreach (TypeSpec typeSpec in interfaces)
						{
							if (typeSpec.Arity > 0 && TypeSpecComparer.Unify.IsEqual(typeSpec, b))
							{
								return true;
							}
						}
						return false;
					}
					return false;
				}
				TypeSpec[] typeArguments = a.TypeArguments;
				TypeSpec[] typeArguments2 = b.TypeArguments;
				for (int i = 0; i < typeArguments.Length; i++)
				{
					if (!TypeSpecComparer.Unify.MayBecomeEqualGenericTypes(typeArguments[i], typeArguments2[i]))
					{
						return false;
					}
				}
				return !a.IsNested || !b.IsNested || TypeSpecComparer.Unify.IsEqual(a.DeclaringType, b.DeclaringType);
			}

			// Token: 0x06002844 RID: 10308 RVA: 0x000BF0D8 File Offset: 0x000BD2D8
			private static bool ContainsTypeParameter(TypeSpec tparam, TypeSpec type)
			{
				TypeSpec[] typeArguments = type.TypeArguments;
				for (int i = 0; i < typeArguments.Length; i++)
				{
					if (tparam == typeArguments[i])
					{
						return true;
					}
					if (TypeSpecComparer.Unify.ContainsTypeParameter(tparam, typeArguments[i]))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002845 RID: 10309 RVA: 0x000BF110 File Offset: 0x000BD310
			private static bool MayBecomeEqualGenericTypes(TypeSpec a, TypeSpec b)
			{
				if (a.IsGenericParameter)
				{
					if (b.IsArray)
					{
						return false;
					}
					if (b.IsGenericParameter)
					{
						return a != b && a.DeclaringType == b.DeclaringType;
					}
					return !TypeSpecComparer.Unify.ContainsTypeParameter(a, b);
				}
				else
				{
					if (b.IsGenericParameter)
					{
						return TypeSpecComparer.Unify.MayBecomeEqualGenericTypes(b, a);
					}
					if (TypeManager.IsGenericType(a) || TypeManager.IsGenericType(b))
					{
						return TypeSpecComparer.Unify.IsEqual(a, b);
					}
					ArrayContainer arrayContainer = a as ArrayContainer;
					if (arrayContainer != null)
					{
						ArrayContainer arrayContainer2 = b as ArrayContainer;
						return arrayContainer2 != null && arrayContainer.Rank == arrayContainer2.Rank && TypeSpecComparer.Unify.MayBecomeEqualGenericTypes(arrayContainer.Element, arrayContainer2.Element);
					}
					return false;
				}
			}
		}
	}
}
