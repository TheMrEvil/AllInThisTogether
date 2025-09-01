using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000230 RID: 560
	internal sealed class SqlStreamingXml
	{
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0007BD51 File Offset: 0x00079F51
		public SqlStreamingXml(int i, SqlDataReader reader)
		{
			this._columnOrdinal = i;
			this._reader = reader;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0007BD67 File Offset: 0x00079F67
		public void Close()
		{
			((IDisposable)this._xmlWriter).Dispose();
			((IDisposable)this._xmlReader).Dispose();
			this._reader = null;
			this._xmlReader = null;
			this._xmlWriter = null;
			this._strWriter = null;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0007BD9B File Offset: 0x00079F9B
		public int ColumnOrdinal
		{
			get
			{
				return this._columnOrdinal;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0007BDA4 File Offset: 0x00079FA4
		public long GetChars(long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._xmlReader == null)
			{
				SqlStream sqlStream = new SqlStream(this._columnOrdinal, this._reader, true, false, false);
				this._xmlReader = sqlStream.ToXmlReader(false);
				this._strWriter = new StringWriter(null);
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.CloseOutput = true;
				xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				this._xmlWriter = XmlWriter.Create(this._strWriter, xmlWriterSettings);
			}
			int num = 0;
			if (dataIndex < this._charsRemoved)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._charsRemoved, "GetChars");
			}
			if (dataIndex > this._charsRemoved)
			{
				num = (int)(dataIndex - this._charsRemoved);
			}
			if (buffer == null)
			{
				return -1L;
			}
			StringBuilder stringBuilder = this._strWriter.GetStringBuilder();
			int num2;
			while (!this._xmlReader.EOF && stringBuilder.Length < length + num)
			{
				this.WriteXmlElement();
				if (num > 0)
				{
					num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
					stringBuilder.Remove(0, num2);
					num -= num2;
					this._charsRemoved += (long)num2;
				}
			}
			if (num > 0)
			{
				num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
				stringBuilder.Remove(0, num2);
				num -= num2;
				this._charsRemoved += (long)num2;
			}
			if (stringBuilder.Length == 0)
			{
				return 0L;
			}
			num2 = ((stringBuilder.Length < length) ? stringBuilder.Length : length);
			for (int i = 0; i < num2; i++)
			{
				buffer[bufferIndex + i] = stringBuilder[i];
			}
			stringBuilder.Remove(0, num2);
			this._charsRemoved += (long)num2;
			return (long)num2;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0007BF34 File Offset: 0x0007A134
		private void WriteXmlElement()
		{
			if (this._xmlReader.EOF)
			{
				return;
			}
			bool canReadValueChunk = this._xmlReader.CanReadValueChunk;
			char[] array = null;
			this._xmlReader.Read();
			switch (this._xmlReader.NodeType)
			{
			case XmlNodeType.Element:
				this._xmlWriter.WriteStartElement(this._xmlReader.Prefix, this._xmlReader.LocalName, this._xmlReader.NamespaceURI);
				this._xmlWriter.WriteAttributes(this._xmlReader, true);
				if (this._xmlReader.IsEmptyElement)
				{
					this._xmlWriter.WriteEndElement();
				}
				break;
			case XmlNodeType.Text:
				if (canReadValueChunk)
				{
					if (array == null)
					{
						array = new char[1024];
					}
					int count;
					while ((count = this._xmlReader.ReadValueChunk(array, 0, 1024)) > 0)
					{
						this._xmlWriter.WriteChars(array, 0, count);
					}
				}
				else
				{
					this._xmlWriter.WriteString(this._xmlReader.Value);
				}
				break;
			case XmlNodeType.CDATA:
				this._xmlWriter.WriteCData(this._xmlReader.Value);
				break;
			case XmlNodeType.EntityReference:
				this._xmlWriter.WriteEntityRef(this._xmlReader.Name);
				break;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.XmlDeclaration:
				this._xmlWriter.WriteProcessingInstruction(this._xmlReader.Name, this._xmlReader.Value);
				break;
			case XmlNodeType.Comment:
				this._xmlWriter.WriteComment(this._xmlReader.Value);
				break;
			case XmlNodeType.DocumentType:
				this._xmlWriter.WriteDocType(this._xmlReader.Name, this._xmlReader.GetAttribute("PUBLIC"), this._xmlReader.GetAttribute("SYSTEM"), this._xmlReader.Value);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this._xmlWriter.WriteWhitespace(this._xmlReader.Value);
				break;
			case XmlNodeType.EndElement:
				this._xmlWriter.WriteFullEndElement();
				break;
			}
			this._xmlWriter.Flush();
		}

		// Token: 0x0400113C RID: 4412
		private int _columnOrdinal;

		// Token: 0x0400113D RID: 4413
		private SqlDataReader _reader;

		// Token: 0x0400113E RID: 4414
		private XmlReader _xmlReader;

		// Token: 0x0400113F RID: 4415
		private XmlWriter _xmlWriter;

		// Token: 0x04001140 RID: 4416
		private StringWriter _strWriter;

		// Token: 0x04001141 RID: 4417
		private long _charsRemoved;
	}
}
