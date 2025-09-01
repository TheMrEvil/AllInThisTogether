using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200005C RID: 92
	internal static class GenericServices
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00006F8C File Offset: 0x0000518C
		internal static IList<Type> GetPureGenericParameters(this Type type)
		{
			Assumes.NotNull<Type>(type);
			if (type.IsGenericType && type.ContainsGenericParameters)
			{
				List<Type> pureGenericParameters = new List<Type>();
				GenericServices.TraverseGenericType(type, delegate(Type t)
				{
					if (t.IsGenericParameter)
					{
						pureGenericParameters.Add(t);
					}
				});
				return pureGenericParameters;
			}
			return Type.EmptyTypes;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006FE0 File Offset: 0x000051E0
		internal static int GetPureGenericArity(this Type type)
		{
			Assumes.NotNull<Type>(type);
			int genericArity = 0;
			if (type.IsGenericType && type.ContainsGenericParameters)
			{
				new List<Type>();
				GenericServices.TraverseGenericType(type, delegate(Type t)
				{
					if (t.IsGenericParameter)
					{
						int genericArity = genericArity;
						genericArity++;
					}
				});
			}
			return genericArity;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007030 File Offset: 0x00005230
		private static void TraverseGenericType(Type type, Action<Type> onType)
		{
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					GenericServices.TraverseGenericType(genericArguments[i], onType);
				}
			}
			onType(type);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000706A File Offset: 0x0000526A
		public static int[] GetGenericParametersOrder(Type type)
		{
			return (from parameter in type.GetPureGenericParameters()
			select parameter.GenericParameterPosition).ToArray<int>();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000709C File Offset: 0x0000529C
		public static string GetGenericName(string originalGenericName, int[] genericParametersOrder, int genericArity)
		{
			string[] array = new string[genericArity];
			for (int i = 0; i < genericParametersOrder.Length; i++)
			{
				array[genericParametersOrder[i]] = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", i);
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			object[] args = array;
			return string.Format(invariantCulture, originalGenericName, args);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000070E8 File Offset: 0x000052E8
		public static T[] Reorder<T>(T[] original, int[] genericParametersOrder)
		{
			T[] array = new T[genericParametersOrder.Length];
			for (int i = 0; i < genericParametersOrder.Length; i++)
			{
				array[i] = original[genericParametersOrder[i]];
			}
			return array;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007120 File Offset: 0x00005320
		public static IEnumerable<Type> CreateTypeSpecializations(this Type[] types, Type[] specializationTypes)
		{
			if (types == null)
			{
				return null;
			}
			return from type in types
			select type.CreateTypeSpecialization(specializationTypes);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007154 File Offset: 0x00005354
		public static Type CreateTypeSpecialization(this Type type, Type[] specializationTypes)
		{
			if (!type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				return specializationTypes[type.GenericParameterPosition];
			}
			Type[] genericArguments = type.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type2 = genericArguments[i];
				array[i] = (type2.IsGenericParameter ? specializationTypes[type2.GenericParameterPosition] : type2);
			}
			return type.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000071C0 File Offset: 0x000053C0
		public static bool CanSpecialize(Type type, IEnumerable<Type> constraints, GenericParameterAttributes attributes)
		{
			return GenericServices.CanSpecialize(type, constraints) && GenericServices.CanSpecialize(type, attributes);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000071D4 File Offset: 0x000053D4
		public static bool CanSpecialize(Type type, IEnumerable<Type> constraintTypes)
		{
			if (constraintTypes == null)
			{
				return true;
			}
			foreach (Type type2 in constraintTypes)
			{
				if (type2 != null && !type2.IsAssignableFrom(type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007234 File Offset: 0x00005434
		public static bool CanSpecialize(Type type, GenericParameterAttributes attributes)
		{
			if (attributes == GenericParameterAttributes.None)
			{
				return true;
			}
			if ((attributes & GenericParameterAttributes.ReferenceTypeConstraint) != GenericParameterAttributes.None && type.IsValueType)
			{
				return false;
			}
			if ((attributes & GenericParameterAttributes.DefaultConstructorConstraint) != GenericParameterAttributes.None && !type.IsValueType && type.GetConstructor(Type.EmptyTypes) == null)
			{
				return false;
			}
			if ((attributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
			{
				if (!type.IsValueType)
				{
					return false;
				}
				if (Nullable.GetUnderlyingType(type) != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0200005D RID: 93
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06000254 RID: 596 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000255 RID: 597 RVA: 0x00007298 File Offset: 0x00005498
			internal void <GetPureGenericParameters>b__0(Type t)
			{
				if (t.IsGenericParameter)
				{
					this.pureGenericParameters.Add(t);
				}
			}

			// Token: 0x040000FA RID: 250
			public List<Type> pureGenericParameters;
		}

		// Token: 0x0200005E RID: 94
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000256 RID: 598 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000257 RID: 599 RVA: 0x000072B0 File Offset: 0x000054B0
			internal void <GetPureGenericArity>b__0(Type t)
			{
				if (t.IsGenericParameter)
				{
					int num = this.genericArity;
					this.genericArity = num + 1;
				}
			}

			// Token: 0x040000FB RID: 251
			public int genericArity;
		}

		// Token: 0x0200005F RID: 95
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000258 RID: 600 RVA: 0x000072D5 File Offset: 0x000054D5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000259 RID: 601 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x0600025A RID: 602 RVA: 0x000072E1 File Offset: 0x000054E1
			internal int <GetGenericParametersOrder>b__3_0(Type parameter)
			{
				return parameter.GenericParameterPosition;
			}

			// Token: 0x040000FC RID: 252
			public static readonly GenericServices.<>c <>9 = new GenericServices.<>c();

			// Token: 0x040000FD RID: 253
			public static Func<Type, int> <>9__3_0;
		}

		// Token: 0x02000060 RID: 96
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x0600025B RID: 603 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x0600025C RID: 604 RVA: 0x000072E9 File Offset: 0x000054E9
			internal Type <CreateTypeSpecializations>b__0(Type type)
			{
				return type.CreateTypeSpecialization(this.specializationTypes);
			}

			// Token: 0x040000FE RID: 254
			public Type[] specializationTypes;
		}
	}
}
