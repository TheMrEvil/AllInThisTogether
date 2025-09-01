using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	/// <summary>Represents an exception for a principal whose identity could not be mapped to a known identity.</summary>
	// Token: 0x020004E3 RID: 1251
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class.</summary>
		// Token: 0x060031ED RID: 12781 RVA: 0x000B7B6B File Offset: 0x000B5D6B
		public IdentityNotMappedException() : base(Locale.GetText("Couldn't translate some identities."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060031EE RID: 12782 RVA: 0x0006E6A5 File Offset: 0x0006C8A5
		public IdentityNotMappedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message and inner exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If <paramref name="inner" /> is not null, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060031EF RID: 12783 RVA: 0x0006E6AE File Offset: 0x0006C8AE
		public IdentityNotMappedException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Represents the collection of unmapped identities for an <see cref="T:System.Security.Principal.IdentityNotMappedException" /> exception.</summary>
		/// <returns>The collection of unmapped identities.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000B7B7D File Offset: 0x000B5D7D
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this._coll == null)
				{
					this._coll = new IdentityReferenceCollection();
				}
				return this._coll;
			}
		}

		/// <summary>Gets serialization information with the data needed to create an instance of this <see cref="T:System.Security.Principal.IdentityNotMappedException" /> object.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The object that contains contextual information about the source or destination.</param>
		// Token: 0x060031F1 RID: 12785 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		[MonoTODO("not implemented")]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x040022BF RID: 8895
		private IdentityReferenceCollection _coll;
	}
}
