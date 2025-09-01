using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000127 RID: 295
	public class PredefinedDebuggableAttribute : PredefinedAttribute
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x0003742F File Offset: 0x0003562F
		public PredefinedDebuggableAttribute(ModuleContainer module, string ns, string name) : base(module, ns, name)
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00037490 File Offset: 0x00035690
		public void EmitAttribute(AssemblyBuilder builder, DebuggableAttribute.DebuggingModes modes)
		{
			PredefinedDebuggableAttribute debuggable = this.module.PredefinedAttributes.Debuggable;
			if (!debuggable.Define())
			{
				return;
			}
			MethodSpec methodSpec = null;
			foreach (MemberSpec memberSpec in MemberCache.FindMembers(debuggable.TypeSpec, Mono.CSharp.Constructor.ConstructorName, true))
			{
				MethodSpec methodSpec2 = (MethodSpec)memberSpec;
				if (methodSpec2.Parameters.Count == 1 && methodSpec2.Parameters.Types[0].Kind == MemberKind.Enum)
				{
					methodSpec = methodSpec2;
				}
			}
			if (methodSpec == null)
			{
				return;
			}
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode((int)modes);
			attributeEncoder.EncodeEmptyNamedArguments();
			builder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}
	}
}
