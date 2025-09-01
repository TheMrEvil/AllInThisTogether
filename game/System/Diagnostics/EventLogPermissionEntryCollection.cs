using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects.</summary>
	// Token: 0x02000264 RID: 612
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		internal EventLogPermissionEntryCollection(EventLogPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					EventLogPermissionEntry value = new EventLogPermissionEntry((EventLogPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
					base.InnerList.Add(value);
				}
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <param name="index">The zero-based index into the collection.</param>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that exists at the specified index.</returns>
		// Token: 0x1700037A RID: 890
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to this collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to add.</param>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</returns>
		// Token: 0x0600131D RID: 4893 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600131E RID: 4894 RVA: 0x00051050 File Offset: 0x0004F250
		public void AddRange(EventLogPermissionEntry[] value)
		{
			foreach (EventLogPermissionEntry value2 in value)
			{
				base.List.Add(value2);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600131F RID: 4895 RVA: 0x00051080 File Offset: 0x0004F280
		public void AddRange(EventLogPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				EventLogPermissionEntry value2 = (EventLogPermissionEntry)obj;
				base.List.Add(value2);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> belongs to this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001320 RID: 4896 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that receives this collection's permission entries.</param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries.</param>
		// Token: 0x06001321 RID: 4897 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <param name="value">The permission entry to search for.</param>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		// Token: 0x06001322 RID: 4898 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry.</param>
		/// <param name="value">The permission entry to insert into this collection.</param>
		// Token: 0x06001323 RID: 4899 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06001324 RID: 4900 RVA: 0x00051116 File Offset: 0x0004F316
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06001325 RID: 4901 RVA: 0x00051123 File Offset: 0x0004F323
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />.</param>
		// Token: 0x06001326 RID: 4902 RVA: 0x00051131 File Offset: 0x0004F331
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06001327 RID: 4903 RVA: 0x0005113F File Offset: 0x0004F33F
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove.</param>
		// Token: 0x06001328 RID: 4904 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal EventLogPermissionEntryCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000ADB RID: 2779
		private EventLogPermission owner;
	}
}
