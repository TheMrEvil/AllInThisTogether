using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000024DC File Offset: 0x000006DC
		private ContactPoint[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002504 File Offset: 0x00000704
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000251C File Offset: 0x0000071C
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Body as Rigidbody;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000253C File Offset: 0x0000073C
		public ArticulationBody articulationBody
		{
			get
			{
				return this.m_Body as ArticulationBody;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000255C File Offset: 0x0000075C
		public Component body
		{
			get
			{
				return this.m_Body;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002574 File Offset: 0x00000774
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000258C File Offset: 0x0000078C
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000025C4 File Offset: 0x000007C4
		public GameObject gameObject
		{
			get
			{
				return (this.m_Body != null) ? this.m_Body.gameObject : this.m_Collider.gameObject;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000025FC File Offset: 0x000007FC
		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002614 File Offset: 0x00000814
		public ContactPoint[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002664 File Offset: 0x00000864
		public ContactPoint GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000026BC File Offset: 0x000008BC
		public int GetContacts(ContactPoint[] contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
			}
			int num = Mathf.Min(this.m_ContactCount, contacts.Length);
			Array.Copy(this.GetContacts_Internal(), contacts, num);
			return num;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002700 File Offset: 0x00000900
		public int GetContacts(List<ContactPoint> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			contacts.AddRange(this.GetContacts_Internal());
			return this.contactCount;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002740 File Offset: 0x00000940
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002758 File Offset: 0x00000958
		[Obsolete("Do not use Collision.GetEnumerator(), enumerate using non-allocating array returned by Collision.GetContacts() or enumerate using Collision.GetContact(index) instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002778 File Offset: 0x00000978
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Collision.relativeVelocity instead. (UnityUpgradable) -> relativeVelocity", false)]
		public Vector3 impactForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002790 File Offset: 0x00000990
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Will always return zero.", true)]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000027A8 File Offset: 0x000009A8
		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Component other
		{
			get
			{
				return (this.m_Body != null) ? this.m_Body : this.m_Collider;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000024D3 File Offset: 0x000006D3
		public Collision()
		{
		}

		// Token: 0x04000046 RID: 70
		internal Vector3 m_Impulse;

		// Token: 0x04000047 RID: 71
		internal Vector3 m_RelativeVelocity;

		// Token: 0x04000048 RID: 72
		internal Component m_Body;

		// Token: 0x04000049 RID: 73
		internal Collider m_Collider;

		// Token: 0x0400004A RID: 74
		internal int m_ContactCount;

		// Token: 0x0400004B RID: 75
		internal ContactPoint[] m_ReusedContacts;

		// Token: 0x0400004C RID: 76
		internal ContactPoint[] m_LegacyContacts;
	}
}
