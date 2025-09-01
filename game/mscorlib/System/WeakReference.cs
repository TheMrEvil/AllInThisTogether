using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	// Token: 0x0200026A RID: 618
	[ComVisible(true)]
	[Serializable]
	public class WeakReference : ISerializable
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x00069200 File Offset: 0x00067400
		private void AllocateHandle(object target)
		{
			if (this.isLongReference)
			{
				this.gcHandle = GCHandle.Alloc(target, GCHandleType.WeakTrackResurrection);
				return;
			}
			this.gcHandle = GCHandle.Alloc(target, GCHandleType.Weak);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object.</summary>
		/// <param name="target">The object to track or <see langword="null" />.</param>
		// Token: 0x06001C1A RID: 7194 RVA: 0x00069225 File Offset: 0x00067425
		public WeakReference(object target) : this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, referencing the specified object and using the specified resurrection tracking.</summary>
		/// <param name="target">An object to track.</param>
		/// <param name="trackResurrection">Indicates when to stop tracking the object. If <see langword="true" />, the object is tracked after finalization; if <see langword="false" />, the object is only tracked until finalization.</param>
		// Token: 0x06001C1B RID: 7195 RVA: 0x0006922F File Offset: 0x0006742F
		public WeakReference(object target, bool trackResurrection)
		{
			this.isLongReference = trackResurrection;
			this.AllocateHandle(target);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference" /> class, using deserialized data from the specified serialization and stream objects.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object.</param>
		/// <param name="context">(Reserved) Describes the source and destination of the serialized stream specified by <paramref name="info" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06001C1C RID: 7196 RVA: 0x00069248 File Offset: 0x00067448
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.isLongReference = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(object));
			this.AllocateHandle(value);
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referenced by the current <see cref="T:System.WeakReference" /> object has not been garbage collected and is still accessible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00069297 File Offset: 0x00067497
		public virtual bool IsAlive
		{
			get
			{
				return this.Target != null;
			}
		}

		/// <summary>Gets or sets the object (the target) referenced by the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <returns>
		///   <see langword="null" /> if the object referenced by the current <see cref="T:System.WeakReference" /> object has been garbage collected; otherwise, a reference to the object referenced by the current <see cref="T:System.WeakReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reference to the target object is invalid. This exception can be thrown while setting this property if the value is a null reference or if the object has been finalized during the set operation.</exception>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x000692A2 File Offset: 0x000674A2
		// (set) Token: 0x06001C1F RID: 7199 RVA: 0x000692BE File Offset: 0x000674BE
		public virtual object Target
		{
			get
			{
				if (!this.gcHandle.IsAllocated)
				{
					return null;
				}
				return this.gcHandle.Target;
			}
			set
			{
				this.gcHandle.Target = value;
			}
		}

		/// <summary>Gets an indication whether the object referenced by the current <see cref="T:System.WeakReference" /> object is tracked after it is finalized.</summary>
		/// <returns>
		///   <see langword="true" /> if the object the current <see cref="T:System.WeakReference" /> object refers to is tracked after finalization; or <see langword="false" /> if the object is only tracked until finalization.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000692CC File Offset: 0x000674CC
		public virtual bool TrackResurrection
		{
			get
			{
				return this.isLongReference;
			}
		}

		/// <summary>Discards the reference to the target represented by the current <see cref="T:System.WeakReference" /> object.</summary>
		// Token: 0x06001C21 RID: 7201 RVA: 0x000692D4 File Offset: 0x000674D4
		~WeakReference()
		{
			this.gcHandle.Free();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data needed to serialize the current <see cref="T:System.WeakReference" /> object.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize the current <see cref="T:System.WeakReference" /> object.</param>
		/// <param name="context">(Reserved) The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06001C22 RID: 7202 RVA: 0x00069308 File Offset: 0x00067508
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.TrackResurrection);
			try
			{
				info.AddValue("TrackedObject", this.Target);
			}
			catch (Exception)
			{
				info.AddValue("TrackedObject", null);
			}
		}

		// Token: 0x040019BC RID: 6588
		private bool isLongReference;

		// Token: 0x040019BD RID: 6589
		private GCHandle gcHandle;
	}
}
