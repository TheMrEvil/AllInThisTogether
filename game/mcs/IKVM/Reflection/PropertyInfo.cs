using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000054 RID: 84
	public abstract class PropertyInfo : MemberInfo
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00005501 File Offset: 0x00003701
		internal PropertyInfo()
		{
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C9C5 File Offset: 0x0000ABC5
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003FC RID: 1020
		public abstract PropertyAttributes Attributes { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003FD RID: 1021
		public abstract bool CanRead { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003FE RID: 1022
		public abstract bool CanWrite { get; }

		// Token: 0x060003FF RID: 1023
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06000400 RID: 1024
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06000401 RID: 1025
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x06000402 RID: 1026
		public abstract object GetRawConstantValue();

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000403 RID: 1027
		internal abstract bool IsPublic { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000404 RID: 1028
		internal abstract bool IsNonPrivate { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000405 RID: 1029
		internal abstract bool IsStatic { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000406 RID: 1030
		internal abstract PropertySignature PropertySignature { get; }

		// Token: 0x06000407 RID: 1031 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public virtual ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] array = new ParameterInfo[this.PropertySignature.ParameterCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PropertyInfo.ParameterInfoImpl(this, i);
			}
			return array;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000CA03 File Offset: 0x0000AC03
		public Type PropertyType
		{
			get
			{
				return this.PropertySignature.PropertyType;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public CustomModifiers __GetCustomModifiers()
		{
			return this.PropertySignature.GetCustomModifiers();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public Type[] GetRequiredCustomModifiers()
		{
			return this.__GetCustomModifiers().GetRequired();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public Type[] GetOptionalCustomModifiers()
		{
			return this.__GetCustomModifiers().GetOptional();
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000CA57 File Offset: 0x0000AC57
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000CA71 File Offset: 0x0000AC71
		public MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000CA7A File Offset: 0x0000AC7A
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CA83 File Offset: 0x0000AC83
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000CA95 File Offset: 0x0000AC95
		public CallingConventions __CallingConvention
		{
			get
			{
				return this.PropertySignature.CallingConvention;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000CAA2 File Offset: 0x0000ACA2
		internal virtual PropertyInfo BindTypeParameters(Type type)
		{
			return new GenericPropertyInfo(this.DeclaringType.BindTypeParameters(type), this);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00005570 File Offset: 0x00003770
		public override string ToString()
		{
			return this.DeclaringType.ToString() + " " + this.Name;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CAB6 File Offset: 0x0000ACB6
		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static, BindingFlags.Instance);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000CADA File Offset: 0x0000ACDA
		internal sealed override bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			return this.IsNonPrivate && MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static | BindingFlags.FlattenHierarchy, BindingFlags.Instance);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000CB07 File Offset: 0x0000AD07
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new PropertyInfoWithReflectedType(type, this);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000055E7 File Offset: 0x000037E7
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			return null;
		}

		// Token: 0x0200032B RID: 811
		private sealed class ParameterInfoImpl : ParameterInfo
		{
			// Token: 0x0600259F RID: 9631 RVA: 0x000B3F33 File Offset: 0x000B2133
			internal ParameterInfoImpl(PropertyInfo property, int parameter)
			{
				this.property = property;
				this.parameter = parameter;
			}

			// Token: 0x1700088A RID: 2186
			// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000055E7 File Offset: 0x000037E7
			public override string Name
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000B3F49 File Offset: 0x000B2149
			public override Type ParameterType
			{
				get
				{
					return this.property.PropertySignature.GetParameter(this.parameter);
				}
			}

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000022F4 File Offset: 0x000004F4
			public override ParameterAttributes Attributes
			{
				get
				{
					return ParameterAttributes.None;
				}
			}

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000B3F61 File Offset: 0x000B2161
			public override int Position
			{
				get
				{
					return this.parameter;
				}
			}

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public override object RawDefaultValue
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060025A5 RID: 9637 RVA: 0x000B3F69 File Offset: 0x000B2169
			public override CustomModifiers __GetCustomModifiers()
			{
				return this.property.PropertySignature.GetParameterCustomModifiers(this.parameter);
			}

			// Token: 0x060025A6 RID: 9638 RVA: 0x000B3F81 File Offset: 0x000B2181
			public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
			{
				fieldMarshal = default(FieldMarshal);
				return false;
			}

			// Token: 0x1700088F RID: 2191
			// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000B3F8B File Offset: 0x000B218B
			public override MemberInfo Member
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x17000890 RID: 2192
			// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000B3F93 File Offset: 0x000B2193
			public override int MetadataToken
			{
				get
				{
					return 134217728;
				}
			}

			// Token: 0x17000891 RID: 2193
			// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000B3F9A File Offset: 0x000B219A
			internal override Module Module
			{
				get
				{
					return this.property.Module;
				}
			}

			// Token: 0x04000E52 RID: 3666
			private readonly PropertyInfo property;

			// Token: 0x04000E53 RID: 3667
			private readonly int parameter;
		}
	}
}
