using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	/// <summary>The exception that is thrown when a method in the <see cref="N:System.Security.AccessControl" /> namespace attempts to enable a privilege that it does not have.</summary>
	// Token: 0x020004FE RID: 1278
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class.</summary>
		// Token: 0x06003335 RID: 13109 RVA: 0x000BC748 File Offset: 0x000BA948
		public PrivilegeNotHeldException() : base("The process does not possess some privilege required for this operation.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified privilege.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		// Token: 0x06003336 RID: 13110 RVA: 0x000BC755 File Offset: 0x000BA955
		public PrivilegeNotHeldException(string privilege) : base(string.Format(CultureInfo.CurrentCulture, "The process does not possess the '{0}' privilege which is required for this operation.", privilege))
		{
			this._privilegeName = privilege;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified exception.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the innerException parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003337 RID: 13111 RVA: 0x000BC774 File Offset: 0x000BA974
		public PrivilegeNotHeldException(string privilege, Exception inner) : base(string.Format(CultureInfo.CurrentCulture, "The process does not possess the '{0}' privilege which is required for this operation.", privilege), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000BC794 File Offset: 0x000BA994
		private PrivilegeNotHeldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		/// <summary>Sets the <paramref name="info" /> parameter with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06003339 RID: 13113 RVA: 0x000BC7AF File Offset: 0x000BA9AF
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		/// <summary>Gets the name of the privilege that is not enabled.</summary>
		/// <returns>The name of the privilege that the method failed to enable.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000BC7D4 File Offset: 0x000BA9D4
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		// Token: 0x040023F8 RID: 9208
		private readonly string _privilegeName;
	}
}
