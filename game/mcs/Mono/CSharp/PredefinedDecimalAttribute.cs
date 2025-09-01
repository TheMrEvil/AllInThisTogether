using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000128 RID: 296
	public class PredefinedDecimalAttribute : PredefinedAttribute
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x0003742F File Offset: 0x0003562F
		public PredefinedDecimalAttribute(ModuleContainer module, string ns, string name) : base(module, ns, name)
		{
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00037560 File Offset: 0x00035760
		public void EmitAttribute(ParameterBuilder builder, decimal value, Location loc)
		{
			MethodSpec methodSpec = this.module.PredefinedMembers.DecimalConstantAttributeCtor.Resolve(loc);
			if (methodSpec == null)
			{
				return;
			}
			int[] bits = decimal.GetBits(value);
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode((byte)(bits[3] >> 16));
			attributeEncoder.Encode((byte)(bits[3] >> 31));
			attributeEncoder.Encode((uint)bits[2]);
			attributeEncoder.Encode((uint)bits[1]);
			attributeEncoder.Encode((uint)bits[0]);
			attributeEncoder.EncodeEmptyNamedArguments();
			builder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000375E8 File Offset: 0x000357E8
		public void EmitAttribute(FieldBuilder builder, decimal value, Location loc)
		{
			MethodSpec methodSpec = this.module.PredefinedMembers.DecimalConstantAttributeCtor.Resolve(loc);
			if (methodSpec == null)
			{
				return;
			}
			int[] bits = decimal.GetBits(value);
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode((byte)((bits[3] & 16711680) >> 16));
			attributeEncoder.Encode((byte)(bits[3] >> 31 << 7));
			attributeEncoder.Encode((uint)bits[2]);
			attributeEncoder.Encode((uint)bits[1]);
			attributeEncoder.Encode((uint)bits[0]);
			attributeEncoder.EncodeEmptyNamedArguments();
			builder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}
	}
}
