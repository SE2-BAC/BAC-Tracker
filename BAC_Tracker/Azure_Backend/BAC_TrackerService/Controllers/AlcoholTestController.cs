using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using BAC_TrackerService.DataObjects;
using BAC_TrackerService.Models;

namespace BAC_TrackerService.Controllers
{
    public class AlcoholTestController : TableController<AlcoholTest>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            BAC_TrackerContext context = new BAC_TrackerContext();
            DomainManager = new EntityDomainManager<AlcoholTest>(context, Request);
        }

        // GET tables/AlcoholTest
        public IQueryable<AlcoholTest> GetAllAlcoholTests()
        {
            return Query();
        }

        // GET tables/AlcoholTest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AlcoholTest> GetAlcoholTest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AlcoholTest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AlcoholTest> PatchAlcoholTest(string id, Delta<AlcoholTest> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/AlcoholTest
        public async Task<IHttpActionResult> PostAlcoholTest(AlcoholTest item)
        {
            AlcoholTest current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AlcoholTest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAlcoholTest(string id)
        {
            return DeleteAsync(id);
        }
    }
}