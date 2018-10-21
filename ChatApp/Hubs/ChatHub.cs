using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using ChatApp.Data.Entities;

namespace ChatApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Send message
        /// </summary>
        public async Task SendMessage(string message)
        {
            var user = await _userManager
                .FindByIdAsync(Context.UserIdentifier);

            if (user != null)
            {
                message = HttpUtility.HtmlEncode(message);
                await Clients.All.SendAsync("ReceiveMessage", user.FullName, message);
            }
        }
    }
}
