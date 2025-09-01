using System;
using System.Collections.Generic;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x02000029 RID: 41
	public abstract class FieldInfo : MemberInfo
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005501 File Offset: 0x00003701
		internal FieldInfo()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005836 File Offset: 0x00003A36
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000133 RID: 307
		public abstract FieldAttributes Attributes { get; }

		// Token: 0x06000134 RID: 308
		public abstract void __GetDataFromRVA(byte[] data, int offset, int length);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000135 RID: 309
		public abstract int __FieldRVA { get; }

		// Token: 0x06000136 RID: 310
		public abstract object GetRawConstantValue();

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000137 RID: 311
		internal abstract FieldSignature FieldSignature { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005839 File Offset: 0x00003A39
		public Type FieldType
		{
			get
			{
				return this.FieldSignature.FieldType;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005846 File Offset: 0x00003A46
		public CustomModifiers __GetCustomModifiers()
		{
			return this.FieldSignature.GetCustomModifiers();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005854 File Offset: 0x00003A54
		public Type[] GetOptionalCustomModifiers()
		{
			return this.__GetCustomModifiers().GetOptional();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005870 File Offset: 0x00003A70
		public Type[] GetRequiredCustomModifiers()
		{
			return this.__GetCustomModifiers().GetRequired();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000588B File Offset: 0x00003A8B
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005899 File Offset: 0x00003A99
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000058A7 File Offset: 0x00003AA7
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000058B5 File Offset: 0x00003AB5
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000058C6 File Offset: 0x00003AC6
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000058D7 File Offset: 0x00003AD7
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000058E4 File Offset: 0x00003AE4
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000058F1 File Offset: 0x00003AF1
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000058FE File Offset: 0x00003AFE
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000590B File Offset: 0x00003B0B
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005918 File Offset: 0x00003B18
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005925 File Offset: 0x00003B25
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005936 File Offset: 0x00003B36
		public virtual FieldInfo __GetFieldOnTypeDefinition()
		{
			return this;
		}

		// Token: 0x06000149 RID: 329
		public abstract bool __TryGetFieldOffset(out int offset);

		// Token: 0x0600014A RID: 330 RVA: 0x00005939 File Offset: 0x00003B39
		public bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return FieldMarshal.ReadFieldMarshal(this.Module, this.GetCurrentToken(), out fieldMarshal);
		}

		// Token: 0x0600014B RID: 331
		internal abstract int ImportTo(ModuleBuilder module);

		// Token: 0x0600014C RID: 332 RVA: 0x0000594D File Offset: 0x00003B4D
		internal virtual FieldInfo BindTypeParameters(Type type)
		{
			return new GenericFieldInstance(this.DeclaringType.BindTypeParameters(type), this);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005961 File Offset: 0x00003B61
		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static, BindingFlags.Instance);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005985 File Offset: 0x00003B85
		internal sealed override bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			return (this.Attributes & FieldAttributes.FieldAccessMask) > FieldAttributes.Private && MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static | BindingFlags.FlattenHierarchy, BindingFlags.Instance);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000059B5 File Offset: 0x00003BB5
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new FieldInfoWithReflectedType(type, this);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000059C0 File Offset: 0x00003BC0
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			Module module = this.Module;
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			FieldMarshal fm;
			if ((attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_MarshalAsAttribute)) && this.__TryGetFieldMarshal(out fm))
			{
				list.Add(CustomAttributeData.CreateMarshalAsPseudoCustomAttribute(module, fm));
			}
			int offset;
			if ((attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_FieldOffsetAttribute)) && this.__TryGetFieldOffset(out offset))
			{
				list.Add(CustomAttributeData.CreateFieldOffsetPseudoCustomAttribute(module, offset));
			}
			return list;
		}
	}
}
