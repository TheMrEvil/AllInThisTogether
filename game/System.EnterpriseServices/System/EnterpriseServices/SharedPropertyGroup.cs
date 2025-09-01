using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.EnterpriseServices
{
	/// <summary>Represents a collection of shared properties. This class cannot be inherited.</summary>
	// Token: 0x0200004B RID: 75
	[ComVisible(false)]
	public sealed class SharedPropertyGroup
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000025E7 File Offset: 0x000007E7
		internal SharedPropertyGroup(ISharedPropertyGroup propertyGroup)
		{
			this.propertyGroup = propertyGroup;
		}

		/// <summary>Creates a property with the given name.</summary>
		/// <param name="name">The name of the new property.</param>
		/// <param name="fExists">Determines whether the property exists. Set to <see langword="true" /> on return if the property exists.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		// Token: 0x06000142 RID: 322 RVA: 0x000025F6 File Offset: 0x000007F6
		public SharedProperty CreateProperty(string name, out bool fExists)
		{
			return new SharedProperty(this.propertyGroup.CreateProperty(name, out fExists));
		}

		/// <summary>Creates a property at the given position.</summary>
		/// <param name="position">The index of the new property</param>
		/// <param name="fExists">Determines whether the property exists. Set to <see langword="true" /> on return if the property exists.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		// Token: 0x06000143 RID: 323 RVA: 0x0000260A File Offset: 0x0000080A
		public SharedProperty CreatePropertyByPosition(int position, out bool fExists)
		{
			return new SharedProperty(this.propertyGroup.CreatePropertyByPosition(position, out fExists));
		}

		/// <summary>Returns the property with the given name.</summary>
		/// <param name="name">The name of requested property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		// Token: 0x06000144 RID: 324 RVA: 0x0000261E File Offset: 0x0000081E
		public SharedProperty Property(string name)
		{
			return new SharedProperty(this.propertyGroup.Property(name));
		}

		/// <summary>Returns the property at the given position.</summary>
		/// <param name="position">The index of the property.</param>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		// Token: 0x06000145 RID: 325 RVA: 0x00002631 File Offset: 0x00000831
		public SharedProperty PropertyByPosition(int position)
		{
			return new SharedProperty(this.propertyGroup.PropertyByPosition(position));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000024CD File Offset: 0x000006CD
		internal SharedPropertyGroup()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000087 RID: 135
		private ISharedPropertyGroup propertyGroup;
	}
}
