using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects.</summary>
	// Token: 0x0200027A RID: 634
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x00052F18 File Offset: 0x00051118
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					PerformanceCounterPermissionAccess permissionAccess = (PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess;
					string machineName = resourcePermissionBaseEntry.PermissionAccessPath[0];
					string categoryName = resourcePermissionBaseEntry.PermissionAccessPath[1];
					PerformanceCounterPermissionEntry value = new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName);
					base.InnerList.Add(value);
				}
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00052F84 File Offset: 0x00051184
		internal PerformanceCounterPermissionEntryCollection(ResourcePermissionBaseEntry[] entries)
		{
			foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
			{
				base.List.Add(new PerformanceCounterPermissionEntry((PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0], resourcePermissionBaseEntry.PermissionAccessPath[1]));
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <param name="index">The zero-based index into the collection.</param>
		/// <returns>The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object that exists at the specified index.</returns>
		// Token: 0x170003C6 RID: 966
		public PerformanceCounterPermissionEntry this[int index]
		{
			get
			{
				return (PerformanceCounterPermissionEntry)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> to this collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to add.</param>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</returns>
		// Token: 0x06001432 RID: 5170 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001433 RID: 5171 RVA: 0x00052FE8 File Offset: 0x000511E8
		public void AddRange(PerformanceCounterPermissionEntry[] value)
		{
			foreach (PerformanceCounterPermissionEntry value2 in value)
			{
				base.List.Add(value2);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001434 RID: 5172 RVA: 0x00053018 File Offset: 0x00051218
		public void AddRange(PerformanceCounterPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				PerformanceCounterPermissionEntry value2 = (PerformanceCounterPermissionEntry)obj;
				base.List.Add(value2);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object belongs to this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001435 RID: 5173 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> that receives this collection's permission entries.</param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries.</param>
		// Token: 0x06001436 RID: 5174 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <param name="value">The permission entry for which to search.</param>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		// Token: 0x06001437 RID: 5175 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry.</param>
		/// <param name="value">The permission entry to insert into this collection.</param>
		// Token: 0x06001438 RID: 5176 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06001439 RID: 5177 RVA: 0x00053074 File Offset: 0x00051274
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x0600143A RID: 5178 RVA: 0x00053081 File Offset: 0x00051281
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />.</param>
		// Token: 0x0600143B RID: 5179 RVA: 0x0005308F File Offset: 0x0005128F
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x0600143C RID: 5180 RVA: 0x0005309D File Offset: 0x0005129D
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove.</param>
		// Token: 0x0600143D RID: 5181 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal PerformanceCounterPermissionEntryCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B32 RID: 2866
		private PerformanceCounterPermission owner;
	}
}
