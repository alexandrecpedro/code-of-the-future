using UnityOfWork.Entities;

namespace UnityOfWork;

class Program
{
    private static UnityOfWork.DAL.UnitOfWork unitOfWork
        = new DAL.UnitOfWork();
    // private static GenericRepository<Aluno> alunoRepository = default!;
    // private static GenericRepository<Curso> cursoRepository = default!;

    static void Main(string[] args)
    {
        
        // var context = new EscolaContext();
        // alunoRepository = new GenericRepository<Aluno>(context);
        // cursoRepository = new GenericRepository<Curso>(context);

        ListaAlunos();

        Console.WriteLine();
        Console.WriteLine("ALTERANDO ALUNO");
        Console.WriteLine("===============");
        var aluno3 = GetAlunoByID(3)!;
        aluno3.Sobrenome = "Andrade ALTERADO";
        UpdateAluno(aluno3);
        Save();
        ListaAlunos();

        Console.WriteLine();
        Console.WriteLine("ALTERANDO DE VOLTA");
        Console.WriteLine("===============");
        aluno3 = GetAlunoByID(3)!;
        aluno3.Sobrenome = "Andrade";
        UpdateAluno(aluno3);
        Save();
        ListaAlunos();

        Console.WriteLine();
        Console.WriteLine("INSERINDO ALUNO");
        Console.WriteLine("===============");
        var novoAluno = new Aluno { Nome = "Fulano", Sobrenome = "de Tal", DataMatricula = new DateTime(2022, 12, 20, 14, 55, 14) };
        InsertAluno(novoAluno);
        Save();

        ListaAlunos();

        Console.WriteLine();
        Console.WriteLine("REMOVENDO ALUNO");
        Console.WriteLine("===============");
        DeleteAluno(novoAluno.ID);
        Save();

        ListaAlunos();
        // ListaCursos();

        // context.Dispose();
        unitOfWork.Dispose();
    }

    private static void ListaAlunos()
    {
        Console.WriteLine();
        Console.WriteLine("Lista Alunos");
        Console.WriteLine("============");
        IEnumerable<Aluno> alunos = GetAlunos();
        foreach (var aluno in alunos)
        {
            Console.WriteLine($"""
            Aluno
            id={aluno.ID}
            nome='{aluno.Nome} {aluno.Sobrenome}'
            data de matrícula={aluno.DataMatricula}
            """);
        }
    }

    private static IEnumerable<Aluno> GetAlunos()
    {
        // return alunoRepository.GetAll().ToList();
        return unitOfWork.AlunoRepository.GetAll().ToList();
    }

    public static Aluno? GetAlunoByID(int id)
    {
        // return alunoRepository.GetByID(id);
        return unitOfWork.AlunoRepository.GetByID(id);
    }

    public static Aluno FindAlunoByName(string name)
    {
        // return alunoRepository.Find(entity => entity.Nome == name);
        return unitOfWork.AlunoRepository.Find(entity => entity.Nome == name);
    }

    public static Aluno InsertAluno(Aluno aluno)
    {
        // return alunoRepository.Insert(aluno);
        return unitOfWork.AlunoRepository.Insert(aluno);
    }

    public static void UpdateAluno(Aluno aluno)
    {
        // alunoRepository.Update(aluno);
        unitOfWork.AlunoRepository.Update(aluno);
    }

    public static void Save()
    {
        // alunoRepository.Save();
        unitOfWork.AlunoRepository.Save();
    }

    public static void DeleteAluno(int alunoID)
    {
        // alunoRepository.Delete(alunoID);
        unitOfWork.AlunoRepository.Delete(alunoID);
    }

    // private static void ListaCursos()
    // {
    //     Console.WriteLine();
    //     Console.WriteLine("Lista Cursos");
    //     Console.WriteLine("============");
    //     IEnumerable<Curso> cursos = GetCursos();
    //     foreach (var curso in cursos)
    //     {
    //         Console.WriteLine($"""
    //         Cursos
    //         id={curso.ID}
    //         nome={curso.Titulo}
    //         créditos={curso.Creditos}
    //         data de matrícula={curso.DataMatricula}
    //         """);
    //     }
    // }

    // private static IEnumerable<Curso> GetCursos()
    // {
    //     return cursoRepository.GetAll().ToList();
    //     return unitOfWork.CursoRepository.GetAll().ToList();
    // }
}
