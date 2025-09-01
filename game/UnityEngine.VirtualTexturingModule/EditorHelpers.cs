using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000003 RID: 3
	[NativeConditional("UNITY_EDITOR")]
	[StaticAccessor("VirtualTexturing::Editor", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class EditorHelpers
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5
		[NativeThrows]
		internal static extern int tileSize { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000006 RID: 6
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ValidateTextureStack([Unmarshalled] [NotNull("ArgumentNullException")] Texture[] textures, out string errorMessage);

		// Token: 0x06000007 RID: 7
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern EditorHelpers.StackValidationResult[] ValidateMaterialTextureStacks([NotNull("ArgumentNullException")] Material mat);

		// Token: 0x06000008 RID: 8
		[NativeConditional("UNITY_EDITOR", "{}")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat[] QuerySupportedFormats();

		// Token: 0x02000004 RID: 4
		[NativeHeader("Runtime/Shaders/SharedMaterialData.h")]
		internal struct StackValidationResult
		{
			// Token: 0x04000002 RID: 2
			public string stackName;

			// Token: 0x04000003 RID: 3
			public string errorMessage;
		}
	}
}
