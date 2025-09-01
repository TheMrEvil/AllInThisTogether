using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x0200002B RID: 43
	[StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h")]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00003690 File Offset: 0x00001890
		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			PlayableOutputHandle handle;
			bool flag = !AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out handle);
			AudioPlayableOutput result;
			if (flag)
			{
				result = AudioPlayableOutput.Null;
			}
			else
			{
				AudioPlayableOutput audioPlayableOutput = new AudioPlayableOutput(handle);
				audioPlayableOutput.SetTarget(target);
				result = audioPlayableOutput;
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000036D0 File Offset: 0x000018D0
		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AudioPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000370C File Offset: 0x0000190C
		public static AudioPlayableOutput Null
		{
			get
			{
				return new AudioPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00003728 File Offset: 0x00001928
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00003740 File Offset: 0x00001940
		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00003760 File Offset: 0x00001960
		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00003780 File Offset: 0x00001980
		public AudioSource GetTarget()
		{
			return AudioPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000379D File Offset: 0x0000199D
		public void SetTarget(AudioSource value)
		{
			AudioPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000037B0 File Offset: 0x000019B0
		public bool GetEvaluateOnSeek()
		{
			return AudioPlayableOutput.InternalGetEvaluateOnSeek(ref this.m_Handle);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000037CD File Offset: 0x000019CD
		public void SetEvaluateOnSeek(bool value)
		{
			AudioPlayableOutput.InternalSetEvaluateOnSeek(ref this.m_Handle, value);
		}

		// Token: 0x060001CD RID: 461
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSource InternalGetTarget(ref PlayableOutputHandle output);

		// Token: 0x060001CE RID: 462
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target);

		// Token: 0x060001CF RID: 463
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEvaluateOnSeek(ref PlayableOutputHandle output);

		// Token: 0x060001D0 RID: 464
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);

		// Token: 0x0400006C RID: 108
		private PlayableOutputHandle m_Handle;
	}
}
