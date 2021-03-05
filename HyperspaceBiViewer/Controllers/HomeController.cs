using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using PowerBIEmbedded_AppOwnsData.Models;
using PowerBIEmbedded_AppOwnsData.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PowerBIEmbedded_AppOwnsData.Controllers
{
	public class HomeController : Controller
	{
		private readonly IEmbedService m_embedService;

		public HomeController()
		{
			m_embedService = new EmbedService();
		}

		public ActionResult Index()
		{
			var result = new IndexConfig();
			var assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.Equals("Microsoft.PowerBI.Api")).FirstOrDefault();
			if (assembly != null)
			{
				result.DotNETSDK = assembly.Version.ToString(3);
			}
			return View(result);
		}

		public async Task<ActionResult> EmbedReport(string id, string id1)
		{
			// --- If we don't have a workspace ID and a Report ID then we need to go back to the index.
			if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(id1))
			{
				return RedirectToAction("Index");
			}

			var embedResult = await m_embedService.EmbedReport(id, id1);
			return View(m_embedService.EmbedConfig);
		}
	}
}
