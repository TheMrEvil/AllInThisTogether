using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000445 RID: 1093
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Director/PlayableGraph.bindings.h")]
	public struct PlayableGraph
	{
		// Token: 0x060025E8 RID: 9704 RVA: 0x0004004C File Offset: 0x0003E24C
		public Playable GetRootPlayable(int index)
		{
			PlayableHandle rootPlayableInternal = this.GetRootPlayableInternal(index);
			return new Playable(rootPlayableInternal);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0004006C File Offset: 0x0003E26C
		public bool Connect<U, V>(U source, int sourceOutputPort, V destination, int destinationInputPort) where U : struct, IPlayable where V : struct, IPlayable
		{
			return this.ConnectInternal(source.GetHandle(), sourceOutputPort, destination.GetHandle(), destinationInputPort);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000400A1 File Offset: 0x0003E2A1
		public void Disconnect<U>(U input, int inputPort) where U : struct, IPlayable
		{
			this.DisconnectInternal(input.GetHandle(), inputPort);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000400B9 File Offset: 0x0003E2B9
		public void DestroyPlayable<U>(U playable) where U : struct, IPlayable
		{
			this.DestroyPlayableInternal(playable.GetHandle());
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000400D0 File Offset: 0x0003E2D0
		public void DestroySubgraph<U>(U playable) where U : struct, IPlayable
		{
			this.DestroySubgraphInternal(playable.GetHandle());
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000400E7 File Offset: 0x0003E2E7
		public void DestroyOutput<U>(U output) where U : struct, IPlayableOutput
		{
			this.DestroyOutputInternal(output.GetHandle());
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x00040100 File Offset: 0x0003E300
		public int GetOutputCountByType<T>() where T : struct, IPlayableOutput
		{
			return this.GetOutputCountByTypeInternal(typeof(T));
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00040124 File Offset: 0x0003E324
		public PlayableOutput GetOutput(int index)
		{
			PlayableOutputHandle handle;
			bool flag = !this.GetOutputInternal(index, out handle);
			PlayableOutput result;
			if (flag)
			{
				result = PlayableOutput.Null;
			}
			else
			{
				result = new PlayableOutput(handle);
			}
			return result;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00040154 File Offset: 0x0003E354
		public PlayableOutput GetOutputByType<T>(int index) where T : struct, IPlayableOutput
		{
			PlayableOutputHandle handle;
			bool flag = !this.GetOutputByTypeInternal(typeof(T), index, out handle);
			PlayableOutput result;
			if (flag)
			{
				result = PlayableOutput.Null;
			}
			else
			{
				result = new PlayableOutput(handle);
			}
			return result;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0004018E File Offset: 0x0003E38E
		public void Evaluate()
		{
			this.Evaluate(0f);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000401A0 File Offset: 0x0003E3A0
		public static PlayableGraph Create()
		{
			return PlayableGraph.Create(null);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000401B8 File Offset: 0x0003E3B8
		public static PlayableGraph Create(string name)
		{
			PlayableGraph result;
			PlayableGraph.Create_Injected(name, out result);
			return result;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000401CE File Offset: 0x0003E3CE
		[FreeFunction("PlayableGraphBindings::Destroy", HasExplicitThis = true, ThrowsException = true)]
		public void Destroy()
		{
			PlayableGraph.Destroy_Injected(ref this);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000401D6 File Offset: 0x0003E3D6
		public bool IsValid()
		{
			return PlayableGraph.IsValid_Injected(ref this);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000401DE File Offset: 0x0003E3DE
		[FreeFunction("PlayableGraphBindings::IsPlaying", HasExplicitThis = true, ThrowsException = true)]
		public bool IsPlaying()
		{
			return PlayableGraph.IsPlaying_Injected(ref this);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000401E6 File Offset: 0x0003E3E6
		[FreeFunction("PlayableGraphBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		public bool IsDone()
		{
			return PlayableGraph.IsDone_Injected(ref this);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000401EE File Offset: 0x0003E3EE
		[FreeFunction("PlayableGraphBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		public void Play()
		{
			PlayableGraph.Play_Injected(ref this);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000401F6 File Offset: 0x0003E3F6
		[FreeFunction("PlayableGraphBindings::Stop", HasExplicitThis = true, ThrowsException = true)]
		public void Stop()
		{
			PlayableGraph.Stop_Injected(ref this);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000401FE File Offset: 0x0003E3FE
		[FreeFunction("PlayableGraphBindings::Evaluate", HasExplicitThis = true, ThrowsException = true)]
		public void Evaluate([DefaultValue("0")] float deltaTime)
		{
			PlayableGraph.Evaluate_Injected(ref this, deltaTime);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00040207 File Offset: 0x0003E407
		[FreeFunction("PlayableGraphBindings::GetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public DirectorUpdateMode GetTimeUpdateMode()
		{
			return PlayableGraph.GetTimeUpdateMode_Injected(ref this);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0004020F File Offset: 0x0003E40F
		[FreeFunction("PlayableGraphBindings::SetTimeUpdateMode", HasExplicitThis = true, ThrowsException = true)]
		public void SetTimeUpdateMode(DirectorUpdateMode value)
		{
			PlayableGraph.SetTimeUpdateMode_Injected(ref this, value);
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00040218 File Offset: 0x0003E418
		[FreeFunction("PlayableGraphBindings::GetResolver", HasExplicitThis = true, ThrowsException = true)]
		public IExposedPropertyTable GetResolver()
		{
			return PlayableGraph.GetResolver_Injected(ref this);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00040220 File Offset: 0x0003E420
		[FreeFunction("PlayableGraphBindings::SetResolver", HasExplicitThis = true, ThrowsException = true)]
		public void SetResolver(IExposedPropertyTable value)
		{
			PlayableGraph.SetResolver_Injected(ref this, value);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00040229 File Offset: 0x0003E429
		[FreeFunction("PlayableGraphBindings::GetPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetPlayableCount()
		{
			return PlayableGraph.GetPlayableCount_Injected(ref this);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00040231 File Offset: 0x0003E431
		[FreeFunction("PlayableGraphBindings::GetRootPlayableCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetRootPlayableCount()
		{
			return PlayableGraph.GetRootPlayableCount_Injected(ref this);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00040239 File Offset: 0x0003E439
		[FreeFunction("PlayableGraphBindings::SynchronizeEvaluation", HasExplicitThis = true, ThrowsException = true)]
		internal void SynchronizeEvaluation(PlayableGraph playable)
		{
			PlayableGraph.SynchronizeEvaluation_Injected(ref this, ref playable);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00040243 File Offset: 0x0003E443
		[FreeFunction("PlayableGraphBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetOutputCount()
		{
			return PlayableGraph.GetOutputCount_Injected(ref this);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0004024C File Offset: 0x0003E44C
		[FreeFunction("PlayableGraphBindings::CreatePlayableHandle", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle CreatePlayableHandle()
		{
			PlayableHandle result;
			PlayableGraph.CreatePlayableHandle_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00040262 File Offset: 0x0003E462
		[FreeFunction("PlayableGraphBindings::CreateScriptOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal bool CreateScriptOutputInternal(string name, out PlayableOutputHandle handle)
		{
			return PlayableGraph.CreateScriptOutputInternal_Injected(ref this, name, out handle);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0004026C File Offset: 0x0003E46C
		[FreeFunction("PlayableGraphBindings::GetRootPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetRootPlayableInternal(int index)
		{
			PlayableHandle result;
			PlayableGraph.GetRootPlayableInternal_Injected(ref this, index, out result);
			return result;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00040283 File Offset: 0x0003E483
		[FreeFunction("PlayableGraphBindings::DestroyOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		internal void DestroyOutputInternal(PlayableOutputHandle handle)
		{
			PlayableGraph.DestroyOutputInternal_Injected(ref this, ref handle);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0004028D File Offset: 0x0003E48D
		[FreeFunction("PlayableGraphBindings::IsMatchFrameRateEnabled", HasExplicitThis = true, ThrowsException = true)]
		internal bool IsMatchFrameRateEnabled()
		{
			return PlayableGraph.IsMatchFrameRateEnabled_Injected(ref this);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00040295 File Offset: 0x0003E495
		[FreeFunction("PlayableGraphBindings::EnableMatchFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal void EnableMatchFrameRate(FrameRate frameRate)
		{
			PlayableGraph.EnableMatchFrameRate_Injected(ref this, ref frameRate);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0004029F File Offset: 0x0003E49F
		[FreeFunction("PlayableGraphBindings::DisableMatchFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal void DisableMatchFrameRate()
		{
			PlayableGraph.DisableMatchFrameRate_Injected(ref this);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000402A8 File Offset: 0x0003E4A8
		[FreeFunction("PlayableGraphBindings::GetFrameRate", HasExplicitThis = true, ThrowsException = true)]
		internal FrameRate GetFrameRate()
		{
			FrameRate result;
			PlayableGraph.GetFrameRate_Injected(ref this, out result);
			return result;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000402BE File Offset: 0x0003E4BE
		[FreeFunction("PlayableGraphBindings::GetOutputInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputInternal(int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputInternal_Injected(ref this, index, out handle);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000402C8 File Offset: 0x0003E4C8
		[FreeFunction("PlayableGraphBindings::GetOutputCountByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private int GetOutputCountByTypeInternal(Type outputType)
		{
			return PlayableGraph.GetOutputCountByTypeInternal_Injected(ref this, outputType);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000402D1 File Offset: 0x0003E4D1
		[FreeFunction("PlayableGraphBindings::GetOutputByTypeInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool GetOutputByTypeInternal(Type outputType, int index, out PlayableOutputHandle handle)
		{
			return PlayableGraph.GetOutputByTypeInternal_Injected(ref this, outputType, index, out handle);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000402DC File Offset: 0x0003E4DC
		[FreeFunction("PlayableGraphBindings::ConnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private bool ConnectInternal(PlayableHandle source, int sourceOutputPort, PlayableHandle destination, int destinationInputPort)
		{
			return PlayableGraph.ConnectInternal_Injected(ref this, ref source, sourceOutputPort, ref destination, destinationInputPort);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000402EB File Offset: 0x0003E4EB
		[FreeFunction("PlayableGraphBindings::DisconnectInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DisconnectInternal(PlayableHandle playable, int inputPort)
		{
			PlayableGraph.DisconnectInternal_Injected(ref this, ref playable, inputPort);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000402F6 File Offset: 0x0003E4F6
		[FreeFunction("PlayableGraphBindings::DestroyPlayableInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroyPlayableInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroyPlayableInternal_Injected(ref this, ref playable);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00040300 File Offset: 0x0003E500
		[FreeFunction("PlayableGraphBindings::DestroySubgraphInternal", HasExplicitThis = true, ThrowsException = true)]
		private void DestroySubgraphInternal(PlayableHandle playable)
		{
			PlayableGraph.DestroySubgraphInternal_Injected(ref this, ref playable);
		}

		// Token: 0x06002612 RID: 9746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected(string name, out PlayableGraph ret);

		// Token: 0x06002613 RID: 9747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002614 RID: 9748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002615 RID: 9749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPlaying_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002616 RID: 9750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002617 RID: 9751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002618 RID: 9752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002619 RID: 9753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Evaluate_Injected(ref PlayableGraph _unity_self, [DefaultValue("0")] float deltaTime);

		// Token: 0x0600261A RID: 9754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorUpdateMode GetTimeUpdateMode_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600261B RID: 9755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTimeUpdateMode_Injected(ref PlayableGraph _unity_self, DirectorUpdateMode value);

		// Token: 0x0600261C RID: 9756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IExposedPropertyTable GetResolver_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600261D RID: 9757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetResolver_Injected(ref PlayableGraph _unity_self, IExposedPropertyTable value);

		// Token: 0x0600261E RID: 9758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x0600261F RID: 9759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRootPlayableCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002620 RID: 9760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SynchronizeEvaluation_Injected(ref PlayableGraph _unity_self, ref PlayableGraph playable);

		// Token: 0x06002621 RID: 9761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOutputCount_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002622 RID: 9762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreatePlayableHandle_Injected(ref PlayableGraph _unity_self, out PlayableHandle ret);

		// Token: 0x06002623 RID: 9763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateScriptOutputInternal_Injected(ref PlayableGraph _unity_self, string name, out PlayableOutputHandle handle);

		// Token: 0x06002624 RID: 9764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootPlayableInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06002625 RID: 9765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyOutputInternal_Injected(ref PlayableGraph _unity_self, ref PlayableOutputHandle handle);

		// Token: 0x06002626 RID: 9766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMatchFrameRateEnabled_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002627 RID: 9767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableMatchFrameRate_Injected(ref PlayableGraph _unity_self, ref FrameRate frameRate);

		// Token: 0x06002628 RID: 9768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableMatchFrameRate_Injected(ref PlayableGraph _unity_self);

		// Token: 0x06002629 RID: 9769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFrameRate_Injected(ref PlayableGraph _unity_self, out FrameRate ret);

		// Token: 0x0600262A RID: 9770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetOutputInternal_Injected(ref PlayableGraph _unity_self, int index, out PlayableOutputHandle handle);

		// Token: 0x0600262B RID: 9771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOutputCountByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType);

		// Token: 0x0600262C RID: 9772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetOutputByTypeInternal_Injected(ref PlayableGraph _unity_self, Type outputType, int index, out PlayableOutputHandle handle);

		// Token: 0x0600262D RID: 9773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle source, int sourceOutputPort, ref PlayableHandle destination, int destinationInputPort);

		// Token: 0x0600262E RID: 9774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisconnectInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable, int inputPort);

		// Token: 0x0600262F RID: 9775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyPlayableInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x06002630 RID: 9776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroySubgraphInternal_Injected(ref PlayableGraph _unity_self, ref PlayableHandle playable);

		// Token: 0x04000E25 RID: 3621
		internal IntPtr m_Handle;

		// Token: 0x04000E26 RID: 3622
		internal uint m_Version;
	}
}
