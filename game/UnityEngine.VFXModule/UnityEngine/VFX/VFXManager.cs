using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000011 RID: 17
	[RequiredByNativeCode]
	[NativeHeader("Modules/VFX/Public/VFXManager.h")]
	[StaticAccessor("GetVFXManager()", StaticAccessorType.Dot)]
	public static class VFXManager
	{
		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VisualEffect[] GetComponents();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006B RID: 107
		internal static extern ScriptableObject runtimeResources { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		public static extern float fixedTimeStep { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		public static extern float maxDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000070 RID: 112
		internal static extern string renderPipeSettingsPath { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002756 File Offset: 0x00000956
		public static void ProcessCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam, VFXManager.kDefaultCameraXRSettings);
			VFXManager.ProcessCameraCommand(cam, null, VFXManager.kDefaultCameraXRSettings);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002772 File Offset: 0x00000972
		public static void PrepareCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam, VFXManager.kDefaultCameraXRSettings);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002781 File Offset: 0x00000981
		public static void PrepareCamera([NotNull("NullExceptionObject")] Camera cam, VFXCameraXRSettings camXRSettings)
		{
			VFXManager.PrepareCamera_Injected(cam, ref camXRSettings);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000278B File Offset: 0x0000098B
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd)
		{
			VFXManager.ProcessCameraCommand(cam, cmd, VFXManager.kDefaultCameraXRSettings);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000279B File Offset: 0x0000099B
		public static void ProcessCameraCommand([NotNull("NullExceptionObject")] Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings)
		{
			VFXManager.ProcessCameraCommand_Injected(cam, cmd, ref camXRSettings);
		}

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VFXCameraBufferTypes IsCameraBufferNeeded([NotNull("NullExceptionObject")] Camera cam);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCameraBuffer([NotNull("NullExceptionObject")] Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height);

		// Token: 0x06000078 RID: 120 RVA: 0x000027A8 File Offset: 0x000009A8
		// Note: this type is marked as 'beforefieldinit'.
		static VFXManager()
		{
		}

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareCamera_Injected(Camera cam, ref VFXCameraXRSettings camXRSettings);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ProcessCameraCommand_Injected(Camera cam, CommandBuffer cmd, ref VFXCameraXRSettings camXRSettings);

		// Token: 0x040000EB RID: 235
		private static readonly VFXCameraXRSettings kDefaultCameraXRSettings = new VFXCameraXRSettings
		{
			viewTotal = 1U,
			viewCount = 1U,
			viewOffset = 0U
		};
	}
}
