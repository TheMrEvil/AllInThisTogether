using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A9 RID: 425
	public sealed class SerializationSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the serialization configuration section for the specified configuration.</summary>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents the configuration to retrieve.</param>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.SerializationSectionGroup" /> that represents the configuration section.</returns>
		// Token: 0x06001563 RID: 5475 RVA: 0x0005468B File Offset: 0x0005288B
		public static SerializationSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			return (SerializationSectionGroup)config.SectionGroups["system.runtime.serialization"];
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.Configuration.DataContractSerializerSection" /> used to set up the known types collection.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Configuration.DataContractSerializerSection" /> used for the serialization configuration section.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000546B0 File Offset: 0x000528B0
		public DataContractSerializerSection DataContractSerializer
		{
			get
			{
				return (DataContractSerializerSection)base.Sections["dataContractSerializer"];
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.Configuration.NetDataContractSerializerSection" /> used to set up the known types collection.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Configuration.NetDataContractSerializerSection" /> object.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000546C7 File Offset: 0x000528C7
		public NetDataContractSerializerSection NetDataContractSerializer
		{
			get
			{
				return (NetDataContractSerializerSection)base.Sections["netDataContractSerializer"];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.SerializationSectionGroup" /> class.</summary>
		// Token: 0x06001566 RID: 5478 RVA: 0x000546DE File Offset: 0x000528DE
		public SerializationSectionGroup()
		{
		}
	}
}
