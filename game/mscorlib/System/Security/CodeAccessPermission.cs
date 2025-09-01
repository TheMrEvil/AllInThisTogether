using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Defines the underlying structure of all code access permissions.</summary>
	// Token: 0x020003DD RID: 989
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.CodeAccessPermission" /> class.</summary>
		// Token: 0x0600288B RID: 10379 RVA: 0x0000259F File Offset: 0x0000079F
		protected CodeAccessPermission()
		{
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.CodeAccessPermission.Assert" /> can create security issues.</summary>
		/// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.  
		///  -or-  
		///  There is already an active <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x0600288C RID: 10380 RVA: 0x00093060 File Offset: 0x00091260
		[SecuritySafeCritical]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Assert()
		{
			new PermissionSet(this).Assert();
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x0600288D RID: 10381
		public abstract IPermission Copy();

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permission specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have the permission specified by the current instance.  
		///  -or-  
		///  A caller higher in the call stack has called <see cref="M:System.Security.CodeAccessPermission.Deny" /> on the current permission object.</exception>
		// Token: 0x0600288E RID: 10382 RVA: 0x0009306D File Offset: 0x0009126D
		[SecuritySafeCritical]
		public void Demand()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			new PermissionSet(this).CasOnlyDemand(3);
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame.</exception>
		// Token: 0x0600288F RID: 10383 RVA: 0x00093083 File Offset: 0x00091283
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecuritySafeCritical]
		public void Deny()
		{
			new PermissionSet(this).Deny();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.CodeAccessPermission" /> object to compare with the current <see cref="T:System.Security.CodeAccessPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002890 RID: 10384 RVA: 0x00093090 File Offset: 0x00091290
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
			return this.IsSubsetOf(codeAccessPermission) && codeAccessPermission.IsSubsetOf(this);
		}

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter does not contain the XML encoding for an instance of the same type as the current instance.  
		///  -or-  
		///  The version number of the <paramref name="elem" /> parameter is not supported.</exception>
		// Token: 0x06002891 RID: 10385
		public abstract void FromXml(SecurityElement elem);

		/// <summary>Gets a hash code for the <see cref="T:System.Security.CodeAccessPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.CodeAccessPermission" /> object.</returns>
		// Token: 0x06002892 RID: 10386 RVA: 0x000930D0 File Offset: 0x000912D0
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not an instance of the same class as the current permission.</exception>
		// Token: 0x06002893 RID: 10387
		public abstract IPermission Intersect(IPermission target);

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002894 RID: 10388
		public abstract bool IsSubsetOf(IPermission target);

		/// <summary>Creates and returns a string representation of the current permission object.</summary>
		/// <returns>A string representation of the current permission object.</returns>
		// Token: 0x06002895 RID: 10389 RVA: 0x000930D8 File Offset: 0x000912D8
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002896 RID: 10390
		public abstract SecurityElement ToXml();

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="other" /> parameter is not <see langword="null" />. This method is only supported at this level when passed <see langword="null" />.</exception>
		// Token: 0x06002897 RID: 10391 RVA: 0x000930E5 File Offset: 0x000912E5
		public virtual IPermission Union(IPermission other)
		{
			if (other != null)
			{
				throw new NotSupportedException();
			}
			return null;
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access all resources except for the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x06002898 RID: 10392 RVA: 0x000930F1 File Offset: 0x000912F1
		[SecuritySafeCritical]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void PermitOnly()
		{
			new PermissionSet(this).PermitOnly();
		}

		/// <summary>Causes all previous overrides for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" />, <see cref="M:System.Security.CodeAccessPermission.Deny" />, or <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x06002899 RID: 10393 RVA: 0x000930FE File Offset: 0x000912FE
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAll()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame.</exception>
		// Token: 0x0600289A RID: 10394 RVA: 0x000930FE File Offset: 0x000912FE
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAssert()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame.</exception>
		// Token: 0x0600289B RID: 10395 RVA: 0x000930FE File Offset: 0x000912FE
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertDeny()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame.</exception>
		// Token: 0x0600289C RID: 10396 RVA: 0x000930FE File Offset: 0x000912FE
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertPermitOnly()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x00093110 File Offset: 0x00091310
		internal SecurityElement Element(int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0009316F File Offset: 0x0009136F
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None && state != PermissionState.Unrestricted)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
			}
			return state;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0009319C File Offset: 0x0009139C
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}"), se.Tag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00093258 File Offset: 0x00091458
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0009328A File Offset: 0x0009148A
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
