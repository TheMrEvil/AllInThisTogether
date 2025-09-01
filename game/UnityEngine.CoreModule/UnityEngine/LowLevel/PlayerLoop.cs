using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x020002EF RID: 751
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public class PlayerLoop
	{
		// Token: 0x06001E8B RID: 7819 RVA: 0x000318AC File Offset: 0x0002FAAC
		public static PlayerLoopSystem GetDefaultPlayerLoop()
		{
			PlayerLoopSystemInternal[] defaultPlayerLoopInternal = PlayerLoop.GetDefaultPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(defaultPlayerLoopInternal, ref num);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000318D0 File Offset: 0x0002FAD0
		public static PlayerLoopSystem GetCurrentPlayerLoop()
		{
			PlayerLoopSystemInternal[] currentPlayerLoopInternal = PlayerLoop.GetCurrentPlayerLoopInternal();
			int num = 0;
			return PlayerLoop.InternalToPlayerLoopSystem(currentPlayerLoopInternal, ref num);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000318F4 File Offset: 0x0002FAF4
		public static void SetPlayerLoop(PlayerLoopSystem loop)
		{
			List<PlayerLoopSystemInternal> list = new List<PlayerLoopSystemInternal>();
			PlayerLoop.PlayerLoopSystemToInternal(loop, ref list);
			PlayerLoop.SetPlayerLoopInternal(list.ToArray());
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00031920 File Offset: 0x0002FB20
		private static int PlayerLoopSystemToInternal(PlayerLoopSystem sys, ref List<PlayerLoopSystemInternal> internalSys)
		{
			int count = internalSys.Count;
			PlayerLoopSystemInternal playerLoopSystemInternal = new PlayerLoopSystemInternal
			{
				type = sys.type,
				updateDelegate = sys.updateDelegate,
				updateFunction = sys.updateFunction,
				loopConditionFunction = sys.loopConditionFunction,
				numSubSystems = 0
			};
			internalSys.Add(playerLoopSystemInternal);
			bool flag = sys.subSystemList != null;
			if (flag)
			{
				for (int i = 0; i < sys.subSystemList.Length; i++)
				{
					playerLoopSystemInternal.numSubSystems += PlayerLoop.PlayerLoopSystemToInternal(sys.subSystemList[i], ref internalSys);
				}
			}
			internalSys[count] = playerLoopSystemInternal;
			return playerLoopSystemInternal.numSubSystems + 1;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000319EC File Offset: 0x0002FBEC
		private static PlayerLoopSystem InternalToPlayerLoopSystem(PlayerLoopSystemInternal[] internalSys, ref int offset)
		{
			PlayerLoopSystem result = new PlayerLoopSystem
			{
				type = internalSys[offset].type,
				updateDelegate = internalSys[offset].updateDelegate,
				updateFunction = internalSys[offset].updateFunction,
				loopConditionFunction = internalSys[offset].loopConditionFunction,
				subSystemList = null
			};
			int num = offset;
			offset = num + 1;
			int num2 = num;
			bool flag = internalSys[num2].numSubSystems > 0;
			if (flag)
			{
				List<PlayerLoopSystem> list = new List<PlayerLoopSystem>();
				while (offset <= num2 + internalSys[num2].numSubSystems)
				{
					list.Add(PlayerLoop.InternalToPlayerLoopSystem(internalSys, ref offset));
				}
				result.subSystemList = list.ToArray();
			}
			return result;
		}

		// Token: 0x06001E90 RID: 7824
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayerLoopSystemInternal[] GetDefaultPlayerLoopInternal();

		// Token: 0x06001E91 RID: 7825
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayerLoopSystemInternal[] GetCurrentPlayerLoopInternal();

		// Token: 0x06001E92 RID: 7826
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayerLoopInternal(PlayerLoopSystemInternal[] loop);

		// Token: 0x06001E93 RID: 7827 RVA: 0x00002072 File Offset: 0x00000272
		public PlayerLoop()
		{
		}
	}
}
