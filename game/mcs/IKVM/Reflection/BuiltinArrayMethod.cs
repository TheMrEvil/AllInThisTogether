using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000060 RID: 96
	internal sealed class BuiltinArrayMethod : ArrayMethod
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00010F4D File Offset: 0x0000F14D
		internal BuiltinArrayMethod(Module module, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes) : base(module, arrayClass, methodName, callingConvention, returnType, parameterTypes)
		{
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00010F5E File Offset: 0x0000F15E
		public override MethodAttributes Attributes
		{
			get
			{
				if (!(this.Name == ".ctor"))
				{
					return MethodAttributes.Public;
				}
				return MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.RTSpecialName;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000022F4 File Offset: 0x000004F4
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00010F79 File Offset: 0x0000F179
		public override int MetadataToken
		{
			get
			{
				return 100663296;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000055E7 File Offset: 0x000037E7
		public override MethodBody GetMethodBody()
		{
			return null;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00010F80 File Offset: 0x0000F180
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] array = new ParameterInfo[this.parameterTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new BuiltinArrayMethod.ParameterInfoImpl(this, this.parameterTypes[i], i);
			}
			return array;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00010FBC File Offset: 0x0000F1BC
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new BuiltinArrayMethod.ParameterInfoImpl(this, this.ReturnType, -1);
			}
		}

		// Token: 0x02000335 RID: 821
		private sealed class ParameterInfoImpl : ParameterInfo
		{
			// Token: 0x060025BC RID: 9660 RVA: 0x000B406B File Offset: 0x000B226B
			internal ParameterInfoImpl(MethodInfo method, Type type, int pos)
			{
				this.method = method;
				this.type = type;
				this.pos = pos;
			}

			// Token: 0x17000892 RID: 2194
			// (get) Token: 0x060025BD RID: 9661 RVA: 0x000B4088 File Offset: 0x000B2288
			public override Type ParameterType
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000893 RID: 2195
			// (get) Token: 0x060025BE RID: 9662 RVA: 0x000055E7 File Offset: 0x000037E7
			public override string Name
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000894 RID: 2196
			// (get) Token: 0x060025BF RID: 9663 RVA: 0x000022F4 File Offset: 0x000004F4
			public override ParameterAttributes Attributes
			{
				get
				{
					return ParameterAttributes.None;
				}
			}

			// Token: 0x17000895 RID: 2197
			// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000B4090 File Offset: 0x000B2290
			public override int Position
			{
				get
				{
					return this.pos;
				}
			}

			// Token: 0x17000896 RID: 2198
			// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000055E7 File Offset: 0x000037E7
			public override object RawDefaultValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060025C2 RID: 9666 RVA: 0x000B4098 File Offset: 0x000B2298
			public override CustomModifiers __GetCustomModifiers()
			{
				return default(CustomModifiers);
			}

			// Token: 0x060025C3 RID: 9667 RVA: 0x000B3F81 File Offset: 0x000B2181
			public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
			{
				fieldMarshal = default(FieldMarshal);
				return false;
			}

			// Token: 0x17000897 RID: 2199
			// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000B40AE File Offset: 0x000B22AE
			public override MemberInfo Member
			{
				get
				{
					if (!this.method.IsConstructor)
					{
						return this.method;
					}
					return new ConstructorInfoImpl(this.method);
				}
			}

			// Token: 0x17000898 RID: 2200
			// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000B3F93 File Offset: 0x000B2193
			public override int MetadataToken
			{
				get
				{
					return 134217728;
				}
			}

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000B40CF File Offset: 0x000B22CF
			internal override Module Module
			{
				get
				{
					return this.method.Module;
				}
			}

			// Token: 0x04000E6F RID: 3695
			private readonly MethodInfo method;

			// Token: 0x04000E70 RID: 3696
			private readonly Type type;

			// Token: 0x04000E71 RID: 3697
			private readonly int pos;
		}
	}
}
