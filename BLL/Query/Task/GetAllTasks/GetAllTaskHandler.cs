using AutoMapper;
using BLL.DTOs;
using DLA.Repositories.Contracts;
using MediatR;

namespace BLL.Query.Task.GetAllTasks;

public class GetAllTaskHandler : IRequestHandler<GetAllTask, List<TaskDTOs>>
{

    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTaskHandler(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TaskDTOs>> Handle(GetAllTask request, CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAll();
        return _mapper.Map<List<TaskDTOs>>(tasks);
    }
}