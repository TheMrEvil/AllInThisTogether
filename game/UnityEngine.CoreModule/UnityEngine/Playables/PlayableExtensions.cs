using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000443 RID: 1091
	public static class PlayableExtensions
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x0003F944 File Offset: 0x0003DB44
		public static bool IsNull<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsNull();
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0003F96C File Offset: 0x0003DB6C
		public static bool IsValid<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsValid();
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0003F994 File Offset: 0x0003DB94
		public static void Destroy<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Destroy();
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		public static PlayableGraph GetGraph<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetGraph();
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		[Obsolete("SetPlayState() has been deprecated. Use Play(), Pause() or SetDelay() instead", false)]
		public static void SetPlayState<U>(this U playable, PlayState value) where U : struct, IPlayable
		{
			bool flag = value == PlayState.Delayed;
			if (flag)
			{
				throw new ArgumentException("Can't set Delayed: use SetDelay() instead");
			}
			if (value != PlayState.Paused)
			{
				if (value == PlayState.Playing)
				{
					playable.GetHandle().Play();
				}
			}
			else
			{
				playable.GetHandle().Pause();
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0003FA40 File Offset: 0x0003DC40
		public static PlayState GetPlayState<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPlayState();
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0003FA68 File Offset: 0x0003DC68
		public static void Play<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Play();
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		public static void Pause<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Pause();
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0003FAB0 File Offset: 0x0003DCB0
		public static void SetSpeed<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetSpeed(value);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0003FAD8 File Offset: 0x0003DCD8
		public static double GetSpeed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetSpeed();
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0003FB00 File Offset: 0x0003DD00
		public static void SetDuration<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDuration(value);
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0003FB28 File Offset: 0x0003DD28
		public static double GetDuration<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDuration();
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0003FB50 File Offset: 0x0003DD50
		public static void SetTime<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTime(value);
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0003FB78 File Offset: 0x0003DD78
		public static double GetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTime();
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0003FBA0 File Offset: 0x0003DDA0
		public static double GetPreviousTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPreviousTime();
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0003FBC8 File Offset: 0x0003DDC8
		public static void SetDone<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDone(value);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0003FBF0 File Offset: 0x0003DDF0
		public static bool IsDone<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDone();
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0003FC18 File Offset: 0x0003DE18
		public static void SetPropagateSetTime<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetPropagateSetTime(value);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0003FC40 File Offset: 0x0003DE40
		public static bool GetPropagateSetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPropagateSetTime();
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0003FC68 File Offset: 0x0003DE68
		public static bool CanChangeInputs<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanChangeInputs();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0003FC90 File Offset: 0x0003DE90
		public static bool CanSetWeights<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanSetWeights();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
		public static bool CanDestroy<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanDestroy();
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		public static void SetInputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputCount(value);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0003FD08 File Offset: 0x0003DF08
		public static int GetInputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputCount();
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0003FD30 File Offset: 0x0003DF30
		public static void SetOutputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetOutputCount(value);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0003FD58 File Offset: 0x0003DF58
		public static int GetOutputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutputCount();
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0003FD80 File Offset: 0x0003DF80
		public static Playable GetInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInput(inputPort);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0003FDA8 File Offset: 0x0003DFA8
		public static Playable GetOutput<U>(this U playable, int outputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutput(outputPort);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
		public static void SetInputWeight<U>(this U playable, int inputIndex, float weight) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(inputIndex, weight);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0003FDF8 File Offset: 0x0003DFF8
		public static void SetInputWeight<U, V>(this U playable, V input, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(input.GetHandle(), weight);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0003FE2C File Offset: 0x0003E02C
		public static float GetInputWeight<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputWeight(inputIndex);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0003FE54 File Offset: 0x0003E054
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.ConnectInput(inputIndex, sourcePlayable, sourceOutputIndex, 0f);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0003FE68 File Offset: 0x0003E068
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetGraph<U>().Connect<V, U>(sourcePlayable, sourceOutputIndex, playable, inputIndex);
			playable.SetInputWeight(inputIndex, weight);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0003FE94 File Offset: 0x0003E094
		public static void DisconnectInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			playable.GetGraph<U>().Disconnect<U>(playable, inputPort);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
		public static int AddInput<U, V>(this U playable, V sourcePlayable, int sourceOutputIndex, float weight = 0f) where U : struct, IPlayable where V : struct, IPlayable
		{
			int inputCount = playable.GetInputCount<U>();
			playable.SetInputCount(inputCount + 1);
			playable.ConnectInput(inputCount, sourcePlayable, sourceOutputIndex, weight);
			return inputCount;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0003FEE4 File Offset: 0x0003E0E4
		[Obsolete("SetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static void SetDelay<U>(this U playable, double delay) where U : struct, IPlayable
		{
			playable.GetHandle().SetDelay(delay);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0003FF0C File Offset: 0x0003E10C
		[Obsolete("GetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static double GetDelay<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDelay();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0003FF34 File Offset: 0x0003E134
		[Obsolete("IsDelayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static bool IsDelayed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDelayed();
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0003FF5C File Offset: 0x0003E15C
		public static void SetLeadTime<U>(this U playable, float value) where U : struct, IPlayable
		{
			playable.GetHandle().SetLeadTime(value);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0003FF84 File Offset: 0x0003E184
		public static float GetLeadTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetLeadTime();
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0003FFAC File Offset: 0x0003E1AC
		public static PlayableTraversalMode GetTraversalMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTraversalMode();
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0003FFD4 File Offset: 0x0003E1D4
		public static void SetTraversalMode<U>(this U playable, PlayableTraversalMode mode) where U : struct, IPlayable
		{
			playable.GetHandle().SetTraversalMode(mode);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0003FFFC File Offset: 0x0003E1FC
		internal static DirectorWrapMode GetTimeWrapMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTimeWrapMode();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00040024 File Offset: 0x0003E224
		internal static void SetTimeWrapMode<U>(this U playable, DirectorWrapMode value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTimeWrapMode(value);
		}
	}
}
