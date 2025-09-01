using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Carries error and status information within a SOAP message. This class cannot be inherited.</summary>
	// Token: 0x02000680 RID: 1664
	[ComVisible(true)]
	[SoapType(Embedded = true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class with default values.</summary>
		// Token: 0x06003DE2 RID: 15842 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapFault()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class, setting the properties to specified values.</summary>
		/// <param name="faultCode">The fault code for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault code identifies the type of the fault that occurred.</param>
		/// <param name="faultString">The fault string for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault string provides a human readable explanation of the fault.</param>
		/// <param name="faultActor">The URI of the object that generated the fault.</param>
		/// <param name="serverFault">The description of a common language runtime exception. This information is also present in the <see cref="P:System.Runtime.Serialization.Formatters.SoapFault.Detail" /> property.</param>
		// Token: 0x06003DE3 RID: 15843 RVA: 0x000D5A8E File Offset: 0x000D3C8E
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000D5AB4 File Offset: 0x000D3CB4
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data to serialize the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for the current serialization.</param>
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000D5B94 File Offset: 0x000D3D94
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		/// <summary>Gets or sets the fault code for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault code for this <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000D5C01 File Offset: 0x000D3E01
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x000D5C09 File Offset: 0x000D3E09
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		/// <summary>Gets or sets the fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000D5C12 File Offset: 0x000D3E12
		// (set) Token: 0x06003DE9 RID: 15849 RVA: 0x000D5C1A File Offset: 0x000D3E1A
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		/// <summary>Gets or sets the fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000D5C23 File Offset: 0x000D3E23
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x000D5C2B File Offset: 0x000D3E2B
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		/// <summary>Gets or sets additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>Additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000D5C34 File Offset: 0x000D3E34
		// (set) Token: 0x06003DED RID: 15853 RVA: 0x000D5C3C File Offset: 0x000D3E3C
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x040027AF RID: 10159
		private string faultCode;

		// Token: 0x040027B0 RID: 10160
		private string faultString;

		// Token: 0x040027B1 RID: 10161
		private string faultActor;

		// Token: 0x040027B2 RID: 10162
		[SoapField(Embedded = true)]
		private object detail;
	}
}
