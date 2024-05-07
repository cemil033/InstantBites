using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.DeleteOrderCategory
{
    public class DeleteOrderCategoryCommandHandler : IRequestHandler<DeleteOrderCategoryCommandRequest, DeleteOrderCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteOrderCategoryCommandResponse> Handle(DeleteOrderCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.W_OrderCategory.RemoveAsync(request.Id);
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
