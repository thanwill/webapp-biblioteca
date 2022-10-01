namespace Biblioteca;

public class Emprestimo
{
    public int EmprestimoId { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Devolucao { get; set; }
    public bool Status { get; set; }
    public int Atrasos { get; set; }
    public double Custo { get; set; }

}