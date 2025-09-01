using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200043B RID: 1083
	[RequiredByNativeCode]
	public struct Playable : IPlayable, IEquatable<Playable>
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x0003F6A0 File Offset: 0x0003D8A0
		public static Playable Null
		{
			get
			{
				return Playable.m_NullPlayable;
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0003F6B8 File Offset: 0x0003D8B8
		public static Playable Create(PlayableGraph graph, int inputCount = 0)
		{
			Playable playable = new Playable(graph.CreatePlayableHandle());
			playable.SetInputCount(inputCount);
			return playable;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0003F6E1 File Offset: 0x0003D8E1
		[VisibleToOtherModules]
		internal Playable(PlayableHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0003F704 File Offset: 0x0003D904
		public bool IsPlayableOfType<T>() where T : struct, IPlayable
		{
			return this.GetHandle().IsPlayableOfType<T>();
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0003F724 File Offset: 0x0003D924
		public Type GetPlayableType()
		{
			return this.GetHandle().GetPlayableType();
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0003F744 File Offset: 0x0003D944
		public bool Equals(Playable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0003F768 File Offset: 0x0003D968
		// Note: this type is marked as 'beforefieldinit'.
		static Playable()
		{
		}

		// Token: 0x04000E10 RID: 3600
		private PlayableHandle m_Handle;

		// Token: 0x04000E11 RID: 3601
		private static readonly Playable m_NullPlayable = new Playable(PlayableHandle.Null);
	}
}
