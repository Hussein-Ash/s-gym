using EvaluationBackend.Controllers;
using EvaluationBackend.Helpers;
using EvaluationBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deli.Controllers;

// [ServiceFilter(typeof(AuthorizeActionFilter))]

public class FileController: BaseController{
    private readonly IFileService _fileService;
    public FileController(IFileService fileService) {
        _fileService = fileService;
    }
    [HttpPost("multi")]
    public async Task<IActionResult> Upload([FromForm] IFormFile[] files) => Ok(await _fileService.Upload(files));

    // [HttpGet("")]
    // public async Task<IActionResult> t() => Ok($"{Request.Scheme}://{Request.Host}");
}
 
