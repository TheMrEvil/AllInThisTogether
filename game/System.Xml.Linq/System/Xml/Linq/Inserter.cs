using System;
using System.Collections;

namespace System.Xml.Linq
{
	// Token: 0x0200003F RID: 63
	internal struct Inserter
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000B541 File Offset: 0x00009741
		public Inserter(XContainer parent, XNode anchor)
		{
			this._parent = parent;
			this._previous = anchor;
			this._text = null;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B558 File Offset: 0x00009758
		public void Add(object content)
		{
			this.AddContent(content);
			if (this._text != null)
			{
				if (this._parent.content == null)
				{
					if (this._parent.SkipNotify())
					{
						this._parent.content = this._text;
						return;
					}
					if (this._text.Length > 0)
					{
						this.InsertNode(new XText(this._text));
						return;
					}
					if (!(this._parent is XElement))
					{
						this._parent.content = this._text;
						return;
					}
					this._parent.NotifyChanging(this._parent, XObjectChangeEventArgs.Value);
					if (this._parent.content != null)
					{
						throw new InvalidOperationException("This operation was corrupted by external code.");
					}
					this._parent.content = this._text;
					this._parent.NotifyChanged(this._parent, XObjectChangeEventArgs.Value);
					return;
				}
				else if (this._text.Length > 0)
				{
					XText xtext = this._previous as XText;
					if (xtext != null && !(this._previous is XCData))
					{
						XText xtext2 = xtext;
						xtext2.Value += this._text;
						return;
					}
					this._parent.ConvertTextToNode();
					this.InsertNode(new XText(this._text));
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B69C File Offset: 0x0000989C
		private void AddContent(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddString(text);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNode(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object content2 in array)
				{
					this.AddContent(content2);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object content3 in enumerable)
				{
					this.AddContent(content3);
				}
				return;
			}
			if (content is XAttribute)
			{
				throw new ArgumentException("An attribute cannot be added to content.");
			}
			this.AddString(XContainer.GetStringValue(content));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B790 File Offset: 0x00009990
		private void AddNode(XNode n)
		{
			this._parent.ValidateNode(n, this._previous);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode parent = this._parent;
				while (parent.parent != null)
				{
					parent = parent.parent;
				}
				if (n == parent)
				{
					n = n.CloneNode();
				}
			}
			this._parent.ConvertTextToNode();
			if (this._text != null)
			{
				if (this._text.Length > 0)
				{
					XText xtext = this._previous as XText;
					if (xtext != null && !(this._previous is XCData))
					{
						XText xtext2 = xtext;
						xtext2.Value += this._text;
					}
					else
					{
						this.InsertNode(new XText(this._text));
					}
				}
				this._text = null;
			}
			this.InsertNode(n);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B85A File Offset: 0x00009A5A
		private void AddString(string s)
		{
			this._parent.ValidateString(s);
			this._text += s;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B87C File Offset: 0x00009A7C
		private void InsertNode(XNode n)
		{
			bool flag = this._parent.NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			n.parent = this._parent;
			if (this._parent.content == null || this._parent.content is string)
			{
				n.next = n;
				this._parent.content = n;
			}
			else if (this._previous == null)
			{
				XNode xnode = (XNode)this._parent.content;
				n.next = xnode.next;
				xnode.next = n;
			}
			else
			{
				n.next = this._previous.next;
				this._previous.next = n;
				if (this._parent.content == this._previous)
				{
					this._parent.content = n;
				}
			}
			this._previous = n;
			if (flag)
			{
				this._parent.NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x04000147 RID: 327
		private XContainer _parent;

		// Token: 0x04000148 RID: 328
		private XNode _previous;

		// Token: 0x04000149 RID: 329
		private string _text;
	}
}
