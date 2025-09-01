using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x0000C7B9 File Offset: 0x0000A9B9
		[FreeFunction(Name = "RenderBufferScripting::SetLoadAction", HasExplicitThis = true)]
		internal void SetLoadAction(RenderBufferLoadAction action)
		{
			RenderBuffer.SetLoadAction_Injected(ref this, action);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		[FreeFunction(Name = "RenderBufferScripting::SetStoreAction", HasExplicitThis = true)]
		internal void SetStoreAction(RenderBufferStoreAction action)
		{
			RenderBuffer.SetStoreAction_Injected(ref this, action);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		[FreeFunction(Name = "RenderBufferScripting::GetLoadAction", HasExplicitThis = true)]
		internal RenderBufferLoadAction GetLoadAction()
		{
			return RenderBuffer.GetLoadAction_Injected(ref this);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		[FreeFunction(Name = "RenderBufferScripting::GetStoreAction", HasExplicitThis = true)]
		internal RenderBufferStoreAction GetStoreAction()
		{
			return RenderBuffer.GetStoreAction_Injected(ref this);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		[FreeFunction(Name = "RenderBufferScripting::GetNativeRenderBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeRenderBufferPtr()
		{
			return RenderBuffer.GetNativeRenderBufferPtr_Injected(ref this);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return this.GetLoadAction();
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0000C808 File Offset: 0x0000AA08
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0000C820 File Offset: 0x0000AA20
		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return this.GetStoreAction();
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		// Token: 0x0600086D RID: 2157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoadAction_Injected(ref RenderBuffer _unity_self, RenderBufferLoadAction action);

		// Token: 0x0600086E RID: 2158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStoreAction_Injected(ref RenderBuffer _unity_self, RenderBufferStoreAction action);

		// Token: 0x0600086F RID: 2159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderBufferLoadAction GetLoadAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x06000870 RID: 2160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderBufferStoreAction GetStoreAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x06000871 RID: 2161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeRenderBufferPtr_Injected(ref RenderBuffer _unity_self);

		// Token: 0x040003C3 RID: 963
		internal int m_RenderTextureInstanceID;

		// Token: 0x040003C4 RID: 964
		internal IntPtr m_BufferPtr;
	}
}
