using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	// Token: 0x02000260 RID: 608
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x06001306 RID: 4870 RVA: 0x00050E01 File Offset: 0x0004F001
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission entries.</summary>
		/// <param name="permissionAccessEntries">An array of  objects that represent permission entries. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is <see langword="null" />.</exception>
		// Token: 0x06001307 RID: 4871 RVA: 0x00050E0F File Offset: 0x0004F00F
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06001308 RID: 4872 RVA: 0x00050E43 File Offset: 0x0004F043
		public EventLogPermission(PermissionState state) : base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the enumeration values that specifies an access level.</param>
		/// <param name="machineName">The name of the computer on which to read or write events.</param>
		// Token: 0x06001309 RID: 4873 RVA: 0x00050E52 File Offset: 0x0004F052
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.Add(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A collection that contains the permission entries for this permissions request.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00050E7F File Offset: 0x0004F07F
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00050E9B File Offset: 0x0004F09B
		private void SetUp()
		{
			base.TagNames = new string[]
			{
				"Machine"
			};
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00050EC1 File Offset: 0x0004F0C1
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00050EC9 File Offset: 0x0004F0C9
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00050ED4 File Offset: 0x0004F0D4
		internal void Add(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.AddPermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00050EF4 File Offset: 0x0004F0F4
		internal void Remove(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.RemovePermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000ACF RID: 2767
		private EventLogPermissionEntryCollection innerCollection;
	}
}
