using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace System.Xml.Linq
{
	/// <summary>Represents elements in an XML tree that supports deferred streaming output.</summary>
	// Token: 0x02000060 RID: 96
	public class XStreamingElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XElement" /> class from the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the name of the element.</param>
		// Token: 0x0600039A RID: 922 RVA: 0x0001036E File Offset: 0x0000E56E
		public XStreamingElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x00010391 File Offset: 0x0000E591
		public XStreamingElement(XName name, object content) : this(name)
		{
			object obj;
			if (!(content is List<object>))
			{
				obj = content;
			}
			else
			{
				(obj = new object[1])[0] = content;
			}
			this.content = obj;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XStreamingElement" /> class with the specified name and content.</summary>
		/// <param name="name">An <see cref="T:System.Xml.Linq.XName" /> that contains the element name.</param>
		/// <param name="content">The contents of the element.</param>
		// Token: 0x0600039C RID: 924 RVA: 0x000103B5 File Offset: 0x0000E5B5
		public XStreamingElement(XName name, params object[] content) : this(name)
		{
			this.content = content;
		}

		/// <summary>Gets or sets the name of this streaming element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> that contains the name of this streaming element.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000103C5 File Offset: 0x0000E5C5
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000103CD File Offset: 0x0000E5CD
		public XName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		// Token: 0x0600039F RID: 927 RVA: 0x000103EC File Offset: 0x0000E5EC
		public void Add(object content)
		{
			if (content != null)
			{
				List<object> list = this.content as List<object>;
				if (list == null)
				{
					list = new List<object>();
					if (this.content != null)
					{
						list.Add(this.content);
					}
					this.content = list;
				}
				list.Add(content);
			}
		}

		/// <summary>Adds the specified content as children to this <see cref="T:System.Xml.Linq.XStreamingElement" />.</summary>
		/// <param name="content">Content to be added to the streaming element.</param>
		// Token: 0x060003A0 RID: 928 RVA: 0x00010433 File Offset: 0x0000E633
		public void Add(params object[] content)
		{
			this.Add(content);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XStreamingElement" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		// Token: 0x060003A1 RID: 929 RVA: 0x0001043C File Offset: 0x0000E63C
		public void Save(Stream stream)
		{
			this.Save(stream, SaveOptions.None);
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XStreamingElement" /> to the specified <see cref="T:System.IO.Stream" />, optionally specifying formatting behavior.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> object that specifies formatting behavior.</param>
		// Token: 0x060003A2 RID: 930 RVA: 0x00010448 File Offset: 0x0000E648
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XStreamingElement" /> will be written to.</param>
		// Token: 0x060003A3 RID: 931 RVA: 0x00010488 File Offset: 0x0000E688
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, SaveOptions.None);
		}

		/// <summary>Serialize this streaming element to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x060003A4 RID: 932 RVA: 0x00010494 File Offset: 0x0000E694
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XElement" /> will be written to.</param>
		// Token: 0x060003A5 RID: 933 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public void Save(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartDocument();
			this.WriteTo(writer);
			writer.WriteEndDocument();
		}

		/// <summary>Serialize this streaming element to a file.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		// Token: 0x060003A6 RID: 934 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public void Save(string fileName)
		{
			this.Save(fileName, SaveOptions.None);
		}

		/// <summary>Serialize this streaming element to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> object that specifies formatting behavior.</param>
		// Token: 0x060003A7 RID: 935 RVA: 0x00010504 File Offset: 0x0000E704
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Returns the formatted (indented) XML for this streaming element.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		// Token: 0x060003A8 RID: 936 RVA: 0x00010544 File Offset: 0x0000E744
		public override string ToString()
		{
			return this.GetXmlString(SaveOptions.None);
		}

		/// <summary>Returns the XML for this streaming element, optionally disabling formatting.</summary>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		// Token: 0x060003A9 RID: 937 RVA: 0x0001054D File Offset: 0x0000E74D
		public string ToString(SaveOptions options)
		{
			return this.GetXmlString(options);
		}

		/// <summary>Writes this streaming element to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x060003AA RID: 938 RVA: 0x00010558 File Offset: 0x0000E758
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new StreamingElementWriter(writer).WriteStreamingElement(this);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010584 File Offset: 0x0000E784
		private string GetXmlString(SaveOptions o)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
				{
					xmlWriterSettings.Indent = true;
				}
				if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
				{
					xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					this.WriteTo(xmlWriter);
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x040001E0 RID: 480
		internal XName name;

		// Token: 0x040001E1 RID: 481
		internal object content;
	}
}
