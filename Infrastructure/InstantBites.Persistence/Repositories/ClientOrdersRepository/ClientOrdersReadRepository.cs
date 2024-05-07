﻿using InstantBites.Application.Repositories.ClientOrdersRepository;
using InstantBites.Domain.Entites;
using InstantBites.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Persistence.Repositories.ClientOrdersRepository
{
    public class ClientOrdersReadRepository : ReadRepository<ClientOrders>, IClientOrdersReadRepository
    {
        public ClientOrdersReadRepository(AppDBContext context) : base(context)
        {
        }
        
    }
}
