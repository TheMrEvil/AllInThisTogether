using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000099 RID: 153
	internal abstract class XmlRawWriter : XmlWriter
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteStartDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteStartDocument(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteEndDocument()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteFullEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001E143 File Offset: 0x0001C343
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			this.base64Encoder.Encode(buffer, index, count);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override string LookupPrefix(string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override WriteState WriteState
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override string XmlLang
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteNmToken(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteName(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001E167 File Offset: 0x0001C367
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(new char[]
			{
				ch
			}));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001E17E File Offset: 0x0001C37E
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(new string(new char[]
			{
				lowChar,
				highChar
			}));
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001E199 File Offset: 0x0001C399
		public override void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		public override void WriteValue(DateTimeOffset value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001E1CE File Offset: 0x0001C3CE
		internal virtual IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600059D RID: 1437
		internal abstract void StartElementContent();

		// Token: 0x0600059E RID: 1438 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void OnRootElement(ConformanceLevel conformanceLevel)
		{
		}

		// Token: 0x0600059F RID: 1439
		internal abstract void WriteEndElement(string prefix, string localName, string ns);

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001E1D7 File Offset: 0x0001C3D7
		internal virtual void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001E1E2 File Offset: 0x0001C3E2
		internal virtual void WriteQualifiedName(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				this.WriteString(prefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x060005A2 RID: 1442
		internal abstract void WriteNamespaceDeclaration(string prefix, string ns);

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal virtual bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00005BD6 File Offset: 0x00003DD6
		internal virtual void WriteStartNamespaceDeclaration(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00005BD6 File Offset: 0x00003DD6
		internal virtual void WriteEndNamespaceDeclaration()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001E205 File Offset: 0x0001C405
		internal virtual void WriteEndBase64()
		{
			this.base64Encoder.Flush();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001E212 File Offset: 0x0001C412
		internal virtual void Close(WriteState currentState)
		{
			this.Close();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteStartDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteEndDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001E21A File Offset: 0x0001C41A
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteFullEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001E221 File Offset: 0x0001C421
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			return this.base64Encoder.EncodeAsync(buffer, index, count);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteNmTokenAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteNameAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001E245 File Offset: 0x0001C445
		public override Task WriteCDataAsync(string text)
		{
			return this.WriteStringAsync(text);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001E24E File Offset: 0x0001C44E
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.WriteStringAsync(new string(new char[]
			{
				ch
			}));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001E265 File Offset: 0x0001C465
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.WriteStringAsync(new string(new char[]
			{
				lowChar,
				highChar
			}));
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001E245 File Offset: 0x0001C445
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.WriteStringAsync(ws);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001E280 File Offset: 0x0001C480
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001E280 File Offset: 0x0001C480
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001E245 File Offset: 0x0001C445
		public override Task WriteRawAsync(string data)
		{
			return this.WriteStringAsync(data);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001E21A File Offset: 0x0001C41A
		internal virtual Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001E21A File Offset: 0x0001C41A
		internal virtual Task WriteXmlDeclarationAsync(string xmldecl)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000349C File Offset: 0x0000169C
		internal virtual Task StartElementContentAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000349C File Offset: 0x0000169C
		internal virtual Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001E290 File Offset: 0x0001C490
		internal virtual Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			return this.WriteEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001E29C File Offset: 0x0001C49C
		internal virtual Task WriteQualifiedNameAsync(string prefix, string localName, string ns)
		{
			XmlRawWriter.<WriteQualifiedNameAsync>d__74 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.prefix = prefix;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlRawWriter.<WriteQualifiedNameAsync>d__74>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000349C File Offset: 0x0000169C
		internal virtual Task WriteNamespaceDeclarationAsync(string prefix, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00005BD6 File Offset: 0x00003DD6
		internal virtual Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00005BD6 File Offset: 0x00003DD6
		internal virtual Task WriteEndNamespaceDeclarationAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001E2EF File Offset: 0x0001C4EF
		internal virtual Task WriteEndBase64Async()
		{
			return this.base64Encoder.FlushAsync();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		protected XmlRawWriter()
		{
		}

		// Token: 0x04000834 RID: 2100
		protected XmlRawWriterBase64Encoder base64Encoder;

		// Token: 0x04000835 RID: 2101
		protected IXmlNamespaceResolver resolver;

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteQualifiedNameAsync>d__74 : IAsyncStateMachine
		{
			// Token: 0x060005C7 RID: 1479 RVA: 0x0001E304 File Offset: 0x0001C504
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlRawWriter xmlRawWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_FC;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_163;
					default:
						if (this.prefix.Length == 0)
						{
							goto IL_103;
						}
						awaiter = xmlRawWriter.WriteStringAsync(this.prefix).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlRawWriter.<WriteQualifiedNameAsync>d__74>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlRawWriter.WriteStringAsync(":").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlRawWriter.<WriteQualifiedNameAsync>d__74>(ref awaiter, ref this);
						return;
					}
					IL_FC:
					awaiter.GetResult();
					IL_103:
					awaiter = xmlRawWriter.WriteStringAsync(this.localName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlRawWriter.<WriteQualifiedNameAsync>d__74>(ref awaiter, ref this);
						return;
					}
					IL_163:
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

			// Token: 0x060005C8 RID: 1480 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000836 RID: 2102
			public int <>1__state;

			// Token: 0x04000837 RID: 2103
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000838 RID: 2104
			public string prefix;

			// Token: 0x04000839 RID: 2105
			public XmlRawWriter <>4__this;

			// Token: 0x0400083A RID: 2106
			public string localName;

			// Token: 0x0400083B RID: 2107
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
