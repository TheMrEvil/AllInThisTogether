using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.LowLevel;

namespace MagicaCloth2
{
	// Token: 0x0200006D RID: 109
	public static class MagicaManager
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000E969 File Offset: 0x0000CB69
		public static TeamManager Team
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[0] : null) as TeamManager;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000E982 File Offset: 0x0000CB82
		public static ClothManager Cloth
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[1] : null) as ClothManager;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000E99B File Offset: 0x0000CB9B
		public static RenderManager Render
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[2] : null) as RenderManager;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public static TransformManager Bone
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[3] : null) as TransformManager;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000E9CD File Offset: 0x0000CBCD
		public static VirtualMeshManager VMesh
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[4] : null) as VirtualMeshManager;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000E9E6 File Offset: 0x0000CBE6
		public static SimulationManager Simulation
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[5] : null) as SimulationManager;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000E9FF File Offset: 0x0000CBFF
		public static ColliderManager Collider
		{
			get
			{
				List<IManager> list = MagicaManager.managers;
				return ((list != null) ? list[6] : null) as ColliderManager;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000EA18 File Offset: 0x0000CC18
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Initialize()
		{
			MagicaManager.Dispose();
			MagicaManager.managers = new List<IManager>();
			MagicaManager.managers.Add(new TeamManager());
			MagicaManager.managers.Add(new ClothManager());
			MagicaManager.managers.Add(new RenderManager());
			MagicaManager.managers.Add(new TransformManager());
			MagicaManager.managers.Add(new VirtualMeshManager());
			MagicaManager.managers.Add(new SimulationManager());
			MagicaManager.managers.Add(new ColliderManager());
			foreach (IManager manager in MagicaManager.managers)
			{
				manager.Initialize();
			}
			MagicaManager.InitCustomGameLoop();
			MagicaManager.isPlaying = true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		private static void Dispose()
		{
			if (MagicaManager.managers != null)
			{
				foreach (IManager manager in MagicaManager.managers)
				{
					manager.Dispose();
				}
				MagicaManager.managers = null;
			}
			MagicaManager.OnPreSimulation = null;
			MagicaManager.OnPostSimulation = null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public static bool IsPlaying()
		{
			return MagicaManager.isPlaying && Application.isPlaying && MagicaManager.IsClothEnabled;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000EB70 File Offset: 0x0000CD70
		private static void InitCustomGameLoop()
		{
			PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			if (MagicaManager.CheckRegist(ref currentPlayerLoop))
			{
				return;
			}
			MagicaManager.SetCustomGameLoop(ref currentPlayerLoop);
			PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		private static void SetCustomGameLoop(ref PlayerLoopSystem playerLoop)
		{
			PlayerLoopSystem method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterEarlyUpdateDelegate;
				if (updateMethod == null)
				{
					return;
				}
				updateMethod();
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "EarlyUpdate", string.Empty, true);
			method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterFixedUpdateDelegate;
				if (updateMethod == null)
				{
					return;
				}
				updateMethod();
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "FixedUpdate", "ScriptRunBehaviourFixedUpdate", false);
			method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterUpdateDelegate;
				if (updateMethod != null)
				{
					updateMethod();
				}
				if (Application.isPlaying)
				{
					MagicaManager.UpdateMethod updateMethod2 = MagicaManager.defaultUpdateDelegate;
					if (updateMethod2 == null)
					{
						return;
					}
					updateMethod2();
				}
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "Update", "ScriptRunDelayedTasks", false);
			method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterLateUpdateDelegate;
				if (updateMethod == null)
				{
					return;
				}
				updateMethod();
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "PreLateUpdate", "ScriptRunBehaviourLateUpdate", false);
			method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterDelayedDelegate;
				if (updateMethod == null)
				{
					return;
				}
				updateMethod();
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "PostLateUpdate", "ScriptRunDelayedDynamicFrameRate", false);
			method = default(PlayerLoopSystem);
			method.type = typeof(MagicaManager);
			method.updateDelegate = delegate()
			{
				MagicaManager.UpdateMethod updateMethod = MagicaManager.afterRenderingDelegate;
				if (updateMethod == null)
				{
					return;
				}
				updateMethod();
			};
			MagicaManager.AddPlayerLoop(method, ref playerLoop, "PostLateUpdate", "FinishFrameRendering", false);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000ED90 File Offset: 0x0000CF90
		private static void AddPlayerLoop(PlayerLoopSystem method, ref PlayerLoopSystem playerLoop, string categoryName, string systemName, bool last = false)
		{
			int num = Array.FindIndex<PlayerLoopSystem>(playerLoop.subSystemList, (PlayerLoopSystem s) => s.type.Name == categoryName);
			PlayerLoopSystem playerLoopSystem = playerLoop.subSystemList[num];
			List<PlayerLoopSystem> list = new List<PlayerLoopSystem>(playerLoopSystem.subSystemList);
			if (last)
			{
				list.Add(method);
			}
			else
			{
				int num2 = list.FindIndex((PlayerLoopSystem h) => h.type.Name.Contains(systemName));
				list.Insert(num2 + 1, method);
			}
			playerLoopSystem.subSystemList = list.ToArray();
			playerLoop.subSystemList[num] = playerLoopSystem;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000EE28 File Offset: 0x0000D028
		private static bool CheckRegist(ref PlayerLoopSystem playerLoop)
		{
			Type t = typeof(MagicaManager);
			Func<PlayerLoopSystem, bool> <>9__0;
			foreach (PlayerLoopSystem playerLoopSystem in playerLoop.subSystemList)
			{
				if (playerLoopSystem.subSystemList != null)
				{
					IEnumerable<PlayerLoopSystem> subSystemList2 = playerLoopSystem.subSystemList;
					Func<PlayerLoopSystem, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((PlayerLoopSystem x) => x.type == t));
					}
					if (subSystemList2.Any(predicate))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000EEA1 File Offset: 0x0000D0A1
		public static void SetGlobalTimeScale(float timeScale)
		{
			if (MagicaManager.IsPlaying())
			{
				MagicaManager.Team.globalTimeScale = Mathf.Clamp01(timeScale);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000EEBA File Offset: 0x0000D0BA
		public static float GetGlobalTimeScale()
		{
			if (MagicaManager.IsPlaying())
			{
				return MagicaManager.Team.globalTimeScale;
			}
			return 1f;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000EED3 File Offset: 0x0000D0D3
		// Note: this type is marked as 'beforefieldinit'.
		static MagicaManager()
		{
		}

		// Token: 0x040002C6 RID: 710
		private static List<IManager> managers = null;

		// Token: 0x040002C7 RID: 711
		public static MagicaManager.UpdateMethod afterEarlyUpdateDelegate;

		// Token: 0x040002C8 RID: 712
		public static MagicaManager.UpdateMethod afterFixedUpdateDelegate;

		// Token: 0x040002C9 RID: 713
		public static MagicaManager.UpdateMethod afterUpdateDelegate;

		// Token: 0x040002CA RID: 714
		public static MagicaManager.UpdateMethod afterLateUpdateDelegate;

		// Token: 0x040002CB RID: 715
		public static MagicaManager.UpdateMethod afterDelayedDelegate;

		// Token: 0x040002CC RID: 716
		public static MagicaManager.UpdateMethod afterRenderingDelegate;

		// Token: 0x040002CD RID: 717
		public static MagicaManager.UpdateMethod defaultUpdateDelegate;

		// Token: 0x040002CE RID: 718
		private static volatile bool isPlaying = false;

		// Token: 0x040002CF RID: 719
		public static bool IsClothEnabled = true;

		// Token: 0x040002D0 RID: 720
		public static Action OnPreSimulation;

		// Token: 0x040002D1 RID: 721
		public static Action OnPostSimulation;

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600017A RID: 378
		public delegate void UpdateMethod();

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600017D RID: 381 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600017E RID: 382 RVA: 0x00002058 File Offset: 0x00000258
			public <>c()
			{
			}

			// Token: 0x0600017F RID: 383 RVA: 0x0000EEF5 File Offset: 0x0000D0F5
			internal void <SetCustomGameLoop>b__29_0()
			{
				MagicaManager.UpdateMethod afterEarlyUpdateDelegate = MagicaManager.afterEarlyUpdateDelegate;
				if (afterEarlyUpdateDelegate == null)
				{
					return;
				}
				afterEarlyUpdateDelegate();
			}

			// Token: 0x06000180 RID: 384 RVA: 0x0000EF06 File Offset: 0x0000D106
			internal void <SetCustomGameLoop>b__29_1()
			{
				MagicaManager.UpdateMethod afterFixedUpdateDelegate = MagicaManager.afterFixedUpdateDelegate;
				if (afterFixedUpdateDelegate == null)
				{
					return;
				}
				afterFixedUpdateDelegate();
			}

			// Token: 0x06000181 RID: 385 RVA: 0x0000EF17 File Offset: 0x0000D117
			internal void <SetCustomGameLoop>b__29_2()
			{
				MagicaManager.UpdateMethod afterUpdateDelegate = MagicaManager.afterUpdateDelegate;
				if (afterUpdateDelegate != null)
				{
					afterUpdateDelegate();
				}
				if (Application.isPlaying)
				{
					MagicaManager.UpdateMethod defaultUpdateDelegate = MagicaManager.defaultUpdateDelegate;
					if (defaultUpdateDelegate == null)
					{
						return;
					}
					defaultUpdateDelegate();
				}
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000EF3F File Offset: 0x0000D13F
			internal void <SetCustomGameLoop>b__29_3()
			{
				MagicaManager.UpdateMethod afterLateUpdateDelegate = MagicaManager.afterLateUpdateDelegate;
				if (afterLateUpdateDelegate == null)
				{
					return;
				}
				afterLateUpdateDelegate();
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000EF50 File Offset: 0x0000D150
			internal void <SetCustomGameLoop>b__29_4()
			{
				MagicaManager.UpdateMethod afterDelayedDelegate = MagicaManager.afterDelayedDelegate;
				if (afterDelayedDelegate == null)
				{
					return;
				}
				afterDelayedDelegate();
			}

			// Token: 0x06000184 RID: 388 RVA: 0x0000EF61 File Offset: 0x0000D161
			internal void <SetCustomGameLoop>b__29_5()
			{
				MagicaManager.UpdateMethod afterRenderingDelegate = MagicaManager.afterRenderingDelegate;
				if (afterRenderingDelegate == null)
				{
					return;
				}
				afterRenderingDelegate();
			}

			// Token: 0x040002D2 RID: 722
			public static readonly MagicaManager.<>c <>9 = new MagicaManager.<>c();

			// Token: 0x040002D3 RID: 723
			public static PlayerLoopSystem.UpdateFunction <>9__29_0;

			// Token: 0x040002D4 RID: 724
			public static PlayerLoopSystem.UpdateFunction <>9__29_1;

			// Token: 0x040002D5 RID: 725
			public static PlayerLoopSystem.UpdateFunction <>9__29_2;

			// Token: 0x040002D6 RID: 726
			public static PlayerLoopSystem.UpdateFunction <>9__29_3;

			// Token: 0x040002D7 RID: 727
			public static PlayerLoopSystem.UpdateFunction <>9__29_4;

			// Token: 0x040002D8 RID: 728
			public static PlayerLoopSystem.UpdateFunction <>9__29_5;
		}

		// Token: 0x02000070 RID: 112
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x06000185 RID: 389 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06000186 RID: 390 RVA: 0x0000EF72 File Offset: 0x0000D172
			internal bool <AddPlayerLoop>b__0(PlayerLoopSystem s)
			{
				return s.type.Name == this.categoryName;
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000EF8A File Offset: 0x0000D18A
			internal bool <AddPlayerLoop>b__1(PlayerLoopSystem h)
			{
				return h.type.Name.Contains(this.systemName);
			}

			// Token: 0x040002D9 RID: 729
			public string categoryName;

			// Token: 0x040002DA RID: 730
			public string systemName;
		}

		// Token: 0x02000071 RID: 113
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x06000188 RID: 392 RVA: 0x00002058 File Offset: 0x00000258
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06000189 RID: 393 RVA: 0x0000EFA2 File Offset: 0x0000D1A2
			internal bool <CheckRegist>b__0(PlayerLoopSystem x)
			{
				return x.type == this.t;
			}

			// Token: 0x040002DB RID: 731
			public Type t;

			// Token: 0x040002DC RID: 732
			public Func<PlayerLoopSystem, bool> <>9__0;
		}
	}
}
