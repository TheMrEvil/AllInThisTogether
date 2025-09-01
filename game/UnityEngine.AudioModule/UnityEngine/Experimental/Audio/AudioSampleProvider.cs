using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x0200002D RID: 45
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h")]
	[StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	public class AudioSampleProvider : IDisposable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x000037E0 File Offset: 0x000019E0
		[VisibleToOtherModules]
		internal static AudioSampleProvider Lookup(uint providerId, Object ownerObj, ushort trackIndex)
		{
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.InternalGetScriptingPtr(providerId);
			bool flag = audioSampleProvider != null || !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider result;
			if (flag)
			{
				result = audioSampleProvider;
			}
			else
			{
				result = new AudioSampleProvider(providerId, ownerObj, trackIndex);
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00003818 File Offset: 0x00001A18
		internal static AudioSampleProvider Create(ushort channelCount, uint sampleRate)
		{
			uint providerId = AudioSampleProvider.InternalCreateSampleProvider(channelCount, sampleRate);
			bool flag = !AudioSampleProvider.InternalIsValid(providerId);
			AudioSampleProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AudioSampleProvider(providerId, null, 0);
			}
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000384C File Offset: 0x00001A4C
		private AudioSampleProvider(uint providerId, Object ownerObj, ushort trackIdx)
		{
			this.owner = ownerObj;
			this.id = providerId;
			this.trackIndex = trackIdx;
			this.m_ConsumeSampleFramesNativeFunction = (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			ushort channelCount = 0;
			uint sampleRate = 0U;
			AudioSampleProvider.InternalGetFormatInfo(providerId, out channelCount, out sampleRate);
			this.channelCount = channelCount;
			this.sampleRate = sampleRate;
			AudioSampleProvider.InternalSetScriptingPtr(providerId, this);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000038C0 File Offset: 0x00001AC0
		~AudioSampleProvider()
		{
			this.owner = null;
			this.Dispose();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000038F8 File Offset: 0x00001AF8
		public void Dispose()
		{
			bool flag = this.id > 0U;
			if (flag)
			{
				AudioSampleProvider.InternalSetScriptingPtr(this.id, null);
				bool flag2 = this.owner == null;
				if (flag2)
				{
					AudioSampleProvider.InternalRemove(this.id);
				}
				this.id = 0U;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000394D File Offset: 0x00001B4D
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00003955 File Offset: 0x00001B55
		public uint id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000395E File Offset: 0x00001B5E
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00003966 File Offset: 0x00001B66
		public ushort trackIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<trackIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<trackIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000396F File Offset: 0x00001B6F
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00003977 File Offset: 0x00001B77
		public Object owner
		{
			[CompilerGenerated]
			get
			{
				return this.<owner>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<owner>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003980 File Offset: 0x00001B80
		public bool valid
		{
			get
			{
				return AudioSampleProvider.InternalIsValid(this.id);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000399D File Offset: 0x00001B9D
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000039A5 File Offset: 0x00001BA5
		public ushort channelCount
		{
			[CompilerGenerated]
			get
			{
				return this.<channelCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<channelCount>k__BackingField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000039AE File Offset: 0x00001BAE
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000039B6 File Offset: 0x00001BB6
		public uint sampleRate
		{
			[CompilerGenerated]
			get
			{
				return this.<sampleRate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sampleRate>k__BackingField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000039C0 File Offset: 0x00001BC0
		public uint maxSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetMaxSampleFrameCount(this.id);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000039E0 File Offset: 0x00001BE0
		public uint availableSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetAvailableSampleFrameCount(this.id);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00003A00 File Offset: 0x00001C00
		public uint freeSampleFrameCount
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCount(this.id);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00003A3D File Offset: 0x00001C3D
		public uint freeSampleFrameCountLowThreshold
		{
			get
			{
				return AudioSampleProvider.InternalGetFreeSampleFrameCountLowThreshold(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetFreeSampleFrameCountLowThreshold(this.id, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00003A50 File Offset: 0x00001C50
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00003A6D File Offset: 0x00001C6D
		public bool enableSampleFramesAvailableEvents
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSampleFramesAvailableEvents(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSampleFramesAvailableEvents(this.id, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00003A80 File Offset: 0x00001C80
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00003A9D File Offset: 0x00001C9D
		public bool enableSilencePadding
		{
			get
			{
				return AudioSampleProvider.InternalGetEnableSilencePadding(this.id);
			}
			set
			{
				AudioSampleProvider.InternalSetEnableSilencePadding(this.id, value);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public uint ConsumeSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				result = this.m_ConsumeSampleFramesNativeFunction(this.id, (IntPtr)sampleFrames.GetUnsafePtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return result;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00003B00 File Offset: 0x00001D00
		public static AudioSampleProvider.ConsumeSampleFramesNativeFunction consumeSampleFramesNativeFunction
		{
			get
			{
				return (AudioSampleProvider.ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(AudioSampleProvider.InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(AudioSampleProvider.ConsumeSampleFramesNativeFunction));
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00003B2C File Offset: 0x00001D2C
		internal uint QueueSampleFrames(NativeArray<float> sampleFrames)
		{
			bool flag = this.channelCount == 0;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				result = AudioSampleProvider.InternalQueueSampleFrames(this.id, (IntPtr)sampleFrames.GetUnsafeReadOnlyPtr<float>(), (uint)(sampleFrames.Length / (int)this.channelCount));
			}
			return result;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001EE RID: 494 RVA: 0x00003B74 File Offset: 0x00001D74
		// (remove) Token: 0x060001EF RID: 495 RVA: 0x00003BAC File Offset: 0x00001DAC
		public event AudioSampleProvider.SampleFramesHandler sampleFramesAvailable
		{
			[CompilerGenerated]
			add
			{
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler = this.sampleFramesAvailable;
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler2;
				do
				{
					sampleFramesHandler2 = sampleFramesHandler;
					AudioSampleProvider.SampleFramesHandler value2 = (AudioSampleProvider.SampleFramesHandler)Delegate.Combine(sampleFramesHandler2, value);
					sampleFramesHandler = Interlocked.CompareExchange<AudioSampleProvider.SampleFramesHandler>(ref this.sampleFramesAvailable, value2, sampleFramesHandler2);
				}
				while (sampleFramesHandler != sampleFramesHandler2);
			}
			[CompilerGenerated]
			remove
			{
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler = this.sampleFramesAvailable;
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler2;
				do
				{
					sampleFramesHandler2 = sampleFramesHandler;
					AudioSampleProvider.SampleFramesHandler value2 = (AudioSampleProvider.SampleFramesHandler)Delegate.Remove(sampleFramesHandler2, value);
					sampleFramesHandler = Interlocked.CompareExchange<AudioSampleProvider.SampleFramesHandler>(ref this.sampleFramesAvailable, value2, sampleFramesHandler2);
				}
				while (sampleFramesHandler != sampleFramesHandler2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001F0 RID: 496 RVA: 0x00003BE4 File Offset: 0x00001DE4
		// (remove) Token: 0x060001F1 RID: 497 RVA: 0x00003C1C File Offset: 0x00001E1C
		public event AudioSampleProvider.SampleFramesHandler sampleFramesOverflow
		{
			[CompilerGenerated]
			add
			{
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler = this.sampleFramesOverflow;
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler2;
				do
				{
					sampleFramesHandler2 = sampleFramesHandler;
					AudioSampleProvider.SampleFramesHandler value2 = (AudioSampleProvider.SampleFramesHandler)Delegate.Combine(sampleFramesHandler2, value);
					sampleFramesHandler = Interlocked.CompareExchange<AudioSampleProvider.SampleFramesHandler>(ref this.sampleFramesOverflow, value2, sampleFramesHandler2);
				}
				while (sampleFramesHandler != sampleFramesHandler2);
			}
			[CompilerGenerated]
			remove
			{
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler = this.sampleFramesOverflow;
				AudioSampleProvider.SampleFramesHandler sampleFramesHandler2;
				do
				{
					sampleFramesHandler2 = sampleFramesHandler;
					AudioSampleProvider.SampleFramesHandler value2 = (AudioSampleProvider.SampleFramesHandler)Delegate.Remove(sampleFramesHandler2, value);
					sampleFramesHandler = Interlocked.CompareExchange<AudioSampleProvider.SampleFramesHandler>(ref this.sampleFramesOverflow, value2, sampleFramesHandler2);
				}
				while (sampleFramesHandler != sampleFramesHandler2);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00003C51 File Offset: 0x00001E51
		public void SetSampleFramesAvailableNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesAvailableNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00003C67 File Offset: 0x00001E67
		public void ClearSampleFramesAvailableNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesAvailableNativeHandler(this.id);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00003C76 File Offset: 0x00001E76
		public void SetSampleFramesOverflowNativeHandler(AudioSampleProvider.SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			AudioSampleProvider.InternalSetSampleFramesOverflowNativeHandler(this.id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00003C8C File Offset: 0x00001E8C
		public void ClearSampleFramesOverflowNativeHandler()
		{
			AudioSampleProvider.InternalClearSampleFramesOverflowNativeHandler(this.id);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00003C9C File Offset: 0x00001E9C
		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			bool flag = this.sampleFramesAvailable != null;
			if (flag)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00003CC8 File Offset: 0x00001EC8
		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			bool flag = this.sampleFramesOverflow != null;
			if (flag)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		// Token: 0x060001F8 RID: 504
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalCreateSampleProvider(ushort channelCount, uint sampleRate);

		// Token: 0x060001F9 RID: 505
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRemove(uint providerId);

		// Token: 0x060001FA RID: 506
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetFormatInfo(uint providerId, out ushort chCount, out uint sRate);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSampleProvider InternalGetScriptingPtr(uint providerId);

		// Token: 0x060001FC RID: 508
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetScriptingPtr(uint providerId, AudioSampleProvider provider);

		// Token: 0x060001FD RID: 509
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalIsValid(uint providerId);

		// Token: 0x060001FE RID: 510
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetMaxSampleFrameCount(uint providerId);

		// Token: 0x060001FF RID: 511
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetAvailableSampleFrameCount(uint providerId);

		// Token: 0x06000200 RID: 512
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCount(uint providerId);

		// Token: 0x06000201 RID: 513
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalGetFreeSampleFrameCountLowThreshold(uint providerId);

		// Token: 0x06000202 RID: 514
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetFreeSampleFrameCountLowThreshold(uint providerId, uint sampleFrameCount);

		// Token: 0x06000203 RID: 515
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSampleFramesAvailableEvents(uint providerId);

		// Token: 0x06000204 RID: 516
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSampleFramesAvailableEvents(uint providerId, bool enable);

		// Token: 0x06000205 RID: 517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesAvailableNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x06000206 RID: 518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesAvailableNativeHandler(uint providerId);

		// Token: 0x06000207 RID: 519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesOverflowNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		// Token: 0x06000208 RID: 520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesOverflowNativeHandler(uint providerId);

		// Token: 0x06000209 RID: 521
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalGetEnableSilencePadding(uint id);

		// Token: 0x0600020A RID: 522
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEnableSilencePadding(uint id, bool enabled);

		// Token: 0x0600020B RID: 523
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetConsumeSampleFramesNativeFunctionPtr();

		// Token: 0x0600020C RID: 524
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint InternalQueueSampleFrames(uint id, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x0400006D RID: 109
		private AudioSampleProvider.ConsumeSampleFramesNativeFunction m_ConsumeSampleFramesNativeFunction;

		// Token: 0x0400006E RID: 110
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private uint <id>k__BackingField;

		// Token: 0x0400006F RID: 111
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private ushort <trackIndex>k__BackingField;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Object <owner>k__BackingField;

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ushort <channelCount>k__BackingField;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <sampleRate>k__BackingField;

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AudioSampleProvider.SampleFramesHandler sampleFramesAvailable;

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AudioSampleProvider.SampleFramesHandler sampleFramesOverflow;

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x0600020E RID: 526
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ConsumeSampleFramesNativeFunction(uint providerId, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x06000212 RID: 530
		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x06000216 RID: 534
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SampleFramesEventNativeFunction(IntPtr userData, uint providerId, uint sampleFrameCount);
	}
}
