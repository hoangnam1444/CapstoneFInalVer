using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IChatRoomRepository
    {
        Task<bool> IsExistRoom(int senderId, int receiverId);
        Task<ChatRoom> GetExistChatRoom(int senderId, int receiverId);
        void Create(ChatRoom newChatRoom);
        Task<Pagination<StudentInChat>> GetChatWithStudent(int connectorId, PagingParameters param);
    }
}