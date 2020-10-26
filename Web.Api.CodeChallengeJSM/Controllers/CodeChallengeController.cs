using Libary.CodeChallengeJSM.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Structure.CodeChallengeJSM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Api.CodeChallengeJSM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeChallengeController : ControllerBase
    {
        #region Constructor
        private readonly Clients _clients;

        public CodeChallengeController(Clients clients)
        {
            _clients = clients;
        }
        #endregion

        #region GetClients
        /// <summary>
        /// All clients per region and/or type
        /// </summary> 
        /// <remarks>GET to list all clients</remarks>
        /// <param name="regionClient">Region of the client (Optional)</param>
        /// <param name="typeClient">type of the client (Optional)</param>
        /// <param name="pageSize">Numer per item on Page (Optional)</param>
        /// <param name="pageNumber">Number of the page (Optional)</param>
        /// <returns>Return all clients</returns>
        ///        
        [Route("v1/ReturnClients/")]
        [HttpGet]
        public ActionResult GetClients(string? RegionClient, string? TypeClient, int? PageSize, int? PageNumber)
        {
            FilterStructure filterStructure = filterClients(RegionClient, TypeClient, PageSize, PageNumber);

            return Ok(new
            {
             pageNumber = filterStructure.filter.pageNumber,
             pageSize = filterStructure.filter.pageSize,
             totalCount = filterStructure.filter.users.Count,
             users = filterStructure.filter.users
            });
        }
        #endregion

        #region Client Filter
        protected FilterStructure filterClients(string? RegionClient, string? TypeClient, int? PageSize, int? PageNumber)
        {
            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();

            int maxPageSize = Convert.ToInt32(configuration.GetSection("Parameters").Get<Dictionary<string, string>>()["maxPageSize"]);
            int minPageSize = Convert.ToInt32(configuration.GetSection("Parameters").Get<Dictionary<string, string>>()["minPageSize"]);
            int defaultPageNumber = Convert.ToInt32(configuration.GetSection("Parameters").Get<Dictionary<string, string>>()["defaultPageNumber"]);


            FilterStructure filterStructure = new FilterStructure();
            filterStructure.filter = new Filter();
            int auxPageSize = 0;
            var allClients = _clients.clients();
                   
            if (RegionClient != null)
            {
                if (RegionClient == "southeast" || RegionClient == "south" || RegionClient == "midwest" || RegionClient == "north" || RegionClient == "northeast")
                {
                    allClients = allClients.Where(x => x.location.region == RegionClient);
                }
            }
            if (TypeClient != null)
            {
                if (TypeClient == "special" || TypeClient == "normal" || TypeClient == "laborious")
                {
                    allClients = allClients.Where(x => x.type == TypeClient);
                }
            }

            if (PageSize > 0)
            {
                if (PageSize > maxPageSize)
                {
                    filterStructure.filter.pageSize = maxPageSize; //default value to max Pagesize
                    auxPageSize = maxPageSize;
                }


                else
                {
                    if (PageSize > allClients.Count())
                    {
                        filterStructure.filter.pageSize = allClients.Count();
                        auxPageSize = allClients.Count();
                    }

                    else
                    {
                        filterStructure.filter.pageSize = Convert.ToInt32(PageSize);
                        auxPageSize = Convert.ToInt32(PageSize);
                    }
                }
            }
            else
            {
                filterStructure.filter.pageSize = minPageSize; //default value to min Pagesize
                auxPageSize = minPageSize;
            }


            if (PageNumber >= allClients.Count())
            {
                if (allClients.Count() % filterStructure.filter.pageSize != 0)
                {
                    filterStructure.filter.pageNumber = (allClients.Count() / filterStructure.filter.pageSize) + 1;
                    filterStructure.filter.pageSize = (allClients.Count() - (allClients.Count() / filterStructure.filter.pageSize) * filterStructure.filter.pageSize);
                }
                else
                {
                    filterStructure.filter.pageNumber = (allClients.Count() / filterStructure.filter.pageSize);
                }
            }
            else
            {
                if (PageNumber > 0)
                    filterStructure.filter.pageNumber = Convert.ToInt32(PageNumber);

                else
                    filterStructure.filter.pageNumber = defaultPageNumber; //default value
            }

            var arrayAllClients = allClients.ToArray();
            int index = 0;

            if (filterStructure.filter.pageNumber == 1)
                index = 0;
            else
            {

                if (((filterStructure.filter.pageNumber - 1) * auxPageSize) > arrayAllClients.Length) 
                    index = arrayAllClients.Length - filterStructure.filter.pageSize;
                else
                    index = ((filterStructure.filter.pageNumber - 1) * auxPageSize);
            }


            int size = index >= arrayAllClients.Length ? arrayAllClients.Length : filterStructure.filter.pageSize + index - 1;
            size = size > arrayAllClients.Length ? arrayAllClients.Length -1 : size;
            filterStructure.filter.users = new List<User>();

            for (int i = index; i <= size; i++)
            {
                filterStructure.filter.users.Add(arrayAllClients[i]);
            }


            return filterStructure;
        }
        #endregion
    }
}
