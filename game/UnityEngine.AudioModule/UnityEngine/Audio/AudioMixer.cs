using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Audio/Public/AudioMixer.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h")]
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	public class AudioMixer : Object
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00003481 File Offset: 0x00001681
		internal AudioMixer()
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A7 RID: 423
		// (set) Token: 0x060001A8 RID: 424
		[NativeProperty]
		public extern AudioMixerGroup outputAudioMixerGroup { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060001A9 RID: 425
		[NativeMethod("FindSnapshotFromName")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerSnapshot FindSnapshot(string name);

		// Token: 0x060001AA RID: 426
		[NativeMethod("AudioMixerBindings::FindMatchingGroups", IsFreeFunction = true, HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerGroup[] FindMatchingGroups(string subPath);

		// Token: 0x060001AB RID: 427 RVA: 0x0000348C File Offset: 0x0000168C
		internal void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			bool flag = snapshot == null;
			if (flag)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			bool flag2 = snapshot.audioMixer != this;
			if (flag2)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Snapshot '",
					snapshot.name,
					"' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '",
					base.name,
					"'"
				}));
			}
			this.TransitionToSnapshotInternal(snapshot, timeToReach);
		}

		// Token: 0x060001AC RID: 428
		[NativeMethod("TransitionToSnapshot")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TransitionToSnapshotInternal(AudioMixerSnapshot snapshot, float timeToReach);

		// Token: 0x060001AD RID: 429
		[NativeMethod("AudioMixerBindings::TransitionToSnapshots", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach);

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AE RID: 430
		// (set) Token: 0x060001AF RID: 431
		[NativeProperty]
		public extern AudioMixerUpdateMode updateMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060001B0 RID: 432
		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetFloat(string name, float value);

		// Token: 0x060001B1 RID: 433
		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool ClearFloat(string name);

		// Token: 0x060001B2 RID: 434
		[NativeMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetFloat(string name, out float value);
	}
}
