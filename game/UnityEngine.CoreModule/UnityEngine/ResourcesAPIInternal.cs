using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001E9 RID: 489
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	internal static class ResourcesAPIInternal
	{
		// Token: 0x0600161F RID: 5663
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06001620 RID: 5664
		[FreeFunction("GetShaderNameRegistry().FindShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader FindShaderByName(string name);

		// Token: 0x06001621 RID: 5665
		[NativeThrows]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[FreeFunction("Resources_Bindings::Load")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object Load(string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		// Token: 0x06001622 RID: 5666
		[NativeThrows]
		[FreeFunction("Resources_Bindings::LoadAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] LoadAll([NotNull("ArgumentNullException")] string path, [NotNull("ArgumentNullException")] Type systemTypeInstance);

		// Token: 0x06001623 RID: 5667
		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ResourceRequest LoadAsyncInternal(string path, Type type);

		// Token: 0x06001624 RID: 5668
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAsset(Object assetToUnload);
	}
}
