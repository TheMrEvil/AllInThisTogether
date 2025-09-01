using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009A RID: 154
	internal sealed class ParameterInfoImpl : ParameterInfo
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x0001A7CF File Offset: 0x000189CF
		internal ParameterInfoImpl(MethodDefImpl method, int position, int index)
		{
			this.method = method;
			this.position = position;
			this.index = index;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001A7EC File Offset: 0x000189EC
		public override string Name
		{
			get
			{
				if (this.index != -1)
				{
					return ((ModuleReader)this.Module).GetString(this.Module.Param.records[this.index].Name);
				}
				return null;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001A829 File Offset: 0x00018A29
		public override Type ParameterType
		{
			get
			{
				if (this.position != -1)
				{
					return this.method.MethodSignature.GetParameterType(this.method, this.position);
				}
				return this.method.MethodSignature.GetReturnType(this.method);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001A867 File Offset: 0x00018A67
		public override ParameterAttributes Attributes
		{
			get
			{
				if (this.index != -1)
				{
					return (ParameterAttributes)this.Module.Param.records[this.index].Flags;
				}
				return ParameterAttributes.None;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001A894 File Offset: 0x00018A94
		public override int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001A89C File Offset: 0x00018A9C
		public override object RawDefaultValue
		{
			get
			{
				if ((this.Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.None)
				{
					return this.Module.Constant.GetRawConstantValue(this.Module, this.MetadataToken);
				}
				Universe universe = this.Module.universe;
				if (this.ParameterType == universe.System_Decimal)
				{
					Type system_Runtime_CompilerServices_DecimalConstantAttribute = universe.System_Runtime_CompilerServices_DecimalConstantAttribute;
					if (system_Runtime_CompilerServices_DecimalConstantAttribute != null)
					{
						foreach (CustomAttributeData customAttributeData in CustomAttributeData.__GetCustomAttributes(this, system_Runtime_CompilerServices_DecimalConstantAttribute, false))
						{
							IList<CustomAttributeTypedArgument> constructorArguments = customAttributeData.ConstructorArguments;
							if (constructorArguments.Count == 5)
							{
								if (constructorArguments[0].ArgumentType == universe.System_Byte && constructorArguments[1].ArgumentType == universe.System_Byte && constructorArguments[2].ArgumentType == universe.System_Int32 && constructorArguments[3].ArgumentType == universe.System_Int32 && constructorArguments[4].ArgumentType == universe.System_Int32)
								{
									return new decimal((int)constructorArguments[4].Value, (int)constructorArguments[3].Value, (int)constructorArguments[2].Value, (byte)constructorArguments[1].Value > 0, (byte)constructorArguments[0].Value);
								}
								if (constructorArguments[0].ArgumentType == universe.System_Byte && constructorArguments[1].ArgumentType == universe.System_Byte && constructorArguments[2].ArgumentType == universe.System_UInt32 && constructorArguments[3].ArgumentType == universe.System_UInt32 && constructorArguments[4].ArgumentType == universe.System_UInt32)
								{
									return new decimal((int)((uint)constructorArguments[4].Value), (int)((uint)constructorArguments[3].Value), (int)((uint)constructorArguments[2].Value), (byte)constructorArguments[1].Value > 0, (byte)constructorArguments[0].Value);
								}
							}
						}
					}
				}
				if ((this.Attributes & ParameterAttributes.Optional) != ParameterAttributes.None)
				{
					return Missing.Value;
				}
				return null;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		public override CustomModifiers __GetCustomModifiers()
		{
			if (this.position != -1)
			{
				return this.method.MethodSignature.GetParameterCustomModifiers(this.method, this.position);
			}
			return this.method.MethodSignature.GetReturnTypeCustomModifiers(this.method);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001ABEE File Offset: 0x00018DEE
		public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return FieldMarshal.ReadFieldMarshal(this.Module, this.MetadataToken, out fieldMarshal);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001AC02 File Offset: 0x00018E02
		public override MemberInfo Member
		{
			get
			{
				return this.method.Module.ResolveMethod(this.method.MetadataToken);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001AC1F File Offset: 0x00018E1F
		public override int MetadataToken
		{
			get
			{
				return (8 << 24) + this.index + 1;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001AC2E File Offset: 0x00018E2E
		internal override Module Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x04000321 RID: 801
		private readonly MethodDefImpl method;

		// Token: 0x04000322 RID: 802
		private readonly int position;

		// Token: 0x04000323 RID: 803
		private readonly int index;
	}
}
