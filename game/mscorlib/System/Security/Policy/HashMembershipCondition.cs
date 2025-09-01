using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its hash value. This class cannot be inherited.</summary>
	// Token: 0x02000413 RID: 1043
	[ComVisible(true)]
	[Serializable]
	public sealed class HashMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IDeserializationCallback, ISerializable
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x0009A3D7 File Offset: 0x000985D7
		internal HashMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.HashMembershipCondition" /> class with the hash algorithm and hash value that determine membership.</summary>
		/// <param name="hashAlg">The hash algorithm to use to compute the hash value for the assembly.</param>
		/// <param name="value">The hash value for which to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hashAlg" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="hashAlg" /> parameter is not a valid hash algorithm.</exception>
		// Token: 0x06002AAD RID: 10925 RVA: 0x0009A3E8 File Offset: 0x000985E8
		public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.hash_algorithm = hashAlg;
			this.hash_value = (byte[])value.Clone();
		}

		/// <summary>Gets or sets the hash algorithm to use for the membership condition.</summary>
		/// <returns>The hash algorithm to use for the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> to <see langword="null" />.</exception>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x0009A436 File Offset: 0x00098636
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x0009A451 File Offset: 0x00098651
		public HashAlgorithm HashAlgorithm
		{
			get
			{
				if (this.hash_algorithm == null)
				{
					this.hash_algorithm = new SHA1Managed();
				}
				return this.hash_algorithm;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashAlgorithm");
				}
				this.hash_algorithm = value;
			}
		}

		/// <summary>Gets or sets the hash value for which the membership condition tests.</summary>
		/// <returns>The hash value for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> to <see langword="null" />.</exception>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x0009A468 File Offset: 0x00098668
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x0009A492 File Offset: 0x00098692
		public byte[] HashValue
		{
			get
			{
				if (this.hash_value == null)
				{
					throw new ArgumentException(Locale.GetText("No HashValue available."));
				}
				return (byte[])this.hash_value.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashValue");
				}
				this.hash_value = (byte[])value.Clone();
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The evidence set against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AB2 RID: 10930 RVA: 0x0009A4B4 File Offset: 0x000986B4
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Hash hash = obj as Hash;
				if (hash != null)
				{
					if (this.Compare(this.hash_value, hash.GenerateHash(this.hash_algorithm)))
					{
						return true;
					}
					break;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06002AB3 RID: 10931 RVA: 0x0009A503 File Offset: 0x00098703
		public IMembershipCondition Copy()
		{
			return new HashMembershipCondition(this.hash_algorithm, this.hash_value);
		}

		/// <summary>Determines whether the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and the <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> from the specified object are equivalent to the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> contained in the current <see cref="T:System.Security.Policy.HashMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.HashMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> from the specified object is equivalent to the <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> and <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> contained in the current <see cref="T:System.Security.Policy.HashMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AB4 RID: 10932 RVA: 0x0009A518 File Offset: 0x00098718
		public override bool Equals(object o)
		{
			HashMembershipCondition hashMembershipCondition = o as HashMembershipCondition;
			return hashMembershipCondition != null && hashMembershipCondition.HashAlgorithm == this.hash_algorithm && this.Compare(this.hash_value, hashMembershipCondition.hash_value);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002AB5 RID: 10933 RVA: 0x0009A553 File Offset: 0x00098753
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The policy level context for resolving named permission set references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002AB6 RID: 10934 RVA: 0x0009A55C File Offset: 0x0009875C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(HashMembershipCondition), this.version);
			securityElement.AddAttribute("HashValue", CryptoConvert.ToHex(this.HashValue));
			securityElement.AddAttribute("HashAlgorithm", this.hash_algorithm.GetType().FullName);
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06002AB7 RID: 10935 RVA: 0x0009A5AF File Offset: 0x000987AF
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level context, used to resolve named permission set references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002AB8 RID: 10936 RVA: 0x0009A5BC File Offset: 0x000987BC
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.hash_value = CryptoConvert.FromHex(e.Attribute("HashValue"));
			string text = e.Attribute("HashAlgorithm");
			this.hash_algorithm = ((text == null) ? null : HashAlgorithm.Create(text));
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06002AB9 RID: 10937 RVA: 0x0009A618 File Offset: 0x00098818
		public override int GetHashCode()
		{
			int num = this.hash_algorithm.GetType().GetHashCode();
			if (this.hash_value != null)
			{
				foreach (byte b in this.hash_value)
				{
					num ^= (int)b;
				}
			}
			return num;
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		// Token: 0x06002ABA RID: 10938 RVA: 0x0009A65C File Offset: 0x0009885C
		public override string ToString()
		{
			Type type = this.HashAlgorithm.GetType();
			return string.Format("Hash - {0} {1} = {2}", type.FullName, type.Assembly, CryptoConvert.ToHex(this.HashValue));
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0009A698 File Offset: 0x00098898
		private bool Compare(byte[] expected, byte[] actual)
		{
			if (expected.Length != actual.Length)
			{
				return false;
			}
			int num = expected.Length;
			for (int i = 0; i < num; i++)
			{
				if (expected[i] != actual[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06002ABC RID: 10940 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MonoTODO("fx 2.0")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination <see cref="T:System.Runtime.Serialization.StreamingContext" /> for this serialization.</param>
		// Token: 0x06002ABD RID: 10941 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MonoTODO("fx 2.0")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x04001F9C RID: 8092
		private readonly int version = 1;

		// Token: 0x04001F9D RID: 8093
		private HashAlgorithm hash_algorithm;

		// Token: 0x04001F9E RID: 8094
		private byte[] hash_value;
	}
}
