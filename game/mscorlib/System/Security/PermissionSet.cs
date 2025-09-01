using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using Unity;

namespace System.Security
{
	/// <summary>Represents a collection that can contain many different types of permissions.</summary>
	// Token: 0x020003E3 RID: 995
	[ComVisible(true)]
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]
	[Serializable]
	public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
	{
		// Token: 0x060028CB RID: 10443 RVA: 0x000936A4 File Offset: 0x000918A4
		internal PermissionSet()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission set's access to resources.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060028CC RID: 10444 RVA: 0x000936B7 File Offset: 0x000918B7
		public PermissionSet(PermissionState state) : this()
		{
			this.state = CodeAccessPermission.CheckPermissionState(state, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with initial values taken from the <paramref name="permSet" /> parameter.</summary>
		/// <param name="permSet">The set from which to take the value of the new <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> to create an empty <see cref="T:System.Security.PermissionSet" />.</param>
		// Token: 0x060028CD RID: 10445 RVA: 0x000936CC File Offset: 0x000918CC
		public PermissionSet(PermissionSet permSet) : this()
		{
			if (permSet != null)
			{
				this.state = permSet.state;
				foreach (object obj in permSet.list)
				{
					IPermission value = (IPermission)obj;
					this.list.Add(value);
				}
			}
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x00093740 File Offset: 0x00091940
		internal PermissionSet(string xml) : this()
		{
			this.state = PermissionState.None;
			if (xml != null)
			{
				SecurityElement et = SecurityElement.FromString(xml);
				this.FromXml(et);
			}
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0009376B File Offset: 0x0009196B
		internal PermissionSet(IPermission perm) : this()
		{
			if (perm != null)
			{
				this.list.Add(perm);
			}
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="perm">The permission to add.</param>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028D0 RID: 10448 RVA: 0x00093784 File Offset: 0x00091984
		public IPermission AddPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			if (this.state == PermissionState.Unrestricted)
			{
				return (IPermission)Activator.CreateInstance(perm.GetType(), PermissionSet.psUnrestricted);
			}
			IPermission permission = this.RemovePermission(perm.GetType());
			if (permission != null)
			{
				perm = perm.Union(permission);
			}
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.PermissionSet.Assert" /> can create security vulnerabilities.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.PermissionSet" /> instance asserted has not been granted to the asserting code.  
		///  -or-  
		///  There is already an active <see cref="M:System.Security.PermissionSet.Assert" /> for the current frame.</exception>
		// Token: 0x060028D1 RID: 10449 RVA: 0x000937E4 File Offset: 0x000919E4
		[SecuritySafeCritical]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecurityPermission(SecurityAction.Demand, Assertion = true)]
		public void Assert()
		{
			int num = this.Count;
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					if (!SecurityManager.IsGranted(permission))
					{
						return;
					}
				}
				else
				{
					num--;
				}
			}
			if (SecurityManager.SecurityEnabled && num > 0)
			{
				throw new NotSupportedException("Currently only declarative Assert are supported.");
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0009386C File Offset: 0x00091A6C
		internal void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x060028D3 RID: 10451 RVA: 0x00093879 File Offset: 0x00091A79
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		/// <summary>Copies the permission objects of the set to the indicated location in an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy.</param>
		/// <param name="index">The starting position in the array to begin copying (zero based).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter has more than one dimension.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is out of the range of the <paramref name="array" /> parameter.</exception>
		// Token: 0x060028D4 RID: 10452 RVA: 0x00093884 File Offset: 0x00091A84
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.list.Count > 0)
			{
				if (array.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Array has more than one dimension"));
				}
				if (index < 0 || index >= array.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				this.list.CopyTo(array, index);
			}
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permissions specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have the permission demanded.</exception>
		// Token: 0x060028D5 RID: 10453 RVA: 0x000938EC File Offset: 0x00091AEC
		[SecuritySafeCritical]
		public void Demand()
		{
			if (this.IsEmpty())
			{
				return;
			}
			int count = this.list.Count;
			if (this._ignored == null || this._ignored.Length != count)
			{
				this._ignored = new bool[count];
			}
			bool flag = this.IsUnrestricted();
			for (int i = 0; i < count; i++)
			{
				IPermission permission = (IPermission)this.list[i];
				if (permission.GetType().IsSubclassOf(typeof(CodeAccessPermission)))
				{
					this._ignored[i] = false;
					flag = true;
				}
				else
				{
					this._ignored[i] = true;
					permission.Demand();
				}
			}
			if (flag && SecurityManager.SecurityEnabled)
			{
				this.CasOnlyDemand(this._declsec ? 5 : 3);
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000939A1 File Offset: 0x00091BA1
		internal void CasOnlyDemand(int skip)
		{
			if (this._ignored == null)
			{
				this._ignored = new bool[this.list.Count];
			}
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for a permission that has an intersection with a permission of a type contained in the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		/// <exception cref="T:System.Security.SecurityException">A previous call to <see cref="M:System.Security.PermissionSet.Deny" /> has already restricted the permissions for the current stack frame.</exception>
		// Token: 0x060028D7 RID: 10455 RVA: 0x000939C4 File Offset: 0x00091BC4
		[SecuritySafeCritical]
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Deny()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IPermission)enumerator.Current) is IStackWalk)
					{
						throw new NotSupportedException("Currently only declarative Deny are supported.");
					}
				}
			}
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="et" /> parameter's version number is not supported.</exception>
		// Token: 0x060028D8 RID: 10456 RVA: 0x00093A30 File Offset: 0x00091C30
		public virtual void FromXml(SecurityElement et)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PermissionSet")
			{
				throw new ArgumentException(string.Format("Invalid tag {0} expected {1}", et.Tag, "PermissionSet"), "et");
			}
			this.list.Clear();
			if (CodeAccessPermission.IsUnrestricted(et))
			{
				this.state = PermissionState.Unrestricted;
				return;
			}
			this.state = PermissionState.None;
			if (et.Children != null)
			{
				foreach (object obj in et.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					string text = securityElement.Attribute("class");
					if (text == null)
					{
						throw new ArgumentException(Locale.GetText("No permission class is specified."));
					}
					if (this.Resolver != null)
					{
						text = this.Resolver.ResolveClassName(text);
					}
					this.list.Add(PermissionBuilder.Create(text, securityElement));
				}
			}
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x060028D9 RID: 10457 RVA: 0x00093B34 File Offset: 0x00091D34
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.PermissionSet" /> is a subset of the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="target">The permission set to test for the subset relationship. This must be either a <see cref="T:System.Security.PermissionSet" /> or a <see cref="T:System.Security.NamedPermissionSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.PermissionSet" /> is a subset of the <paramref name="target" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028DA RID: 10458 RVA: 0x00093B44 File Offset: 0x00091D44
		public bool IsSubsetOf(PermissionSet target)
		{
			if (target == null || target.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (target.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.IsUnrestricted() && (target == null || !target.IsUnrestricted()))
			{
				return false;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				Type type = permission.GetType();
				IPermission target2;
				if (target.IsUnrestricted() && permission is CodeAccessPermission && permission is IUnrestrictedPermission)
				{
					target2 = (IPermission)Activator.CreateInstance(type, PermissionSet.psUnrestricted);
				}
				else
				{
					target2 = target.GetPermission(type);
				}
				if (!permission.IsSubsetOf(target2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for any <see cref="T:System.Security.PermissionSet" /> that is not a subset of the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		// Token: 0x060028DB RID: 10459 RVA: 0x00093C24 File Offset: 0x00091E24
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecuritySafeCritical]
		public void PermitOnly()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IPermission)enumerator.Current) is IStackWalk)
					{
						throw new NotSupportedException("Currently only declarative Deny are supported.");
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028DC RID: 10460 RVA: 0x00093C90 File Offset: 0x00091E90
		public bool ContainsNonCodeAccessPermissions()
		{
			if (this.list.Count > 0)
			{
				using (IEnumerator enumerator = this.list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((IPermission)enumerator.Current).GetType().IsSubclassOf(typeof(CodeAccessPermission)))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Converts an encoded <see cref="T:System.Security.PermissionSet" /> from one XML encoding format to another XML encoding format.</summary>
		/// <param name="inFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <param name="inData">An XML-encoded permission set.</param>
		/// <param name="outFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <returns>An encrypted permission set with the specified output format.</returns>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x060028DD RID: 10461 RVA: 0x00093D0C File Offset: 0x00091F0C
		public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
		{
			if (inFormat == null)
			{
				throw new ArgumentNullException("inFormat");
			}
			if (outFormat == null)
			{
				throw new ArgumentNullException("outFormat");
			}
			if (inData == null)
			{
				return null;
			}
			if (inFormat == outFormat)
			{
				return inData;
			}
			PermissionSet permissionSet = null;
			if (inFormat == "BINARY")
			{
				if (outFormat.StartsWith("XML"))
				{
					using (MemoryStream memoryStream = new MemoryStream(inData))
					{
						permissionSet = (PermissionSet)new BinaryFormatter().Deserialize(memoryStream);
						memoryStream.Close();
					}
					string s = permissionSet.ToString();
					if (outFormat == "XML" || outFormat == "XMLASCII")
					{
						return Encoding.ASCII.GetBytes(s);
					}
					if (outFormat == "XMLUNICODE")
					{
						return Encoding.Unicode.GetBytes(s);
					}
				}
			}
			else
			{
				if (!inFormat.StartsWith("XML"))
				{
					return null;
				}
				if (outFormat == "BINARY")
				{
					string text = null;
					if (!(inFormat == "XML") && !(inFormat == "XMLASCII"))
					{
						if (inFormat == "XMLUNICODE")
						{
							text = Encoding.Unicode.GetString(inData);
						}
					}
					else
					{
						text = Encoding.ASCII.GetString(inData);
					}
					if (text != null)
					{
						permissionSet = new PermissionSet(PermissionState.None);
						permissionSet.FromXml(SecurityElement.FromString(text));
						MemoryStream memoryStream2 = new MemoryStream();
						new BinaryFormatter().Serialize(memoryStream2, permissionSet);
						memoryStream2.Close();
						return memoryStream2.ToArray();
					}
				}
				else if (outFormat.StartsWith("XML"))
				{
					throw new XmlSyntaxException(string.Format(Locale.GetText("Can't convert from {0} to {1}"), inFormat, outFormat));
				}
			}
			throw new SerializationException(string.Format(Locale.GetText("Unknown output format {0}."), outFormat));
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <param name="permClass">The type of the desired permission object.</param>
		/// <returns>A copy of the permission object of the type specified by the <paramref name="permClass" /> parameter contained in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if none exists.</returns>
		// Token: 0x060028DE RID: 10462 RVA: 0x00093EC4 File Offset: 0x000920C4
		public IPermission GetPermission(Type permClass)
		{
			if (permClass == null || this.list.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj != null && obj.GetType().Equals(permClass))
				{
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Creates and returns a permission set that is the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="other">A permission set to intersect with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>A new permission set that represents the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified target. This object is <see langword="null" /> if the intersection is empty.</returns>
		// Token: 0x060028DF RID: 10463 RVA: 0x00093F48 File Offset: 0x00092148
		public PermissionSet Intersect(PermissionSet other)
		{
			if (other == null || other.IsEmpty() || this.IsEmpty())
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (this.IsUnrestricted() && other.IsUnrestricted())
			{
				permissionState = PermissionState.Unrestricted;
			}
			PermissionSet permissionSet;
			if (permissionState == PermissionState.Unrestricted)
			{
				permissionSet = new PermissionSet(permissionState);
			}
			else if (this.IsUnrestricted())
			{
				permissionSet = other.Copy();
			}
			else if (other.IsUnrestricted())
			{
				permissionSet = this.Copy();
			}
			else
			{
				permissionSet = new PermissionSet(permissionState);
				this.InternalIntersect(permissionSet, this, other, false);
			}
			return permissionSet;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00093FC4 File Offset: 0x000921C4
		internal void InternalIntersect(PermissionSet intersect, PermissionSet a, PermissionSet b, bool unrestricted)
		{
			foreach (object obj in b.list)
			{
				IPermission permission = (IPermission)obj;
				IPermission permission2 = a.GetPermission(permission.GetType());
				if (permission2 != null)
				{
					intersect.AddPermission(permission.Intersect(permission2));
				}
				else if (unrestricted)
				{
					intersect.AddPermission(permission);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028E1 RID: 10465 RVA: 0x00094044 File Offset: 0x00092244
		public bool IsEmpty()
		{
			if (this.state == PermissionState.Unrestricted)
			{
				return false;
			}
			if (this.list == null || this.list.Count == 0)
			{
				return true;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IPermission)enumerator.Current).IsSubsetOf(null))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="T:System.Security.PermissionSet" /> is <see langword="Unrestricted" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.PermissionSet" /> is <see langword="Unrestricted" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028E2 RID: 10466 RVA: 0x000940C8 File Offset: 0x000922C8
		public bool IsUnrestricted()
		{
			return this.state == PermissionState.Unrestricted;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <param name="permClass">The type of permission to delete.</param>
		/// <returns>The permission removed from the set.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028E3 RID: 10467 RVA: 0x000940D4 File Offset: 0x000922D4
		public IPermission RemovePermission(Type permClass)
		{
			if (permClass == null || this._readOnly)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj.GetType().Equals(permClass))
				{
					this.list.Remove(obj);
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <param name="perm">The permission to set.</param>
		/// <returns>The set permission.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028E4 RID: 10468 RVA: 0x0009415C File Offset: 0x0009235C
		public IPermission SetPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			IUnrestrictedPermission unrestrictedPermission = perm as IUnrestrictedPermission;
			if (unrestrictedPermission == null)
			{
				this.state = PermissionState.None;
			}
			else
			{
				this.state = (unrestrictedPermission.IsUnrestricted() ? this.state : PermissionState.None);
			}
			this.RemovePermission(perm.GetType());
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x060028E5 RID: 10469 RVA: 0x000941BB File Offset: 0x000923BB
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060028E6 RID: 10470 RVA: 0x000941C8 File Offset: 0x000923C8
		public virtual SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", base.GetType().FullName);
			securityElement.AddAttribute("version", 1.ToString());
			if (this.state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				securityElement.AddChild(permission.ToXml());
			}
			return securityElement;
		}

		/// <summary>Creates a <see cref="T:System.Security.PermissionSet" /> that is the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="other">The permission set to form the union with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>A new permission set that represents the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x060028E7 RID: 10471 RVA: 0x00094278 File Offset: 0x00092478
		public PermissionSet Union(PermissionSet other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			PermissionSet permissionSet = null;
			if (this.IsUnrestricted() || other.IsUnrestricted())
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			permissionSet = this.Copy();
			foreach (object obj in other.list)
			{
				IPermission perm = (IPermission)obj;
				permissionSet.AddPermission(perm);
			}
			return permissionSet;
		}

		/// <summary>Gets the number of permission objects contained in the permission set.</summary>
		/// <returns>The number of permission objects contained in the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x00094300 File Offset: 0x00092500
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x0009430D File Offset: 0x0009250D
		public virtual bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the root object of the current collection.</summary>
		/// <returns>The root object of the current collection.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0000270D File Offset: 0x0000090D
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0009431A File Offset: 0x0009251A
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x00094322 File Offset: 0x00092522
		internal bool DeclarativeSecurity
		{
			get
			{
				return this._declsec;
			}
			set
			{
				this._declsec = value;
			}
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x060028EE RID: 10478 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MonoTODO("may not be required")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.PermissionSet" /> or <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Security.PermissionSet" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current <see cref="T:System.Security.PermissionSet" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028EF RID: 10479 RVA: 0x0009432C File Offset: 0x0009252C
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PermissionSet permissionSet = obj as PermissionSet;
			if (permissionSet == null)
			{
				return false;
			}
			if (this.state != permissionSet.state)
			{
				return false;
			}
			if (this.list.Count != permissionSet.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				bool flag = false;
				int num = 0;
				while (i < permissionSet.list.Count)
				{
					if (this.list[i].Equals(permissionSet.list[num]))
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.PermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.PermissionSet" /> object.</returns>
		// Token: 0x060028F0 RID: 10480 RVA: 0x000943C6 File Offset: 0x000925C6
		[ComVisible(false)]
		public override int GetHashCode()
		{
			if (this.list.Count != 0)
			{
				return base.GetHashCode();
			}
			return (int)this.state;
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer be in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x060028F1 RID: 10481 RVA: 0x000943E2 File Offset: 0x000925E2
		public static void RevertAssert()
		{
			CodeAccessPermission.RevertAssert();
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000943E9 File Offset: 0x000925E9
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000943F1 File Offset: 0x000925F1
		internal PolicyLevel Resolver
		{
			get
			{
				return this._policyLevel;
			}
			set
			{
				this._policyLevel = value;
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000943FA File Offset: 0x000925FA
		internal void SetReadOnly(bool value)
		{
			this._readOnly = value;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00094404 File Offset: 0x00092604
		private bool AllIgnored()
		{
			if (this._ignored == null)
			{
				throw new NotSupportedException("bad bad bad");
			}
			for (int i = 0; i < this._ignored.Length; i++)
			{
				if (!this._ignored[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00094444 File Offset: 0x00092644
		internal static PermissionSet CreateFromBinaryFormat(byte[] data)
		{
			if (data == null || data[0] != 46 || data.Length < 2)
			{
				throw new SecurityException(Locale.GetText("Invalid data in 2.0 metadata format."));
			}
			int num = 1;
			int num2 = PermissionSet.ReadEncodedInt(data, ref num);
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			for (int i = 0; i < num2; i++)
			{
				IPermission permission = PermissionSet.ProcessAttribute(data, ref num);
				if (permission == null)
				{
					throw new SecurityException(Locale.GetText("Unsupported data found in 2.0 metadata format."));
				}
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000944B8 File Offset: 0x000926B8
		internal static int ReadEncodedInt(byte[] data, ref int position)
		{
			int result;
			if ((data[position] & 128) == 0)
			{
				result = (int)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				result = ((int)(data[position] & 63) << 8 | (int)data[position + 1]);
				position += 2;
			}
			else
			{
				result = ((int)(data[position] & 31) << 24 | (int)data[position + 1] << 16 | (int)data[position + 2] << 8 | (int)data[position + 3]);
				position += 4;
			}
			return result;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00094530 File Offset: 0x00092730
		internal static IPermission ProcessAttribute(byte[] data, ref int position)
		{
			int num = PermissionSet.ReadEncodedInt(data, ref position);
			string @string = Encoding.UTF8.GetString(data, position, num);
			position += num;
			Type type = Type.GetType(@string);
			SecurityAttribute securityAttribute = Activator.CreateInstance(type, PermissionSet.action) as SecurityAttribute;
			if (securityAttribute == null)
			{
				return null;
			}
			PermissionSet.ReadEncodedInt(data, ref position);
			int num2 = PermissionSet.ReadEncodedInt(data, ref position);
			for (int i = 0; i < num2; i++)
			{
				int num3 = position;
				position = num3 + 1;
				byte b = data[num3];
				bool flag;
				if (b != 83)
				{
					if (b != 84)
					{
						return null;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = false;
				num3 = position;
				position = num3 + 1;
				byte b2 = data[num3];
				if (b2 == 29)
				{
					flag2 = true;
					num3 = position;
					position = num3 + 1;
					b2 = data[num3];
				}
				int num4 = PermissionSet.ReadEncodedInt(data, ref position);
				string string2 = Encoding.UTF8.GetString(data, position, num4);
				position += num4;
				int num5 = 1;
				if (flag2)
				{
					num5 = BitConverter.ToInt32(data, position);
					position += 4;
				}
				object[] index = null;
				for (int j = 0; j < num5; j++)
				{
					object value;
					switch (b2)
					{
					case 2:
						num3 = position;
						position = num3 + 1;
						value = Convert.ToBoolean(data[num3]);
						break;
					case 3:
						value = Convert.ToChar(data[position]);
						position += 2;
						break;
					case 4:
						num3 = position;
						position = num3 + 1;
						value = Convert.ToSByte(data[num3]);
						break;
					case 5:
						num3 = position;
						position = num3 + 1;
						value = Convert.ToByte(data[num3]);
						break;
					case 6:
						value = Convert.ToInt16(data[position]);
						position += 2;
						break;
					case 7:
						value = Convert.ToUInt16(data[position]);
						position += 2;
						break;
					case 8:
						value = Convert.ToInt32(data[position]);
						position += 4;
						break;
					case 9:
						value = Convert.ToUInt32(data[position]);
						position += 4;
						break;
					case 10:
						value = Convert.ToInt64(data[position]);
						position += 8;
						break;
					case 11:
						value = Convert.ToUInt64(data[position]);
						position += 8;
						break;
					case 12:
						value = Convert.ToSingle(data[position]);
						position += 4;
						break;
					case 13:
						value = Convert.ToDouble(data[position]);
						position += 8;
						break;
					case 14:
					{
						string text = null;
						if (data[position] != 255)
						{
							int num6 = PermissionSet.ReadEncodedInt(data, ref position);
							text = Encoding.UTF8.GetString(data, position, num6);
							position += num6;
						}
						else
						{
							position++;
						}
						value = text;
						break;
					}
					default:
					{
						if (b2 != 80)
						{
							return null;
						}
						int num7 = PermissionSet.ReadEncodedInt(data, ref position);
						value = Type.GetType(Encoding.UTF8.GetString(data, position, num7));
						position += num7;
						break;
					}
					}
					if (flag)
					{
						type.GetProperty(string2).SetValue(securityAttribute, value, index);
					}
					else
					{
						type.GetField(string2).SetValue(securityAttribute, value);
					}
				}
			}
			return securityAttribute.CreatePermission();
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00094876 File Offset: 0x00092A76
		// Note: this type is marked as 'beforefieldinit'.
		static PermissionSet()
		{
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="perm">The permission to add.</param>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if <paramref name="perm" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028FA RID: 10490 RVA: 0x00052955 File Offset: 0x00050B55
		protected virtual IPermission AddPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x060028FB RID: 10491 RVA: 0x00052955 File Offset: 0x00050B55
		protected virtual IEnumerator GetEnumeratorImpl()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <param name="permClass">The type of the permission object.</param>
		/// <returns>A copy of the permission object, of the type specified by the <paramref name="permClass" /> parameter, contained in the <see cref="T:System.Security.PermissionSet" />, or <see langword="null" /> if none exists.</returns>
		// Token: 0x060028FC RID: 10492 RVA: 0x00052955 File Offset: 0x00050B55
		protected virtual IPermission GetPermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <param name="permClass">The type of the permission to remove.</param>
		/// <returns>The permission removed from the set.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028FD RID: 10493 RVA: 0x00052955 File Offset: 0x00050B55
		protected virtual IPermission RemovePermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <param name="perm">The permission to set.</param>
		/// <returns>The set permission.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x060028FE RID: 10494 RVA: 0x00052955 File Offset: 0x00050B55
		protected virtual IPermission SetPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001EC2 RID: 7874
		private const string tagName = "PermissionSet";

		// Token: 0x04001EC3 RID: 7875
		private const int version = 1;

		// Token: 0x04001EC4 RID: 7876
		private static object[] psUnrestricted = new object[]
		{
			PermissionState.Unrestricted
		};

		// Token: 0x04001EC5 RID: 7877
		private PermissionState state;

		// Token: 0x04001EC6 RID: 7878
		private ArrayList list;

		// Token: 0x04001EC7 RID: 7879
		private PolicyLevel _policyLevel;

		// Token: 0x04001EC8 RID: 7880
		private bool _declsec;

		// Token: 0x04001EC9 RID: 7881
		private bool _readOnly;

		// Token: 0x04001ECA RID: 7882
		private bool[] _ignored;

		// Token: 0x04001ECB RID: 7883
		private static object[] action = new object[]
		{
			(SecurityAction)0
		};
	}
}
