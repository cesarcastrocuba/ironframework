using BusinessEntities;
using BusinessObject;
using DataTransferObject;
using DataTransferObject.Model;
using IronFramework.Utility;
using IronFramework.Utility.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi2.Controllers;
	
namespace WebApi2.Controllers
{   

    [RoutePrefix("api/EmployeePayHistory")]
	public partial class EmployeePayHistoryController : BaseWebAPIController 
	{

        /// <summary>
        /// The EmployeePayHistory bo
        /// </summary>
        private IEmployeePayHistoryBO _EmployeePayHistoryBO=null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeePayHistoryController"/> class.
        /// </summary>
        /// <param name="EmployeePayHistoryBO">The _ userbo.</param>
        public EmployeePayHistoryController(IEmployeePayHistoryBO  _varEmployeePayHistoryBO)
        {
            _EmployeePayHistoryBO = _varEmployeePayHistoryBO;
        }


         /// <summary>
        /// Get the List ofEmployeePayHistorydto.
        /// </summary>
        /// <param name="_EmployeePayHistoryDto">The EmployeePayHistory dto.</param>
        /// <example><code> HTTP GET: api/EmployeePayHistory/?pageindex=1&pagesize=10&....</code></example>
        /// <returns>DatagridData List of EmployeePayHistoryDto</returns>
        public EasyuiDatagridData<EmployeePayHistoryDto> Get([FromUri] EmployeePayHistoryDto _EmployeePayHistoryDto)
        {
           return  _EmployeePayHistoryBO.FindAll(_EmployeePayHistoryDto);
        }

        /// <summary>
        /// Gets the specified page index.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <example><code>GET: api/EmployeePayHistory/?pageindex=1&pagesize=10</code></example>
        /// <returns>DatagridData List of EmployeePayHistoryDto</returns>
        public EasyuiDatagridData<EmployeePayHistoryDto> Get([FromUri] int pageIndex, int pageSize)
        {
            return _EmployeePayHistoryBO.FindEnties(new EmployeePayHistoryDto { pageIndex=pageIndex,pageSize=pageSize});
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <example><code>GET: api/EmployeePayHistory/5</code></example>
        /// <returns>EmployeePayHistoryDto</returns>
        public EmployeePayHistoryDto Get(Int32 id)
        {
            var dtoEntity = new EmployeePayHistoryDto() { EmployeeID=id};
            return _EmployeePayHistoryBO.GetEntiyByPK(dtoEntity);
        }

        /// <summary>
        /// Gets the EmployeePayHistory with aync 
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <example><code>GET: api/EmployeePayHistory/GetAync?pageindex=1&pagesize=10</code></example>
        /// <returns>EmployeePayHistoryDto</returns>
        [HttpGet]
        [Route("GetAync")]
        public async Task<PagedList<EmployeePayHistoryDto>> GetAync([FromUri] int pageIndex, int pageSize)
        {
            return await _EmployeePayHistoryBO.FindEntiesAsync(pageIndex, pageSize);
        }

        // GET: api/EmployeePayHistory/GetAync?pageindex=1&pagesize=10
        [HttpGet]
        [Route("GetPageListAync")]
        [PagedListActionFilter] 
        public async Task<object> GetPageListAync()
        {
		    int index = Request.GetPageIndex();
            int size = Request.GetPageSize();
            var results = await _EmployeePayHistoryBO.FindEntiesAsync(index, size);

            return new { Items = results, MetaData = results.GetMetaData() };
        }


        // POST: api/EmployeePayHistory
        [ValidateModel]
        public void Post(EmployeePayHistoryDto value)
        {
            _EmployeePayHistoryBO.CreateEntiy(value);
        }

        // PUT: api/EmployeePayHistory/
        [ValidateModel]
        public void Put(EmployeePayHistoryDto value)
        {
            _EmployeePayHistoryBO.UpdateWithAttachEntiy(value);
        }

        // DELETE: api/EmployeePayHistory/5
        public void Delete(int id)
        {
            var entity = new EmployeePayHistoryDto() { EmployeeID = id };
            _EmployeePayHistoryBO.DeleteWithAttachEntiy(entity);
        }


        /// <summary>
        /// Deletes the specified identifier. 
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <example>
        /// POST http://localhost:14488/api/EmployeePayHistory
        /// BODY: {
        /// "Ids": ["1621","1622"]
        ///}
        /// 
        /// </example>
        [HttpPut]
        [Route("DeleteByList")]
        public void DeleteByList(DeleteObject deletedObjectList)
        {
            if (deletedObjectList != null)
            {
                int[] id = deletedObjectList.Ids;
                if (id != null && id.Length > 0)
                {
                    var listEnties = new List<EmployeePayHistoryDto>();
                    foreach (int i in id)
                    {
                        listEnties.Add(new EmployeePayHistoryDto() { EmployeeID = i });
                    }
                    _EmployeePayHistoryBO.DeleteWithAttachEntiy(listEnties.ToArray());
                }
            }
        }
	}
}
