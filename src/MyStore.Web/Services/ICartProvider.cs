using System;
using MyStore.Web.Domain;

namespace MyStore.Web.Services
{
    public interface ICartProvider
    {
        Cart Get(Guid userId);
        void Set(Cart cart);
    }
}