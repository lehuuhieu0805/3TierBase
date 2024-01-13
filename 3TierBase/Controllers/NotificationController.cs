using _3TierBase.Business.ViewModels;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    public class NotificationController : BaseApiController
    {
        /// <summary>
        /// Endpoint for subscribe topic
        /// </summary>
        /// <param name="topic">Topic to subscribe</param>
        /// <param name="registrationTokens">Lsit token generate form client FCM SKDs to subscribe</param>
        /// <returns>A success message</returns>
        /// <response code="200">Return a success message</response>
        [HttpPost("subscribe")]
        [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SubscribeTopic([FromBody]IReadOnlyList<string> registrationTokens, [FromBody]string topic)

        {
            // These registration tokens come from the client FCM SDKs
            // Subscribe the devices corresponding to the registration tokens to the topic
            var response = await FirebaseMessaging
                .DefaultInstance.SubscribeToTopicAsync(registrationTokens, topic);

            // See the TopicManagementResponse reference documentation
            // for the contents of response.
            if (response.SuccessCount == 0)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Msg = "Subscribe to topic not successfully",
                    Success = false,
                    Data = "",
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Msg = "Subscribe to topic successfully",
                    Success = true,
                    Data = "",
                });
            }
        }

        /// <summary>
        /// Endpoint for unsubscribe topic
        /// </summary>
        /// <param name="topic">Topic to subscribe</param>
        /// <param name="registrationTokens">Lsit token generate form client FCM SKDs to subscribe</param>
        /// <returns>A success message</returns>
        /// <response code="200">Return a success message</response>
        [HttpPost("unsubscribe")]
        [ProducesResponseType(typeof(BaseResponse<>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UnSubscribeTopic([FromBody]IReadOnlyList<string> registrationTokens, [FromBody]string topic)

        {
            // Unsubscribe the devices corresponding to the registration tokens from the topic
            var response = await FirebaseMessaging.DefaultInstance
                .UnsubscribeFromTopicAsync(registrationTokens, topic);

            // See the TopicManagementResponse reference documentation
            // for the contents of response
            if (response.SuccessCount == 0)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Msg = "UnSubscribe to topic not successfully",
                    Success = false,
                    Data = response.Errors,
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Msg = "UnSubscribe to topic successfully",
                    Success = true,
                    Data = "",
                });
            }
        }
    }
}
