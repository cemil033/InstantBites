using InstantBites.Application.Repositories.OrderCategoryRepository;
using InstantBites.Application.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Application.Features.Commands.OrderCategories.UpdateOrderCategory
{
    public class UpdateOrderCategoryCommandHandler : IRequestHandler<UpdateOrderCategoryCommandRequest, UpdateOrderCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateOrderCategoryCommandResponse> Handle(UpdateOrderCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ordercategory = await _unitOfWork.R_OrderCategory.Get(request.Id);
                ordercategory.Name = request.Name;
                var result = await _unitOfWork.W_OrderCategory.Update(ordercategory);
                if (result) await  _unitOfWork.W_OrderCategory.SaveChangesAsync();
                return new() { Success = result };
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
