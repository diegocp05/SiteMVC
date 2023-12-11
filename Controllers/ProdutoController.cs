using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Context;
using SiteMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutosContext _context;

        public ProdutoController(ProdutosContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]       
        public IActionResult Criar(Produtos produto)
        {
            if (ModelState.IsValid)
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        public IActionResult Editar(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return RedirectToAction(nameof(Index));
            
            return View(produto);
        }

       [HttpPost]
        public IActionResult Editar(Produtos produto)
        {
            var produtosBanco = _context.Produtos.Find(produto.Id);

            produtosBanco.Nome = produto.Nome;
            produtosBanco.Preco = produto.Preco;
            produtosBanco.Ativo = produto.Ativo;

            _context.Produtos.Update(produtosBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
                return RedirectToAction(nameof(Index));
                
            return View(produto);
        }

        public IActionResult Deletar(int id)
        {
             var produto = _context.Produtos.Find(id);

            if (produto == null)
                return RedirectToAction(nameof(Index));
                
            return View(produto);
        }

        [HttpPost]
        public IActionResult Deletar(Produtos produto)
        {
            var produtosBanco = _context.Produtos.Find(produto.Id);
            
            _context.Produtos.Remove(produtosBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}