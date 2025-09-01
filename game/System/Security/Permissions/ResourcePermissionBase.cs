using System;
using System.Collections;

namespace System.Security.Permissions
{
	/// <summary>Allows control of code access security permissions.</summary>
	// Token: 0x02000296 RID: 662
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class.</summary>
		// Token: 0x060014DD RID: 5341 RVA: 0x00054A50 File Offset: 0x00052C50
		protected ResourcePermissionBase()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class with the specified level of access to resources at creation.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060014DE RID: 5342 RVA: 0x00054A63 File Offset: 0x00052C63
		protected ResourcePermissionBase(PermissionState state) : this()
		{
			PermissionHelper.CheckPermissionState(state, true);
			this._unrestricted = (state == PermissionState.Unrestricted);
		}

		/// <summary>Gets or sets an enumeration value that describes the types of access that you are giving the resource.</summary>
		/// <returns>An enumeration value that is derived from <see cref="T:System.Type" /> and describes the types of access that you are giving the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property value is not an enumeration value.</exception>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00054A7D File Offset: 0x00052C7D
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00054A85 File Offset: 0x00052C85
		protected Type PermissionAccessType
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PermissionAccessType");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException("!Enum", "PermissionAccessType");
				}
				this._type = value;
			}
		}

		/// <summary>Gets or sets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are trying to protect.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the array is 0.</exception>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00054ABA File Offset: 0x00052CBA
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x00054AC2 File Offset: 0x00052CC2
		protected string[] TagNames
		{
			get
			{
				return this._tags;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("TagNames");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Length==0", "TagNames");
				}
				this._tags = value;
			}
		}

		/// <summary>Adds a permission entry to the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.  
		///  -or-  
		///  The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is already included in the permission.</exception>
		// Token: 0x060014E3 RID: 5347 RVA: 0x00054AED File Offset: 0x00052CED
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			if (this.Exists(entry))
			{
				throw new InvalidOperationException(Locale.GetText("Entry already exists."));
			}
			this._list.Add(entry);
		}

		/// <summary>Clears the permission of the added permission entries.</summary>
		// Token: 0x060014E4 RID: 5348 RVA: 0x00054B1C File Offset: 0x00052D1C
		protected void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x060014E5 RID: 5349 RVA: 0x00054B2C File Offset: 0x00052D2C
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = ResourcePermissionBase.CreateFromType(base.GetType(), this._unrestricted);
			if (this._tags != null)
			{
				resourcePermissionBase._tags = (string[])this._tags.Clone();
			}
			resourcePermissionBase._type = this._type;
			resourcePermissionBase._list.AddRange(this._list);
			return resourcePermissionBase;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> parameter is not a valid permission element.  
		///  -or-  
		///  The version number of the <paramref name="securityElement" /> parameter is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060014E6 RID: 5350 RVA: 0x00054B88 File Offset: 0x00052D88
		[MonoTODO("incomplete - need more test")]
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._list.Clear();
			this._unrestricted = PermissionHelper.IsUnrestricted(securityElement);
			if (securityElement.Children == null || securityElement.Children.Count < 1)
			{
				return;
			}
			string[] array = new string[1];
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				array[0] = securityElement2.Attribute("name");
				ResourcePermissionBaseEntry entry = new ResourcePermissionBaseEntry((int)Enum.Parse(this.PermissionAccessType, securityElement2.Attribute("access")), array);
				this.AddPermissionAccess(entry);
			}
		}

		/// <summary>Returns an array of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects added to this permission.</summary>
		/// <returns>An array of <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects that were added to this permission.</returns>
		// Token: 0x060014E7 RID: 5351 RVA: 0x00054C68 File Offset: 0x00052E68
		protected ResourcePermissionBaseEntry[] GetPermissionEntries()
		{
			ResourcePermissionBaseEntry[] array = new ResourcePermissionBaseEntry[this._list.Count];
			this._list.CopyTo(array, 0);
			return array;
		}

		/// <summary>Creates and returns a permission object that is the intersection of the current permission object and a target permission object.</summary>
		/// <param name="target">A permission object of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The target permission object is not of the same type as the current permission object.</exception>
		// Token: 0x060014E8 RID: 5352 RVA: 0x00054C94 File Offset: 0x00052E94
		public override IPermission Intersect(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return null;
			}
			bool flag = this.IsUnrestricted();
			bool flag2 = resourcePermissionBase.IsUnrestricted();
			if (this.IsEmpty() && !flag2)
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty() && !flag)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag && flag2);
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
				if (flag2 || resourcePermissionBase.Exists(entry))
				{
					resourcePermissionBase2.AddPermissionAccess(entry);
				}
			}
			foreach (object obj2 in resourcePermissionBase._list)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj2;
				if ((flag || this.Exists(entry2)) && !resourcePermissionBase2.Exists(entry2))
				{
					resourcePermissionBase2.AddPermissionAccess(entry2);
				}
			}
			return resourcePermissionBase2;
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <param name="target">A permission object that is to be tested for the subset relationship.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission object is a subset of the specified permission object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014E9 RID: 5353 RVA: 0x00054DB0 File Offset: 0x00052FB0
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return true;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				return false;
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.IsUnrestricted();
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
				if (!resourcePermissionBase.Exists(entry))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014EA RID: 5354 RVA: 0x00054E40 File Offset: 0x00053040
		public bool IsUnrestricted()
		{
			return this._unrestricted;
		}

		/// <summary>Removes a permission entry from the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.  
		///  -or-  
		///  The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is not in the permission.</exception>
		// Token: 0x060014EB RID: 5355 RVA: 0x00054E48 File Offset: 0x00053048
		protected void RemovePermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			for (int i = 0; i < this._list.Count; i++)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)this._list[i];
				if (this.Equals(entry, entry2))
				{
					this._list.RemoveAt(i);
					return;
				}
			}
			throw new InvalidOperationException(Locale.GetText("Entry doesn't exists."));
		}

		/// <summary>Creates and returns an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060014EC RID: 5356 RVA: 0x00054EAC File Offset: 0x000530AC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(base.GetType(), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					SecurityElement securityElement2 = securityElement;
					string text = null;
					if (this.PermissionAccessType != null)
					{
						text = Enum.Format(this.PermissionAccessType, resourcePermissionBaseEntry.PermissionAccess, "g");
					}
					for (int i = 0; i < this._tags.Length; i++)
					{
						SecurityElement securityElement3 = new SecurityElement(this._tags[i]);
						securityElement3.AddAttribute("name", resourcePermissionBaseEntry.PermissionAccessPath[i]);
						if (text != null)
						{
							securityElement3.AddAttribute("access", text);
						}
						securityElement2.AddChild(securityElement3);
					}
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission object that combines the current permission object and the target permission object.</summary>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object.</param>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> permission object is not of the same type as the current permission object.</exception>
		// Token: 0x060014ED RID: 5357 RVA: 0x00054FBC File Offset: 0x000531BC
		public override IPermission Union(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && resourcePermissionBase.IsEmpty())
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return resourcePermissionBase.Copy();
			}
			bool flag = this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted();
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag);
			if (!flag)
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
					resourcePermissionBase2.AddPermissionAccess(entry);
				}
				foreach (object obj2 in resourcePermissionBase._list)
				{
					ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj2;
					if (!resourcePermissionBase2.Exists(entry2))
					{
						resourcePermissionBase2.AddPermissionAccess(entry2);
					}
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000550DC File Offset: 0x000532DC
		private bool IsEmpty()
		{
			return !this._unrestricted && this._list.Count == 0;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000550F6 File Offset: 0x000532F6
		private ResourcePermissionBase Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(ResourcePermissionBase));
			}
			return resourcePermissionBase;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00055116 File Offset: 0x00053316
		internal void CheckEntry(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath == null || entry.PermissionAccessPath.Length != this._tags.Length)
			{
				throw new InvalidOperationException(Locale.GetText("Entry doesn't match TagNames"));
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00055150 File Offset: 0x00053350
		internal bool Equals(ResourcePermissionBaseEntry entry1, ResourcePermissionBaseEntry entry2)
		{
			if (entry1.PermissionAccess != entry2.PermissionAccess)
			{
				return false;
			}
			if (entry1.PermissionAccessPath.Length != entry2.PermissionAccessPath.Length)
			{
				return false;
			}
			for (int i = 0; i < entry1.PermissionAccessPath.Length; i++)
			{
				if (entry1.PermissionAccessPath[i] != entry2.PermissionAccessPath[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000551B0 File Offset: 0x000533B0
		internal bool Exists(ResourcePermissionBaseEntry entry)
		{
			if (this._list.Count == 0)
			{
				return false;
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj;
				if (this.Equals(entry2, entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00055224 File Offset: 0x00053424
		internal static void ValidateMachineName(string name)
		{
			if (name == null || name.Length == 0 || name.IndexOfAny(ResourcePermissionBase.invalidChars) != -1)
			{
				string text = Locale.GetText("Invalid machine name '{0}'.");
				if (name == null)
				{
					name = "(null)";
				}
				throw new ArgumentException(string.Format(text, name), "MachineName");
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00055264 File Offset: 0x00053464
		internal static ResourcePermissionBase CreateFromType(Type type, bool unrestricted)
		{
			return (ResourcePermissionBase)Activator.CreateInstance(type, new object[]
			{
				unrestricted ? PermissionState.Unrestricted : PermissionState.None
			});
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00055293 File Offset: 0x00053493
		// Note: this type is marked as 'beforefieldinit'.
		static ResourcePermissionBase()
		{
		}

		// Token: 0x04000BB9 RID: 3001
		private const int version = 1;

		// Token: 0x04000BBA RID: 3002
		private ArrayList _list;

		// Token: 0x04000BBB RID: 3003
		private bool _unrestricted;

		// Token: 0x04000BBC RID: 3004
		private Type _type;

		// Token: 0x04000BBD RID: 3005
		private string[] _tags;

		/// <summary>Specifies the character to be used to represent the any wildcard character.</summary>
		// Token: 0x04000BBE RID: 3006
		public const string Any = "*";

		/// <summary>Specifies the character to be used to represent a local reference.</summary>
		// Token: 0x04000BBF RID: 3007
		public const string Local = ".";

		// Token: 0x04000BC0 RID: 3008
		private static char[] invalidChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' ',
			'\\',
			'Š'
		};
	}
}
