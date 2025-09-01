using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[NativeHeader("Modules/Director/PlayableDirector.h")]
	[RequiredByNativeCode]
	public class PlayableDirector : Behaviour, IExposedPropertyTable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PlayState state
		{
			get
			{
				return this.GetPlayState();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public DirectorWrapMode extrapolationMode
		{
			get
			{
				return this.GetWrapMode();
			}
			set
			{
				this.SetWrapMode(value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A9 File Offset: 0x000002A9
		public PlayableAsset playableAsset
		{
			get
			{
				return this.Internal_GetPlayableAsset() as PlayableAsset;
			}
			set
			{
				this.SetPlayableAsset(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000002B4
		public PlayableGraph playableGraph
		{
			get
			{
				return this.GetGraphHandle();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020E4 File Offset: 0x000002E4
		public bool playOnAwake
		{
			get
			{
				return this.GetPlayOnAwake();
			}
			set
			{
				this.SetPlayOnAwake(value);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020EF File Offset: 0x000002EF
		public void DeferredEvaluate()
		{
			this.EvaluateNextFrame();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F9 File Offset: 0x000002F9
		internal void Play(FrameRate frameRate)
		{
			this.PlayOnFrame(frameRate);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		public void Play(PlayableAsset asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.Play(asset, this.extrapolationMode);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002138 File Offset: 0x00000338
		public void Play(PlayableAsset asset, DirectorWrapMode mode)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.playableAsset = asset;
			this.extrapolationMode = mode;
			this.Play();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002173 File Offset: 0x00000373
		public void SetGenericBinding(Object key, Object value)
		{
			this.Internal_SetGenericBinding(key, value);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x0600000E RID: 14
		public extern DirectorUpdateMode timeUpdateMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000010 RID: 16
		public extern double time { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000012 RID: 18
		public extern double initialTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20
		public extern double duration { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000015 RID: 21
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Evaluate();

		// Token: 0x06000016 RID: 22 RVA: 0x0000217F File Offset: 0x0000037F
		[NativeThrows]
		private void PlayOnFrame(FrameRate frameRate)
		{
			this.PlayOnFrame_Injected(ref frameRate);
		}

		// Token: 0x06000017 RID: 23
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Resume();

		// Token: 0x0600001B RID: 27
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebuildGraph();

		// Token: 0x0600001C RID: 28 RVA: 0x00002189 File Offset: 0x00000389
		public void ClearReferenceValue(PropertyName id)
		{
			this.ClearReferenceValue_Injected(ref id);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002193 File Offset: 0x00000393
		public void SetReferenceValue(PropertyName id, Object value)
		{
			this.SetReferenceValue_Injected(ref id, value);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000219E File Offset: 0x0000039E
		public Object GetReferenceValue(PropertyName id, out bool idValid)
		{
			return this.GetReferenceValue_Injected(ref id, out idValid);
		}

		// Token: 0x0600001F RID: 31
		[NativeMethod("GetBindingFor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Object GetGenericBinding(Object key);

		// Token: 0x06000020 RID: 32
		[NativeMethod("ClearBindingFor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearGenericBinding(Object key);

		// Token: 0x06000021 RID: 33
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RebindPlayableGraphOutputs();

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ProcessPendingGraphChanges();

		// Token: 0x06000023 RID: 35
		[NativeMethod("HasBinding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool HasGenericBinding(Object key);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern PlayState GetPlayState();

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetWrapMode(DirectorWrapMode mode);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern DirectorWrapMode GetWrapMode();

		// Token: 0x06000027 RID: 39
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EvaluateNextFrame();

		// Token: 0x06000028 RID: 40 RVA: 0x000021AC File Offset: 0x000003AC
		private PlayableGraph GetGraphHandle()
		{
			PlayableGraph result;
			this.GetGraphHandle_Injected(out result);
			return result;
		}

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlayOnAwake(bool on);

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetPlayOnAwake();

		// Token: 0x0600002B RID: 43
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetGenericBinding(Object key, Object value);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlayableAsset(ScriptableObject asset);

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ScriptableObject Internal_GetPlayableAsset();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002E RID: 46 RVA: 0x000021C4 File Offset: 0x000003C4
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x000021FC File Offset: 0x000003FC
		public event Action<PlayableDirector> played
		{
			[CompilerGenerated]
			add
			{
				Action<PlayableDirector> action = this.played;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.played, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PlayableDirector> action = this.played;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.played, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000030 RID: 48 RVA: 0x00002234 File Offset: 0x00000434
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x0000226C File Offset: 0x0000046C
		public event Action<PlayableDirector> paused
		{
			[CompilerGenerated]
			add
			{
				Action<PlayableDirector> action = this.paused;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.paused, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PlayableDirector> action = this.paused;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.paused, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000032 RID: 50 RVA: 0x000022A4 File Offset: 0x000004A4
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x000022DC File Offset: 0x000004DC
		public event Action<PlayableDirector> stopped
		{
			[CompilerGenerated]
			add
			{
				Action<PlayableDirector> action = this.stopped;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.stopped, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PlayableDirector> action = this.stopped;
				Action<PlayableDirector> action2;
				do
				{
					action2 = action;
					Action<PlayableDirector> value2 = (Action<PlayableDirector>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PlayableDirector>>(ref this.stopped, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000034 RID: 52
		[NativeHeader("Runtime/Director/Core/DirectorManager.h")]
		[StaticAccessor("GetDirectorManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetFrameTiming();

		// Token: 0x06000035 RID: 53 RVA: 0x00002314 File Offset: 0x00000514
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPlay()
		{
			bool flag = this.played != null;
			if (flag)
			{
				this.played(this);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000233C File Offset: 0x0000053C
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPause()
		{
			bool flag = this.paused != null;
			if (flag)
			{
				this.paused(this);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002364 File Offset: 0x00000564
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorStop()
		{
			bool flag = this.stopped != null;
			if (flag)
			{
				this.stopped(this);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000238C File Offset: 0x0000058C
		public PlayableDirector()
		{
		}

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void PlayOnFrame_Injected(ref FrameRate frameRate);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearReferenceValue_Injected(ref PropertyName id);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetReferenceValue_Injected(ref PropertyName id, Object value);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Object GetReferenceValue_Injected(ref PropertyName id, out bool idValid);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGraphHandle_Injected(out PlayableGraph ret);

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<PlayableDirector> played;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<PlayableDirector> paused;

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<PlayableDirector> stopped;
	}
}
