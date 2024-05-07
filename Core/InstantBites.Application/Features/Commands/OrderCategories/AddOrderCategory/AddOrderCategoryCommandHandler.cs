using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.AddOrderCategory
{
    public class AddOrderCategoryCommandHandler : IRequestHandler<AddOrderCategoryCommandRequest, AddOrderCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddOrderCategoryCommandResponse> Handle(AddOrderCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.W_OrderCategory.AddAsync(new() { Id = Guid.NewGuid().ToString(), Name = request.Name });
                if (result)  await _unitOfWork.W_OrderCategory.SaveChangesAsync();
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
