using WSVenta.Models.NewFolder;
using WSVenta.Models.ViewModels;

namespace WSVenta.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
