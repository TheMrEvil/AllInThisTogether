using System;
using System.Configuration;
using Unity;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> class is a collection of service principal names that represent a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x020002AD RID: 685
	[ConfigurationCollection(typeof(ServiceNameElement))]
	public sealed class ServiceNameElementCollection : ConfigurationElementCollection
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance at the specified index location.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then <see langword="null" /> is returned.</returns>
		// Token: 0x17000407 RID: 1031
		public ServiceNameElement this[int index]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(index);
			}
		}

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance based on a string that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then <see langword="null" /> is returned.</returns>
		// Token: 0x17000408 RID: 1032
		public ServiceNameElement this[string name]
		{
			get
			{
				return (ServiceNameElement)base.BaseGet(name);
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Add(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method adds a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to add to this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x06001551 RID: 5457 RVA: 0x0000829A File Offset: 0x0000649A
		public void Add(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Clear" /> method removes all configuration element objects from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		// Token: 0x06001552 RID: 5458 RVA: 0x0000829A File Offset: 0x0000649A
		public void Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00055CE6 File Offset: 0x00053EE6
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServiceNameElement();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00055CED File Offset: 0x00053EED
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return ((ServiceNameElement)element).Name;
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.IndexOf(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method retrieves the index of the specified configuration element in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to retrieve the index of in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The index of the specified <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</returns>
		// Token: 0x06001555 RID: 5461 RVA: 0x0000829A File Offset: 0x0000649A
		public int IndexOf(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the <see cref="T:System.String" /> specified.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /></param>
		// Token: 0x06001556 RID: 5462 RVA: 0x0000829A File Offset: 0x0000649A
		public void Remove(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is <see langword="null" />.</exception>
		// Token: 0x06001557 RID: 5463 RVA: 0x0000829A File Offset: 0x0000649A
		public void Remove(ServiceNameElement element)
		{
			throw new NotImplementedException();
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Remove(System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement)" /> method removes a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> based on the index specified.</summary>
		/// <param name="index">The index of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance to remove from this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		// Token: 0x06001558 RID: 5464 RVA: 0x0000829A File Offset: 0x0000649A
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> class.</summary>
		// Token: 0x06001559 RID: 5465 RVA: 0x000316D3 File Offset: 0x0002F8D3
		public ServiceNameElementCollection()
		{
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00013BCA File Offset: 0x00011DCA
		public void set_Item(int index, ServiceNameElement value)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection.Item(System.String)" /> property gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance based on a string that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance in this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</param>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance requested. If the requested instance is not found, then <see langword="null" /> is returned.</returns>
		// Token: 0x17000409 RID: 1033
		public string this[string name]
		{
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
