using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000056 RID: 86
	public static class SceneUtilities
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00009A1D File Offset: 0x00007C1D
		public static IEnumerable<Scene> GetScenesInBuild()
		{
			int sceneCount = SceneManager.sceneCountInBuildSettings;
			int num;
			for (int i = 0; i < sceneCount; i = num + 1)
			{
				yield return SceneManager.GetSceneByBuildIndex(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009A26 File Offset: 0x00007C26
		public static IEnumerable<Scene> GetLoadedScenes()
		{
			int sceneCount = SceneManager.sceneCount;
			int num;
			for (int i = 0; i < sceneCount; i = num + 1)
			{
				yield return SceneManager.GetSceneAt(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009A2F File Offset: 0x00007C2F
		public static IEnumerable<Scene> GetAllScenes()
		{
			return SceneUtilities.GetScenesInBuild();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009A38 File Offset: 0x00007C38
		public static IEnumerable<string> GetAllScenePaths()
		{
			return from x in SceneUtilities.GetAllScenes()
			where x.IsValid()
			select x.path;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009A94 File Offset: 0x00007C94
		public static IEnumerable<string> GetAllSceneNames()
		{
			return from x in SceneUtilities.GetAllScenes()
			where x.IsValid()
			select x.name;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009AEE File Offset: 0x00007CEE
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsync(sceneName, mode);
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private sealed class <GetScenesInBuild>d__0 : IEnumerable<Scene>, IEnumerable, IEnumerator<Scene>, IEnumerator, IDisposable
		{
			// Token: 0x06000359 RID: 857 RVA: 0x0000C643 File Offset: 0x0000A843
			[DebuggerHidden]
			public <GetScenesInBuild>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600035A RID: 858 RVA: 0x0000C65D File Offset: 0x0000A85D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000C660 File Offset: 0x0000A860
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					sceneCount = SceneManager.sceneCountInBuildSettings;
					i = 0;
				}
				if (i >= sceneCount)
				{
					return false;
				}
				this.<>2__current = SceneManager.GetSceneByBuildIndex(i);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
			Scene IEnumerator<Scene>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
			[DebuggerHidden]
			IEnumerator<Scene> IEnumerable<Scene>.GetEnumerator()
			{
				SceneUtilities.<GetScenesInBuild>d__0 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SceneUtilities.<GetScenesInBuild>d__0(0);
				}
				return result;
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000C72B File Offset: 0x0000A92B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.SceneManagement.Scene>.GetEnumerator();
			}

			// Token: 0x0400022B RID: 555
			private int <>1__state;

			// Token: 0x0400022C RID: 556
			private Scene <>2__current;

			// Token: 0x0400022D RID: 557
			private int <>l__initialThreadId;

			// Token: 0x0400022E RID: 558
			private int <sceneCount>5__2;

			// Token: 0x0400022F RID: 559
			private int <i>5__3;
		}

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class <GetLoadedScenes>d__1 : IEnumerable<Scene>, IEnumerable, IEnumerator<Scene>, IEnumerator, IDisposable
		{
			// Token: 0x06000361 RID: 865 RVA: 0x0000C733 File Offset: 0x0000A933
			[DebuggerHidden]
			public <GetLoadedScenes>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0000C74D File Offset: 0x0000A94D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000363 RID: 867 RVA: 0x0000C750 File Offset: 0x0000A950
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					sceneCount = SceneManager.sceneCount;
					i = 0;
				}
				if (i >= sceneCount)
				{
					return false;
				}
				this.<>2__current = SceneManager.GetSceneAt(i);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
			Scene IEnumerator<Scene>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C7D7 File Offset: 0x0000A9D7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
			[DebuggerHidden]
			IEnumerator<Scene> IEnumerable<Scene>.GetEnumerator()
			{
				SceneUtilities.<GetLoadedScenes>d__1 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SceneUtilities.<GetLoadedScenes>d__1(0);
				}
				return result;
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000C81B File Offset: 0x0000AA1B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.SceneManagement.Scene>.GetEnumerator();
			}

			// Token: 0x04000230 RID: 560
			private int <>1__state;

			// Token: 0x04000231 RID: 561
			private Scene <>2__current;

			// Token: 0x04000232 RID: 562
			private int <>l__initialThreadId;

			// Token: 0x04000233 RID: 563
			private int <sceneCount>5__2;

			// Token: 0x04000234 RID: 564
			private int <i>5__3;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000369 RID: 873 RVA: 0x0000C823 File Offset: 0x0000AA23
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000C82F File Offset: 0x0000AA2F
			public <>c()
			{
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000C837 File Offset: 0x0000AA37
			internal bool <GetAllScenePaths>b__3_0(Scene x)
			{
				return x.IsValid();
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000C840 File Offset: 0x0000AA40
			internal string <GetAllScenePaths>b__3_1(Scene x)
			{
				return x.path;
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000C849 File Offset: 0x0000AA49
			internal bool <GetAllSceneNames>b__4_0(Scene x)
			{
				return x.IsValid();
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000C852 File Offset: 0x0000AA52
			internal string <GetAllSceneNames>b__4_1(Scene x)
			{
				return x.name;
			}

			// Token: 0x04000235 RID: 565
			public static readonly SceneUtilities.<>c <>9 = new SceneUtilities.<>c();

			// Token: 0x04000236 RID: 566
			public static Func<Scene, bool> <>9__3_0;

			// Token: 0x04000237 RID: 567
			public static Func<Scene, string> <>9__3_1;

			// Token: 0x04000238 RID: 568
			public static Func<Scene, bool> <>9__4_0;

			// Token: 0x04000239 RID: 569
			public static Func<Scene, string> <>9__4_1;
		}
	}
}
