namespace Web.Test.Entities
{
    public class Inventario
    {
        public Inventario(int id, string nome, int referencia, int quantidade, double preco)
        {
            Id = id;
            Nome = nome;
            Referencia = referencia;
            Quantidade = quantidade;
            Preco = preco;
        }

        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required int Referencia { get; set; }
        public required int Quantidade { get; set; }
        public required double Preco { get; set; }
    }
}
