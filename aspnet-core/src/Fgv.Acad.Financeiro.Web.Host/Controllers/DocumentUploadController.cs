using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.Storage;

namespace Fgv.Acad.Financeiro.Web.Controllers
{
	public class DocumentUploadController : DocumentUploadControllerBase
	{
		public DocumentUploadController(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
		{
		}
	}
}
