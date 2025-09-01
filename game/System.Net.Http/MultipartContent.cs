using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
	/// <summary>Provides a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
	// Token: 0x0200002E RID: 46
	public class MultipartContent : HttpContent, IEnumerable<HttpContent>, IEnumerable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		// Token: 0x0600017A RID: 378 RVA: 0x00006012 File Offset: 0x00004212
		public MultipartContent() : this("mixed")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was <see langword="null" /> or contains only white space characters.</exception>
		// Token: 0x0600017B RID: 379 RVA: 0x00006020 File Offset: 0x00004220
		public MultipartContent(string subtype) : this(subtype, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture))
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartContent" /> class.</summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <param name="boundary">The boundary string for the multipart content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subtype" /> was <see langword="null" /> or an empty string.  
		///  The <paramref name="boundary" /> was <see langword="null" /> or contains only white space characters.  
		///  -or-  
		///  The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		// Token: 0x0600017C RID: 380 RVA: 0x0000604C File Offset: 0x0000424C
		public MultipartContent(string subtype, string boundary)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw new ArgumentException("boundary");
			}
			if (string.IsNullOrWhiteSpace(boundary))
			{
				throw new ArgumentException("boundary");
			}
			if (boundary.Length > 70)
			{
				throw new ArgumentOutOfRangeException("boundary");
			}
			if (boundary.Last<char>() == ' ' || !MultipartContent.IsValidRFC2049(boundary))
			{
				throw new ArgumentException("boundary");
			}
			this.boundary = boundary;
			this.nested_content = new List<HttpContent>(2);
			base.Headers.ContentType = new MediaTypeHeaderValue("multipart/" + subtype)
			{
				Parameters = 
				{
					new NameValueHeaderValue("boundary", "\"" + boundary + "\"")
				}
			};
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000610C File Offset: 0x0000430C
		private static bool IsValidRFC2049(string s)
		{
			foreach (char c in s)
			{
				if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9'))
				{
					if (c <= ':')
					{
						switch (c)
						{
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
							goto IL_71;
						case '*':
							break;
						default:
							if (c == ':')
							{
								goto IL_71;
							}
							break;
						}
					}
					else if (c == '=' || c == '?')
					{
						goto IL_71;
					}
					return false;
				}
				IL_71:;
			}
			return true;
		}

		/// <summary>Add multipart HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was <see langword="null" />.</exception>
		// Token: 0x0600017E RID: 382 RVA: 0x00006198 File Offset: 0x00004398
		public virtual void Add(HttpContent content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (this.nested_content == null)
			{
				this.nested_content = new List<HttpContent>();
			}
			this.nested_content.Add(content);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Http.MultipartContent" /> and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x0600017F RID: 383 RVA: 0x000061C8 File Offset: 0x000043C8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpContent httpContent in this.nested_content)
				{
					httpContent.Dispose();
				}
				this.nested_content = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Serialize the multipart HTTP content to a stream as an asynchronous operation.</summary>
		/// <param name="stream">The target stream.</param>
		/// <param name="context">Information about the transport (channel binding token, for example). This parameter may be <see langword="null" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06000180 RID: 384 RVA: 0x0000622C File Offset: 0x0000442C
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			MultipartContent.<SerializeToStreamAsync>d__8 <SerializeToStreamAsync>d__;
			<SerializeToStreamAsync>d__.<>4__this = this;
			<SerializeToStreamAsync>d__.stream = stream;
			<SerializeToStreamAsync>d__.context = context;
			<SerializeToStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SerializeToStreamAsync>d__.<>1__state = -1;
			<SerializeToStreamAsync>d__.<>t__builder.Start<MultipartContent.<SerializeToStreamAsync>d__8>(ref <SerializeToStreamAsync>d__);
			return <SerializeToStreamAsync>d__.<>t__builder.Task;
		}

		/// <summary>Determines whether the HTTP multipart content has a valid length in bytes.</summary>
		/// <param name="length">The length in bytes of the HHTP content.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="length" /> is a valid length; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000181 RID: 385 RVA: 0x00006280 File Offset: 0x00004480
		protected internal override bool TryComputeLength(out long length)
		{
			length = (long)(12 + 2 * this.boundary.Length);
			for (int i = 0; i < this.nested_content.Count; i++)
			{
				HttpContent httpContent = this.nested_content[i];
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in httpContent.Headers)
				{
					length += (long)keyValuePair.Key.Length;
					length += 4L;
					foreach (string text in keyValuePair.Value)
					{
						length += (long)text.Length;
					}
				}
				long num;
				if (!httpContent.TryComputeLength(out num))
				{
					return false;
				}
				length += 2L;
				length += num;
				if (i != this.nested_content.Count - 1)
				{
					length += 6L;
					length += (long)this.boundary.Length;
				}
			}
			return true;
		}

		/// <summary>Returns an enumerator that iterates through the collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized using the multipart/* content type specification.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		// Token: 0x06000182 RID: 386 RVA: 0x000063A8 File Offset: 0x000045A8
		public IEnumerator<HttpContent> GetEnumerator()
		{
			return this.nested_content.GetEnumerator();
		}

		/// <summary>The explicit implementation of the <see cref="M:System.Net.Http.MultipartContent.GetEnumerator" /> method.</summary>
		/// <returns>An object that can be used to iterate through the collection.</returns>
		// Token: 0x06000183 RID: 387 RVA: 0x000063A8 File Offset: 0x000045A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.nested_content.GetEnumerator();
		}

		// Token: 0x040000C9 RID: 201
		private List<HttpContent> nested_content;

		// Token: 0x040000CA RID: 202
		private readonly string boundary;

		// Token: 0x0200002F RID: 47
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SerializeToStreamAsync>d__8 : IAsyncStateMachine
		{
			// Token: 0x06000184 RID: 388 RVA: 0x000063BC File Offset: 0x000045BC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MultipartContent multipartContent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_25D;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_3DD;
					default:
						this.<sb>5__2 = new StringBuilder();
						this.<sb>5__2.Append('-').Append('-');
						this.<sb>5__2.Append(multipartContent.boundary);
						this.<sb>5__2.Append('\r').Append('\n');
						this.<i>5__3 = 0;
						goto IL_2E3;
					}
					IL_1E4:
					awaiter.GetResult();
					awaiter = this.<c>5__4.SerializeToStreamAsync_internal(this.stream, this.context).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 1);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MultipartContent.<SerializeToStreamAsync>d__8>(ref awaiter, ref this);
						return;
					}
					IL_25D:
					awaiter.GetResult();
					if (this.<i>5__3 != multipartContent.nested_content.Count - 1)
					{
						this.<sb>5__2.Append('\r').Append('\n');
						this.<sb>5__2.Append('-').Append('-');
						this.<sb>5__2.Append(multipartContent.boundary);
						this.<sb>5__2.Append('\r').Append('\n');
					}
					this.<c>5__4 = null;
					int num2 = this.<i>5__3;
					this.<i>5__3 = num2 + 1;
					IL_2E3:
					if (this.<i>5__3 >= multipartContent.nested_content.Count)
					{
						this.<sb>5__2.Append('\r').Append('\n');
						this.<sb>5__2.Append('-').Append('-');
						this.<sb>5__2.Append(multipartContent.boundary);
						this.<sb>5__2.Append('-').Append('-');
						this.<sb>5__2.Append('\r').Append('\n');
						byte[] bytes = Encoding.ASCII.GetBytes(this.<sb>5__2.ToString());
						awaiter = this.stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MultipartContent.<SerializeToStreamAsync>d__8>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						this.<c>5__4 = multipartContent.nested_content[this.<i>5__3];
						IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator = this.<c>5__4.Headers.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<string, IEnumerable<string>> keyValuePair = enumerator.Current;
								this.<sb>5__2.Append(keyValuePair.Key);
								this.<sb>5__2.Append(':').Append(' ');
								IEnumerator<string> enumerator2 = keyValuePair.Value.GetEnumerator();
								try
								{
									while (enumerator2.MoveNext())
									{
										string value = enumerator2.Current;
										this.<sb>5__2.Append(value);
									}
								}
								finally
								{
									if (num < 0 && enumerator2 != null)
									{
										enumerator2.Dispose();
									}
								}
								this.<sb>5__2.Append('\r').Append('\n');
							}
						}
						finally
						{
							if (num < 0 && enumerator != null)
							{
								enumerator.Dispose();
							}
						}
						this.<sb>5__2.Append('\r').Append('\n');
						byte[] bytes = Encoding.ASCII.GetBytes(this.<sb>5__2.ToString());
						this.<sb>5__2.Clear();
						awaiter = this.stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MultipartContent.<SerializeToStreamAsync>d__8>(ref awaiter, ref this);
							return;
						}
						goto IL_1E4;
					}
					IL_3DD:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000185 RID: 389 RVA: 0x00006838 File Offset: 0x00004A38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000CB RID: 203
			public int <>1__state;

			// Token: 0x040000CC RID: 204
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000CD RID: 205
			public MultipartContent <>4__this;

			// Token: 0x040000CE RID: 206
			public Stream stream;

			// Token: 0x040000CF RID: 207
			public TransportContext context;

			// Token: 0x040000D0 RID: 208
			private StringBuilder <sb>5__2;

			// Token: 0x040000D1 RID: 209
			private int <i>5__3;

			// Token: 0x040000D2 RID: 210
			private HttpContent <c>5__4;

			// Token: 0x040000D3 RID: 211
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
