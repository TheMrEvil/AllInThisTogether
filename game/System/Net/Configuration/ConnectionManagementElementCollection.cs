using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x02000761 RID: 1889
	[ConfigurationCollection(typeof(ConnectionManagementElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> class.</summary>
		// Token: 0x06003B99 RID: 15257 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public ConnectionManagementElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> at the specified location.</returns>
		// Token: 0x17000D7B RID: 3451
		[MonoTODO]
		public ConnectionManagementElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> with the specified key or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000D7C RID: 3452
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to add to the collection.</param>
		// Token: 0x06003B9E RID: 15262 RVA: 0x000316DB File Offset: 0x0002F8DB
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003B9F RID: 15263 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000CC610 File Offset: 0x000CA810
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000CC617 File Offset: 0x000CA817
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is ConnectionManagementElement))
			{
				throw new ArgumentException("element");
			}
			return ((ConnectionManagementElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.ConnectionManagementElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000CC425 File Offset: 0x000CA625
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to remove.</param>
		// Token: 0x06003BA3 RID: 15267 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(ConnectionManagementElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003BA4 RID: 15268 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003BA5 RID: 15269 RVA: 0x000CC437 File Offset: 0x000CA637
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
