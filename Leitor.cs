namespace Biblioteca
{
    class Leitor
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }


        public Leitor(string nome, string cpf, string email, string telefone){
            this.Nome = nome;
            this.CPF = cpf;
            this.Email = email;
            this.Telefone = telefone;
        }
        
    }
}