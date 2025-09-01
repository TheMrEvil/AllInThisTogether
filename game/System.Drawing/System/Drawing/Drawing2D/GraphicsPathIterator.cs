using System;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Provides the ability to iterate through subpaths in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> and test the types of shapes contained in each subpath. This class cannot be inherited.</summary>
	// Token: 0x02000140 RID: 320
	public sealed class GraphicsPathIterator : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> class with the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object for which this helper class is to be initialized.</param>
		// Token: 0x06000E2E RID: 3630 RVA: 0x00020148 File Offset: 0x0001E348
		public GraphicsPathIterator(GraphicsPath path)
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCreatePathIter(out zero, new HandleRef(path, (path == null) ? IntPtr.Zero : path.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.nativeIter = zero;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> object.</summary>
		// Token: 0x06000E2F RID: 3631 RVA: 0x00020190 File Offset: 0x0001E390
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000201A0 File Offset: 0x0001E3A0
		private void Dispose(bool disposing)
		{
			if (this.nativeIter != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDeletePathIter(new HandleRef(this, this.nativeIter));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeIter = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000E31 RID: 3633 RVA: 0x00020208 File Offset: 0x0001E408
		~GraphicsPathIterator()
		{
			this.Dispose(false);
		}

		/// <summary>Moves the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the next subpath in the path. The start index and end index of the next subpath are contained in the [out] parameters.</summary>
		/// <param name="startIndex">[out] Receives the starting index of the next subpath.</param>
		/// <param name="endIndex">[out] Receives the ending index of the next subpath.</param>
		/// <param name="isClosed">[out] Indicates whether the subpath is closed.</param>
		/// <returns>The number of subpaths in the <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</returns>
		// Token: 0x06000E32 RID: 3634 RVA: 0x00020238 File Offset: 0x0001E438
		public int NextSubpath(out int startIndex, out int endIndex, out bool isClosed)
		{
			int result;
			int num2;
			int num3;
			int num = GDIPlus.GdipPathIterNextSubpath(new HandleRef(this, this.nativeIter), out result, out num2, out num3, out isClosed);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			startIndex = num2;
			endIndex = num3;
			return result;
		}

		/// <summary>Gets the next figure (subpath) from the associated path of this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" />.</summary>
		/// <param name="path">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that is to have its data points set to match the data points of the retrieved figure (subpath) for this iterator.</param>
		/// <param name="isClosed">[out] Indicates whether the current subpath is closed. It is <see langword="true" /> if the if the figure is closed, otherwise it is <see langword="false" />.</param>
		/// <returns>The number of data points in the retrieved figure (subpath). If there are no more figures to retrieve, zero is returned.</returns>
		// Token: 0x06000E33 RID: 3635 RVA: 0x00020270 File Offset: 0x0001E470
		public int NextSubpath(GraphicsPath path, out bool isClosed)
		{
			int result;
			int num = GDIPlus.GdipPathIterNextSubpathPath(new HandleRef(this, this.nativeIter), out result, new HandleRef(path, (path == null) ? IntPtr.Zero : path.nativePath), out isClosed);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		/// <summary>Gets the starting index and the ending index of the next group of data points that all have the same type.</summary>
		/// <param name="pathType">[out] Receives the point type shared by all points in the group. Possible types can be retrieved from the <see cref="T:System.Drawing.Drawing2D.PathPointType" /> enumeration.</param>
		/// <param name="startIndex">[out] Receives the starting index of the group of points.</param>
		/// <param name="endIndex">[out] Receives the ending index of the group of points.</param>
		/// <returns>This method returns the number of data points in the group. If there are no more groups in the path, this method returns 0.</returns>
		// Token: 0x06000E34 RID: 3636 RVA: 0x000202B4 File Offset: 0x0001E4B4
		public int NextPathType(out byte pathType, out int startIndex, out int endIndex)
		{
			int result;
			int num = GDIPlus.GdipPathIterNextPathType(new HandleRef(this, this.nativeIter), out result, out pathType, out startIndex, out endIndex);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		/// <summary>Increments the <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the next marker in the path and returns the start and stop indexes by way of the [out] parameters.</summary>
		/// <param name="startIndex">[out] The integer reference supplied to this parameter receives the index of the point that starts a subpath.</param>
		/// <param name="endIndex">[out] The integer reference supplied to this parameter receives the index of the point that ends the subpath to which <paramref name="startIndex" /> points.</param>
		/// <returns>The number of points between this marker and the next.</returns>
		// Token: 0x06000E35 RID: 3637 RVA: 0x000202E4 File Offset: 0x0001E4E4
		public int NextMarker(out int startIndex, out int endIndex)
		{
			int result;
			int num = GDIPlus.GdipPathIterNextMarker(new HandleRef(this, this.nativeIter), out result, out startIndex, out endIndex);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		/// <summary>This <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> object has a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object associated with it. The <see cref="M:System.Drawing.Drawing2D.GraphicsPathIterator.NextMarker(System.Drawing.Drawing2D.GraphicsPath)" /> method increments the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to the next marker in its path and copies all the points contained between the current marker and the next marker (or end of path) to a second <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object passed in to the parameter.</summary>
		/// <param name="path">The <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object to which the points will be copied.</param>
		/// <returns>The number of points between this marker and the next.</returns>
		// Token: 0x06000E36 RID: 3638 RVA: 0x00020314 File Offset: 0x0001E514
		public int NextMarker(GraphicsPath path)
		{
			int result;
			int num = GDIPlus.GdipPathIterNextMarkerPath(new HandleRef(this, this.nativeIter), out result, new HandleRef(path, (path == null) ? IntPtr.Zero : path.nativePath));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		/// <summary>Gets the number of points in the path.</summary>
		/// <returns>The number of points in the path.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00020358 File Offset: 0x0001E558
		public int Count
		{
			get
			{
				int result;
				int num = GDIPlus.GdipPathIterGetCount(new HandleRef(this, this.nativeIter), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
		}

		/// <summary>Gets the number of subpaths in the path.</summary>
		/// <returns>The number of subpaths in the path.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00020384 File Offset: 0x0001E584
		public int SubpathCount
		{
			get
			{
				int result;
				int num = GDIPlus.GdipPathIterGetSubpathCount(new HandleRef(this, this.nativeIter), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
		}

		/// <summary>Indicates whether the path associated with this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> contains a curve.</summary>
		/// <returns>This method returns <see langword="true" /> if the current subpath contains a curve; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000E39 RID: 3641 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public bool HasCurve()
		{
			bool result;
			int num = GDIPlus.GdipPathIterHasCurve(new HandleRef(this, this.nativeIter), out result);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return result;
		}

		/// <summary>Rewinds this <see cref="T:System.Drawing.Drawing2D.GraphicsPathIterator" /> to the beginning of its associated path.</summary>
		// Token: 0x06000E3A RID: 3642 RVA: 0x000203DC File Offset: 0x0001E5DC
		public void Rewind()
		{
			int num = GDIPlus.GdipPathIterRewind(new HandleRef(this, this.nativeIter));
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Copies the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> property and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> property arrays of the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into the two specified arrays.</summary>
		/// <param name="points">Upon return, contains an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points in the path.</param>
		/// <param name="types">Upon return, contains an array of bytes that represents the types of points in the path.</param>
		/// <returns>The number of points copied.</returns>
		// Token: 0x06000E3B RID: 3643 RVA: 0x00020408 File Offset: 0x0001E608
		public int Enumerate(ref PointF[] points, ref byte[] types)
		{
			if (points.Length != types.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = 0;
			int num2 = Marshal.SizeOf(typeof(GPPOINTF));
			int num3 = points.Length;
			byte[] array = new byte[num3];
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num3 * num2));
			try
			{
				int num4 = GDIPlus.GdipPathIterEnumerate(new HandleRef(this, this.nativeIter), out num, intPtr, array, num3);
				if (num4 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num4);
				}
				if (num < num3)
				{
					SafeNativeMethods.ZeroMemory(checked((long)intPtr + num * num2), (ulong)((long)((num3 - num) * num2)));
				}
				points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, num3);
				array.CopyTo(types, 0);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return num;
		}

		/// <summary>Copies the <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathPoints" /> property and <see cref="P:System.Drawing.Drawing2D.GraphicsPath.PathTypes" /> property arrays of the associated <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> into the two specified arrays.</summary>
		/// <param name="points">Upon return, contains an array of <see cref="T:System.Drawing.PointF" /> structures that represents the points in the path.</param>
		/// <param name="types">Upon return, contains an array of bytes that represents the types of points in the path.</param>
		/// <param name="startIndex">Specifies the starting index of the arrays.</param>
		/// <param name="endIndex">Specifies the ending index of the arrays.</param>
		/// <returns>The number of points copied.</returns>
		// Token: 0x06000E3C RID: 3644 RVA: 0x000204C0 File Offset: 0x0001E6C0
		public int CopyData(ref PointF[] points, ref byte[] types, int startIndex, int endIndex)
		{
			if (points.Length != types.Length || endIndex - startIndex + 1 > points.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			int num = 0;
			int num2 = Marshal.SizeOf(typeof(GPPOINTF));
			int num3 = points.Length;
			byte[] array = new byte[num3];
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num3 * num2));
			try
			{
				int num4 = GDIPlus.GdipPathIterCopyData(new HandleRef(this, this.nativeIter), out num, intPtr, array, startIndex, endIndex);
				if (num4 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num4);
				}
				if (num < num3)
				{
					SafeNativeMethods.ZeroMemory(checked((long)intPtr + num * num2), (ulong)((long)((num3 - num) * num2)));
				}
				points = SafeNativeMethods.Gdip.ConvertGPPOINTFArrayF(intPtr, num3);
				array.CopyTo(types, 0);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return num;
		}

		// Token: 0x04000AE1 RID: 2785
		internal IntPtr nativeIter;
	}
}
