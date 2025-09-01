using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000068 RID: 104
	internal class XmlCharCheckingReader : XmlWrappingReader
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00011CAC File Offset: 0x0000FEAC
		internal XmlCharCheckingReader(XmlReader reader, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing) : base(reader)
		{
			this.state = XmlCharCheckingReader.State.Initial;
			this.checkCharacters = checkCharacters;
			this.ignoreWhitespace = ignoreWhitespace;
			this.ignoreComments = ignoreComments;
			this.ignorePis = ignorePis;
			this.dtdProcessing = dtdProcessing;
			this.lastNodeType = XmlNodeType.None;
			if (checkCharacters)
			{
				this.xmlCharType = XmlCharType.Instance;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00011D04 File Offset: 0x0000FF04
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.reader.Settings;
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				else
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (this.checkCharacters)
				{
					xmlReaderSettings.CheckCharacters = true;
				}
				if (this.ignoreWhitespace)
				{
					xmlReaderSettings.IgnoreWhitespace = true;
				}
				if (this.ignoreComments)
				{
					xmlReaderSettings.IgnoreComments = true;
				}
				if (this.ignorePis)
				{
					xmlReaderSettings.IgnoreProcessingInstructions = true;
				}
				if (this.dtdProcessing != (DtdProcessing)(-1))
				{
					xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011D88 File Offset: 0x0000FF88
		public override bool MoveToAttribute(string name)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011DA5 File Offset: 0x0000FFA5
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011DC3 File Offset: 0x0000FFC3
		public override void MoveToAttribute(int i)
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public override bool MoveToFirstAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011DFC File Offset: 0x0000FFFC
		public override bool MoveToNextAttribute()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011E18 File Offset: 0x00010018
		public override bool MoveToElement()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.MoveToElement();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011E34 File Offset: 0x00010034
		public override bool Read()
		{
			switch (this.state)
			{
			case XmlCharCheckingReader.State.Initial:
				this.state = XmlCharCheckingReader.State.Interactive;
				if (this.reader.ReadState != ReadState.Initial)
				{
					goto IL_55;
				}
				break;
			case XmlCharCheckingReader.State.InReadBinary:
				this.FinishReadBinary();
				this.state = XmlCharCheckingReader.State.Interactive;
				break;
			case XmlCharCheckingReader.State.Error:
				return false;
			case XmlCharCheckingReader.State.Interactive:
				break;
			default:
				return false;
			}
			if (!this.reader.Read())
			{
				return false;
			}
			IL_55:
			XmlNodeType nodeType = this.reader.NodeType;
			if (!this.checkCharacters)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
					if (this.ignorePis)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Comment:
					if (this.ignoreComments)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.DocumentType:
					if (this.dtdProcessing == DtdProcessing.Prohibit)
					{
						this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
					}
					else if (this.dtdProcessing == DtdProcessing.Ignore)
					{
						return this.Read();
					}
					break;
				case XmlNodeType.Whitespace:
					if (this.ignoreWhitespace)
					{
						return this.Read();
					}
					break;
				}
				return true;
			}
			switch (nodeType)
			{
			case XmlNodeType.Element:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
					if (this.reader.MoveToFirstAttribute())
					{
						do
						{
							this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
							this.CheckCharacters(this.reader.Value);
						}
						while (this.reader.MoveToNextAttribute());
						this.reader.MoveToElement();
					}
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.EntityReference:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
				}
				break;
			case XmlNodeType.ProcessingInstruction:
				if (this.ignorePis)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.Comment:
				if (this.ignoreComments)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckCharacters(this.reader.Value);
				}
				break;
			case XmlNodeType.DocumentType:
				if (this.dtdProcessing == DtdProcessing.Prohibit)
				{
					this.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
				}
				else if (this.dtdProcessing == DtdProcessing.Ignore)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Name);
					this.CheckCharacters(this.reader.Value);
					string attribute = this.reader.GetAttribute("SYSTEM");
					if (attribute != null)
					{
						this.CheckCharacters(attribute);
					}
					attribute = this.reader.GetAttribute("PUBLIC");
					int invCharIndex;
					if (attribute != null && (invCharIndex = this.xmlCharType.IsPublicId(attribute)) >= 0)
					{
						this.Throw("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(attribute, invCharIndex));
					}
				}
				break;
			case XmlNodeType.Whitespace:
				if (this.ignoreWhitespace)
				{
					return this.Read();
				}
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.SignificantWhitespace:
				if (this.checkCharacters)
				{
					this.CheckWhitespace(this.reader.Value);
				}
				break;
			case XmlNodeType.EndElement:
				if (this.checkCharacters)
				{
					this.ValidateQName(this.reader.Prefix, this.reader.LocalName);
				}
				break;
			}
			this.lastNodeType = nodeType;
			return true;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000121C8 File Offset: 0x000103C8
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XmlCharCheckingReader.State.Initial:
					if (this.reader.ReadState != ReadState.Closed)
					{
						return ReadState.Initial;
					}
					return ReadState.Closed;
				case XmlCharCheckingReader.State.Error:
					return ReadState.Error;
				}
				return this.reader.ReadState;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012213 File Offset: 0x00010413
		public override bool ReadAttributeValue()
		{
			if (this.state == XmlCharCheckingReader.State.InReadBinary)
			{
				this.FinishReadBinary();
			}
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00012234 File Offset: 0x00010434
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000122C0 File Offset: 0x000104C0
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001234C File Offset: 0x0001054C
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBase64(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBase64(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012418 File Offset: 0x00010618
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XmlCharCheckingReader.State.InReadBinary)
			{
				if (base.CanReadBinaryContent && !this.checkCharacters)
				{
					this.readBinaryHelper = null;
					this.state = XmlCharCheckingReader.State.InReadBinary;
					return base.ReadElementContentAsBinHex(buffer, index, count);
				}
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
			}
			else if (this.readBinaryHelper == null)
			{
				return base.ReadElementContentAsBinHex(buffer, index, count);
			}
			this.state = XmlCharCheckingReader.State.Interactive;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.state = XmlCharCheckingReader.State.InReadBinary;
			return result;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000124E2 File Offset: 0x000106E2
		private void Throw(string res, string arg)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, arg, null);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000124F3 File Offset: 0x000106F3
		private void Throw(string res, string[] args)
		{
			this.state = XmlCharCheckingReader.State.Error;
			throw new XmlException(res, args, null);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00012504 File Offset: 0x00010704
		private void CheckWhitespace(string value)
		{
			int invCharIndex;
			if ((invCharIndex = this.xmlCharType.IsOnlyWhitespaceWithPos(value)) != -1)
			{
				this.Throw("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(value, invCharIndex));
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012534 File Offset: 0x00010734
		private void ValidateQName(string name)
		{
			string text;
			string text2;
			ValidateNames.ParseQNameThrow(name, out text, out text2);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001254C File Offset: 0x0001074C
		private void ValidateQName(string prefix, string localName)
		{
			try
			{
				if (prefix.Length > 0)
				{
					ValidateNames.ParseNCNameThrow(prefix);
				}
				ValidateNames.ParseNCNameThrow(localName);
			}
			catch
			{
				this.state = XmlCharCheckingReader.State.Error;
				throw;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001258C File Offset: 0x0001078C
		private void CheckCharacters(string value)
		{
			XmlConvert.VerifyCharData(value, ExceptionType.ArgumentException, ExceptionType.XmlException);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012596 File Offset: 0x00010796
		private void FinishReadBinary()
		{
			this.state = XmlCharCheckingReader.State.Interactive;
			if (this.readBinaryHelper != null)
			{
				this.readBinaryHelper.Finish();
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000125B4 File Offset: 0x000107B4
		public override Task<bool> ReadAsync()
		{
			XmlCharCheckingReader.<ReadAsync>d__36 <ReadAsync>d__;
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadAsync>d__36>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000125F8 File Offset: 0x000107F8
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadContentAsBase64Async>d__37 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadContentAsBase64Async>d__37>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012654 File Offset: 0x00010854
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000126B0 File Offset: 0x000108B0
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001270C File Offset: 0x0001090C
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012768 File Offset: 0x00010968
		private Task FinishReadBinaryAsync()
		{
			XmlCharCheckingReader.<FinishReadBinaryAsync>d__41 <FinishReadBinaryAsync>d__;
			<FinishReadBinaryAsync>d__.<>4__this = this;
			<FinishReadBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadBinaryAsync>d__.<>1__state = -1;
			<FinishReadBinaryAsync>d__.<>t__builder.Start<XmlCharCheckingReader.<FinishReadBinaryAsync>d__41>(ref <FinishReadBinaryAsync>d__);
			return <FinishReadBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000127AB File Offset: 0x000109AB
		[CompilerGenerated]
		[DebuggerHidden]
		private bool <>n__0()
		{
			return base.CanReadBinaryContent;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000127B3 File Offset: 0x000109B3
		[CompilerGenerated]
		[DebuggerHidden]
		private Task<int> <>n__1(byte[] buffer, int index, int count)
		{
			return base.ReadContentAsBase64Async(buffer, index, count);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000127BE File Offset: 0x000109BE
		[CompilerGenerated]
		[DebuggerHidden]
		private Task<int> <>n__2(byte[] buffer, int index, int count)
		{
			return base.ReadContentAsBinHexAsync(buffer, index, count);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000127C9 File Offset: 0x000109C9
		[DebuggerHidden]
		[CompilerGenerated]
		private Task<int> <>n__3(byte[] buffer, int index, int count)
		{
			return base.ReadElementContentAsBase64Async(buffer, index, count);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000127D4 File Offset: 0x000109D4
		[CompilerGenerated]
		[DebuggerHidden]
		private Task<int> <>n__4(byte[] buffer, int index, int count)
		{
			return base.ReadElementContentAsBinHexAsync(buffer, index, count);
		}

		// Token: 0x040006BB RID: 1723
		private XmlCharCheckingReader.State state;

		// Token: 0x040006BC RID: 1724
		private bool checkCharacters;

		// Token: 0x040006BD RID: 1725
		private bool ignoreWhitespace;

		// Token: 0x040006BE RID: 1726
		private bool ignoreComments;

		// Token: 0x040006BF RID: 1727
		private bool ignorePis;

		// Token: 0x040006C0 RID: 1728
		private DtdProcessing dtdProcessing;

		// Token: 0x040006C1 RID: 1729
		private XmlNodeType lastNodeType;

		// Token: 0x040006C2 RID: 1730
		private XmlCharType xmlCharType;

		// Token: 0x040006C3 RID: 1731
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x02000069 RID: 105
		private enum State
		{
			// Token: 0x040006C5 RID: 1733
			Initial,
			// Token: 0x040006C6 RID: 1734
			InReadBinary,
			// Token: 0x040006C7 RID: 1735
			Error,
			// Token: 0x040006C8 RID: 1736
			Interactive
		}

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsync>d__36 : IAsyncStateMachine
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x000127E0 File Offset: 0x000109E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_15F;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_230;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2A9;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_322;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3B3;
					case 6:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_50C;
					case 7:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5AC;
					case 8:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_654;
					case 9:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_707;
					case 10:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_811;
					case 11:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_890;
					case 12:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_918;
					default:
						switch (xmlCharCheckingReader.state)
						{
						case XmlCharCheckingReader.State.Initial:
							xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
							if (xmlCharCheckingReader.reader.ReadState == ReadState.Initial)
							{
								goto IL_F9;
							}
							goto IL_176;
						case XmlCharCheckingReader.State.InReadBinary:
							awaiter = xmlCharCheckingReader.FinishReadBinaryAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter, ref this);
								return;
							}
							break;
						case XmlCharCheckingReader.State.Error:
							result = false;
							goto IL_978;
						case XmlCharCheckingReader.State.Interactive:
							goto IL_F9;
						default:
							result = false;
							goto IL_978;
						}
						break;
					}
					awaiter.GetResult();
					xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
					IL_F9:
					awaiter2 = xmlCharCheckingReader.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
						return;
					}
					IL_15F:
					if (!awaiter2.GetResult())
					{
						result = false;
						goto IL_978;
					}
					IL_176:
					this.<nodeType>5__2 = xmlCharCheckingReader.reader.NodeType;
					if (!xmlCharCheckingReader.checkCharacters)
					{
						switch (this.<nodeType>5__2)
						{
						case XmlNodeType.ProcessingInstruction:
							if (xmlCharCheckingReader.ignorePis)
							{
								awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 4;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
									return;
								}
								goto IL_322;
							}
							break;
						case XmlNodeType.Comment:
							if (xmlCharCheckingReader.ignoreComments)
							{
								awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
									return;
								}
								goto IL_230;
							}
							break;
						case XmlNodeType.DocumentType:
							if (xmlCharCheckingReader.dtdProcessing == DtdProcessing.Prohibit)
							{
								xmlCharCheckingReader.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
							}
							else if (xmlCharCheckingReader.dtdProcessing == DtdProcessing.Ignore)
							{
								awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 5;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
									return;
								}
								goto IL_3B3;
							}
							break;
						case XmlNodeType.Whitespace:
							if (xmlCharCheckingReader.ignoreWhitespace)
							{
								awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 3;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
									return;
								}
								goto IL_2A9;
							}
							break;
						}
						result = true;
						goto IL_978;
					}
					switch (this.<nodeType>5__2)
					{
					case XmlNodeType.Element:
						if (!xmlCharCheckingReader.checkCharacters)
						{
							goto IL_94F;
						}
						xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Prefix, xmlCharCheckingReader.reader.LocalName);
						if (xmlCharCheckingReader.reader.MoveToFirstAttribute())
						{
							do
							{
								xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Prefix, xmlCharCheckingReader.reader.LocalName);
								xmlCharCheckingReader.CheckCharacters(xmlCharCheckingReader.reader.Value);
							}
							while (xmlCharCheckingReader.reader.MoveToNextAttribute());
							xmlCharCheckingReader.reader.MoveToElement();
							goto IL_94F;
						}
						goto IL_94F;
					case XmlNodeType.Attribute:
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
						goto IL_94F;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						if (!xmlCharCheckingReader.checkCharacters)
						{
							goto IL_94F;
						}
						awaiter3 = xmlCharCheckingReader.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter3, ref this);
							return;
						}
						goto IL_50C;
					case XmlNodeType.EntityReference:
						if (xmlCharCheckingReader.checkCharacters)
						{
							xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Name);
							goto IL_94F;
						}
						goto IL_94F;
					case XmlNodeType.ProcessingInstruction:
						if (xmlCharCheckingReader.ignorePis)
						{
							awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 7;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
								return;
							}
							goto IL_5AC;
						}
						else
						{
							if (xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Name);
								xmlCharCheckingReader.CheckCharacters(xmlCharCheckingReader.reader.Value);
								goto IL_94F;
							}
							goto IL_94F;
						}
						break;
					case XmlNodeType.Comment:
						if (xmlCharCheckingReader.ignoreComments)
						{
							awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 8;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
								return;
							}
							goto IL_654;
						}
						else
						{
							if (xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.CheckCharacters(xmlCharCheckingReader.reader.Value);
								goto IL_94F;
							}
							goto IL_94F;
						}
						break;
					case XmlNodeType.DocumentType:
					{
						if (xmlCharCheckingReader.dtdProcessing == DtdProcessing.Prohibit)
						{
							xmlCharCheckingReader.Throw("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.", string.Empty);
						}
						else if (xmlCharCheckingReader.dtdProcessing == DtdProcessing.Ignore)
						{
							awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 9;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
								return;
							}
							goto IL_707;
						}
						if (!xmlCharCheckingReader.checkCharacters)
						{
							goto IL_94F;
						}
						xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Name);
						xmlCharCheckingReader.CheckCharacters(xmlCharCheckingReader.reader.Value);
						string attribute = xmlCharCheckingReader.reader.GetAttribute("SYSTEM");
						if (attribute != null)
						{
							xmlCharCheckingReader.CheckCharacters(attribute);
						}
						attribute = xmlCharCheckingReader.reader.GetAttribute("PUBLIC");
						int invCharIndex;
						if (attribute != null && (invCharIndex = xmlCharCheckingReader.xmlCharType.IsPublicId(attribute)) >= 0)
						{
							xmlCharCheckingReader.Throw("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(attribute, invCharIndex));
							goto IL_94F;
						}
						goto IL_94F;
					}
					case XmlNodeType.Whitespace:
						if (xmlCharCheckingReader.ignoreWhitespace)
						{
							awaiter2 = xmlCharCheckingReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 10;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter2, ref this);
								return;
							}
							goto IL_811;
						}
						else
						{
							if (!xmlCharCheckingReader.checkCharacters)
							{
								goto IL_94F;
							}
							awaiter3 = xmlCharCheckingReader.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								this.<>1__state = 11;
								this.<>u__3 = awaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter3, ref this);
								return;
							}
							goto IL_890;
						}
						break;
					case XmlNodeType.SignificantWhitespace:
						if (!xmlCharCheckingReader.checkCharacters)
						{
							goto IL_94F;
						}
						awaiter3 = xmlCharCheckingReader.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 12;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadAsync>d__36>(ref awaiter3, ref this);
							return;
						}
						goto IL_918;
					case XmlNodeType.EndElement:
						if (xmlCharCheckingReader.checkCharacters)
						{
							xmlCharCheckingReader.ValidateQName(xmlCharCheckingReader.reader.Prefix, xmlCharCheckingReader.reader.LocalName);
							goto IL_94F;
						}
						goto IL_94F;
					default:
						goto IL_94F;
					}
					IL_230:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_2A9:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_322:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_3B3:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_50C:
					string result2 = awaiter3.GetResult();
					xmlCharCheckingReader.CheckCharacters(result2);
					goto IL_94F;
					IL_5AC:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_654:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_707:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_811:
					result = awaiter2.GetResult();
					goto IL_978;
					IL_890:
					result2 = awaiter3.GetResult();
					xmlCharCheckingReader.CheckWhitespace(result2);
					goto IL_94F;
					IL_918:
					result2 = awaiter3.GetResult();
					xmlCharCheckingReader.CheckWhitespace(result2);
					IL_94F:
					xmlCharCheckingReader.lastNodeType = this.<nodeType>5__2;
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_978:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x00013198 File Offset: 0x00011398
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006C9 RID: 1737
			public int <>1__state;

			// Token: 0x040006CA RID: 1738
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x040006CB RID: 1739
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006CC RID: 1740
			private XmlNodeType <nodeType>5__2;

			// Token: 0x040006CD RID: 1741
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040006CE RID: 1742
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x040006CF RID: 1743
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x0200006B RID: 107
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBase64Async>d__37 : IAsyncStateMachine
		{
			// Token: 0x06000419 RID: 1049 RVA: 0x000131A8 File Offset: 0x000113A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1F2;
					default:
						if (xmlCharCheckingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_21C;
						}
						if (xmlCharCheckingReader.state != XmlCharCheckingReader.State.InReadBinary)
						{
							if (xmlCharCheckingReader.<>n__0() && !xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.readBinaryHelper = null;
								xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
								awaiter = xmlCharCheckingReader.<>n__1(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBase64Async>d__37>(ref awaiter, ref this);
									return;
								}
								break;
							}
							else
							{
								xmlCharCheckingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlCharCheckingReader.readBinaryHelper, xmlCharCheckingReader);
							}
						}
						else if (xmlCharCheckingReader.readBinaryHelper == null)
						{
							awaiter = xmlCharCheckingReader.<>n__1(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBase64Async>d__37>(ref awaiter, ref this);
								return;
							}
							goto IL_16C;
						}
						xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
						awaiter = xmlCharCheckingReader.readBinaryHelper.ReadContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBase64Async>d__37>(ref awaiter, ref this);
							return;
						}
						goto IL_1F2;
					}
					result = awaiter.GetResult();
					goto IL_21C;
					IL_16C:
					result = awaiter.GetResult();
					goto IL_21C;
					IL_1F2:
					int result2 = awaiter.GetResult();
					xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_21C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x00013404 File Offset: 0x00011604
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006D0 RID: 1744
			public int <>1__state;

			// Token: 0x040006D1 RID: 1745
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040006D2 RID: 1746
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006D3 RID: 1747
			public byte[] buffer;

			// Token: 0x040006D4 RID: 1748
			public int index;

			// Token: 0x040006D5 RID: 1749
			public int count;

			// Token: 0x040006D6 RID: 1750
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200006C RID: 108
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinHexAsync>d__38 : IAsyncStateMachine
		{
			// Token: 0x0600041B RID: 1051 RVA: 0x00013414 File Offset: 0x00011614
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1F2;
					default:
						if (xmlCharCheckingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_21C;
						}
						if (xmlCharCheckingReader.state != XmlCharCheckingReader.State.InReadBinary)
						{
							if (xmlCharCheckingReader.<>n__0() && !xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.readBinaryHelper = null;
								xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
								awaiter = xmlCharCheckingReader.<>n__2(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38>(ref awaiter, ref this);
									return;
								}
								break;
							}
							else
							{
								xmlCharCheckingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlCharCheckingReader.readBinaryHelper, xmlCharCheckingReader);
							}
						}
						else if (xmlCharCheckingReader.readBinaryHelper == null)
						{
							awaiter = xmlCharCheckingReader.<>n__2(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38>(ref awaiter, ref this);
								return;
							}
							goto IL_16C;
						}
						xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
						awaiter = xmlCharCheckingReader.readBinaryHelper.ReadContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadContentAsBinHexAsync>d__38>(ref awaiter, ref this);
							return;
						}
						goto IL_1F2;
					}
					result = awaiter.GetResult();
					goto IL_21C;
					IL_16C:
					result = awaiter.GetResult();
					goto IL_21C;
					IL_1F2:
					int result2 = awaiter.GetResult();
					xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_21C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x00013670 File Offset: 0x00011870
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006D7 RID: 1751
			public int <>1__state;

			// Token: 0x040006D8 RID: 1752
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040006D9 RID: 1753
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006DA RID: 1754
			public byte[] buffer;

			// Token: 0x040006DB RID: 1755
			public int index;

			// Token: 0x040006DC RID: 1756
			public int count;

			// Token: 0x040006DD RID: 1757
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200006D RID: 109
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBase64Async>d__39 : IAsyncStateMachine
		{
			// Token: 0x0600041D RID: 1053 RVA: 0x00013680 File Offset: 0x00011880
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C9;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24F;
					default:
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.buffer.Length - this.index < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (xmlCharCheckingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_279;
						}
						if (xmlCharCheckingReader.state != XmlCharCheckingReader.State.InReadBinary)
						{
							if (xmlCharCheckingReader.<>n__0() && !xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.readBinaryHelper = null;
								xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
								awaiter = xmlCharCheckingReader.<>n__3(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39>(ref awaiter, ref this);
									return;
								}
								break;
							}
							else
							{
								xmlCharCheckingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlCharCheckingReader.readBinaryHelper, xmlCharCheckingReader);
							}
						}
						else if (xmlCharCheckingReader.readBinaryHelper == null)
						{
							awaiter = xmlCharCheckingReader.<>n__3(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39>(ref awaiter, ref this);
								return;
							}
							goto IL_1C9;
						}
						xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
						awaiter = xmlCharCheckingReader.readBinaryHelper.ReadElementContentAsBase64Async(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBase64Async>d__39>(ref awaiter, ref this);
							return;
						}
						goto IL_24F;
					}
					result = awaiter.GetResult();
					goto IL_279;
					IL_1C9:
					result = awaiter.GetResult();
					goto IL_279;
					IL_24F:
					int result2 = awaiter.GetResult();
					xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_279:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00013938 File Offset: 0x00011B38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006DE RID: 1758
			public int <>1__state;

			// Token: 0x040006DF RID: 1759
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040006E0 RID: 1760
			public byte[] buffer;

			// Token: 0x040006E1 RID: 1761
			public int count;

			// Token: 0x040006E2 RID: 1762
			public int index;

			// Token: 0x040006E3 RID: 1763
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006E4 RID: 1764
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinHexAsync>d__40 : IAsyncStateMachine
		{
			// Token: 0x0600041F RID: 1055 RVA: 0x00013948 File Offset: 0x00011B48
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C9;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24F;
					default:
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.buffer.Length - this.index < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (xmlCharCheckingReader.ReadState != ReadState.Interactive)
						{
							result = 0;
							goto IL_279;
						}
						if (xmlCharCheckingReader.state != XmlCharCheckingReader.State.InReadBinary)
						{
							if (xmlCharCheckingReader.<>n__0() && !xmlCharCheckingReader.checkCharacters)
							{
								xmlCharCheckingReader.readBinaryHelper = null;
								xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
								awaiter = xmlCharCheckingReader.<>n__4(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40>(ref awaiter, ref this);
									return;
								}
								break;
							}
							else
							{
								xmlCharCheckingReader.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(xmlCharCheckingReader.readBinaryHelper, xmlCharCheckingReader);
							}
						}
						else if (xmlCharCheckingReader.readBinaryHelper == null)
						{
							awaiter = xmlCharCheckingReader.<>n__4(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40>(ref awaiter, ref this);
								return;
							}
							goto IL_1C9;
						}
						xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
						awaiter = xmlCharCheckingReader.readBinaryHelper.ReadElementContentAsBinHexAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlCharCheckingReader.<ReadElementContentAsBinHexAsync>d__40>(ref awaiter, ref this);
							return;
						}
						goto IL_24F;
					}
					result = awaiter.GetResult();
					goto IL_279;
					IL_1C9:
					result = awaiter.GetResult();
					goto IL_279;
					IL_24F:
					int result2 = awaiter.GetResult();
					xmlCharCheckingReader.state = XmlCharCheckingReader.State.InReadBinary;
					result = result2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_279:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x00013C00 File Offset: 0x00011E00
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006E5 RID: 1765
			public int <>1__state;

			// Token: 0x040006E6 RID: 1766
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040006E7 RID: 1767
			public byte[] buffer;

			// Token: 0x040006E8 RID: 1768
			public int count;

			// Token: 0x040006E9 RID: 1769
			public int index;

			// Token: 0x040006EA RID: 1770
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006EB RID: 1771
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200006F RID: 111
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishReadBinaryAsync>d__41 : IAsyncStateMachine
		{
			// Token: 0x06000421 RID: 1057 RVA: 0x00013C10 File Offset: 0x00011E10
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlCharCheckingReader xmlCharCheckingReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlCharCheckingReader.state = XmlCharCheckingReader.State.Interactive;
						if (xmlCharCheckingReader.readBinaryHelper == null)
						{
							goto IL_86;
						}
						awaiter = xmlCharCheckingReader.readBinaryHelper.FinishAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlCharCheckingReader.<FinishReadBinaryAsync>d__41>(ref awaiter, ref this);
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
					IL_86:;
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

			// Token: 0x06000422 RID: 1058 RVA: 0x00013CE4 File Offset: 0x00011EE4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040006EC RID: 1772
			public int <>1__state;

			// Token: 0x040006ED RID: 1773
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040006EE RID: 1774
			public XmlCharCheckingReader <>4__this;

			// Token: 0x040006EF RID: 1775
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
