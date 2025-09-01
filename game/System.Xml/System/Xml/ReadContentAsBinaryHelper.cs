using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000049 RID: 73
	internal class ReadContentAsBinaryHelper
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		internal ReadContentAsBinaryHelper(XmlReader reader)
		{
			this.reader = reader;
			this.canReadValueChunk = reader.CanReadValueChunk;
			if (this.canReadValueChunk)
			{
				this.valueChunk = new char[256];
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DCED File Offset: 0x0000BEED
		internal static ReadContentAsBinaryHelper CreateOrReset(ReadContentAsBinaryHelper helper, XmlReader reader)
		{
			if (helper == null)
			{
				return new ReadContentAsBinaryHelper(reader);
			}
			helper.Reset();
			return helper;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DD00 File Offset: 0x0000BF00
		internal int ReadContentAsBase64(byte[] buffer, int index, int count)
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
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (!this.reader.CanReadContentAs())
				{
					throw this.reader.CreateReadContentAsException("ReadContentAsBase64");
				}
				if (!this.Init())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				if (this.decoder == this.base64Decoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			default:
				return 0;
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		internal int ReadContentAsBinHex(byte[] buffer, int index, int count)
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
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (!this.reader.CanReadContentAs())
				{
					throw this.reader.CreateReadContentAsException("ReadContentAsBinHex");
				}
				if (!this.Init())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				if (this.decoder == this.binHexDecoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			default:
				return 0;
			}
			this.InitBinHexDecoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DE90 File Offset: 0x0000C090
		internal int ReadElementContentAsBase64(byte[] buffer, int index, int count)
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
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
				}
				if (!this.InitOnElement())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				if (this.decoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
				break;
			default:
				return 0;
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DF5C File Offset: 0x0000C15C
		internal int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
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
			switch (this.state)
			{
			case ReadContentAsBinaryHelper.State.None:
				if (this.reader.NodeType != XmlNodeType.Element)
				{
					throw this.reader.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitOnElement())
				{
					return 0;
				}
				break;
			case ReadContentAsBinaryHelper.State.InReadContent:
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
			case ReadContentAsBinaryHelper.State.InReadElementContent:
				if (this.decoder == this.binHexDecoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
				break;
			default:
				return 0;
			}
			this.InitBinHexDecoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E028 File Offset: 0x0000C228
		internal void Finish()
		{
			if (this.state != ReadContentAsBinaryHelper.State.None)
			{
				while (this.MoveToNextContentNode(true))
				{
				}
				if (this.state == ReadContentAsBinaryHelper.State.InReadElementContent)
				{
					if (this.reader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
					}
					this.reader.Read();
				}
			}
			this.Reset();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E09F File Offset: 0x0000C29F
		internal void Reset()
		{
			this.state = ReadContentAsBinaryHelper.State.None;
			this.isEnd = false;
			this.valueOffset = 0;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
		private bool Init()
		{
			if (!this.MoveToNextContentNode(false))
			{
				return false;
			}
			this.state = ReadContentAsBinaryHelper.State.InReadContent;
			this.isEnd = false;
			return true;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		private bool InitOnElement()
		{
			bool isEmptyElement = this.reader.IsEmptyElement;
			this.reader.Read();
			if (isEmptyElement)
			{
				return false;
			}
			if (this.MoveToNextContentNode(false))
			{
				this.state = ReadContentAsBinaryHelper.State.InReadElementContent;
				this.isEnd = false;
				return true;
			}
			if (this.reader.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.reader.Read();
			return false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000E160 File Offset: 0x0000C360
		private void InitBase64Decoder()
		{
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			else
			{
				this.base64Decoder.Reset();
			}
			this.decoder = this.base64Decoder;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000E18E File Offset: 0x0000C38E
		private void InitBinHexDecoder()
		{
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			else
			{
				this.binHexDecoder.Reset();
			}
			this.decoder = this.binHexDecoder;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		private int ReadContentAsBinary(byte[] buffer, int index, int count)
		{
			if (this.isEnd)
			{
				this.Reset();
				return 0;
			}
			this.decoder.SetNextOutputBuffer(buffer, index, count);
			for (;;)
			{
				if (this.canReadValueChunk)
				{
					for (;;)
					{
						if (this.valueOffset < this.valueChunkLength)
						{
							int num = this.decoder.Decode(this.valueChunk, this.valueOffset, this.valueChunkLength - this.valueOffset);
							this.valueOffset += num;
						}
						if (this.decoder.IsFull)
						{
							goto Block_3;
						}
						if ((this.valueChunkLength = this.reader.ReadValueChunk(this.valueChunk, 0, 256)) == 0)
						{
							break;
						}
						this.valueOffset = 0;
					}
				}
				else
				{
					string value = this.reader.Value;
					int num2 = this.decoder.Decode(value, this.valueOffset, value.Length - this.valueOffset);
					this.valueOffset += num2;
					if (this.decoder.IsFull)
					{
						goto Block_5;
					}
				}
				this.valueOffset = 0;
				if (!this.MoveToNextContentNode(true))
				{
					goto Block_6;
				}
			}
			Block_3:
			return this.decoder.DecodedCount;
			Block_5:
			return this.decoder.DecodedCount;
			Block_6:
			this.isEnd = true;
			return this.decoder.DecodedCount;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		private int ReadElementContentAsBinary(byte[] buffer, int index, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = this.ReadContentAsBinary(buffer, index, count);
			if (num > 0)
			{
				return num;
			}
			if (this.reader.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.reader.NodeType.ToString(), this.reader as IXmlLineInfo);
			}
			this.reader.Read();
			this.state = ReadContentAsBinaryHelper.State.None;
			return 0;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000E368 File Offset: 0x0000C568
		private bool MoveToNextContentNode(bool moveIfOnContentNode)
		{
			for (;;)
			{
				switch (this.reader.NodeType)
				{
				case XmlNodeType.Attribute:
					goto IL_52;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (!moveIfOnContentNode)
					{
						return true;
					}
					goto IL_78;
				case XmlNodeType.EntityReference:
					if (this.reader.CanResolveEntity)
					{
						this.reader.ResolveEntity();
						goto IL_78;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_78;
				}
				break;
				IL_78:
				moveIfOnContentNode = false;
				if (!this.reader.Read())
				{
					return false;
				}
			}
			return false;
			IL_52:
			return !moveIfOnContentNode;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000E404 File Offset: 0x0000C604
		internal Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27 <ReadContentAsBase64Async>d__;
			<ReadContentAsBase64Async>d__.<>4__this = this;
			<ReadContentAsBase64Async>d__.buffer = buffer;
			<ReadContentAsBase64Async>d__.index = index;
			<ReadContentAsBase64Async>d__.count = count;
			<ReadContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64Async>d__.<>1__state = -1;
			<ReadContentAsBase64Async>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27>(ref <ReadContentAsBase64Async>d__);
			return <ReadContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E460 File Offset: 0x0000C660
		internal Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		internal Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29 <ReadElementContentAsBase64Async>d__;
			<ReadElementContentAsBase64Async>d__.<>4__this = this;
			<ReadElementContentAsBase64Async>d__.buffer = buffer;
			<ReadElementContentAsBase64Async>d__.index = index;
			<ReadElementContentAsBase64Async>d__.count = count;
			<ReadElementContentAsBase64Async>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29>(ref <ReadElementContentAsBase64Async>d__);
			return <ReadElementContentAsBase64Async>d__.<>t__builder.Task;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000E518 File Offset: 0x0000C718
		internal Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E574 File Offset: 0x0000C774
		internal Task FinishAsync()
		{
			ReadContentAsBinaryHelper.<FinishAsync>d__31 <FinishAsync>d__;
			<FinishAsync>d__.<>4__this = this;
			<FinishAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishAsync>d__.<>1__state = -1;
			<FinishAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<FinishAsync>d__31>(ref <FinishAsync>d__);
			return <FinishAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		private Task<bool> InitAsync()
		{
			ReadContentAsBinaryHelper.<InitAsync>d__32 <InitAsync>d__;
			<InitAsync>d__.<>4__this = this;
			<InitAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitAsync>d__.<>1__state = -1;
			<InitAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<InitAsync>d__32>(ref <InitAsync>d__);
			return <InitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		private Task<bool> InitOnElementAsync()
		{
			ReadContentAsBinaryHelper.<InitOnElementAsync>d__33 <InitOnElementAsync>d__;
			<InitOnElementAsync>d__.<>4__this = this;
			<InitOnElementAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitOnElementAsync>d__.<>1__state = -1;
			<InitOnElementAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<InitOnElementAsync>d__33>(ref <InitOnElementAsync>d__);
			return <InitOnElementAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E640 File Offset: 0x0000C840
		private Task<int> ReadContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34 <ReadContentAsBinaryAsync>d__;
			<ReadContentAsBinaryAsync>d__.<>4__this = this;
			<ReadContentAsBinaryAsync>d__.buffer = buffer;
			<ReadContentAsBinaryAsync>d__.index = index;
			<ReadContentAsBinaryAsync>d__.count = count;
			<ReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadContentAsBinaryAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34>(ref <ReadContentAsBinaryAsync>d__);
			return <ReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E69C File Offset: 0x0000C89C
		private Task<int> ReadElementContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35 <ReadElementContentAsBinaryAsync>d__;
			<ReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<ReadElementContentAsBinaryAsync>d__.buffer = buffer;
			<ReadElementContentAsBinaryAsync>d__.index = index;
			<ReadElementContentAsBinaryAsync>d__.count = count;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35>(ref <ReadElementContentAsBinaryAsync>d__);
			return <ReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private Task<bool> MoveToNextContentNodeAsync(bool moveIfOnContentNode)
		{
			ReadContentAsBinaryHelper.<MoveToNextContentNodeAsync>d__36 <MoveToNextContentNodeAsync>d__;
			<MoveToNextContentNodeAsync>d__.<>4__this = this;
			<MoveToNextContentNodeAsync>d__.moveIfOnContentNode = moveIfOnContentNode;
			<MoveToNextContentNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<MoveToNextContentNodeAsync>d__.<>1__state = -1;
			<MoveToNextContentNodeAsync>d__.<>t__builder.Start<ReadContentAsBinaryHelper.<MoveToNextContentNodeAsync>d__36>(ref <MoveToNextContentNodeAsync>d__);
			return <MoveToNextContentNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000629 RID: 1577
		private XmlReader reader;

		// Token: 0x0400062A RID: 1578
		private ReadContentAsBinaryHelper.State state;

		// Token: 0x0400062B RID: 1579
		private int valueOffset;

		// Token: 0x0400062C RID: 1580
		private bool isEnd;

		// Token: 0x0400062D RID: 1581
		private bool canReadValueChunk;

		// Token: 0x0400062E RID: 1582
		private char[] valueChunk;

		// Token: 0x0400062F RID: 1583
		private int valueChunkLength;

		// Token: 0x04000630 RID: 1584
		private IncrementalReadDecoder decoder;

		// Token: 0x04000631 RID: 1585
		private Base64Decoder base64Decoder;

		// Token: 0x04000632 RID: 1586
		private BinHexDecoder binHexDecoder;

		// Token: 0x04000633 RID: 1587
		private const int ChunkSize = 256;

		// Token: 0x0200004A RID: 74
		private enum State
		{
			// Token: 0x04000635 RID: 1589
			None,
			// Token: 0x04000636 RID: 1590
			InReadContent,
			// Token: 0x04000637 RID: 1591
			InReadElementContent
		}

		// Token: 0x0200004B RID: 75
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBase64Async>d__27 : IAsyncStateMachine
		{
			// Token: 0x0600027A RID: 634 RVA: 0x0000E744 File Offset: 0x0000C944
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B1;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24B;
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
						switch (readContentAsBinaryHelper.state)
						{
						case ReadContentAsBinaryHelper.State.None:
							if (!readContentAsBinaryHelper.reader.CanReadContentAs())
							{
								throw readContentAsBinaryHelper.reader.CreateReadContentAsException("ReadContentAsBase64");
							}
							awaiter = readContentAsBinaryHelper.InitAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27>(ref awaiter, ref this);
								return;
							}
							break;
						case ReadContentAsBinaryHelper.State.InReadContent:
							if (readContentAsBinaryHelper.decoder != readContentAsBinaryHelper.base64Decoder)
							{
								goto IL_1D5;
							}
							awaiter2 = readContentAsBinaryHelper.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27>(ref awaiter2, ref this);
								return;
							}
							goto IL_1B1;
						case ReadContentAsBinaryHelper.State.InReadElementContent:
							throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
						default:
							result = 0;
							goto IL_26E;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						result = 0;
						goto IL_26E;
					}
					goto IL_1D5;
					IL_1B1:
					result = awaiter2.GetResult();
					goto IL_26E;
					IL_1D5:
					readContentAsBinaryHelper.InitBase64Decoder();
					awaiter2 = readContentAsBinaryHelper.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBase64Async>d__27>(ref awaiter2, ref this);
						return;
					}
					IL_24B:
					result = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26E:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600027B RID: 635 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000638 RID: 1592
			public int <>1__state;

			// Token: 0x04000639 RID: 1593
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x0400063A RID: 1594
			public byte[] buffer;

			// Token: 0x0400063B RID: 1595
			public int count;

			// Token: 0x0400063C RID: 1596
			public int index;

			// Token: 0x0400063D RID: 1597
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x0400063E RID: 1598
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400063F RID: 1599
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinHexAsync>d__28 : IAsyncStateMachine
		{
			// Token: 0x0600027C RID: 636 RVA: 0x0000EA00 File Offset: 0x0000CC00
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B1;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24B;
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
						switch (readContentAsBinaryHelper.state)
						{
						case ReadContentAsBinaryHelper.State.None:
							if (!readContentAsBinaryHelper.reader.CanReadContentAs())
							{
								throw readContentAsBinaryHelper.reader.CreateReadContentAsException("ReadContentAsBinHex");
							}
							awaiter = readContentAsBinaryHelper.InitAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28>(ref awaiter, ref this);
								return;
							}
							break;
						case ReadContentAsBinaryHelper.State.InReadContent:
							if (readContentAsBinaryHelper.decoder != readContentAsBinaryHelper.binHexDecoder)
							{
								goto IL_1D5;
							}
							awaiter2 = readContentAsBinaryHelper.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28>(ref awaiter2, ref this);
								return;
							}
							goto IL_1B1;
						case ReadContentAsBinaryHelper.State.InReadElementContent:
							throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
						default:
							result = 0;
							goto IL_26E;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						result = 0;
						goto IL_26E;
					}
					goto IL_1D5;
					IL_1B1:
					result = awaiter2.GetResult();
					goto IL_26E;
					IL_1D5:
					readContentAsBinaryHelper.InitBinHexDecoder();
					awaiter2 = readContentAsBinaryHelper.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinHexAsync>d__28>(ref awaiter2, ref this);
						return;
					}
					IL_24B:
					result = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26E:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600027D RID: 637 RVA: 0x0000ECAC File Offset: 0x0000CEAC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000640 RID: 1600
			public int <>1__state;

			// Token: 0x04000641 RID: 1601
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000642 RID: 1602
			public byte[] buffer;

			// Token: 0x04000643 RID: 1603
			public int count;

			// Token: 0x04000644 RID: 1604
			public int index;

			// Token: 0x04000645 RID: 1605
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000646 RID: 1606
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000647 RID: 1607
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200004D RID: 77
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBase64Async>d__29 : IAsyncStateMachine
		{
			// Token: 0x0600027E RID: 638 RVA: 0x0000ECBC File Offset: 0x0000CEBC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C2;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24C;
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
						switch (readContentAsBinaryHelper.state)
						{
						case ReadContentAsBinaryHelper.State.None:
							if (readContentAsBinaryHelper.reader.NodeType != XmlNodeType.Element)
							{
								throw readContentAsBinaryHelper.reader.CreateReadElementContentAsException("ReadElementContentAsBase64");
							}
							awaiter = readContentAsBinaryHelper.InitOnElementAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29>(ref awaiter, ref this);
								return;
							}
							break;
						case ReadContentAsBinaryHelper.State.InReadContent:
							throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
						case ReadContentAsBinaryHelper.State.InReadElementContent:
							if (readContentAsBinaryHelper.decoder != readContentAsBinaryHelper.base64Decoder)
							{
								goto IL_1D6;
							}
							awaiter2 = readContentAsBinaryHelper.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29>(ref awaiter2, ref this);
								return;
							}
							goto IL_1C2;
						default:
							result = 0;
							goto IL_26F;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						result = 0;
						goto IL_26F;
					}
					goto IL_1D6;
					IL_1C2:
					result = awaiter2.GetResult();
					goto IL_26F;
					IL_1D6:
					readContentAsBinaryHelper.InitBase64Decoder();
					awaiter2 = readContentAsBinaryHelper.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBase64Async>d__29>(ref awaiter2, ref this);
						return;
					}
					IL_24C:
					result = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600027F RID: 639 RVA: 0x0000EF68 File Offset: 0x0000D168
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000648 RID: 1608
			public int <>1__state;

			// Token: 0x04000649 RID: 1609
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x0400064A RID: 1610
			public byte[] buffer;

			// Token: 0x0400064B RID: 1611
			public int count;

			// Token: 0x0400064C RID: 1612
			public int index;

			// Token: 0x0400064D RID: 1613
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x0400064E RID: 1614
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400064F RID: 1615
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200004E RID: 78
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinHexAsync>d__30 : IAsyncStateMachine
		{
			// Token: 0x06000280 RID: 640 RVA: 0x0000EF78 File Offset: 0x0000D178
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C2;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24C;
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
						switch (readContentAsBinaryHelper.state)
						{
						case ReadContentAsBinaryHelper.State.None:
							if (readContentAsBinaryHelper.reader.NodeType != XmlNodeType.Element)
							{
								throw readContentAsBinaryHelper.reader.CreateReadElementContentAsException("ReadElementContentAsBinHex");
							}
							awaiter = readContentAsBinaryHelper.InitOnElementAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30>(ref awaiter, ref this);
								return;
							}
							break;
						case ReadContentAsBinaryHelper.State.InReadContent:
							throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
						case ReadContentAsBinaryHelper.State.InReadElementContent:
							if (readContentAsBinaryHelper.decoder != readContentAsBinaryHelper.binHexDecoder)
							{
								goto IL_1D6;
							}
							awaiter2 = readContentAsBinaryHelper.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30>(ref awaiter2, ref this);
								return;
							}
							goto IL_1C2;
						default:
							result = 0;
							goto IL_26F;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						result = 0;
						goto IL_26F;
					}
					goto IL_1D6;
					IL_1C2:
					result = awaiter2.GetResult();
					goto IL_26F;
					IL_1D6:
					readContentAsBinaryHelper.InitBinHexDecoder();
					awaiter2 = readContentAsBinaryHelper.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBinHexAsync>d__30>(ref awaiter2, ref this);
						return;
					}
					IL_24C:
					result = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000281 RID: 641 RVA: 0x0000F224 File Offset: 0x0000D424
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000650 RID: 1616
			public int <>1__state;

			// Token: 0x04000651 RID: 1617
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000652 RID: 1618
			public byte[] buffer;

			// Token: 0x04000653 RID: 1619
			public int count;

			// Token: 0x04000654 RID: 1620
			public int index;

			// Token: 0x04000655 RID: 1621
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000656 RID: 1622
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000657 RID: 1623
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200004F RID: 79
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x06000282 RID: 642 RVA: 0x0000F234 File Offset: 0x0000D434
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_81;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_134;
					}
					if (readContentAsBinaryHelper.state == ReadContentAsBinaryHelper.State.None)
					{
						goto IL_13C;
					}
					IL_23:
					awaiter = readContentAsBinaryHelper.MoveToNextContentNodeAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<FinishAsync>d__31>(ref awaiter, ref this);
						return;
					}
					IL_81:
					if (awaiter.GetResult())
					{
						goto IL_23;
					}
					if (readContentAsBinaryHelper.state != ReadContentAsBinaryHelper.State.InReadElementContent)
					{
						goto IL_13C;
					}
					if (readContentAsBinaryHelper.reader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", readContentAsBinaryHelper.reader.NodeType.ToString(), readContentAsBinaryHelper.reader as IXmlLineInfo);
					}
					awaiter = readContentAsBinaryHelper.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<FinishAsync>d__31>(ref awaiter, ref this);
						return;
					}
					IL_134:
					awaiter.GetResult();
					IL_13C:
					readContentAsBinaryHelper.Reset();
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

			// Token: 0x06000283 RID: 643 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000658 RID: 1624
			public int <>1__state;

			// Token: 0x04000659 RID: 1625
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400065A RID: 1626
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x0400065B RID: 1627
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06000284 RID: 644 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = readContentAsBinaryHelper.MoveToNextContentNodeAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<InitAsync>d__32>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (!awaiter.GetResult())
					{
						result = false;
					}
					else
					{
						readContentAsBinaryHelper.state = ReadContentAsBinaryHelper.State.InReadContent;
						readContentAsBinaryHelper.isEnd = false;
						result = true;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000285 RID: 645 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400065C RID: 1628
			public int <>1__state;

			// Token: 0x0400065D RID: 1629
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400065E RID: 1630
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x0400065F RID: 1631
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000051 RID: 81
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitOnElementAsync>d__33 : IAsyncStateMachine
		{
			// Token: 0x06000286 RID: 646 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_10A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B5;
					default:
						this.<isEmpty>5__2 = readContentAsBinaryHelper.reader.IsEmptyElement;
						awaiter = readContentAsBinaryHelper.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<InitOnElementAsync>d__33>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					if (this.<isEmpty>5__2)
					{
						result = false;
						goto IL_1EC;
					}
					awaiter = readContentAsBinaryHelper.MoveToNextContentNodeAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<InitOnElementAsync>d__33>(ref awaiter, ref this);
						return;
					}
					IL_10A:
					if (awaiter.GetResult())
					{
						readContentAsBinaryHelper.state = ReadContentAsBinaryHelper.State.InReadElementContent;
						readContentAsBinaryHelper.isEnd = false;
						result = true;
						goto IL_1EC;
					}
					if (readContentAsBinaryHelper.reader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", readContentAsBinaryHelper.reader.NodeType.ToString(), readContentAsBinaryHelper.reader as IXmlLineInfo);
					}
					awaiter = readContentAsBinaryHelper.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<InitOnElementAsync>d__33>(ref awaiter, ref this);
						return;
					}
					IL_1B5:
					awaiter.GetResult();
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1EC:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000287 RID: 647 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000660 RID: 1632
			public int <>1__state;

			// Token: 0x04000661 RID: 1633
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000662 RID: 1634
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000663 RID: 1635
			private bool <isEmpty>5__2;

			// Token: 0x04000664 RID: 1636
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000052 RID: 82
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinaryAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06000288 RID: 648 RVA: 0x0000F704 File Offset: 0x0000D904
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_12E;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1BB;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_27A;
					default:
						if (readContentAsBinaryHelper.isEnd)
						{
							readContentAsBinaryHelper.Reset();
							result = 0;
							goto IL_2B4;
						}
						readContentAsBinaryHelper.decoder.SetNextOutputBuffer(this.buffer, this.index, this.count);
						break;
					}
					IL_52:
					if (!readContentAsBinaryHelper.canReadValueChunk)
					{
						awaiter2 = readContentAsBinaryHelper.reader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34>(ref awaiter2, ref this);
							return;
						}
						goto IL_1BB;
					}
					IL_5D:
					if (readContentAsBinaryHelper.valueOffset < readContentAsBinaryHelper.valueChunkLength)
					{
						int num2 = readContentAsBinaryHelper.decoder.Decode(readContentAsBinaryHelper.valueChunk, readContentAsBinaryHelper.valueOffset, readContentAsBinaryHelper.valueChunkLength - readContentAsBinaryHelper.valueOffset);
						readContentAsBinaryHelper.valueOffset += num2;
					}
					if (readContentAsBinaryHelper.decoder.IsFull)
					{
						result = readContentAsBinaryHelper.decoder.DecodedCount;
						goto IL_2B4;
					}
					awaiter = readContentAsBinaryHelper.reader.ReadValueChunkAsync(readContentAsBinaryHelper.valueChunk, 0, 256).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34>(ref awaiter, ref this);
						return;
					}
					IL_12E:
					int result2 = awaiter.GetResult();
					if ((readContentAsBinaryHelper.valueChunkLength = result2) != 0)
					{
						readContentAsBinaryHelper.valueOffset = 0;
						goto IL_5D;
					}
					goto IL_214;
					IL_1BB:
					string result3 = awaiter2.GetResult();
					int num3 = readContentAsBinaryHelper.decoder.Decode(result3, readContentAsBinaryHelper.valueOffset, result3.Length - readContentAsBinaryHelper.valueOffset);
					readContentAsBinaryHelper.valueOffset += num3;
					if (readContentAsBinaryHelper.decoder.IsFull)
					{
						result = readContentAsBinaryHelper.decoder.DecodedCount;
						goto IL_2B4;
					}
					IL_214:
					readContentAsBinaryHelper.valueOffset = 0;
					awaiter3 = readContentAsBinaryHelper.MoveToNextContentNodeAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadContentAsBinaryAsync>d__34>(ref awaiter3, ref this);
						return;
					}
					IL_27A:
					if (awaiter3.GetResult())
					{
						goto IL_52;
					}
					readContentAsBinaryHelper.isEnd = true;
					result = readContentAsBinaryHelper.decoder.DecodedCount;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2B4:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000289 RID: 649 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000665 RID: 1637
			public int <>1__state;

			// Token: 0x04000666 RID: 1638
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000667 RID: 1639
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000668 RID: 1640
			public byte[] buffer;

			// Token: 0x04000669 RID: 1641
			public int index;

			// Token: 0x0400066A RID: 1642
			public int count;

			// Token: 0x0400066B RID: 1643
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400066C RID: 1644
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x0400066D RID: 1645
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x02000053 RID: 83
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinaryAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x0600028A RID: 650 RVA: 0x0000FA08 File Offset: 0x0000DC08
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_14F;
						}
						if (this.count == 0)
						{
							result = 0;
							goto IL_17B;
						}
						awaiter2 = readContentAsBinaryHelper.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter2.GetResult();
					if (result2 > 0)
					{
						result = result2;
						goto IL_17B;
					}
					if (readContentAsBinaryHelper.reader.NodeType != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", readContentAsBinaryHelper.reader.NodeType.ToString(), readContentAsBinaryHelper.reader as IXmlLineInfo);
					}
					awaiter = readContentAsBinaryHelper.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<ReadElementContentAsBinaryAsync>d__35>(ref awaiter, ref this);
						return;
					}
					IL_14F:
					awaiter.GetResult();
					readContentAsBinaryHelper.state = ReadContentAsBinaryHelper.State.None;
					result = 0;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_17B:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600028B RID: 651 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400066E RID: 1646
			public int <>1__state;

			// Token: 0x0400066F RID: 1647
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000670 RID: 1648
			public int count;

			// Token: 0x04000671 RID: 1649
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000672 RID: 1650
			public byte[] buffer;

			// Token: 0x04000673 RID: 1651
			public int index;

			// Token: 0x04000674 RID: 1652
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000675 RID: 1653
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000054 RID: 84
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <MoveToNextContentNodeAsync>d__36 : IAsyncStateMachine
		{
			// Token: 0x0600028C RID: 652 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ReadContentAsBinaryHelper readContentAsBinaryHelper = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_10F;
					}
					IL_14:
					switch (readContentAsBinaryHelper.reader.NodeType)
					{
					case XmlNodeType.Attribute:
						result = !this.moveIfOnContentNode;
						goto IL_138;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						if (!this.moveIfOnContentNode)
						{
							result = true;
							goto IL_138;
						}
						goto IL_A5;
					case XmlNodeType.EntityReference:
						if (readContentAsBinaryHelper.reader.CanResolveEntity)
						{
							readContentAsBinaryHelper.reader.ResolveEntity();
							goto IL_A5;
						}
						break;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.EndEntity:
						goto IL_A5;
					}
					result = false;
					goto IL_138;
					IL_A5:
					this.moveIfOnContentNode = false;
					awaiter = readContentAsBinaryHelper.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, ReadContentAsBinaryHelper.<MoveToNextContentNodeAsync>d__36>(ref awaiter, ref this);
						return;
					}
					IL_10F:
					if (awaiter.GetResult())
					{
						goto IL_14;
					}
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_138:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000FD48 File Offset: 0x0000DF48
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000676 RID: 1654
			public int <>1__state;

			// Token: 0x04000677 RID: 1655
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000678 RID: 1656
			public ReadContentAsBinaryHelper <>4__this;

			// Token: 0x04000679 RID: 1657
			public bool moveIfOnContentNode;

			// Token: 0x0400067A RID: 1658
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
