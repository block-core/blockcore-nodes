using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stratis.Bitcoin.Controllers;
using Stratis.Bitcoin.Interfaces;

namespace Cirrus.Node.Features.KubernetesProbes
{
    [ApiVersion("1")]
    [Route("api/k8s")]
    public class KubernetesProbesController : FeatureController
    {
        private readonly IInitialBlockDownloadState _idbStateAccessor;

        public KubernetesProbesController(IInitialBlockDownloadState idbStateAccessor)
        {
            _idbStateAccessor = idbStateAccessor;
        }

        /// <summary>Readiness probe indicating that traffic can be routed to the node.</summary>
        [HttpGet("ready")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetReadiness()
        {
            return !_idbStateAccessor.IsInitialBlockDownload()
                ? NoContent()
                : StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
    }

}
