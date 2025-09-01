using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access registry variables. This class cannot be inherited.</summary>
	// Token: 0x02000453 RID: 1107
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002CE7 RID: 11495 RVA: 0x000A109D File Offset: 0x0009F29D
		public RegistryPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002CE8 RID: 11496 RVA: 0x000A10D3 File Offset: 0x0009F2D3
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this._state = PermissionState.None;
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.AddPathList(access, pathList);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables and the specified access rights to registry control information.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002CE9 RID: 11497 RVA: 0x000A110C File Offset: 0x0009F30C
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			if (!Enum.IsDefined(typeof(AccessControlActions), control))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), control), "AccessControlActions");
			}
			this._state = PermissionState.None;
			this.AddPathList(access, control, pathList);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002CEA RID: 11498 RVA: 0x000A1168 File Offset: 0x0009F368
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.AddWithUnionKey(this.readList, pathList);
				return;
			case RegistryPermissionAccess.Write:
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			case RegistryPermissionAccess.Create:
				this.AddWithUnionKey(this.createList, pathList);
				return;
			case RegistryPermissionAccess.AllAccess:
				this.AddWithUnionKey(this.createList, pathList);
				this.AddWithUnionKey(this.readList, pathList);
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission, specifying registry permission access and access control actions.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">One of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values. </param>
		/// <param name="pathList">A list of registry variables (separated by semicolons).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002CEB RID: 11499 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets paths for all registry variables with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values that represents a single type of registry variable access.</param>
		/// <returns>A list of the registry variables (semicolon-separated) with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		/// -or-  
		/// <paramref name="access" /> is <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />, which represents more than one type of registry variable access, or <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" />, which does not represent any type of registry variable access.</exception>
		// Token: 0x06002CEC RID: 11500 RVA: 0x000A1208 File Offset: 0x0009F408
		public string GetPathList(RegistryPermissionAccess access)
		{
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
			case RegistryPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(access, true);
				goto IL_61;
			case RegistryPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case RegistryPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			case RegistryPermissionAccess.Create:
				return this.GetPathList(this.createList);
			}
			this.ThrowInvalidFlag(access, false);
			IL_61:
			return null;
		}

		/// <summary>Sets new access for the specified registry variable names to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002CED RID: 11501 RVA: 0x000A1278 File Offset: 0x0009F478
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.readList.Clear();
				foreach (string value in pathList.Split(';', StringSplitOptions.None))
				{
					this.readList.Add(value);
				}
				return;
			case RegistryPermissionAccess.Write:
				this.writeList.Clear();
				foreach (string value2 in pathList.Split(';', StringSplitOptions.None))
				{
					this.writeList.Add(value2);
				}
				return;
			case RegistryPermissionAccess.Create:
				this.createList.Clear();
				foreach (string value3 in pathList.Split(';', StringSplitOptions.None))
				{
					this.createList.Add(value3);
				}
				return;
			case RegistryPermissionAccess.AllAccess:
				this.createList.Clear();
				this.readList.Clear();
				this.writeList.Clear();
				foreach (string value4 in pathList.Split(';', StringSplitOptions.None))
				{
					this.createList.Add(value4);
					this.readList.Add(value4);
					this.writeList.Add(value4);
				}
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002CEE RID: 11502 RVA: 0x000A13D0 File Offset: 0x0009F5D0
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(this._state);
			string pathList = this.GetPathList(RegistryPermissionAccess.Create);
			if (pathList != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, pathList);
			}
			pathList = this.GetPathList(RegistryPermissionAccess.Read);
			if (pathList != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, pathList);
			}
			pathList = this.GetPathList(RegistryPermissionAccess.Write);
			if (pathList != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, pathList);
			}
			return registryPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="elem" /> parameter's version number is not valid.</exception>
		// Token: 0x06002CEF RID: 11503 RVA: 0x000A1424 File Offset: 0x0009F624
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Create");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Create, text);
			}
			string text2 = esd.Attribute("Read");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Read, text2);
			}
			string text3 = esd.Attribute("Write");
			if (text3 != null && text3.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Write, text3);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CF0 RID: 11504 RVA: 0x000A14BC File Offset: 0x0009F6BC
		[SecuritySafeCritical]
		public override IPermission Intersect(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.Copy();
			}
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			RegistryPermission registryPermission2 = new RegistryPermission(PermissionState.None);
			this.IntersectKeys(this.createList, registryPermission.createList, registryPermission2.createList);
			this.IntersectKeys(this.readList, registryPermission.readList, registryPermission2.readList);
			this.IntersectKeys(this.writeList, registryPermission.writeList, registryPermission2.writeList);
			if (!registryPermission2.IsEmpty())
			{
				return registryPermission2;
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CF1 RID: 11505 RVA: 0x000A1550 File Offset: 0x0009F750
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return false;
			}
			if (registryPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.IsUnrestricted();
			}
			return registryPermission.IsUnrestricted() || (this.KeyIsSubsetOf(this.createList, registryPermission.createList) && this.KeyIsSubsetOf(this.readList, registryPermission.readList) && this.KeyIsSubsetOf(this.writeList, registryPermission.writeList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002CF2 RID: 11506 RVA: 0x000A15D5 File Offset: 0x0009F7D5
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000A15E0 File Offset: 0x0009F7E0
		[SecuritySafeCritical]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string pathList = this.GetPathList(RegistryPermissionAccess.Create);
				if (pathList != null)
				{
					securityElement.AddAttribute("Create", pathList);
				}
				pathList = this.GetPathList(RegistryPermissionAccess.Read);
				if (pathList != null)
				{
					securityElement.AddAttribute("Read", pathList);
				}
				pathList = this.GetPathList(RegistryPermissionAccess.Write);
				if (pathList != null)
				{
					securityElement.AddAttribute("Write", pathList);
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CF4 RID: 11508 RVA: 0x000A1658 File Offset: 0x0009F858
		[SecuritySafeCritical]
		public override IPermission Union(IPermission other)
		{
			RegistryPermission registryPermission = this.Cast(other);
			if (registryPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && registryPermission.IsEmpty())
			{
				return null;
			}
			RegistryPermission registryPermission2 = (RegistryPermission)this.Copy();
			string pathList = registryPermission.GetPathList(RegistryPermissionAccess.Create);
			if (pathList != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Create, pathList);
			}
			pathList = registryPermission.GetPathList(RegistryPermissionAccess.Read);
			if (pathList != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Read, pathList);
			}
			pathList = registryPermission.GetPathList(RegistryPermissionAccess.Write);
			if (pathList != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Write, pathList);
			}
			return registryPermission2;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x0003CDA0 File Offset: 0x0003AFA0
		int IBuiltInPermission.GetTokenIndex()
		{
			return 5;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000A16E6 File Offset: 0x0009F8E6
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.createList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000A171A File Offset: 0x0009F91A
		private RegistryPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(RegistryPermission));
			}
			return registryPermission;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000A173C File Offset: 0x0009F93C
		internal void ThrowInvalidFlag(RegistryPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000A177C File Offset: 0x0009F97C
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string value = (string)obj;
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			string text = stringBuilder.ToString();
			int length = text.Length;
			if (length > 0)
			{
				return text.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000A1828 File Offset: 0x0009FA28
		internal bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string value = (string)obj2;
					if (text.StartsWith(value))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000A18D4 File Offset: 0x0009FAD4
		internal void AddWithUnionKey(IList list, string pathList)
		{
			foreach (string text in pathList.Split(';', StringSplitOptions.None))
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					for (int j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (text2.StartsWith(text))
						{
							list[j] = text;
						}
						else if (!text.StartsWith(text2))
						{
							list.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000A1958 File Offset: 0x0009FB58
		internal void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (text2.StartsWith(text))
						{
							result.Add(text2);
						}
					}
					else if (text.StartsWith(text2))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x0400207F RID: 8319
		private const int version = 1;

		// Token: 0x04002080 RID: 8320
		private PermissionState _state;

		// Token: 0x04002081 RID: 8321
		private ArrayList createList;

		// Token: 0x04002082 RID: 8322
		private ArrayList readList;

		// Token: 0x04002083 RID: 8323
		private ArrayList writeList;
	}
}
