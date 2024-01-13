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
    public class InventarioDetalleRepositorio : Repositorio<InventarioDetalle>, IInventarioDetalleRepositorio
    {
        private readonly ApplicationDbContext _db;

        public InventarioDetalleRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(InventarioDetalle inventarioDetalle)
        {
            var inventarioDetalleDB = _db.InventarioDetalles.FirstOrDefault(b => b.Id == inventarioDetalle.Id);
            if(inventarioDetalleDB != null)
            {

                inventarioDetalleDB.StockAnterior = inventarioDetalle.StockAnterior;
                inventarioDetalleDB.Cantidad = inventarioDetalle.Cantidad;

                _db.SaveChanges(); //se guardan los datos y actualiza los datos
            }
        }

      
    }
}
