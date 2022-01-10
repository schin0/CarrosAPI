namespace Carros.WebAPI.Dal.Repositories
{
    public interface ICarro
    {
        Task<List<Model.Carro>> GetCarros();
        string AddCarro(Model.Carro carro);
        void UpdateCarro(Model.Carro carro);
        Task<Model.Carro> GetCarroId(string id);
        void DeleteCarro(string id);
    }
}
