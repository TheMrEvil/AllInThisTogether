using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000151 RID: 337
	internal class XmlSerializableWriter : XmlWriter
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x00044F80 File Offset: 0x00043180
		internal void BeginWrite(XmlWriter xmlWriter, object obj)
		{
			this.depth = 0;
			this.xmlWriter = xmlWriter;
			this.obj = obj;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00044F98 File Offset: 0x00043198
		internal void EndWrite()
		{
			if (this.depth != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("IXmlSerializable.WriteXml method of type '{0}' did not close all open tags. Verify that the IXmlSerializable implementation is correct.", new object[]
				{
					(this.obj == null) ? string.Empty : DataContract.GetClrTypeFullName(this.obj.GetType())
				})));
			}
			this.obj = null;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00044FF1 File Offset: 0x000431F1
		public override void WriteStartDocument()
		{
			if (this.WriteState == WriteState.Start)
			{
				this.xmlWriter.WriteStartDocument();
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00045006 File Offset: 0x00043206
		public override void WriteEndDocument()
		{
			this.xmlWriter.WriteEndDocument();
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00045013 File Offset: 0x00043213
		public override void WriteStartDocument(bool standalone)
		{
			if (this.WriteState == WriteState.Start)
			{
				this.xmlWriter.WriteStartDocument(standalone);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00045029 File Offset: 0x00043229
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.xmlWriter.WriteStartElement(prefix, localName, ns);
			this.depth++;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00045048 File Offset: 0x00043248
		public override void WriteEndElement()
		{
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("IXmlSerializable.WriteXml method of type '{0}' attempted to close too many tags.  Verify that the IXmlSerializable implementation is correct.", new object[]
				{
					(this.obj == null) ? string.Empty : DataContract.GetClrTypeFullName(this.obj.GetType())
				})));
			}
			this.xmlWriter.WriteEndElement();
			this.depth--;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000450B4 File Offset: 0x000432B4
		public override void WriteFullEndElement()
		{
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("IXmlSerializable.WriteXml method of type '{0}' attempted to close too many tags.  Verify that the IXmlSerializable implementation is correct.", new object[]
				{
					(this.obj == null) ? string.Empty : DataContract.GetClrTypeFullName(this.obj.GetType())
				})));
			}
			this.xmlWriter.WriteFullEndElement();
			this.depth--;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00044975 File Offset: 0x00042B75
		public override void Close()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("This method cannot be called from IXmlSerializable implementations.")));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004511F File Offset: 0x0004331F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.xmlWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004512F File Offset: 0x0004332F
		public override void WriteEndAttribute()
		{
			this.xmlWriter.WriteEndAttribute();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0004513C File Offset: 0x0004333C
		public override void WriteCData(string text)
		{
			this.xmlWriter.WriteCData(text);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0004514A File Offset: 0x0004334A
		public override void WriteComment(string text)
		{
			this.xmlWriter.WriteComment(text);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00045158 File Offset: 0x00043358
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.xmlWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00045167 File Offset: 0x00043367
		public override void WriteEntityRef(string name)
		{
			this.xmlWriter.WriteEntityRef(name);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00045175 File Offset: 0x00043375
		public override void WriteCharEntity(char ch)
		{
			this.xmlWriter.WriteCharEntity(ch);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00045183 File Offset: 0x00043383
		public override void WriteWhitespace(string ws)
		{
			this.xmlWriter.WriteWhitespace(ws);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00045191 File Offset: 0x00043391
		public override void WriteString(string text)
		{
			this.xmlWriter.WriteString(text);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004519F File Offset: 0x0004339F
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.xmlWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000451AE File Offset: 0x000433AE
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.xmlWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000451BE File Offset: 0x000433BE
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.xmlWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000451CE File Offset: 0x000433CE
		public override void WriteRaw(string data)
		{
			this.xmlWriter.WriteRaw(data);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000451DC File Offset: 0x000433DC
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.xmlWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000451EC File Offset: 0x000433EC
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.xmlWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x000451FC File Offset: 0x000433FC
		public override WriteState WriteState
		{
			get
			{
				return this.xmlWriter.WriteState;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00045209 File Offset: 0x00043409
		public override void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00045216 File Offset: 0x00043416
		public override void WriteName(string name)
		{
			this.xmlWriter.WriteName(name);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00045224 File Offset: 0x00043424
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.xmlWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00045233 File Offset: 0x00043433
		public override string LookupPrefix(string ns)
		{
			return this.xmlWriter.LookupPrefix(ns);
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00045241 File Offset: 0x00043441
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlWriter.XmlSpace;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0004524E File Offset: 0x0004344E
		public override string XmlLang
		{
			get
			{
				return this.xmlWriter.XmlLang;
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004525B File Offset: 0x0004345B
		public override void WriteNmToken(string name)
		{
			this.xmlWriter.WriteNmToken(name);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0001995D File Offset: 0x00017B5D
		public XmlSerializableWriter()
		{
		}

		// Token: 0x0400072F RID: 1839
		private XmlWriter xmlWriter;

		// Token: 0x04000730 RID: 1840
		private int depth;

		// Token: 0x04000731 RID: 1841
		private object obj;
	}
}
