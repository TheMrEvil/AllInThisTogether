using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C5 RID: 197
	public struct Editor
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x000133FB File Offset: 0x000115FB
		internal Editor(WorkshopFileType filetype)
		{
			this = default(Editor);
			this.creatingNew = true;
			this.creatingType = filetype;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00013413 File Offset: 0x00011613
		public Editor(PublishedFileId fileId)
		{
			this = default(Editor);
			this.fileId = fileId;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00013424 File Offset: 0x00011624
		public static Editor NewCommunityFile
		{
			get
			{
				return new Editor(WorkshopFileType.First);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001342C File Offset: 0x0001162C
		public static Editor NewMicrotransactionFile
		{
			get
			{
				return new Editor(WorkshopFileType.Microtransaction);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00013434 File Offset: 0x00011634
		public Editor ForAppId(AppId id)
		{
			this.consumerAppId = id;
			return this;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00013454 File Offset: 0x00011654
		public Editor WithTitle(string t)
		{
			this.Title = t;
			return this;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00013474 File Offset: 0x00011674
		public Editor WithDescription(string t)
		{
			this.Description = t;
			return this;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00013494 File Offset: 0x00011694
		public Editor WithMetaData(string t)
		{
			this.MetaData = t;
			return this;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000134B4 File Offset: 0x000116B4
		public Editor WithChangeLog(string t)
		{
			this.ChangeLog = t;
			return this;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000134D4 File Offset: 0x000116D4
		public Editor InLanguage(string t)
		{
			this.Language = t;
			return this;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000134F4 File Offset: 0x000116F4
		public Editor WithPreviewFile(string t)
		{
			this.PreviewFile = t;
			return this;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00013514 File Offset: 0x00011714
		public Editor WithContent(DirectoryInfo t)
		{
			this.ContentFolder = t;
			return this;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00013534 File Offset: 0x00011734
		public Editor WithContent(string folderName)
		{
			return this.WithContent(new DirectoryInfo(folderName));
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00013554 File Offset: 0x00011754
		public Editor WithPublicVisibility()
		{
			this.Visibility = new RemoteStoragePublishedFileVisibility?(RemoteStoragePublishedFileVisibility.Public);
			return this;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00013578 File Offset: 0x00011778
		public Editor WithFriendsOnlyVisibility()
		{
			this.Visibility = new RemoteStoragePublishedFileVisibility?(RemoteStoragePublishedFileVisibility.FriendsOnly);
			return this;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001359C File Offset: 0x0001179C
		public Editor WithPrivateVisibility()
		{
			this.Visibility = new RemoteStoragePublishedFileVisibility?(RemoteStoragePublishedFileVisibility.Private);
			return this;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000135C0 File Offset: 0x000117C0
		public Editor WithTag(string tag)
		{
			bool flag = this.Tags == null;
			if (flag)
			{
				this.Tags = new List<string>();
			}
			this.Tags.Add(tag);
			return this;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00013600 File Offset: 0x00011800
		public Editor AddKeyValueTag(string key, string value)
		{
			bool flag = this.KeyValueTags == null;
			if (flag)
			{
				this.KeyValueTags = new Dictionary<string, string>();
			}
			this.KeyValueTags.Add(key, value);
			return this;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00013640 File Offset: 0x00011840
		public async Task<PublishResult> SubmitAsync(IProgress<float> progress = null)
		{
			PublishResult result = default(PublishResult);
			if (progress != null)
			{
				progress.Report(0f);
			}
			bool flag = this.consumerAppId == 0U;
			if (flag)
			{
				this.consumerAppId = SteamClient.AppId;
			}
			bool flag2 = this.creatingNew;
			if (flag2)
			{
				result.Result = Result.Fail;
				CreateItemResult_t? createItemResult_t = await SteamUGC.Internal.CreateItem(this.consumerAppId, this.creatingType);
				CreateItemResult_t? created = createItemResult_t;
				createItemResult_t = null;
				if (created == null)
				{
					return result;
				}
				result.Result = created.Value.Result;
				if (result.Result != Result.OK)
				{
					return result;
				}
				this.fileId = created.Value.PublishedFileId;
				result.NeedsWorkshopAgreement = created.Value.UserNeedsToAcceptWorkshopLegalAgreement;
				result.FileId = this.fileId;
				created = null;
			}
			result.FileId = this.fileId;
			UGCUpdateHandle_t handle = SteamUGC.Internal.StartItemUpdate(this.consumerAppId, this.fileId);
			PublishResult result2;
			if (handle == 18446744073709551615UL)
			{
				result2 = result;
			}
			else
			{
				if (this.Title != null)
				{
					SteamUGC.Internal.SetItemTitle(handle, this.Title);
				}
				if (this.Description != null)
				{
					SteamUGC.Internal.SetItemDescription(handle, this.Description);
				}
				if (this.MetaData != null)
				{
					SteamUGC.Internal.SetItemMetadata(handle, this.MetaData);
				}
				if (this.Language != null)
				{
					SteamUGC.Internal.SetItemUpdateLanguage(handle, this.Language);
				}
				if (this.ContentFolder != null)
				{
					SteamUGC.Internal.SetItemContent(handle, this.ContentFolder.FullName);
				}
				if (this.PreviewFile != null)
				{
					SteamUGC.Internal.SetItemPreview(handle, this.PreviewFile);
				}
				if (this.Visibility != null)
				{
					SteamUGC.Internal.SetItemVisibility(handle, this.Visibility.Value);
				}
				if (this.Tags != null && this.Tags.Count > 0)
				{
					using (SteamParamStringArray a = SteamParamStringArray.From(this.Tags.ToArray()))
					{
						SteamParamStringArray_t val = a.Value;
						SteamUGC.Internal.SetItemTags(handle, ref val);
					}
					SteamParamStringArray a = default(SteamParamStringArray);
				}
				if (this.KeyValueTags != null && this.KeyValueTags.Count > 0)
				{
					foreach (KeyValuePair<string, string> keyValueTag in this.KeyValueTags)
					{
						SteamUGC.Internal.AddItemKeyValueTag(handle, keyValueTag.Key, keyValueTag.Value);
						keyValueTag = default(KeyValuePair<string, string>);
					}
					Dictionary<string, string>.Enumerator enumerator = default(Dictionary<string, string>.Enumerator);
				}
				result.Result = Result.Fail;
				if (this.ChangeLog == null)
				{
					this.ChangeLog = "";
				}
				CallResult<SubmitItemUpdateResult_t> updating = SteamUGC.Internal.SubmitItemUpdate(handle, this.ChangeLog);
				while (!updating.IsCompleted)
				{
					if (progress != null)
					{
						ulong total = 0UL;
						ulong processed = 0UL;
						switch (SteamUGC.Internal.GetItemUpdateProgress(handle, ref processed, ref total))
						{
						case ItemUpdateStatus.PreparingConfig:
							if (progress != null)
							{
								progress.Report(0.1f);
							}
							break;
						case ItemUpdateStatus.PreparingContent:
							if (progress != null)
							{
								progress.Report(0.2f);
							}
							break;
						case ItemUpdateStatus.UploadingContent:
						{
							float uploaded = (total > 0UL) ? (processed / total) : 0f;
							if (progress != null)
							{
								progress.Report(0.2f + uploaded * 0.7f);
							}
							break;
						}
						case ItemUpdateStatus.UploadingPreviewFile:
							if (progress != null)
							{
								progress.Report(0.8f);
							}
							break;
						case ItemUpdateStatus.CommittingChanges:
							if (progress != null)
							{
								progress.Report(1f);
							}
							break;
						}
					}
					await Task.Delay(16);
				}
				if (progress != null)
				{
					progress.Report(1f);
				}
				SubmitItemUpdateResult_t? updated = updating.GetResult();
				if (updated == null)
				{
					result2 = result;
				}
				else
				{
					result.Result = updated.Value.Result;
					if (result.Result != Result.OK)
					{
						result2 = result;
					}
					else
					{
						result.NeedsWorkshopAgreement = updated.Value.UserNeedsToAcceptWorkshopLegalAgreement;
						result.FileId = this.fileId;
						updating = default(CallResult<SubmitItemUpdateResult_t>);
						updated = null;
						result2 = result;
					}
				}
			}
			return result2;
		}

		// Token: 0x04000788 RID: 1928
		private PublishedFileId fileId;

		// Token: 0x04000789 RID: 1929
		private bool creatingNew;

		// Token: 0x0400078A RID: 1930
		private WorkshopFileType creatingType;

		// Token: 0x0400078B RID: 1931
		private AppId consumerAppId;

		// Token: 0x0400078C RID: 1932
		private string Title;

		// Token: 0x0400078D RID: 1933
		private string Description;

		// Token: 0x0400078E RID: 1934
		private string MetaData;

		// Token: 0x0400078F RID: 1935
		private string ChangeLog;

		// Token: 0x04000790 RID: 1936
		private string Language;

		// Token: 0x04000791 RID: 1937
		private string PreviewFile;

		// Token: 0x04000792 RID: 1938
		private DirectoryInfo ContentFolder;

		// Token: 0x04000793 RID: 1939
		private RemoteStoragePublishedFileVisibility? Visibility;

		// Token: 0x04000794 RID: 1940
		private List<string> Tags;

		// Token: 0x04000795 RID: 1941
		private Dictionary<string, string> KeyValueTags;

		// Token: 0x02000273 RID: 627
		[CompilerGenerated]
		private sealed class <SubmitAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x0600120D RID: 4621 RVA: 0x00022076 File Offset: 0x00020276
			public <SubmitAsync>d__34()
			{
			}

			// Token: 0x0600120E RID: 4622 RVA: 0x00022080 File Offset: 0x00020280
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				PublishResult result2;
				try
				{
					TaskAwaiter taskAwaiter;
					IProgress<float> progress;
					CallResult<CreateItemResult_t> callResult;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							num = (num2 = -1);
							goto IL_6DE;
						}
						result = default(PublishResult);
						progress = progress;
						if (progress != null)
						{
							progress.Report(0f);
						}
						bool flag = this.consumerAppId == 0U;
						if (flag)
						{
							this.consumerAppId = SteamClient.AppId;
						}
						bool creatingNew = this.creatingNew;
						if (!creatingNew)
						{
							goto IL_1DC;
						}
						result.Result = Result.Fail;
						callResult = SteamUGC.Internal.CreateItem(this.consumerAppId, this.creatingType).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num = (num2 = 0);
							CallResult<CreateItemResult_t> callResult2 = callResult;
							Editor.<SubmitAsync>d__34 <SubmitAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<CreateItemResult_t>, Editor.<SubmitAsync>d__34>(ref callResult, ref <SubmitAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<CreateItemResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<CreateItemResult_t>);
						num = (num2 = -1);
					}
					createItemResult_t = callResult.GetResult();
					created = createItemResult_t;
					createItemResult_t = null;
					bool flag2 = created == null;
					if (flag2)
					{
						result2 = result;
						goto IL_7EE;
					}
					result.Result = created.Value.Result;
					bool flag3 = result.Result != Result.OK;
					if (flag3)
					{
						result2 = result;
						goto IL_7EE;
					}
					this.fileId = created.Value.PublishedFileId;
					result.NeedsWorkshopAgreement = created.Value.UserNeedsToAcceptWorkshopLegalAgreement;
					result.FileId = this.fileId;
					created = null;
					IL_1DC:
					result.FileId = this.fileId;
					handle = SteamUGC.Internal.StartItemUpdate(this.consumerAppId, this.fileId);
					bool flag4 = handle == ulong.MaxValue;
					if (flag4)
					{
						result2 = result;
						goto IL_7EE;
					}
					bool flag5 = this.Title != null;
					if (flag5)
					{
						SteamUGC.Internal.SetItemTitle(handle, this.Title);
					}
					bool flag6 = this.Description != null;
					if (flag6)
					{
						SteamUGC.Internal.SetItemDescription(handle, this.Description);
					}
					bool flag7 = this.MetaData != null;
					if (flag7)
					{
						SteamUGC.Internal.SetItemMetadata(handle, this.MetaData);
					}
					bool flag8 = this.Language != null;
					if (flag8)
					{
						SteamUGC.Internal.SetItemUpdateLanguage(handle, this.Language);
					}
					bool flag9 = this.ContentFolder != null;
					if (flag9)
					{
						SteamUGC.Internal.SetItemContent(handle, this.ContentFolder.FullName);
					}
					bool flag10 = this.PreviewFile != null;
					if (flag10)
					{
						SteamUGC.Internal.SetItemPreview(handle, this.PreviewFile);
					}
					bool flag11 = this.Visibility != null;
					if (flag11)
					{
						SteamUGC.Internal.SetItemVisibility(handle, this.Visibility.Value);
					}
					bool flag12 = this.Tags != null && this.Tags.Count > 0;
					if (flag12)
					{
						a = SteamParamStringArray.From(this.Tags.ToArray());
						try
						{
							val = a.Value;
							SteamUGC.Internal.SetItemTags(handle, ref val);
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)a).Dispose();
							}
						}
						a = default(SteamParamStringArray);
					}
					bool flag13 = this.KeyValueTags != null && this.KeyValueTags.Count > 0;
					if (flag13)
					{
						enumerator = this.KeyValueTags.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								keyValueTag = enumerator.Current;
								SteamUGC.Internal.AddItemKeyValueTag(handle, keyValueTag.Key, keyValueTag.Value);
								keyValueTag = default(KeyValuePair<string, string>);
							}
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)enumerator).Dispose();
							}
						}
						enumerator = default(Dictionary<string, string>.Enumerator);
					}
					result.Result = Result.Fail;
					bool flag14 = this.ChangeLog == null;
					if (flag14)
					{
						this.ChangeLog = "";
					}
					updating = SteamUGC.Internal.SubmitItemUpdate(handle, this.ChangeLog);
					goto IL_6E7;
					IL_6DE:
					taskAwaiter.GetResult();
					IL_6E7:
					if (updating.IsCompleted)
					{
						IProgress<float> progress2 = progress;
						if (progress2 != null)
						{
							progress2.Report(1f);
						}
						updated = updating.GetResult();
						bool flag15 = updated == null;
						if (flag15)
						{
							result2 = result;
						}
						else
						{
							result.Result = updated.Value.Result;
							bool flag16 = result.Result != Result.OK;
							if (flag16)
							{
								result2 = result;
							}
							else
							{
								result.NeedsWorkshopAgreement = updated.Value.UserNeedsToAcceptWorkshopLegalAgreement;
								result.FileId = this.fileId;
								updating = default(CallResult<SubmitItemUpdateResult_t>);
								updated = null;
								result2 = result;
							}
						}
					}
					else
					{
						bool flag17 = progress != null;
						if (flag17)
						{
							total = 0UL;
							processed = 0UL;
							ItemUpdateStatus r = SteamUGC.Internal.GetItemUpdateProgress(handle, ref processed, ref total);
							ItemUpdateStatus itemUpdateStatus = r;
							ItemUpdateStatus itemUpdateStatus2 = itemUpdateStatus;
							switch (itemUpdateStatus2)
							{
							case ItemUpdateStatus.PreparingConfig:
							{
								IProgress<float> progress3 = progress;
								if (progress3 != null)
								{
									progress3.Report(0.1f);
								}
								break;
							}
							case ItemUpdateStatus.PreparingContent:
							{
								IProgress<float> progress4 = progress;
								if (progress4 != null)
								{
									progress4.Report(0.2f);
								}
								break;
							}
							case ItemUpdateStatus.UploadingContent:
							{
								uploaded = ((total > 0UL) ? (processed / total) : 0f);
								IProgress<float> progress5 = progress;
								if (progress5 != null)
								{
									progress5.Report(0.2f + uploaded * 0.7f);
								}
								break;
							}
							case ItemUpdateStatus.UploadingPreviewFile:
							{
								IProgress<float> progress6 = progress;
								if (progress6 != null)
								{
									progress6.Report(0.8f);
								}
								break;
							}
							case ItemUpdateStatus.CommittingChanges:
							{
								IProgress<float> progress7 = progress;
								if (progress7 != null)
								{
									progress7.Report(1f);
								}
								break;
							}
							}
						}
						taskAwaiter = Task.Delay(16).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = (num2 = 1);
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							Editor.<SubmitAsync>d__34 <SubmitAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Editor.<SubmitAsync>d__34>(ref taskAwaiter, ref <SubmitAsync>d__);
							return;
						}
						goto IL_6DE;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_7EE:
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x000228DC File Offset: 0x00020ADC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E88 RID: 3720
			public int <>1__state;

			// Token: 0x04000E89 RID: 3721
			public AsyncTaskMethodBuilder<PublishResult> <>t__builder;

			// Token: 0x04000E8A RID: 3722
			public IProgress<float> progress;

			// Token: 0x04000E8B RID: 3723
			public Editor <>4__this;

			// Token: 0x04000E8C RID: 3724
			private PublishResult <result>5__1;

			// Token: 0x04000E8D RID: 3725
			private CreateItemResult_t? <created>5__2;

			// Token: 0x04000E8E RID: 3726
			private CreateItemResult_t? <>s__3;

			// Token: 0x04000E8F RID: 3727
			private UGCUpdateHandle_t <handle>5__4;

			// Token: 0x04000E90 RID: 3728
			private CallResult<SubmitItemUpdateResult_t> <updating>5__5;

			// Token: 0x04000E91 RID: 3729
			private SubmitItemUpdateResult_t? <updated>5__6;

			// Token: 0x04000E92 RID: 3730
			private SteamParamStringArray <a>5__7;

			// Token: 0x04000E93 RID: 3731
			private SteamParamStringArray_t <val>5__8;

			// Token: 0x04000E94 RID: 3732
			private Dictionary<string, string>.Enumerator <>s__9;

			// Token: 0x04000E95 RID: 3733
			private KeyValuePair<string, string> <keyValueTag>5__10;

			// Token: 0x04000E96 RID: 3734
			private ulong <total>5__11;

			// Token: 0x04000E97 RID: 3735
			private ulong <processed>5__12;

			// Token: 0x04000E98 RID: 3736
			private ItemUpdateStatus <r>5__13;

			// Token: 0x04000E99 RID: 3737
			private ItemUpdateStatus <>s__14;

			// Token: 0x04000E9A RID: 3738
			private float <uploaded>5__15;

			// Token: 0x04000E9B RID: 3739
			private CallResult<CreateItemResult_t> <>u__1;

			// Token: 0x04000E9C RID: 3740
			private TaskAwaiter <>u__2;
		}
	}
}
