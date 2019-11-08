using AutoMapper;
using FinancialChat.Domain.ApiModels.Response;
using FinancialChat.Providers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        protected readonly IDatabaseProvider _databaseProvider;
        protected readonly IMapper _mapper;

        public MessagesController(IDatabaseProvider databaseProvider, IMapper mapper)
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtains all messages from the database
        /// </summary>
        public async Task<List<MessageDto>> GetRoomMessages()
        {
            var messages = await _databaseProvider.GetMessages();

            return _mapper.Map<List<MessageDto>>(messages);
        }
    }
}