using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200001A RID: 26
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h")]
	[NativeConditional("ENABLE_XR")]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000DE RID: 222 RVA: 0x0000416C File Offset: 0x0000236C
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x000041A4 File Offset: 0x000023A4
		public event Action<bool> displayFocusChanged
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.displayFocusChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.displayFocusChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.displayFocusChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.displayFocusChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000041DC File Offset: 0x000023DC
		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			bool flag = this.displayFocusChanged != null;
			if (flag)
			{
				this.displayFocusChanged(focus);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004204 File Offset: 0x00002404
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004224 File Offset: 0x00002424
		[Obsolete("singlePassRenderingDisabled{get;set;} is deprecated. Use textureLayout and supportedTextureLayouts instead.", false)]
		public bool singlePassRenderingDisabled
		{
			get
			{
				return (this.textureLayout & XRDisplaySubsystem.TextureLayout.Texture2DArray) == (XRDisplaySubsystem.TextureLayout)0;
			}
			set
			{
				if (value)
				{
					this.textureLayout = XRDisplaySubsystem.TextureLayout.SeparateTexture2Ds;
				}
				else
				{
					bool flag = (this.supportedTextureLayouts & XRDisplaySubsystem.TextureLayout.Texture2DArray) > (XRDisplaySubsystem.TextureLayout)0;
					if (flag)
					{
						this.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
					}
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E3 RID: 227
		public extern bool displayOpaque { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E4 RID: 228
		// (set) Token: 0x060000E5 RID: 229
		public extern bool contentProtectionEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		public extern float scaleOfAllViewports { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E9 RID: 233
		public extern float scaleOfAllRenderTargets { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EA RID: 234
		// (set) Token: 0x060000EB RID: 235
		public extern float zNear { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000ED RID: 237
		public extern float zFar { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EE RID: 238
		// (set) Token: 0x060000EF RID: 239
		public extern bool sRGB { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F0 RID: 240
		// (set) Token: 0x060000F1 RID: 241
		public extern float occlusionMaskScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkTransformLateLatched(Transform transform, XRDisplaySubsystem.LateLatchNode nodeType);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F3 RID: 243
		// (set) Token: 0x060000F4 RID: 244
		public extern XRDisplaySubsystem.TextureLayout textureLayout { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F5 RID: 245
		public extern XRDisplaySubsystem.TextureLayout supportedTextureLayouts { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F6 RID: 246
		// (set) Token: 0x060000F7 RID: 247
		public extern XRDisplaySubsystem.ReprojectionMode reprojectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x0000425C File Offset: 0x0000245C
		public void SetFocusPlane(Vector3 point, Vector3 normal, Vector3 velocity)
		{
			this.SetFocusPlane_Injected(ref point, ref normal, ref velocity);
		}

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMSAALevel(int level);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FA RID: 250
		// (set) Token: 0x060000FB RID: 251
		public extern bool disableLegacyRenderer { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetRenderPassCount();

		// Token: 0x060000FD RID: 253 RVA: 0x0000426C File Offset: 0x0000246C
		public void GetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			bool flag = !this.Internal_TryGetRenderPass(renderPassIndex, out renderPass);
			if (flag)
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		// Token: 0x060000FE RID: 254
		[NativeMethod("TryGetRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryGetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass);

		// Token: 0x060000FF RID: 255 RVA: 0x00004298 File Offset: 0x00002498
		public void EndRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryEndRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x06000100 RID: 256
		[NativeMethod("TryEndRecordingIfLateLatched")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryEndRecordingIfLateLatched(Camera camera);

		// Token: 0x06000101 RID: 257 RVA: 0x000042D0 File Offset: 0x000024D0
		public void BeginRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryBeginRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x06000102 RID: 258
		[NativeMethod("TryBeginRecordingIfLateLatched")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryBeginRecordingIfLateLatched(Camera camera);

		// Token: 0x06000103 RID: 259 RVA: 0x00004308 File Offset: 0x00002508
		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			bool flag = !this.Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters);
			if (!flag)
			{
				return;
			}
			bool flag2 = camera == null;
			if (flag2)
			{
				throw new ArgumentNullException("camera");
			}
			throw new IndexOutOfRangeException("cullingPassIndex");
		}

		// Token: 0x06000104 RID: 260
		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
		[NativeMethod("TryGetCullingParams")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		// Token: 0x06000105 RID: 261
		[NativeMethod("TryGetAppGPUTimeLastFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetAppGPUTimeLastFrame(out float gpuTimeLastFrame);

		// Token: 0x06000106 RID: 262
		[NativeMethod("TryGetCompositorGPUTimeLastFrame")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetCompositorGPUTimeLastFrame(out float gpuTimeLastFrameCompositor);

		// Token: 0x06000107 RID: 263
		[NativeMethod("TryGetDroppedFrameCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		// Token: 0x06000108 RID: 264
		[NativeMethod("TryGetFramePresentCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetFramePresentCount(out int framePresentCount);

		// Token: 0x06000109 RID: 265
		[NativeMethod("TryGetDisplayRefreshRate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetDisplayRefreshRate(out float displayRefreshRate);

		// Token: 0x0600010A RID: 266
		[NativeMethod("TryGetMotionToPhoton")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryGetMotionToPhoton(out float motionToPhoton);

		// Token: 0x0600010B RID: 267
		[NativeMethod(Name = "GetTextureForRenderPass", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern RenderTexture GetRenderTextureForRenderPass(int renderPass);

		// Token: 0x0600010C RID: 268
		[NativeMethod(Name = "GetSharedDepthTextureForRenderPass", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern RenderTexture GetSharedDepthTextureForRenderPass(int renderPass);

		// Token: 0x0600010D RID: 269
		[NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPreferredMirrorBlitMode();

		// Token: 0x0600010E RID: 270
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "SetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPreferredMirrorBlitMode(int blitMode);

		// Token: 0x0600010F RID: 271 RVA: 0x0000434C File Offset: 0x0000254C
		[Obsolete("GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc) is deprecated. Use GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc, int) instead.", false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc)
		{
			return this.GetMirrorViewBlitDesc(mirrorRt, out outDesc, -1);
		}

		// Token: 0x06000110 RID: 272
		[NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode);

		// Token: 0x06000111 RID: 273 RVA: 0x00004368 File Offset: 0x00002568
		[Obsolete("AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool) is deprecated. Use AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool, int) instead.", false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate)
		{
			return this.AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, -1);
		}

		// Token: 0x06000112 RID: 274
		[NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode);

		// Token: 0x06000113 RID: 275 RVA: 0x00004383 File Offset: 0x00002583
		public XRDisplaySubsystem()
		{
		}

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFocusPlane_Injected(ref Vector3 point, ref Vector3 normal, ref Vector3 velocity);

		// Token: 0x040000B0 RID: 176
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<bool> displayFocusChanged;

		// Token: 0x0200001B RID: 27
		public enum LateLatchNode
		{
			// Token: 0x040000B2 RID: 178
			Head,
			// Token: 0x040000B3 RID: 179
			LeftHand,
			// Token: 0x040000B4 RID: 180
			RightHand
		}

		// Token: 0x0200001C RID: 28
		[Flags]
		public enum TextureLayout
		{
			// Token: 0x040000B6 RID: 182
			Texture2DArray = 1,
			// Token: 0x040000B7 RID: 183
			SingleTexture2D = 2,
			// Token: 0x040000B8 RID: 184
			SeparateTexture2Ds = 4
		}

		// Token: 0x0200001D RID: 29
		public enum ReprojectionMode
		{
			// Token: 0x040000BA RID: 186
			Unspecified,
			// Token: 0x040000BB RID: 187
			PositionAndOrientation,
			// Token: 0x040000BC RID: 188
			OrientationOnly,
			// Token: 0x040000BD RID: 189
			None
		}

		// Token: 0x0200001E RID: 30
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			// Token: 0x040000BE RID: 190
			public Matrix4x4 view;

			// Token: 0x040000BF RID: 191
			public Matrix4x4 projection;

			// Token: 0x040000C0 RID: 192
			public Rect viewport;

			// Token: 0x040000C1 RID: 193
			public Mesh occlusionMesh;

			// Token: 0x040000C2 RID: 194
			public int textureArraySlice;

			// Token: 0x040000C3 RID: 195
			public Matrix4x4 previousView;

			// Token: 0x040000C4 RID: 196
			public bool isPreviousViewValid;
		}

		// Token: 0x0200001F RID: 31
		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		public struct XRRenderPass
		{
			// Token: 0x06000115 RID: 277 RVA: 0x0000438C File Offset: 0x0000258C
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter)
			{
				XRDisplaySubsystem.XRRenderPass.GetRenderParameter_Injected(ref this, camera, renderParameterIndex, out renderParameter);
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00004397 File Offset: 0x00002597
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			public int GetRenderParameterCount()
			{
				return XRDisplaySubsystem.XRRenderPass.GetRenderParameterCount_Injected(ref this);
			}

			// Token: 0x06000117 RID: 279
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRenderParameter_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self, Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter);

			// Token: 0x06000118 RID: 280
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetRenderParameterCount_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self);

			// Token: 0x040000C5 RID: 197
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000C6 RID: 198
			public int renderPassIndex;

			// Token: 0x040000C7 RID: 199
			public RenderTargetIdentifier renderTarget;

			// Token: 0x040000C8 RID: 200
			public RenderTextureDescriptor renderTargetDesc;

			// Token: 0x040000C9 RID: 201
			public bool hasMotionVectorPass;

			// Token: 0x040000CA RID: 202
			public RenderTargetIdentifier motionVectorRenderTarget;

			// Token: 0x040000CB RID: 203
			public RenderTextureDescriptor motionVectorRenderTargetDesc;

			// Token: 0x040000CC RID: 204
			public bool shouldFillOutDepth;

			// Token: 0x040000CD RID: 205
			public int cullingPassIndex;
		}

		// Token: 0x02000020 RID: 32
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			// Token: 0x040000CE RID: 206
			public RenderTexture srcTex;

			// Token: 0x040000CF RID: 207
			public int srcTexArraySlice;

			// Token: 0x040000D0 RID: 208
			public Rect srcRect;

			// Token: 0x040000D1 RID: 209
			public Rect destRect;
		}

		// Token: 0x02000021 RID: 33
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			// Token: 0x06000119 RID: 281 RVA: 0x0000439F File Offset: 0x0000259F
			[NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetBlitParameter(int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter)
			{
				XRDisplaySubsystem.XRMirrorViewBlitDesc.GetBlitParameter_Injected(ref this, blitParameterIndex, out blitParameter);
			}

			// Token: 0x0600011A RID: 282
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBlitParameter_Injected(ref XRDisplaySubsystem.XRMirrorViewBlitDesc _unity_self, int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter);

			// Token: 0x040000D2 RID: 210
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000D3 RID: 211
			public bool nativeBlitAvailable;

			// Token: 0x040000D4 RID: 212
			public bool nativeBlitInvalidStates;

			// Token: 0x040000D5 RID: 213
			public int blitParamsCount;
		}
	}
}
