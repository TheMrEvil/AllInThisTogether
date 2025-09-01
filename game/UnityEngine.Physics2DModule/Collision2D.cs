using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision2D
	{
		// Token: 0x06000222 RID: 546 RVA: 0x000067B8 File Offset: 0x000049B8
		private ContactPoint2D[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000067E0 File Offset: 0x000049E0
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006804 File Offset: 0x00004A04
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006828 File Offset: 0x00004A28
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000684C File Offset: 0x00004A4C
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006870 File Offset: 0x00004A70
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000068A8 File Offset: 0x00004AA8
		public GameObject gameObject
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000068E0 File Offset: 0x00004AE0
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000068F8 File Offset: 0x00004AF8
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006914 File Offset: 0x00004B14
		public ContactPoint2D[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint2D[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006964 File Offset: 0x00004B64
		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000697C File Offset: 0x00004B7C
		public ContactPoint2D GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000069D4 File Offset: 0x00004BD4
		public int GetContacts(ContactPoint2D[] contacts)
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

		// Token: 0x0600022F RID: 559 RVA: 0x00006A18 File Offset: 0x00004C18
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			ContactPoint2D[] contacts_Internal = this.GetContacts_Internal();
			for (int i = 0; i < this.m_ContactCount; i++)
			{
				contacts.Add(contacts_Internal[i]);
			}
			return this.m_ContactCount;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000055D4 File Offset: 0x000037D4
		public Collision2D()
		{
		}

		// Token: 0x0400005B RID: 91
		internal int m_Collider;

		// Token: 0x0400005C RID: 92
		internal int m_OtherCollider;

		// Token: 0x0400005D RID: 93
		internal int m_Rigidbody;

		// Token: 0x0400005E RID: 94
		internal int m_OtherRigidbody;

		// Token: 0x0400005F RID: 95
		internal Vector2 m_RelativeVelocity;

		// Token: 0x04000060 RID: 96
		internal int m_Enabled;

		// Token: 0x04000061 RID: 97
		internal int m_ContactCount;

		// Token: 0x04000062 RID: 98
		internal ContactPoint2D[] m_ReusedContacts;

		// Token: 0x04000063 RID: 99
		internal ContactPoint2D[] m_LegacyContacts;
	}
}
