namespace Application.Interfaces.InterfaceRepositories.NotarizationDetail
{
    public interface INotarizationDetailRepository : IGenericRepository<Domain.Entities.NotarizationDetail>
    {
        public Task AddManyNotarizationDetails(Guid id, Guid[] Docid);
    }
}
