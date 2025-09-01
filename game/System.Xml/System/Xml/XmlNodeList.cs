using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	/// <summary>Represents an ordered collection of nodes.</summary>
	// Token: 0x020001D4 RID: 468
	public abstract class XmlNodeList : IEnumerable, IDisposable
	{
		/// <summary>Retrieves a node at the given index.</summary>
		/// <param name="index">The zero-based index into the list of nodes.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the list, this returns <see langword="null" />.</returns>
		// Token: 0x06001243 RID: 4675
		public abstract XmlNode Item(int index);

		/// <summary>Gets the number of nodes in the <see langword="XmlNodeList" />.</summary>
		/// <returns>The number of nodes in the <see langword="XmlNodeList" />.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001244 RID: 4676
		public abstract int Count { get; }

		/// <summary>Gets an enumerator that iterates through the collection of nodes.</summary>
		/// <returns>An enumerator used to iterate through the collection of nodes.</returns>
		// Token: 0x06001245 RID: 4677
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets a node at the given index.</summary>
		/// <param name="i">The zero-based index into the list of nodes.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If index is greater than or equal to the number of nodes in the list, this returns <see langword="null" />.</returns>
		// Token: 0x17000350 RID: 848
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.XmlNodeList" /> class.</summary>
		// Token: 0x06001247 RID: 4679 RVA: 0x0006E518 File Offset: 0x0006C718
		void IDisposable.Dispose()
		{
			this.PrivateDisposeNodeList();
		}

		/// <summary>Disposes resources in the node list privately.</summary>
		// Token: 0x06001248 RID: 4680 RVA: 0x0000B528 File Offset: 0x00009728
		protected virtual void PrivateDisposeNodeList()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNodeList" /> class.</summary>
		// Token: 0x06001249 RID: 4681 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlNodeList()
		{
		}
	}
}
