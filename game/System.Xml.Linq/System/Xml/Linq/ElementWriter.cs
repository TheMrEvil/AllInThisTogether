using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	// Token: 0x02000041 RID: 65
	internal struct ElementWriter
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000B99D File Offset: 0x00009B9D
		public ElementWriter(XmlWriter writer)
		{
			this._writer = writer;
			this._resolver = default(NamespaceResolver);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public void WriteElement(XElement e)
		{
			this.PushAncestors(e);
			XElement xelement = e;
			XNode xnode = e;
			for (;;)
			{
				e = (xnode as XElement);
				if (e != null)
				{
					this.WriteStartElement(e);
					if (e.content == null)
					{
						this.WriteEndElement();
					}
					else
					{
						string text = e.content as string;
						if (text == null)
						{
							xnode = ((XNode)e.content).next;
							continue;
						}
						this._writer.WriteString(text);
						this.WriteFullEndElement();
					}
				}
				else
				{
					xnode.WriteTo(this._writer);
				}
				while (xnode != xelement && xnode == xnode.parent.content)
				{
					xnode = xnode.parent;
					this.WriteFullEndElement();
				}
				if (xnode == xelement)
				{
					break;
				}
				xnode = xnode.next;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000BA64 File Offset: 0x00009C64
		public Task WriteElementAsync(XElement e, CancellationToken cancellationToken)
		{
			ElementWriter.<WriteElementAsync>d__4 <WriteElementAsync>d__;
			<WriteElementAsync>d__.<>4__this = this;
			<WriteElementAsync>d__.e = e;
			<WriteElementAsync>d__.cancellationToken = cancellationToken;
			<WriteElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementAsync>d__.<>1__state = -1;
			<WriteElementAsync>d__.<>t__builder.Start<ElementWriter.<WriteElementAsync>d__4>(ref <WriteElementAsync>d__);
			return <WriteElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BABC File Offset: 0x00009CBC
		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = this._resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000BB10 File Offset: 0x00009D10
		private void PushAncestors(XElement e)
		{
			for (;;)
			{
				e = (e.parent as XElement);
				if (e == null)
				{
					break;
				}
				XAttribute xattribute = e.lastAttr;
				if (xattribute != null)
				{
					do
					{
						xattribute = xattribute.next;
						if (xattribute.IsNamespaceDeclaration)
						{
							this._resolver.AddFirst((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
						}
					}
					while (xattribute != e.lastAttr);
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000BB8C File Offset: 0x00009D8C
		private void PushElement(XElement e)
		{
			this._resolver.PushScope();
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					if (xattribute.IsNamespaceDeclaration)
					{
						this._resolver.Add((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
					}
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BC00 File Offset: 0x00009E00
		private void WriteEndElement()
		{
			this._writer.WriteEndElement();
			this._resolver.PopScope();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BC18 File Offset: 0x00009E18
		private Task WriteEndElementAsync(CancellationToken cancellationToken)
		{
			ElementWriter.<WriteEndElementAsync>d__9 <WriteEndElementAsync>d__;
			<WriteEndElementAsync>d__.<>4__this = this;
			<WriteEndElementAsync>d__.cancellationToken = cancellationToken;
			<WriteEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndElementAsync>d__.<>1__state = -1;
			<WriteEndElementAsync>d__.<>t__builder.Start<ElementWriter.<WriteEndElementAsync>d__9>(ref <WriteEndElementAsync>d__);
			return <WriteEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000BC68 File Offset: 0x00009E68
		private void WriteFullEndElement()
		{
			this._writer.WriteFullEndElement();
			this._resolver.PopScope();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BC80 File Offset: 0x00009E80
		private Task WriteFullEndElementAsync(CancellationToken cancellationToken)
		{
			ElementWriter.<WriteFullEndElementAsync>d__11 <WriteFullEndElementAsync>d__;
			<WriteFullEndElementAsync>d__.<>4__this = this;
			<WriteFullEndElementAsync>d__.cancellationToken = cancellationToken;
			<WriteFullEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteFullEndElementAsync>d__.<>1__state = -1;
			<WriteFullEndElementAsync>d__.<>t__builder.Start<ElementWriter.<WriteFullEndElementAsync>d__11>(ref <WriteFullEndElementAsync>d__);
			return <WriteFullEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private void WriteStartElement(XElement e)
		{
			this.PushElement(e);
			XNamespace @namespace = e.Name.Namespace;
			this._writer.WriteStartElement(this.GetPrefixOfNamespace(@namespace, true), e.Name.LocalName, @namespace.NamespaceName);
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					@namespace = xattribute.Name.Namespace;
					string localName = xattribute.Name.LocalName;
					string namespaceName = @namespace.NamespaceName;
					this._writer.WriteAttributeString(this.GetPrefixOfNamespace(@namespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xattribute.Value);
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BD84 File Offset: 0x00009F84
		private Task WriteStartElementAsync(XElement e, CancellationToken cancellationToken)
		{
			ElementWriter.<WriteStartElementAsync>d__13 <WriteStartElementAsync>d__;
			<WriteStartElementAsync>d__.<>4__this = this;
			<WriteStartElementAsync>d__.e = e;
			<WriteStartElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync>d__.<>1__state = -1;
			<WriteStartElementAsync>d__.<>t__builder.Start<ElementWriter.<WriteStartElementAsync>d__13>(ref <WriteStartElementAsync>d__);
			return <WriteStartElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400014C RID: 332
		private XmlWriter _writer;

		// Token: 0x0400014D RID: 333
		private NamespaceResolver _resolver;

		// Token: 0x02000042 RID: 66
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteElementAsync>d__4 : IAsyncStateMachine
		{
			// Token: 0x06000264 RID: 612 RVA: 0x0000BDD4 File Offset: 0x00009FD4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_D8;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_154;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1EA;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_259;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2F8;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_37D;
					default:
						this.<>4__this.PushAncestors(this.e);
						this.<root>5__2 = this.e;
						this.<n>5__3 = this.e;
						break;
					}
					IL_4E:
					this.e = (this.<n>5__3 as XElement);
					if (this.e != null)
					{
						awaiter = this.<>4__this.WriteStartElementAsync(this.e, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<n>5__3.WriteToAsync(this.<>4__this._writer, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
							return;
						}
						goto IL_2F8;
					}
					IL_D8:
					awaiter.GetResult();
					if (this.e.content == null)
					{
						awaiter = this.<>4__this.WriteEndElementAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						string text = this.e.content as string;
						if (text == null)
						{
							this.<n>5__3 = ((XNode)this.e.content).next;
							goto IL_4E;
						}
						this.cancellationToken.ThrowIfCancellationRequested();
						awaiter = this.<>4__this._writer.WriteStringAsync(text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
							return;
						}
						goto IL_1EA;
					}
					IL_154:
					awaiter.GetResult();
					goto IL_384;
					IL_1EA:
					awaiter.GetResult();
					awaiter = this.<>4__this.WriteFullEndElementAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
						return;
					}
					IL_259:
					awaiter.GetResult();
					goto IL_384;
					IL_2F8:
					awaiter.GetResult();
					goto IL_384;
					IL_37D:
					awaiter.GetResult();
					IL_384:
					if (this.<n>5__3 == this.<root>5__2 || this.<n>5__3 != this.<n>5__3.parent.content)
					{
						if (this.<n>5__3 != this.<root>5__2)
						{
							this.<n>5__3 = this.<n>5__3.next;
							goto IL_4E;
						}
					}
					else
					{
						this.<n>5__3 = this.<n>5__3.parent;
						awaiter = this.<>4__this.WriteFullEndElementAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 5;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteElementAsync>d__4>(ref awaiter, ref this);
							return;
						}
						goto IL_37D;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<root>5__2 = null;
					this.<n>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<root>5__2 = null;
				this.<n>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000265 RID: 613 RVA: 0x0000C218 File Offset: 0x0000A418
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400014E RID: 334
			public int <>1__state;

			// Token: 0x0400014F RID: 335
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000150 RID: 336
			public ElementWriter <>4__this;

			// Token: 0x04000151 RID: 337
			public XElement e;

			// Token: 0x04000152 RID: 338
			public CancellationToken cancellationToken;

			// Token: 0x04000153 RID: 339
			private XElement <root>5__2;

			// Token: 0x04000154 RID: 340
			private XNode <n>5__3;

			// Token: 0x04000155 RID: 341
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000043 RID: 67
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEndElementAsync>d__9 : IAsyncStateMachine
		{
			// Token: 0x06000266 RID: 614 RVA: 0x0000C228 File Offset: 0x0000A428
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						awaiter = this.<>4__this._writer.WriteEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteEndElementAsync>d__9>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					this.<>4__this._resolver.PopScope();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000267 RID: 615 RVA: 0x0000C304 File Offset: 0x0000A504
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000156 RID: 342
			public int <>1__state;

			// Token: 0x04000157 RID: 343
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000158 RID: 344
			public CancellationToken cancellationToken;

			// Token: 0x04000159 RID: 345
			public ElementWriter <>4__this;

			// Token: 0x0400015A RID: 346
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000044 RID: 68
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteFullEndElementAsync>d__11 : IAsyncStateMachine
		{
			// Token: 0x06000268 RID: 616 RVA: 0x0000C314 File Offset: 0x0000A514
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.cancellationToken.ThrowIfCancellationRequested();
						awaiter = this.<>4__this._writer.WriteFullEndElementAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteFullEndElementAsync>d__11>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					this.<>4__this._resolver.PopScope();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400015B RID: 347
			public int <>1__state;

			// Token: 0x0400015C RID: 348
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400015D RID: 349
			public CancellationToken cancellationToken;

			// Token: 0x0400015E RID: 350
			public ElementWriter <>4__this;

			// Token: 0x0400015F RID: 351
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000045 RID: 69
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartElementAsync>d__13 : IAsyncStateMachine
		{
			// Token: 0x0600026A RID: 618 RVA: 0x0000C400 File Offset: 0x0000A600
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					XNamespace @namespace;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_1BD;
						}
						this.<>4__this.PushElement(this.e);
						@namespace = this.e.Name.Namespace;
						awaiter = this.<>4__this._writer.WriteStartElementAsync(this.<>4__this.GetPrefixOfNamespace(@namespace, true), this.e.Name.LocalName, @namespace.NamespaceName).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteStartElementAsync>d__13>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					this.<a>5__2 = this.e.lastAttr;
					if (this.<a>5__2 == null)
					{
						goto IL_1DA;
					}
					IL_E3:
					this.<a>5__2 = this.<a>5__2.next;
					@namespace = this.<a>5__2.Name.Namespace;
					string localName = this.<a>5__2.Name.LocalName;
					string namespaceName = @namespace.NamespaceName;
					awaiter = this.<>4__this._writer.WriteAttributeStringAsync(this.<>4__this.GetPrefixOfNamespace(@namespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, this.<a>5__2.Value).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ElementWriter.<WriteStartElementAsync>d__13>(ref awaiter, ref this);
						return;
					}
					IL_1BD:
					awaiter.GetResult();
					if (this.<a>5__2 != this.e.lastAttr)
					{
						goto IL_E3;
					}
					IL_1DA:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<a>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<a>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600026B RID: 619 RVA: 0x0000C640 File Offset: 0x0000A840
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000160 RID: 352
			public int <>1__state;

			// Token: 0x04000161 RID: 353
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000162 RID: 354
			public ElementWriter <>4__this;

			// Token: 0x04000163 RID: 355
			public XElement e;

			// Token: 0x04000164 RID: 356
			private XAttribute <a>5__2;

			// Token: 0x04000165 RID: 357
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
