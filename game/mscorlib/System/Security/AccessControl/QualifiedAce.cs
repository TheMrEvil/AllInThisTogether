using System;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that contains a qualifier. The qualifier, represented by an <see cref="T:System.Security.AccessControl.AceQualifier" /> object, specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms. The <see cref="T:System.Security.AccessControl.QualifiedAce" /> class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonAce" /> and <see cref="T:System.Security.AccessControl.ObjectAce" /> classes.</summary>
	// Token: 0x02000544 RID: 1348
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x0600355D RID: 13661 RVA: 0x000C0F28 File Offset: 0x000BF128
		internal QualifiedAce(AceType type, AceFlags flags, byte[] opaque) : base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000C0F39 File Offset: 0x000BF139
		internal QualifiedAce(byte[] binaryForm, int offset) : base(binaryForm, offset)
		{
		}

		/// <summary>Gets a value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</summary>
		/// <returns>A value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000C0F44 File Offset: 0x000BF144
		public AceQualifier AceQualifier
		{
			get
			{
				switch (base.AceType)
				{
				case AceType.AccessAllowed:
				case AceType.AccessAllowedCompound:
				case AceType.AccessAllowedObject:
				case AceType.AccessAllowedCallback:
				case AceType.AccessAllowedCallbackObject:
					return AceQualifier.AccessAllowed;
				case AceType.AccessDenied:
				case AceType.AccessDeniedObject:
				case AceType.AccessDeniedCallback:
				case AceType.AccessDeniedCallbackObject:
					return AceQualifier.AccessDenied;
				case AceType.SystemAudit:
				case AceType.SystemAuditObject:
				case AceType.SystemAuditCallback:
				case AceType.SystemAuditCallbackObject:
					return AceQualifier.SystemAudit;
				case AceType.SystemAlarm:
				case AceType.SystemAlarmObject:
				case AceType.SystemAlarmCallback:
				case AceType.SystemAlarmCallbackObject:
					return AceQualifier.SystemAlarm;
				default:
					throw new ArgumentException("Unrecognised ACE type: " + base.AceType.ToString());
				}
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data; otherwise, false.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000C0FD0 File Offset: 0x000BF1D0
		public bool IsCallback
		{
			get
			{
				return base.AceType == AceType.AccessAllowedCallback || base.AceType == AceType.AccessAllowedCallbackObject || base.AceType == AceType.AccessDeniedCallback || base.AceType == AceType.AccessDeniedCallbackObject || base.AceType == AceType.SystemAlarmCallback || base.AceType == AceType.SystemAlarmCallbackObject || base.AceType == AceType.SystemAuditCallback || base.AceType == AceType.SystemAuditCallbackObject;
			}
		}

		/// <summary>Gets the length of the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. This property is valid only for callback Access Control Entries (ACEs).</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000C102F File Offset: 0x000BF22F
		public int OpaqueLength
		{
			get
			{
				if (this.opaque == null)
				{
					return 0;
				}
				return this.opaque.Length;
			}
		}

		/// <summary>Returns the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <returns>An array of byte values that represents the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</returns>
		// Token: 0x06003562 RID: 13666 RVA: 0x000C1043 File Offset: 0x000BF243
		public byte[] GetOpaque()
		{
			if (this.opaque == null)
			{
				return null;
			}
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</param>
		// Token: 0x06003563 RID: 13667 RVA: 0x000C105F File Offset: 0x000BF25F
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000173AD File Offset: 0x000155AD
		internal QualifiedAce()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024D4 RID: 9428
		private byte[] opaque;
	}
}
