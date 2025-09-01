using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x0200050F RID: 1295
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x06003365 RID: 13157 RVA: 0x000BCCBC File Offset: 0x000BAEBC
		internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			if (rawAcl == null)
			{
				rawAcl = new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 10);
			}
			else
			{
				byte[] binaryForm = new byte[rawAcl.BinaryLength];
				rawAcl.GetBinaryForm(binaryForm, 0);
				rawAcl = new RawAcl(binaryForm, 0);
			}
			this.Init(isContainer, isDS, rawAcl);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000BCD12 File Offset: 0x000BAF12
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this.Init(isContainer, isDS, new RawAcl(revision, capacity));
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000BCD2A File Offset: 0x000BAF2A
		internal CommonAcl(bool isContainer, bool isDS, int capacity) : this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000BCD44 File Offset: 0x000BAF44
		private void Init(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.raw_acl = rawAcl;
			this.CanonicalizeAndClearAefa();
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object. This length should be used before marshaling the access control list (ACL) into a binary array by using the <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000BCD61 File Offset: 0x000BAF61
		public sealed override int BinaryLength
		{
			get
			{
				return this.raw_acl.BinaryLength;
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000BCD6E File Offset: 0x000BAF6E
		public sealed override int Count
		{
			get
			{
				return this.raw_acl.Count;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order.</summary>
		/// <returns>
		///   <see langword="true" /> if the ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000BCD7B File Offset: 0x000BAF7B
		public bool IsCanonical
		{
			get
			{
				return this.is_canonical;
			}
		}

		/// <summary>Sets whether the <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000BCD83 File Offset: 0x000BAF83
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Sets whether the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object access control list (ACL).</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object ACL.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000BCD8B File Offset: 0x000BAF8B
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000BCD93 File Offset: 0x000BAF93
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000BCD9B File Offset: 0x000BAF9B
		internal bool IsAefa
		{
			get
			{
				return this.is_aefa;
			}
			set
			{
				this.is_aefa = value;
			}
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</returns>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000BCDA4 File Offset: 0x000BAFA4
		public sealed override byte Revision
		{
			get
			{
				return this.raw_acl.Revision;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.CommonAce" /> to get or set.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</returns>
		// Token: 0x17000701 RID: 1793
		public sealed override GenericAce this[int index]
		{
			get
			{
				return CommonAcl.CopyAce(this.raw_acl[index]);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		// Token: 0x06003373 RID: 13171 RVA: 0x000BCDC4 File Offset: 0x000BAFC4
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.raw_acl.GetBinaryForm(binaryForm, offset);
		}

		/// <summary>Removes all access control entries (ACEs) contained by this <see cref="T:System.Security.AccessControl.CommonAcl" /> object that are associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object to check for.</param>
		// Token: 0x06003374 RID: 13172 RVA: 0x000BCDD4 File Offset: 0x000BAFD4
		public void Purge(SecurityIdentifier sid)
		{
			this.RequireCanonicity();
			this.RemoveAces<KnownAce>((KnownAce ace) => ace.SecurityIdentifier == sid);
		}

		/// <summary>Removes all inherited access control entries (ACEs) from this <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		// Token: 0x06003375 RID: 13173 RVA: 0x000BCE06 File Offset: 0x000BB006
		public void RemoveInheritedAces()
		{
			this.RequireCanonicity();
			this.RemoveAces<GenericAce>((GenericAce ace) => ace.IsInherited);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000BCE33 File Offset: 0x000BB033
		internal void RequireCanonicity()
		{
			if (!this.IsCanonical)
			{
				throw new InvalidOperationException("ACL is not canonical.");
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000BCE48 File Offset: 0x000BB048
		internal void CanonicalizeAndClearAefa()
		{
			this.RemoveAces<GenericAce>(new CommonAcl.RemoveAcesCallback<GenericAce>(this.IsAceMeaningless));
			this.is_canonical = this.TestCanonicity();
			if (this.IsCanonical)
			{
				this.ApplyCanonicalSortToExplicitAces();
				this.MergeExplicitAces();
			}
			this.IsAefa = false;
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000BCE84 File Offset: 0x000BB084
		internal virtual bool IsAceMeaningless(GenericAce ace)
		{
			AceFlags aceFlags = ace.AceFlags;
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null)
			{
				if (knownAce.AccessMask == 0)
				{
					return true;
				}
				if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					if (knownAce is ObjectAce)
					{
						return true;
					}
					if (!this.IsContainer)
					{
						return true;
					}
					if ((aceFlags & (AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == AceFlags.None)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000BCED4 File Offset: 0x000BB0D4
		private bool TestCanonicity()
		{
			AceEnumerator enumerator = base.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!(enumerator.Current is QualifiedAce))
				{
					return false;
				}
			}
			bool flag = false;
			enumerator = base.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((QualifiedAce)enumerator.Current).IsInherited)
				{
					flag = true;
				}
				else if (flag)
				{
					return false;
				}
			}
			bool flag2 = false;
			foreach (GenericAce genericAce in this)
			{
				QualifiedAce qualifiedAce = (QualifiedAce)genericAce;
				if (qualifiedAce.IsInherited)
				{
					break;
				}
				if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
				{
					flag2 = true;
				}
				else if (AceQualifier.AccessDenied == qualifiedAce.AceQualifier && flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000BCF74 File Offset: 0x000BB174
		internal int GetCanonicalExplicitDenyAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				QualifiedAce qualifiedAce = this.raw_acl[num] as QualifiedAce;
				if (qualifiedAce == null || qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000BCFCC File Offset: 0x000BB1CC
		internal int GetCanonicalExplicitAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000BD000 File Offset: 0x000BB200
		private void MergeExplicitAces()
		{
			int num = this.GetCanonicalExplicitAceCount();
			int i = 0;
			while (i < num - 1)
			{
				GenericAce genericAce = this.MergeExplicitAcePair(this.raw_acl[i], this.raw_acl[i + 1]);
				if (null != genericAce)
				{
					this.raw_acl[i] = genericAce;
					this.raw_acl.RemoveAce(i + 1);
					num--;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000BD070 File Offset: 0x000BB270
		private GenericAce MergeExplicitAcePair(GenericAce ace1, GenericAce ace2)
		{
			QualifiedAce qualifiedAce = ace1 as QualifiedAce;
			QualifiedAce qualifiedAce2 = ace2 as QualifiedAce;
			if (!(null != qualifiedAce) || !(null != qualifiedAce2))
			{
				return null;
			}
			if (qualifiedAce.AceQualifier != qualifiedAce2.AceQualifier)
			{
				return null;
			}
			if (!(qualifiedAce.SecurityIdentifier == qualifiedAce2.SecurityIdentifier))
			{
				return null;
			}
			AceFlags aceFlags = qualifiedAce.AceFlags;
			AceFlags aceFlags2 = qualifiedAce2.AceFlags;
			int accessMask = qualifiedAce.AccessMask;
			int accessMask2 = qualifiedAce2.AccessMask;
			if (!this.IsContainer)
			{
				aceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
				aceFlags2 &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
			}
			AceFlags aceFlags3;
			int accessMask3;
			if (aceFlags != aceFlags2)
			{
				if (accessMask != accessMask2)
				{
					return null;
				}
				if ((aceFlags & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == (aceFlags2 & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)))
				{
					aceFlags3 = (aceFlags | aceFlags2);
					accessMask3 = accessMask;
				}
				else
				{
					if ((aceFlags & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)) != (aceFlags2 & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)))
					{
						return null;
					}
					aceFlags3 = (aceFlags | aceFlags2);
					accessMask3 = accessMask;
				}
			}
			else
			{
				aceFlags3 = aceFlags;
				accessMask3 = (accessMask | accessMask2);
			}
			CommonAce commonAce = ace1 as CommonAce;
			CommonAce right = ace2 as CommonAce;
			if (null != commonAce && null != right)
			{
				return new CommonAce(aceFlags3, commonAce.AceQualifier, accessMask3, commonAce.SecurityIdentifier, commonAce.IsCallback, commonAce.GetOpaque());
			}
			ObjectAce objectAce = ace1 as ObjectAce;
			ObjectAce objectAce2 = ace2 as ObjectAce;
			if (null != objectAce && null != objectAce2)
			{
				Guid a;
				Guid a2;
				CommonAcl.GetObjectAceTypeGuids(objectAce, out a, out a2);
				Guid b;
				Guid b2;
				CommonAcl.GetObjectAceTypeGuids(objectAce2, out b, out b2);
				if (a == b && a2 == b2)
				{
					return new ObjectAce(aceFlags3, objectAce.AceQualifier, accessMask3, objectAce.SecurityIdentifier, objectAce.ObjectAceFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType, objectAce.IsCallback, objectAce.GetOpaque());
				}
			}
			return null;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000BD218 File Offset: 0x000BB418
		private static void GetObjectAceTypeGuids(ObjectAce ace, out Guid type, out Guid inheritedType)
		{
			type = Guid.Empty;
			inheritedType = Guid.Empty;
			if ((ace.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				type = ace.ObjectAceType;
			}
			if ((ace.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				inheritedType = ace.InheritedObjectAceType;
			}
		}

		// Token: 0x0600337F RID: 13183
		internal abstract void ApplyCanonicalSortToExplicitAces();

		// Token: 0x06003380 RID: 13184 RVA: 0x000BD268 File Offset: 0x000BB468
		internal void ApplyCanonicalSortToExplicitAces(int start, int count)
		{
			for (int i = start + 1; i < start + count; i++)
			{
				KnownAce knownAce = (KnownAce)this.raw_acl[i];
				SecurityIdentifier securityIdentifier = knownAce.SecurityIdentifier;
				int num = i;
				while (num > start && ((KnownAce)this.raw_acl[num - 1]).SecurityIdentifier.CompareTo(securityIdentifier) > 0)
				{
					this.raw_acl[num] = this.raw_acl[num - 1];
					num--;
				}
				this.raw_acl[num] = knownAce;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000BD2F2 File Offset: 0x000BB4F2
		internal override string GetSddlForm(ControlFlags sdFlags, bool isDacl)
		{
			return this.raw_acl.GetSddlForm(sdFlags, isDacl);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000BD304 File Offset: 0x000BB504
		internal void RemoveAces<T>(CommonAcl.RemoveAcesCallback<T> callback) where T : GenericAce
		{
			int i = 0;
			while (i < this.raw_acl.Count)
			{
				if (this.raw_acl[i] is T && callback((T)((object)this.raw_acl[i])))
				{
					this.raw_acl.RemoveAce(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000BD364 File Offset: 0x000BB564
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce newAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.AddAce(newAce);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000BD388 File Offset: 0x000BB588
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce newAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.AddAce(newAce);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				return this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			}
			return new ObjectAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, null);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000BD404 File Offset: 0x000BB604
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			return new CommonAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, false, null);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000BD41C File Offset: 0x000BB61C
		private void AddAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			int aceInsertPosition = this.GetAceInsertPosition(newAce.AceQualifier);
			this.raw_acl.InsertAce(aceInsertPosition, CommonAcl.CopyAce(newAce));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000BD454 File Offset: 0x000BB654
		private static GenericAce CopyAce(GenericAce ace)
		{
			byte[] binaryForm = new byte[ace.BinaryLength];
			ace.GetBinaryForm(binaryForm, 0);
			return GenericAce.CreateFromBinaryForm(binaryForm, 0);
		}

		// Token: 0x06003389 RID: 13193
		internal abstract int GetAceInsertPosition(AceQualifier aceQualifier);

		// Token: 0x0600338A RID: 13194 RVA: 0x000BD47C File Offset: 0x000BB67C
		private AceFlags GetAceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			if (inheritanceFlags != InheritanceFlags.None && !this.IsContainer)
			{
				throw new ArgumentException("Flags only work with containers.", "inheritanceFlags");
			}
			if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("Propagation flags need inheritance flags.", "propagationFlags");
			}
			AceFlags aceFlags = AceFlags.None;
			if ((InheritanceFlags.ContainerInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((InheritanceFlags.ObjectInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if ((PropagationFlags.InheritOnly & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.InheritOnly;
			}
			if ((PropagationFlags.NoPropagateInherit & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.NoPropagateInherit;
			}
			if ((AuditFlags.Success & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((AuditFlags.Failure & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			return aceFlags;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000BD4F8 File Offset: 0x000BB6F8
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			this.RequireCanonicity();
			this.RemoveAces<CommonAce>((CommonAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags);
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000BD558 File Offset: 0x000BB758
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				this.RemoveAceSpecific(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
				return;
			}
			this.RequireCanonicity();
			this.RemoveAces<ObjectAce>((ObjectAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags && ace.ObjectAceFlags == objectFlags && ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || !(ace.ObjectAceType != objectType)) && ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || !(ace.InheritedObjectAceType != objectType)));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000BD60C File Offset: 0x000BB80C
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce ace = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.SetAce(ace);
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000BD630 File Offset: 0x000BB830
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce ace = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.SetAce(ace);
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000BD65C File Offset: 0x000BB85C
		private void SetAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			this.RemoveAces<QualifiedAce>((QualifiedAce oldAce) => oldAce.AceQualifier == newAce.AceQualifier && oldAce.SecurityIdentifier == newAce.SecurityIdentifier);
			this.CanonicalizeAndClearAefa();
			this.AddAce(newAce);
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000173AD File Offset: 0x000155AD
		internal CommonAcl()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400243E RID: 9278
		private const int default_capacity = 10;

		// Token: 0x0400243F RID: 9279
		private bool is_aefa;

		// Token: 0x04002440 RID: 9280
		private bool is_canonical;

		// Token: 0x04002441 RID: 9281
		private bool is_container;

		// Token: 0x04002442 RID: 9282
		private bool is_ds;

		// Token: 0x04002443 RID: 9283
		internal RawAcl raw_acl;

		// Token: 0x02000510 RID: 1296
		// (Invoke) Token: 0x06003392 RID: 13202
		internal delegate bool RemoveAcesCallback<T>(T ace);

		// Token: 0x02000511 RID: 1297
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x06003395 RID: 13205 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06003396 RID: 13206 RVA: 0x000BD6A0 File Offset: 0x000BB8A0
			internal bool <Purge>b__0(KnownAce ace)
			{
				return ace.SecurityIdentifier == this.sid;
			}

			// Token: 0x04002444 RID: 9284
			public SecurityIdentifier sid;
		}

		// Token: 0x02000512 RID: 1298
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003397 RID: 13207 RVA: 0x000BD6B3 File Offset: 0x000BB8B3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003398 RID: 13208 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06003399 RID: 13209 RVA: 0x000BD6BF File Offset: 0x000BB8BF
			internal bool <RemoveInheritedAces>b__31_0(GenericAce ace)
			{
				return ace.IsInherited;
			}

			// Token: 0x04002445 RID: 9285
			public static readonly CommonAcl.<>c <>9 = new CommonAcl.<>c();

			// Token: 0x04002446 RID: 9286
			public static CommonAcl.RemoveAcesCallback<GenericAce> <>9__31_0;
		}

		// Token: 0x02000513 RID: 1299
		[CompilerGenerated]
		private sealed class <>c__DisplayClass53_0
		{
			// Token: 0x0600339A RID: 13210 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass53_0()
			{
			}

			// Token: 0x0600339B RID: 13211 RVA: 0x000BD6C8 File Offset: 0x000BB8C8
			internal bool <RemoveAceSpecific>b__0(CommonAce ace)
			{
				return ace.AccessMask == this.accessMask && ace.AceQualifier == this.aceQualifier && !(ace.SecurityIdentifier != this.sid) && ace.InheritanceFlags == this.inheritanceFlags && (this.inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == this.propagationFlags) && ace.AuditFlags == this.auditFlags;
			}

			// Token: 0x04002447 RID: 9287
			public int accessMask;

			// Token: 0x04002448 RID: 9288
			public AceQualifier aceQualifier;

			// Token: 0x04002449 RID: 9289
			public SecurityIdentifier sid;

			// Token: 0x0400244A RID: 9290
			public InheritanceFlags inheritanceFlags;

			// Token: 0x0400244B RID: 9291
			public PropagationFlags propagationFlags;

			// Token: 0x0400244C RID: 9292
			public AuditFlags auditFlags;
		}

		// Token: 0x02000514 RID: 1300
		[CompilerGenerated]
		private sealed class <>c__DisplayClass54_0
		{
			// Token: 0x0600339C RID: 13212 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass54_0()
			{
			}

			// Token: 0x0600339D RID: 13213 RVA: 0x000BD744 File Offset: 0x000BB944
			internal bool <RemoveAceSpecific>b__0(ObjectAce ace)
			{
				return ace.AccessMask == this.accessMask && ace.AceQualifier == this.aceQualifier && !(ace.SecurityIdentifier != this.sid) && ace.InheritanceFlags == this.inheritanceFlags && (this.inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == this.propagationFlags) && ace.AuditFlags == this.auditFlags && ace.ObjectAceFlags == this.objectFlags && ((this.objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || !(ace.ObjectAceType != this.objectType)) && ((this.objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || !(ace.InheritedObjectAceType != this.objectType));
			}

			// Token: 0x0400244D RID: 9293
			public int accessMask;

			// Token: 0x0400244E RID: 9294
			public AceQualifier aceQualifier;

			// Token: 0x0400244F RID: 9295
			public SecurityIdentifier sid;

			// Token: 0x04002450 RID: 9296
			public InheritanceFlags inheritanceFlags;

			// Token: 0x04002451 RID: 9297
			public PropagationFlags propagationFlags;

			// Token: 0x04002452 RID: 9298
			public AuditFlags auditFlags;

			// Token: 0x04002453 RID: 9299
			public ObjectAceFlags objectFlags;

			// Token: 0x04002454 RID: 9300
			public Guid objectType;
		}

		// Token: 0x02000515 RID: 1301
		[CompilerGenerated]
		private sealed class <>c__DisplayClass57_0
		{
			// Token: 0x0600339E RID: 13214 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass57_0()
			{
			}

			// Token: 0x0600339F RID: 13215 RVA: 0x000BD80D File Offset: 0x000BBA0D
			internal bool <SetAce>b__0(QualifiedAce oldAce)
			{
				return oldAce.AceQualifier == this.newAce.AceQualifier && oldAce.SecurityIdentifier == this.newAce.SecurityIdentifier;
			}

			// Token: 0x04002455 RID: 9301
			public QualifiedAce newAce;
		}
	}
}
