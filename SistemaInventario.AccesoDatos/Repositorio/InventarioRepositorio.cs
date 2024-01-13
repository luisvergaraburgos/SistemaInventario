using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class InventarioRepositorio : Repositorio<Inventario>, IInventarioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public InventarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Inventario inventario)
        {
            var inventarioDB = _db.Inventarios.FirstOrDefault(b => b.Id == inventario.Id);
            if(inventarioDB != null)
            {
                
                inventarioDB.BodegaId = inventario.BodegaId;
                inventarioDB.FechaFinal = inventario.FechaFinal;
                inventarioDB.Estado = inventario.Estado;
                _db.SaveChanges(); //se guardan los datos y actualiza los datos
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdawnLista(string obj)
        {
            if(obj == "Bodega")
            {
                return _db.Bodegas.Where(b => b.Estado == true).Select(b => new SelectListItem
                {
                    Text = b.Nombre,
                    Value = b.Id.ToString()
                });
            }
            return null;
        }
    }
}
