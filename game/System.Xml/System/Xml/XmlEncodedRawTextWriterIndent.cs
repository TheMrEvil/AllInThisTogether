using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200008C RID: 140
	internal class XmlEncodedRawTextWriterIndent : XmlEncodedRawTextWriter
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0001C196 File Offset: 0x0001A396
		public XmlEncodedRawTextWriterIndent(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001C1A7 File Offset: 0x0001A3A7
		public XmlEncodedRawTextWriterIndent(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = base.Settings;
				settings.ReadOnly = false;
				settings.Indent = true;
				settings.IndentChars = this.indentChars;
				settings.NewLineOnAttributes = this.newLineOnAttributes;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001C1ED File Offset: 0x0001A3ED
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001C218 File Offset: 0x0001A418
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.indentLevel++;
			this.mixedContentStack.PushBit(this.mixedContent);
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001C269 File Offset: 0x0001A469
		internal override void StartElementContent()
		{
			if (this.indentLevel == 1 && this.conformanceLevel == ConformanceLevel.Document)
			{
				this.mixedContent = false;
			}
			else
			{
				this.mixedContent = this.mixedContentStack.PeekBit();
			}
			base.StartElementContent();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001C29D File Offset: 0x0001A49D
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001C308 File Offset: 0x0001A508
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.indentLevel--;
			if (!this.mixedContent && this.contentPos != this.bufPos && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			this.mixedContent = this.mixedContentStack.PopBit();
			base.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001C367 File Offset: 0x0001A567
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001C380 File Offset: 0x0001A580
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001C390 File Offset: 0x0001A590
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001C3B5 File Offset: 0x0001A5B5
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001C3DB File Offset: 0x0001A5DB
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001C3EB File Offset: 0x0001A5EB
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001C3FB File Offset: 0x0001A5FB
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001C40C File Offset: 0x0001A60C
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001C41C File Offset: 0x0001A61C
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001C42C File Offset: 0x0001A62C
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001C43E File Offset: 0x0001A63E
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001C450 File Offset: 0x0001A650
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001C460 File Offset: 0x0001A660
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001C474 File Offset: 0x0001A674
		private void Init(XmlWriterSettings settings)
		{
			this.indentLevel = 0;
			this.indentChars = settings.IndentChars;
			this.newLineOnAttributes = settings.NewLineOnAttributes;
			this.mixedContentStack = new BitStack();
			if (this.checkCharacters)
			{
				if (this.newLineOnAttributes)
				{
					base.ValidateContentChars(this.indentChars, "IndentChars", true);
					base.ValidateContentChars(this.newLineChars, "NewLineChars", true);
					return;
				}
				base.ValidateContentChars(this.indentChars, "IndentChars", false);
				if (this.newLineHandling != NewLineHandling.Replace)
				{
					base.ValidateContentChars(this.newLineChars, "NewLineChars", false);
				}
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001C50C File Offset: 0x0001A70C
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001C544 File Offset: 0x0001A744
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32 <WriteStartElementAsync>d__;
			<WriteStartElementAsync>d__.<>4__this = this;
			<WriteStartElementAsync>d__.prefix = prefix;
			<WriteStartElementAsync>d__.localName = localName;
			<WriteStartElementAsync>d__.ns = ns;
			<WriteStartElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync>d__.<>1__state = -1;
			<WriteStartElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32>(ref <WriteStartElementAsync>d__);
			return <WriteStartElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001C604 File Offset: 0x0001A804
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33 <WriteEndElementAsync>d__;
			<WriteEndElementAsync>d__.<>4__this = this;
			<WriteEndElementAsync>d__.prefix = prefix;
			<WriteEndElementAsync>d__.localName = localName;
			<WriteEndElementAsync>d__.ns = ns;
			<WriteEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndElementAsync>d__.<>1__state = -1;
			<WriteEndElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33>(ref <WriteEndElementAsync>d__);
			return <WriteEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001C660 File Offset: 0x0001A860
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34 <WriteFullEndElementAsync>d__;
			<WriteFullEndElementAsync>d__.<>4__this = this;
			<WriteFullEndElementAsync>d__.prefix = prefix;
			<WriteFullEndElementAsync>d__.localName = localName;
			<WriteFullEndElementAsync>d__.ns = ns;
			<WriteFullEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteFullEndElementAsync>d__.<>1__state = -1;
			<WriteFullEndElementAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34>(ref <WriteFullEndElementAsync>d__);
			return <WriteFullEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35 <WriteStartAttributeAsync>d__;
			<WriteStartAttributeAsync>d__.<>4__this = this;
			<WriteStartAttributeAsync>d__.prefix = prefix;
			<WriteStartAttributeAsync>d__.localName = localName;
			<WriteStartAttributeAsync>d__.ns = ns;
			<WriteStartAttributeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync>d__.<>1__state = -1;
			<WriteStartAttributeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35>(ref <WriteStartAttributeAsync>d__);
			return <WriteStartAttributeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C717 File Offset: 0x0001A917
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C730 File Offset: 0x0001A930
		public override Task WriteCommentAsync(string text)
		{
			XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001C77C File Offset: 0x0001A97C
		public override Task WriteProcessingInstructionAsync(string target, string text)
		{
			XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.target = target;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001C7CF File Offset: 0x0001A9CF
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001C7E5 File Offset: 0x0001A9E5
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001C7FB File Offset: 0x0001A9FB
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C812 File Offset: 0x0001AA12
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C828 File Offset: 0x0001AA28
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001C83E File Offset: 0x0001AA3E
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001C856 File Offset: 0x0001AA56
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001C86E File Offset: 0x0001AA6E
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001C884 File Offset: 0x0001AA84
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001C89C File Offset: 0x0001AA9C
		private Task WriteIndentAsync()
		{
			XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48 <WriteIndentAsync>d__;
			<WriteIndentAsync>d__.<>4__this = this;
			<WriteIndentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIndentAsync>d__.<>1__state = -1;
			<WriteIndentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48>(ref <WriteIndentAsync>d__);
			return <WriteIndentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001C8DF File Offset: 0x0001AADF
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0(string name, string pubid, string sysid, string subset)
		{
			return base.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__1(string prefix, string localName, string ns)
		{
			return base.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001C8F7 File Offset: 0x0001AAF7
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__2(string prefix, string localName, string ns)
		{
			return base.WriteEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001C902 File Offset: 0x0001AB02
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__3(string prefix, string localName, string ns)
		{
			return base.WriteFullEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001C90D File Offset: 0x0001AB0D
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__4(string prefix, string localName, string ns)
		{
			return base.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001C918 File Offset: 0x0001AB18
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__5(string text)
		{
			return base.WriteCommentAsync(text);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001C921 File Offset: 0x0001AB21
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__6(string name, string text)
		{
			return base.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x040007C9 RID: 1993
		protected int indentLevel;

		// Token: 0x040007CA RID: 1994
		protected bool newLineOnAttributes;

		// Token: 0x040007CB RID: 1995
		protected string indentChars;

		// Token: 0x040007CC RID: 1996
		protected bool mixedContent;

		// Token: 0x040007CD RID: 1997
		private BitStack mixedContentStack;

		// Token: 0x040007CE RID: 1998
		protected ConformanceLevel conformanceLevel;

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteDocTypeAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x0600051E RID: 1310 RVA: 0x0001C92C File Offset: 0x0001AB2C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_10A;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31>(ref awaiter, ref this);
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
					IL_98:
					awaiter = xmlEncodedRawTextWriterIndent.<>n__0(this.name, this.pubid, this.sysid, this.subset).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteDocTypeAsync>d__31>(ref awaiter, ref this);
						return;
					}
					IL_10A:
					awaiter.GetResult();
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

			// Token: 0x0600051F RID: 1311 RVA: 0x0001CA94 File Offset: 0x0001AC94
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007CF RID: 1999
			public int <>1__state;

			// Token: 0x040007D0 RID: 2000
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007D1 RID: 2001
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007D2 RID: 2002
			public string name;

			// Token: 0x040007D3 RID: 2003
			public string pubid;

			// Token: 0x040007D4 RID: 2004
			public string sysid;

			// Token: 0x040007D5 RID: 2005
			public string subset;

			// Token: 0x040007D6 RID: 2006
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartElementAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06000520 RID: 1312 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_123;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32>(ref awaiter, ref this);
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
					IL_98:
					xmlEncodedRawTextWriterIndent.indentLevel++;
					xmlEncodedRawTextWriterIndent.mixedContentStack.PushBit(xmlEncodedRawTextWriterIndent.mixedContent);
					awaiter = xmlEncodedRawTextWriterIndent.<>n__1(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteStartElementAsync>d__32>(ref awaiter, ref this);
						return;
					}
					IL_123:
					awaiter.GetResult();
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

			// Token: 0x06000521 RID: 1313 RVA: 0x0001CC28 File Offset: 0x0001AE28
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007D7 RID: 2007
			public int <>1__state;

			// Token: 0x040007D8 RID: 2008
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007D9 RID: 2009
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007DA RID: 2010
			public string prefix;

			// Token: 0x040007DB RID: 2011
			public string localName;

			// Token: 0x040007DC RID: 2012
			public string ns;

			// Token: 0x040007DD RID: 2013
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEndElementAsync>d__33 : IAsyncStateMachine
		{
			// Token: 0x06000522 RID: 1314 RVA: 0x0001CC38 File Offset: 0x0001AE38
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_137;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						xmlEncodedRawTextWriterIndent.indentLevel--;
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.contentPos == xmlEncodedRawTextWriterIndent.bufPos || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_BA;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33>(ref awaiter, ref this);
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
					IL_BA:
					xmlEncodedRawTextWriterIndent.mixedContent = xmlEncodedRawTextWriterIndent.mixedContentStack.PopBit();
					awaiter = xmlEncodedRawTextWriterIndent.<>n__2(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteEndElementAsync>d__33>(ref awaiter, ref this);
						return;
					}
					IL_137:
					awaiter.GetResult();
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

			// Token: 0x06000523 RID: 1315 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007DE RID: 2014
			public int <>1__state;

			// Token: 0x040007DF RID: 2015
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007E0 RID: 2016
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007E1 RID: 2017
			public string prefix;

			// Token: 0x040007E2 RID: 2018
			public string localName;

			// Token: 0x040007E3 RID: 2019
			public string ns;

			// Token: 0x040007E4 RID: 2020
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000090 RID: 144
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteFullEndElementAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06000524 RID: 1316 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_137;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						xmlEncodedRawTextWriterIndent.indentLevel--;
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.contentPos == xmlEncodedRawTextWriterIndent.bufPos || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_BA;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34>(ref awaiter, ref this);
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
					IL_BA:
					xmlEncodedRawTextWriterIndent.mixedContent = xmlEncodedRawTextWriterIndent.mixedContentStack.PopBit();
					awaiter = xmlEncodedRawTextWriterIndent.<>n__3(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteFullEndElementAsync>d__34>(ref awaiter, ref this);
						return;
					}
					IL_137:
					awaiter.GetResult();
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

			// Token: 0x06000525 RID: 1317 RVA: 0x0001CF78 File Offset: 0x0001B178
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007E5 RID: 2021
			public int <>1__state;

			// Token: 0x040007E6 RID: 2022
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007E7 RID: 2023
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007E8 RID: 2024
			public string prefix;

			// Token: 0x040007E9 RID: 2025
			public string localName;

			// Token: 0x040007EA RID: 2026
			public string ns;

			// Token: 0x040007EB RID: 2027
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartAttributeAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x06000526 RID: 1318 RVA: 0x0001CF88 File Offset: 0x0001B188
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_F6;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						if (!xmlEncodedRawTextWriterIndent.newLineOnAttributes)
						{
							goto IL_8A;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35>(ref awaiter, ref this);
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
					IL_8A:
					awaiter = xmlEncodedRawTextWriterIndent.<>n__4(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteStartAttributeAsync>d__35>(ref awaiter, ref this);
						return;
					}
					IL_F6:
					awaiter.GetResult();
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

			// Token: 0x06000527 RID: 1319 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007EC RID: 2028
			public int <>1__state;

			// Token: 0x040007ED RID: 2029
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007EE RID: 2030
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007EF RID: 2031
			public string prefix;

			// Token: 0x040007F0 RID: 2032
			public string localName;

			// Token: 0x040007F1 RID: 2033
			public string ns;

			// Token: 0x040007F2 RID: 2034
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentAsync>d__37 : IAsyncStateMachine
		{
			// Token: 0x06000528 RID: 1320 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_F8;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37>(ref awaiter, ref this);
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
					IL_98:
					awaiter = xmlEncodedRawTextWriterIndent.<>n__5(this.text).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteCommentAsync>d__37>(ref awaiter, ref this);
						return;
					}
					IL_F8:
					awaiter.GetResult();
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

			// Token: 0x06000529 RID: 1321 RVA: 0x0001D22C File Offset: 0x0001B42C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007F3 RID: 2035
			public int <>1__state;

			// Token: 0x040007F4 RID: 2036
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007F5 RID: 2037
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007F6 RID: 2038
			public string text;

			// Token: 0x040007F7 RID: 2039
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteProcessingInstructionAsync>d__38 : IAsyncStateMachine
		{
			// Token: 0x0600052A RID: 1322 RVA: 0x0001D23C File Offset: 0x0001B43C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_FE;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						if (xmlEncodedRawTextWriterIndent.mixedContent || xmlEncodedRawTextWriterIndent.textPos == xmlEncodedRawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlEncodedRawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38>(ref awaiter, ref this);
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
					IL_98:
					awaiter = xmlEncodedRawTextWriterIndent.<>n__6(this.target, this.text).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteProcessingInstructionAsync>d__38>(ref awaiter, ref this);
						return;
					}
					IL_FE:
					awaiter.GetResult();
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

			// Token: 0x0600052B RID: 1323 RVA: 0x0001D38C File Offset: 0x0001B58C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007F8 RID: 2040
			public int <>1__state;

			// Token: 0x040007F9 RID: 2041
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007FA RID: 2042
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x040007FB RID: 2043
			public string target;

			// Token: 0x040007FC RID: 2044
			public string text;

			// Token: 0x040007FD RID: 2045
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000094 RID: 148
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteIndentAsync>d__48 : IAsyncStateMachine
		{
			// Token: 0x0600052C RID: 1324 RVA: 0x0001D39C File Offset: 0x0001B59C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriterIndent xmlEncodedRawTextWriterIndent = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_F6;
						}
						xmlEncodedRawTextWriterIndent.CheckAsyncCall();
						awaiter = xmlEncodedRawTextWriterIndent.RawTextAsync(xmlEncodedRawTextWriterIndent.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48>(ref awaiter, ref this);
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
					this.<i>5__2 = xmlEncodedRawTextWriterIndent.indentLevel;
					goto IL_10F;
					IL_F6:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 - 1;
					IL_10F:
					if (this.<i>5__2 > 0)
					{
						awaiter = xmlEncodedRawTextWriterIndent.RawTextAsync(xmlEncodedRawTextWriterIndent.indentChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriterIndent.<WriteIndentAsync>d__48>(ref awaiter, ref this);
							return;
						}
						goto IL_F6;
					}
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

			// Token: 0x0600052D RID: 1325 RVA: 0x0001D510 File Offset: 0x0001B710
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007FE RID: 2046
			public int <>1__state;

			// Token: 0x040007FF RID: 2047
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000800 RID: 2048
			public XmlEncodedRawTextWriterIndent <>4__this;

			// Token: 0x04000801 RID: 2049
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000802 RID: 2050
			private int <i>5__2;
		}
	}
}
