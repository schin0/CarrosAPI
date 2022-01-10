using Google.Cloud.Firestore;

namespace Carros.WebAPI.Model
{
    [FirestoreData]
    public class Carro
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public double Ano { get; set; }
    }
}
