using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x020001F0 RID: 496
	public struct Achievement
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x0001981B File Offset: 0x00017A1B
		public Achievement(string name)
		{
			this.Value = name;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00019825 File Offset: 0x00017A25
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00019830 File Offset: 0x00017A30
		public bool State
		{
			get
			{
				bool result = false;
				SteamUserStats.Internal.GetAchievement(this.Value, ref result);
				return result;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00019858 File Offset: 0x00017A58
		public string Identifier
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00019860 File Offset: 0x00017A60
		public string Name
		{
			get
			{
				return SteamUserStats.Internal.GetAchievementDisplayAttribute(this.Value, "name");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00019877 File Offset: 0x00017A77
		public string Description
		{
			get
			{
				return SteamUserStats.Internal.GetAchievementDisplayAttribute(this.Value, "desc");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00019890 File Offset: 0x00017A90
		public DateTime? UnlockTime
		{
			get
			{
				bool flag = false;
				uint value = 0U;
				bool flag2 = !SteamUserStats.Internal.GetAchievementAndUnlockTime(this.Value, ref flag, ref value) || !flag;
				DateTime? result;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new DateTime?(Epoch.ToDateTime(value));
				}
				return result;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000198E8 File Offset: 0x00017AE8
		public Image? GetIcon()
		{
			return SteamUtils.GetImage(SteamUserStats.Internal.GetAchievementIcon(this.Value));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00019910 File Offset: 0x00017B10
		public async Task<Image?> GetIconAsync(int timeout = 5000)
		{
			Achievement.<>c__DisplayClass14_0 CS$<>8__locals1 = new Achievement.<>c__DisplayClass14_0();
			CS$<>8__locals1.i = SteamUserStats.Internal.GetAchievementIcon(this.Value);
			bool flag = CS$<>8__locals1.i != 0;
			Image? result;
			if (flag)
			{
				result = SteamUtils.GetImage(CS$<>8__locals1.i);
			}
			else
			{
				CS$<>8__locals1.ident = this.Identifier;
				CS$<>8__locals1.gotCallback = false;
				try
				{
					SteamUserStats.OnAchievementIconFetched += CS$<>8__locals1.<GetIconAsync>g__f|0;
					int waited = 0;
					while (!CS$<>8__locals1.gotCallback)
					{
						await Task.Delay(10);
						waited += 10;
						if (waited > timeout)
						{
							return null;
						}
					}
					if (CS$<>8__locals1.i == 0)
					{
						result = null;
					}
					else
					{
						result = SteamUtils.GetImage(CS$<>8__locals1.i);
					}
				}
				finally
				{
					SteamUserStats.OnAchievementIconFetched -= CS$<>8__locals1.<GetIconAsync>g__f|0;
				}
			}
			return result;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00019964 File Offset: 0x00017B64
		public float GlobalUnlocked
		{
			get
			{
				float num = 0f;
				bool flag = !SteamUserStats.Internal.GetAchievementAchievedPercent(this.Value, ref num);
				float result;
				if (flag)
				{
					result = -1f;
				}
				else
				{
					result = num / 100f;
				}
				return result;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000199A4 File Offset: 0x00017BA4
		public bool Trigger(bool apply = true)
		{
			bool flag = SteamUserStats.Internal.SetAchievement(this.Value);
			bool flag2 = apply && flag;
			if (flag2)
			{
				SteamUserStats.Internal.StoreStats();
			}
			return flag;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000199DC File Offset: 0x00017BDC
		public bool Clear()
		{
			return SteamUserStats.Internal.ClearAchievement(this.Value);
		}

		// Token: 0x04000BEB RID: 3051
		internal string Value;

		// Token: 0x02000285 RID: 645
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06001239 RID: 4665 RVA: 0x00023E0E File Offset: 0x0002200E
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x00023E18 File Offset: 0x00022018
			internal void <GetIconAsync>g__f|0(string x, int icon)
			{
				bool flag = x != this.ident;
				if (!flag)
				{
					this.i = icon;
					this.gotCallback = true;
				}
			}

			// Token: 0x04000F0B RID: 3851
			public string ident;

			// Token: 0x04000F0C RID: 3852
			public int i;

			// Token: 0x04000F0D RID: 3853
			public bool gotCallback;
		}

		// Token: 0x02000286 RID: 646
		[CompilerGenerated]
		private sealed class <GetIconAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x0600123B RID: 4667 RVA: 0x00023E46 File Offset: 0x00022046
			public <GetIconAsync>d__14()
			{
			}

			// Token: 0x0600123C RID: 4668 RVA: 0x00023E50 File Offset: 0x00022050
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? result;
				try
				{
					if (num != 0)
					{
						CS$<>8__locals1 = new Achievement.<>c__DisplayClass14_0();
						CS$<>8__locals1.i = SteamUserStats.Internal.GetAchievementIcon(this.Value);
						bool flag = CS$<>8__locals1.i != 0;
						if (flag)
						{
							result = SteamUtils.GetImage(CS$<>8__locals1.i);
							goto IL_1C5;
						}
						CS$<>8__locals1.ident = base.Identifier;
						CS$<>8__locals1.gotCallback = false;
					}
					try
					{
						if (num != 0)
						{
							SteamUserStats.OnAchievementIconFetched += CS$<>8__locals1.<GetIconAsync>g__f|0;
							waited = 0;
							goto IL_145;
						}
						TaskAwaiter taskAwaiter2;
						TaskAwaiter taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num = (num2 = -1);
						IL_10C:
						taskAwaiter.GetResult();
						waited += 10;
						bool flag2 = waited > timeout;
						if (flag2)
						{
							result = null;
							goto IL_1C5;
						}
						IL_145:
						if (CS$<>8__locals1.gotCallback)
						{
							bool flag3 = CS$<>8__locals1.i == 0;
							if (flag3)
							{
								result = null;
							}
							else
							{
								result = SteamUtils.GetImage(CS$<>8__locals1.i);
							}
						}
						else
						{
							taskAwaiter = Task.Delay(10).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								taskAwaiter2 = taskAwaiter;
								Achievement.<GetIconAsync>d__14 <GetIconAsync>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Achievement.<GetIconAsync>d__14>(ref taskAwaiter, ref <GetIconAsync>d__);
								return;
							}
							goto IL_10C;
						}
					}
					finally
					{
						if (num < 0)
						{
							SteamUserStats.OnAchievementIconFetched -= CS$<>8__locals1.<GetIconAsync>g__f|0;
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1C5:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600123D RID: 4669 RVA: 0x0002406C File Offset: 0x0002226C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F0E RID: 3854
			public int <>1__state;

			// Token: 0x04000F0F RID: 3855
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000F10 RID: 3856
			public int timeout;

			// Token: 0x04000F11 RID: 3857
			public Achievement <>4__this;

			// Token: 0x04000F12 RID: 3858
			private Achievement.<>c__DisplayClass14_0 <>8__1;

			// Token: 0x04000F13 RID: 3859
			private int <waited>5__2;

			// Token: 0x04000F14 RID: 3860
			private TaskAwaiter <>u__1;
		}
	}
}
