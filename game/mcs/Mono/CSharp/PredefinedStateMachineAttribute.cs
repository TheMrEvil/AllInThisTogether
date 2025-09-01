using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000129 RID: 297
	public class PredefinedStateMachineAttribute : PredefinedAttribute
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0003742F File Offset: 0x0003562F
		public PredefinedStateMachineAttribute(ModuleContainer module, string ns, string name) : base(module, ns, name)
		{
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00037678 File Offset: 0x00035878
		public void EmitAttribute(MethodBuilder builder, StateMachine type)
		{
			MethodSpec methodSpec = this.module.PredefinedMembers.AsyncStateMachineAttributeCtor.Get();
			if (methodSpec == null)
			{
				return;
			}
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.EncodeTypeName(type);
			attributeEncoder.EncodeEmptyNamedArguments();
			builder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}
	}
}
