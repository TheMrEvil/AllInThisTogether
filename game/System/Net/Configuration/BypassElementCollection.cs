using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x0200075F RID: 1887
	[ConfigurationCollection(typeof(BypassElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Configuration.BypassElementCollection" /> class.</summary>
		// Token: 0x06003B83 RID: 15235 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public BypassElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> at the specified location.</returns>
		// Token: 0x17000D75 RID: 3445
		[MonoTODO]
		public BypassElement this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="name">The key for an element in the collection.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.BypassElement" /> with the specified key, or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000D76 RID: 3446
		public BypassElement this[string name]
		{
			get
			{
				return (BypassElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x00003062 File Offset: 0x00001262
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to add to the collection.</param>
		// Token: 0x06003B89 RID: 15241 RVA: 0x000316DB File Offset: 0x0002F8DB
		public void Add(BypassElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003B8A RID: 15242 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000CC509 File Offset: 0x000CA709
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000CC510 File Offset: 0x000CA710
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is BypassElement))
			{
				throw new ArgumentException("element");
			}
			return ((BypassElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.BypassElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06003B8D RID: 15245 RVA: 0x000CC425 File Offset: 0x000CA625
		public int IndexOf(BypassElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.BypassElement" /> to remove.</param>
		// Token: 0x06003B8E RID: 15246 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(BypassElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003B8F RID: 15247 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003B90 RID: 15248 RVA: 0x000CC437 File Offset: 0x000CA637
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
