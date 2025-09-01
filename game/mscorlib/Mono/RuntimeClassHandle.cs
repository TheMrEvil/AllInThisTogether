using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000048 RID: 72
	internal struct RuntimeClassHandle
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000464C File Offset: 0x0000284C
		internal unsafe RuntimeClassHandle(RuntimeStructs.MonoClass* value)
		{
			this.value = value;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004655 File Offset: 0x00002855
		internal unsafe RuntimeClassHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.MonoClass*)((void*)ptr);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004663 File Offset: 0x00002863
		internal unsafe RuntimeStructs.MonoClass* Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000466C File Offset: 0x0000286C
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeClassHandle)obj).Value;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000046B4 File Offset: 0x000028B4
		public unsafe override int GetHashCode()
		{
			return ((IntPtr)((void*)this.value)).GetHashCode();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000046D4 File Offset: 0x000028D4
		public bool Equals(RuntimeClassHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000046E8 File Offset: 0x000028E8
		public static bool operator ==(RuntimeClassHandle left, object right)
		{
			if (right != null && right is RuntimeClassHandle)
			{
				RuntimeClassHandle handle = (RuntimeClassHandle)right;
				return left.Equals(handle);
			}
			return false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004711 File Offset: 0x00002911
		public static bool operator !=(RuntimeClassHandle left, object right)
		{
			return !(left == right);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004720 File Offset: 0x00002920
		public static bool operator ==(object left, RuntimeClassHandle right)
		{
			return left != null && left is RuntimeClassHandle && ((RuntimeClassHandle)left).Equals(right);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004749 File Offset: 0x00002949
		public static bool operator !=(object left, RuntimeClassHandle right)
		{
			return !(left == right);
		}

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr GetTypeFromClass(RuntimeStructs.MonoClass* klass);

		// Token: 0x06000102 RID: 258 RVA: 0x00004755 File Offset: 0x00002955
		internal RuntimeTypeHandle GetTypeHandle()
		{
			return new RuntimeTypeHandle(RuntimeClassHandle.GetTypeFromClass(this.value));
		}

		// Token: 0x04000DDF RID: 3551
		private unsafe RuntimeStructs.MonoClass* value;
	}
}
