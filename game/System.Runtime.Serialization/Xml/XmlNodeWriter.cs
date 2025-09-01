using System;
using System.Runtime;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200008D RID: 141
	internal abstract class XmlNodeWriter
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001F962 File Offset: 0x0001DB62
		public static XmlNodeWriter Null
		{
			get
			{
				if (XmlNodeWriter.nullNodeWriter == null)
				{
					XmlNodeWriter.nullNodeWriter = new XmlNodeWriter.XmlNullNodeWriter();
				}
				return XmlNodeWriter.nullNodeWriter;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001F97A File Offset: 0x0001DB7A
		internal virtual AsyncCompletionResult WriteBase64TextAsync(AsyncEventArgs<XmlNodeWriterWriteBase64TextArgs> state)
		{
			throw Fx.AssertAndThrow("WriteBase64TextAsync not implemented.");
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001F986 File Offset: 0x0001DB86
		public virtual IAsyncResult BeginWriteBase64Text(byte[] trailBuffer, int trailCount, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new XmlNodeWriter.WriteBase64TextAsyncResult(trailBuffer, trailCount, buffer, offset, count, this, callback, state);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018B48 File Offset: 0x00016D48
		public virtual void EndWriteBase64Text(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x06000752 RID: 1874
		public abstract void Flush();

		// Token: 0x06000753 RID: 1875
		public abstract void Close();

		// Token: 0x06000754 RID: 1876
		public abstract void WriteDeclaration();

		// Token: 0x06000755 RID: 1877
		public abstract void WriteComment(string text);

		// Token: 0x06000756 RID: 1878
		public abstract void WriteCData(string text);

		// Token: 0x06000757 RID: 1879
		public abstract void WriteStartElement(string prefix, string localName);

		// Token: 0x06000758 RID: 1880 RVA: 0x0001F999 File Offset: 0x0001DB99
		public virtual void WriteStartElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			this.WriteStartElement(Encoding.UTF8.GetString(prefixBuffer, prefixOffset, prefixLength), Encoding.UTF8.GetString(localNameBuffer, localNameOffset, localNameLength));
		}

		// Token: 0x06000759 RID: 1881
		public abstract void WriteStartElement(string prefix, XmlDictionaryString localName);

		// Token: 0x0600075A RID: 1882
		public abstract void WriteEndStartElement(bool isEmpty);

		// Token: 0x0600075B RID: 1883
		public abstract void WriteEndElement(string prefix, string localName);

		// Token: 0x0600075C RID: 1884 RVA: 0x0001F9BE File Offset: 0x0001DBBE
		public virtual void WriteEndElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			this.WriteEndElement(Encoding.UTF8.GetString(prefixBuffer, prefixOffset, prefixLength), Encoding.UTF8.GetString(localNameBuffer, localNameOffset, localNameLength));
		}

		// Token: 0x0600075D RID: 1885
		public abstract void WriteXmlnsAttribute(string prefix, string ns);

		// Token: 0x0600075E RID: 1886 RVA: 0x0001F9E3 File Offset: 0x0001DBE3
		public virtual void WriteXmlnsAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] nsBuffer, int nsOffset, int nsLength)
		{
			this.WriteXmlnsAttribute(Encoding.UTF8.GetString(prefixBuffer, prefixOffset, prefixLength), Encoding.UTF8.GetString(nsBuffer, nsOffset, nsLength));
		}

		// Token: 0x0600075F RID: 1887
		public abstract void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns);

		// Token: 0x06000760 RID: 1888
		public abstract void WriteStartAttribute(string prefix, string localName);

		// Token: 0x06000761 RID: 1889 RVA: 0x0001FA08 File Offset: 0x0001DC08
		public virtual void WriteStartAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			this.WriteStartAttribute(Encoding.UTF8.GetString(prefixBuffer, prefixOffset, prefixLength), Encoding.UTF8.GetString(localNameBuffer, localNameOffset, localNameLength));
		}

		// Token: 0x06000762 RID: 1890
		public abstract void WriteStartAttribute(string prefix, XmlDictionaryString localName);

		// Token: 0x06000763 RID: 1891
		public abstract void WriteEndAttribute();

		// Token: 0x06000764 RID: 1892
		public abstract void WriteCharEntity(int ch);

		// Token: 0x06000765 RID: 1893
		public abstract void WriteEscapedText(string value);

		// Token: 0x06000766 RID: 1894
		public abstract void WriteEscapedText(XmlDictionaryString value);

		// Token: 0x06000767 RID: 1895
		public abstract void WriteEscapedText(char[] chars, int offset, int count);

		// Token: 0x06000768 RID: 1896
		public abstract void WriteEscapedText(byte[] buffer, int offset, int count);

		// Token: 0x06000769 RID: 1897
		public abstract void WriteText(string value);

		// Token: 0x0600076A RID: 1898
		public abstract void WriteText(XmlDictionaryString value);

		// Token: 0x0600076B RID: 1899
		public abstract void WriteText(char[] chars, int offset, int count);

		// Token: 0x0600076C RID: 1900
		public abstract void WriteText(byte[] buffer, int offset, int count);

		// Token: 0x0600076D RID: 1901
		public abstract void WriteInt32Text(int value);

		// Token: 0x0600076E RID: 1902
		public abstract void WriteInt64Text(long value);

		// Token: 0x0600076F RID: 1903
		public abstract void WriteBoolText(bool value);

		// Token: 0x06000770 RID: 1904
		public abstract void WriteUInt64Text(ulong value);

		// Token: 0x06000771 RID: 1905
		public abstract void WriteFloatText(float value);

		// Token: 0x06000772 RID: 1906
		public abstract void WriteDoubleText(double value);

		// Token: 0x06000773 RID: 1907
		public abstract void WriteDecimalText(decimal value);

		// Token: 0x06000774 RID: 1908
		public abstract void WriteDateTimeText(DateTime value);

		// Token: 0x06000775 RID: 1909
		public abstract void WriteUniqueIdText(UniqueId value);

		// Token: 0x06000776 RID: 1910
		public abstract void WriteTimeSpanText(TimeSpan value);

		// Token: 0x06000777 RID: 1911
		public abstract void WriteGuidText(Guid value);

		// Token: 0x06000778 RID: 1912
		public abstract void WriteStartListText();

		// Token: 0x06000779 RID: 1913
		public abstract void WriteListSeparator();

		// Token: 0x0600077A RID: 1914
		public abstract void WriteEndListText();

		// Token: 0x0600077B RID: 1915
		public abstract void WriteBase64Text(byte[] trailBuffer, int trailCount, byte[] buffer, int offset, int count);

		// Token: 0x0600077C RID: 1916
		public abstract void WriteQualifiedName(string prefix, XmlDictionaryString localName);

		// Token: 0x0600077D RID: 1917 RVA: 0x0000222F File Offset: 0x0000042F
		protected XmlNodeWriter()
		{
		}

		// Token: 0x0400036E RID: 878
		private static XmlNodeWriter nullNodeWriter;

		// Token: 0x0200008E RID: 142
		private class XmlNullNodeWriter : XmlNodeWriter
		{
			// Token: 0x0600077E RID: 1918 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void Flush()
			{
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void Close()
			{
			}

			// Token: 0x06000780 RID: 1920 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteDeclaration()
			{
			}

			// Token: 0x06000781 RID: 1921 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteComment(string text)
			{
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteCData(string text)
			{
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartElement(string prefix, string localName)
			{
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
			{
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartElement(string prefix, XmlDictionaryString localName)
			{
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEndStartElement(bool isEmpty)
			{
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEndElement(string prefix, string localName)
			{
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEndElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
			{
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteXmlnsAttribute(string prefix, string ns)
			{
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteXmlnsAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] nsBuffer, int nsOffset, int nsLength)
			{
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
			{
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartAttribute(string prefix, string localName)
			{
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
			{
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartAttribute(string prefix, XmlDictionaryString localName)
			{
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEndAttribute()
			{
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteCharEntity(int ch)
			{
			}

			// Token: 0x06000791 RID: 1937 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEscapedText(string value)
			{
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEscapedText(XmlDictionaryString value)
			{
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEscapedText(char[] chars, int offset, int count)
			{
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEscapedText(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteText(string value)
			{
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteText(XmlDictionaryString value)
			{
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteText(char[] chars, int offset, int count)
			{
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteText(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteInt32Text(int value)
			{
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteInt64Text(long value)
			{
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteBoolText(bool value)
			{
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteUInt64Text(ulong value)
			{
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteFloatText(float value)
			{
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteDoubleText(double value)
			{
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteDecimalText(decimal value)
			{
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteDateTimeText(DateTime value)
			{
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteUniqueIdText(UniqueId value)
			{
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteTimeSpanText(TimeSpan value)
			{
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteGuidText(Guid value)
			{
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteStartListText()
			{
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteListSeparator()
			{
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteEndListText()
			{
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteBase64Text(byte[] trailBuffer, int trailCount, byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void WriteQualifiedName(string prefix, XmlDictionaryString localName)
			{
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x0001FA2D File Offset: 0x0001DC2D
			public XmlNullNodeWriter()
			{
			}
		}

		// Token: 0x0200008F RID: 143
		private class WriteBase64TextAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x060007AA RID: 1962 RVA: 0x0001FA35 File Offset: 0x0001DC35
			public WriteBase64TextAsyncResult(byte[] trailBuffer, int trailCount, byte[] buffer, int offset, int count, XmlNodeWriter nodeWriter, AsyncCallback callback, object state) : base(callback, state)
			{
				this.trailBuffer = trailBuffer;
				this.trailCount = trailCount;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
				this.nodeWriter = nodeWriter;
				base.Schedule();
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x0001FA74 File Offset: 0x0001DC74
			protected override void OnDoWork()
			{
				this.nodeWriter.WriteBase64Text(this.trailBuffer, this.trailCount, this.buffer, this.offset, this.count);
			}

			// Token: 0x0400036F RID: 879
			private byte[] trailBuffer;

			// Token: 0x04000370 RID: 880
			private int trailCount;

			// Token: 0x04000371 RID: 881
			private byte[] buffer;

			// Token: 0x04000372 RID: 882
			private int offset;

			// Token: 0x04000373 RID: 883
			private int count;

			// Token: 0x04000374 RID: 884
			private XmlNodeWriter nodeWriter;
		}
	}
}
