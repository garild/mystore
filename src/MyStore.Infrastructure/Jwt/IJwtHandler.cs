using System;
using System.Collections.Generic;

namespace MyStore.Infrastructure.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId, string username, string role,
            IDictionary<string, string> claims = null);
    }
}