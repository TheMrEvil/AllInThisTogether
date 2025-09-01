using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x020004F9 RID: 1273
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000BC393 File Offset: 0x000BA593
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000BC3B0 File Offset: 0x000BA5B0
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x040023F0 RID: 9200
		private string m_issuer;

		// Token: 0x040023F1 RID: 9201
		private string[] m_roles;

		// Token: 0x040023F2 RID: 9202
		private ClaimsIdentity m_subject;

		// Token: 0x020004FA RID: 1274
		[CompilerGenerated]
		private sealed class <get_Claims>d__5 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x0600330D RID: 13069 RVA: 0x000BC3C0 File Offset: 0x000BA5C0
			[DebuggerHidden]
			public <get_Claims>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600330E RID: 13070 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600330F RID: 13071 RVA: 0x000BC3DC File Offset: 0x000BA5DC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				RoleClaimProvider roleClaimProvider = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					i = 0;
					goto IL_90;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_80:
				int num2 = i;
				i = num2 + 1;
				IL_90:
				if (i >= roleClaimProvider.m_roles.Length)
				{
					return false;
				}
				if (roleClaimProvider.m_roles[i] != null)
				{
					this.<>2__current = new Claim(roleClaimProvider.m_subject.RoleClaimType, roleClaimProvider.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", roleClaimProvider.m_issuer, roleClaimProvider.m_issuer, roleClaimProvider.m_subject);
					this.<>1__state = 1;
					return true;
				}
				goto IL_80;
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06003310 RID: 13072 RVA: 0x000BC48A File Offset: 0x000BA68A
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003311 RID: 13073 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x06003312 RID: 13074 RVA: 0x000BC48A File Offset: 0x000BA68A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003313 RID: 13075 RVA: 0x000BC494 File Offset: 0x000BA694
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				RoleClaimProvider.<get_Claims>d__5 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new RoleClaimProvider.<get_Claims>d__5(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x06003314 RID: 13076 RVA: 0x000BC4D7 File Offset: 0x000BA6D7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x040023F3 RID: 9203
			private int <>1__state;

			// Token: 0x040023F4 RID: 9204
			private Claim <>2__current;

			// Token: 0x040023F5 RID: 9205
			private int <>l__initialThreadId;

			// Token: 0x040023F6 RID: 9206
			public RoleClaimProvider <>4__this;

			// Token: 0x040023F7 RID: 9207
			private int <i>5__2;
		}
	}
}
