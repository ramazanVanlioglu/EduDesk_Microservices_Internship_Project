using System;
namespace IdentityService.Services;

public interface ITenantService
{
    Guid GetTenantId();
}

//tenant kurum id'sini getirecek arayüz