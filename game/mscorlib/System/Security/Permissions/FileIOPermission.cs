using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files and folders. This class cannot be inherited.</summary>
	// Token: 0x02000438 RID: 1080
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06002BC6 RID: 11206 RVA: 0x0009E002 File Offset: 0x0009C202
		static FileIOPermission()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002BC7 RID: 11207 RVA: 0x0009E018 File Offset: 0x0009C218
		public FileIOPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.m_Unrestricted = true;
				this.m_AllFilesAccess = FileIOPermissionAccess.AllAccess;
				this.m_AllLocalFilesAccess = FileIOPermissionAccess.AllAccess;
			}
			this.CreateLists();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter does not specify the absolute path to the file or directory.</exception>
		// Token: 0x06002BC8 RID: 11208 RVA: 0x0009E047 File Offset: 0x0009C247
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CreateLists();
			this.AddPathList(access, path);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not a valid string.</exception>
		// Token: 0x06002BC9 RID: 11209 RVA: 0x0009E06B File Offset: 0x0009C26B
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			this.CreateLists();
			this.AddPathList(access, pathList);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0009E08F File Offset: 0x0009C28F
		internal void CreateLists()
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.appendList = new ArrayList();
			this.pathList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter does not specify the absolute path to the file or directory.</exception>
		// Token: 0x06002BCB RID: 11211 RVA: 0x0009E0BD File Offset: 0x0009C2BD
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not a valid string.</exception>
		// Token: 0x06002BCC RID: 11212 RVA: 0x0009E0BD File Offset: 0x0009C2BD
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0009E0CA File Offset: 0x0009C2CA
		internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>The set of file I/O flags for all files.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0009E0D2 File Offset: 0x0009C2D2
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x0009E0DA File Offset: 0x0009C2DA
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_AllFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllFilesAccess = value;
				}
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>The set of file I/O flags for all local files.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x0009E0EB File Offset: 0x0009C2EB
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x0009E0F3 File Offset: 0x0009C2F3
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_AllLocalFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllLocalFilesAccess = value;
				}
			}
		}

		/// <summary>Adds access for the specified file or directory to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="path">The absolute path of a file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter did not specify the absolute path to the file or directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter has an invalid format.</exception>
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0009E104 File Offset: 0x0009C304
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.AddPathInternal(access, path);
		}

		/// <summary>Adds access for the specified files and directories to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not valid.</exception>
		/// <exception cref="T:System.NotSupportedException">An entry in the <paramref name="pathList" /> array has an invalid format.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002BD3 RID: 11219 RVA: 0x0009E124 File Offset: 0x0009C324
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			foreach (string path in pathList)
			{
				this.AddPathInternal(access, path);
			}
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0009E164 File Offset: 0x0009C364
		internal void AddPathInternal(FileIOPermissionAccess access, string path)
		{
			path = Path.InsecureGetFullPath(path);
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Add(path);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Add(path);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002BD5 RID: 11221 RVA: 0x0009E1C8 File Offset: 0x0009C3C8
		public override IPermission Copy()
		{
			if (this.m_Unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			return new FileIOPermission(PermissionState.None)
			{
				readList = (ArrayList)this.readList.Clone(),
				writeList = (ArrayList)this.writeList.Clone(),
				appendList = (ArrayList)this.appendList.Clone(),
				pathList = (ArrayList)this.pathList.Clone(),
				m_AllFilesAccess = this.m_AllFilesAccess,
				m_AllLocalFilesAccess = this.m_AllLocalFilesAccess
			};
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not compatible.</exception>
		// Token: 0x06002BD6 RID: 11222 RVA: 0x0009E25C File Offset: 0x0009C45C
		[SecuritySafeCritical]
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.m_Unrestricted = true;
				return;
			}
			this.m_Unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Read, array);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Write, array);
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.Append, array);
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				string[] array = text.Split(';', StringSplitOptions.None);
				this.AddPathList(FileIOPermissionAccess.PathDiscovery, array);
			}
		}

		/// <summary>Gets all files and directories with the specified <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents a single type of file access.</param>
		/// <returns>An array containing the paths of the files and directories to which access specified by the <paramref name="access" /> parameter is granted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		/// -or-  
		/// <paramref name="access" /> is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, which represents more than one type of file access, or <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, which does not represent any type of file access.</exception>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x0009E314 File Offset: 0x0009C514
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			ArrayList arrayList = new ArrayList();
			switch (access)
			{
			case FileIOPermissionAccess.NoAccess:
				goto IL_7F;
			case FileIOPermissionAccess.Read:
				arrayList.AddRange(this.readList);
				goto IL_7F;
			case FileIOPermissionAccess.Write:
				arrayList.AddRange(this.writeList);
				goto IL_7F;
			case FileIOPermissionAccess.Append:
				arrayList.AddRange(this.appendList);
				goto IL_7F;
			case FileIOPermissionAccess.PathDiscovery:
				arrayList.AddRange(this.pathList);
				goto IL_7F;
			}
			FileIOPermission.ThrowInvalidFlag(access, false);
			IL_7F:
			if (arrayList.Count <= 0)
			{
				return null;
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BD8 RID: 11224 RVA: 0x0009E3C0 File Offset: 0x0009C5C0
		public override IPermission Intersect(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission2 = new FileIOPermission(PermissionState.None);
			fileIOPermission2.AllFiles = (this.m_AllFilesAccess & fileIOPermission.AllFiles);
			fileIOPermission2.AllLocalFiles = (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles);
			FileIOPermission.IntersectKeys(this.readList, fileIOPermission.readList, fileIOPermission2.readList);
			FileIOPermission.IntersectKeys(this.writeList, fileIOPermission.writeList, fileIOPermission2.writeList);
			FileIOPermission.IntersectKeys(this.appendList, fileIOPermission.appendList, fileIOPermission2.appendList);
			FileIOPermission.IntersectKeys(this.pathList, fileIOPermission.pathList, fileIOPermission2.pathList);
			if (!fileIOPermission2.IsEmpty())
			{
				return fileIOPermission2;
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x0009E48C File Offset: 0x0009C68C
		public override bool IsSubsetOf(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return false;
			}
			if (fileIOPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.IsUnrestricted();
			}
			return fileIOPermission.IsUnrestricted() || ((this.m_AllFilesAccess & fileIOPermission.AllFiles) == this.m_AllFilesAccess && (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles) == this.m_AllLocalFilesAccess && FileIOPermission.KeyIsSubsetOf(this.appendList, fileIOPermission.appendList) && FileIOPermission.KeyIsSubsetOf(this.readList, fileIOPermission.readList) && FileIOPermission.KeyIsSubsetOf(this.writeList, fileIOPermission.writeList) && FileIOPermission.KeyIsSubsetOf(this.pathList, fileIOPermission.pathList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BDA RID: 11226 RVA: 0x0009E550 File Offset: 0x0009C750
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		/// <summary>Sets the specified access to the specified file or directory, replacing the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter did not specify the absolute path to the file or directory.</exception>
		// Token: 0x06002BDB RID: 11227 RVA: 0x0009E558 File Offset: 0x0009C758
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.Clear(access);
			this.AddPathInternal(access, path);
		}

		/// <summary>Sets the specified access to the specified files and directories, replacing the current state for the specified access with the new set of paths.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x06002BDC RID: 11228 RVA: 0x0009E580 File Offset: 0x0009C780
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			this.Clear(access);
			foreach (string path in pathList)
			{
				this.AddPathInternal(access, path);
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002BDD RID: 11229 RVA: 0x0009E5C4 File Offset: 0x0009C7C4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string[] array = this.GetPathList(FileIOPermissionAccess.Append);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Append", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Read);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Read", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Write);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("Write", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.PathDiscovery);
				if (array != null && array.Length != 0)
				{
					securityElement.AddAttribute("PathDiscovery", string.Join(";", array));
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BDE RID: 11230 RVA: 0x0009E68C File Offset: 0x0009C88C
		public override IPermission Union(IPermission other)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(other);
			if (fileIOPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && fileIOPermission.IsEmpty())
			{
				return null;
			}
			FileIOPermission fileIOPermission2 = (FileIOPermission)this.Copy();
			fileIOPermission2.AllFiles |= fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles |= fileIOPermission.AllLocalFiles;
			string[] array = fileIOPermission.GetPathList(FileIOPermissionAccess.Read);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.readList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Write);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.writeList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Append);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.appendList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.PathDiscovery);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.pathList, array);
			}
			return fileIOPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.FileIOPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BDF RID: 11231 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return false;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.FileIOPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object.</returns>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x0009E762 File Offset: 0x0009C962
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x00015831 File Offset: 0x00013A31
		int IBuiltInPermission.GetTokenIndex()
		{
			return 2;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0009E76C File Offset: 0x0009C96C
		private bool IsEmpty()
		{
			return !this.m_Unrestricted && this.appendList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0 && this.pathList.Count == 0;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x0009E7B8 File Offset: 0x0009C9B8
		private static FileIOPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileIOPermission));
			}
			return fileIOPermission;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		internal static void ThrowInvalidFlag(FileIOPermissionAccess access, bool context)
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
			throw new ArgumentException(string.Format(text, access), "access");
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0009E818 File Offset: 0x0009CA18
		internal static void ThrowIfInvalidPath(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != null && directoryName.LastIndexOfAny(FileIOPermission.BadPathNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), path), "path");
			}
			string fileName = Path.GetFileName(path);
			if (fileName != null && fileName.LastIndexOfAny(FileIOPermission.BadFileNameCharacters) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), path), "path");
			}
			if (!Path.IsPathRooted(path))
			{
				throw new ArgumentException(Locale.GetText("Absolute path information is required."), "path");
			}
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0009E8A8 File Offset: 0x0009CAA8
		internal static void ThrowIfInvalidPath(string[] paths)
		{
			for (int i = 0; i < paths.Length; i++)
			{
				FileIOPermission.ThrowIfInvalidPath(paths[i]);
			}
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0009E8D0 File Offset: 0x0009CAD0
		internal void Clear(FileIOPermissionAccess access)
		{
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Clear();
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Clear();
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Clear();
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Clear();
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0009E924 File Offset: 0x0009CB24
		internal static bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string path = (string)obj;
				using (IEnumerator enumerator2 = target.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (Path.IsPathSubsetOf((string)enumerator2.Current, path))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0009E9CC File Offset: 0x0009CBCC
		internal static void UnionKeys(IList list, string[] paths)
		{
			foreach (string text in paths)
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					int j;
					for (j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (Path.IsPathSubsetOf(text, text2))
						{
							list[j] = text;
							break;
						}
						if (Path.IsPathSubsetOf(text2, text))
						{
							break;
						}
					}
					if (j == count)
					{
						list.Add(text);
					}
				}
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0009EA4C File Offset: 0x0009CC4C
		internal static void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (Path.IsPathSubsetOf(text, text2))
						{
							result.Add(text2);
						}
					}
					else if (Path.IsPathSubsetOf(text2, text))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x04002013 RID: 8211
		private const int version = 1;

		// Token: 0x04002014 RID: 8212
		private static char[] BadPathNameCharacters = Path.GetInvalidPathChars();

		// Token: 0x04002015 RID: 8213
		private static char[] BadFileNameCharacters = Path.GetInvalidFileNameChars();

		// Token: 0x04002016 RID: 8214
		private bool m_Unrestricted;

		// Token: 0x04002017 RID: 8215
		private FileIOPermissionAccess m_AllFilesAccess;

		// Token: 0x04002018 RID: 8216
		private FileIOPermissionAccess m_AllLocalFilesAccess;

		// Token: 0x04002019 RID: 8217
		private ArrayList readList;

		// Token: 0x0400201A RID: 8218
		private ArrayList writeList;

		// Token: 0x0400201B RID: 8219
		private ArrayList appendList;

		// Token: 0x0400201C RID: 8220
		private ArrayList pathList;
	}
}
