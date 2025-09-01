using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Compares nodes to determine whether they are equal. This class cannot be inherited.</summary>
	// Token: 0x02000058 RID: 88
	public sealed class XNodeEqualityComparer : IEqualityComparer, IEqualityComparer<XNode>
	{
		/// <summary>Compares the values of two nodes.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>A <see cref="T:System.Boolean" /> indicating if the nodes are equal.</returns>
		// Token: 0x06000316 RID: 790 RVA: 0x0000E23D File Offset: 0x0000C43D
		public bool Equals(XNode x, XNode y)
		{
			return XNode.DeepEquals(x, y);
		}

		/// <summary>Returns a hash code based on an <see cref="T:System.Xml.Linq.XNode" />.</summary>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XNode" /> to hash.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains a value-based hash code for the node.</returns>
		// Token: 0x06000317 RID: 791 RVA: 0x0000E246 File Offset: 0x0000C446
		public int GetHashCode(XNode obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}

		/// <summary>Compares the values of two nodes.</summary>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the nodes are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x06000318 RID: 792 RVA: 0x0000E254 File Offset: 0x0000C454
		bool IEqualityComparer.Equals(object x, object y)
		{
			XNode xnode = x as XNode;
			if (xnode == null && x != null)
			{
				throw new ArgumentException(SR.Format("The argument must be derived from {0}.", typeof(XNode)), "x");
			}
			XNode xnode2 = y as XNode;
			if (xnode2 == null && y != null)
			{
				throw new ArgumentException(SR.Format("The argument must be derived from {0}.", typeof(XNode)), "y");
			}
			return this.Equals(xnode, xnode2);
		}

		/// <summary>Returns a hash code based on the value of a node.</summary>
		/// <param name="obj">The node to hash.</param>
		/// <returns>A <see cref="T:System.Int32" /> that contains a value-based hash code for the node.</returns>
		// Token: 0x06000319 RID: 793 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		int IEqualityComparer.GetHashCode(object obj)
		{
			XNode xnode = obj as XNode;
			if (xnode == null && obj != null)
			{
				throw new ArgumentException(SR.Format("The argument must be derived from {0}.", typeof(XNode)), "obj");
			}
			return this.GetHashCode(xnode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XNodeEqualityComparer" /> class.</summary>
		// Token: 0x0600031A RID: 794 RVA: 0x00003E36 File Offset: 0x00002036
		public XNodeEqualityComparer()
		{
		}
	}
}
