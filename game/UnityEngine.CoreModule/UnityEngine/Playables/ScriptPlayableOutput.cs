using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200044D RID: 1101
	[RequiredByNativeCode]
	public struct ScriptPlayableOutput : IPlayableOutput
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x00041054 File Offset: 0x0003F254
		public static ScriptPlayableOutput Create(PlayableGraph graph, string name)
		{
			PlayableOutputHandle handle;
			bool flag = !graph.CreateScriptOutputInternal(name, out handle);
			ScriptPlayableOutput result;
			if (flag)
			{
				result = ScriptPlayableOutput.Null;
			}
			else
			{
				result = new ScriptPlayableOutput(handle);
			}
			return result;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00041088 File Offset: 0x0003F288
		internal ScriptPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<ScriptPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not a ScriptPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x000410C4 File Offset: 0x0003F2C4
		public static ScriptPlayableOutput Null
		{
			get
			{
				return new ScriptPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000410E0 File Offset: 0x0003F2E0
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000410F8 File Offset: 0x0003F2F8
		public static implicit operator PlayableOutput(ScriptPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00041118 File Offset: 0x0003F318
		public static explicit operator ScriptPlayableOutput(PlayableOutput output)
		{
			return new ScriptPlayableOutput(output.GetHandle());
		}

		// Token: 0x04000E35 RID: 3637
		private PlayableOutputHandle m_Handle;
	}
}
