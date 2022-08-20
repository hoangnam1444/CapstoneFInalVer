using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ChatRoomRepository : RepositoryBase<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(DataContext context) : base(context)
        {
        }

        public async Task<Pagination<StudentInChat>> GetChatWithStudent(int connectorId, PagingParameters param)
        {
            var data = await FindByCondition(x => x.ConnectorId == connectorId, false)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Include(x => x.Student)
                .Select(x => new StudentInChat
                {
                    RoomId = x.Id,
                    ImagePath = x.Student.ImagePath,
                    Name = x.Student.UserName,
                    StudentId = x.StudentId
                }).ToListAsync();
            return new Pagination<StudentInChat>
            {
                PageSize = param.PageSize,
                PageNumber = param.PageNumber,
                Count = await FindByCondition(x => x.ConnectorId == connectorId, false).CountAsync(),
                Data = data
            };
        }

        public async Task<ChatRoom> GetExistChatRoom(int senderId, int receiverId)
        {
            return await FindByCondition(x => x.ConnectorId == receiverId && x.StudentId == senderId, false)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistRoom(int senderId, int receiverId)
        {
            var room = await FindByCondition(x => x.ConnectorId == receiverId && x.StudentId == senderId, false)
                .FirstOrDefaultAsync();
            return room != null;
        }
    }
}