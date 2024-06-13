using ConexionEF.Entities;
using ConexionEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConexionEF.Controllers
{
    public class MarcasController : Controller
    {
        public readonly ApplicationDbContext _context;

        public MarcasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MarcaAdd()
        {
            MarcaProducto entity = new MarcaProducto();
            entity.Id = new Guid();
            entity.Nombre = "Coca-Cola";
            entity.Descripcion = "Marca de grupo FAMSA";
            
            //Esto es para guardar en la base de datos.
            _context.MarcasProductos.Add(entity);
            _context.SaveChanges();

            return View("MarcaList");
        }

        public IActionResult MarcaList()
        {
            List<MarcaModel> list = 
            _context.MarcasProductos
                .Select(m => new MarcaModel()
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        Descripcion = m.Descripcion
                    })
                .ToList();

            return View(list);
        }
        [HttpGet]
        public IActionResult MarcaEdit(Guid Id)
        {
             var Marca =_context.MarcasProductos.FirstOrDefault(p=>p.Id==Id);
            if(Marca == null)
            {
                return RedirectToAction("MarcaList");
            }

            var model = new MarcaModel
            {
                Id = Marca.Id,
                Nombre = Marca.Nombre,
                Descripcion = Marca.Descripcion,
            };

            return View(model);
        }

        public IActionResult MarcasEdit(MarcaModel model)
        {
              if (ModelState.IsValid)
            {
                var Marca = _context.MarcasProductos.FirstOrDefault(p => p.Id == model.Id);
                if (Marca!=null)
                {
                    Marca.Nombre=model.Nombre;
                    Marca.Descripcion = model.Descripcion;

                    _context.SaveChanges();
                    return RedirectToAction("MarcaList");
                }
            }

            return View(model);
        }

        public IActionResult MarcaDelete()
        {
            return View();
        }

    }
}