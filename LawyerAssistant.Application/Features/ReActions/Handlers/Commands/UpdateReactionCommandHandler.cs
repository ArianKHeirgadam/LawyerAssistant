using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.ReActions.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.ReActions.Handlers.Commands;

public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, SysResult>
{
    private readonly IRepository<ReActionModel> _repository;
    private readonly IRepository<FilesModel> _fileRepository;
    private readonly IRepository<SMSCenterModel> _smsRepository;
    private readonly IRepository<ActionTypesModel> _actionTypesRepository;
    private readonly ITransactionHandler _transactionHandler;
    private readonly IKavenegarSmsSenderService _kavenegarSmsSenderService;
    public UpdateReactionCommandHandler(IRepository<ReActionModel> repository, ITransactionHandler transactionHandler, IKavenegarSmsSenderService kavenegarSmsSenderService, IRepository<FilesModel> fileRepository, IRepository<ActionTypesModel> actionTypesRepository, IRepository<SMSCenterModel> smsRepository)
    {
        _repository = repository;
        _transactionHandler = transactionHandler;
        _kavenegarSmsSenderService = kavenegarSmsSenderService;
        _fileRepository = fileRepository;
        _actionTypesRepository = actionTypesRepository;
        _smsRepository = smsRepository;
    }

    public async Task<SysResult> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
    {
        var model = request.Dto;
        var reaction = await _repository.SelectAllAsQuerable().Include(c => c.Branch)
            .Include(c => c.Complexe).Include(c => c.ActionType).Include(c => c.Files)
            .FirstOrDefaultAsync(c => c.Id == model.Id);

        if (reaction == null) throw new CustomException(SystemCommonMessage.ReactionIsNotFound);

        if (model.IsRemember)
        {
            if (model.RememberTime != reaction.RememberTime || model.VisitTime != reaction.VisitTime)
            { 
                
            
            }
        }

    }
}
