using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x0200000A RID: 10
	public abstract class ConstructorInfo : MethodBase
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000031F6 File Offset: 0x000013F6
		internal ConstructorInfo()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031FE File Offset: 0x000013FE
		public sealed override string ToString()
		{
			return this.GetMethodInfo().ToString();
		}

		// Token: 0x0600007E RID: 126
		internal abstract MethodInfo GetMethodInfo();

		// Token: 0x0600007F RID: 127 RVA: 0x0000320B File Offset: 0x0000140B
		internal override MethodBase BindTypeParameters(Type type)
		{
			return new ConstructorInfoImpl((MethodInfo)this.GetMethodInfo().BindTypeParameters(type));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003223 File Offset: 0x00001423
		public sealed override MethodBase __GetMethodOnTypeDefinition()
		{
			return new ConstructorInfoImpl((MethodInfo)this.GetMethodInfo().__GetMethodOnTypeDefinition());
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000212D File Offset: 0x0000032D
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000323A File Offset: 0x0000143A
		public sealed override int __MethodRVA
		{
			get
			{
				return this.GetMethodInfo().__MethodRVA;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003247 File Offset: 0x00001447
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return this.GetMethodInfo().ContainsGenericParameters;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003254 File Offset: 0x00001454
		public ParameterInfo __ReturnParameter
		{
			get
			{
				return new ParameterInfoWrapper(this, this.GetMethodInfo().ReturnParameter);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003268 File Offset: 0x00001468
		public sealed override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parameters = this.GetMethodInfo().GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i] = new ParameterInfoWrapper(this, parameters[i]);
			}
			return parameters;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000329C File Offset: 0x0000149C
		public sealed override CallingConventions CallingConvention
		{
			get
			{
				return this.GetMethodInfo().CallingConvention;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000032A9 File Offset: 0x000014A9
		public sealed override MethodAttributes Attributes
		{
			get
			{
				return this.GetMethodInfo().Attributes;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032B6 File Offset: 0x000014B6
		public sealed override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.GetMethodInfo().GetMethodImplementationFlags();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000032C3 File Offset: 0x000014C3
		public sealed override Type DeclaringType
		{
			get
			{
				return this.GetMethodInfo().DeclaringType;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000032D0 File Offset: 0x000014D0
		public sealed override string Name
		{
			get
			{
				return this.GetMethodInfo().Name;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000032DD File Offset: 0x000014DD
		public sealed override int MetadataToken
		{
			get
			{
				return this.GetMethodInfo().MetadataToken;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000032EA File Offset: 0x000014EA
		public sealed override Module Module
		{
			get
			{
				return this.GetMethodInfo().Module;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032F7 File Offset: 0x000014F7
		public sealed override MethodBody GetMethodBody()
		{
			return this.GetMethodInfo().GetMethodBody();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003304 File Offset: 0x00001504
		public sealed override bool __IsMissing
		{
			get
			{
				return this.GetMethodInfo().__IsMissing;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003311 File Offset: 0x00001511
		internal sealed override int ParameterCount
		{
			get
			{
				return this.GetMethodInfo().ParameterCount;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000331E File Offset: 0x0000151E
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new ConstructorInfoWithReflectedType(type, this);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003327 File Offset: 0x00001527
		internal sealed override int GetCurrentToken()
		{
			return this.GetMethodInfo().GetCurrentToken();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003334 File Offset: 0x00001534
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			return this.GetMethodInfo().GetPseudoCustomAttributes(attributeType);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003342 File Offset: 0x00001542
		internal sealed override bool IsBaked
		{
			get
			{
				return this.GetMethodInfo().IsBaked;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000334F File Offset: 0x0000154F
		internal sealed override MethodSignature MethodSignature
		{
			get
			{
				return this.GetMethodInfo().MethodSignature;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000335C File Offset: 0x0000155C
		internal sealed override int ImportTo(ModuleBuilder module)
		{
			return this.GetMethodInfo().ImportTo(module);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000336A File Offset: 0x0000156A
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructorInfo()
		{
		}

		// Token: 0x0400002E RID: 46
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x0400002F RID: 47
		public static readonly string TypeConstructorName = ".cctor";
	}
}
