using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000484 RID: 1156
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public static class ShaderWarmup
	{
		// Token: 0x060028A5 RID: 10405 RVA: 0x00043494 File Offset: 0x00041694
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShader")]
		public static void WarmupShader(Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShader_Injected(shader, ref setup);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0004349E File Offset: 0x0004169E
		[FreeFunction(Name = "ShaderWarmupScripting::WarmupShaderFromCollection")]
		public static void WarmupShaderFromCollection(ShaderVariantCollection collection, Shader shader, ShaderWarmupSetup setup)
		{
			ShaderWarmup.WarmupShaderFromCollection_Injected(collection, shader, ref setup);
		}

		// Token: 0x060028A7 RID: 10407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShader_Injected(Shader shader, ref ShaderWarmupSetup setup);

		// Token: 0x060028A8 RID: 10408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmupShaderFromCollection_Injected(ShaderVariantCollection collection, Shader shader, ref ShaderWarmupSetup setup);
	}
}
