using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Xml.Linq
{
	/// <summary>Contains the LINQ to XML extension methods.</summary>
	// Token: 0x02000010 RID: 16
	public static class Extensions
	{
		/// <summary>Returns a collection of the attributes of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the attributes of every element in the source collection.</returns>
		// Token: 0x06000074 RID: 116 RVA: 0x00003EEF File Offset: 0x000020EF
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAttributes(source, null);
		}

		/// <summary>Returns a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains a filtered collection of the attributes of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x06000075 RID: 117 RVA: 0x00003F06 File Offset: 0x00002106
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XAttribute.EmptySequence;
			}
			return Extensions.GetAttributes(source, name);
		}

		/// <summary>Returns a collection of elements that contains the ancestors of every node in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection.</returns>
		// Token: 0x06000076 RID: 118 RVA: 0x00003F2C File Offset: 0x0000212C
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAncestors<T>(source, null, false);
		}

		/// <summary>Returns a filtered collection of elements that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the ancestors of every node in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x06000077 RID: 119 RVA: 0x00003F44 File Offset: 0x00002144
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source, XName name) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetAncestors<T>(source, name, false);
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection.</returns>
		// Token: 0x06000078 RID: 120 RVA: 0x00003F6B File Offset: 0x0000216B
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAncestors<XElement>(source, null, true);
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the ancestors of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x06000079 RID: 121 RVA: 0x00003F83 File Offset: 0x00002183
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetAncestors<XElement>(source, name, true);
		}

		/// <summary>Returns a collection of the child nodes of every document and element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the child nodes of every document and element in the source collection.</returns>
		// Token: 0x0600007A RID: 122 RVA: 0x00003FAA File Offset: 0x000021AA
		public static IEnumerable<XNode> Nodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.NodesIterator<T>(source);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003FC0 File Offset: 0x000021C0
		private static IEnumerable<XNode> NodesIterator<T>(IEnumerable<T> source) where T : XContainer
		{
			foreach (!0 ! in source)
			{
				XContainer root = !;
				if (root != null)
				{
					XNode i = root.LastNode;
					if (i != null)
					{
						do
						{
							i = i.next;
							yield return i;
						}
						while (i.parent == root && i != root.content);
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Returns a collection of the descendant nodes of every document and element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the descendant nodes of every document and element in the source collection.</returns>
		// Token: 0x0600007C RID: 124 RVA: 0x00003FD0 File Offset: 0x000021D0
		public static IEnumerable<XNode> DescendantNodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendantNodes<T>(source, false);
		}

		/// <summary>Returns a collection of elements that contains the descendant elements of every element and document in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection.</returns>
		// Token: 0x0600007D RID: 125 RVA: 0x00003FE7 File Offset: 0x000021E7
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendants<T>(source, null, false);
		}

		/// <summary>Returns a filtered collection of elements that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XContainer" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the descendant elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x0600007E RID: 126 RVA: 0x00003FFF File Offset: 0x000021FF
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetDescendants<T>(source, name, false);
		}

		/// <summary>Returns a collection of nodes that contains every element in the source collection, and the descendant nodes of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains every element in the source collection, and the descendant nodes of every element in the source collection.</returns>
		// Token: 0x0600007F RID: 127 RVA: 0x00004026 File Offset: 0x00002226
		public static IEnumerable<XNode> DescendantNodesAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendantNodes<XElement>(source, true);
		}

		/// <summary>Returns a collection of elements that contains every element in the source collection, and the descendent elements of every element in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendent elements of every element in the source collection.</returns>
		// Token: 0x06000080 RID: 128 RVA: 0x0000403D File Offset: 0x0000223D
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendants<XElement>(source, null, true);
		}

		/// <summary>Returns a filtered collection of elements that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains every element in the source collection, and the descendents of every element in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x06000081 RID: 129 RVA: 0x00004055 File Offset: 0x00002255
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetDescendants<XElement>(source, name, true);
		}

		/// <summary>Returns a collection of the child elements of every element and document in the source collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element or document in the source collection.</returns>
		// Token: 0x06000082 RID: 130 RVA: 0x0000407C File Offset: 0x0000227C
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetElements<T>(source, null);
		}

		/// <summary>Returns a filtered collection of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> that contains the source collection.</param>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XContainer" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the child elements of every element and document in the source collection. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x06000083 RID: 131 RVA: 0x00004093 File Offset: 0x00002293
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetElements<T>(source, name);
		}

		/// <summary>Returns a collection of nodes that contains all nodes in the source collection, sorted in document order.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains all nodes in the source collection, sorted in document order.</returns>
		// Token: 0x06000084 RID: 132 RVA: 0x000040B9 File Offset: 0x000022B9
		public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.DocumentOrderIterator<T>(source);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000040CF File Offset: 0x000022CF
		private static IEnumerable<T> DocumentOrderIterator<T>(IEnumerable<T> source) where T : XNode
		{
			int count;
			T[] items = EnumerableHelpers.ToArray<T>(source, out count);
			if (count > 0)
			{
				XNode[] array = items;
				Array.Sort<XNode>(array, 0, count, XNode.DocumentOrderComparer);
				int num;
				for (int i = 0; i != count; i = num)
				{
					yield return items[i];
					num = i + 1;
				}
			}
			yield break;
		}

		/// <summary>Removes every attribute in the source collection from its parent element.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> that contains the source collection.</param>
		// Token: 0x06000086 RID: 134 RVA: 0x000040E0 File Offset: 0x000022E0
		public static void Remove(this IEnumerable<XAttribute> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			XAttribute[] array = EnumerableHelpers.ToArray<XAttribute>(source, out num);
			for (int i = 0; i < num; i++)
			{
				XAttribute xattribute = array[i];
				if (xattribute != null)
				{
					xattribute.Remove();
				}
			}
		}

		/// <summary>Removes every node in the source collection from its parent node.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> that contains the source collection.</param>
		/// <typeparam name="T">The type of the objects in <paramref name="source" />, constrained to <see cref="T:System.Xml.Linq.XNode" />.</typeparam>
		// Token: 0x06000087 RID: 135 RVA: 0x00004120 File Offset: 0x00002320
		public static void Remove<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(source, out num);
			for (int i = 0; i < num; i++)
			{
				T t = array[i];
				if (t != null)
				{
					t.Remove();
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000416B File Offset: 0x0000236B
		private static IEnumerable<XAttribute> GetAttributes(IEnumerable<XElement> source, XName name)
		{
			foreach (XElement e in source)
			{
				if (e != null)
				{
					XAttribute a = e.lastAttr;
					if (a != null)
					{
						do
						{
							a = a.next;
							if (name == null || a.name == name)
							{
								yield return a;
							}
						}
						while (a.parent == e && a != e.lastAttr);
					}
					a = null;
				}
				e = null;
			}
			IEnumerator<XElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004182 File Offset: 0x00002382
		private static IEnumerable<XElement> GetAncestors<T>(IEnumerable<T> source, XName name, bool self) where T : XNode
		{
			foreach (!0 ! in source)
			{
				XNode xnode = !;
				if (xnode != null)
				{
					XElement e;
					for (e = ((self ? xnode : xnode.parent) as XElement); e != null; e = (e.parent as XElement))
					{
						if (name == null || e.name == name)
						{
							yield return e;
						}
					}
					e = null;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000041A0 File Offset: 0x000023A0
		private static IEnumerable<XNode> GetDescendantNodes<T>(IEnumerable<T> source, bool self) where T : XContainer
		{
			foreach (!0 ! in source)
			{
				XContainer root = !;
				if (root != null)
				{
					if (self)
					{
						yield return root;
					}
					XNode i = root;
					for (;;)
					{
						XContainer xcontainer = i as XContainer;
						XNode firstNode;
						if (xcontainer != null && (firstNode = xcontainer.FirstNode) != null)
						{
							i = firstNode;
						}
						else
						{
							while (i != null && i != root && i == i.parent.content)
							{
								i = i.parent;
							}
							if (i == null || i == root)
							{
								break;
							}
							i = i.next;
						}
						yield return i;
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000041B7 File Offset: 0x000023B7
		private static IEnumerable<XElement> GetDescendants<T>(IEnumerable<T> source, XName name, bool self) where T : XContainer
		{
			foreach (!0 ! in source)
			{
				XContainer root = !;
				if (root != null)
				{
					if (self)
					{
						XElement xelement = (XElement)root;
						if (name == null || xelement.name == name)
						{
							yield return xelement;
						}
					}
					XNode i = root;
					XContainer xcontainer = root;
					for (;;)
					{
						if (xcontainer != null && xcontainer.content is XNode)
						{
							i = ((XNode)xcontainer.content).next;
						}
						else
						{
							while (i != null && i != root && i == i.parent.content)
							{
								i = i.parent;
							}
							if (i == null || i == root)
							{
								break;
							}
							i = i.next;
						}
						XElement e = i as XElement;
						if (e != null && (name == null || e.name == name))
						{
							yield return e;
						}
						xcontainer = e;
						e = null;
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000041D5 File Offset: 0x000023D5
		private static IEnumerable<XElement> GetElements<T>(IEnumerable<T> source, XName name) where T : XContainer
		{
			foreach (!0 ! in source)
			{
				XContainer root = !;
				if (root != null)
				{
					XNode i = root.content as XNode;
					if (i != null)
					{
						do
						{
							i = i.next;
							XElement xelement = i as XElement;
							if (xelement != null && (name == null || xelement.name == name))
							{
								yield return xelement;
							}
						}
						while (i.parent == root && i != root.content);
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class <NodesIterator>d__7<T> : IEnumerable<XNode>, IEnumerable, IEnumerator<XNode>, IDisposable, IEnumerator where T : XContainer
		{
			// Token: 0x0600008D RID: 141 RVA: 0x000041EC File Offset: 0x000023EC
			[DebuggerHidden]
			public <NodesIterator>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00004208 File Offset: 0x00002408
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00004240 File Offset: 0x00002440
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
						goto IL_D8;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					if (i.parent != root || i == root.content)
					{
						goto IL_CA;
					}
					IL_74:
					i = i.next;
					this.<>2__current = i;
					this.<>1__state = 1;
					return true;
					IL_CA:
					i = null;
					IL_D1:
					root = null;
					IL_D8:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						root = enumerator.Current;
						if (root == null)
						{
							goto IL_D1;
						}
						i = root.LastNode;
						if (i != null)
						{
							goto IL_74;
						}
						goto IL_CA;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00004360 File Offset: 0x00002560
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000091 RID: 145 RVA: 0x0000437C File Offset: 0x0000257C
			XNode IEnumerator<XNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000092 RID: 146 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000093 RID: 147 RVA: 0x0000437C File Offset: 0x0000257C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00004384 File Offset: 0x00002584
			[DebuggerHidden]
			IEnumerator<XNode> IEnumerable<XNode>.GetEnumerator()
			{
				Extensions.<NodesIterator>d__7<T> <NodesIterator>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<NodesIterator>d__ = this;
				}
				else
				{
					<NodesIterator>d__ = new Extensions.<NodesIterator>d__7<T>(0);
				}
				<NodesIterator>d__.source = source;
				return <NodesIterator>d__;
			}

			// Token: 0x06000095 RID: 149 RVA: 0x000043C7 File Offset: 0x000025C7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode>.GetEnumerator();
			}

			// Token: 0x0400003F RID: 63
			private int <>1__state;

			// Token: 0x04000040 RID: 64
			private XNode <>2__current;

			// Token: 0x04000041 RID: 65
			private int <>l__initialThreadId;

			// Token: 0x04000042 RID: 66
			private IEnumerable<T> source;

			// Token: 0x04000043 RID: 67
			public IEnumerable<T> <>3__source;

			// Token: 0x04000044 RID: 68
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x04000045 RID: 69
			private XContainer <root>5__3;

			// Token: 0x04000046 RID: 70
			private XNode <n>5__4;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class <DocumentOrderIterator>d__17<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator where T : XNode
		{
			// Token: 0x06000096 RID: 150 RVA: 0x000043CF File Offset: 0x000025CF
			[DebuggerHidden]
			public <DocumentOrderIterator>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000097 RID: 151 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000098 RID: 152 RVA: 0x000043EC File Offset: 0x000025EC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i + 1;
					i = num2;
				}
				else
				{
					this.<>1__state = -1;
					items = EnumerableHelpers.ToArray<T>(source, out count);
					if (count <= 0)
					{
						return false;
					}
					XNode[] array = items;
					Array.Sort<XNode>(array, 0, count, XNode.DocumentOrderComparer);
					i = 0;
				}
				if (i != count)
				{
					this.<>2__current = items[i];
					this.<>1__state = 1;
					return true;
				}
				return false;
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000099 RID: 153 RVA: 0x00004498 File Offset: 0x00002698
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600009A RID: 154 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600009B RID: 155 RVA: 0x000044A0 File Offset: 0x000026A0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600009C RID: 156 RVA: 0x000044B0 File Offset: 0x000026B0
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				Extensions.<DocumentOrderIterator>d__17<T> <DocumentOrderIterator>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<DocumentOrderIterator>d__ = this;
				}
				else
				{
					<DocumentOrderIterator>d__ = new Extensions.<DocumentOrderIterator>d__17<T>(0);
				}
				<DocumentOrderIterator>d__.source = source;
				return <DocumentOrderIterator>d__;
			}

			// Token: 0x0600009D RID: 157 RVA: 0x000044F3 File Offset: 0x000026F3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000047 RID: 71
			private int <>1__state;

			// Token: 0x04000048 RID: 72
			private T <>2__current;

			// Token: 0x04000049 RID: 73
			private int <>l__initialThreadId;

			// Token: 0x0400004A RID: 74
			private IEnumerable<T> source;

			// Token: 0x0400004B RID: 75
			public IEnumerable<T> <>3__source;

			// Token: 0x0400004C RID: 76
			private int <count>5__2;

			// Token: 0x0400004D RID: 77
			private T[] <items>5__3;

			// Token: 0x0400004E RID: 78
			private int <i>5__4;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		private sealed class <GetAttributes>d__20 : IEnumerable<XAttribute>, IEnumerable, IEnumerator<XAttribute>, IDisposable, IEnumerator
		{
			// Token: 0x0600009E RID: 158 RVA: 0x000044FB File Offset: 0x000026FB
			[DebuggerHidden]
			public <GetAttributes>d__20(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600009F RID: 159 RVA: 0x00004518 File Offset: 0x00002718
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x00004550 File Offset: 0x00002750
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
						goto IL_FC;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					goto IL_C8;
					IL_72:
					a = a.next;
					if (name == null || a.name == name)
					{
						this.<>2__current = a;
						this.<>1__state = 1;
						return true;
					}
					IL_C8:
					if (a.parent == e && a != e.lastAttr)
					{
						goto IL_72;
					}
					IL_EE:
					a = null;
					IL_F5:
					e = null;
					IL_FC:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						e = enumerator.Current;
						if (e == null)
						{
							goto IL_F5;
						}
						a = e.lastAttr;
						if (a != null)
						{
							goto IL_72;
						}
						goto IL_EE;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060000A1 RID: 161 RVA: 0x000046A0 File Offset: 0x000028A0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x000046BC File Offset: 0x000028BC
			XAttribute IEnumerator<XAttribute>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x000046BC File Offset: 0x000028BC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x000046C4 File Offset: 0x000028C4
			[DebuggerHidden]
			IEnumerator<XAttribute> IEnumerable<XAttribute>.GetEnumerator()
			{
				Extensions.<GetAttributes>d__20 <GetAttributes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAttributes>d__ = this;
				}
				else
				{
					<GetAttributes>d__ = new Extensions.<GetAttributes>d__20(0);
				}
				<GetAttributes>d__.source = source;
				<GetAttributes>d__.name = name;
				return <GetAttributes>d__;
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00004713 File Offset: 0x00002913
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XAttribute>.GetEnumerator();
			}

			// Token: 0x0400004F RID: 79
			private int <>1__state;

			// Token: 0x04000050 RID: 80
			private XAttribute <>2__current;

			// Token: 0x04000051 RID: 81
			private int <>l__initialThreadId;

			// Token: 0x04000052 RID: 82
			private IEnumerable<XElement> source;

			// Token: 0x04000053 RID: 83
			public IEnumerable<XElement> <>3__source;

			// Token: 0x04000054 RID: 84
			private XName name;

			// Token: 0x04000055 RID: 85
			public XName <>3__name;

			// Token: 0x04000056 RID: 86
			private IEnumerator<XElement> <>7__wrap1;

			// Token: 0x04000057 RID: 87
			private XElement <e>5__3;

			// Token: 0x04000058 RID: 88
			private XAttribute <a>5__4;
		}

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		private sealed class <GetAncestors>d__21<T> : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator where T : XNode
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x0000471B File Offset: 0x0000291B
			[DebuggerHidden]
			public <GetAncestors>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x00004738 File Offset: 0x00002938
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x00004770 File Offset: 0x00002970
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						goto IL_B7;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					IL_DC:
					while (enumerator.MoveNext())
					{
						XNode xnode = enumerator.Current;
						if (xnode != null)
						{
							e = ((self ? xnode : xnode.parent) as XElement);
							goto IL_CD;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					return false;
					IL_B7:
					e = (e.parent as XElement);
					IL_CD:
					if (e == null)
					{
						e = null;
						goto IL_DC;
					}
					if (!(name == null) && !(e.name == name))
					{
						goto IL_B7;
					}
					this.<>2__current = e;
					this.<>1__state = 1;
					result = true;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060000AA RID: 170 RVA: 0x00004894 File Offset: 0x00002A94
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000AB RID: 171 RVA: 0x000048B0 File Offset: 0x00002AB0
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000AC RID: 172 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000AD RID: 173 RVA: 0x000048B0 File Offset: 0x00002AB0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000048B8 File Offset: 0x00002AB8
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				Extensions.<GetAncestors>d__21<T> <GetAncestors>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAncestors>d__ = this;
				}
				else
				{
					<GetAncestors>d__ = new Extensions.<GetAncestors>d__21<T>(0);
				}
				<GetAncestors>d__.source = source;
				<GetAncestors>d__.name = name;
				<GetAncestors>d__.self = self;
				return <GetAncestors>d__;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00004913 File Offset: 0x00002B13
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x04000059 RID: 89
			private int <>1__state;

			// Token: 0x0400005A RID: 90
			private XElement <>2__current;

			// Token: 0x0400005B RID: 91
			private int <>l__initialThreadId;

			// Token: 0x0400005C RID: 92
			private IEnumerable<T> source;

			// Token: 0x0400005D RID: 93
			public IEnumerable<T> <>3__source;

			// Token: 0x0400005E RID: 94
			private bool self;

			// Token: 0x0400005F RID: 95
			public bool <>3__self;

			// Token: 0x04000060 RID: 96
			private XName name;

			// Token: 0x04000061 RID: 97
			public XName <>3__name;

			// Token: 0x04000062 RID: 98
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x04000063 RID: 99
			private XElement <e>5__3;
		}

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		private sealed class <GetDescendantNodes>d__22<T> : IEnumerable<XNode>, IEnumerable, IEnumerator<XNode>, IDisposable, IEnumerator where T : XContainer
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x0000491B File Offset: 0x00002B1B
			[DebuggerHidden]
			public <GetDescendantNodes>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00004938 File Offset: 0x00002B38
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x00004974 File Offset: 0x00002B74
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
						goto IL_156;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -3;
						goto IL_9C;
					default:
						return false;
					}
					IL_90:
					i = root;
					IL_9C:
					XContainer xcontainer = i as XContainer;
					XNode firstNode;
					if (xcontainer != null && (firstNode = xcontainer.FirstNode) != null)
					{
						i = firstNode;
					}
					else
					{
						while (i != null && i != root && i == i.parent.content)
						{
							i = i.parent;
						}
						if (i == null || i == root)
						{
							i = null;
							goto IL_14F;
						}
						i = i.next;
					}
					this.<>2__current = i;
					this.<>1__state = 2;
					return true;
					IL_14F:
					root = null;
					IL_156:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						root = enumerator.Current;
						if (root == null)
						{
							goto IL_14F;
						}
						if (!self)
						{
							goto IL_90;
						}
						this.<>2__current = root;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00004B1C File Offset: 0x00002D1C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004B38 File Offset: 0x00002D38
			XNode IEnumerator<XNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004B38 File Offset: 0x00002D38
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00004B40 File Offset: 0x00002D40
			[DebuggerHidden]
			IEnumerator<XNode> IEnumerable<XNode>.GetEnumerator()
			{
				Extensions.<GetDescendantNodes>d__22<T> <GetDescendantNodes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDescendantNodes>d__ = this;
				}
				else
				{
					<GetDescendantNodes>d__ = new Extensions.<GetDescendantNodes>d__22<T>(0);
				}
				<GetDescendantNodes>d__.source = source;
				<GetDescendantNodes>d__.self = self;
				return <GetDescendantNodes>d__;
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00004B8F File Offset: 0x00002D8F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode>.GetEnumerator();
			}

			// Token: 0x04000064 RID: 100
			private int <>1__state;

			// Token: 0x04000065 RID: 101
			private XNode <>2__current;

			// Token: 0x04000066 RID: 102
			private int <>l__initialThreadId;

			// Token: 0x04000067 RID: 103
			private IEnumerable<T> source;

			// Token: 0x04000068 RID: 104
			public IEnumerable<T> <>3__source;

			// Token: 0x04000069 RID: 105
			private bool self;

			// Token: 0x0400006A RID: 106
			public bool <>3__self;

			// Token: 0x0400006B RID: 107
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x0400006C RID: 108
			private XContainer <root>5__3;

			// Token: 0x0400006D RID: 109
			private XNode <n>5__4;
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		private sealed class <GetDescendants>d__23<T> : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator where T : XContainer
		{
			// Token: 0x060000B9 RID: 185 RVA: 0x00004B97 File Offset: 0x00002D97
			[DebuggerHidden]
			public <GetDescendants>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00004BB4 File Offset: 0x00002DB4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00004BF0 File Offset: 0x00002DF0
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
						goto IL_1DE;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -3;
						goto IL_1BD;
					default:
						return false;
					}
					IL_B8:
					i = root;
					XContainer xcontainer = root;
					IL_CB:
					if (xcontainer != null && xcontainer.content is XNode)
					{
						i = ((XNode)xcontainer.content).next;
					}
					else
					{
						while (i != null && i != root && i == i.parent.content)
						{
							i = i.parent;
						}
						if (i == null || i == root)
						{
							i = null;
							goto IL_1D7;
						}
						i = i.next;
					}
					e = (i as XElement);
					if (e != null && (name == null || e.name == name))
					{
						this.<>2__current = e;
						this.<>1__state = 2;
						return true;
					}
					IL_1BD:
					xcontainer = e;
					e = null;
					goto IL_CB;
					IL_1D7:
					root = null;
					IL_1DE:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						root = enumerator.Current;
						if (root == null)
						{
							goto IL_1D7;
						}
						if (!self)
						{
							goto IL_B8;
						}
						XElement xelement = (XElement)root;
						if (!(name == null) && !(xelement.name == name))
						{
							goto IL_B8;
						}
						this.<>2__current = xelement;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00004E20 File Offset: 0x00003020
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00004E3C File Offset: 0x0000303C
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000BE RID: 190 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00004E3C File Offset: 0x0000303C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00004E44 File Offset: 0x00003044
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				Extensions.<GetDescendants>d__23<T> <GetDescendants>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDescendants>d__ = this;
				}
				else
				{
					<GetDescendants>d__ = new Extensions.<GetDescendants>d__23<T>(0);
				}
				<GetDescendants>d__.source = source;
				<GetDescendants>d__.name = name;
				<GetDescendants>d__.self = self;
				return <GetDescendants>d__;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00004E9F File Offset: 0x0000309F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x0400006E RID: 110
			private int <>1__state;

			// Token: 0x0400006F RID: 111
			private XElement <>2__current;

			// Token: 0x04000070 RID: 112
			private int <>l__initialThreadId;

			// Token: 0x04000071 RID: 113
			private IEnumerable<T> source;

			// Token: 0x04000072 RID: 114
			public IEnumerable<T> <>3__source;

			// Token: 0x04000073 RID: 115
			private bool self;

			// Token: 0x04000074 RID: 116
			public bool <>3__self;

			// Token: 0x04000075 RID: 117
			private XName name;

			// Token: 0x04000076 RID: 118
			public XName <>3__name;

			// Token: 0x04000077 RID: 119
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x04000078 RID: 120
			private XContainer <root>5__3;

			// Token: 0x04000079 RID: 121
			private XNode <n>5__4;

			// Token: 0x0400007A RID: 122
			private XElement <e>5__5;
		}

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		private sealed class <GetElements>d__24<T> : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator where T : XContainer
		{
			// Token: 0x060000C2 RID: 194 RVA: 0x00004EA7 File Offset: 0x000030A7
			[DebuggerHidden]
			public <GetElements>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004EC4 File Offset: 0x000030C4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00004EFC File Offset: 0x000030FC
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
						goto IL_111;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					goto IL_DA;
					IL_7F:
					i = i.next;
					XElement xelement = i as XElement;
					if (xelement != null && (name == null || xelement.name == name))
					{
						this.<>2__current = xelement;
						this.<>1__state = 1;
						return true;
					}
					IL_DA:
					if (i.parent == root && i != root.content)
					{
						goto IL_7F;
					}
					IL_103:
					i = null;
					IL_10A:
					root = null;
					IL_111:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						root = enumerator.Current;
						if (root == null)
						{
							goto IL_10A;
						}
						i = (root.content as XNode);
						if (i != null)
						{
							goto IL_7F;
						}
						goto IL_103;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00005060 File Offset: 0x00003260
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000507C File Offset: 0x0000327C
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000507C File Offset: 0x0000327C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00005084 File Offset: 0x00003284
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				Extensions.<GetElements>d__24<T> <GetElements>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetElements>d__ = this;
				}
				else
				{
					<GetElements>d__ = new Extensions.<GetElements>d__24<T>(0);
				}
				<GetElements>d__.source = source;
				<GetElements>d__.name = name;
				return <GetElements>d__;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x000050D3 File Offset: 0x000032D3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x0400007B RID: 123
			private int <>1__state;

			// Token: 0x0400007C RID: 124
			private XElement <>2__current;

			// Token: 0x0400007D RID: 125
			private int <>l__initialThreadId;

			// Token: 0x0400007E RID: 126
			private IEnumerable<T> source;

			// Token: 0x0400007F RID: 127
			public IEnumerable<T> <>3__source;

			// Token: 0x04000080 RID: 128
			private XName name;

			// Token: 0x04000081 RID: 129
			public XName <>3__name;

			// Token: 0x04000082 RID: 130
			private IEnumerator<T> <>7__wrap1;

			// Token: 0x04000083 RID: 131
			private XContainer <root>5__3;

			// Token: 0x04000084 RID: 132
			private XNode <n>5__4;
		}
	}
}
