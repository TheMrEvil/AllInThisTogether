using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000126 RID: 294
	public class PredefinedDebuggerBrowsableAttribute : PredefinedAttribute
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x0003742F File Offset: 0x0003562F
		public PredefinedDebuggerBrowsableAttribute(ModuleContainer module, string ns, string name) : base(module, ns, name)
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003743C File Offset: 0x0003563C
		public void EmitAttribute(FieldBuilder builder, DebuggerBrowsableState state)
		{
			MethodSpec methodSpec = this.module.PredefinedMembers.DebuggerBrowsableAttributeCtor.Get();
			if (methodSpec == null)
			{
				return;
			}
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode((int)state);
			attributeEncoder.EncodeEmptyNamedArguments();
			builder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}
	}
}
