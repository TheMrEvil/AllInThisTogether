using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000447 RID: 1095
	[NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		// Token: 0x06002631 RID: 9777 RVA: 0x0004030C File Offset: 0x0003E50C
		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					result = (T)((object)scriptInstance);
				}
			}
			return result;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0004035C File Offset: 0x0003E55C
		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00040380 File Offset: 0x0003E580
		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00040398 File Offset: 0x0003E598
		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000403B8 File Offset: 0x0003E5B8
		internal Playable GetOutput(int outputPort)
		{
			return new Playable(this.GetOutputHandle(outputPort));
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000403D8 File Offset: 0x0003E5D8
		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool result;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00040404 File Offset: 0x0003E604
		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float result;
			if (flag)
			{
				result = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00040434 File Offset: 0x0003E634
		internal void Destroy()
		{
			this.GetGraph().DestroyPlayable<Playable>(new Playable(this));
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0004045C File Offset: 0x0003E65C
		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00040478 File Offset: 0x0003E678
		public static bool operator !=(PlayableHandle x, PlayableHandle y)
		{
			return !PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00040494 File Offset: 0x0003E694
		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000404C0 File Offset: 0x0003E6C0
		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000404E0 File Offset: 0x0003E6E0
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0004050C File Offset: 0x0003E70C
		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00040544 File Offset: 0x0003E744
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00040560 File Offset: 0x0003E760
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 && acceptAny;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = inputIndex < 0;
				if (flag2)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag3 = this.GetInputCount() <= inputIndex;
				if (flag3)
				{
					throw new IndexOutOfRangeException(string.Concat(new string[]
					{
						"inputIndex ",
						inputIndex.ToString(),
						" is greater than the number of available inputs (",
						this.GetInputCount().ToString(),
						")."
					}));
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000405EB File Offset: 0x0003E7EB
		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableHandle.IsNull_Injected(ref this);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000405F3 File Offset: 0x0003E7F3
		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableHandle.IsValid_Injected(ref this);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000405FB File Offset: 0x0003E7FB
		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal Type GetPlayableType()
		{
			return PlayableHandle.GetPlayableType_Injected(ref this);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00040603 File Offset: 0x0003E803
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetJobType()
		{
			return PlayableHandle.GetJobType_Injected(ref this);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0004060B File Offset: 0x0003E80B
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		internal void SetScriptInstance(object scriptInstance)
		{
			PlayableHandle.SetScriptInstance_Injected(ref this, scriptInstance);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00040614 File Offset: 0x0003E814
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::CanChangeInputs", HasExplicitThis = true, ThrowsException = true)]
		internal bool CanChangeInputs()
		{
			return PlayableHandle.CanChangeInputs_Injected(ref this);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0004061C File Offset: 0x0003E81C
		[FreeFunction("PlayableHandleBindings::CanSetWeights", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanSetWeights()
		{
			return PlayableHandle.CanSetWeights_Injected(ref this);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00040624 File Offset: 0x0003E824
		[FreeFunction("PlayableHandleBindings::CanDestroy", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanDestroy()
		{
			return PlayableHandle.CanDestroy_Injected(ref this);
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0004062C File Offset: 0x0003E82C
		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal PlayState GetPlayState()
		{
			return PlayableHandle.GetPlayState_Injected(ref this);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00040634 File Offset: 0x0003E834
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		internal void Play()
		{
			PlayableHandle.Play_Injected(ref this);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0004063C File Offset: 0x0003E83C
		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void Pause()
		{
			PlayableHandle.Pause_Injected(ref this);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00040644 File Offset: 0x0003E844
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true)]
		internal double GetSpeed()
		{
			return PlayableHandle.GetSpeed_Injected(ref this);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0004064C File Offset: 0x0003E84C
		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetSpeed(double value)
		{
			PlayableHandle.SetSpeed_Injected(ref this, value);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00040655 File Offset: 0x0003E855
		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal double GetTime()
		{
			return PlayableHandle.GetTime_Injected(ref this);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0004065D File Offset: 0x0003E85D
		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetTime(double value)
		{
			PlayableHandle.SetTime_Injected(ref this, value);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00040666 File Offset: 0x0003E866
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		internal bool IsDone()
		{
			return PlayableHandle.IsDone_Injected(ref this);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0004066E File Offset: 0x0003E86E
		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetDone(bool value)
		{
			PlayableHandle.SetDone_Injected(ref this, value);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00040677 File Offset: 0x0003E877
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true)]
		internal double GetDuration()
		{
			return PlayableHandle.GetDuration_Injected(ref this);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0004067F File Offset: 0x0003E87F
		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetDuration(double value)
		{
			PlayableHandle.SetDuration_Injected(ref this, value);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00040688 File Offset: 0x0003E888
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		internal bool GetPropagateSetTime()
		{
			return PlayableHandle.GetPropagateSetTime_Injected(ref this);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00040690 File Offset: 0x0003E890
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		internal void SetPropagateSetTime(bool value)
		{
			PlayableHandle.SetPropagateSetTime_Injected(ref this, value);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0004069C File Offset: 0x0003E89C
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph result;
			PlayableHandle.GetGraph_Injected(ref this, out result);
			return result;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000406B2 File Offset: 0x0003E8B2
		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal int GetInputCount()
		{
			return PlayableHandle.GetInputCount_Injected(ref this);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000406BA File Offset: 0x0003E8BA
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true)]
		internal void SetInputCount(int value)
		{
			PlayableHandle.SetInputCount_Injected(ref this, value);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000406C3 File Offset: 0x0003E8C3
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		internal int GetOutputCount()
		{
			return PlayableHandle.GetOutputCount_Injected(ref this);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000406CB File Offset: 0x0003E8CB
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		internal void SetOutputCount(int value)
		{
			PlayableHandle.SetOutputCount_Injected(ref this, value);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000406D4 File Offset: 0x0003E8D4
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000406DF File Offset: 0x0003E8DF
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDelay", HasExplicitThis = true, ThrowsException = true)]
		internal void SetDelay(double delay)
		{
			PlayableHandle.SetDelay_Injected(ref this, delay);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000406E8 File Offset: 0x0003E8E8
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDelay", HasExplicitThis = true, ThrowsException = true)]
		internal double GetDelay()
		{
			return PlayableHandle.GetDelay_Injected(ref this);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000406F0 File Offset: 0x0003E8F0
		[FreeFunction("PlayableHandleBindings::IsDelayed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool IsDelayed()
		{
			return PlayableHandle.IsDelayed_Injected(ref this);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000406F8 File Offset: 0x0003E8F8
		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal double GetPreviousTime()
		{
			return PlayableHandle.GetPreviousTime_Injected(ref this);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00040700 File Offset: 0x0003E900
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		internal void SetLeadTime(float value)
		{
			PlayableHandle.SetLeadTime_Injected(ref this, value);
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00040709 File Offset: 0x0003E909
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		internal float GetLeadTime()
		{
			return PlayableHandle.GetLeadTime_Injected(ref this);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00040711 File Offset: 0x0003E911
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableTraversalMode GetTraversalMode()
		{
			return PlayableHandle.GetTraversalMode_Injected(ref this);
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00040719 File Offset: 0x0003E919
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTraversalMode(PlayableTraversalMode mode)
		{
			PlayableHandle.SetTraversalMode_Injected(ref this, mode);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00040722 File Offset: 0x0003E922
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobData", HasExplicitThis = true, ThrowsException = true)]
		internal IntPtr GetJobData()
		{
			return PlayableHandle.GetJobData_Injected(ref this);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0004072A File Offset: 0x0003E92A
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		internal DirectorWrapMode GetTimeWrapMode()
		{
			return PlayableHandle.GetTimeWrapMode_Injected(ref this);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00040732 File Offset: 0x0003E932
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTimeWrapMode(DirectorWrapMode mode)
		{
			PlayableHandle.SetTimeWrapMode_Injected(ref this, mode);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0004073B File Offset: 0x0003E93B
		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		private object GetScriptInstance()
		{
			return PlayableHandle.GetScriptInstance_Injected(ref this);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00040744 File Offset: 0x0003E944
		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle result;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out result);
			return result;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0004075C File Offset: 0x0003E95C
		[FreeFunction("PlayableHandleBindings::GetOutputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetOutputHandle(int index)
		{
			PlayableHandle result;
			PlayableHandle.GetOutputHandle_Injected(ref this, index, out result);
			return result;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00040773 File Offset: 0x0003E973
		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private void SetInputWeightFromIndex(int index, float weight)
		{
			PlayableHandle.SetInputWeightFromIndex_Injected(ref this, index, weight);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0004077D File Offset: 0x0003E97D
		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private float GetInputWeightFromIndex(int index)
		{
			return PlayableHandle.GetInputWeightFromIndex_Injected(ref this, index);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00040786 File Offset: 0x0003E986
		// Note: this type is marked as 'beforefieldinit'.
		static PlayableHandle()
		{
		}

		// Token: 0x0600266D RID: 9837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNull_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600266E RID: 9838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600266F RID: 9839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetPlayableType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002670 RID: 9840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetJobType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002671 RID: 9841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScriptInstance_Injected(ref PlayableHandle _unity_self, object scriptInstance);

		// Token: 0x06002672 RID: 9842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanChangeInputs_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002673 RID: 9843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanSetWeights_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002674 RID: 9844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanDestroy_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002675 RID: 9845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayState GetPlayState_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002676 RID: 9846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002677 RID: 9847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002678 RID: 9848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetSpeed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002679 RID: 9849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpeed_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x0600267A RID: 9850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600267B RID: 9851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTime_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x0600267C RID: 9852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600267D RID: 9853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDone_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x0600267E RID: 9854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetDuration_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600267F RID: 9855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDuration_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x06002680 RID: 9856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPropagateSetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002681 RID: 9857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPropagateSetTime_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x06002682 RID: 9858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		// Token: 0x06002683 RID: 9859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002684 RID: 9860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x06002685 RID: 9861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetOutputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002686 RID: 9862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOutputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x06002687 RID: 9863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, ref PlayableHandle input, float weight);

		// Token: 0x06002688 RID: 9864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDelay_Injected(ref PlayableHandle _unity_self, double delay);

		// Token: 0x06002689 RID: 9865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetDelay_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600268A RID: 9866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDelayed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600268B RID: 9867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetPreviousTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600268C RID: 9868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLeadTime_Injected(ref PlayableHandle _unity_self, float value);

		// Token: 0x0600268D RID: 9869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLeadTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600268E RID: 9870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayableTraversalMode GetTraversalMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x0600268F RID: 9871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTraversalMode_Injected(ref PlayableHandle _unity_self, PlayableTraversalMode mode);

		// Token: 0x06002690 RID: 9872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetJobData_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002691 RID: 9873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorWrapMode GetTimeWrapMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002692 RID: 9874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTimeWrapMode_Injected(ref PlayableHandle _unity_self, DirectorWrapMode mode);

		// Token: 0x06002693 RID: 9875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetScriptInstance_Injected(ref PlayableHandle _unity_self);

		// Token: 0x06002694 RID: 9876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06002695 RID: 9877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06002696 RID: 9878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index, float weight);

		// Token: 0x06002697 RID: 9879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index);

		// Token: 0x04000E2B RID: 3627
		internal IntPtr m_Handle;

		// Token: 0x04000E2C RID: 3628
		internal uint m_Version;

		// Token: 0x04000E2D RID: 3629
		private static readonly PlayableHandle m_Null = default(PlayableHandle);
	}
}
