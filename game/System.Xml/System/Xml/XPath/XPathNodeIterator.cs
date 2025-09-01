using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace System.Xml.XPath
{
	/// <summary>Provides an iterator over a selected set of nodes.</summary>
	// Token: 0x02000261 RID: 609
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy}")]
	public abstract class XPathNodeIterator : ICloneable, IEnumerable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x060016F3 RID: 5875 RVA: 0x000885AB File Offset: 0x000867AB
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>When overridden in a derived class, returns a clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</summary>
		/// <returns>A new <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object clone of this <see cref="T:System.Xml.XPath.XPathNodeIterator" /> object.</returns>
		// Token: 0x060016F4 RID: 5876
		public abstract XPathNodeIterator Clone();

		/// <summary>When overridden in a derived class, moves the <see cref="T:System.Xml.XPath.XPathNavigator" /> object returned by the <see cref="P:System.Xml.XPath.XPathNodeIterator.Current" /> property to the next node in the selected node set.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XPath.XPathNavigator" /> object moved to the next node; <see langword="false" /> if there are no more selected nodes.</returns>
		// Token: 0x060016F5 RID: 5877
		public abstract bool MoveNext();

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XPath.XPathNavigator" /> object for this <see cref="T:System.Xml.XPath.XPathNodeIterator" />, positioned on the current context node.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object positioned on the context node from which the node set was selected. The <see cref="M:System.Xml.XPath.XPathNodeIterator.MoveNext" /> method must be called to move the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> to the first node in the selected set.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060016F6 RID: 5878
		public abstract XPathNavigator Current { get; }

		/// <summary>When overridden in a derived class, gets the index of the current position in the selected set of nodes.</summary>
		/// <returns>The index of the current position.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060016F7 RID: 5879
		public abstract int CurrentPosition { get; }

		/// <summary>Gets the index of the last node in the selected set of nodes.</summary>
		/// <returns>The index of the last node in the selected set of nodes, or 0 if there are no selected nodes.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000885B4 File Offset: 0x000867B4
		public virtual int Count
		{
			get
			{
				if (this.count == -1)
				{
					XPathNodeIterator xpathNodeIterator = this.Clone();
					while (xpathNodeIterator.MoveNext())
					{
					}
					this.count = xpathNodeIterator.CurrentPosition;
				}
				return this.count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to iterate through the selected node set.</returns>
		// Token: 0x060016F9 RID: 5881 RVA: 0x000885EB File Offset: 0x000867EB
		public virtual IEnumerator GetEnumerator()
		{
			return new XPathNodeIterator.Enumerator(this);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000885F3 File Offset: 0x000867F3
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.Current != null)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> class.</summary>
		// Token: 0x060016FB RID: 5883 RVA: 0x0008860F File Offset: 0x0008680F
		protected XPathNodeIterator()
		{
		}

		// Token: 0x04001824 RID: 6180
		internal int count = -1;

		// Token: 0x02000262 RID: 610
		private class Enumerator : IEnumerator
		{
			// Token: 0x060016FC RID: 5884 RVA: 0x0008861E File Offset: 0x0008681E
			public Enumerator(XPathNodeIterator original)
			{
				this.original = original.Clone();
			}

			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x060016FD RID: 5885 RVA: 0x00088634 File Offset: 0x00086834
			public virtual object Current
			{
				get
				{
					if (!this.iterationStarted)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[]
						{
							string.Empty
						}));
					}
					if (this.current == null)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[]
						{
							string.Empty
						}));
					}
					return this.current.Current.Clone();
				}
			}

			// Token: 0x060016FE RID: 5886 RVA: 0x000886A0 File Offset: 0x000868A0
			public virtual bool MoveNext()
			{
				if (!this.iterationStarted)
				{
					this.current = this.original.Clone();
					this.iterationStarted = true;
				}
				if (this.current == null || !this.current.MoveNext())
				{
					this.current = null;
					return false;
				}
				return true;
			}

			// Token: 0x060016FF RID: 5887 RVA: 0x000886EC File Offset: 0x000868EC
			public virtual void Reset()
			{
				this.iterationStarted = false;
			}

			// Token: 0x04001825 RID: 6181
			private XPathNodeIterator original;

			// Token: 0x04001826 RID: 6182
			private XPathNodeIterator current;

			// Token: 0x04001827 RID: 6183
			private bool iterationStarted;
		}

		// Token: 0x02000263 RID: 611
		private struct DebuggerDisplayProxy
		{
			// Token: 0x06001700 RID: 5888 RVA: 0x000886F5 File Offset: 0x000868F5
			public DebuggerDisplayProxy(XPathNodeIterator nodeIterator)
			{
				this.nodeIterator = nodeIterator;
			}

			// Token: 0x06001701 RID: 5889 RVA: 0x00088700 File Offset: 0x00086900
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Position=");
				stringBuilder.Append(this.nodeIterator.CurrentPosition);
				stringBuilder.Append(", Current=");
				if (this.nodeIterator.Current == null)
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append('{');
					stringBuilder.Append(new XPathNavigator.DebuggerDisplayProxy(this.nodeIterator.Current).ToString());
					stringBuilder.Append('}');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001828 RID: 6184
			private XPathNodeIterator nodeIterator;
		}
	}
}
