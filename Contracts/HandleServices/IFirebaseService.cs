using Entities.DTOs;
using System.Threading.Tasks;

namespace Contracts.HandleServices
{
    public interface IFirebaseService
    {
        void InitFirebase();
        Task<FirebaseInfo> GetInforFromToken(string firebaseToken);
    }
}
