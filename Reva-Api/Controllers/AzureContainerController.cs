using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reva_Api.Model;
using System.Net;

namespace Reva_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AzureContainerController : ControllerBase
    {
        readonly BlobServiceClient _blobServiceClient;
        readonly ApiResponse _response;
        public AzureContainerController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _response = new ApiResponse();
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetContainerList()
        {
            List<string> listofcontainer = new List<string>();
            try
            {
                var result = _blobServiceClient.GetBlobContainersAsync().AsPages();
                await foreach (Azure.Page<BlobContainerItem> item in result)
                {
                    foreach(BlobContainerItem blobitem in item.Values)
                    {
                        listofcontainer.Add(blobitem.Name);
                    }

                }
                _response.Result = listofcontainer;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = listofcontainer;
            }
            catch (Exception ex)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = new List<string> { ex.Message};
            }
            return _response;
        }
    }
}
