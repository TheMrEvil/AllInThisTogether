using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Defines a permission set that has a name and description associated with it. This class cannot be inherited.</summary>
	// Token: 0x020003E1 RID: 993
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x060028B6 RID: 10422 RVA: 0x000933E6 File Offset: 0x000915E6
		internal NamedPermissionSet()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.NamedPermissionSet" /> class with the specified name from a permission set.</summary>
		/// <param name="name">The name for the named permission set.</param>
		/// <param name="permSet">The permission set from which to take the value of the new named permission set.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" /> or is an empty string ("").</exception>
		// Token: 0x060028B7 RID: 10423 RVA: 0x000933EE File Offset: 0x000915EE
		public NamedPermissionSet(string name, PermissionSet permSet) : base(permSet)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.NamedPermissionSet" /> class with the specified name in either an unrestricted or a fully restricted state.</summary>
		/// <param name="name">The name for the new named permission set.</param>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" /> or is an empty string ("").</exception>
		// Token: 0x060028B8 RID: 10424 RVA: 0x000933FE File Offset: 0x000915FE
		public NamedPermissionSet(string name, PermissionState state) : base(state)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.NamedPermissionSet" /> class from another named permission set.</summary>
		/// <param name="permSet">The named permission set from which to create the new instance.</param>
		// Token: 0x060028B9 RID: 10425 RVA: 0x0009340E File Offset: 0x0009160E
		public NamedPermissionSet(NamedPermissionSet permSet) : base(permSet)
		{
			this.name = permSet.name;
			this.description = permSet.description;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Security.NamedPermissionSet" /> class with the specified name.</summary>
		/// <param name="name">The name for the new named permission set.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" /> or is an empty string ("").</exception>
		// Token: 0x060028BA RID: 10426 RVA: 0x0009342F File Offset: 0x0009162F
		public NamedPermissionSet(string name) : this(name, PermissionState.Unrestricted)
		{
		}

		/// <summary>Gets or sets the text description of the current named permission set.</summary>
		/// <returns>A text description of the named permission set.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x00093439 File Offset: 0x00091639
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x00093441 File Offset: 0x00091641
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the name of the current named permission set.</summary>
		/// <returns>The name of the named permission set.</returns>
		/// <exception cref="T:System.ArgumentException">The name is <see langword="null" /> or is an empty string ("").</exception>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x0009344A File Offset: 0x0009164A
		// (set) Token: 0x060028BE RID: 10430 RVA: 0x00093452 File Offset: 0x00091652
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new ArgumentException(Locale.GetText("invalid name"));
				}
				this.name = value;
			}
		}

		/// <summary>Creates a permission set copy from a named permission set.</summary>
		/// <returns>A permission set that is a copy of the permissions in the named permission set.</returns>
		// Token: 0x060028BF RID: 10431 RVA: 0x0009347B File Offset: 0x0009167B
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		/// <summary>Creates a copy of the named permission set with a different name but the same permissions.</summary>
		/// <param name="name">The name for the new named permission set.</param>
		/// <returns>A copy of the named permission set with the new name.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" /> or is an empty string ("").</exception>
		// Token: 0x060028C0 RID: 10432 RVA: 0x00093483 File Offset: 0x00091683
		public NamedPermissionSet Copy(string name)
		{
			return new NamedPermissionSet(this)
			{
				Name = name
			};
		}

		/// <summary>Reconstructs a named permission set with a specified state from an XML encoding.</summary>
		/// <param name="et">A security element containing the XML representation of the named permission set.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid representation of a named permission set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060028C1 RID: 10433 RVA: 0x00093492 File Offset: 0x00091692
		public override void FromXml(SecurityElement et)
		{
			base.FromXml(et);
			this.name = et.Attribute("Name");
			this.description = et.Attribute("Description");
			if (this.description == null)
			{
				this.description = string.Empty;
			}
		}

		/// <summary>Creates an XML element description of the named permission set.</summary>
		/// <returns>The XML representation of the named permission set.</returns>
		// Token: 0x060028C2 RID: 10434 RVA: 0x000934D0 File Offset: 0x000916D0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml();
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.description != null)
			{
				securityElement.AddAttribute("Description", this.description);
			}
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.NamedPermissionSet" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.NamedPermissionSet" /> object to compare with the current <see cref="T:System.Security.NamedPermissionSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.NamedPermissionSet" /> is equal to the current <see cref="T:System.Security.NamedPermissionSet" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028C3 RID: 10435 RVA: 0x00093518 File Offset: 0x00091718
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			NamedPermissionSet namedPermissionSet = obj as NamedPermissionSet;
			return namedPermissionSet != null && this.name == namedPermissionSet.Name && base.Equals(obj);
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.NamedPermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.NamedPermissionSet" /> object.</returns>
		// Token: 0x060028C4 RID: 10436 RVA: 0x00093554 File Offset: 0x00091754
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.name != null)
			{
				num ^= this.name.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001EBF RID: 7871
		private string name;

		// Token: 0x04001EC0 RID: 7872
		private string description;
	}
}
