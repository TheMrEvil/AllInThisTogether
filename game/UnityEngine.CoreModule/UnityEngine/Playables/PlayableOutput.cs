using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000448 RID: 1096
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x00040794 File Offset: 0x0003E994
		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000407AB File Offset: 0x0003E9AB
		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000407B8 File Offset: 0x0003E9B8
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000407D0 File Offset: 0x0003E9D0
		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000407F0 File Offset: 0x0003E9F0
		public Type GetPlayableOutputType()
		{
			return this.GetHandle().GetPlayableOutputType();
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00040810 File Offset: 0x0003EA10
		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00040834 File Offset: 0x0003EA34
		// Note: this type is marked as 'beforefieldinit'.
		static PlayableOutput()
		{
		}

		// Token: 0x04000E2E RID: 3630
		private PlayableOutputHandle m_Handle;

		// Token: 0x04000E2F RID: 3631
		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);
	}
}
