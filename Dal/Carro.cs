using Carros.WebAPI.Dal.Repositories;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Carros.WebAPI.Dal
{
    public class Carro : ICarro
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public Carro()
        {
            string arquivoApiKey = @"nodeprojectimersao-firebase-adminsdk-dyqac-ca0b3da710.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", arquivoApiKey);
            projectId = "nodeprojectimersao";
            fireStoreDb = FirestoreDb.Create(projectId);
        }

        public string AddCarro(Model.Carro carro)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("carros");
                var id = colRef.AddAsync(carro).Result.Id;
                var shardRef = colRef.Document(id.ToString());
                shardRef.UpdateAsync("Id", id);
                return id;
            }
            catch
            {
                return "Error";
            }
        }

        public async void DeleteCarro(string id)
        {
            try
            {
                DocumentReference carroRef = fireStoreDb.Collection("carros").Document(id);
                await carroRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Model.Carro> GetCarroId(string id)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("carros").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Model.Carro carro = snapshot.ConvertTo<Model.Carro>();
                    carro.Id = snapshot.Id;
                    return carro;
                }
                else
                {
                    return new Model.Carro();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Model.Carro>> GetCarros()
        {
            try
            {
                Query carroQuery = fireStoreDb.Collection("carros");
                QuerySnapshot inscricaoQuerySnaphot = await carroQuery.GetSnapshotAsync();
                List<Model.Carro> listaCarros = new List<Model.Carro>();
                foreach (DocumentSnapshot documentSnapshot in inscricaoQuerySnaphot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        Model.Carro novoCarro = JsonConvert.DeserializeObject<Model.Carro>(json);
                        novoCarro.Id = documentSnapshot.Id;
                        listaCarros.Add(novoCarro);
                    }
                }
                List<Model.Carro> listaCarroOrdenada = listaCarros.OrderBy(x => x.Name).ToList();
                return listaCarroOrdenada;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                throw;
            }

        }

        public async void UpdateCarro(Model.Carro carro)
        {
            try
            {
                DocumentReference carroRef = fireStoreDb.Collection("carros").Document(carro.Id);
                await carroRef.SetAsync(carro, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }

        }
    }
}
