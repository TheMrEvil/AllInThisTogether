using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Installations;
using Parse.Abstractions.Platform.Users;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Execution
{
	// Token: 0x02000061 RID: 97
	public class ParseCommandRunner : IParseCommandRunner
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000F508 File Offset: 0x0000D708
		private IWebClient WebClient
		{
			[CompilerGenerated]
			get
			{
				return this.<WebClient>k__BackingField;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000F510 File Offset: 0x0000D710
		private IParseInstallationController InstallationController
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationController>k__BackingField;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000F518 File Offset: 0x0000D718
		private IMetadataController MetadataController
		{
			[CompilerGenerated]
			get
			{
				return this.<MetadataController>k__BackingField;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000F520 File Offset: 0x0000D720
		private IServerConnectionData ServerConnectionData
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerConnectionData>k__BackingField;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000F528 File Offset: 0x0000D728
		private Lazy<IParseUserController> UserController
		{
			[CompilerGenerated]
			get
			{
				return this.<UserController>k__BackingField;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000F530 File Offset: 0x0000D730
		private IWebClient GetWebClient()
		{
			return this.WebClient;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000F538 File Offset: 0x0000D738
		public ParseCommandRunner(IWebClient webClient, IParseInstallationController installationController, IMetadataController metadataController, IServerConnectionData serverConnectionData, Lazy<IParseUserController> userController)
		{
			this.WebClient = webClient;
			this.InstallationController = installationController;
			this.MetadataController = metadataController;
			this.ServerConnectionData = serverConnectionData;
			this.UserController = userController;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000F568 File Offset: 0x0000D768
		public Task<Tuple<HttpStatusCode, IDictionary<string, object>>> RunCommandAsync(ParseCommand command, IProgress<IDataTransferLevel> uploadProgress = null, IProgress<IDataTransferLevel> downloadProgress = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Func<Task<Tuple<HttpStatusCode, string>>, Tuple<HttpStatusCode, IDictionary<string, object>>> <>9__1;
			return this.PrepareCommand(command).ContinueWith<Task<Tuple<HttpStatusCode, IDictionary<string, object>>>>(delegate(Task<ParseCommand> commandTask)
			{
				Task<Tuple<HttpStatusCode, string>> task2 = this.GetWebClient().ExecuteAsync(commandTask.Result, uploadProgress, downloadProgress, cancellationToken);
				Func<Task<Tuple<HttpStatusCode, string>>, Tuple<HttpStatusCode, IDictionary<string, object>>> continuation;
				if ((continuation = <>9__1) == null)
				{
					continuation = (<>9__1 = delegate(Task<Tuple<HttpStatusCode, string>> task)
					{
						cancellationToken.ThrowIfCancellationRequested();
						Tuple<HttpStatusCode, string> result = task.Result;
						string item = result.Item2;
						int item2 = (int)result.Item1;
						if (item2 >= 500)
						{
							throw new ParseFailureException(ParseFailureException.ErrorCode.InternalServerError, result.Item2, null);
						}
						if (item == null)
						{
							return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, null);
						}
						IDictionary<string, object> dictionary = null;
						try
						{
							IDictionary<string, object> dictionary2;
							if (!item.StartsWith("["))
							{
								dictionary2 = (JsonUtilities.Parse(item) as IDictionary<string, object>);
							}
							else
							{
								Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
								dictionary3["results"] = JsonUtilities.Parse(item);
								IDictionary<string, object> dictionary4 = dictionary3;
								dictionary2 = dictionary4;
							}
							dictionary = dictionary2;
						}
						catch (Exception cause)
						{
							throw new ParseFailureException(ParseFailureException.ErrorCode.OtherCause, "Invalid or alternatively-formatted response recieved from server.", cause);
						}
						if (item2 < 200 || item2 > 299)
						{
							throw new ParseFailureException(dictionary.ContainsKey("code") ? ((ParseFailureException.ErrorCode)((long)dictionary["code"])) : ParseFailureException.ErrorCode.OtherCause, dictionary.ContainsKey("error") ? (dictionary["error"] as string) : item, null);
						}
						return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, dictionary);
					});
				}
				return task2.OnSuccess(continuation);
			}).Unwrap<Tuple<HttpStatusCode, IDictionary<string, object>>>();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		private Task<ParseCommand> PrepareCommand(ParseCommand command)
		{
			ParseCommand newCommand = new ParseCommand(command)
			{
				Resource = this.ServerConnectionData.ServerURI
			};
			Task<ParseCommand> result = this.InstallationController.GetAsync().ContinueWith<ParseCommand>(delegate(Task<Guid?> task)
			{
				IList<KeyValuePair<string, string>> headers2 = newCommand.Headers;
				lock (headers2)
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Installation-Id", task.Result.ToString()));
				}
				return newCommand;
			});
			IList<KeyValuePair<string, string>> headers = newCommand.Headers;
			lock (headers)
			{
				newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Application-Id", this.ServerConnectionData.ApplicationID));
				newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Client-Version", ParseClient.Version.ToString()));
				if (this.ServerConnectionData.Headers != null)
				{
					foreach (KeyValuePair<string, string> item in this.ServerConnectionData.Headers)
					{
						newCommand.Headers.Add(item);
					}
				}
				if (!string.IsNullOrEmpty(this.MetadataController.HostManifestData.Version))
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-App-Build-Version", this.MetadataController.HostManifestData.Version));
				}
				if (!string.IsNullOrEmpty(this.MetadataController.HostManifestData.ShortVersion))
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-App-Display-Version", this.MetadataController.HostManifestData.ShortVersion));
				}
				if (!string.IsNullOrEmpty(this.MetadataController.EnvironmentData.OSVersion))
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-OS-Version", this.MetadataController.EnvironmentData.OSVersion));
				}
				if (!string.IsNullOrEmpty(this.ServerConnectionData.MasterKey))
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Master-Key", this.ServerConnectionData.MasterKey));
				}
				else
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Windows-Key", this.ServerConnectionData.Key));
				}
				if (this.UserController.Value.RevocableSessionEnabled)
				{
					newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Revocable-Session", "1"));
				}
			}
			return result;
		}

		// Token: 0x040000E1 RID: 225
		[CompilerGenerated]
		private readonly IWebClient <WebClient>k__BackingField;

		// Token: 0x040000E2 RID: 226
		[CompilerGenerated]
		private readonly IParseInstallationController <InstallationController>k__BackingField;

		// Token: 0x040000E3 RID: 227
		[CompilerGenerated]
		private readonly IMetadataController <MetadataController>k__BackingField;

		// Token: 0x040000E4 RID: 228
		[CompilerGenerated]
		private readonly IServerConnectionData <ServerConnectionData>k__BackingField;

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		private readonly Lazy<IParseUserController> <UserController>k__BackingField;

		// Token: 0x0200012F RID: 303
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x0600079E RID: 1950 RVA: 0x0001713E File Offset: 0x0001533E
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x00017148 File Offset: 0x00015348
			internal Task<Tuple<HttpStatusCode, IDictionary<string, object>>> <RunCommandAsync>b__0(Task<ParseCommand> commandTask)
			{
				Task<Tuple<HttpStatusCode, string>> task2 = this.<>4__this.GetWebClient().ExecuteAsync(commandTask.Result, this.uploadProgress, this.downloadProgress, this.cancellationToken);
				Func<Task<Tuple<HttpStatusCode, string>>, Tuple<HttpStatusCode, IDictionary<string, object>>> continuation;
				if ((continuation = this.<>9__1) == null)
				{
					continuation = (this.<>9__1 = delegate(Task<Tuple<HttpStatusCode, string>> task)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						Tuple<HttpStatusCode, string> result = task.Result;
						string item = result.Item2;
						int item2 = (int)result.Item1;
						if (item2 >= 500)
						{
							throw new ParseFailureException(ParseFailureException.ErrorCode.InternalServerError, result.Item2, null);
						}
						if (item == null)
						{
							return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, null);
						}
						IDictionary<string, object> dictionary = null;
						try
						{
							IDictionary<string, object> dictionary2;
							if (!item.StartsWith("["))
							{
								dictionary2 = (JsonUtilities.Parse(item) as IDictionary<string, object>);
							}
							else
							{
								Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
								dictionary3["results"] = JsonUtilities.Parse(item);
								IDictionary<string, object> dictionary4 = dictionary3;
								dictionary2 = dictionary4;
							}
							dictionary = dictionary2;
						}
						catch (Exception cause)
						{
							throw new ParseFailureException(ParseFailureException.ErrorCode.OtherCause, "Invalid or alternatively-formatted response recieved from server.", cause);
						}
						if (item2 < 200 || item2 > 299)
						{
							throw new ParseFailureException(dictionary.ContainsKey("code") ? ((ParseFailureException.ErrorCode)((long)dictionary["code"])) : ParseFailureException.ErrorCode.OtherCause, dictionary.ContainsKey("error") ? (dictionary["error"] as string) : item, null);
						}
						return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, dictionary);
					});
				}
				return task2.OnSuccess(continuation);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x000171A4 File Offset: 0x000153A4
			internal Tuple<HttpStatusCode, IDictionary<string, object>> <RunCommandAsync>b__1(Task<Tuple<HttpStatusCode, string>> task)
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				Tuple<HttpStatusCode, string> result = task.Result;
				string item = result.Item2;
				int item2 = (int)result.Item1;
				if (item2 >= 500)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.InternalServerError, result.Item2, null);
				}
				if (item == null)
				{
					return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, null);
				}
				IDictionary<string, object> dictionary = null;
				try
				{
					IDictionary<string, object> dictionary2;
					if (!item.StartsWith("["))
					{
						dictionary2 = (JsonUtilities.Parse(item) as IDictionary<string, object>);
					}
					else
					{
						Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
						dictionary3["results"] = JsonUtilities.Parse(item);
						IDictionary<string, object> dictionary4 = dictionary3;
						dictionary2 = dictionary4;
					}
					dictionary = dictionary2;
				}
				catch (Exception cause)
				{
					throw new ParseFailureException(ParseFailureException.ErrorCode.OtherCause, "Invalid or alternatively-formatted response recieved from server.", cause);
				}
				if (item2 < 200 || item2 > 299)
				{
					throw new ParseFailureException(dictionary.ContainsKey("code") ? ((ParseFailureException.ErrorCode)((long)dictionary["code"])) : ParseFailureException.ErrorCode.OtherCause, dictionary.ContainsKey("error") ? (dictionary["error"] as string) : item, null);
				}
				return new Tuple<HttpStatusCode, IDictionary<string, object>>(result.Item1, dictionary);
			}

			// Token: 0x040002C0 RID: 704
			public ParseCommandRunner <>4__this;

			// Token: 0x040002C1 RID: 705
			public IProgress<IDataTransferLevel> uploadProgress;

			// Token: 0x040002C2 RID: 706
			public IProgress<IDataTransferLevel> downloadProgress;

			// Token: 0x040002C3 RID: 707
			public CancellationToken cancellationToken;

			// Token: 0x040002C4 RID: 708
			public Func<Task<Tuple<HttpStatusCode, string>>, Tuple<HttpStatusCode, IDictionary<string, object>>> <>9__1;
		}

		// Token: 0x02000130 RID: 304
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x060007A1 RID: 1953 RVA: 0x000172B8 File Offset: 0x000154B8
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x000172C0 File Offset: 0x000154C0
			internal ParseCommand <PrepareCommand>b__0(Task<Guid?> task)
			{
				IList<KeyValuePair<string, string>> headers = this.newCommand.Headers;
				lock (headers)
				{
					this.newCommand.Headers.Add(new KeyValuePair<string, string>("X-Parse-Installation-Id", task.Result.ToString()));
				}
				return this.newCommand;
			}

			// Token: 0x040002C5 RID: 709
			public ParseCommand newCommand;
		}
	}
}
