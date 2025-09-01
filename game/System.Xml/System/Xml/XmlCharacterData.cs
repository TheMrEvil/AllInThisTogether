using System;
using System.Text;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Provides text manipulation methods that are used by several classes.</summary>
	// Token: 0x020001B7 RID: 439
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCharacterData" /> class.</summary>
		/// <param name="data">String that contains character data to be added to document.</param>
		/// <param name="doc">
		///       <see cref="T:System.Xml.XmlDocument" /> to contain character data.</param>
		// Token: 0x06001027 RID: 4135 RVA: 0x000673F4 File Offset: 0x000655F4
		protected internal XmlCharacterData(string data, XmlDocument doc) : base(doc)
		{
			this.data = data;
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00067404 File Offset: 0x00065604
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0006740C File Offset: 0x0006560C
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all the children of the node.</summary>
		/// <returns>The concatenated values of the node and all the children of the node.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00067415 File Offset: 0x00065615
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0006741D File Offset: 0x0006561D
		public override string InnerText
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Contains the data of the node.</summary>
		/// <returns>The data of the node.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00067426 File Offset: 0x00065626
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0006743C File Offset: 0x0006563C
		public virtual string Data
		{
			get
			{
				if (this.data != null)
				{
					return this.data;
				}
				return string.Empty;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		/// <summary>Gets the length of the data, in characters.</summary>
		/// <returns>The length, in characters, of the string in the <see cref="P:System.Xml.XmlCharacterData.Data" /> property. The length may be zero; that is, CharacterData nodes can be empty.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0006747D File Offset: 0x0006567D
		public virtual int Length
		{
			get
			{
				if (this.data != null)
				{
					return this.data.Length;
				}
				return 0;
			}
		}

		/// <summary>Retrieves a substring of the full string from the specified range.</summary>
		/// <param name="offset">The position within the string to start retrieving. An offset of zero indicates the starting point is at the start of the data. </param>
		/// <param name="count">The number of characters to retrieve. </param>
		/// <returns>The substring corresponding to the specified range.</returns>
		// Token: 0x0600102F RID: 4143 RVA: 0x00067494 File Offset: 0x00065694
		public virtual string Substring(int offset, int count)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0)
			{
				if (num < offset + count)
				{
					count = num - offset;
				}
				return this.data.Substring(offset, count);
			}
			return string.Empty;
		}

		/// <summary>Appends the specified string to the end of the character data of the node.</summary>
		/// <param name="strData">The string to insert into the existing string. </param>
		// Token: 0x06001030 RID: 4144 RVA: 0x000674DC File Offset: 0x000656DC
		public virtual void AppendData(string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = (this.data != null) ? this.data.Length : 0;
			if (strData != null)
			{
				num += strData.Length;
			}
			string newValue = new StringBuilder(num).Append(this.data).Append(strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Inserts the specified string at the specified character offset.</summary>
		/// <param name="offset">The position within the string to insert the supplied string data. </param>
		/// <param name="strData">The string data that is to be inserted into the existing string. </param>
		// Token: 0x06001031 RID: 4145 RVA: 0x00067560 File Offset: 0x00065760
		public virtual void InsertData(int offset, string strData)
		{
			XmlNode parentNode = this.ParentNode;
			int num = (this.data != null) ? this.data.Length : 0;
			if (strData != null)
			{
				num += strData.Length;
			}
			string newValue = new StringBuilder(num).Append(this.data).Insert(offset, strData).ToString();
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Removes a range of characters from the node.</summary>
		/// <param name="offset">The position within the string to start deleting. </param>
		/// <param name="count">The number of characters to delete. </param>
		// Token: 0x06001032 RID: 4146 RVA: 0x000675E4 File Offset: 0x000657E4
		public virtual void DeleteData(int offset, int count)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			string newValue = new StringBuilder(this.data).Remove(offset, count).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		/// <summary>Replaces the specified number of characters starting at the specified offset with the specified string.</summary>
		/// <param name="offset">The position within the string to start replacing. </param>
		/// <param name="count">The number of characters to replace. </param>
		/// <param name="strData">The new data that replaces the old string data. </param>
		// Token: 0x06001033 RID: 4147 RVA: 0x0006766C File Offset: 0x0006586C
		public virtual void ReplaceData(int offset, int count, string strData)
		{
			int num = (this.data != null) ? this.data.Length : 0;
			if (num > 0 && num < offset + count)
			{
				count = Math.Max(num - offset, 0);
			}
			string newValue = new StringBuilder(this.data).Remove(offset, count).Insert(offset, strData).ToString();
			XmlNode parentNode = this.ParentNode;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, newValue, XmlNodeChangedAction.Change);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			this.data = newValue;
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000676F8 File Offset: 0x000658F8
		internal bool CheckOnData(string data)
		{
			return XmlCharType.Instance.IsOnlyWhitespace(data);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00067714 File Offset: 0x00065914
		internal bool DecideXPNodeTypeForTextNodes(XmlNode node, ref XPathNodeType xnt)
		{
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType <= XmlNodeType.EntityReference)
				{
					if (nodeType - XmlNodeType.Text <= 1)
					{
						xnt = XPathNodeType.Text;
						return false;
					}
					if (nodeType != XmlNodeType.EntityReference)
					{
						return false;
					}
					if (!this.DecideXPNodeTypeForTextNodes(node.FirstChild, ref xnt))
					{
						return false;
					}
				}
				else if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return false;
					}
					xnt = XPathNodeType.SignificantWhitespace;
				}
				node = node.NextSibling;
			}
			return true;
		}

		// Token: 0x0400103C RID: 4156
		private string data;
	}
}
