using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	// Token: 0x02000276 RID: 630
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class.</summary>
		// Token: 0x06001417 RID: 5143 RVA: 0x00052CDD File Offset: 0x00050EDD
		public PerformanceCounterPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.PerformanceCounterPermission.PermissionEntries" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is <see langword="null" />.</exception>
		// Token: 0x06001418 RID: 5144 RVA: 0x00052CEB File Offset: 0x00050EEB
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06001419 RID: 5145 RVA: 0x00052D1F File Offset: 0x00050F1F
		public PerformanceCounterPermission(PermissionState state) : base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified access levels, the name of the computer to use, and the category associated with the performance counter.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</param>
		/// <param name="machineName">The server on which the performance counter and its associate category reside.</param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which the performance counter is associated.</param>
		// Token: 0x0600141A RID: 5146 RVA: 0x00052D2E File Offset: 0x00050F2E
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.Add(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00052D5C File Offset: 0x00050F5C
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00052D78 File Offset: 0x00050F78
		private void SetUp()
		{
			base.TagNames = new string[]
			{
				"Machine",
				"Category"
			};
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00050EC1 File Offset: 0x0004F0C1
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00050EC9 File Offset: 0x0004F0C9
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00052DA8 File Offset: 0x00050FA8
		internal void Add(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.AddPermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00052DC8 File Offset: 0x00050FC8
		internal void Remove(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.RemovePermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04000B23 RID: 2851
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
