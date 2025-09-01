using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the exception thrown when a component cannot be granted a license.</summary>
	// Token: 0x02000418 RID: 1048
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class LicenseException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type of component that was denied a license.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		// Token: 0x060021CE RID: 8654 RVA: 0x00073AFD File Offset: 0x00071CFD
		public LicenseException(Type type) : this(type, null, SR.GetString("A valid license cannot be granted for the type {0}. Contact the manufacturer of the component for more information.", new object[]
		{
			type.FullName
		}))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		// Token: 0x060021CF RID: 8655 RVA: 0x00073B20 File Offset: 0x00071D20
		public LicenseException(Type type, object instance) : this(type, null, SR.GetString("An instance of type '{1}' was being created, and a valid license could not be granted for the type '{0}'. Please,  contact the manufacturer of the component for more information.", new object[]
		{
			type.FullName,
			instance.GetType().FullName
		}))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		/// <param name="message">The exception message to display.</param>
		// Token: 0x060021D0 RID: 8656 RVA: 0x00073B51 File Offset: 0x00071D51
		public LicenseException(Type type, object instance, string message) : base(message)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class for the type and the instance of the component that was denied a license, along with a message to display and the original exception thrown.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</param>
		/// <param name="instance">The instance of the component that was not granted a license.</param>
		/// <param name="message">The exception message to display.</param>
		/// <param name="innerException">An <see cref="T:System.Exception" /> that represents the original exception.</param>
		// Token: 0x060021D1 RID: 8657 RVA: 0x00073B73 File Offset: 0x00071D73
		public LicenseException(Type type, object instance, string message, Exception innerException) : base(message, innerException)
		{
			this.type = type;
			this.instance = instance;
			base.HResult = -2146232063;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060021D2 RID: 8658 RVA: 0x00073B98 File Offset: 0x00071D98
		protected LicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (Type)info.GetValue("type", typeof(Type));
			this.instance = info.GetValue("instance", typeof(object));
		}

		/// <summary>Gets the type of the component that was not granted a license.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component that was not granted a license.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x00073BE8 File Offset: 0x00071DE8
		public Type LicensedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060021D4 RID: 8660 RVA: 0x00073BF0 File Offset: 0x00071DF0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("type", this.type);
			info.AddValue("instance", this.instance);
			base.GetObjectData(info, context);
		}

		// Token: 0x04001029 RID: 4137
		private Type type;

		// Token: 0x0400102A RID: 4138
		private object instance;
	}
}
