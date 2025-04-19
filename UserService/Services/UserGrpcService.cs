using Contracts;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UserService.Data;

namespace UserService.Services
{
    public class UserGrpcService(UserDbContext context) : UserServiceRpc.UserServiceRpcBase
    {
        private readonly UserDbContext _context = context;

        public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Login == request.Login &&
                u.Password == request.Password); // в будущем: сравнивать хеш

            if(user is null)
                return new RegisterResponse
                {
                    Success = false,
                    Message = "Неверные данные"
                };

            return new RegisterResponse
            {
                Success = true,
                Message = user.Login
            };
        }
    }
}