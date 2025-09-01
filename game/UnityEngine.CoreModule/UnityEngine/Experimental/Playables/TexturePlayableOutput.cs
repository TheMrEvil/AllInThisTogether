using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200046E RID: 1134
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Graphics/Director/TexturePlayableOutput.h")]
	[StaticAccessor("TexturePlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Export/Director/TexturePlayableOutput.bindings.h")]
	public struct TexturePlayableOutput : IPlayableOutput
	{
		// Token: 0x0600281E RID: 10270 RVA: 0x00042DB8 File Offset: 0x00040FB8
		public static TexturePlayableOutput Create(PlayableGraph graph, string name, RenderTexture target)
		{
			PlayableOutputHandle handle;
			bool flag = !TexturePlayableGraphExtensions.InternalCreateTextureOutput(ref graph, name, out handle);
			TexturePlayableOutput result;
			if (flag)
			{
				result = TexturePlayableOutput.Null;
			}
			else
			{
				TexturePlayableOutput texturePlayableOutput = new TexturePlayableOutput(handle);
				texturePlayableOutput.SetTarget(target);
				result = texturePlayableOutput;
			}
			return result;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00042DF8 File Offset: 0x00040FF8
		internal TexturePlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<TexturePlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TexturePlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x00042E34 File Offset: 0x00041034
		public static TexturePlayableOutput Null
		{
			get
			{
				return new TexturePlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00042E50 File Offset: 0x00041050
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00042E68 File Offset: 0x00041068
		public static implicit operator PlayableOutput(TexturePlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00042E88 File Offset: 0x00041088
		public static explicit operator TexturePlayableOutput(PlayableOutput output)
		{
			return new TexturePlayableOutput(output.GetHandle());
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00042EA8 File Offset: 0x000410A8
		public RenderTexture GetTarget()
		{
			return TexturePlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00042EC5 File Offset: 0x000410C5
		public void SetTarget(RenderTexture value)
		{
			TexturePlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x06002826 RID: 10278
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTexture InternalGetTarget(ref PlayableOutputHandle output);

		// Token: 0x06002827 RID: 10279
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, RenderTexture target);

		// Token: 0x04000EC9 RID: 3785
		private PlayableOutputHandle m_Handle;
	}
}
