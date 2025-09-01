using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C9 RID: 201
	public struct ResultPage : IDisposable
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00014A60 File Offset: 0x00012C60
		public IEnumerable<Item> Entries
		{
			get
			{
				SteamUGCDetails_t details = default(SteamUGCDetails_t);
				uint i = 0U;
				while ((ulong)i < (ulong)((long)this.ResultCount))
				{
					bool queryUGCResult = SteamUGC.Internal.GetQueryUGCResult(this.Handle, i, ref details);
					if (queryUGCResult)
					{
						Item item = Item.From(details);
						item.NumSubscriptions = this.GetStat(i, ItemStatistic.NumSubscriptions);
						item.NumFavorites = this.GetStat(i, ItemStatistic.NumFavorites);
						item.NumFollowers = this.GetStat(i, ItemStatistic.NumFollowers);
						item.NumUniqueSubscriptions = this.GetStat(i, ItemStatistic.NumUniqueSubscriptions);
						item.NumUniqueFavorites = this.GetStat(i, ItemStatistic.NumUniqueFavorites);
						item.NumUniqueFollowers = this.GetStat(i, ItemStatistic.NumUniqueFollowers);
						item.NumUniqueWebsiteViews = this.GetStat(i, ItemStatistic.NumUniqueWebsiteViews);
						item.ReportScore = this.GetStat(i, ItemStatistic.ReportScore);
						item.NumSecondsPlayed = this.GetStat(i, ItemStatistic.NumSecondsPlayed);
						item.NumPlaytimeSessions = this.GetStat(i, ItemStatistic.NumPlaytimeSessions);
						item.NumComments = this.GetStat(i, ItemStatistic.NumComments);
						item.NumSecondsPlayedDuringTimePeriod = this.GetStat(i, ItemStatistic.NumSecondsPlayedDuringTimePeriod);
						item.NumPlaytimeSessionsDuringTimePeriod = this.GetStat(i, ItemStatistic.NumPlaytimeSessionsDuringTimePeriod);
						string preview;
						bool queryUGCPreviewURL = SteamUGC.Internal.GetQueryUGCPreviewURL(this.Handle, i, out preview);
						if (queryUGCPreviewURL)
						{
							item.PreviewImageUrl = preview;
						}
						yield return item;
						item = default(Item);
						preview = null;
					}
					uint num = i;
					i = num + 1U;
				}
				yield break;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00014A84 File Offset: 0x00012C84
		private ulong GetStat(uint index, ItemStatistic stat)
		{
			ulong num = 0UL;
			bool flag = !SteamUGC.Internal.GetQueryUGCStatistic(this.Handle, index, stat, ref num);
			ulong result;
			if (flag)
			{
				result = 0UL;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00014ABC File Offset: 0x00012CBC
		public void Dispose()
		{
			bool flag = this.Handle > 0UL;
			if (flag)
			{
				SteamUGC.Internal.ReleaseQueryUGCRequest(this.Handle);
				this.Handle = 0UL;
			}
		}

		// Token: 0x040007C4 RID: 1988
		internal UGCQueryHandle_t Handle;

		// Token: 0x040007C5 RID: 1989
		public int ResultCount;

		// Token: 0x040007C6 RID: 1990
		public int TotalCount;

		// Token: 0x040007C7 RID: 1991
		public bool CachedData;

		// Token: 0x0200027D RID: 637
		[CompilerGenerated]
		private sealed class <get_Entries>d__5 : IEnumerable<Item>, IEnumerable, IEnumerator<Item>, IDisposable, IEnumerator
		{
			// Token: 0x0600122B RID: 4651 RVA: 0x000235AE File Offset: 0x000217AE
			[DebuggerHidden]
			public <get_Entries>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600122C RID: 4652 RVA: 0x000235C9 File Offset: 0x000217C9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600122D RID: 4653 RVA: 0x000235CC File Offset: 0x000217CC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					details = default(SteamUGCDetails_t);
					i = 0U;
					goto IL_27A;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				item = default(Item);
				preview = null;
				IL_269:
				uint num2 = i;
				i = num2 + 1U;
				IL_27A:
				if ((ulong)i >= (ulong)((long)this.ResultCount))
				{
					return false;
				}
				bool queryUGCResult = SteamUGC.Internal.GetQueryUGCResult(this.Handle, i, ref details);
				if (queryUGCResult)
				{
					item = Item.From(details);
					item.NumSubscriptions = base.GetStat(i, ItemStatistic.NumSubscriptions);
					item.NumFavorites = base.GetStat(i, ItemStatistic.NumFavorites);
					item.NumFollowers = base.GetStat(i, ItemStatistic.NumFollowers);
					item.NumUniqueSubscriptions = base.GetStat(i, ItemStatistic.NumUniqueSubscriptions);
					item.NumUniqueFavorites = base.GetStat(i, ItemStatistic.NumUniqueFavorites);
					item.NumUniqueFollowers = base.GetStat(i, ItemStatistic.NumUniqueFollowers);
					item.NumUniqueWebsiteViews = base.GetStat(i, ItemStatistic.NumUniqueWebsiteViews);
					item.ReportScore = base.GetStat(i, ItemStatistic.ReportScore);
					item.NumSecondsPlayed = base.GetStat(i, ItemStatistic.NumSecondsPlayed);
					item.NumPlaytimeSessions = base.GetStat(i, ItemStatistic.NumPlaytimeSessions);
					item.NumComments = base.GetStat(i, ItemStatistic.NumComments);
					item.NumSecondsPlayedDuringTimePeriod = base.GetStat(i, ItemStatistic.NumSecondsPlayedDuringTimePeriod);
					item.NumPlaytimeSessionsDuringTimePeriod = base.GetStat(i, ItemStatistic.NumPlaytimeSessionsDuringTimePeriod);
					bool queryUGCPreviewURL = SteamUGC.Internal.GetQueryUGCPreviewURL(this.Handle, i, out preview);
					if (queryUGCPreviewURL)
					{
						item.PreviewImageUrl = preview;
					}
					this.<>2__current = item;
					this.<>1__state = 1;
					return true;
				}
				goto IL_269;
			}

			// Token: 0x17000307 RID: 775
			// (get) Token: 0x0600122E RID: 4654 RVA: 0x00023872 File Offset: 0x00021A72
			Item IEnumerator<Item>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600122F RID: 4655 RVA: 0x0002387A File Offset: 0x00021A7A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000308 RID: 776
			// (get) Token: 0x06001230 RID: 4656 RVA: 0x00023881 File Offset: 0x00021A81
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001231 RID: 4657 RVA: 0x00023890 File Offset: 0x00021A90
			[DebuggerHidden]
			IEnumerator<Item> IEnumerable<Item>.GetEnumerator()
			{
				ResultPage.<get_Entries>d__5 <get_Entries>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Entries>d__ = this;
				}
				else
				{
					<get_Entries>d__ = new ResultPage.<get_Entries>d__5(0);
				}
				<get_Entries>d__.<>4__this = ref this;
				return <get_Entries>d__;
			}

			// Token: 0x06001232 RID: 4658 RVA: 0x000238D3 File Offset: 0x00021AD3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Ugc.Item>.GetEnumerator();
			}

			// Token: 0x04000ED9 RID: 3801
			private int <>1__state;

			// Token: 0x04000EDA RID: 3802
			private Item <>2__current;

			// Token: 0x04000EDB RID: 3803
			private int <>l__initialThreadId;

			// Token: 0x04000EDC RID: 3804
			public ResultPage <>4__this;

			// Token: 0x04000EDD RID: 3805
			public ResultPage <>3__<>4__this;

			// Token: 0x04000EDE RID: 3806
			private SteamUGCDetails_t <details>5__1;

			// Token: 0x04000EDF RID: 3807
			private uint <i>5__2;

			// Token: 0x04000EE0 RID: 3808
			private Item <item>5__3;

			// Token: 0x04000EE1 RID: 3809
			private string <preview>5__4;
		}
	}
}
