using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class represents the extended protection policy used by the server to validate incoming client connections.</summary>
	// Token: 0x020002A6 RID: 678
	[MonoTODO]
	[TypeConverter(typeof(ExtendedProtectionPolicyTypeConverter))]
	[Serializable]
	public class ExtendedProtectionPolicy : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		// Token: 0x06001530 RID: 5424 RVA: 0x0000219B File Offset: 0x0000039B
		[MonoTODO("Not implemented.")]
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced and the channel binding token (CBT) to be used.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="customChannelBinding">A <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that contains a custom channel binding to use for validation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customChannelBinding" /> is <see langword="null" />.</exception>
		// Token: 0x06001531 RID: 5425 RVA: 0x00055ABD File Offset: 0x00053CBD
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ChannelBinding customChannelBinding)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced, the kind of protection enforced by the policy, and a custom Service Provider Name (SPN) list that is used to match against a client's SPN.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="protectionScenario">A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</param>
		/// <param name="customServiceNames">A <see cref="T:System.Collections.ICollection" /> that contains the custom SPN list that is used to match against a client's SPN.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customServiceNames" /> is <see langword="null" /> or an empty list.</exception>
		// Token: 0x06001532 RID: 5426 RVA: 0x00055ABD File Offset: 0x00053CBD
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ICollection customServiceNames)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class that specifies when the extended protection policy should be enforced, the kind of protection enforced by the policy, and a custom Service Provider Name (SPN) list that is used to match against a client's SPN.</summary>
		/// <param name="policyEnforcement">A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</param>
		/// <param name="protectionScenario">A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</param>
		/// <param name="customServiceNames">A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains the custom SPN list that is used to match against a client's SPN.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="policyEnforcement" /> is specified as <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Never" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customServiceNames" /> is <see langword="null" /> or an empty list.</exception>
		// Token: 0x06001533 RID: 5427 RVA: 0x00055ABD File Offset: 0x00053CBD
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ServiceNameCollection customServiceNames)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> class from a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the required data to populate the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</param>
		// Token: 0x06001534 RID: 5428 RVA: 0x00055ABD File Offset: 0x00053CBD
		protected ExtendedProtectionPolicy(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a custom channel binding token (CBT) to use for validation.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> that contains a custom channel binding to use for validation.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0000829A File Offset: 0x0000649A
		public ChannelBinding CustomChannelBinding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the custom Service Provider Name (SPN) list used to match against a client's SPN.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains the custom SPN list that is used to match against a client's SPN.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0000829A File Offset: 0x0000649A
		public ServiceNameCollection CustomServiceNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Indicates whether the operating system supports integrated windows authentication with extended protection.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system supports integrated windows authentication with extended protection, otherwise <see langword="false" />.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0000829A File Offset: 0x0000649A
		public static bool OSSupportsExtendedProtection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets when the extended protection policy should be enforced.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0000829A File Offset: 0x0000649A
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the kind of protection enforced by the extended protection policy.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0000829A File Offset: 0x0000649A
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string representation for the extended protection policy instance.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the representation of the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance.</returns>
		// Token: 0x0600153A RID: 5434 RVA: 0x00055ACA File Offset: 0x00053CCA
		[MonoTODO]
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized data for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</param>
		// Token: 0x0600153B RID: 5435 RVA: 0x0000829A File Offset: 0x0000649A
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}
