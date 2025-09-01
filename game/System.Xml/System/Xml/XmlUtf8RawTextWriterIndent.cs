using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000136 RID: 310
	internal class XmlUtf8RawTextWriterIndent : XmlUtf8RawTextWriter
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0004BA2E File Offset: 0x00049C2E
		public XmlUtf8RawTextWriterIndent(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0004BA3F File Offset: 0x00049C3F
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

		// Token: 0x06000B11 RID: 2833 RVA: 0x0004BA74 File Offset: 0x00049C74
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0004BAA0 File Offset: 0x00049CA0
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

		// Token: 0x06000B13 RID: 2835 RVA: 0x0004BAF1 File Offset: 0x00049CF1
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

		// Token: 0x06000B14 RID: 2836 RVA: 0x0004BB25 File Offset: 0x00049D25
		internal override void OnRootElement(ConformanceLevel currentConformanceLevel)
		{
			this.conformanceLevel = currentConformanceLevel;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0004BB30 File Offset: 0x00049D30
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

		// Token: 0x06000B16 RID: 2838 RVA: 0x0004BB90 File Offset: 0x00049D90
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

		// Token: 0x06000B17 RID: 2839 RVA: 0x0004BBEF File Offset: 0x00049DEF
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.newLineOnAttributes)
			{
				this.WriteIndent();
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0004BC08 File Offset: 0x00049E08
		public override void WriteCData(string text)
		{
			this.mixedContent = true;
			base.WriteCData(text);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0004BC18 File Offset: 0x00049E18
		public override void WriteComment(string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteComment(text);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004BC3D File Offset: 0x00049E3D
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (!this.mixedContent && this.textPos != this.bufPos)
			{
				this.WriteIndent();
			}
			base.WriteProcessingInstruction(target, text);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0004BC63 File Offset: 0x00049E63
		public override void WriteEntityRef(string name)
		{
			this.mixedContent = true;
			base.WriteEntityRef(name);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0004BC73 File Offset: 0x00049E73
		public override void WriteCharEntity(char ch)
		{
			this.mixedContent = true;
			base.WriteCharEntity(ch);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0004BC83 File Offset: 0x00049E83
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.mixedContent = true;
			base.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004BC94 File Offset: 0x00049E94
		public override void WriteWhitespace(string ws)
		{
			this.mixedContent = true;
			base.WriteWhitespace(ws);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004BCA4 File Offset: 0x00049EA4
		public override void WriteString(string text)
		{
			this.mixedContent = true;
			base.WriteString(text);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0004BCB4 File Offset: 0x00049EB4
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteChars(buffer, index, count);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0004BCC6 File Offset: 0x00049EC6
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0004BCD8 File Offset: 0x00049ED8
		public override void WriteRaw(string data)
		{
			this.mixedContent = true;
			base.WriteRaw(data);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004BCE8 File Offset: 0x00049EE8
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.mixedContent = true;
			base.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004BCFC File Offset: 0x00049EFC
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

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004BD94 File Offset: 0x00049F94
		private void WriteIndent()
		{
			base.RawText(this.newLineChars);
			for (int i = this.indentLevel; i > 0; i--)
			{
				base.RawText(this.indentChars);
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004BDCC File Offset: 0x00049FCC
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004BE30 File Offset: 0x0004A030
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31 <WriteStartElementAsync>d__;
			<WriteStartElementAsync>d__.<>4__this = this;
			<WriteStartElementAsync>d__.prefix = prefix;
			<WriteStartElementAsync>d__.localName = localName;
			<WriteStartElementAsync>d__.ns = ns;
			<WriteStartElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync>d__.<>1__state = -1;
			<WriteStartElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31>(ref <WriteStartElementAsync>d__);
			return <WriteStartElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0004BE8C File Offset: 0x0004A08C
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32 <WriteEndElementAsync>d__;
			<WriteEndElementAsync>d__.<>4__this = this;
			<WriteEndElementAsync>d__.prefix = prefix;
			<WriteEndElementAsync>d__.localName = localName;
			<WriteEndElementAsync>d__.ns = ns;
			<WriteEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndElementAsync>d__.<>1__state = -1;
			<WriteEndElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32>(ref <WriteEndElementAsync>d__);
			return <WriteEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0004BEE8 File Offset: 0x0004A0E8
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33 <WriteFullEndElementAsync>d__;
			<WriteFullEndElementAsync>d__.<>4__this = this;
			<WriteFullEndElementAsync>d__.prefix = prefix;
			<WriteFullEndElementAsync>d__.localName = localName;
			<WriteFullEndElementAsync>d__.ns = ns;
			<WriteFullEndElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteFullEndElementAsync>d__.<>1__state = -1;
			<WriteFullEndElementAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33>(ref <WriteFullEndElementAsync>d__);
			return <WriteFullEndElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004BF44 File Offset: 0x0004A144
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34 <WriteStartAttributeAsync>d__;
			<WriteStartAttributeAsync>d__.<>4__this = this;
			<WriteStartAttributeAsync>d__.prefix = prefix;
			<WriteStartAttributeAsync>d__.localName = localName;
			<WriteStartAttributeAsync>d__.ns = ns;
			<WriteStartAttributeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync>d__.<>1__state = -1;
			<WriteStartAttributeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34>(ref <WriteStartAttributeAsync>d__);
			return <WriteStartAttributeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004BF9F File Offset: 0x0004A19F
		public override Task WriteCDataAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCDataAsync(text);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004BFB8 File Offset: 0x0004A1B8
		public override Task WriteCommentAsync(string text)
		{
			XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0004C004 File Offset: 0x0004A204
		public override Task WriteProcessingInstructionAsync(string target, string text)
		{
			XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.target = target;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0004C057 File Offset: 0x0004A257
		public override Task WriteEntityRefAsync(string name)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteEntityRefAsync(name);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0004C06D File Offset: 0x0004A26D
		public override Task WriteCharEntityAsync(char ch)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0004C083 File Offset: 0x0004A283
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0004C09A File Offset: 0x0004A29A
		public override Task WriteWhitespaceAsync(string ws)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0004C0B0 File Offset: 0x0004A2B0
		public override Task WriteStringAsync(string text)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteStringAsync(text);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0004C0C6 File Offset: 0x0004A2C6
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004C0DE File Offset: 0x0004A2DE
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0004C0F6 File Offset: 0x0004A2F6
		public override Task WriteRawAsync(string data)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteRawAsync(data);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0004C10C File Offset: 0x0004A30C
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			base.CheckAsyncCall();
			this.mixedContent = true;
			return base.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0004C124 File Offset: 0x0004A324
		private Task WriteIndentAsync()
		{
			XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47 <WriteIndentAsync>d__;
			<WriteIndentAsync>d__.<>4__this = this;
			<WriteIndentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteIndentAsync>d__.<>1__state = -1;
			<WriteIndentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47>(ref <WriteIndentAsync>d__);
			return <WriteIndentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0004C167 File Offset: 0x0004A367
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0(string name, string pubid, string sysid, string subset)
		{
			return base.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0004C174 File Offset: 0x0004A374
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__1(string prefix, string localName, string ns)
		{
			return base.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0004C17F File Offset: 0x0004A37F
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__2(string prefix, string localName, string ns)
		{
			return base.WriteEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0004C18A File Offset: 0x0004A38A
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__3(string prefix, string localName, string ns)
		{
			return base.WriteFullEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0004C195 File Offset: 0x0004A395
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__4(string prefix, string localName, string ns)
		{
			return base.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0004C1A0 File Offset: 0x0004A3A0
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__5(string text)
		{
			return base.WriteCommentAsync(text);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0004C1A9 File Offset: 0x0004A3A9
		[DebuggerHidden]
		[CompilerGenerated]
		private Task <>n__6(string name, string text)
		{
			return base.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x04000CE9 RID: 3305
		protected int indentLevel;

		// Token: 0x04000CEA RID: 3306
		protected bool newLineOnAttributes;

		// Token: 0x04000CEB RID: 3307
		protected string indentChars;

		// Token: 0x04000CEC RID: 3308
		protected bool mixedContent;

		// Token: 0x04000CED RID: 3309
		private BitStack mixedContentStack;

		// Token: 0x04000CEE RID: 3310
		protected ConformanceLevel conformanceLevel;

		// Token: 0x02000137 RID: 311
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteDocTypeAsync>d__30 : IAsyncStateMachine
		{
			// Token: 0x06000B3F RID: 2879 RVA: 0x0004C1B4 File Offset: 0x0004A3B4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30>(ref awaiter, ref this);
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
					awaiter = xmlUtf8RawTextWriterIndent.<>n__0(this.name, this.pubid, this.sysid, this.subset).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteDocTypeAsync>d__30>(ref awaiter, ref this);
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

			// Token: 0x06000B40 RID: 2880 RVA: 0x0004C31C File Offset: 0x0004A51C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CEF RID: 3311
			public int <>1__state;

			// Token: 0x04000CF0 RID: 3312
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CF1 RID: 3313
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000CF2 RID: 3314
			public string name;

			// Token: 0x04000CF3 RID: 3315
			public string pubid;

			// Token: 0x04000CF4 RID: 3316
			public string sysid;

			// Token: 0x04000CF5 RID: 3317
			public string subset;

			// Token: 0x04000CF6 RID: 3318
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000138 RID: 312
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartElementAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x06000B41 RID: 2881 RVA: 0x0004C32C File Offset: 0x0004A52C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31>(ref awaiter, ref this);
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
					xmlUtf8RawTextWriterIndent.indentLevel++;
					xmlUtf8RawTextWriterIndent.mixedContentStack.PushBit(xmlUtf8RawTextWriterIndent.mixedContent);
					awaiter = xmlUtf8RawTextWriterIndent.<>n__1(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteStartElementAsync>d__31>(ref awaiter, ref this);
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

			// Token: 0x06000B42 RID: 2882 RVA: 0x0004C4B0 File Offset: 0x0004A6B0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CF7 RID: 3319
			public int <>1__state;

			// Token: 0x04000CF8 RID: 3320
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CF9 RID: 3321
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000CFA RID: 3322
			public string prefix;

			// Token: 0x04000CFB RID: 3323
			public string localName;

			// Token: 0x04000CFC RID: 3324
			public string ns;

			// Token: 0x04000CFD RID: 3325
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000139 RID: 313
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEndElementAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06000B43 RID: 2883 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						xmlUtf8RawTextWriterIndent.indentLevel--;
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.contentPos == xmlUtf8RawTextWriterIndent.bufPos || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_BA;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32>(ref awaiter, ref this);
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
					xmlUtf8RawTextWriterIndent.mixedContent = xmlUtf8RawTextWriterIndent.mixedContentStack.PopBit();
					awaiter = xmlUtf8RawTextWriterIndent.<>n__2(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteEndElementAsync>d__32>(ref awaiter, ref this);
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

			// Token: 0x06000B44 RID: 2884 RVA: 0x0004C658 File Offset: 0x0004A858
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CFE RID: 3326
			public int <>1__state;

			// Token: 0x04000CFF RID: 3327
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D00 RID: 3328
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D01 RID: 3329
			public string prefix;

			// Token: 0x04000D02 RID: 3330
			public string localName;

			// Token: 0x04000D03 RID: 3331
			public string ns;

			// Token: 0x04000D04 RID: 3332
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200013A RID: 314
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteFullEndElementAsync>d__33 : IAsyncStateMachine
		{
			// Token: 0x06000B45 RID: 2885 RVA: 0x0004C668 File Offset: 0x0004A868
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						xmlUtf8RawTextWriterIndent.indentLevel--;
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.contentPos == xmlUtf8RawTextWriterIndent.bufPos || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_BA;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33>(ref awaiter, ref this);
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
					xmlUtf8RawTextWriterIndent.mixedContent = xmlUtf8RawTextWriterIndent.mixedContentStack.PopBit();
					awaiter = xmlUtf8RawTextWriterIndent.<>n__3(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteFullEndElementAsync>d__33>(ref awaiter, ref this);
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

			// Token: 0x06000B46 RID: 2886 RVA: 0x0004C800 File Offset: 0x0004AA00
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D05 RID: 3333
			public int <>1__state;

			// Token: 0x04000D06 RID: 3334
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D07 RID: 3335
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D08 RID: 3336
			public string prefix;

			// Token: 0x04000D09 RID: 3337
			public string localName;

			// Token: 0x04000D0A RID: 3338
			public string ns;

			// Token: 0x04000D0B RID: 3339
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200013B RID: 315
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartAttributeAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06000B47 RID: 2887 RVA: 0x0004C810 File Offset: 0x0004AA10
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						if (!xmlUtf8RawTextWriterIndent.newLineOnAttributes)
						{
							goto IL_8A;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34>(ref awaiter, ref this);
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
					awaiter = xmlUtf8RawTextWriterIndent.<>n__4(this.prefix, this.localName, this.ns).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteStartAttributeAsync>d__34>(ref awaiter, ref this);
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

			// Token: 0x06000B48 RID: 2888 RVA: 0x0004C958 File Offset: 0x0004AB58
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D0C RID: 3340
			public int <>1__state;

			// Token: 0x04000D0D RID: 3341
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D0E RID: 3342
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D0F RID: 3343
			public string prefix;

			// Token: 0x04000D10 RID: 3344
			public string localName;

			// Token: 0x04000D11 RID: 3345
			public string ns;

			// Token: 0x04000D12 RID: 3346
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200013C RID: 316
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentAsync>d__36 : IAsyncStateMachine
		{
			// Token: 0x06000B49 RID: 2889 RVA: 0x0004C968 File Offset: 0x0004AB68
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36>(ref awaiter, ref this);
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
					awaiter = xmlUtf8RawTextWriterIndent.<>n__5(this.text).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteCommentAsync>d__36>(ref awaiter, ref this);
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

			// Token: 0x06000B4A RID: 2890 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D13 RID: 3347
			public int <>1__state;

			// Token: 0x04000D14 RID: 3348
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D15 RID: 3349
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D16 RID: 3350
			public string text;

			// Token: 0x04000D17 RID: 3351
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200013D RID: 317
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteProcessingInstructionAsync>d__37 : IAsyncStateMachine
		{
			// Token: 0x06000B4B RID: 2891 RVA: 0x0004CAC4 File Offset: 0x0004ACC4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						if (xmlUtf8RawTextWriterIndent.mixedContent || xmlUtf8RawTextWriterIndent.textPos == xmlUtf8RawTextWriterIndent.bufPos)
						{
							goto IL_98;
						}
						awaiter = xmlUtf8RawTextWriterIndent.WriteIndentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37>(ref awaiter, ref this);
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
					awaiter = xmlUtf8RawTextWriterIndent.<>n__6(this.target, this.text).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteProcessingInstructionAsync>d__37>(ref awaiter, ref this);
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

			// Token: 0x06000B4C RID: 2892 RVA: 0x0004CC14 File Offset: 0x0004AE14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D18 RID: 3352
			public int <>1__state;

			// Token: 0x04000D19 RID: 3353
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D1A RID: 3354
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D1B RID: 3355
			public string target;

			// Token: 0x04000D1C RID: 3356
			public string text;

			// Token: 0x04000D1D RID: 3357
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200013E RID: 318
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteIndentAsync>d__47 : IAsyncStateMachine
		{
			// Token: 0x06000B4D RID: 2893 RVA: 0x0004CC24 File Offset: 0x0004AE24
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriterIndent xmlUtf8RawTextWriterIndent = this.<>4__this;
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
						xmlUtf8RawTextWriterIndent.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriterIndent.RawTextAsync(xmlUtf8RawTextWriterIndent.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47>(ref awaiter, ref this);
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
					this.<i>5__2 = xmlUtf8RawTextWriterIndent.indentLevel;
					goto IL_10F;
					IL_F6:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 - 1;
					IL_10F:
					if (this.<i>5__2 > 0)
					{
						awaiter = xmlUtf8RawTextWriterIndent.RawTextAsync(xmlUtf8RawTextWriterIndent.indentChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriterIndent.<WriteIndentAsync>d__47>(ref awaiter, ref this);
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

			// Token: 0x06000B4E RID: 2894 RVA: 0x0004CD98 File Offset: 0x0004AF98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000D1E RID: 3358
			public int <>1__state;

			// Token: 0x04000D1F RID: 3359
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000D20 RID: 3360
			public XmlUtf8RawTextWriterIndent <>4__this;

			// Token: 0x04000D21 RID: 3361
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000D22 RID: 3362
			private int <i>5__2;
		}
	}
}
