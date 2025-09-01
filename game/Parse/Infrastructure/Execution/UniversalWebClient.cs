using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Execution
{
	// Token: 0x02000062 RID: 98
	public class UniversalWebClient : IWebClient
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000F84C File Offset: 0x0000DA4C
		private static HashSet<string> ContentHeaders
		{
			[CompilerGenerated]
			get
			{
				return UniversalWebClient.<ContentHeaders>k__BackingField;
			}
		} = new HashSet<string>
		{
			"Allow",
			"Content-Disposition",
			"Content-Encoding",
			"Content-Language",
			"Content-Length",
			"Content-Location",
			"Content-MD5",
			"Content-Range",
			"Content-Type",
			"Expires",
			"Last-Modified"
		};

		// Token: 0x06000470 RID: 1136 RVA: 0x0000F853 File Offset: 0x0000DA53
		public UniversalWebClient() : this(new HttpClient())
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000F860 File Offset: 0x0000DA60
		public UniversalWebClient(HttpClient client)
		{
			this.Client = client;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000F86F File Offset: 0x0000DA6F
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000F877 File Offset: 0x0000DA77
		private HttpClient Client
		{
			[CompilerGenerated]
			get
			{
				return this.<Client>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Client>k__BackingField = value;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000F880 File Offset: 0x0000DA80
		public Task<Tuple<HttpStatusCode, string>> ExecuteAsync(WebRequest httpRequest, IProgress<IDataTransferLevel> uploadProgress, IProgress<IDataTransferLevel> downloadProgress, CancellationToken cancellationToken)
		{
			if (uploadProgress == null)
			{
				uploadProgress = new Progress<IDataTransferLevel>();
			}
			if (downloadProgress == null)
			{
				downloadProgress = new Progress<IDataTransferLevel>();
			}
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod(httpRequest.Method), httpRequest.Target);
			Stream stream = (httpRequest.Data == null && httpRequest.Method.ToLower().Equals("post")) ? new MemoryStream(new byte[0]) : httpRequest.Data;
			if (stream != null)
			{
				httpRequestMessage.Content = new StreamContent(stream);
			}
			if (httpRequest.Headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in httpRequest.Headers)
				{
					if (UniversalWebClient.ContentHeaders.Contains(keyValuePair.Key))
					{
						httpRequestMessage.Content.Headers.Add(keyValuePair.Key, keyValuePair.Value);
					}
					else
					{
						httpRequestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			httpRequestMessage.Headers.Add("Cache-Control", "no-cache");
			httpRequestMessage.Headers.IfModifiedSince = new DateTimeOffset?(DateTimeOffset.UtcNow);
			uploadProgress.Report(new DataTransferLevel
			{
				Amount = 0.0
			});
			return this.Client.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ContinueWith<Task<Task<Tuple<HttpStatusCode, string>>>>(delegate(Task<HttpResponseMessage> httpMessageTask)
			{
				HttpResponseMessage response = httpMessageTask.Result;
				uploadProgress.Report(new DataTransferLevel
				{
					Amount = 1.0
				});
				return response.Content.ReadAsStreamAsync().ContinueWith<Task<Tuple<HttpStatusCode, string>>>(delegate(Task<Stream> streamTask)
				{
					MemoryStream resultStream = new MemoryStream();
					Stream responseStream = streamTask.Result;
					int bufferSize = 4096;
					int bytesRead = 0;
					byte[] buffer = new byte[bufferSize];
					long totalLength = -1L;
					long readSoFar = 0L;
					try
					{
						totalLength = responseStream.Length;
					}
					catch
					{
					}
					Func<Task<int>, bool> <>9__6;
					Action<Task> <>9__7;
					return InternalExtensions.WhileAsync(delegate
					{
						Task<int> task = responseStream.ReadAsync(buffer, 0, bufferSize, cancellationToken);
						Func<Task<int>, bool> continuation;
						if ((continuation = <>9__6) == null)
						{
							continuation = (<>9__6 = ((Task<int> readTask) => (bytesRead = readTask.Result) > 0));
						}
						return task.OnSuccess(continuation);
					}, delegate
					{
						cancellationToken.ThrowIfCancellationRequested();
						Task task = resultStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
						Action<Task> continuation;
						if ((continuation = <>9__7) == null)
						{
							continuation = (<>9__7 = delegate(Task _)
							{
								cancellationToken.ThrowIfCancellationRequested();
								readSoFar += (long)bytesRead;
								if (totalLength > -1L)
								{
									downloadProgress.Report(new DataTransferLevel
									{
										Amount = 1.0 * (double)readSoFar / (double)totalLength
									});
								}
							});
						}
						return task.OnSuccess(continuation);
					}).ContinueWith<Task>(delegate(Task _)
					{
						responseStream.Dispose();
						return _;
					}).Unwrap().OnSuccess(delegate(Task _)
					{
						if (totalLength == -1L)
						{
							downloadProgress.Report(new DataTransferLevel
							{
								Amount = 1.0
							});
						}
						byte[] array = resultStream.ToArray();
						resultStream.Dispose();
						return new Tuple<HttpStatusCode, string>(response.StatusCode, Encoding.UTF8.GetString(array, 0, array.Length));
					});
				});
			}).Unwrap<Task<Tuple<HttpStatusCode, string>>>().Unwrap<Tuple<HttpStatusCode, string>>();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000FA30 File Offset: 0x0000DC30
		// Note: this type is marked as 'beforefieldinit'.
		static UniversalWebClient()
		{
		}

		// Token: 0x040000E6 RID: 230
		[CompilerGenerated]
		private static readonly HashSet<string> <ContentHeaders>k__BackingField;

		// Token: 0x040000E7 RID: 231
		[CompilerGenerated]
		private HttpClient <Client>k__BackingField;

		// Token: 0x02000131 RID: 305
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x060007A3 RID: 1955 RVA: 0x00017334 File Offset: 0x00015534
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0001733C File Offset: 0x0001553C
			internal Task<Task<Tuple<HttpStatusCode, string>>> <ExecuteAsync>b__0(Task<HttpResponseMessage> httpMessageTask)
			{
				UniversalWebClient.<>c__DisplayClass9_1 CS$<>8__locals1 = new UniversalWebClient.<>c__DisplayClass9_1();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				CS$<>8__locals1.response = httpMessageTask.Result;
				this.uploadProgress.Report(new DataTransferLevel
				{
					Amount = 1.0
				});
				return CS$<>8__locals1.response.Content.ReadAsStreamAsync().ContinueWith<Task<Tuple<HttpStatusCode, string>>>(new Func<Task<Stream>, Task<Tuple<HttpStatusCode, string>>>(CS$<>8__locals1.<ExecuteAsync>b__1));
			}

			// Token: 0x040002C6 RID: 710
			public IProgress<IDataTransferLevel> uploadProgress;

			// Token: 0x040002C7 RID: 711
			public CancellationToken cancellationToken;

			// Token: 0x040002C8 RID: 712
			public IProgress<IDataTransferLevel> downloadProgress;
		}

		// Token: 0x02000132 RID: 306
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_1
		{
			// Token: 0x060007A5 RID: 1957 RVA: 0x000173A2 File Offset: 0x000155A2
			public <>c__DisplayClass9_1()
			{
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x000173AC File Offset: 0x000155AC
			internal Task<Tuple<HttpStatusCode, string>> <ExecuteAsync>b__1(Task<Stream> streamTask)
			{
				UniversalWebClient.<>c__DisplayClass9_2 CS$<>8__locals1 = new UniversalWebClient.<>c__DisplayClass9_2();
				CS$<>8__locals1.CS$<>8__locals2 = this;
				CS$<>8__locals1.resultStream = new MemoryStream();
				CS$<>8__locals1.responseStream = streamTask.Result;
				CS$<>8__locals1.bufferSize = 4096;
				CS$<>8__locals1.bytesRead = 0;
				CS$<>8__locals1.buffer = new byte[CS$<>8__locals1.bufferSize];
				CS$<>8__locals1.totalLength = -1L;
				CS$<>8__locals1.readSoFar = 0L;
				try
				{
					CS$<>8__locals1.totalLength = CS$<>8__locals1.responseStream.Length;
				}
				catch
				{
				}
				return InternalExtensions.WhileAsync(new Func<Task<bool>>(CS$<>8__locals1.<ExecuteAsync>b__2), new Func<Task>(CS$<>8__locals1.<ExecuteAsync>b__3)).ContinueWith<Task>(new Func<Task, Task>(CS$<>8__locals1.<ExecuteAsync>b__4)).Unwrap().OnSuccess(new Func<Task, Tuple<HttpStatusCode, string>>(CS$<>8__locals1.<ExecuteAsync>b__5));
			}

			// Token: 0x040002C9 RID: 713
			public HttpResponseMessage response;

			// Token: 0x040002CA RID: 714
			public UniversalWebClient.<>c__DisplayClass9_0 CS$<>8__locals1;
		}

		// Token: 0x02000133 RID: 307
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_2
		{
			// Token: 0x060007A7 RID: 1959 RVA: 0x0001747C File Offset: 0x0001567C
			public <>c__DisplayClass9_2()
			{
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x00017484 File Offset: 0x00015684
			internal Task<bool> <ExecuteAsync>b__2()
			{
				Task<int> task = this.responseStream.ReadAsync(this.buffer, 0, this.bufferSize, this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken);
				Func<Task<int>, bool> continuation;
				if ((continuation = this.<>9__6) == null)
				{
					continuation = (this.<>9__6 = ((Task<int> readTask) => (this.bytesRead = readTask.Result) > 0));
				}
				return task.OnSuccess(continuation);
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x000174E0 File Offset: 0x000156E0
			internal bool <ExecuteAsync>b__6(Task<int> readTask)
			{
				return (this.bytesRead = readTask.Result) > 0;
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00017500 File Offset: 0x00015700
			internal Task <ExecuteAsync>b__3()
			{
				this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
				Task task = this.resultStream.WriteAsync(this.buffer, 0, this.bytesRead, this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken);
				Action<Task> continuation;
				if ((continuation = this.<>9__7) == null)
				{
					continuation = (this.<>9__7 = delegate(Task _)
					{
						this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
						this.readSoFar += (long)this.bytesRead;
						if (this.totalLength > -1L)
						{
							this.CS$<>8__locals2.CS$<>8__locals1.downloadProgress.Report(new DataTransferLevel
							{
								Amount = 1.0 * (double)this.readSoFar / (double)this.totalLength
							});
						}
					});
				}
				return task.OnSuccess(continuation);
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x00017570 File Offset: 0x00015770
			internal void <ExecuteAsync>b__7(Task _)
			{
				this.CS$<>8__locals2.CS$<>8__locals1.cancellationToken.ThrowIfCancellationRequested();
				this.readSoFar += (long)this.bytesRead;
				if (this.totalLength > -1L)
				{
					this.CS$<>8__locals2.CS$<>8__locals1.downloadProgress.Report(new DataTransferLevel
					{
						Amount = 1.0 * (double)this.readSoFar / (double)this.totalLength
					});
				}
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x000175E9 File Offset: 0x000157E9
			internal Task <ExecuteAsync>b__4(Task _)
			{
				this.responseStream.Dispose();
				return _;
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x000175F8 File Offset: 0x000157F8
			internal Tuple<HttpStatusCode, string> <ExecuteAsync>b__5(Task _)
			{
				if (this.totalLength == -1L)
				{
					this.CS$<>8__locals2.CS$<>8__locals1.downloadProgress.Report(new DataTransferLevel
					{
						Amount = 1.0
					});
				}
				byte[] array = this.resultStream.ToArray();
				this.resultStream.Dispose();
				return new Tuple<HttpStatusCode, string>(this.CS$<>8__locals2.response.StatusCode, Encoding.UTF8.GetString(array, 0, array.Length));
			}

			// Token: 0x040002CB RID: 715
			public Stream responseStream;

			// Token: 0x040002CC RID: 716
			public byte[] buffer;

			// Token: 0x040002CD RID: 717
			public int bufferSize;

			// Token: 0x040002CE RID: 718
			public int bytesRead;

			// Token: 0x040002CF RID: 719
			public MemoryStream resultStream;

			// Token: 0x040002D0 RID: 720
			public long readSoFar;

			// Token: 0x040002D1 RID: 721
			public long totalLength;

			// Token: 0x040002D2 RID: 722
			public UniversalWebClient.<>c__DisplayClass9_1 CS$<>8__locals2;

			// Token: 0x040002D3 RID: 723
			public Func<Task<int>, bool> <>9__6;

			// Token: 0x040002D4 RID: 724
			public Action<Task> <>9__7;
		}
	}
}
