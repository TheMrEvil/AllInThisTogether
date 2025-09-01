using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C8 RID: 200
	public struct Query
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x00013EAA File Offset: 0x000120AA
		public Query(UgcType type)
		{
			this = default(Query);
			this.matchingType = type;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00013EBB File Offset: 0x000120BB
		public static Query All
		{
			get
			{
				return new Query(UgcType.All);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00013EC3 File Offset: 0x000120C3
		public static Query Items
		{
			get
			{
				return new Query(UgcType.Items);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00013ECB File Offset: 0x000120CB
		public static Query ItemsMtx
		{
			get
			{
				return new Query(UgcType.Items_Mtx);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00013ED3 File Offset: 0x000120D3
		public static Query ItemsReadyToUse
		{
			get
			{
				return new Query(UgcType.Items_ReadyToUse);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00013EDB File Offset: 0x000120DB
		public static Query Collections
		{
			get
			{
				return new Query(UgcType.Collections);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00013EE3 File Offset: 0x000120E3
		public static Query Artwork
		{
			get
			{
				return new Query(UgcType.Artwork);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00013EEB File Offset: 0x000120EB
		public static Query Videos
		{
			get
			{
				return new Query(UgcType.Videos);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00013EF3 File Offset: 0x000120F3
		public static Query Screenshots
		{
			get
			{
				return new Query(UgcType.Screenshots);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00013EFB File Offset: 0x000120FB
		public static Query AllGuides
		{
			get
			{
				return new Query(UgcType.AllGuides);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00013F03 File Offset: 0x00012103
		public static Query WebGuides
		{
			get
			{
				return new Query(UgcType.WebGuides);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00013F0B File Offset: 0x0001210B
		public static Query IntegratedGuides
		{
			get
			{
				return new Query(UgcType.IntegratedGuides);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00013F14 File Offset: 0x00012114
		public static Query UsableInGame
		{
			get
			{
				return new Query(UgcType.UsableInGame);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00013F1D File Offset: 0x0001211D
		public static Query ControllerBindings
		{
			get
			{
				return new Query(UgcType.ControllerBindings);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00013F26 File Offset: 0x00012126
		public static Query GameManagedItems
		{
			get
			{
				return new Query(UgcType.GameManagedItems);
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00013F30 File Offset: 0x00012130
		public Query RankedByVote()
		{
			this.queryType = UGCQuery.RankedByVote;
			return this;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00013F50 File Offset: 0x00012150
		public Query RankedByPublicationDate()
		{
			this.queryType = UGCQuery.RankedByPublicationDate;
			return this;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00013F70 File Offset: 0x00012170
		public Query RankedByAcceptanceDate()
		{
			this.queryType = UGCQuery.AcceptedForGameRankedByAcceptanceDate;
			return this;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00013F90 File Offset: 0x00012190
		public Query RankedByTrend()
		{
			this.queryType = UGCQuery.RankedByTrend;
			return this;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00013FB0 File Offset: 0x000121B0
		public Query FavoritedByFriends()
		{
			this.queryType = UGCQuery.FavoritedByFriendsRankedByPublicationDate;
			return this;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00013FD0 File Offset: 0x000121D0
		public Query CreatedByFriends()
		{
			this.queryType = UGCQuery.CreatedByFriendsRankedByPublicationDate;
			return this;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00013FF0 File Offset: 0x000121F0
		public Query RankedByNumTimesReported()
		{
			this.queryType = UGCQuery.RankedByNumTimesReported;
			return this;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00014010 File Offset: 0x00012210
		public Query CreatedByFollowedUsers()
		{
			this.queryType = UGCQuery.CreatedByFollowedUsersRankedByPublicationDate;
			return this;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00014030 File Offset: 0x00012230
		public Query NotYetRated()
		{
			this.queryType = UGCQuery.NotYetRated;
			return this;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00014050 File Offset: 0x00012250
		public Query RankedByTotalVotesAsc()
		{
			this.queryType = UGCQuery.RankedByTotalVotesAsc;
			return this;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00014070 File Offset: 0x00012270
		public Query RankedByVotesUp()
		{
			this.queryType = UGCQuery.RankedByVotesUp;
			return this;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00014090 File Offset: 0x00012290
		public Query RankedByTextSearch()
		{
			this.queryType = UGCQuery.RankedByTextSearch;
			return this;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000140B0 File Offset: 0x000122B0
		public Query RankedByTotalUniqueSubscriptions()
		{
			this.queryType = UGCQuery.RankedByTotalUniqueSubscriptions;
			return this;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000140D0 File Offset: 0x000122D0
		public Query RankedByPlaytimeTrend()
		{
			this.queryType = UGCQuery.RankedByPlaytimeTrend;
			return this;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000140F0 File Offset: 0x000122F0
		public Query RankedByTotalPlaytime()
		{
			this.queryType = UGCQuery.RankedByTotalPlaytime;
			return this;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00014110 File Offset: 0x00012310
		public Query RankedByAveragePlaytimeTrend()
		{
			this.queryType = UGCQuery.RankedByAveragePlaytimeTrend;
			return this;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00014130 File Offset: 0x00012330
		public Query RankedByLifetimeAveragePlaytime()
		{
			this.queryType = UGCQuery.RankedByLifetimeAveragePlaytime;
			return this;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00014150 File Offset: 0x00012350
		public Query RankedByPlaytimeSessionsTrend()
		{
			this.queryType = UGCQuery.RankedByPlaytimeSessionsTrend;
			return this;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00014170 File Offset: 0x00012370
		public Query RankedByLifetimePlaytimeSessions()
		{
			this.queryType = UGCQuery.RankedByLifetimePlaytimeSessions;
			return this;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00014190 File Offset: 0x00012390
		internal Query LimitUser(SteamId steamid)
		{
			bool flag = steamid.Value == 0UL;
			if (flag)
			{
				steamid = SteamClient.SteamId;
			}
			this.steamid = new SteamId?(steamid);
			return this;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000141CC File Offset: 0x000123CC
		public Query WhereUserPublished(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.Published;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000141F4 File Offset: 0x000123F4
		public Query WhereUserVotedOn(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.VotedOn;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001421C File Offset: 0x0001241C
		public Query WhereUserVotedUp(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.VotedUp;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00014244 File Offset: 0x00012444
		public Query WhereUserVotedDown(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.VotedDown;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001426C File Offset: 0x0001246C
		public Query WhereUserWillVoteLater(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.WillVoteLater;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00014294 File Offset: 0x00012494
		public Query WhereUserFavorited(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.Favorited;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000142BC File Offset: 0x000124BC
		public Query WhereUserSubscribed(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.Subscribed;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000142E4 File Offset: 0x000124E4
		public Query WhereUserUsedOrPlayed(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.UsedOrPlayed;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001430C File Offset: 0x0001250C
		public Query WhereUserFollowed(SteamId user = default(SteamId))
		{
			this.userType = UserUGCList.Followed;
			this.LimitUser(user);
			return this;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00014334 File Offset: 0x00012534
		public Query SortByCreationDate()
		{
			this.userSort = UserUGCListSortOrder.CreationOrderDesc;
			return this;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00014354 File Offset: 0x00012554
		public Query SortByCreationDateAsc()
		{
			this.userSort = UserUGCListSortOrder.CreationOrderAsc;
			return this;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00014374 File Offset: 0x00012574
		public Query SortByTitleAsc()
		{
			this.userSort = UserUGCListSortOrder.TitleAsc;
			return this;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00014394 File Offset: 0x00012594
		public Query SortByUpdateDate()
		{
			this.userSort = UserUGCListSortOrder.LastUpdatedDesc;
			return this;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000143B4 File Offset: 0x000125B4
		public Query SortBySubscriptionDate()
		{
			this.userSort = UserUGCListSortOrder.SubscriptionDateDesc;
			return this;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000143D4 File Offset: 0x000125D4
		public Query SortByVoteScore()
		{
			this.userSort = UserUGCListSortOrder.VoteScoreDesc;
			return this;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000143F4 File Offset: 0x000125F4
		public Query SortByModeration()
		{
			this.userSort = UserUGCListSortOrder.ForModeration;
			return this;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00014414 File Offset: 0x00012614
		public Query WhereSearchText(string searchText)
		{
			this.searchText = searchText;
			return this;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00014434 File Offset: 0x00012634
		public Query WithFileId(params PublishedFileId[] files)
		{
			this.Files = files;
			return this;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00014454 File Offset: 0x00012654
		public async Task<ResultPage?> GetPageAsync(int page)
		{
			bool flag = page <= 0;
			if (flag)
			{
				throw new Exception("page should be > 0");
			}
			bool flag2 = this.consumerApp == 0U;
			if (flag2)
			{
				this.consumerApp = SteamClient.AppId;
			}
			bool flag3 = this.creatorApp == 0U;
			if (flag3)
			{
				this.creatorApp = this.consumerApp;
			}
			bool flag4 = this.Files != null;
			UGCQueryHandle_t handle;
			if (flag4)
			{
				handle = SteamUGC.Internal.CreateQueryUGCDetailsRequest(this.Files, (uint)this.Files.Length);
			}
			else
			{
				bool flag5 = this.steamid != null;
				if (flag5)
				{
					handle = SteamUGC.Internal.CreateQueryUserUGCRequest(this.steamid.Value.AccountId, this.userType, this.matchingType, this.userSort, this.creatorApp.Value, this.consumerApp.Value, (uint)page);
				}
				else
				{
					handle = SteamUGC.Internal.CreateQueryAllUGCRequest(this.queryType, this.matchingType, this.creatorApp.Value, this.consumerApp.Value, (uint)page);
				}
			}
			this.ApplyReturns(handle);
			bool flag6 = this.maxCacheAge != null;
			if (flag6)
			{
				SteamUGC.Internal.SetAllowCachedResponse(handle, (uint)this.maxCacheAge.Value);
			}
			this.ApplyConstraints(handle);
			SteamUGCQueryCompleted_t? steamUGCQueryCompleted_t = await SteamUGC.Internal.SendQueryUGCRequest(handle);
			SteamUGCQueryCompleted_t? result = steamUGCQueryCompleted_t;
			steamUGCQueryCompleted_t = null;
			ResultPage? result2;
			if (result == null)
			{
				result2 = null;
			}
			else if (result.Value.Result != Result.OK)
			{
				result2 = null;
			}
			else
			{
				result2 = new ResultPage?(new ResultPage
				{
					Handle = result.Value.Handle,
					ResultCount = (int)result.Value.NumResultsReturned,
					TotalCount = (int)result.Value.TotalMatchingResults,
					CachedData = result.Value.CachedData
				});
			}
			return result2;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000144A8 File Offset: 0x000126A8
		public Query WithType(UgcType type)
		{
			this.matchingType = type;
			return this;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000144C8 File Offset: 0x000126C8
		public Query AllowCachedResponse(int maxSecondsAge)
		{
			this.maxCacheAge = new int?(maxSecondsAge);
			return this;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000144EC File Offset: 0x000126EC
		public Query InLanguage(string lang)
		{
			this.language = lang;
			return this;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001450C File Offset: 0x0001270C
		public Query WithTrendDays(int days)
		{
			this.trendDays = new int?(days);
			return this;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00014530 File Offset: 0x00012730
		public Query MatchAnyTag()
		{
			this.matchAnyTag = new bool?(true);
			return this;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00014554 File Offset: 0x00012754
		public Query MatchAllTags()
		{
			this.matchAnyTag = new bool?(false);
			return this;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00014578 File Offset: 0x00012778
		public Query WithTag(string tag)
		{
			bool flag = this.requiredTags == null;
			if (flag)
			{
				this.requiredTags = new List<string>();
			}
			this.requiredTags.Add(tag);
			return this;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000145B8 File Offset: 0x000127B8
		public Query AddRequiredKeyValueTag(string key, string value)
		{
			bool flag = this.requiredKv == null;
			if (flag)
			{
				this.requiredKv = new Dictionary<string, string>();
			}
			this.requiredKv.Add(key, value);
			return this;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000145F8 File Offset: 0x000127F8
		public Query WithoutTag(string tag)
		{
			bool flag = this.excludedTags == null;
			if (flag)
			{
				this.excludedTags = new List<string>();
			}
			this.excludedTags.Add(tag);
			return this;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00014638 File Offset: 0x00012838
		private void ApplyConstraints(UGCQueryHandle_t handle)
		{
			bool flag = this.requiredTags != null;
			if (flag)
			{
				foreach (string pTagName in this.requiredTags)
				{
					SteamUGC.Internal.AddRequiredTag(handle, pTagName);
				}
			}
			bool flag2 = this.excludedTags != null;
			if (flag2)
			{
				foreach (string pTagName2 in this.excludedTags)
				{
					SteamUGC.Internal.AddExcludedTag(handle, pTagName2);
				}
			}
			bool flag3 = this.requiredKv != null;
			if (flag3)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.requiredKv)
				{
					SteamUGC.Internal.AddRequiredKeyValueTag(handle, keyValuePair.Key, keyValuePair.Value);
				}
			}
			bool flag4 = this.matchAnyTag != null;
			if (flag4)
			{
				SteamUGC.Internal.SetMatchAnyTag(handle, this.matchAnyTag.Value);
			}
			bool flag5 = this.trendDays != null;
			if (flag5)
			{
				SteamUGC.Internal.SetRankedByTrendDays(handle, (uint)this.trendDays.Value);
			}
			bool flag6 = !string.IsNullOrEmpty(this.searchText);
			if (flag6)
			{
				SteamUGC.Internal.SetSearchText(handle, this.searchText);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000147E8 File Offset: 0x000129E8
		public Query WithOnlyIDs(bool b)
		{
			this.WantsReturnOnlyIDs = new bool?(b);
			return this;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001480C File Offset: 0x00012A0C
		public Query WithKeyValueTag(bool b)
		{
			this.WantsReturnKeyValueTags = new bool?(b);
			return this;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00014830 File Offset: 0x00012A30
		public Query WithLongDescription(bool b)
		{
			this.WantsReturnLongDescription = new bool?(b);
			return this;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00014854 File Offset: 0x00012A54
		public Query WithMetadata(bool b)
		{
			this.WantsReturnMetadata = new bool?(b);
			return this;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00014878 File Offset: 0x00012A78
		public Query WithChildren(bool b)
		{
			this.WantsReturnChildren = new bool?(b);
			return this;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001489C File Offset: 0x00012A9C
		public Query WithAdditionalPreviews(bool b)
		{
			this.WantsReturnAdditionalPreviews = new bool?(b);
			return this;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000148C0 File Offset: 0x00012AC0
		public Query WithTotalOnly(bool b)
		{
			this.WantsReturnTotalOnly = new bool?(b);
			return this;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000148E4 File Offset: 0x00012AE4
		public Query WithPlaytimeStats(uint unDays)
		{
			this.WantsReturnPlaytimeStats = new uint?(unDays);
			return this;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00014908 File Offset: 0x00012B08
		private void ApplyReturns(UGCQueryHandle_t handle)
		{
			bool flag = this.WantsReturnOnlyIDs != null;
			if (flag)
			{
				SteamUGC.Internal.SetReturnOnlyIDs(handle, this.WantsReturnOnlyIDs.Value);
			}
			bool flag2 = this.WantsReturnKeyValueTags != null;
			if (flag2)
			{
				SteamUGC.Internal.SetReturnKeyValueTags(handle, this.WantsReturnKeyValueTags.Value);
			}
			bool flag3 = this.WantsReturnLongDescription != null;
			if (flag3)
			{
				SteamUGC.Internal.SetReturnLongDescription(handle, this.WantsReturnLongDescription.Value);
			}
			bool flag4 = this.WantsReturnMetadata != null;
			if (flag4)
			{
				SteamUGC.Internal.SetReturnMetadata(handle, this.WantsReturnMetadata.Value);
			}
			bool flag5 = this.WantsReturnChildren != null;
			if (flag5)
			{
				SteamUGC.Internal.SetReturnChildren(handle, this.WantsReturnChildren.Value);
			}
			bool flag6 = this.WantsReturnAdditionalPreviews != null;
			if (flag6)
			{
				SteamUGC.Internal.SetReturnAdditionalPreviews(handle, this.WantsReturnAdditionalPreviews.Value);
			}
			bool flag7 = this.WantsReturnTotalOnly != null;
			if (flag7)
			{
				SteamUGC.Internal.SetReturnTotalOnly(handle, this.WantsReturnTotalOnly.Value);
			}
			bool flag8 = this.WantsReturnPlaytimeStats != null;
			if (flag8)
			{
				SteamUGC.Internal.SetReturnPlaytimeStats(handle, this.WantsReturnPlaytimeStats.Value);
			}
		}

		// Token: 0x040007AC RID: 1964
		private UgcType matchingType;

		// Token: 0x040007AD RID: 1965
		private UGCQuery queryType;

		// Token: 0x040007AE RID: 1966
		private AppId consumerApp;

		// Token: 0x040007AF RID: 1967
		private AppId creatorApp;

		// Token: 0x040007B0 RID: 1968
		private string searchText;

		// Token: 0x040007B1 RID: 1969
		private SteamId? steamid;

		// Token: 0x040007B2 RID: 1970
		private UserUGCList userType;

		// Token: 0x040007B3 RID: 1971
		private UserUGCListSortOrder userSort;

		// Token: 0x040007B4 RID: 1972
		private PublishedFileId[] Files;

		// Token: 0x040007B5 RID: 1973
		private int? maxCacheAge;

		// Token: 0x040007B6 RID: 1974
		private string language;

		// Token: 0x040007B7 RID: 1975
		private int? trendDays;

		// Token: 0x040007B8 RID: 1976
		private List<string> requiredTags;

		// Token: 0x040007B9 RID: 1977
		private bool? matchAnyTag;

		// Token: 0x040007BA RID: 1978
		private List<string> excludedTags;

		// Token: 0x040007BB RID: 1979
		private Dictionary<string, string> requiredKv;

		// Token: 0x040007BC RID: 1980
		private bool? WantsReturnOnlyIDs;

		// Token: 0x040007BD RID: 1981
		private bool? WantsReturnKeyValueTags;

		// Token: 0x040007BE RID: 1982
		private bool? WantsReturnLongDescription;

		// Token: 0x040007BF RID: 1983
		private bool? WantsReturnMetadata;

		// Token: 0x040007C0 RID: 1984
		private bool? WantsReturnChildren;

		// Token: 0x040007C1 RID: 1985
		private bool? WantsReturnAdditionalPreviews;

		// Token: 0x040007C2 RID: 1986
		private bool? WantsReturnTotalOnly;

		// Token: 0x040007C3 RID: 1987
		private uint? WantsReturnPlaytimeStats;

		// Token: 0x0200027C RID: 636
		[CompilerGenerated]
		private sealed class <GetPageAsync>d__76 : IAsyncStateMachine
		{
			// Token: 0x06001228 RID: 4648 RVA: 0x000231E6 File Offset: 0x000213E6
			public <GetPageAsync>d__76()
			{
			}

			// Token: 0x06001229 RID: 4649 RVA: 0x000231F0 File Offset: 0x000213F0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				ResultPage? result2;
				try
				{
					CallResult<SteamUGCQueryCompleted_t> callResult;
					if (num != 0)
					{
						bool flag = page <= 0;
						if (flag)
						{
							throw new Exception("page should be > 0");
						}
						bool flag2 = this.consumerApp == 0U;
						if (flag2)
						{
							this.consumerApp = SteamClient.AppId;
						}
						bool flag3 = this.creatorApp == 0U;
						if (flag3)
						{
							this.creatorApp = this.consumerApp;
						}
						bool flag4 = this.Files != null;
						if (flag4)
						{
							handle = SteamUGC.Internal.CreateQueryUGCDetailsRequest(this.Files, (uint)this.Files.Length);
						}
						else
						{
							bool flag5 = this.steamid != null;
							if (flag5)
							{
								handle = SteamUGC.Internal.CreateQueryUserUGCRequest(this.steamid.Value.AccountId, this.userType, this.matchingType, this.userSort, this.creatorApp.Value, this.consumerApp.Value, (uint)page);
							}
							else
							{
								handle = SteamUGC.Internal.CreateQueryAllUGCRequest(this.queryType, this.matchingType, this.creatorApp.Value, this.consumerApp.Value, (uint)page);
							}
						}
						base.ApplyReturns(handle);
						bool flag6 = this.maxCacheAge != null;
						if (flag6)
						{
							SteamUGC.Internal.SetAllowCachedResponse(handle, (uint)this.maxCacheAge.Value);
						}
						base.ApplyConstraints(handle);
						callResult = SteamUGC.Internal.SendQueryUGCRequest(handle).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<SteamUGCQueryCompleted_t> callResult2 = callResult;
							Query.<GetPageAsync>d__76 <GetPageAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<SteamUGCQueryCompleted_t>, Query.<GetPageAsync>d__76>(ref callResult, ref <GetPageAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<SteamUGCQueryCompleted_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<SteamUGCQueryCompleted_t>);
						num2 = -1;
					}
					steamUGCQueryCompleted_t = callResult.GetResult();
					result = steamUGCQueryCompleted_t;
					steamUGCQueryCompleted_t = null;
					bool flag7 = result == null;
					if (flag7)
					{
						result2 = null;
					}
					else
					{
						bool flag8 = result.Value.Result != Result.OK;
						if (flag8)
						{
							result2 = null;
						}
						else
						{
							result2 = new ResultPage?(new ResultPage
							{
								Handle = result.Value.Handle,
								ResultCount = (int)result.Value.NumResultsReturned,
								TotalCount = (int)result.Value.TotalMatchingResults,
								CachedData = result.Value.CachedData
							});
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600122A RID: 4650 RVA: 0x000235AC File Offset: 0x000217AC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000ED1 RID: 3793
			public int <>1__state;

			// Token: 0x04000ED2 RID: 3794
			public AsyncTaskMethodBuilder<ResultPage?> <>t__builder;

			// Token: 0x04000ED3 RID: 3795
			public int page;

			// Token: 0x04000ED4 RID: 3796
			public Query <>4__this;

			// Token: 0x04000ED5 RID: 3797
			private UGCQueryHandle_t <handle>5__1;

			// Token: 0x04000ED6 RID: 3798
			private SteamUGCQueryCompleted_t? <result>5__2;

			// Token: 0x04000ED7 RID: 3799
			private SteamUGCQueryCompleted_t? <>s__3;

			// Token: 0x04000ED8 RID: 3800
			private CallResult<SteamUGCQueryCompleted_t> <>u__1;
		}
	}
}
