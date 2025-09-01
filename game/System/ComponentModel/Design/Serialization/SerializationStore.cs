using System;
using System.Collections;
using System.IO;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for storing serialization data for the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />.</summary>
	// Token: 0x02000493 RID: 1171
	public abstract class SerializationStore : IDisposable
	{
		/// <summary>Gets a collection of errors that occurred during serialization or deserialization.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains errors that occurred during serialization or deserialization.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600255E RID: 9566
		public abstract ICollection Errors { get; }

		/// <summary>Closes the serialization store.</summary>
		// Token: 0x0600255F RID: 9567
		public abstract void Close();

		/// <summary>Saves the store to the given stream.</summary>
		/// <param name="stream">The stream to which the store will be serialized.</param>
		// Token: 0x06002560 RID: 9568
		public abstract void Save(Stream stream);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		// Token: 0x06002561 RID: 9569 RVA: 0x00083377 File Offset: 0x00081577
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002562 RID: 9570 RVA: 0x00083380 File Offset: 0x00081580
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> class.</summary>
		// Token: 0x06002563 RID: 9571 RVA: 0x0000219B File Offset: 0x0000039B
		protected SerializationStore()
		{
		}
	}
}
