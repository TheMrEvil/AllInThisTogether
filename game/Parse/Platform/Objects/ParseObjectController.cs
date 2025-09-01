using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Objects
{
	// Token: 0x02000031 RID: 49
	public class ParseObjectController : IParseObjectController
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009EBD File Offset: 0x000080BD
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009EC5 File Offset: 0x000080C5
		private IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009ECD File Offset: 0x000080CD
		private IServerConnectionData ServerConnectionData
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerConnectionData>k__BackingField;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009ED8 File Offset: 0x000080D8
		public ParseObjectController(IParseCommandRunner commandRunner, IParseDataDecoder decoder, IServerConnectionData serverConnectionData)
		{
			this.CommandRunner = commandRunner;
			this.Decoder = decoder;
			this.ServerConnectionData = serverConnectionData;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009F10 File Offset: 0x00008110
		public Task<IObjectState> FetchAsync(IObjectState state, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			ParseCommand command = new ParseCommand("classes/" + Uri.EscapeDataString(state.ClassName) + "/" + Uri.EscapeDataString(state.ObjectId), "GET", sessionToken, null, null);
			return this.CommandRunner.RunCommandAsync(command, null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009F88 File Offset: 0x00008188
		public Task<IObjectState> SaveAsync(IObjectState state, IDictionary<string, IParseFieldOperation> operations, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			ParseCommand command = new ParseCommand((state.ObjectId == null) ? ("classes/" + Uri.EscapeDataString(state.ClassName)) : ("classes/" + Uri.EscapeDataString(state.ClassName) + "/" + state.ObjectId), (state.ObjectId == null) ? "POST" : "PUT", sessionToken, null, serviceHub.GenerateJSONObjectForSaving(operations));
			return this.CommandRunner.RunCommandAsync(command, null, null, cancellationToken).OnSuccess((Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task) => ParseObjectCoder.Instance.Decode(task.Result.Item2, this.Decoder, serviceHub).MutatedClone(delegate(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (task.Result.Item1 == HttpStatusCode.Created);
			}));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A034 File Offset: 0x00008234
		public IList<Task<IObjectState>> SaveAllAsync(IList<IObjectState> states, IList<IDictionary<string, IParseFieldOperation>> operationsList, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			Func<Task<IDictionary<string, object>>, IObjectState> <>9__2;
			return this.ExecuteBatchRequests(states.Zip(operationsList, (IObjectState item, IDictionary<string, IParseFieldOperation> operations) => new ParseCommand((item.ObjectId == null) ? ("classes/" + Uri.EscapeDataString(item.ClassName)) : ("classes/" + Uri.EscapeDataString(item.ClassName) + "/" + Uri.EscapeDataString(item.ObjectId)), (item.ObjectId == null) ? "POST" : "PUT", null, null, serviceHub.GenerateJSONObjectForSaving(operations))).ToList<ParseCommand>(), sessionToken, cancellationToken).Select(delegate(Task<IDictionary<string, object>> task)
			{
				Func<Task<IDictionary<string, object>>, IObjectState> continuation;
				if ((continuation = <>9__2) == null)
				{
					continuation = (<>9__2 = ((Task<IDictionary<string, object>> task) => ParseObjectCoder.Instance.Decode(task.Result, this.Decoder, serviceHub)));
				}
				return task.OnSuccess(continuation);
			}).ToList<Task<IObjectState>>();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A08D File Offset: 0x0000828D
		public Task DeleteAsync(IObjectState state, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CommandRunner.RunCommandAsync(new ParseCommand("classes/" + state.ClassName + "/" + state.ObjectId, "DELETE", sessionToken, null, null), null, null, cancellationToken);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public IList<Task> DeleteAllAsync(IList<IObjectState> states, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.ExecuteBatchRequests((from item in states
			where item.ObjectId != null
			select new ParseCommand("classes/" + Uri.EscapeDataString(item.ClassName) + "/" + Uri.EscapeDataString(item.ObjectId), "DELETE", null, null, null)).ToList<ParseCommand>(), sessionToken, cancellationToken).Cast<Task>().ToList<Task>();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A135 File Offset: 0x00008335
		private int MaximumBatchSize
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumBatchSize>k__BackingField;
			}
		} = 50;

		// Token: 0x06000276 RID: 630 RVA: 0x0000A140 File Offset: 0x00008340
		internal IList<Task<IDictionary<string, object>>> ExecuteBatchRequests(IList<ParseCommand> requests, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			List<Task<IDictionary<string, object>>> list = new List<Task<IDictionary<string, object>>>();
			int i = requests.Count;
			IEnumerable<ParseCommand> source = requests;
			while (i > this.MaximumBatchSize)
			{
				List<ParseCommand> requests2 = source.Take(this.MaximumBatchSize).ToList<ParseCommand>();
				source = source.Skip(this.MaximumBatchSize);
				list.AddRange(this.ExecuteBatchRequest(requests2, sessionToken, cancellationToken));
				i = source.Count<ParseCommand>();
			}
			list.AddRange(this.ExecuteBatchRequest(source.ToList<ParseCommand>(), sessionToken, cancellationToken));
			return list;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A1B4 File Offset: 0x000083B4
		private IList<Task<IDictionary<string, object>>> ExecuteBatchRequest(IList<ParseCommand> requests, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			int batchSize = requests.Count;
			List<Task<IDictionary<string, object>>> list = new List<Task<IDictionary<string, object>>>();
			List<TaskCompletionSource<IDictionary<string, object>>> completionSources = new List<TaskCompletionSource<IDictionary<string, object>>>();
			for (int i = 0; i < batchSize; i++)
			{
				TaskCompletionSource<IDictionary<string, object>> taskCompletionSource = new TaskCompletionSource<IDictionary<string, object>>();
				completionSources.Add(taskCompletionSource);
				list.Add(taskCompletionSource.Task);
			}
			List<object> value = requests.Select(delegate(ParseCommand request)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2["method"] = request.Method;
				dictionary2["path"] = ((request != null && request.Path != null && request.Resource != null) ? request.Target.AbsolutePath : new Uri(new Uri(this.ServerConnectionData.ServerURI), request.Path).AbsolutePath);
				Dictionary<string, object> dictionary3 = dictionary2;
				if (request.DataObject != null)
				{
					dictionary3["body"] = request.DataObject;
				}
				return dictionary3;
			}).Cast<object>().ToList<object>();
			string relativeUri = "batch";
			string method = "POST";
			IList<KeyValuePair<string, string>> headers = null;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["requests"] = value;
			ParseCommand command = new ParseCommand(relativeUri, method, sessionToken, headers, dictionary);
			this.CommandRunner.RunCommandAsync(command, null, null, cancellationToken).ContinueWith(delegate(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				if (task.IsFaulted || task.IsCanceled)
				{
					foreach (TaskCompletionSource<IDictionary<string, object>> taskCompletionSource2 in completionSources)
					{
						if (task.IsFaulted)
						{
							taskCompletionSource2.TrySetException(task.Exception);
						}
						else if (task.IsCanceled)
						{
							taskCompletionSource2.TrySetCanceled();
						}
					}
					return;
				}
				IList<object> list2 = Conversion.As<IList<object>>(task.Result.Item2["results"]);
				int count = list2.Count;
				if (count != batchSize)
				{
					foreach (TaskCompletionSource<IDictionary<string, object>> taskCompletionSource3 in completionSources)
					{
						taskCompletionSource3.TrySetException(new InvalidOperationException(string.Format("Batch command result count expected: {0} but was: {1}.", batchSize, count)));
					}
					return;
				}
				for (int j = 0; j < batchSize; j++)
				{
					Dictionary<string, object> dictionary2 = list2[j] as Dictionary<string, object>;
					TaskCompletionSource<IDictionary<string, object>> taskCompletionSource4 = completionSources[j];
					if (dictionary2.ContainsKey("success"))
					{
						taskCompletionSource4.TrySetResult(dictionary2["success"] as IDictionary<string, object>);
					}
					else if (dictionary2.ContainsKey("error"))
					{
						IDictionary<string, object> dictionary3 = dictionary2["error"] as IDictionary<string, object>;
						taskCompletionSource4.TrySetException(new ParseFailureException((ParseFailureException.ErrorCode)((long)dictionary3["code"]), dictionary3["error"] as string, null));
					}
					else
					{
						taskCompletionSource4.TrySetException(new InvalidOperationException("Invalid batch command response."));
					}
				}
			});
			return list;
		}

		// Token: 0x04000065 RID: 101
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000066 RID: 102
		[CompilerGenerated]
		private readonly IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x04000067 RID: 103
		[CompilerGenerated]
		private readonly IServerConnectionData <ServerConnectionData>k__BackingField;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private readonly int <MaximumBatchSize>k__BackingField;

		// Token: 0x020000FE RID: 254
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060006D3 RID: 1747 RVA: 0x00014FD8 File Offset: 0x000131D8
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x00014FE0 File Offset: 0x000131E0
			internal IObjectState <FetchAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result.Item2, this.<>4__this.Decoder, this.serviceHub);
			}

			// Token: 0x0400020C RID: 524
			public ParseObjectController <>4__this;

			// Token: 0x0400020D RID: 525
			public IServiceHub serviceHub;
		}

		// Token: 0x020000FF RID: 255
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x060006D5 RID: 1749 RVA: 0x00015008 File Offset: 0x00013208
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00015010 File Offset: 0x00013210
			internal IObjectState <SaveAsync>b__0(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				ParseObjectController.<>c__DisplayClass11_1 CS$<>8__locals1 = new ParseObjectController.<>c__DisplayClass11_1();
				CS$<>8__locals1.task = task;
				return ParseObjectCoder.Instance.Decode(CS$<>8__locals1.task.Result.Item2, this.<>4__this.Decoder, this.serviceHub).MutatedClone(new Action<MutableObjectState>(CS$<>8__locals1.<SaveAsync>b__1));
			}

			// Token: 0x0400020E RID: 526
			public ParseObjectController <>4__this;

			// Token: 0x0400020F RID: 527
			public IServiceHub serviceHub;
		}

		// Token: 0x02000100 RID: 256
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_1
		{
			// Token: 0x060006D7 RID: 1751 RVA: 0x00015066 File Offset: 0x00013266
			public <>c__DisplayClass11_1()
			{
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001506E File Offset: 0x0001326E
			internal void <SaveAsync>b__1(MutableObjectState mutableClone)
			{
				mutableClone.IsNew = (this.task.Result.Item1 == HttpStatusCode.Created);
			}

			// Token: 0x04000210 RID: 528
			public Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task;
		}

		// Token: 0x02000101 RID: 257
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x060006D9 RID: 1753 RVA: 0x0001508D File Offset: 0x0001328D
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x00015098 File Offset: 0x00013298
			internal ParseCommand <SaveAllAsync>b__0(IObjectState item, IDictionary<string, IParseFieldOperation> operations)
			{
				return new ParseCommand((item.ObjectId == null) ? ("classes/" + Uri.EscapeDataString(item.ClassName)) : ("classes/" + Uri.EscapeDataString(item.ClassName) + "/" + Uri.EscapeDataString(item.ObjectId)), (item.ObjectId == null) ? "POST" : "PUT", null, null, this.serviceHub.GenerateJSONObjectForSaving(operations));
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x00015110 File Offset: 0x00013310
			internal Task<IObjectState> <SaveAllAsync>b__1(Task<IDictionary<string, object>> task)
			{
				Func<Task<IDictionary<string, object>>, IObjectState> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = ((Task<IDictionary<string, object>> task) => ParseObjectCoder.Instance.Decode(task.Result, this.<>4__this.Decoder, this.serviceHub)));
				}
				return task2.OnSuccess(continuation);
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x00015142 File Offset: 0x00013342
			internal IObjectState <SaveAllAsync>b__2(Task<IDictionary<string, object>> task)
			{
				return ParseObjectCoder.Instance.Decode(task.Result, this.<>4__this.Decoder, this.serviceHub);
			}

			// Token: 0x04000211 RID: 529
			public IServiceHub serviceHub;

			// Token: 0x04000212 RID: 530
			public ParseObjectController <>4__this;

			// Token: 0x04000213 RID: 531
			public Func<Task<IDictionary<string, object>>, IObjectState> <>9__2;
		}

		// Token: 0x02000102 RID: 258
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006DD RID: 1757 RVA: 0x00015165 File Offset: 0x00013365
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x00015171 File Offset: 0x00013371
			public <>c()
			{
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00015179 File Offset: 0x00013379
			internal bool <DeleteAllAsync>b__14_0(IObjectState item)
			{
				return item.ObjectId != null;
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x00015184 File Offset: 0x00013384
			internal ParseCommand <DeleteAllAsync>b__14_1(IObjectState item)
			{
				return new ParseCommand("classes/" + Uri.EscapeDataString(item.ClassName) + "/" + Uri.EscapeDataString(item.ObjectId), "DELETE", null, null, null);
			}

			// Token: 0x04000214 RID: 532
			public static readonly ParseObjectController.<>c <>9 = new ParseObjectController.<>c();

			// Token: 0x04000215 RID: 533
			public static Func<IObjectState, bool> <>9__14_0;

			// Token: 0x04000216 RID: 534
			public static Func<IObjectState, ParseCommand> <>9__14_1;
		}

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x060006E1 RID: 1761 RVA: 0x000151B8 File Offset: 0x000133B8
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x000151C0 File Offset: 0x000133C0
			internal Dictionary<string, object> <ExecuteBatchRequest>b__0(ParseCommand request)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["method"] = request.Method;
				dictionary["path"] = ((request != null && request.Path != null && request.Resource != null) ? request.Target.AbsolutePath : new Uri(new Uri(this.<>4__this.ServerConnectionData.ServerURI), request.Path).AbsolutePath);
				Dictionary<string, object> dictionary2 = dictionary;
				if (request.DataObject != null)
				{
					dictionary2["body"] = request.DataObject;
				}
				return dictionary2;
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x00015250 File Offset: 0x00013450
			internal void <ExecuteBatchRequest>b__1(Task<Tuple<HttpStatusCode, IDictionary<string, object>>> task)
			{
				if (task.IsFaulted || task.IsCanceled)
				{
					foreach (TaskCompletionSource<IDictionary<string, object>> taskCompletionSource in this.completionSources)
					{
						if (task.IsFaulted)
						{
							taskCompletionSource.TrySetException(task.Exception);
						}
						else if (task.IsCanceled)
						{
							taskCompletionSource.TrySetCanceled();
						}
					}
					return;
				}
				IList<object> list = Conversion.As<IList<object>>(task.Result.Item2["results"]);
				int count = list.Count;
				if (count != this.batchSize)
				{
					foreach (TaskCompletionSource<IDictionary<string, object>> taskCompletionSource2 in this.completionSources)
					{
						taskCompletionSource2.TrySetException(new InvalidOperationException(string.Format("Batch command result count expected: {0} but was: {1}.", this.batchSize, count)));
					}
					return;
				}
				for (int i = 0; i < this.batchSize; i++)
				{
					Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
					TaskCompletionSource<IDictionary<string, object>> taskCompletionSource3 = this.completionSources[i];
					if (dictionary.ContainsKey("success"))
					{
						taskCompletionSource3.TrySetResult(dictionary["success"] as IDictionary<string, object>);
					}
					else if (dictionary.ContainsKey("error"))
					{
						IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
						taskCompletionSource3.TrySetException(new ParseFailureException((ParseFailureException.ErrorCode)((long)dictionary2["code"]), dictionary2["error"] as string, null));
					}
					else
					{
						taskCompletionSource3.TrySetException(new InvalidOperationException("Invalid batch command response."));
					}
				}
			}

			// Token: 0x04000217 RID: 535
			public ParseObjectController <>4__this;

			// Token: 0x04000218 RID: 536
			public List<TaskCompletionSource<IDictionary<string, object>>> completionSources;

			// Token: 0x04000219 RID: 537
			public int batchSize;
		}
	}
}
