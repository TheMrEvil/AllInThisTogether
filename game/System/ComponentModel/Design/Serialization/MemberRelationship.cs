using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents a single relationship between an object and a member.</summary>
	// Token: 0x0200048F RID: 1167
	public readonly struct MemberRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> class.</summary>
		/// <param name="owner">The object that owns <paramref name="member" />.</param>
		/// <param name="member">The member which is to be related to <paramref name="owner" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> or <paramref name="member" /> is <see langword="null" />.</exception>
		// Token: 0x06002546 RID: 9542 RVA: 0x0008318E File Offset: 0x0008138E
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.Owner = owner;
			this.Member = member;
		}

		/// <summary>Gets a value indicating whether this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship.</summary>
		/// <returns>
		///   <see langword="true" /> if this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000831BA File Offset: 0x000813BA
		public bool IsEmpty
		{
			get
			{
				return this.Owner == null;
			}
		}

		/// <summary>Gets the related member.</summary>
		/// <returns>The member that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x000831C5 File Offset: 0x000813C5
		public MemberDescriptor Member
		{
			[CompilerGenerated]
			get
			{
				return this.<Member>k__BackingField;
			}
		}

		/// <summary>Gets the owning object.</summary>
		/// <returns>The owning object that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000831CD File Offset: 0x000813CD
		public object Owner
		{
			[CompilerGenerated]
			get
			{
				return this.<Owner>k__BackingField;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> to compare with the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> is equal to the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600254A RID: 9546 RVA: 0x000831D8 File Offset: 0x000813D8
		public override bool Equals(object obj)
		{
			if (!(obj is MemberRelationship))
			{
				return false;
			}
			MemberRelationship memberRelationship = (MemberRelationship)obj;
			return memberRelationship.Owner == this.Owner && memberRelationship.Member == this.Member;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</returns>
		// Token: 0x0600254B RID: 9547 RVA: 0x00083216 File Offset: 0x00081416
		public override int GetHashCode()
		{
			if (this.Owner == null)
			{
				return base.GetHashCode();
			}
			return this.Owner.GetHashCode() ^ this.Member.GetHashCode();
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equivalent.</summary>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the equality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600254C RID: 9548 RVA: 0x00083248 File Offset: 0x00081448
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different.</summary>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the inequality operator.</param>
		/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600254D RID: 9549 RVA: 0x0008326C File Offset: 0x0008146C
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00003917 File Offset: 0x00001B17
		// Note: this type is marked as 'beforefieldinit'.
		static MemberRelationship()
		{
		}

		/// <summary>Represents the empty member relationship. This field is read-only.</summary>
		// Token: 0x0400149E RID: 5278
		public static readonly MemberRelationship Empty;

		// Token: 0x0400149F RID: 5279
		[CompilerGenerated]
		private readonly MemberDescriptor <Member>k__BackingField;

		// Token: 0x040014A0 RID: 5280
		[CompilerGenerated]
		private readonly object <Owner>k__BackingField;
	}
}
