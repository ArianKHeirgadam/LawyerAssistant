using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers.Commands;

public class UpdateComplexCommandHandler : IRequestHandler<UpdateComplexCommand, SysResult>
{
    private readonly IRepository<ComplexesModel> _repository;

    public UpdateComplexCommandHandler(IRepository<ComplexesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(UpdateComplexCommand request, CancellationToken cancellationToken)
    {
        var complex = await ValidateAndReturnComplexe(request);

        complex.Edit(request.Title, request.CityId);

        _repository.Update(complex);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }


    public async Task<ComplexesModel> ValidateAndReturnComplexe(UpdateComplexCommand request)
    {
        var complex = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (complex == null)
            throw new CustomException(SystemCommonMessage.ComplexeIsNotFound);


        var city = await _repository.FirstOrDefaultAsync(b => b.Id == request.CityId);

        if (city is null) throw new CustomException(SystemCommonMessage.CityIsNotFound);

        return complex;
    }
}