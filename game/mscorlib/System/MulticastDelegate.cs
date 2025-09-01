using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a multicast delegate; that is, a delegate that can have more than one element in its invocation list.</summary>
	// Token: 0x02000243 RID: 579
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MulticastDelegate : Delegate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastDelegate" /> class.</summary>
		/// <param name="target">The object on which <paramref name="method" /> is defined.</param>
		/// <param name="method">The name of the method for which a delegate is created.</param>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A4E RID: 6734 RVA: 0x0006107C File Offset: 0x0005F27C
		protected MulticastDelegate(object target, string method) : base(target, method)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastDelegate" /> class.</summary>
		/// <param name="target">The type of object on which <paramref name="method" /> is defined.</param>
		/// <param name="method">The name of the static method for which a delegate is created.</param>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A4F RID: 6735 RVA: 0x00061086 File Offset: 0x0005F286
		protected MulticastDelegate(Type target, string method) : base(target, method)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data needed to serialize this instance.</summary>
		/// <param name="info">An object that holds all the data needed to serialize or deserialize this instance.</param>
		/// <param name="context">(Reserved) The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">A serialization error occurred.</exception>
		// Token: 0x06001A50 RID: 6736 RVA: 0x00061090 File Offset: 0x0005F290
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0006109C File Offset: 0x0005F29C
		protected sealed override object DynamicInvokeImpl(object[] args)
		{
			if (this.delegates == null)
			{
				return base.DynamicInvokeImpl(args);
			}
			int num = 0;
			int num2 = this.delegates.Length;
			object result;
			do
			{
				result = this.delegates[num].DynamicInvoke(args);
			}
			while (++num < num2);
			return result;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000610DC File Offset: 0x0005F2DC
		internal bool HasSingleTarget
		{
			get
			{
				return this.delegates == null;
			}
		}

		/// <summary>Determines whether this multicast delegate and the specified object are equal.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance have the same invocation lists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A53 RID: 6739 RVA: 0x000610E8 File Offset: 0x0005F2E8
		public sealed override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			MulticastDelegate multicastDelegate = obj as MulticastDelegate;
			if (multicastDelegate == null)
			{
				return false;
			}
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				return true;
			}
			if (this.delegates == null ^ multicastDelegate.delegates == null)
			{
				return false;
			}
			if (this.delegates.Length != multicastDelegate.delegates.Length)
			{
				return false;
			}
			for (int i = 0; i < this.delegates.Length; i++)
			{
				if (!this.delegates[i].Equals(multicastDelegate.delegates[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A54 RID: 6740 RVA: 0x00061176 File Offset: 0x0005F376
		public sealed override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a static method represented by the current <see cref="T:System.MulticastDelegate" />.</summary>
		/// <returns>A static method represented by the current <see cref="T:System.MulticastDelegate" />.</returns>
		// Token: 0x06001A55 RID: 6741 RVA: 0x0006117E File Offset: 0x0005F37E
		protected override MethodInfo GetMethodImpl()
		{
			if (this.delegates != null)
			{
				return this.delegates[this.delegates.Length - 1].Method;
			}
			return base.GetMethodImpl();
		}

		/// <summary>Returns the invocation list of this multicast delegate, in invocation order.</summary>
		/// <returns>An array of delegates whose invocation lists collectively match the invocation list of this instance.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A56 RID: 6742 RVA: 0x000611A5 File Offset: 0x0005F3A5
		public sealed override Delegate[] GetInvocationList()
		{
			if (this.delegates != null)
			{
				return (Delegate[])this.delegates.Clone();
			}
			return new Delegate[]
			{
				this
			};
		}

		/// <summary>Combines this <see cref="T:System.Delegate" /> with the specified <see cref="T:System.Delegate" /> to form a new delegate.</summary>
		/// <param name="follow">The delegate to combine with this delegate.</param>
		/// <returns>A delegate that is the new root of the <see cref="T:System.MulticastDelegate" /> invocation list.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="follow" /> does not have the same type as this instance.</exception>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A57 RID: 6743 RVA: 0x000611CC File Offset: 0x0005F3CC
		protected sealed override Delegate CombineImpl(Delegate follow)
		{
			if (follow == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)follow;
			MulticastDelegate multicastDelegate2 = Delegate.AllocDelegateLike_internal(this);
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[]
				{
					this,
					multicastDelegate
				};
			}
			else if (this.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[1 + multicastDelegate.delegates.Length];
				multicastDelegate2.delegates[0] = this;
				Array.Copy(multicastDelegate.delegates, 0, multicastDelegate2.delegates, 1, multicastDelegate.delegates.Length);
			}
			else if (multicastDelegate.delegates == null)
			{
				multicastDelegate2.delegates = new Delegate[this.delegates.Length + 1];
				Array.Copy(this.delegates, 0, multicastDelegate2.delegates, 0, this.delegates.Length);
				multicastDelegate2.delegates[multicastDelegate2.delegates.Length - 1] = multicastDelegate;
			}
			else
			{
				multicastDelegate2.delegates = new Delegate[this.delegates.Length + multicastDelegate.delegates.Length];
				Array.Copy(this.delegates, 0, multicastDelegate2.delegates, 0, this.delegates.Length);
				Array.Copy(multicastDelegate.delegates, 0, multicastDelegate2.delegates, this.delegates.Length, multicastDelegate.delegates.Length);
			}
			return multicastDelegate2;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00061304 File Offset: 0x0005F504
		private int LastIndexOf(Delegate[] haystack, Delegate[] needle)
		{
			if (haystack.Length < needle.Length)
			{
				return -1;
			}
			if (haystack.Length == needle.Length)
			{
				for (int i = 0; i < haystack.Length; i++)
				{
					if (!haystack[i].Equals(needle[i]))
					{
						return -1;
					}
				}
				return 0;
			}
			int num;
			for (int j = haystack.Length - needle.Length; j >= 0; j -= num + 1)
			{
				num = 0;
				while (needle[num].Equals(haystack[j]))
				{
					if (num == needle.Length - 1)
					{
						return j - num;
					}
					j++;
					num++;
				}
			}
			return -1;
		}

		/// <summary>Removes an element from the invocation list of this <see cref="T:System.MulticastDelegate" /> that is equal to the specified delegate.</summary>
		/// <param name="value">The delegate to search for in the invocation list.</param>
		/// <returns>If <paramref name="value" /> is found in the invocation list for this instance, then a new <see cref="T:System.Delegate" /> without <paramref name="value" /> in its invocation list; otherwise, this instance with its original invocation list.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A59 RID: 6745 RVA: 0x0006137C File Offset: 0x0005F57C
		protected sealed override Delegate RemoveImpl(Delegate value)
		{
			if (value == null)
			{
				return this;
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)value;
			if (this.delegates == null && multicastDelegate.delegates == null)
			{
				if (!this.Equals(multicastDelegate))
				{
					return this;
				}
				return null;
			}
			else
			{
				if (this.delegates == null)
				{
					foreach (Delegate obj in multicastDelegate.delegates)
					{
						if (this.Equals(obj))
						{
							return null;
						}
					}
					return this;
				}
				if (multicastDelegate.delegates == null)
				{
					int num = Array.LastIndexOf<Delegate>(this.delegates, multicastDelegate);
					if (num == -1)
					{
						return this;
					}
					if (this.delegates.Length <= 1)
					{
						throw new InvalidOperationException();
					}
					if (this.delegates.Length == 2)
					{
						return this.delegates[(num == 0) ? 1 : 0];
					}
					MulticastDelegate multicastDelegate2 = Delegate.AllocDelegateLike_internal(this);
					multicastDelegate2.delegates = new Delegate[this.delegates.Length - 1];
					Array.Copy(this.delegates, multicastDelegate2.delegates, num);
					Array.Copy(this.delegates, num + 1, multicastDelegate2.delegates, num, this.delegates.Length - num - 1);
					return multicastDelegate2;
				}
				else
				{
					if (this.delegates.Equals(multicastDelegate.delegates))
					{
						return null;
					}
					int num2 = this.LastIndexOf(this.delegates, multicastDelegate.delegates);
					if (num2 == -1)
					{
						return this;
					}
					MulticastDelegate multicastDelegate3 = Delegate.AllocDelegateLike_internal(this);
					multicastDelegate3.delegates = new Delegate[this.delegates.Length - multicastDelegate.delegates.Length];
					Array.Copy(this.delegates, multicastDelegate3.delegates, num2);
					Array.Copy(this.delegates, num2 + multicastDelegate.delegates.Length, multicastDelegate3.delegates, num2, this.delegates.Length - num2 - multicastDelegate.delegates.Length);
					return multicastDelegate3;
				}
			}
		}

		/// <summary>Determines whether two <see cref="T:System.MulticastDelegate" /> objects are equal.</summary>
		/// <param name="d1">The left operand.</param>
		/// <param name="d2">The right operand.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> have the same invocation lists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00061524 File Offset: 0x0005F724
		public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d1.Equals(d2);
		}

		/// <summary>Determines whether two <see cref="T:System.MulticastDelegate" /> objects are not equal.</summary>
		/// <param name="d1">The left operand.</param>
		/// <param name="d2">The right operand.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> do not have the same invocation lists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06001A5B RID: 6747 RVA: 0x00061535 File Offset: 0x0005F735
		public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d1 == null)
			{
				return d2 != null;
			}
			return !d1.Equals(d2);
		}

		// Token: 0x04001732 RID: 5938
		private Delegate[] delegates;
	}
}
