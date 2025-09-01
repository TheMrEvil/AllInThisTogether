using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for authentication module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200075C RID: 1884
	[ConfigurationCollection(typeof(AuthenticationModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AuthenticationModuleElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> class.</summary>
		// Token: 0x06003B6A RID: 15210 RVA: 0x000316D3 File Offset: 0x0002F8D3
		[MonoTODO]
		public AuthenticationModuleElementCollection()
		{
		}

		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <param name="index">The zero-based index of the element.</param>
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> at the specified location.</returns>
		// Token: 0x17000D6F RID: 3439
		[MonoTODO]
		public AuthenticationModuleElement this[int index]
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
		/// <returns>The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> with the specified key or <see langword="null" /> if there is no element with the specified key.</returns>
		// Token: 0x17000D70 RID: 3440
		[MonoTODO]
		public AuthenticationModuleElement this[string name]
		{
			get
			{
				return (AuthenticationModuleElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to add to the collection.</param>
		// Token: 0x06003B6F RID: 15215 RVA: 0x000316DB File Offset: 0x0002F8DB
		public void Add(AuthenticationModuleElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003B70 RID: 15216 RVA: 0x000316E4 File Offset: 0x0002F8E4
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000CC3FE File Offset: 0x000CA5FE
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthenticationModuleElement();
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000CC405 File Offset: 0x000CA605
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is AuthenticationModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((AuthenticationModuleElement)element).Type;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <param name="element">A <see cref="T:System.Net.Configuration.AuthenticationModuleElement" />.</param>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		// Token: 0x06003B73 RID: 15219 RVA: 0x000CC425 File Offset: 0x000CA625
		public int IndexOf(AuthenticationModuleElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> to remove.</param>
		// Token: 0x06003B74 RID: 15220 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(AuthenticationModuleElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x06003B75 RID: 15221 RVA: 0x000CC42E File Offset: 0x000CA62E
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003B76 RID: 15222 RVA: 0x000CC437 File Offset: 0x000CA637
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
