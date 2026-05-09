using IdentityService.Services;
using System;

namespace IdentityService.Services;

public class TenantService : ITenantService
{
    public Guid GetTenantId()
    {
        return Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851");
    }
}
    //sabit bir id verelim, veri tabanında sıkıntı olmasın diye
