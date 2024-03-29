﻿using Microsoft.AspNetCore.Mvc;
using Dinding.Data;
using Dinding.Helpers;

namespace Dinding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DmsController : Controller
    {
        StorageObjectService blob;
        //AzureBlobHelper blob;
        //public DmsController(AzureBlobHelper blob)
        public DmsController(StorageObjectService blob)
        {
            this.blob = blob;

        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetFile(string filename)
        {
            try
            {


                /*
                var res = await blob.DownloadFile(fileName);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(res)
                };
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                */

                //var file = await blob.DownloadFile(filename);
                var item = await blob.DownloadByKey(filename);
                var file = item.Data;
                if (file != null)
                {
                    //var stream = new MemoryStream(file);
                    /*
                    var mime = "image/jpeg";
                    switch (Path.GetExtension(filename).ToLower())
                    {
                        case ".jpg":
                            mime = "image/jpeg";
                            break;
                        case ".png":
                            mime = "image/png";
                            break;
                        case ".gif":
                            mime = "image/gif";
                            break;
                        case ".bmp":
                            mime = "image/bmp";
                            break;
                        case ".jpeg":
                            mime = "image/jpeg";
                            break;
                        case ".zip":
                            mime = "application/zip";
                            break;
                        case ".pdf":
                            mime = "application/pdf";
                            break;
                        case ".ppt":
                            mime = "application/vnd.ms-powerpoint";
                            break;
                        case ".pptx":
                            mime = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                            break;
                        case ".doc":
                            mime = "application/msword";
                            break;
                        case ".docx":
                            mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                            break;
                        case ".xls":
                            mime = "application/vnd.ms-excel";
                            break;
                        case ".xlsx":
                            mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            break;
                        case ".mp4":
                            mime = "video/mp4";
                            break;
                        case ".mp3":
                            mime = "audio/mpeg";
                            break;
                        case ".txt":
                            mime = "text/plain";
                            break;
                    }*/
                    var mime = MimeTypeHelper.GetMimeType(Path.GetExtension(filename));
                    return File(file, mime, filename);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return NotFound();
        }

    }
}
