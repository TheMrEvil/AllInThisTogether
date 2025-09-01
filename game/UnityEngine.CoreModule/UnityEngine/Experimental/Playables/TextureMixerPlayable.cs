using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200046B RID: 1131
	[NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h")]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x00042C5C File Offset: 0x00040E5C
		public static TextureMixerPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = TextureMixerPlayable.CreateHandle(graph);
			return new TextureMixerPlayable(handle);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00042C7C File Offset: 0x00040E7C
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !TextureMixerPlayable.CreateTextureMixerPlayableInternal(ref graph, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x00042CB0 File Offset: 0x00040EB0
		internal TextureMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<TextureMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TextureMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00042CEC File Offset: 0x00040EEC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00042D04 File Offset: 0x00040F04
		public static implicit operator Playable(TextureMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00042D24 File Offset: 0x00040F24
		public static explicit operator TextureMixerPlayable(Playable playable)
		{
			return new TextureMixerPlayable(playable.GetHandle());
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00042D44 File Offset: 0x00040F44
		public bool Equals(TextureMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600281A RID: 10266
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateTextureMixerPlayableInternal(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000EC8 RID: 3784
		private PlayableHandle m_Handle;
	}
}
