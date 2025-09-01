using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for Web request module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200077F RID: 1919
	[ConfigurationCollection(typeof(WebRequestModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> class.</summary>
		// Token: 0x06003C7B RID: 15483 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public WebRequestModuleElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> at the specified location.</returns>
		// Token: 0x17000DDA RID: 3546
		[MonoTODO]
		public WebRequestModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> with the specified key or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000DDB RID: 3547
		[MonoTODO]
		public WebRequestModuleElement this[string name]
		{
			get
			{
				return (WebRequestModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to add to the collection.</param>
		// Token: 0x06003C80 RID: 15488 RVA: 0x000316DB File Offset: 0x0002F8DB
		public void Add(WebRequestModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003C81 RID: 15489 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x000CE3EE File Offset: 0x000CC5EE
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x000CE3F5 File Offset: 0x000CC5F5
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is WebRequestModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((WebRequestModuleElement)element).Prefix;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.WebRequestModuleElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06003C84 RID: 15492 RVA: 0x000CC425 File Offset: 0x000CA625
		public int IndexOf(WebRequestModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> to remove.</param>
		// Token: 0x06003C85 RID: 15493 RVA: 0x000CE415 File Offset: 0x000CC615
		public void Remove(WebRequestModuleElement element)
		{
			base.BaseRemove(element.Prefix);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003C86 RID: 15494 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003C87 RID: 15495 RVA: 0x000CC437 File Offset: 0x000CA637
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
