using System;
using System.Xml.Schema;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000467 RID: 1127
	internal sealed class XmlAttributeCache : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x06002B9C RID: 11164 RVA: 0x001049B9 File Offset: 0x00102BB9
		public void Init(XmlRawWriter wrapped)
		{
			this.SetWrappedWriter(wrapped);
			this.numEntries = 0;
			this.idxLastName = 0;
			this.hashCodeUnion = 0;
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x001049D7 File Offset: 0x00102BD7
		public int Count
		{
			get
			{
				return this.numEntries;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x001049DF File Offset: 0x00102BDF
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x001049E7 File Offset: 0x00102BE7
		public OnRemoveWriter OnRemoveWriterEvent
		{
			get
			{
				return this.onRemove;
			}
			set
			{
				this.onRemove = value;
			}
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x001049F0 File Offset: 0x00102BF0
		private void SetWrappedWriter(XmlRawWriter writer)
		{
			IRemovableWriter removableWriter = writer as IRemovableWriter;
			if (removableWriter != null)
			{
				removableWriter.OnRemoveWriterEvent = new OnRemoveWriter(this.SetWrappedWriter);
			}
			this.wrapped = writer;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x00104A20 File Offset: 0x00102C20
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			int num = 0;
			int num2 = 1 << (int)localName[0];
			if ((this.hashCodeUnion & num2) != 0)
			{
				while (!this.arrAttrs[num].IsDuplicate(localName, ns, num2))
				{
					num = this.arrAttrs[num].NextNameIndex;
					if (num == 0)
					{
						break;
					}
				}
			}
			else
			{
				this.hashCodeUnion |= num2;
			}
			this.EnsureAttributeCache();
			if (this.numEntries != 0)
			{
				this.arrAttrs[this.idxLastName].NextNameIndex = this.numEntries;
			}
			int num3 = this.numEntries;
			this.numEntries = num3 + 1;
			this.idxLastName = num3;
			this.arrAttrs[this.idxLastName].Init(prefix, localName, ns, num2);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteEndAttribute()
		{
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x00104ADF File Offset: 0x00102CDF
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.FlushAttributes();
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x00104AF4 File Offset: 0x00102CF4
		public override void WriteString(string text)
		{
			this.EnsureAttributeCache();
			XmlAttributeCache.AttrNameVal[] array = this.arrAttrs;
			int num = this.numEntries;
			this.numEntries = num + 1;
			array[num].Init(text);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00104B2C File Offset: 0x00102D2C
		public override void WriteValue(object value)
		{
			this.EnsureAttributeCache();
			XmlAttributeCache.AttrNameVal[] array = this.arrAttrs;
			int num = this.numEntries;
			this.numEntries = num + 1;
			array[num].Init((XmlAtomicValue)value);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00104B66 File Offset: 0x00102D66
		public override void WriteValue(string value)
		{
			this.WriteValue(value);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00104B6F File Offset: 0x00102D6F
		internal override void StartElementContent()
		{
			this.FlushAttributes();
			this.wrapped.StartElementContent();
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteComment(string text)
		{
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x00104B82 File Offset: 0x00102D82
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00104B8F File Offset: 0x00102D8F
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x00104B9C File Offset: 0x00102D9C
		private void FlushAttributes()
		{
			int num = 0;
			while (num != this.numEntries)
			{
				int nextNameIndex = this.arrAttrs[num].NextNameIndex;
				if (nextNameIndex == 0)
				{
					nextNameIndex = this.numEntries;
				}
				string localName = this.arrAttrs[num].LocalName;
				if (localName != null)
				{
					string prefix = this.arrAttrs[num].Prefix;
					string @namespace = this.arrAttrs[num].Namespace;
					this.wrapped.WriteStartAttribute(prefix, localName, @namespace);
					while (++num != nextNameIndex)
					{
						string text = this.arrAttrs[num].Text;
						if (text != null)
						{
							this.wrapped.WriteString(text);
						}
						else
						{
							this.wrapped.WriteValue(this.arrAttrs[num].Value);
						}
					}
					this.wrapped.WriteEndAttribute();
				}
				else
				{
					num = nextNameIndex;
				}
			}
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x00104C98 File Offset: 0x00102E98
		private void EnsureAttributeCache()
		{
			if (this.arrAttrs == null)
			{
				this.arrAttrs = new XmlAttributeCache.AttrNameVal[32];
				return;
			}
			if (this.numEntries >= this.arrAttrs.Length)
			{
				XmlAttributeCache.AttrNameVal[] destinationArray = new XmlAttributeCache.AttrNameVal[this.numEntries * 2];
				Array.Copy(this.arrAttrs, destinationArray, this.numEntries);
				this.arrAttrs = destinationArray;
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00104CF2 File Offset: 0x00102EF2
		public XmlAttributeCache()
		{
		}

		// Token: 0x04002295 RID: 8853
		private XmlRawWriter wrapped;

		// Token: 0x04002296 RID: 8854
		private OnRemoveWriter onRemove;

		// Token: 0x04002297 RID: 8855
		private XmlAttributeCache.AttrNameVal[] arrAttrs;

		// Token: 0x04002298 RID: 8856
		private int numEntries;

		// Token: 0x04002299 RID: 8857
		private int idxLastName;

		// Token: 0x0400229A RID: 8858
		private int hashCodeUnion;

		// Token: 0x0400229B RID: 8859
		private const int DefaultCacheSize = 32;

		// Token: 0x02000468 RID: 1128
		private struct AttrNameVal
		{
			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x00104CFA File Offset: 0x00102EFA
			public string LocalName
			{
				get
				{
					return this.localName;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00104D02 File Offset: 0x00102F02
			public string Prefix
			{
				get
				{
					return this.prefix;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x00104D0A File Offset: 0x00102F0A
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x00104D12 File Offset: 0x00102F12
			public string Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x00104D1A File Offset: 0x00102F1A
			public XmlAtomicValue Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x00104D22 File Offset: 0x00102F22
			// (set) Token: 0x06002BB8 RID: 11192 RVA: 0x00104D2A File Offset: 0x00102F2A
			public int NextNameIndex
			{
				get
				{
					return this.nextNameIndex;
				}
				set
				{
					this.nextNameIndex = value;
				}
			}

			// Token: 0x06002BB9 RID: 11193 RVA: 0x00104D33 File Offset: 0x00102F33
			public void Init(string prefix, string localName, string ns, int hashCode)
			{
				this.localName = localName;
				this.prefix = prefix;
				this.namespaceName = ns;
				this.hashCode = hashCode;
				this.nextNameIndex = 0;
			}

			// Token: 0x06002BBA RID: 11194 RVA: 0x00104D59 File Offset: 0x00102F59
			public void Init(string text)
			{
				this.text = text;
				this.value = null;
			}

			// Token: 0x06002BBB RID: 11195 RVA: 0x00104D69 File Offset: 0x00102F69
			public void Init(XmlAtomicValue value)
			{
				this.text = null;
				this.value = value;
			}

			// Token: 0x06002BBC RID: 11196 RVA: 0x00104D79 File Offset: 0x00102F79
			public bool IsDuplicate(string localName, string ns, int hashCode)
			{
				if (this.localName != null && this.hashCode == hashCode && this.localName.Equals(localName) && this.namespaceName.Equals(ns))
				{
					this.localName = null;
					return true;
				}
				return false;
			}

			// Token: 0x0400229C RID: 8860
			private string localName;

			// Token: 0x0400229D RID: 8861
			private string prefix;

			// Token: 0x0400229E RID: 8862
			private string namespaceName;

			// Token: 0x0400229F RID: 8863
			private string text;

			// Token: 0x040022A0 RID: 8864
			private XmlAtomicValue value;

			// Token: 0x040022A1 RID: 8865
			private int hashCode;

			// Token: 0x040022A2 RID: 8866
			private int nextNameIndex;
		}
	}
}
