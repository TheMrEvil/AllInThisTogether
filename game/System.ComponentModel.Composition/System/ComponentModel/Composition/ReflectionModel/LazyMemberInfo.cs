using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	/// <summary>Represents a <see cref="T:System.Reflection.MemberInfo" /> object that does not load assemblies or create objects until requested.</summary>
	// Token: 0x0200006B RID: 107
	public struct LazyMemberInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class, representing the specified member.</summary>
		/// <param name="member">The member to represent.</param>
		// Token: 0x060002AC RID: 684 RVA: 0x000086B8 File Offset: 0x000068B8
		public LazyMemberInfo(MemberInfo member)
		{
			Requires.NotNull<MemberInfo>(member, "member");
			LazyMemberInfo.EnsureSupportedMemberType(member.MemberType, "member");
			this._accessorsCreator = null;
			this._memberType = member.MemberType;
			MemberTypes memberType = this._memberType;
			if (memberType == MemberTypes.Event)
			{
				EventInfo eventInfo = (EventInfo)member;
				this._accessors = new MemberInfo[]
				{
					eventInfo.GetRaiseMethod(true),
					eventInfo.GetAddMethod(true),
					eventInfo.GetRemoveMethod(true)
				};
				return;
			}
			if (memberType == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				Assumes.NotNull<PropertyInfo>(propertyInfo);
				this._accessors = new MemberInfo[]
				{
					propertyInfo.GetGetMethod(true),
					propertyInfo.GetSetMethod(true)
				};
				return;
			}
			this._accessors = new MemberInfo[]
			{
				member
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class for a member of the specified type with the specified accessors.</summary>
		/// <param name="memberType">The type of the represented member.</param>
		/// <param name="accessors">An array of the accessors for the represented member.</param>
		/// <exception cref="T:System.ArgumentException">One or more of the objects in <paramref name="accessors" /> are not valid accessors for this member.</exception>
		// Token: 0x060002AD RID: 685 RVA: 0x00008774 File Offset: 0x00006974
		public LazyMemberInfo(MemberTypes memberType, params MemberInfo[] accessors)
		{
			LazyMemberInfo.EnsureSupportedMemberType(memberType, "memberType");
			Requires.NotNull<MemberInfo[]>(accessors, "accessors");
			string message;
			if (!LazyMemberInfo.AreAccessorsValid(memberType, accessors, out message))
			{
				throw new ArgumentException(message, "accessors");
			}
			this._memberType = memberType;
			this._accessors = accessors;
			this._accessorsCreator = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> class for a member of the specified type with the specified accessors.</summary>
		/// <param name="memberType">The type of the represented member.</param>
		/// <param name="accessorsCreator">A function whose return value is a collection of the accessors for the represented member.</param>
		// Token: 0x060002AE RID: 686 RVA: 0x000087C3 File Offset: 0x000069C3
		public LazyMemberInfo(MemberTypes memberType, Func<MemberInfo[]> accessorsCreator)
		{
			LazyMemberInfo.EnsureSupportedMemberType(memberType, "memberType");
			Requires.NotNull<Func<MemberInfo[]>>(accessorsCreator, "accessorsCreator");
			this._memberType = memberType;
			this._accessors = null;
			this._accessorsCreator = accessorsCreator;
		}

		/// <summary>Gets the type of the represented member.</summary>
		/// <returns>The type of the represented member.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000087F0 File Offset: 0x000069F0
		public MemberTypes MemberType
		{
			get
			{
				return this._memberType;
			}
		}

		/// <summary>Gets an array of the accessors for the represented member.</summary>
		/// <returns>An array of the accessors for the represented member.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the accessors in this object are invalid.</exception>
		// Token: 0x060002B0 RID: 688 RVA: 0x000087F8 File Offset: 0x000069F8
		public MemberInfo[] GetAccessors()
		{
			if (this._accessors == null && this._accessorsCreator != null)
			{
				MemberInfo[] accessors = this._accessorsCreator();
				string message;
				if (!LazyMemberInfo.AreAccessorsValid(this.MemberType, accessors, out message))
				{
					throw new InvalidOperationException(message);
				}
				this._accessors = accessors;
			}
			return this._accessors;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x060002B1 RID: 689 RVA: 0x00008848 File Offset: 0x00006A48
		public override int GetHashCode()
		{
			if (this._accessorsCreator != null)
			{
				return this.MemberType.GetHashCode() ^ this._accessorsCreator.GetHashCode();
			}
			Assumes.NotNull<MemberInfo[]>(this._accessors);
			Assumes.NotNull<MemberInfo>(this._accessors[0]);
			return this.MemberType.GetHashCode() ^ this._accessors[0].GetHashCode();
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002B2 RID: 690 RVA: 0x000088B8 File Offset: 0x00006AB8
		public override bool Equals(object obj)
		{
			LazyMemberInfo lazyMemberInfo = (LazyMemberInfo)obj;
			if (this._memberType != lazyMemberInfo._memberType)
			{
				return false;
			}
			if (this._accessorsCreator != null || lazyMemberInfo._accessorsCreator != null)
			{
				return object.Equals(this._accessorsCreator, lazyMemberInfo._accessorsCreator);
			}
			Assumes.NotNull<MemberInfo[]>(this._accessors);
			Assumes.NotNull<MemberInfo[]>(lazyMemberInfo._accessors);
			return this._accessors.SequenceEqual(lazyMemberInfo._accessors);
		}

		/// <summary>Determines whether the two specified <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to test.</param>
		/// <param name="right">The second object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002B3 RID: 691 RVA: 0x00008925 File Offset: 0x00006B25
		public static bool operator ==(LazyMemberInfo left, LazyMemberInfo right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether the two specified <see cref="T:System.ComponentModel.Composition.ReflectionModel.LazyMemberInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to test.</param>
		/// <param name="right">The second object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002B4 RID: 692 RVA: 0x0000893A File Offset: 0x00006B3A
		public static bool operator !=(LazyMemberInfo left, LazyMemberInfo right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008954 File Offset: 0x00006B54
		private static void EnsureSupportedMemberType(MemberTypes memberType, string argument)
		{
			MemberTypes enumFlagSet = MemberTypes.All;
			Requires.IsInMembertypeSet(memberType, argument, enumFlagSet);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008970 File Offset: 0x00006B70
		private static bool AreAccessorsValid(MemberTypes memberType, MemberInfo[] accessors, out string errorMessage)
		{
			errorMessage = string.Empty;
			if (accessors == null)
			{
				errorMessage = Strings.LazyMemberInfo_AccessorsNull;
				return false;
			}
			if (accessors.All((MemberInfo accessor) => accessor == null))
			{
				errorMessage = Strings.LazyMemberInfo_NoAccessors;
				return false;
			}
			if (memberType != MemberTypes.Event)
			{
				if (memberType == MemberTypes.Property)
				{
					if (accessors.Length != 2)
					{
						errorMessage = Strings.LazyMemberInfo_InvalidPropertyAccessors_Cardinality;
						return false;
					}
					if ((from accessor in accessors
					where accessor != null && accessor.MemberType != MemberTypes.Method
					select accessor).Any<MemberInfo>())
					{
						errorMessage = Strings.LazyMemberinfo_InvalidPropertyAccessors_AccessorType;
						return false;
					}
				}
				else if (accessors.Length != 1 || (accessors.Length == 1 && accessors[0].MemberType != memberType))
				{
					errorMessage = string.Format(CultureInfo.CurrentCulture, Strings.LazyMemberInfo_InvalidAccessorOnSimpleMember, memberType);
					return false;
				}
			}
			else
			{
				if (accessors.Length != 3)
				{
					errorMessage = Strings.LazyMemberInfo_InvalidEventAccessors_Cardinality;
					return false;
				}
				if ((from accessor in accessors
				where accessor != null && accessor.MemberType != MemberTypes.Method
				select accessor).Any<MemberInfo>())
				{
					errorMessage = Strings.LazyMemberinfo_InvalidEventAccessors_AccessorType;
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000124 RID: 292
		private readonly MemberTypes _memberType;

		// Token: 0x04000125 RID: 293
		private MemberInfo[] _accessors;

		// Token: 0x04000126 RID: 294
		private readonly Func<MemberInfo[]> _accessorsCreator;

		// Token: 0x0200006C RID: 108
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002B7 RID: 695 RVA: 0x00008A85 File Offset: 0x00006C85
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00008A91 File Offset: 0x00006C91
			internal bool <AreAccessorsValid>b__14_0(MemberInfo accessor)
			{
				return accessor == null;
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00008A9A File Offset: 0x00006C9A
			internal bool <AreAccessorsValid>b__14_1(MemberInfo accessor)
			{
				return accessor != null && accessor.MemberType != MemberTypes.Method;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00008A9A File Offset: 0x00006C9A
			internal bool <AreAccessorsValid>b__14_2(MemberInfo accessor)
			{
				return accessor != null && accessor.MemberType != MemberTypes.Method;
			}

			// Token: 0x04000127 RID: 295
			public static readonly LazyMemberInfo.<>c <>9 = new LazyMemberInfo.<>c();

			// Token: 0x04000128 RID: 296
			public static Func<MemberInfo, bool> <>9__14_0;

			// Token: 0x04000129 RID: 297
			public static Func<MemberInfo, bool> <>9__14_1;

			// Token: 0x0400012A RID: 298
			public static Func<MemberInfo, bool> <>9__14_2;
		}
	}
}
