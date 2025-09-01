using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using WebSocketSharp.Net;

namespace WebSocketSharp.Server
{
	// Token: 0x02000048 RID: 72
	public class HttpRequestEventArgs : EventArgs
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001B3CF File Offset: 0x000195CF
		internal HttpRequestEventArgs(HttpListenerContext context, string documentRootPath)
		{
			this._context = context;
			this._docRootPath = documentRootPath;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001B3E8 File Offset: 0x000195E8
		public HttpListenerRequest Request
		{
			get
			{
				return this._context.Request;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001B408 File Offset: 0x00019608
		public HttpListenerResponse Response
		{
			get
			{
				return this._context.Response;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001B428 File Offset: 0x00019628
		public IPrincipal User
		{
			get
			{
				return this._context.User;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001B448 File Offset: 0x00019648
		private string createFilePath(string childPath)
		{
			childPath = childPath.TrimStart(new char[]
			{
				'/',
				'\\'
			});
			return new StringBuilder(this._docRootPath, 32).AppendFormat("/{0}", childPath).ToString().Replace('\\', '/');
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001B498 File Offset: 0x00019698
		private static bool tryReadFile(string path, out byte[] contents)
		{
			contents = null;
			bool flag = !File.Exists(path);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					contents = File.ReadAllBytes(path);
				}
				catch
				{
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001B4E4 File Offset: 0x000196E4
		public byte[] ReadFile(string path)
		{
			bool flag = path == null;
			if (flag)
			{
				throw new ArgumentNullException("path");
			}
			bool flag2 = path.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "path");
			}
			bool flag3 = path.IndexOf("..") > -1;
			if (flag3)
			{
				throw new ArgumentException("It contains '..'.", "path");
			}
			path = this.createFilePath(path);
			byte[] result;
			HttpRequestEventArgs.tryReadFile(path, out result);
			return result;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001B560 File Offset: 0x00019760
		public bool TryReadFile(string path, out byte[] contents)
		{
			bool flag = path == null;
			if (flag)
			{
				throw new ArgumentNullException("path");
			}
			bool flag2 = path.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "path");
			}
			bool flag3 = path.IndexOf("..") > -1;
			if (flag3)
			{
				throw new ArgumentException("It contains '..'.", "path");
			}
			path = this.createFilePath(path);
			return HttpRequestEventArgs.tryReadFile(path, out contents);
		}

		// Token: 0x0400023D RID: 573
		private HttpListenerContext _context;

		// Token: 0x0400023E RID: 574
		private string _docRootPath;
	}
}
