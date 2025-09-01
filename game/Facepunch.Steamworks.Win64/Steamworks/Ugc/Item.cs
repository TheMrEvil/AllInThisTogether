using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C7 RID: 199
	public struct Item
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001369E File Offset: 0x0001189E
		public Item(PublishedFileId id)
		{
			this = default(Item);
			this._id = id;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000136AF File Offset: 0x000118AF
		public PublishedFileId Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000136B7 File Offset: 0x000118B7
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x000136BF File Offset: 0x000118BF
		public string Title
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Title>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Title>k__BackingField = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000136C8 File Offset: 0x000118C8
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x000136D0 File Offset: 0x000118D0
		public string Description
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000136D9 File Offset: 0x000118D9
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x000136E1 File Offset: 0x000118E1
		public string[] Tags
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000136EA File Offset: 0x000118EA
		public AppId CreatorApp
		{
			get
			{
				return this.details.CreatorAppID;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000136F7 File Offset: 0x000118F7
		public AppId ConsumerApp
		{
			get
			{
				return this.details.ConsumerAppID;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00013704 File Offset: 0x00011904
		public Friend Owner
		{
			get
			{
				return new Friend(this.details.SteamIDOwner);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001371B File Offset: 0x0001191B
		public float Score
		{
			get
			{
				return this.details.Score;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00013728 File Offset: 0x00011928
		public DateTime Created
		{
			get
			{
				return Epoch.ToDateTime(this.details.TimeCreated);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001373F File Offset: 0x0001193F
		public DateTime Updated
		{
			get
			{
				return Epoch.ToDateTime(this.details.TimeUpdated);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00013756 File Offset: 0x00011956
		public bool IsPublic
		{
			get
			{
				return this.details.Visibility == RemoteStoragePublishedFileVisibility.Public;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00013766 File Offset: 0x00011966
		public bool IsFriendsOnly
		{
			get
			{
				return this.details.Visibility == RemoteStoragePublishedFileVisibility.FriendsOnly;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00013776 File Offset: 0x00011976
		public bool IsPrivate
		{
			get
			{
				return this.details.Visibility == RemoteStoragePublishedFileVisibility.Private;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00013786 File Offset: 0x00011986
		public bool IsBanned
		{
			get
			{
				return this.details.Banned;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00013793 File Offset: 0x00011993
		public bool IsAcceptedForUse
		{
			get
			{
				return this.details.AcceptedForUse;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000137A0 File Offset: 0x000119A0
		public uint VotesUp
		{
			get
			{
				return this.details.VotesUp;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x000137AD File Offset: 0x000119AD
		public uint VotesDown
		{
			get
			{
				return this.details.VotesDown;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000137BA File Offset: 0x000119BA
		public bool IsInstalled
		{
			get
			{
				return (this.State & ItemState.Installed) == ItemState.Installed;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x000137C7 File Offset: 0x000119C7
		public bool IsDownloading
		{
			get
			{
				return (this.State & ItemState.Downloading) == ItemState.Downloading;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000137D6 File Offset: 0x000119D6
		public bool IsDownloadPending
		{
			get
			{
				return (this.State & ItemState.DownloadPending) == ItemState.DownloadPending;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000137E5 File Offset: 0x000119E5
		public bool IsSubscribed
		{
			get
			{
				return (this.State & ItemState.Subscribed) == ItemState.Subscribed;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000137F2 File Offset: 0x000119F2
		public bool NeedsUpdate
		{
			get
			{
				return (this.State & ItemState.NeedsUpdate) == ItemState.NeedsUpdate;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00013800 File Offset: 0x00011A00
		public string Directory
		{
			get
			{
				ulong num = 0UL;
				uint num2 = 0U;
				string text;
				bool flag = !SteamUGC.Internal.GetItemInstallInfo(this.Id, ref num, out text, ref num2);
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = text;
				}
				return result;
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001383C File Offset: 0x00011A3C
		public bool Download(bool highPriority = false)
		{
			return SteamUGC.Download(this.Id, highPriority);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001385C File Offset: 0x00011A5C
		public long DownloadBytesTotal
		{
			get
			{
				bool flag = !this.NeedsUpdate;
				long result;
				if (flag)
				{
					result = this.SizeBytes;
				}
				else
				{
					ulong num = 0UL;
					ulong num2 = 0UL;
					bool itemDownloadInfo = SteamUGC.Internal.GetItemDownloadInfo(this.Id, ref num, ref num2);
					if (itemDownloadInfo)
					{
						result = (long)num2;
					}
					else
					{
						result = -1L;
					}
				}
				return result;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000138AC File Offset: 0x00011AAC
		public long DownloadBytesDownloaded
		{
			get
			{
				bool flag = !this.NeedsUpdate;
				long result;
				if (flag)
				{
					result = this.SizeBytes;
				}
				else
				{
					ulong num = 0UL;
					ulong num2 = 0UL;
					bool itemDownloadInfo = SteamUGC.Internal.GetItemDownloadInfo(this.Id, ref num, ref num2);
					if (itemDownloadInfo)
					{
						result = (long)num;
					}
					else
					{
						result = -1L;
					}
				}
				return result;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000138FC File Offset: 0x00011AFC
		public long SizeBytes
		{
			get
			{
				bool needsUpdate = this.NeedsUpdate;
				long result;
				if (needsUpdate)
				{
					result = this.DownloadBytesDownloaded;
				}
				else
				{
					ulong num = 0UL;
					uint num2 = 0U;
					string text;
					bool flag = !SteamUGC.Internal.GetItemInstallInfo(this.Id, ref num, out text, ref num2);
					if (flag)
					{
						result = 0L;
					}
					else
					{
						result = (long)num;
					}
				}
				return result;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001394C File Offset: 0x00011B4C
		public float DownloadAmount
		{
			get
			{
				bool flag = !this.IsDownloading;
				float result;
				if (flag)
				{
					result = 1f;
				}
				else
				{
					ulong num = 0UL;
					ulong num2 = 0UL;
					bool flag2 = SteamUGC.Internal.GetItemDownloadInfo(this.Id, ref num, ref num2) && num2 > 0UL;
					if (flag2)
					{
						result = (float)(num / num2);
					}
					else
					{
						bool flag3 = this.NeedsUpdate || !this.IsInstalled || this.IsDownloading;
						if (flag3)
						{
							result = 0f;
						}
						else
						{
							result = 1f;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000139D4 File Offset: 0x00011BD4
		private ItemState State
		{
			get
			{
				return (ItemState)SteamUGC.Internal.GetItemState(this.Id);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000139E8 File Offset: 0x00011BE8
		public static async Task<Item?> GetAsync(PublishedFileId id, int maxageseconds = 1800)
		{
			SteamUGCRequestUGCDetailsResult_t? steamUGCRequestUGCDetailsResult_t = await SteamUGC.Internal.RequestUGCDetails(id, (uint)maxageseconds);
			SteamUGCRequestUGCDetailsResult_t? result = steamUGCRequestUGCDetailsResult_t;
			steamUGCRequestUGCDetailsResult_t = null;
			Item? result2;
			if (result == null)
			{
				result2 = null;
			}
			else
			{
				result2 = new Item?(Item.From(result.Value.Details));
			}
			return result2;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00013A38 File Offset: 0x00011C38
		internal static Item From(SteamUGCDetails_t details)
		{
			return new Item
			{
				_id = details.PublishedFileId,
				details = details,
				Title = details.TitleUTF8(),
				Description = details.DescriptionUTF8(),
				Tags = details.TagsUTF8().ToLower().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries)
			};
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00013AB0 File Offset: 0x00011CB0
		public bool HasTag(string find)
		{
			bool flag = this.Tags.Length == 0;
			return !flag && this.Tags.Contains(find, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00013AE8 File Offset: 0x00011CE8
		public async Task<bool> Subscribe()
		{
			RemoteStorageSubscribePublishedFileResult_t? remoteStorageSubscribePublishedFileResult_t = await SteamUGC.Internal.SubscribeItem(this._id);
			RemoteStorageSubscribePublishedFileResult_t? result = remoteStorageSubscribePublishedFileResult_t;
			remoteStorageSubscribePublishedFileResult_t = null;
			return result != null && result.GetValueOrDefault().Result == Result.OK;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00013B34 File Offset: 0x00011D34
		public async Task<bool> DownloadAsync(Action<float> progress = null, int milisecondsUpdateDelay = 60, CancellationToken ct = default(CancellationToken))
		{
			return await SteamUGC.DownloadAsync(this.Id, progress, milisecondsUpdateDelay, ct);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00013B98 File Offset: 0x00011D98
		public async Task<bool> Unsubscribe()
		{
			RemoteStorageUnsubscribePublishedFileResult_t? remoteStorageUnsubscribePublishedFileResult_t = await SteamUGC.Internal.UnsubscribeItem(this._id);
			RemoteStorageUnsubscribePublishedFileResult_t? result = remoteStorageUnsubscribePublishedFileResult_t;
			remoteStorageUnsubscribePublishedFileResult_t = null;
			return result != null && result.GetValueOrDefault().Result == Result.OK;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00013BE4 File Offset: 0x00011DE4
		public async Task<bool> AddFavorite()
		{
			UserFavoriteItemsListChanged_t? userFavoriteItemsListChanged_t = await SteamUGC.Internal.AddItemToFavorites(this.details.ConsumerAppID, this._id);
			UserFavoriteItemsListChanged_t? result = userFavoriteItemsListChanged_t;
			userFavoriteItemsListChanged_t = null;
			return result != null && result.GetValueOrDefault().Result == Result.OK;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00013C30 File Offset: 0x00011E30
		public async Task<bool> RemoveFavorite()
		{
			UserFavoriteItemsListChanged_t? userFavoriteItemsListChanged_t = await SteamUGC.Internal.RemoveItemFromFavorites(this.details.ConsumerAppID, this._id);
			UserFavoriteItemsListChanged_t? result = userFavoriteItemsListChanged_t;
			userFavoriteItemsListChanged_t = null;
			return result != null && result.GetValueOrDefault().Result == Result.OK;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00013C7C File Offset: 0x00011E7C
		public async Task<Result?> Vote(bool up)
		{
			SetUserItemVoteResult_t? setUserItemVoteResult_t = await SteamUGC.Internal.SetUserItemVote(this.Id, up);
			SetUserItemVoteResult_t? r = setUserItemVoteResult_t;
			setUserItemVoteResult_t = null;
			return (r != null) ? new Result?(r.GetValueOrDefault().Result) : null;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00013CD0 File Offset: 0x00011ED0
		public async Task<UserItemVote?> GetUserVote()
		{
			GetUserItemVoteResult_t? getUserItemVoteResult_t = await SteamUGC.Internal.GetUserItemVote(this._id);
			GetUserItemVoteResult_t? result = getUserItemVoteResult_t;
			getUserItemVoteResult_t = null;
			UserItemVote? result2;
			if (result == null)
			{
				result2 = null;
			}
			else
			{
				result2 = UserItemVote.From(result.Value);
			}
			return result2;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00013D1C File Offset: 0x00011F1C
		public string Url
		{
			get
			{
				return string.Format("http://steamcommunity.com/sharedfiles/filedetails/?source=Facepunch.Steamworks&id={0}", this.Id);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00013D33 File Offset: 0x00011F33
		public string ChangelogUrl
		{
			get
			{
				return string.Format("http://steamcommunity.com/sharedfiles/filedetails/changelog/{0}", this.Id);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00013D4A File Offset: 0x00011F4A
		public string CommentsUrl
		{
			get
			{
				return string.Format("http://steamcommunity.com/sharedfiles/filedetails/comments/{0}", this.Id);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00013D61 File Offset: 0x00011F61
		public string DiscussUrl
		{
			get
			{
				return string.Format("http://steamcommunity.com/sharedfiles/filedetails/discussions/{0}", this.Id);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00013D78 File Offset: 0x00011F78
		public string StatsUrl
		{
			get
			{
				return string.Format("http://steamcommunity.com/sharedfiles/filedetails/stats/{0}", this.Id);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00013D8F File Offset: 0x00011F8F
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00013D97 File Offset: 0x00011F97
		public ulong NumSubscriptions
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumSubscriptions>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumSubscriptions>k__BackingField = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00013DA0 File Offset: 0x00011FA0
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00013DA8 File Offset: 0x00011FA8
		public ulong NumFavorites
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumFavorites>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumFavorites>k__BackingField = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00013DB1 File Offset: 0x00011FB1
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00013DB9 File Offset: 0x00011FB9
		public ulong NumFollowers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumFollowers>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumFollowers>k__BackingField = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00013DC2 File Offset: 0x00011FC2
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00013DCA File Offset: 0x00011FCA
		public ulong NumUniqueSubscriptions
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumUniqueSubscriptions>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumUniqueSubscriptions>k__BackingField = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00013DD3 File Offset: 0x00011FD3
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00013DDB File Offset: 0x00011FDB
		public ulong NumUniqueFavorites
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumUniqueFavorites>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumUniqueFavorites>k__BackingField = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00013DE4 File Offset: 0x00011FE4
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00013DEC File Offset: 0x00011FEC
		public ulong NumUniqueFollowers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumUniqueFollowers>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumUniqueFollowers>k__BackingField = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00013DF5 File Offset: 0x00011FF5
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00013DFD File Offset: 0x00011FFD
		public ulong NumUniqueWebsiteViews
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumUniqueWebsiteViews>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumUniqueWebsiteViews>k__BackingField = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00013E06 File Offset: 0x00012006
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00013E0E File Offset: 0x0001200E
		public ulong ReportScore
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<ReportScore>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ReportScore>k__BackingField = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00013E17 File Offset: 0x00012017
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00013E1F File Offset: 0x0001201F
		public ulong NumSecondsPlayed
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumSecondsPlayed>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumSecondsPlayed>k__BackingField = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00013E28 File Offset: 0x00012028
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00013E30 File Offset: 0x00012030
		public ulong NumPlaytimeSessions
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumPlaytimeSessions>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumPlaytimeSessions>k__BackingField = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00013E39 File Offset: 0x00012039
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00013E41 File Offset: 0x00012041
		public ulong NumComments
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumComments>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumComments>k__BackingField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00013E4A File Offset: 0x0001204A
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00013E52 File Offset: 0x00012052
		public ulong NumSecondsPlayedDuringTimePeriod
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumSecondsPlayedDuringTimePeriod>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumSecondsPlayedDuringTimePeriod>k__BackingField = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00013E5B File Offset: 0x0001205B
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00013E63 File Offset: 0x00012063
		public ulong NumPlaytimeSessionsDuringTimePeriod
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<NumPlaytimeSessionsDuringTimePeriod>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumPlaytimeSessionsDuringTimePeriod>k__BackingField = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00013E6C File Offset: 0x0001206C
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00013E74 File Offset: 0x00012074
		public string PreviewImageUrl
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<PreviewImageUrl>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PreviewImageUrl>k__BackingField = value;
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00013E80 File Offset: 0x00012080
		public Editor Edit()
		{
			return new Editor(this.Id);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00013E9D File Offset: 0x0001209D
		public Result Result
		{
			get
			{
				return this.details.Result;
			}
		}

		// Token: 0x04000799 RID: 1945
		internal SteamUGCDetails_t details;

		// Token: 0x0400079A RID: 1946
		internal PublishedFileId _id;

		// Token: 0x0400079B RID: 1947
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Title>k__BackingField;

		// Token: 0x0400079C RID: 1948
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Description>k__BackingField;

		// Token: 0x0400079D RID: 1949
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string[] <Tags>k__BackingField;

		// Token: 0x0400079E RID: 1950
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumSubscriptions>k__BackingField;

		// Token: 0x0400079F RID: 1951
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumFavorites>k__BackingField;

		// Token: 0x040007A0 RID: 1952
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumFollowers>k__BackingField;

		// Token: 0x040007A1 RID: 1953
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumUniqueSubscriptions>k__BackingField;

		// Token: 0x040007A2 RID: 1954
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumUniqueFavorites>k__BackingField;

		// Token: 0x040007A3 RID: 1955
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumUniqueFollowers>k__BackingField;

		// Token: 0x040007A4 RID: 1956
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumUniqueWebsiteViews>k__BackingField;

		// Token: 0x040007A5 RID: 1957
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <ReportScore>k__BackingField;

		// Token: 0x040007A6 RID: 1958
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumSecondsPlayed>k__BackingField;

		// Token: 0x040007A7 RID: 1959
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumPlaytimeSessions>k__BackingField;

		// Token: 0x040007A8 RID: 1960
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumComments>k__BackingField;

		// Token: 0x040007A9 RID: 1961
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumSecondsPlayedDuringTimePeriod>k__BackingField;

		// Token: 0x040007AA RID: 1962
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <NumPlaytimeSessionsDuringTimePeriod>k__BackingField;

		// Token: 0x040007AB RID: 1963
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <PreviewImageUrl>k__BackingField;

		// Token: 0x02000274 RID: 628
		[CompilerGenerated]
		private sealed class <GetAsync>d__66 : IAsyncStateMachine
		{
			// Token: 0x06001210 RID: 4624 RVA: 0x000228DE File Offset: 0x00020ADE
			public <GetAsync>d__66()
			{
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x000228E8 File Offset: 0x00020AE8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Item? result2;
				try
				{
					CallResult<SteamUGCRequestUGCDetailsResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.RequestUGCDetails(id, (uint)maxageseconds).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<SteamUGCRequestUGCDetailsResult_t> callResult2 = callResult;
							Item.<GetAsync>d__66 <GetAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<SteamUGCRequestUGCDetailsResult_t>, Item.<GetAsync>d__66>(ref callResult, ref <GetAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<SteamUGCRequestUGCDetailsResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<SteamUGCRequestUGCDetailsResult_t>);
						num2 = -1;
					}
					steamUGCRequestUGCDetailsResult_t = callResult.GetResult();
					result = steamUGCRequestUGCDetailsResult_t;
					steamUGCRequestUGCDetailsResult_t = null;
					bool flag = result == null;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						result2 = new Item?(Item.From(result.Value.Details));
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

			// Token: 0x06001212 RID: 4626 RVA: 0x00022A10 File Offset: 0x00020C10
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E9D RID: 3741
			public int <>1__state;

			// Token: 0x04000E9E RID: 3742
			public AsyncTaskMethodBuilder<Item?> <>t__builder;

			// Token: 0x04000E9F RID: 3743
			public PublishedFileId id;

			// Token: 0x04000EA0 RID: 3744
			public int maxageseconds;

			// Token: 0x04000EA1 RID: 3745
			private SteamUGCRequestUGCDetailsResult_t? <result>5__1;

			// Token: 0x04000EA2 RID: 3746
			private SteamUGCRequestUGCDetailsResult_t? <>s__2;

			// Token: 0x04000EA3 RID: 3747
			private CallResult<SteamUGCRequestUGCDetailsResult_t> <>u__1;
		}

		// Token: 0x02000275 RID: 629
		[CompilerGenerated]
		private sealed class <Subscribe>d__69 : IAsyncStateMachine
		{
			// Token: 0x06001213 RID: 4627 RVA: 0x00022A12 File Offset: 0x00020C12
			public <Subscribe>d__69()
			{
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x00022A1C File Offset: 0x00020C1C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<RemoteStorageSubscribePublishedFileResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.SubscribeItem(this._id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<RemoteStorageSubscribePublishedFileResult_t> callResult2 = callResult;
							Item.<Subscribe>d__69 <Subscribe>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<RemoteStorageSubscribePublishedFileResult_t>, Item.<Subscribe>d__69>(ref callResult, ref <Subscribe>d__);
							return;
						}
					}
					else
					{
						CallResult<RemoteStorageSubscribePublishedFileResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<RemoteStorageSubscribePublishedFileResult_t>);
						num2 = -1;
					}
					remoteStorageSubscribePublishedFileResult_t = callResult.GetResult();
					result = remoteStorageSubscribePublishedFileResult_t;
					remoteStorageSubscribePublishedFileResult_t = null;
					result2 = (result != null && result.GetValueOrDefault().Result == Result.OK);
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

			// Token: 0x06001215 RID: 4629 RVA: 0x00022B2C File Offset: 0x00020D2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EA4 RID: 3748
			public int <>1__state;

			// Token: 0x04000EA5 RID: 3749
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EA6 RID: 3750
			public Item <>4__this;

			// Token: 0x04000EA7 RID: 3751
			private RemoteStorageSubscribePublishedFileResult_t? <result>5__1;

			// Token: 0x04000EA8 RID: 3752
			private RemoteStorageSubscribePublishedFileResult_t? <>s__2;

			// Token: 0x04000EA9 RID: 3753
			private CallResult<RemoteStorageSubscribePublishedFileResult_t> <>u__1;
		}

		// Token: 0x02000276 RID: 630
		[CompilerGenerated]
		private sealed class <DownloadAsync>d__70 : IAsyncStateMachine
		{
			// Token: 0x06001216 RID: 4630 RVA: 0x00022B2E File Offset: 0x00020D2E
			public <DownloadAsync>d__70()
			{
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x00022B38 File Offset: 0x00020D38
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					TaskAwaiter<bool> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamUGC.DownloadAsync(base.Id, progress, milisecondsUpdateDelay, ct).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
							Item.<DownloadAsync>d__70 <DownloadAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, Item.<DownloadAsync>d__70>(ref taskAwaiter, ref <DownloadAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
						num2 = -1;
					}
					bool result = taskAwaiter.GetResult();
					result2 = result;
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

			// Token: 0x06001218 RID: 4632 RVA: 0x00022C1C File Offset: 0x00020E1C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EAA RID: 3754
			public int <>1__state;

			// Token: 0x04000EAB RID: 3755
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EAC RID: 3756
			public Action<float> progress;

			// Token: 0x04000EAD RID: 3757
			public int milisecondsUpdateDelay;

			// Token: 0x04000EAE RID: 3758
			public CancellationToken ct;

			// Token: 0x04000EAF RID: 3759
			public Item <>4__this;

			// Token: 0x04000EB0 RID: 3760
			private bool <>s__1;

			// Token: 0x04000EB1 RID: 3761
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x02000277 RID: 631
		[CompilerGenerated]
		private sealed class <Unsubscribe>d__71 : IAsyncStateMachine
		{
			// Token: 0x06001219 RID: 4633 RVA: 0x00022C1E File Offset: 0x00020E1E
			public <Unsubscribe>d__71()
			{
			}

			// Token: 0x0600121A RID: 4634 RVA: 0x00022C28 File Offset: 0x00020E28
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<RemoteStorageUnsubscribePublishedFileResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.UnsubscribeItem(this._id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<RemoteStorageUnsubscribePublishedFileResult_t> callResult2 = callResult;
							Item.<Unsubscribe>d__71 <Unsubscribe>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<RemoteStorageUnsubscribePublishedFileResult_t>, Item.<Unsubscribe>d__71>(ref callResult, ref <Unsubscribe>d__);
							return;
						}
					}
					else
					{
						CallResult<RemoteStorageUnsubscribePublishedFileResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<RemoteStorageUnsubscribePublishedFileResult_t>);
						num2 = -1;
					}
					remoteStorageUnsubscribePublishedFileResult_t = callResult.GetResult();
					result = remoteStorageUnsubscribePublishedFileResult_t;
					remoteStorageUnsubscribePublishedFileResult_t = null;
					result2 = (result != null && result.GetValueOrDefault().Result == Result.OK);
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

			// Token: 0x0600121B RID: 4635 RVA: 0x00022D38 File Offset: 0x00020F38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EB2 RID: 3762
			public int <>1__state;

			// Token: 0x04000EB3 RID: 3763
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EB4 RID: 3764
			public Item <>4__this;

			// Token: 0x04000EB5 RID: 3765
			private RemoteStorageUnsubscribePublishedFileResult_t? <result>5__1;

			// Token: 0x04000EB6 RID: 3766
			private RemoteStorageUnsubscribePublishedFileResult_t? <>s__2;

			// Token: 0x04000EB7 RID: 3767
			private CallResult<RemoteStorageUnsubscribePublishedFileResult_t> <>u__1;
		}

		// Token: 0x02000278 RID: 632
		[CompilerGenerated]
		private sealed class <AddFavorite>d__72 : IAsyncStateMachine
		{
			// Token: 0x0600121C RID: 4636 RVA: 0x00022D3A File Offset: 0x00020F3A
			public <AddFavorite>d__72()
			{
			}

			// Token: 0x0600121D RID: 4637 RVA: 0x00022D44 File Offset: 0x00020F44
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<UserFavoriteItemsListChanged_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.AddItemToFavorites(this.details.ConsumerAppID, this._id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<UserFavoriteItemsListChanged_t> callResult2 = callResult;
							Item.<AddFavorite>d__72 <AddFavorite>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<UserFavoriteItemsListChanged_t>, Item.<AddFavorite>d__72>(ref callResult, ref <AddFavorite>d__);
							return;
						}
					}
					else
					{
						CallResult<UserFavoriteItemsListChanged_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<UserFavoriteItemsListChanged_t>);
						num2 = -1;
					}
					userFavoriteItemsListChanged_t = callResult.GetResult();
					result = userFavoriteItemsListChanged_t;
					userFavoriteItemsListChanged_t = null;
					result2 = (result != null && result.GetValueOrDefault().Result == Result.OK);
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

			// Token: 0x0600121E RID: 4638 RVA: 0x00022E64 File Offset: 0x00021064
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EB8 RID: 3768
			public int <>1__state;

			// Token: 0x04000EB9 RID: 3769
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EBA RID: 3770
			public Item <>4__this;

			// Token: 0x04000EBB RID: 3771
			private UserFavoriteItemsListChanged_t? <result>5__1;

			// Token: 0x04000EBC RID: 3772
			private UserFavoriteItemsListChanged_t? <>s__2;

			// Token: 0x04000EBD RID: 3773
			private CallResult<UserFavoriteItemsListChanged_t> <>u__1;
		}

		// Token: 0x02000279 RID: 633
		[CompilerGenerated]
		private sealed class <RemoveFavorite>d__73 : IAsyncStateMachine
		{
			// Token: 0x0600121F RID: 4639 RVA: 0x00022E66 File Offset: 0x00021066
			public <RemoveFavorite>d__73()
			{
			}

			// Token: 0x06001220 RID: 4640 RVA: 0x00022E70 File Offset: 0x00021070
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<UserFavoriteItemsListChanged_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.RemoveItemFromFavorites(this.details.ConsumerAppID, this._id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<UserFavoriteItemsListChanged_t> callResult2 = callResult;
							Item.<RemoveFavorite>d__73 <RemoveFavorite>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<UserFavoriteItemsListChanged_t>, Item.<RemoveFavorite>d__73>(ref callResult, ref <RemoveFavorite>d__);
							return;
						}
					}
					else
					{
						CallResult<UserFavoriteItemsListChanged_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<UserFavoriteItemsListChanged_t>);
						num2 = -1;
					}
					userFavoriteItemsListChanged_t = callResult.GetResult();
					result = userFavoriteItemsListChanged_t;
					userFavoriteItemsListChanged_t = null;
					result2 = (result != null && result.GetValueOrDefault().Result == Result.OK);
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

			// Token: 0x06001221 RID: 4641 RVA: 0x00022F90 File Offset: 0x00021190
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EBE RID: 3774
			public int <>1__state;

			// Token: 0x04000EBF RID: 3775
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EC0 RID: 3776
			public Item <>4__this;

			// Token: 0x04000EC1 RID: 3777
			private UserFavoriteItemsListChanged_t? <result>5__1;

			// Token: 0x04000EC2 RID: 3778
			private UserFavoriteItemsListChanged_t? <>s__2;

			// Token: 0x04000EC3 RID: 3779
			private CallResult<UserFavoriteItemsListChanged_t> <>u__1;
		}

		// Token: 0x0200027A RID: 634
		[CompilerGenerated]
		private sealed class <Vote>d__74 : IAsyncStateMachine
		{
			// Token: 0x06001222 RID: 4642 RVA: 0x00022F92 File Offset: 0x00021192
			public <Vote>d__74()
			{
			}

			// Token: 0x06001223 RID: 4643 RVA: 0x00022F9C File Offset: 0x0002119C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Result? result;
				try
				{
					CallResult<SetUserItemVoteResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.SetUserItemVote(base.Id, up).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<SetUserItemVoteResult_t> callResult2 = callResult;
							Item.<Vote>d__74 <Vote>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<SetUserItemVoteResult_t>, Item.<Vote>d__74>(ref callResult, ref <Vote>d__);
							return;
						}
					}
					else
					{
						CallResult<SetUserItemVoteResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<SetUserItemVoteResult_t>);
						num2 = -1;
					}
					setUserItemVoteResult_t = callResult.GetResult();
					r = setUserItemVoteResult_t;
					setUserItemVoteResult_t = null;
					result = ((r != null) ? new Result?(r.GetValueOrDefault().Result) : null);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001224 RID: 4644 RVA: 0x000230BC File Offset: 0x000212BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EC4 RID: 3780
			public int <>1__state;

			// Token: 0x04000EC5 RID: 3781
			public AsyncTaskMethodBuilder<Result?> <>t__builder;

			// Token: 0x04000EC6 RID: 3782
			public bool up;

			// Token: 0x04000EC7 RID: 3783
			public Item <>4__this;

			// Token: 0x04000EC8 RID: 3784
			private SetUserItemVoteResult_t? <r>5__1;

			// Token: 0x04000EC9 RID: 3785
			private SetUserItemVoteResult_t? <>s__2;

			// Token: 0x04000ECA RID: 3786
			private CallResult<SetUserItemVoteResult_t> <>u__1;
		}

		// Token: 0x0200027B RID: 635
		[CompilerGenerated]
		private sealed class <GetUserVote>d__75 : IAsyncStateMachine
		{
			// Token: 0x06001225 RID: 4645 RVA: 0x000230BE File Offset: 0x000212BE
			public <GetUserVote>d__75()
			{
			}

			// Token: 0x06001226 RID: 4646 RVA: 0x000230C8 File Offset: 0x000212C8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				UserItemVote? result2;
				try
				{
					CallResult<GetUserItemVoteResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUGC.Internal.GetUserItemVote(this._id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<GetUserItemVoteResult_t> callResult2 = callResult;
							Item.<GetUserVote>d__75 <GetUserVote>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GetUserItemVoteResult_t>, Item.<GetUserVote>d__75>(ref callResult, ref <GetUserVote>d__);
							return;
						}
					}
					else
					{
						CallResult<GetUserItemVoteResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<GetUserItemVoteResult_t>);
						num2 = -1;
					}
					getUserItemVoteResult_t = callResult.GetResult();
					result = getUserItemVoteResult_t;
					getUserItemVoteResult_t = null;
					bool flag = result == null;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						result2 = UserItemVote.From(result.Value);
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

			// Token: 0x06001227 RID: 4647 RVA: 0x000231E4 File Offset: 0x000213E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000ECB RID: 3787
			public int <>1__state;

			// Token: 0x04000ECC RID: 3788
			public AsyncTaskMethodBuilder<UserItemVote?> <>t__builder;

			// Token: 0x04000ECD RID: 3789
			public Item <>4__this;

			// Token: 0x04000ECE RID: 3790
			private GetUserItemVoteResult_t? <result>5__1;

			// Token: 0x04000ECF RID: 3791
			private GetUserItemVoteResult_t? <>s__2;

			// Token: 0x04000ED0 RID: 3792
			private CallResult<GetUserItemVoteResult_t> <>u__1;
		}
	}
}
