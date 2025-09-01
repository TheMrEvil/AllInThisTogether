using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	/// <summary>Represents the XML object model for encoding security objects. This class cannot be inherited.</summary>
	// Token: 0x020003E6 RID: 998
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag.</summary>
		/// <param name="tag">The tag name of an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter is invalid in XML.</exception>
		// Token: 0x06002910 RID: 10512 RVA: 0x00094E28 File Offset: 0x00093028
		public SecurityElement(string tag) : this(tag, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag and text.</summary>
		/// <param name="tag">The tag name of the XML element.</param>
		/// <param name="text">The text content within the element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter or <paramref name="text" /> parameter is invalid in XML.</exception>
		// Token: 0x06002911 RID: 10513 RVA: 0x00094E34 File Offset: 0x00093034
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00094E88 File Offset: 0x00093088
		internal SecurityElement(SecurityElement se)
		{
			this.Tag = se.Tag;
			this.Text = se.Text;
			if (se.attributes != null)
			{
				foreach (object obj in se.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					this.AddAttribute(securityAttribute.Name, securityAttribute.Value);
				}
			}
			if (se.children != null)
			{
				foreach (object obj2 in se.children)
				{
					SecurityElement child = (SecurityElement)obj2;
					this.AddChild(child);
				}
			}
		}

		/// <summary>Gets or sets the attributes of an XML element as name/value pairs.</summary>
		/// <returns>The <see cref="T:System.Collections.Hashtable" /> object for the attribute values of the XML element.</returns>
		/// <exception cref="T:System.InvalidCastException">The name or value of the <see cref="T:System.Collections.Hashtable" /> object is invalid.</exception>
		/// <exception cref="T:System.ArgumentException">The name is not a valid XML attribute name.</exception>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x00094F64 File Offset: 0x00093164
		// (set) Token: 0x06002914 RID: 10516 RVA: 0x00094FE4 File Offset: 0x000931E4
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.attributes.Clear();
					return;
				}
				if (this.attributes == null)
				{
					this.attributes = new ArrayList();
				}
				else
				{
					this.attributes.Clear();
				}
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.attributes.Add(new SecurityElement.SecurityAttribute((string)enumerator.Key, (string)enumerator.Value));
				}
			}
		}

		/// <summary>Gets or sets the array of child elements of the XML element.</summary>
		/// <returns>The ordered child elements of the XML element as security elements.</returns>
		/// <exception cref="T:System.ArgumentException">A child of the XML parent node is <see langword="null" />.</exception>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x00095060 File Offset: 0x00093260
		// (set) Token: 0x06002916 RID: 10518 RVA: 0x00095068 File Offset: 0x00093268
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
			set
			{
				if (value != null)
				{
					using (IEnumerator enumerator = value.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw new ArgumentNullException();
							}
						}
					}
				}
				this.children = value;
			}
		}

		/// <summary>Gets or sets the tag name of an XML element.</summary>
		/// <returns>The tag name of an XML element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The tag is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The tag is not valid in XML.</exception>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x000950C4 File Offset: 0x000932C4
		// (set) Token: 0x06002918 RID: 10520 RVA: 0x000950CC File Offset: 0x000932CC
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text within an XML element.</summary>
		/// <returns>The value of the text within an XML element.</returns>
		/// <exception cref="T:System.ArgumentException">The text is not valid in XML.</exception>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x00095106 File Offset: 0x00093306
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x0009510E File Offset: 0x0009330E
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		/// <summary>Adds a name/value attribute to an XML element.</summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is invalid in XML.  
		///  -or-  
		///  An attribute with the name specified by the <paramref name="name" /> parameter already exists.</exception>
		// Token: 0x0600291B RID: 10523 RVA: 0x00095144 File Offset: 0x00093344
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		/// <summary>Adds a child element to the XML element.</summary>
		/// <param name="child">The child element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600291C RID: 10524 RVA: 0x000951B2 File Offset: 0x000933B2
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		/// <summary>Finds an attribute by name in an XML element.</summary>
		/// <param name="name">The name of the attribute for which to search.</param>
		/// <returns>The value associated with the named attribute, or <see langword="null" /> if no attribute with <paramref name="name" /> exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600291D RID: 10525 RVA: 0x000951E4 File Offset: 0x000933E4
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			if (attribute != null)
			{
				return attribute.Value;
			}
			return null;
		}

		/// <summary>Creates and returns an identical copy of the current <see cref="T:System.Security.SecurityElement" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Security.SecurityElement" /> object.</returns>
		// Token: 0x0600291E RID: 10526 RVA: 0x00095212 File Offset: 0x00093412
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this);
		}

		/// <summary>Compares two XML element objects for equality.</summary>
		/// <param name="other">An XML element object to which to compare the current XML element object.</param>
		/// <returns>
		///   <see langword="true" /> if the tag, attribute names and values, child elements, and text fields in the current XML element are identical to their counterparts in the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600291F RID: 10527 RVA: 0x0009521C File Offset: 0x0009341C
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.text != other.text)
			{
				return false;
			}
			if (this.tag != other.tag)
			{
				return false;
			}
			if (this.attributes == null && other.attributes != null && other.attributes.Count != 0)
			{
				return false;
			}
			if (other.attributes == null && this.attributes != null && this.attributes.Count != 0)
			{
				return false;
			}
			if (this.attributes != null && other.attributes != null)
			{
				if (this.attributes.Count != other.attributes.Count)
				{
					return false;
				}
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					SecurityElement.SecurityAttribute attribute = other.GetAttribute(securityAttribute.Name);
					if (attribute == null || securityAttribute.Value != attribute.Value)
					{
						return false;
					}
				}
			}
			if (this.children == null && other.children != null && other.children.Count != 0)
			{
				return false;
			}
			if (other.children == null && this.children != null && this.children.Count != 0)
			{
				return false;
			}
			if (this.children != null && other.children != null)
			{
				if (this.children.Count != other.children.Count)
				{
					return false;
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					if (!((SecurityElement)this.children[i]).Equal((SecurityElement)other.children[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Replaces invalid XML characters in a string with their valid XML equivalent.</summary>
		/// <param name="str">The string within which to escape invalid characters.</param>
		/// <returns>The input string with invalid characters replaced.</returns>
		// Token: 0x06002920 RID: 10528 RVA: 0x000953F0 File Offset: 0x000935F0
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c != '&')
						{
							goto IL_96;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_96;
						}
						stringBuilder.Append("&gt;");
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&apos;");
				}
				IL_9E:
				i++;
				continue;
				IL_96:
				stringBuilder.Append(c);
				goto IL_9E;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000954AC File Offset: 0x000936AC
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		/// <summary>Creates a security element from an XML-encoded string.</summary>
		/// <param name="xml">The XML-encoded string from which to create the security element.</param>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> created from the XML.</returns>
		/// <exception cref="T:System.Security.XmlSyntaxException">
		///   <paramref name="xml" /> contains one or more single quotation mark characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is <see langword="null" />.</exception>
		// Token: 0x06002922 RID: 10530 RVA: 0x00095520 File Offset: 0x00093720
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement result;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				result = securityParser.ToXml();
			}
			catch (Exception inner)
			{
				throw new XmlSyntaxException(Locale.GetText("Invalid XML."), inner);
			}
			return result;
		}

		/// <summary>Determines whether a string is a valid attribute name.</summary>
		/// <param name="name">The attribute name to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="name" /> parameter is a valid XML attribute name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002923 RID: 10531 RVA: 0x0009558C File Offset: 0x0009378C
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid attribute value.</summary>
		/// <param name="value">The attribute value to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a valid XML attribute value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002924 RID: 10532 RVA: 0x000955A1 File Offset: 0x000937A1
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid tag.</summary>
		/// <param name="tag">The tag to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="tag" /> parameter is a valid XML tag; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002925 RID: 10533 RVA: 0x000955B6 File Offset: 0x000937B6
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		/// <summary>Determines whether a string is valid as text within an XML element.</summary>
		/// <param name="text">The text to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="text" /> parameter is a valid XML text element; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002926 RID: 10534 RVA: 0x000955CB File Offset: 0x000937CB
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		/// <summary>Finds a child by its tag name.</summary>
		/// <param name="tag">The tag for which to search in child elements.</param>
		/// <returns>The first child XML element with the specified tag value, or <see langword="null" /> if no child element with <paramref name="tag" /> exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002927 RID: 10535 RVA: 0x000955E0 File Offset: 0x000937E0
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		/// <summary>Finds a child by its tag name and returns the contained text.</summary>
		/// <param name="tag">The tag for which to search in child elements.</param>
		/// <returns>The text contents of the first child element with the specified tag value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tag" /> is <see langword="null" />.</exception>
		// Token: 0x06002928 RID: 10536 RVA: 0x00095640 File Offset: 0x00093840
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.tag == tag)
			{
				return this.text;
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				string text = ((SecurityElement)this.children[i]).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		/// <summary>Produces a string representation of an XML element and its constituent attributes, child elements, and text.</summary>
		/// <returns>The XML element and its contents.</returns>
		// Token: 0x06002929 RID: 10537 RVA: 0x000956B0 File Offset: 0x000938B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000956D4 File Offset: 0x000938D4
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value)).Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
				return;
			}
			s.Append(">").Append(SecurityElement.Escape(this.text));
			if (this.children != null)
			{
				s.Append(Environment.NewLine);
				foreach (object obj in this.children)
				{
					((SecurityElement)obj).ToXml(ref s, level + 1);
				}
			}
			s.Append("</").Append(this.tag).Append(">").Append(Environment.NewLine);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00095880 File Offset: 0x00093A80
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x000950C4 File Offset: 0x000932C4
		internal string m_strTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00095106 File Offset: 0x00093306
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000958F0 File Offset: 0x00093AF0
		internal string m_strText
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x000958F9 File Offset: 0x00093AF9
		internal ArrayList m_lAttributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x00095060 File Offset: 0x00093260
		internal ArrayList InternalChildren
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x00095904 File Offset: 0x00093B04
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.tag == null)
			{
				return null;
			}
			if (this.tag.Equals(strLocalName) || this.tag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.text);
			}
			if (this.children == null)
			{
				return null;
			}
			foreach (object obj in this.children)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00095994 File Offset: 0x00093B94
		// Note: this type is marked as 'beforefieldinit'.
		static SecurityElement()
		{
		}

		// Token: 0x04001ED6 RID: 7894
		private string text;

		// Token: 0x04001ED7 RID: 7895
		private string tag;

		// Token: 0x04001ED8 RID: 7896
		private ArrayList attributes;

		// Token: 0x04001ED9 RID: 7897
		private ArrayList children;

		// Token: 0x04001EDA RID: 7898
		private static readonly char[] invalid_tag_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04001EDB RID: 7899
		private static readonly char[] invalid_text_chars = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x04001EDC RID: 7900
		private static readonly char[] invalid_attr_name_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04001EDD RID: 7901
		private static readonly char[] invalid_attr_value_chars = new char[]
		{
			'"',
			'<',
			'>'
		};

		// Token: 0x04001EDE RID: 7902
		private static readonly char[] invalid_chars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x020003E7 RID: 999
		internal class SecurityAttribute
		{
			// Token: 0x06002933 RID: 10547 RVA: 0x00095A10 File Offset: 0x00093C10
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x06002934 RID: 10548 RVA: 0x00095A7C File Offset: 0x00093C7C
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06002935 RID: 10549 RVA: 0x00095A84 File Offset: 0x00093C84
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04001EDF RID: 7903
			private string _name;

			// Token: 0x04001EE0 RID: 7904
			private string _value;
		}
	}
}
