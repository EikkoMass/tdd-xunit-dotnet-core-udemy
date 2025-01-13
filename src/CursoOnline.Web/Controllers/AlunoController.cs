using System.Linq;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly IRepositorio<Aluno> _cursoRepositorio;

        public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IRepositorio<Aluno> alunoRepositorio)
        {
            _armazenadorDeAluno = armazenadorDeAluno;
            _cursoRepositorio = alunoRepositorio;
        }

        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            if (cursos.Any())
            {
                var dtos = cursos.Select(c => new AlunoParaListagemDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Cpf = c.Cpf, 
                    PublicoAlvo = c.PublicoAlvo.ToString(),
                    Email = c.Email
                });
                return View("Index", PaginatedList<AlunoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<AlunoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var aluno = _cursoRepositorio.ObterPorId(id);
            var dto = new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                PublicoAlvo = aluno.PublicoAlvo.ToString(),
                Email = aluno.Email
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new AlunoDto());
        }

        [HttpPost]
        public IActionResult Salvar(AlunoDto model)
        {
            _armazenadorDeAluno.Armazenar(model);
            return Ok();
        }
    }
}